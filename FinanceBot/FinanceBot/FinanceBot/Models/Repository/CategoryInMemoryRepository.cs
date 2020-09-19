using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FinanceBot.Models.EntityModels;

namespace FinanceBot.Models.Repository
{
    public class CategoryInMemoryRepository : ICategoryRepository
    {
        private IUserAccountRepository _userAccountRepository;
        private IQueryable<Category> _categories;

        public CategoryInMemoryRepository(
            [FromServices] IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
            _categories = new List<Category>()
            {
                new Category
                {
                    CategoryName = "taxi",
                    CategoryId = 1,
                    Description = "просто категория",
                    IsBasic = false,
                    Author = _userAccountRepository.Accounts.FirstOrDefault(),
                    IsMounthly = false
                }
            }.AsQueryable<Category>();
        }

        public IQueryable<Category> Categories => _categories;

        public void AddCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public Category DeleteCategory(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
