using LocalPassBank.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalPassBank.Models
{
    public class Database
    {
        private DatabaseEntities database;

        public Database()
        {
            database = new DatabaseEntities();
        }

        public Accounts GetAccountById(int id)
        {
            foreach (Accounts account in database.Accounts.ToArray())
            {
                if (account.Id == id)
                    return (account);
            }
            return (null);
        }

        public Accounts GetAccountByName(String name)
        {
            foreach (Accounts account in database.Accounts.ToArray())
            {
                if (account.Name == name)
                    return (account);
            }
            return (null);
        }

        public int GetNextId()
        {
            int id;
            try
            {
                id = database.Accounts.Max((acc) => acc.Id);
                return (id + 1);
            }
            catch
            {
                return (0);
            }
        }

        public void AddAccount(Accounts account)
        {
            account.Id = GetNextId();
            database.Accounts.Add(account);
            database.SaveChanges();
        }
    }
}
