using AutoMapper;
using Business.Extensions;
using Domain.Entities;
using Model.Models.House;
using Model.Models.User;
using System;
using System.Linq;

namespace Business.AutoMapper
{
    public class HouseProfile : Profile
    {
        public HouseProfile()
        {
            CreateMap<HouseEf, House>().ConvertUsing(ConvertFrom_Entity_To_Model);
            CreateMap<HouseToAdd, HouseEf>().ConvertUsing(ConvertFrom_HouseToAdd_To_Entity);
            CreateMap<HouseToUpdate, HouseEf>().ConvertUsing(ConvertFrom_Entity_To_HouseToUpdate);
            CreateMap<HouseEf, HouseToUpdate>().ConvertUsing(ConvertFrom_HouseToUpdate_To_Entity);
        }

        private static House ConvertFrom_Entity_To_Model(HouseEf entity, House model)
        {
            var instance = model ?? new House();

            instance.Id = entity.Id;
            instance.Name = entity.Name;
            instance.Street = $"{entity.Street} {entity.OutdoorNumber} {entity.InteriorNumber}";
            instance.Users = string.Join(", ", entity.HouseUsers?.Select(x => $"{x.User.Name} {x.User.LastName}").ToArray());

            return instance;
        }

        private static HouseEf ConvertFrom_HouseToAdd_To_Entity(HouseToAdd model, HouseEf entity)
        {
            var instance = entity ?? new HouseEf();

            instance.IsActive = true;
            instance.CreationDate = DateTime.Now;
            instance.Name = model.Name.RemoveSpace();
            instance.Street = model.Street.RemoveSpace();
            instance.ResidentialId = model.ResidentialId;
            instance.OutdoorNumber = model.OutdoorNumber.RemoveSpace();
            instance.InteriorNumber = model.InteriorNumber.RemoveSpace();

            instance.HouseUsers = model.Users?
                .Select(user => new HouseUserEf { UserId = user.Id }).ToList();

            return instance;
        }

        private static HouseEf ConvertFrom_Entity_To_HouseToUpdate(HouseToUpdate model, HouseEf entity)
        {
            var instance = entity ?? new HouseEf();

            instance.Name = model.Name.RemoveSpace();
            instance.Street = model.Street.RemoveSpace();
            instance.OutdoorNumber = model.OutdoorNumber.RemoveSpace();
            instance.InteriorNumber = model.InteriorNumber.RemoveSpace();

            instance.HouseUsers.Clear();
            instance.HouseUsers = model.Users?
                .Select(user => new HouseUserEf { UserId = user.Id, HouseId = model.Id }).ToList();

            return instance;
        }

        private static HouseToUpdate ConvertFrom_HouseToUpdate_To_Entity(HouseEf entity, HouseToUpdate model)
        {
            var instance = model ?? new HouseToUpdate();

            instance.Id = entity.Id;
            instance.Name = entity.Name;
            instance.Street = entity.Street;
            instance.IsActive = entity.IsActive;
            instance.OutdoorNumber = entity.OutdoorNumber;
            instance.ResidentialId = entity.ResidentialId;
            instance.InteriorNumber = entity.InteriorNumber;

            instance.Users = entity.HouseUsers?.Select(
                houseUser => new FilteredUser
                {
                    Id = houseUser.User.Id,
                    Email = houseUser.User.Email,
                    FullName = $"{houseUser.User.Name} {houseUser.User.LastName}"
                }).ToList();


            return instance;
        }

    }
}
