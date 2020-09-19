using System;
using System.Linq;
using FinanceBot.Models.EntityModels;

namespace FinanceBot.Models.Repository
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }

        void AddCategory(Category category);

        Category DeleteCategory(int categoryId);
    }
}
