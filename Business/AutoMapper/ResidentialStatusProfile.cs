using AutoMapper;
using Domain.Entities;
using Model.Models.Residential;

namespace Business.AutoMapper
{
    public class ResidentialStatusProfile : Profile
    {
        public ResidentialStatusProfile()
        {
            CreateMap<ResidentialStatusEf, ResidentialStatus>().ConvertUsing(ConvertFrom_Model_To_Entity);
        }

        private ResidentialStatus ConvertFrom_Model_To_Entity(ResidentialStatusEf entity, ResidentialStatus model)
        {
            var instance = model ?? new ResidentialStatus();

            instance.Id = entity.Id;
            instance.Name = entity.Name;

            return instance;
        }

    }
}
