using AutoMapper;
using Business.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Model.Models.Announcement;
using Repository.Interfaces;
using Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Managers
{
    public class AnnouncementManager : IAnnouncementManager
    {
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        private readonly IAnnouncementRepository _announcementRepository;
        public AnnouncementManager(
            IMapper mapper,
            IEmailService emailService,
            IUserRepository userRepository,
            IAnnouncementRepository announcementRepository)
        {
            _mapper = mapper;
            _emailService = emailService;
            _userRepository = userRepository;
            _announcementRepository = announcementRepository;
        }

        public async Task<bool> ExistsAsync(int residentialId, int announcementId)
        {
            return await _announcementRepository.ExistsAsync(residentialId, announcementId);
        }

        public async Task<IEnumerable<Announcement>> GetAllAsync(int residentialId)
        {
            var entities = await _announcementRepository
                .AsNoTracking().Include(x => x.Creator)
                .Where(x => x.ResidentialId == residentialId)
                .OrderByDescending(x => x.CreationDate).ToListAsync();

            return _mapper.Map<IEnumerable<Announcement>>(entities);
        }

        public async Task<AnnouncementToUpdate> GetByIdAsync(int id)
        {
            var entity = await _announcementRepository.GetByIdAsync(id);

            return _mapper.Map<AnnouncementToUpdate>(entity);
        }

        public async Task UpdateAsync(AnnouncementToUpdate model)
        {
            var entity = await _announcementRepository.GetByIdAsync(model.Id);

            _mapper.Map(model, entity);

            await _announcementRepository.UpdateAsync(entity);

            if (model.CanSendEmail)
            {
                await SendEmailAsync(entity);
            }
        }

        public async Task AddAsync(AnnouncementToAdd model)
        {
            var entity = _mapper.Map<AnnouncementEf>(model);

            await _announcementRepository.AddAsync(entity);

            if (model.CanSendEmail)
            {
                await SendEmailAsync(entity);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _announcementRepository.DeleteAsync(id);
        }

        public async Task ResendEmailAsync(int announcementId)
        {
            var entity = await _announcementRepository.GetByIdAsync(announcementId);

            await SendEmailAsync(entity);
        }

        private async Task SendEmailAsync(AnnouncementEf entity)
        {

            var emails = await _userRepository.GetEmailsAsync(entity.ResidentialId);

            await _emailService.SendAnnouncementAsync(entity, emails);
        }
    }

}
