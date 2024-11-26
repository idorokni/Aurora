using Aurora.Server.Database.Data;
using Aurora.Server.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Server.Communication
{
    internal class DatabaseManager
    {
        private static DatabaseManager _instance;

        public static DatabaseManager Instance
        {
            get
            {
                _instance ??= new DatabaseManager();
                return _instance;
            }
        }

        public string FindEmail(string username)
        {
            using (var db = new AuroraDB())
            {
                return db.Users.Find(username).Email;
            }
        }

        public bool UserExists(string username)
        {
            using(var db = new AuroraDB())
            {
                return db.Users.Any(user => user.Username == username);
            }
        }

        public void AddUser(string username, string password, string email)
        {
            using(var db = new AuroraDB())
            {
                db.Users.Add(new User
                {
                    Username = username,
                    Password = password,
                    Email = email
                });

                db.SaveChanges();
            }
        }

        public void RemoveUser(string username)
        {
            using(var db = new AuroraDB())
            {
                db.Users.Remove(db.Users.Find(username));
            }
        }

        public bool checkIfPasswordsMatch(string username, string password)
        {
            using (var db = new AuroraDB())
            {
                return db.Users.Find(username).Password == password;
            }
        }
    }
}
