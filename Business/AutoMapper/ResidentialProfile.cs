using AutoMapper;
using Domain.Entities;
using Model.Models.Residential;

namespace Business.AutoMapper
{
    public class ResidentialProfile : Profile
    {
        public ResidentialProfile()
        {
            CreateMap<ResidentialToAdd, ResidentialEf>().ConvertUsing(ConvertFrom_ResidentialToAdd_To_Entity);
            CreateMap<ResidentialToUpdate, ResidentialEf>().ConvertUsing(ConvertFrom_ResidentialToUpdate_To_Entity);
            CreateMap<ResidentialEf, ResidentialToUpdate>().ConvertUsing(ConvertFrom_Entity_To_ResidentialToUpdate);
        }

        private static ResidentialEf ConvertFrom_ResidentialToAdd_To_Entity(ResidentialToAdd model, ResidentialEf entity)
        {
            var instance = entity ?? new ResidentialEf();

            instance.Name = model.Name;
            instance.Address = model.Address;
            instance.Cellphone = model.Cellphone;
            instance.LandPhone = model.LandPhone;
            instance.LogoPath = model.LogoPath;
            instance.ResidentialStatusId = model.StatusId;

            return instance;
        }

        private static ResidentialEf ConvertFrom_ResidentialToUpdate_To_Entity(ResidentialToUpdate model, ResidentialEf entity)
        {
            var instance = entity ?? new ResidentialEf();

            instance.Id = model.Id;
            instance.Name = model.Name;
            instance.Address = model.Address;
            instance.LogoPath = model.LogoPath;
            instance.Cellphone = model.Cellphone;
            instance.LandPhone = model.LandPhone;
            instance.ResidentialStatusId = model.StatusId;

            return instance;
        }

        private static ResidentialToUpdate ConvertFrom_Entity_To_ResidentialToUpdate(ResidentialEf entity, ResidentialToUpdate model)
        {
            var instance = model ?? new ResidentialToUpdate();

            instance.Id = entity.Id;
            instance.Name = entity.Name;
            instance.Address = entity.Address;
            instance.LogoPath = entity.LogoPath;
            instance.Cellphone = entity.Cellphone;
            instance.LandPhone = entity.LandPhone;
            instance.StatusId = entity.ResidentialStatusId;

            return instance;

        }

    }
}
