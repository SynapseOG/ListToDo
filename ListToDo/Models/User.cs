using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ListToDo.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
       
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        
        public IEnumerable<ToDo> ToDoes { get; set; }



        
    }
}
