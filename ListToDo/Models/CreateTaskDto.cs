using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ListToDo.Models
{
    public class CreateTaskDto
    {
        [Required]
        public string Task { get; set; }
        [Required]
        public DateTime DateAddition { get; set; }
        public int UserId { get; set; }
    }
}
