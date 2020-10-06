using System;
using System.Linq;
using System.Threading.Tasks;
using FinanceBot.Models.EntityModels;

namespace FinanceBot.Models.Repository
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }

        void AddCategory(Category category);
        //Task<bool> AddCategory(Category category);

        Category DeleteCategory(int categoryId);
    }
}
