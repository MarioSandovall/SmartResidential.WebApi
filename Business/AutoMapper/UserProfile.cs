using AutoMapper;
using Business.Extensions;
using Domain.Entities;
using Model.Models.User;
using System.Linq;

namespace Business.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEf, User>().ConvertUsing(ConvertFrom_Entity_To_Model);
            CreateMap<UserToAdd, UserEf>().ConvertUsing(ConvertFrom_UserToAdd_To_Entity);
            CreateMap<UserEf, UserToUpdate>().ConvertUsing(ConvertFrom_Entity_To_UserToUpdate);
            CreateMap<UserToUpdate, UserEf>().ConvertUsing(ConvertFrom_UserToUpdate_To_Entity);
            CreateMap<UserEf, FilteredUser>().ConvertUsing(ConvertFrom_Entity_To_FilteredUser);
        }

        private static User ConvertFrom_Entity_To_Model(UserEf entity, User model)
        {
            var instance = model ?? new User();

            instance.Id = entity.Id;
            instance.Name = entity.Name;
            instance.Email = entity.Email;
            instance.LastName = entity.LastName;
            instance.RoleNames = string.Join(", ", entity.UserRoles.Select(x => x.Role.Name).ToArray());

            return instance;
        }

        private static UserEf ConvertFrom_UserToAdd_To_Entity(UserToAdd model, UserEf entity)
        {
            var instance = entity ?? new UserEf();

            instance.Name = model.Name.RemoveSpace();
            instance.Email = model.Email.RemoveSpace();
            instance.Password = model.Password.RemoveSpace();
            instance.LastName = model.LastName.RemoveSpace();
            instance.CellPhone = model.CellPhone.RemoveSpace();
            instance.LandPhone = model.LandPhone.RemoveSpace();
            instance.ResidentialId = model.ResidentialId;

            instance.UserRoles = model.Roles.Select(roleId => new UserRoleEf { RoleId = roleId }).ToList();

            return instance;
        }

        private static UserToUpdate ConvertFrom_Entity_To_UserToUpdate(UserEf entity, UserToUpdate model)
        {
            var instance = model ?? new UserToUpdate();

            instance.Id = entity.Id;
            instance.Name = entity.Name;
            instance.Email = entity.Email;
            instance.Password = entity.Password;
            instance.LastName = entity.LastName;
            instance.CellPhone = entity.CellPhone;
            instance.LandPhone = entity.LandPhone;

            instance.Roles = entity?.UserRoles.Select(x => x.RoleId).ToList();

            return instance;
        }

        private static UserEf ConvertFrom_UserToUpdate_To_Entity(UserToUpdate model, UserEf entity)
        {
            var instance = entity ?? new UserEf();

            instance.Name = model.Name.RemoveSpace();
            instance.Email = model.Email.RemoveSpace();
            instance.Password = model.Password.RemoveSpace();
            instance.LastName = model.LastName.RemoveSpace();
            instance.CellPhone = model.CellPhone.RemoveSpace();
            instance.LandPhone = model.LandPhone.RemoveSpace();

            instance.UserRoles.Clear();
            instance.UserRoles = model.Roles
                .Select(roleId => new UserRoleEf { RoleId = roleId, UserId = model.Id }).ToList();

            return instance;
        }

        private FilteredUser ConvertFrom_Entity_To_FilteredUser(UserEf entity, FilteredUser model)
        {
            var instance = model ?? new FilteredUser();

            instance.Id = entity.Id;
            instance.Email = entity.Email;
            instance.FullName = $"{entity.Name} {entity.LastName}";

            return instance;
        }

    }
}
