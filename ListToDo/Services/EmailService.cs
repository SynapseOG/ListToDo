using ListToDo.Data;
using ListToDo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ListToDo.Services
{
    public class EmailService
    {
        private SmtpClient smtp;
        private MailMessage _mail;

            MailMessage message = new MailMessage();
        private readonly ApplicationDbContext _db;
        public EmailService(ApplicationDbContext db)
        {
            _db = db;
        }
        //zapisujemy w liscie maile uzytkownikow gdzie ich daty sa mniejsze od datetime.now
        public List<string> GetDate()
        {
            var user = _db.ToDoes;
            List<int> idList = new List<int>();

            var emails = _db.Users;
            List<string> emailsList = new List<string>();

            //przeszukanie bazy danych 
            foreach (var item in user)
            {
                // porownujemy dwie daty, dzisiejsza i ta z bazy jezeli wartosc bedzie mniejsza niz 0 to zapisujemy do listy user id dla tych dat
                if(item.DateAddition.CompareTo(DateTime.Now) < 0)
                {
                    //zapisujemy tylko pojedyncze wartosci id 
                    if(idList.Count ==  0 || idList.Contains(item.UserId)==false )
                    idList.Add(item.UserId);
                }             
            }
            //przeszukujemy liste id 
            foreach (var item in idList)
            {
               // przeszukujemy baze userow w poszukiwaniu adresow email gdzie id z listy z numerami id jest takie same jak w bazie danych
                foreach (var email in emails)
                {
                    //porwnujemy dwa id ze soba jezeli wartosc jest prawda to zapisujemy do listy maili te adresy 
                    if(email.Id == item)
                    {
                        emailsList.Add(email.Email);
                    }

                }
                
            }
            //zwracamy liste adresow email
            return emailsList;
        }
        
        //wysylanie wiadomosci na email
        public void Send()
        {
            //przypisujemy do listy emails wynik z metody GetDate ktora zwraca adresy email
            List<string> emails = GetDate();

            //jezeli lista adresow nie jest pusta do przechodzimy do ustawien i wyslania wiadomosci na poczte
            if(emails.Count > 0)
            {
                //przypisujemy adres e mail nadawcy
                    string fromMail = "synapsetest1244@gmail.com";
                //przypisujemy haslo dla maila ktore zostalo wygenerowane przez google dla aplikacji
                    string fromPassword = "ykdixsmpbcwtrrgo";

                //ustawienie adresu nadawcy
                    message.From = new MailAddress(fromMail);
                //temat wiadomosci
                    message.Subject = "Lista zadań";

                
                foreach (var item in emails)
                {
                    //dodajemy adresy email do kolekcji To
                    message.To.Add(new MailAddress(item));
                }
                //wiadomosc ktorą chcemy przeslac
                    message.Body = "<htlm><body> Twoje zadania czekają na realizacje! </body></html>";
                    message.IsBodyHtml = true;

                //ustawienia klienta smtp dla google
                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential(fromMail, fromPassword),
                        EnableSsl = true,
                    };
                    smtpClient.Send(message);          

            }

        }
        

    }
}
