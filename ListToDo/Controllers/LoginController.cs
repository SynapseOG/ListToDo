using AutoMapper;
using ListToDo.Data;
using ListToDo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListToDo.Controllers
{
    
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _db;
        
        public LoginController(ApplicationDbContext db)
        {
            _db = db;
        }

        //pokazanie uzytkownikowi widoku logowania
        public IActionResult UserLogin()
        {
            return View();
        }

        //Akcja odpowiedzialna za logowanie użytkownika
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UserLogin(User obj)
        {
            //zmienne potrzebne do ustawiania wartosci temp data
            var name = "";
            var isLogged = false;
            var id = 0;

            //Jezeli walidacja modelu jest poprawna wtedy zaczynamy logowanie
            if (ModelState.IsValid)
            {
                //sprawdzamy czy wprowadzone dane istnieja w bazie danych
                //tutaj trzeba dodac funkcje dekodowania w przyszlosci
                if (_db.Users.Any(u => u.UserName == obj.UserName && u.Password == obj.Password))
                {
                    name = obj.UserName;

                    var data = from p in _db.Users
                               where p.UserName == name
                               select p;
                    //wybieramy z bazy Id zalogowanego uzytkownika i jego wartosc zapisujemy w zmiennej id
                    foreach (var item in data)
                    {
                        id = item.Id;
                    }
                    isLogged = true;

                }
            }
            //tymczasowe dane ktore sluża w kontrolerze zadan
            TempData["UserId"] = id;
            TempData["name"] = name;
            if (isLogged)
            {
                return RedirectToAction("TaskIndex", "Task", new {area = "" });
            }
                return View(obj);
        }

        //wylogowanie i powrot do strony głownej
        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home", new { area= "" });   
        }
    }
}
