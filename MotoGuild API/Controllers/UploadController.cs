using Data;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MotoGuild_API.Controllers;

[ApiController]
[Route("api/upload")]
public class UploadController : ControllerBase
{

    private IWebHostEnvironment _hostingEnvironment;

    public UploadController(IWebHostEnvironment hostingEnvironment)
    {
        _hostingEnvironment = hostingEnvironment;
    }


    [HttpPost ("{folderName}")]
    public IActionResult UploadImages(string folderName)
    {
        try
        {
            var files = HttpContext.Request.Form.Files;
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    FileInfo fileInfo = new FileInfo(file.FileName);
                    var newfilename = "Image_" + DateTime.Now.Ticks + fileInfo.Extension;
                    var path = Path.Combine("",
                        _hostingEnvironment.ContentRootPath + "\\Uploads\\" + folderName + "\\" + newfilename);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok(newfilename);
                }
            }
            else
            {
                BadRequest("No files selected to upload");
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return NotFound();
    }


    [HttpGet("{folderName}/{imageName}")]
    public FileContentResult GetImageUpload(string folderName,string imageName)
    {
        string imagePath = Path.Combine("",
            _hostingEnvironment.ContentRootPath + "\\Uploads\\"+folderName+"\\" + imageName);
        string[] ext = imagePath.Split(".");

        if (System.IO.File.Exists(imagePath))
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
            return File(imageBytes, $"image/{ext.Last()}");
        }


        string placeholderImagePath = Path.Combine("",
            _hostingEnvironment.ContentRootPath + "\\Uploads\\noimage.jpg");
        byte[] placeholderImageBytes = System.IO.File.ReadAllBytes(placeholderImagePath);
        return File(placeholderImageBytes, $"image/jpg");
    }


    [HttpDelete("{folderName}/{imageName}")]
    public IActionResult DeleteImageUpload(string folderName, string imageName)
    {
        string imagePath = Path.Combine("",
            _hostingEnvironment.ContentRootPath + "\\Uploads\\" + folderName + "\\" + imageName);

        if (System.IO.File.Exists(imagePath))
        {
            System.IO.File.Delete(imagePath);
            return NoContent();
        }

        return BadRequest("File not exist");
    }

}