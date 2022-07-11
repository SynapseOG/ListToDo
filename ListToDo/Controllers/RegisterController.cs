using ListToDo.Data;
using ListToDo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListToDo.Controllers
{
    public class RegisterController : Controller
    {
        
        private readonly ApplicationDbContext _db;
        public RegisterController(ApplicationDbContext db)
        {
            _db = db;
        }

        //widok rejestracji 
        public IActionResult RegisterIndex()
        {
            return View();
        }

       


        //akcja odpowiedzialna za stworzenie uzytkownika
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User obj)
        {
            // obiekt ktory otrzymujemy od klienta sprawdzamy pod wzgledem wystepowania takich samych danych w bazie
            //aby nie stworzyc tych samych uzytkownikow
            //trzeba dodac walidacje bo dostajemy BadRequest w przypadku tych samych danych
                if(_db.Users.Any(u=>u.Email == obj.Email || u.UserName == obj.UserName))
                {
                    return BadRequest();
                }          
                //zapis do nazwy uzytkownika, emaila i hasla
                    _db.Users.Add(obj);
                    _db.SaveChanges();

              
           

            return RedirectToAction("RegisterIndex");
        }
       


    }
}
