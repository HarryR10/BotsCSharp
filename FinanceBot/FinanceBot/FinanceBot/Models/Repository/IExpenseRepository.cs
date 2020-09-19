using System;
using System.Linq;
using FinanceBot.Models.EntityModels;

namespace FinanceBot.Models.Repository
{
    public interface IExpenseRepository
    {
        IQueryable<Expense> Expenses { get; }

        void AddExpense(Expense expense);

        Expense DeleteExpense(int expenseId);
    }
}
