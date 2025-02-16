using Microsoft.AspNetCore.Mvc;
using SkillTest.Models;

namespace SkillTest.Controllers
{
    [Route("Task")]
    public class TaskController : Controller
    {

        private readonly ApplicationDbContext _context;

        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var tasks = _context.Task.ToList();
            if (tasks.Count != 0)
            {
                return Ok(tasks);
            }
            return BadRequest("Error getting all tasks.");
        }

        // GET: /task/details/1
        [HttpGet("details/{id}")]
        public ActionResult<User> Details(int id)
        {
            var task = _context.Task.FirstOrDefault(x => x.ID == id);
            if (task != null)
            {
                
                return Ok(task);
            }
            else
            {
                return NotFound("Task does not exist.");
            }
        }

        ///task/create?Title=hello&Description=123&userID=1&duedate=2024/01/01&Active=true
        [HttpPost("create")]
        public ActionResult Create(string Title, string Description, int userID, DateTime dueDate, bool Active)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Models.Task task = new Models.Task();
                    task.Title = Title;
                    task.Description = Description;
                    task.DueDate = dueDate;
                    task.Active = Active;
                    var user = _context.Users.Where(x => x.ID == userID).FirstOrDefault();
                    if(user == null)
                    {
                        return BadRequest();
                    }
                    task.Assignee = user;
                    _context.Task.Add(task);
                    _context.SaveChanges();
                    return Ok();
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest("Erro creating new task.");
            }
        }

        [HttpGet("active")]
        public ActionResult GetActiveTasks()
        {
            try
            {
                var tasks = _context.Task.Where(x => x.Active == true).ToList();
                return Ok(tasks);
            }
            catch
            {
                return BadRequest("Error retrieving active tasks.");
            }
        }

        [HttpGet("expired")]
        public ActionResult GetExpiredTasks()
        {
            try
            {
                var tasks = _context.Task.Where(x => x.DueDate < DateTime.Now).ToList();
                return Ok(tasks);
            }
            catch
            {
                return BadRequest("Error retrieving expired tasks.");
            }
        }

        [HttpPost("edit")]
        public ActionResult Edit(int id, string Title, string Description, int userID, DateTime dueDate, bool Active)
        {
            try
            {
                var task = _context.Task.Where(x => x.ID == id).FirstOrDefault();
                if (task != null)
                {
                    task.Title = Title;
                    task.Description = Description;
                    var user = _context.Users.Where(x => x.ID == userID).FirstOrDefault();
                    if (user == null)
                    {
                        return BadRequest();
                    }
                    task.Assignee = user;
                    task.DueDate = dueDate;
                    task.Active = Active;
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound("Error editing a task.");
            }
            catch
            {
                return BadRequest("Error editing a task.");
            }
        }

        // DELETE: /task/5
        [HttpDelete("{id}")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                var task = _context.Task.Where(x => x.ID == id).FirstOrDefault();
                if (task != null)
                {
                    task.Active = false;
                    _context.SaveChanges();
                    return Ok();
                }
                return BadRequest("Error deleting a task.");
            }
            catch
            {
                return BadRequest("Error deleting a task.");
            }
        }
    }
}
