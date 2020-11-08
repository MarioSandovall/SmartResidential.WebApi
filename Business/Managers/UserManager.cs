using AutoMapper;
using Business.Interfaces;
using Business.Utils;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using Model.Models.User;
using Repository.Extensions;
using Repository.Interfaces;
using Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;
        private readonly IEmailService _emailService;

        public UserManager(
            IMapper mapper,
            IUserRepository repository,
            IEmailService emailService)
        {
            _mapper = mapper;
            _repository = repository;
            _emailService = emailService;
        }

        public async Task<bool> ExistsAsync(int residentialId, int userId)
        {
            return await _repository.ExistsAsync(residentialId, userId);
        }

        public async Task<bool> ExistsEmailAsync(int residentialId, string email)
        {
            return await _repository.ExistsEmailAsync(residentialId, email);
        }

        public async Task<bool> ExistsEmailAsync(int residentialId, int userId, string email)
        {
            return await _repository.ExistsEmailAsync(residentialId, userId, email);
        }

        public async Task<bool> IsAdminAsync(int residentialId, int userId)
        {
            return await _repository.IsAdminAsync(residentialId, userId);
        }

        public async Task<PaginatedList<User>> GetAllAsync(int residenitalId, Parameter parameter)
        {

            var columnsToFilter = ColumnFilter.GetNames<UserEf>(x => x.Name, x => x.LastName, x => x.Email);

            var query = _repository.AsNoTracking()
                .Where(x => x.ResidentialId == residenitalId)
                .Include(x => x.Residential).Include(x => x.UserRoles).ThenInclude(x => x.Role)
                .Contains(parameter.Filter, columnsToFilter).ApplySort(parameter.SortBy, parameter.IsSortDesc);

            var totalOfItems = await query.CountAsync();
            var entities = await query.Take(parameter.Page, parameter.ItemsPerPage).ToListAsync();

            var models = _mapper.Map<IEnumerable<User>>(entities);
            return new PaginatedList<User>(models, totalOfItems);
        }

        public async Task<UserToUpdate> GetByIdAsync(int userId)
        {
            var entity = await _repository.GetByIdAsync(userId);

            return _mapper.Map<UserToUpdate>(entity);
        }

        public async Task UpdateAsync(UserToUpdate model)
        {
            var entity = await _repository.GetByIdAsync(model.Id);

            _mapper.Map(model, entity);

            await _repository.UpdateAsync(entity);
        }

        public async Task AddAsync(UserToAdd model)
        {
            var entity = _mapper.Map<UserEf>(model);

            await _repository.AddAsync(entity);

            if (model.CanSendInvitationEmail)
            {
                var user = await _repository.GetByIdWithResidentialAsync(entity.Id);

                await _emailService.SendInvitationAsync(user);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task ResendInvitationAsync(int userId)
        {
            var user = await _repository.GetByIdWithResidentialAsync(userId);

            await _emailService.SendInvitationAsync(user);
        }

        public async Task<IEnumerable<FilteredUser>> SearchAsync(int residenitalId, string name)
        {
            var columnsToFilter = ColumnFilter.GetNames<UserEf>(x => x.Name, x => x.LastName, x => x.Email);

            var entities = await _repository.AsNoTracking()
                .Where(x => x.ResidentialId == residenitalId)
                .Contains(name, columnsToFilter).ToListAsync();

            return _mapper.Map<IEnumerable<FilteredUser>>(entities);
        }

    }
}
