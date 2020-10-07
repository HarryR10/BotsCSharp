using System;
using FinanceBot.Models.Repository;

namespace FinanceBot.Models.EntityModels
{
    public class UserAccount
    {
        public int UserId { get; set; }
        public int SalaryDay { get; set; }
        public DateTime CountdownDate { get; set; }
        public DateTime ResetDate { get; set; }
        //public DateTime DateAdded { get; }

        private object _lock = new object();
        public object GetLock => _lock;

        public UserAccount(int userId, int salaryDay)
        {
            UserId = userId;
            SalaryDay = salaryDay;
            //DateAdded = DateTime.Now;

            this.InitUserDates(salaryDay);
        }
    }
}
