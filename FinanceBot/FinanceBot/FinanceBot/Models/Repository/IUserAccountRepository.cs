using System;
using System.Linq;
using FinanceBot.Models.EntityModels;

namespace FinanceBot.Models.Repository
{
    public interface IUserAccountRepository
    {
        IQueryable<UserAccount> Accounts { get; }

        void AddAccount(UserAccount account);

        UserAccount DeleteAccount(int userId);
    }
}
