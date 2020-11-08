using AutoMapper;
using Business.Extensions;
using Domain.Entities;
using Model.Models.Announcement;
using System;

namespace Business.AutoMapper
{
    public class AnnouncementProfile : Profile
    {
        public AnnouncementProfile()
        {
            CreateMap<AnnouncementEf, Announcement>().ConvertUsing(ConvertFrom_Entity_To_Model);
            CreateMap<AnnouncementToAdd, AnnouncementEf>().ConvertUsing(ConvertFrom_AnnouncementToAdd_To_Entity);
            CreateMap<AnnouncementToUpdate, AnnouncementEf>().ConvertUsing(ConvertFrom_AnnouncementToUpdate_To_Entity);
            CreateMap<AnnouncementEf, AnnouncementToUpdate>().ConvertUsing(ConvertFrom_Entity_To_AnnouncementToUpdate);
        }

        private static Announcement ConvertFrom_Entity_To_Model(AnnouncementEf entity, Announcement model)
        {
            var instance = model ?? new Announcement();

            instance.Id = entity.Id;
            instance.Title = entity.Title;
            instance.Description = entity.Description;
            instance.Information = $"{entity.Creator?.Name} {entity.Creator?.LastName} - { entity.CreationDate.ToString("yyyy/MM/dd HH:mm")}";

            return instance;
        }

        private static AnnouncementEf ConvertFrom_AnnouncementToAdd_To_Entity(AnnouncementToAdd model, AnnouncementEf entity)
        {
            var instance = entity ?? new AnnouncementEf();

            instance.CreatorId = model.UserId;
            instance.CreationDate = DateTime.Now;
            instance.Title = model.Title.RemoveSpace();
            instance.ResidentialId = model.ResidentialId;
            instance.Description = model.Description.RemoveSpace();

            return instance;
        }

        private AnnouncementEf ConvertFrom_AnnouncementToUpdate_To_Entity(AnnouncementToUpdate model, AnnouncementEf entity)
        {
            var instance = entity ?? new AnnouncementEf();

            instance.CreatorId = model.UserId;
            instance.CreationDate = DateTime.Now;
            instance.Title = model.Title.RemoveSpace();
            instance.Description = model.Description.RemoveSpace();

            return instance;
        }

        private AnnouncementToUpdate ConvertFrom_Entity_To_AnnouncementToUpdate(AnnouncementEf entity, AnnouncementToUpdate model)
        {
            var instance = model ?? new AnnouncementToUpdate();

            instance.Id = entity.Id;
            instance.Title = entity.Title;
            instance.Description = entity.Description;

            return instance;
        }

    }
}
