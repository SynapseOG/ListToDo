using ListToDo.Data;
using ListToDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListToDo
{
    //wypelnianie bazy danych jezeli jest pusta testowymi uzytkownikami
    public class UserSeeder
    {
        private readonly ApplicationDbContext _db;
        public UserSeeder(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Seed()
        {
            if (_db.Database.CanConnect())
            {
                if (!_db.Users.Any())
                {
                    var users = GetUsers();
                    _db.Users.AddRange(users);
                    _db.SaveChanges();
                }
            }
        }



        private IEnumerable<User> GetUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                    UserName = "test",
                    Email = "test@test",
                    Password = "password",
                    ToDoes = new List<ToDo>()
                    {
                        new ToDo()
                        {
                            Task = "Task Test",
                            DateAddition = new DateTime(2022,06,30)
                        },
                        new ToDo()
                        {
                            Task = "Task test 2",
                            DateAddition = new DateTime(2022,06,29)
                        }
                    }

                },
                new User()
                {
                    UserName = "test2",
                    Email = "test2@test",
                    Password = "password",
                    ToDoes = new List<ToDo>()
                    {
                        new ToDo()
                        {
                            Task = "Task Test3",
                            DateAddition = new DateTime(2022,06,30)
                        },
                        new ToDo()
                        {
                            Task = "Task test4",
                            DateAddition = new DateTime(2022,06,29)
                        }
                    }
                }

            };
            return users;

            
        }
    }
}
