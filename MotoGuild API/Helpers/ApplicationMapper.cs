using AutoMapper;
using Domain;
using MotoGuild_API.Dto.CommentDtos;
using MotoGuild_API.Dto.EventDtos;
using MotoGuild_API.Dto.FeedDtos;
using MotoGuild_API.Dto.GroupDtos;
using MotoGuild_API.Dto.PostDtos;
using MotoGuild_API.Dto.RideDtos;
using MotoGuild_API.Dto.RouteDtos;
using MotoGuild_API.Dto.StopDtos;
using MotoGuild_API.Dto.UserDtos;
using Route = Domain.Route;

namespace MotoGuild_API.Helpers;

public class ApplicationMapper : Profile
{
    public ApplicationMapper()
    {
        //User
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<CreateUserDto, User>().ReverseMap();
        CreateMap<UpdateUserDto, User>().ReverseMap();

        //Group
        CreateMap<Group, SelectedGroupDto>().ReverseMap();
        CreateMap<CreateGroupDto, Group>().ReverseMap();
        CreateMap<UpdateGroupDto, Group>().ReverseMap();
        CreateMap<GroupDto, Group>().ReverseMap();

        //Event
        CreateMap<CreateEventDto, Event>().ReverseMap();
        CreateMap<Event, EventDto>().ReverseMap();
        CreateMap<UpdateEventDto, Event>().ReverseMap();

        //Ride
        CreateMap<Ride, RideDto>().ReverseMap();
        CreateMap<CreateRideDto, Ride>().ReverseMap();

        //Route
        CreateMap<Route, RouteDto>().ReverseMap();
        CreateMap<Route, FullRouteDto>().ReverseMap();
        CreateMap<CreateRouteDto, Route>().ReverseMap();
        CreateMap<UpdateRouteDto, Route>().ReverseMap();

        //Post
        CreateMap<Post, PostDto>().ReverseMap();
        CreateMap<Post, CreatePostDto>().ReverseMap();

        //Stop
        CreateMap<Stop, StopDto>().ReverseMap();
        CreateMap<Stop, CreateStopDto>().ReverseMap();
        CreateMap<Stop, UpdateStopDto>().ReverseMap();

        //Comment
        CreateMap<Comment, CommentDto>().ReverseMap();
        CreateMap<Comment, CreateCommentDto>().ReverseMap();

        //Feed
        CreateMap<FeedDto, Feed>().ReverseMap();

        //Profile
        CreateMap<UserProfileDto, User>().ReverseMap();
        CreateMap<UserProfileDataDto, User>().ReverseMap();
        CreateMap<GroupUserProfilDto, Group>().ReverseMap();
        CreateMap<EventUserProfileDto, Event>().ReverseMap();
        CreateMap<RideUserProfileDto, Ride>().ReverseMap();
        CreateMap<RouteUserProfilDto, Route>().ReverseMap();
    }
}