using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListToDo.Models
{
    public class ToDoDto
    {
        public int Id { get; set; }

        public string Task { get; set; }

        public DateTime DateAddition { get; set; }


    }
}
