using AutoMapper;
using ProjectManager.Entities.DTO;
using ProjectManager.Entities.Models;

namespace ProjectManager.API
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForAuth, User>();
        }
    }
}