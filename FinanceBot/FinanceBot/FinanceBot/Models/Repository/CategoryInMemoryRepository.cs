using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FinanceBot.Models.EntityModels;
using System.Threading.Tasks;

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
                    CategoryName = "taxi",
                    CategoryId = 1,
                    Description = "просто категория",
                    IsBasic = false,
                    Author = _userAccountRepository.Accounts.FirstOrDefault(),
                    IsMounthly = false
                }
            };
        }

        public IQueryable<Category> Categories => _categories.AsQueryable<Category>();

        public void AddCategory(Category category)
        {
            //throw new NotImplementedException();
            //return await new Task<bool>(()=> true);
        }

        //public async Task<bool> AddCategory(
        //    string categoryName,
        //    int categoryId,
        //    string description,
        //    bool isBasic,
        //    UserAccount author,
        //    bool isMounthly)
        //{
        //    throw new NotImplementedException();
        //}

        public Category DeleteCategory(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
