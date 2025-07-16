using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListAPI.Application.Dto
{
    public class AddTaskDto
    {
        public int State { get; set; }
        public string Description { get; set; } = null!;
    }
}
