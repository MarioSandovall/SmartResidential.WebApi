using AutoMapper;
using Domain.Entities;
using Model.Models.Login;
using System.Linq;

namespace Business.AutoMapper
{
    public class UserLoginProfile : Profile
    {
        public UserLoginProfile()
        {
            CreateMap<UserEf, UserLogin>().ConvertUsing(ConvertFrom_Entity_To_Model);
        }

        private static UserLogin ConvertFrom_Entity_To_Model(UserEf entity, UserLogin model)
        {
            var instance = model ?? new UserLogin();

            instance.Id = entity.Id;
            instance.Name = entity.Name;
            instance.Email = entity.Email;
            instance.LastName = entity.LastName;
            instance.CellPhone = entity.CellPhone;
            instance.LandPhone = entity.LandPhone;
            instance.ResidentialId = entity.ResidentialId;
            instance.Residential = entity.Residential?.Name;
            instance.RoleIds = entity.UserRoles.Select(x => x.Role.Id).ToList();
            instance.Roles = entity.UserRoles.Select(x => x.Role?.Name).ToList();

            return instance;
        }

    }
}
