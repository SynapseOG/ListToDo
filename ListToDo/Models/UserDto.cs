using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListToDo.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        
        public string UserName { get; set; }

        public IEnumerable<ToDoDto> ToDoes { get; set; }
    }
}
