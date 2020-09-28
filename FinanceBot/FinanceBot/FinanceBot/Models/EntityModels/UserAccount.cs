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

        public UserAccount(int userId, int salaryDay)
        {
            UserId = userId;
            SalaryDay = salaryDay;

            this.InitUserDates();
        }
    }
}
