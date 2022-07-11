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
  
    public class TaskController : Controller
    {

        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _db;
        public TaskController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
     
        // ładowanie widoku wraz z zadaniami uzytkownika o ile istnieja :)
        public ActionResult TaskIndex()
        {
            //tymczasowa wartosc przypisujemy do zmiennej name
            var name = TempData["name"].ToString();
            //zatrzymujemy dane z TempData aby wczytywac dane zalogowanego uzytkownika po utworzeniu lub edycji czy tez usunieciu zadania
            TempData.Keep("name");
            
            //do enumatora przypisujemy zadania dla uzytkownika ktory jest aktualnie zalogowany
            IEnumerable<ToDo> objList = _db.ToDoes
                .Where(u => u.User.UserName == name);

            //przypisujemy id usera z powiazanej bazy danych do uzycia go w akcji create
            int a = 0;
            foreach (var item in objList)
            {
                a = item.UserId;
            }
            TempData["id"] = a;
            //zwracamy widok zadan
            return View(objList);         
        }
        //widok tworzenia zadania
        public IActionResult Create()
        {
            return View();
        }
        //akcja do tworzenia zadania podanego w formualarzu przez uzytkownika
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateTaskDto obj)
        {
            //Jezeli walidacja modelu jest poprawna wtedy zaczynamy dodawanie zadania wraz z data
            if (ModelState.IsValid)
            {
                //przypisanie id usera do zmiennej
                var tempId = Convert.ToInt32(TempData["UserId"]);

                //wybieranie z bazy danych uzytkownika o podanym wyzej id 
                var user = _db.Users.FirstOrDefault(u => u.Id == tempId);

                //jezeli uzytkownik nie istnieje w bazie zwracamy notfound
                if (user is null)
                    return NotFound();

                //do zmiennej przypisujemy zmapowany obiekt CreatTaskDto do bazy danych z zadaniami - ToDo
                var taskEntity = _mapper.Map<ToDo>(obj);

                //dopisujemy do userid id zalogowanego uzytkownika
                taskEntity.UserId = tempId;

                //dodajemy zadanie i date do bazy nastepnie zapisujemy zmiany
                _db.ToDoes.Add(taskEntity);
                _db.SaveChanges();

                //powracamy do widoku z lista zadan
                return RedirectToAction("TaskIndex");
            }
            return View(obj);
        }

        //akcja zwracania widoku usuwania
        public IActionResult Delete(int? id)
        {
            //jezeli wyslane od klienta id jest rowne null lub 0 to zwracamy notfound
             if(id == null || id==0)
             {
                return NotFound();
             }

             //do zmiennej zapisujemy wynik find 
            var obj = _db.ToDoes.Find(id);

            //jezeli obj jest null czyli find zwrocilo null to returnujemy notfound
            if(obj == null)
            {
                return NotFound();
            }
            //przechodzimy do widoku
            return View(obj);

        }
        //akcja do usuwania z bazy danych
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {

            //do zmiennej zapisujemy wynik find 
            var obj = _db.ToDoes.Find(id);
            //jezeli obj jest null czyli find zwrocilo null to returnujemy notfound
            if (obj == null)
            {
                return NotFound();
            }
            //jezeli wszystko jest ok to usuwamy z bazy danych znaleziony obiekt
            _db.ToDoes.Remove(obj);
            _db.SaveChanges();
            //wracamy do widoku z zadaniami
            return RedirectToAction("TaskIndex");

        }
        //widok edycji - dziala tak samo 
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.ToDoes.Find(id);

            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        //akcja edycji dziala tak samo jak delete ale zamiast usuwac to pozwala na wprowadzenie zmian w konkretnym wybranym zadaniu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ToDo obj)
        {
            if (ModelState.IsValid)
            {
                var id = Convert.ToInt32(TempData["id"]);
                obj.UserId = id;

                _db.ToDoes.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("TaskIndex");
            }
            return View(obj);
        }


    }
}
