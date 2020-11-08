using AutoMapper;
using Business.Extensions;
using Domain.Entities;
using Model.Models.ExpenseCategory;

namespace Business.AutoMapper
{
    public class ExpenseCategoryProfile : Profile
    {
        public ExpenseCategoryProfile()
        {
            CreateMap<ExpenseCategoryEf, ExpenseCategory>().ConvertUsing(ConvertFrom_Entity_To_HouseModel);
            CreateMap<ExpenseCategoryToAdd, ExpenseCategoryEf>().ConvertUsing(ConvertFrom_ExpenseCategoryToAdd_To_Entity);
            CreateMap<ExpenseCategoryToUpdate, ExpenseCategoryEf>().ConvertUsing(ConvertFrom_ExpenseCategoryToUpdate_To_Entity);
            CreateMap<ExpenseCategoryEf, ExpenseCategoryToUpdate>().ConvertUsing(ConvertFrom_Entity_To_CategoryToUpdate);
        }

        private static ExpenseCategory ConvertFrom_Entity_To_HouseModel(ExpenseCategoryEf entity, ExpenseCategory model)
        {
            var instance = model ?? new ExpenseCategory();

            instance.Id = entity.Id;
            instance.Name = entity.Name;
            instance.Description = entity.Description;

            return instance;
        }

        private ExpenseCategoryEf ConvertFrom_ExpenseCategoryToAdd_To_Entity(ExpenseCategoryToAdd model, ExpenseCategoryEf entity)
        {
            var instance = entity ?? new ExpenseCategoryEf();

            instance.Name = model.Name.RemoveSpace();
            instance.Description = model.Description.RemoveSpace();
            instance.ResidentialId = model.ResidentialId;

            return instance;
        }

        private ExpenseCategoryEf ConvertFrom_ExpenseCategoryToUpdate_To_Entity(ExpenseCategoryToUpdate model, ExpenseCategoryEf entity)
        {
            var instance = entity ?? new ExpenseCategoryEf();

            instance.Name = model.Name.RemoveSpace();
            instance.Description = model.Description.RemoveSpace();

            return instance;
        }

        private ExpenseCategoryToUpdate ConvertFrom_Entity_To_CategoryToUpdate(ExpenseCategoryEf entity, ExpenseCategoryToUpdate model)
        {
            var instance = model ?? new ExpenseCategoryToUpdate();

            instance.Id = entity.Id;
            instance.Name = entity.Name;
            instance.Description = entity.Description;

            return instance;
        }
    }
}
