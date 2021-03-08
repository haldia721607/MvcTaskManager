using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcTaskManager.Identity;

namespace MvcTaskManager.Controllers
{
    public class TaskStatusController : Controller
    {
        private ApplicationDbContext db;

        public TaskStatusController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [Route("api/taskstatuses")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public List<MvcTaskManager.Models.TaskStatus> Get()
        {
            List<MvcTaskManager.Models.TaskStatus> taskStatus = db.TaskStatuses.ToList();
            return taskStatus;
        }

        [HttpGet]
        [Route("api/taskstatuses/searchbytaskstatusid/{TaskStatusID}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetByTaskStatusID(int TaskStatusID)
        {
            MvcTaskManager.Models.TaskStatus taskStatuses = db.TaskStatuses.Where(temp => temp.TaskStatusID == TaskStatusID).FirstOrDefault();
            if (taskStatuses != null)
            {
                return Ok(taskStatuses);
            }
            else
                return NoContent();
        }

        [HttpPost]
        [Route("api/taskstatuses")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public MvcTaskManager.Models.TaskStatus Post([FromBody] MvcTaskManager.Models.TaskStatus taskStatus)
        {
            db.TaskStatuses.Add(taskStatus);
            db.SaveChanges();

            MvcTaskManager.Models.TaskStatus existingTaskPriority = db.TaskStatuses.Where(temp => temp.TaskStatusID == taskStatus.TaskStatusID).FirstOrDefault();
            return taskStatus;
        }

        [HttpPut]
        [Route("api/taskstatuses")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public MvcTaskManager.Models.TaskStatus Put([FromBody] MvcTaskManager.Models.TaskStatus taskStatus)
        {
            MvcTaskManager.Models.TaskStatus existingTaskPriority = db.TaskStatuses.Where(temp => temp.TaskStatusID == taskStatus.TaskStatusID).FirstOrDefault();
            if (existingTaskPriority != null)
            {
                existingTaskPriority.TaskStatusName = taskStatus.TaskStatusName;
                db.SaveChanges();
                return existingTaskPriority;
            }
            else
            {
                return null;
            }
        }

        [HttpDelete]
        [Route("api/taskstatuses")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public int Delete(int TaskStatusID)
        {
            MvcTaskManager.Models.TaskStatus existingTaskPriority = db.TaskStatuses.Where(temp => temp.TaskStatusID == TaskStatusID).FirstOrDefault();
            if (existingTaskPriority != null)
            {
                db.TaskStatuses.Remove(existingTaskPriority);
                db.SaveChanges();
                return TaskStatusID;
            }
            else
            {
                return -1;
            }
        }
    }
}
