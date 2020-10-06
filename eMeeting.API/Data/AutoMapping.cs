using AutoMapper;
using eMeeting.Model;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<MeetingModel, Meeting>(); // means you want to map from User to UserDTO
    }
}