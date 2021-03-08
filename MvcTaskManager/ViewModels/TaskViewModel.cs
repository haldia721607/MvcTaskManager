using MvcTaskManager.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTaskManager.ViewModels
{
    public class TaskViewModel
    {
        public int TaskID { get; set; } //Primary Key for Tasks table
        public string TaskName { get; set; } //Name / title of the task
        public string Description { get; set; } //Description of the task
        public int ProjectID { get; set; } //FK, refers to "Projects" table
        public string AssignedTo { get; set; } //FK, refers to "AspNetUsers" table, indicating to whom the task is assigned
        public int TaskPriorityID { get; set; } //FK, refers to "TaskPriorities" table
        public string CurrentStatus { get; set; } //Current status name of the task
    }
}
