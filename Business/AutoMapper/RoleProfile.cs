using AutoMapper;
using Domain.Entities;
using Model.Models.Role;

namespace Business.AutoMapper
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleEf, Role>().ConvertUsing(ConvertFrom_Entity_To_Model);
        }

        private static Role ConvertFrom_Entity_To_Model(RoleEf entity, Role model)
        {
            var instance = model ?? new Role();

            instance.Id = entity.Id;
            instance.Name = entity.Name;

            return instance;
        }

    }
}
