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

        public static void InitUserDates(this UserAccount userAccount)
        {
            DateTime dayInCurrentMounth;
            DateTime dayInNextMounth;

            //если дата зп еще не выдавали в этом месяце, начинаем считать с сегодняшней даты
            //т.е обрабатываем сразу два случая:
            //-дата еще не наступила
            //-в месяце дней меньше, чем значение SalaryDay

            if (DateTime.Now.Day <= userAccount.SalaryDay)
            {
                dayInCurrentMounth = DateTime.Now;

                int dayToNextMounth = userAccount.SalaryDay - DateTime.Now.Day;
                dayInNextMounth = dayInCurrentMounth.AddMonths(1);
                try
                {
                    dayInNextMounth = dayInNextMounth.AddDays(dayToNextMounth);
                }
                catch (ArgumentException)
                {
                    dayInNextMounth = new DateTime(
                        dayInNextMounth.Year,
                        dayInNextMounth.Month,
                        DateTime.DaysInMonth(dayInNextMounth.Year,
                        dayInNextMounth.Month));
                }
            }
            else
            {
                dayInCurrentMounth = new DateTime(
                    DateTime.Today.Year,
                    DateTime.Today.Month,
                    userAccount.SalaryDay);

                dayInNextMounth = dayInCurrentMounth.AddMonths(1);

            }

            userAccount.CountdownDate = dayInCurrentMounth;
            userAccount.ResetDate = dayInNextMounth;
        }
    }
}
