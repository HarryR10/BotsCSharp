using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FinanceBot.Models.EntityModels;
using System.Threading.Tasks;
using FinanceBot.Models.CommandsException;

namespace FinanceBot.Models.Repository
{
    public class CategoryInMemoryRepository : ICategoryRepository
    {
        private IUserAccountRepository _userAccountRepository;
        private List<Category> _categories;

        public CategoryInMemoryRepository(
            [FromServices] IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
            _categories = new List<Category>()
            {
                new Category
                {
                    CategoryName = "other",
                    CategoryId = 1,
                    Description = "общая категория",
                    IsBasic = false,
                    Author = _userAccountRepository.Accounts.FirstOrDefault(),
                    IsMounthly = false
                }
            };
        }

        public IQueryable<Category> Categories => _categories.AsQueryable<Category>();

        public void AddCategory(Category category, UserAccount userAccount)
        {
            lock (userAccount.GetLock)
            {
                if(!Categories.Where(c => c.CategoryId == category.CategoryId)
                    .Any())
                {
                    _categories.Add(category);
                }
                else
                {
                    throw new CategoryAlredyExistException(category.CategoryName);
                }
            }
        }

        public Category DeleteCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
