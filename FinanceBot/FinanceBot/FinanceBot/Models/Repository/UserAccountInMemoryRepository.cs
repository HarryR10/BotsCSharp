using System;
using System.Collections.Generic;
using System.Linq;
using FinanceBot.Models.EntityModels;

namespace FinanceBot.Models.Repository
{
    public class UserAccountInMemoryRepository : IUserAccountRepository
    {
        public IQueryable<UserAccount> Accounts => new List<UserAccount>()
        {
            new UserAccount(1, 1)
        }.AsQueryable<UserAccount>();

        public void AddAccount(UserAccount account)
        {
            throw new NotImplementedException();
        }

        public UserAccount DeleteAccount(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
