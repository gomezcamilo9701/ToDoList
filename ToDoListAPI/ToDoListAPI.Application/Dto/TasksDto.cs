using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListAPI.Application.Dto
{
    public class TasksDto
    {
        public long Id { get; set; }

        public int State { get; set; }

        public string Description { get; set; }
    }
}
