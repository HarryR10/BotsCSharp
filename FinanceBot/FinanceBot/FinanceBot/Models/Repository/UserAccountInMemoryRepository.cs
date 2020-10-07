using System;
using System.Collections.Generic;
using System.Linq;
using FinanceBot.Models.CommandsException;
using FinanceBot.Models.EntityModels;

namespace FinanceBot.Models.Repository
{
    public class UserAccountInMemoryRepository : IUserAccountRepository
    {
        private object _lock = new object();

        private List<UserAccount> _accounts = new List<UserAccount>
        {
            new UserAccount(1, 1)
        };

        public IQueryable<UserAccount> Accounts =>
            _accounts.AsQueryable<UserAccount>();
            


        public void AddAccount(UserAccount account)
        {
            //TODO: to slow...
            lock (_lock)
            {
                if (!Accounts.Where(a => a.UserId == account.UserId).Any())
                {
                    _accounts.Add(account);
                }
                else
                {
                    throw new UserAlredyExistException(account.UserId);
                }
            }
        }

        public UserAccount DeleteAccount(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
