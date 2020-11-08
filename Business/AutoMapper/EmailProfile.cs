using AutoMapper;
using Domain.Entities;
using Model.Models.Email;

namespace Business.AutoMapper
{
    public class EmailProfile : Profile
    {
        public EmailProfile()
        {
            CreateMap<UserEf, InvitationEmail>().ConvertUsing(ConvertFrom_UserEf_To_InvitationEmail);
        }

        private static InvitationEmail ConvertFrom_UserEf_To_InvitationEmail(UserEf entity, InvitationEmail model)
        {
            var instance = model ?? new InvitationEmail();

            instance.Email = entity.Email;
            instance.UserName = entity.Name;
            instance.Password = entity.Password;
            instance.Residential = entity.Residential?.Name;

            return instance;
        }
    }
}
