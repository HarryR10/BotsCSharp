using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FinanceBot.Models.EntityModels;

namespace FinanceBot.Models.Repository
{
    public class ExpenseInMemoryRepository : IExpenseRepository
    {
        private IUserAccountRepository _userAccountRepository;
        private ICategoryRepository _categoryRepository;
        private List<Expense> _expenses;

        public ExpenseInMemoryRepository(
            [FromServices] IUserAccountRepository userAccountRepository,
            [FromServices] ICategoryRepository categoryRepository)
        {
            _userAccountRepository = userAccountRepository;
            _categoryRepository = categoryRepository;

            _expenses = new List<Expense>()
            {
                new Expense
                {
                    UserAccount = _userAccountRepository.Accounts.FirstOrDefault(),
                    ExpenseId = 1,
                    ExpenseDateTime = DateTime.Now,
                    Category = _categoryRepository.Categories.FirstOrDefault(),
                    Amount = 12m,
                    Description = "just one Expense"
                }
            };
        }

        public IQueryable<Expense> Expenses => _expenses.AsQueryable<Expense>();

        public void AddExpense(Expense expense)
        {
            _expenses.Add(expense);
        }

        public Expense DeleteExpense(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
