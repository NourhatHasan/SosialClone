using AutoMapper;
using sosialClone;


namespace RepositoryAplication.Tools
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Entities, Entities>();
        }
    }
}
