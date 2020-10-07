using System;
using System.Linq;
using FinanceBot.Models.EntityModels;

namespace FinanceBot.Models.Repository
{
    public static class RepositoryExtension
    {
        public static bool GetUser(this IUserAccountRepository userAccountRepository,
            int Id, out UserAccount userAccount)
        {
            var userInBase = userAccountRepository.Accounts.Where(u => Id == u.UserId);
            if (userInBase.Count() == 0)
            {
                userAccount = null;
                return false;
            }

            userAccount = userInBase.FirstOrDefault();
            return true;
        }

        public static void InitUserDates(this UserAccount userAccount,
            int salaryDay = default)
        {
            if (salaryDay != default)
            {
                lock (userAccount.GetLock)
                {
                    userAccount.SalaryDay = salaryDay;
                } 
            }

            DateTime CurrentDay;
            DateTime NextDay;

            //если зп еще не выдавали в этом месяце, начинаем считать с сегодняшней даты
            //т.е обрабатываем сразу два случая:
            //-дата еще не наступила
            //-в месяце дней меньше, чем значение SalaryDay

            if (userAccount.SalaryDay > DateTime.Now.Day)
            {
                CurrentDay = DateTime.Now;

                int daysInCurrentMounth = DateTime.DaysInMonth(CurrentDay.Year,
                        CurrentDay.Month);

                if(userAccount.SalaryDay > daysInCurrentMounth)
                {
                    NextDay = new DateTime(
                        CurrentDay.Year,
                        CurrentDay.Month,
                        daysInCurrentMounth);
                }
                else
                {
                    NextDay = new DateTime(
                        CurrentDay.Year,
                        CurrentDay.Month,
                        userAccount.SalaryDay);
                }
            }
            else
            {
                CurrentDay = new DateTime(
                    DateTime.Today.Year,
                    DateTime.Today.Month,
                    userAccount.SalaryDay);

                NextDay = CurrentDay.AddMonths(1);

            }

            lock (userAccount.GetLock)
            {
                userAccount.CountdownDate = CurrentDay;
                userAccount.ResetDate = NextDay;
            }
        }
    }
}
