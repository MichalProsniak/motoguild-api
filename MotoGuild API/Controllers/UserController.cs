using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MotoGuild_API.Dto.UserDtos;
using MotoGuild_API.Repository.Interface;

namespace MotoGuild_API.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly ILoggedUserRepository _loggedUserRepository;

    public UserController(IUserRepository userRepository, IMapper mapper, IConfiguration configuration, ILoggedUserRepository loggedUserRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _configuration = configuration;
        _loggedUserRepository = loggedUserRepository;
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = _userRepository.GetAll();
        return Ok(_mapper.Map<List<UserDto>>(users));
    }
   
    [HttpGet("{id:int}", Name = "GetUser")]
    public IActionResult GetUser(int id, [FromQuery] bool profile = false)
    {
        var user = _userRepository.Get(id);
        if (!profile)
        {
            if (user == null) return NotFound();
            return Ok(_mapper.Map<UserDto>(user));
        }
        else
        {
            if (user == null) return NotFound();
            foreach(var group in user.Groups)
            {
                group.Rating = group.Participants.Average(p => p.Rating);
            }
            return Ok(_mapper.Map<UserProfileDto>(user));
        }
    }


    [HttpPost("register")]
    public IActionResult RegisterUser([FromBody] CreateUserDto createUserDto)
    {
        if (_userRepository.UserNameExist(createUserDto.UserName) || _userRepository.UserEmailExist(createUserDto.Email))
        {
            return BadRequest("User already exists.");
        }
        CreatePasswordHash(createUserDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
        var user = _mapper.Map<User>(createUserDto);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        user.Role = "User";
        _userRepository.Insert(user);
        _userRepository.Save();
        var userDto = _mapper.Map<UserDto>(user);
        return CreatedAtRoute("GetUser", new {id = userDto.Id}, userDto);
    }

    [HttpPost("login")]
    public IActionResult LoginUser([FromBody] LoginUserDto loginUserDto)
    {
        if (!_userRepository.UserNameExist(loginUserDto.UserName))
        {
            return BadRequest("User not found.");
        }

        var user = _userRepository.GetUserByName(loginUserDto.UserName);

        if (!VerifyPasswordHash(loginUserDto.Password, user.PasswordHash, user.PasswordSalt))
        {
            return BadRequest("Wrong password.");
        }

        string token = CreateToken(user);

        //var refreshToken = GenerateRefreshToken();
        //SetRefreshToken(refreshToken, user);

        return Ok(token);
    }

    //[HttpPost("refresh-token")]
    //public IActionResult RefreshToken()
    //{
    //    string refreshToken = "";

    //    foreach (var header in Request.Headers)
    //    {
    //        if (header.Key == "x-refreshtoken")
    //        {
    //            refreshToken = header.Value;
    //        }
    //    }

    //    var user = _userRepository.FindUserByRefreshToken(refreshToken);
    //    if (user == null)
    //    {
    //        return Unauthorized("Invalid Refresh Token.");
    //    }

    //    if (user.TokenExpires < DateTime.Now)
    //    {
    //        return Unauthorized("Token expired.");
    //    }

    //    string token = CreateToken(user);
    //    var newRefreshToken = GenerateRefreshToken();
    //    SetRefreshToken(newRefreshToken, user);

    //    return Ok(new { token, newRefreshToken});
    //}

    //private RefreshToken GenerateRefreshToken()
    //{
    //    var refreshToken = new RefreshToken
    //    {
    //        Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
    //        Expires = DateTime.Now.AddDays(7),
    //        Created = DateTime.Now
    //    };

    //    return refreshToken;
    //}

    //private void SetRefreshToken(RefreshToken newRefreshToken, User user)
    //{
    //    var cookieOptions = new CookieOptions
    //    {
    //        HttpOnly = false,
    //        Expires = newRefreshToken.Expires
    //    };
    //    Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

    //    user.RefreshToken = newRefreshToken.Token;
    //    user.TokenCreated = newRefreshToken.Created;
    //    user.TokenExpires = newRefreshToken.Expires;
    //    _userRepository.Save();

    //}

    [Authorize]
    [HttpGet("logged")]
    public IActionResult GetLogged()
    {
        var userName = _loggedUserRepository.GetLoggedUserName();
        var user = _userRepository.GetUserByName(userName);
        var userDto = _mapper.Map<UserDto>(user);

        return Ok(userDto);
    }


    [HttpDelete("{id:int}")]
    public IActionResult DeleteUser(int id)
    {
        var user = _userRepository.Get(id);
        if (user == null) return NotFound();
        _userRepository.Delete(id);
        _userRepository.Save();
        return Ok();
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
    {
        var user = _userRepository.Get(id);
        if (user == null) return NotFound();
        _mapper.Map(updateUserDto, user);
        _userRepository.Update(user);
        _userRepository.Save();
        return NoContent();
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.SerialNumber, user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("Jwt:Key").Value));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(3),
            signingCredentials: cred
            );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}