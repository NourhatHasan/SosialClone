
using AutoMapper;
using RepositoryAplication.Activities;
using RepositoryAplication.DTO;
using RepositoryAplication.DTOs;
using sosialClone;


namespace RepositoryAplication.Tools
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Entities, Entities>();
            CreateMap<Entities, ActivityDTO>()
                .ForMember(x => x.HostUsername, o => 
                o.MapFrom(a => a.Attendies.FirstOrDefault(x => x.isHost).AppUser.UserName));

            CreateMap<EntityUser, AtendeeDTO>()
                .ForMember(x => x.username, o => o.MapFrom(a => a.AppUser.UserName))
                .ForMember(x => x.DisplayName, o => o.MapFrom(a => a.AppUser.DisplayName))
                .ForMember(x => x.Bio, o => o.MapFrom(a => a.AppUser.Bio))
               // .ForMember(x=>x.photos, o=>o.MapFrom(a=>a.AppUser.photos.ToList()))
            .ForMember(x => x.Picture, o => o.MapFrom(a => a.AppUser.photos.FirstOrDefault(l => l.IsMain).Url));
            

            CreateMap<AppUser, ProfileDTO>()
                .ForMember(x => x.Picture, o =>
                o.MapFrom(a => a.photos.FirstOrDefault(x => x.IsMain).Url));

           
        }
    }
}
