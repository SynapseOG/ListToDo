using AutoMapper;
using ListToDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListToDo
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<ToDo, ToDoDto>();

            CreateMap<CreateTaskDto,ToDo>();
        }
    }
}
