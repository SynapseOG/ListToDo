using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ListToDo.Models
{
    public class ToDo
    {    
        [Key]
        public int Id { get; set; }
        [DisplayName("Zadanie")]
        [Required]
        public string Task { get; set; }
        [DisplayName("Data Dodania")]
        [Required]
        public DateTime DateAddition { get; set; }
        public int UserId { get; set; }        
        public virtual User User { get; set; }

    }
}
