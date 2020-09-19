using System;
namespace FinanceBot.Models.EntityModels
{
    public class Expense
    {
        public UserAccount UserAccount { get; set; }
        public int ExpenseId { get; set; }
        public DateTime ExpenseDateTime { get; set; }
        public Category Category { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        //public Expense(UserAccount userAccount)
        //{

        //}
    }
}
