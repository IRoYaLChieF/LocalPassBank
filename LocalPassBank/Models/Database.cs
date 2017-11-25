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

        public int GetNextAccountId()
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

        public int GetNextPasswordId()
        {
            int id;
            try
            {
                id = database.Passwords.Max((acc) => acc.Id);
                return (id + 1);
            }
            catch
            {
                return (0);
            }
        }

        public void AddAccount(Accounts account)
        {
            account.Id = GetNextAccountId();
            database.Accounts.Add(account);
            database.SaveChanges();
        }

        public PasswordInfo[] GetPasswordInfoFromId(int id, Byte[] key)
        {
            var array = database.Passwords.Where(x => x.AccountId == id)
                .Select(x => new
                {
                    KeyWord = x.KeyWord,
                    Description = x.Description
                }).AsEnumerable()
                .Select(x => new PasswordInfo(x.KeyWord, x.Description, key));
            return (array.ToArray());
        }

        public void AddPassword(int id, Byte[] keyWord, Byte[] description, Byte[] login, Byte[] password)
        {
            Passwords pass = new Passwords()
            {
                Id = GetNextPasswordId(),
                AccountId = id,
                KeyWord = keyWord,
                Description = description,
                Login = login,
                Password = password
            };
            database.Passwords.Add(pass);
            database.SaveChanges();
        }
    }
}
