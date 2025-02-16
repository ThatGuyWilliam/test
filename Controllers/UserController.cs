using Microsoft.AspNetCore.Mvc;
using SkillTest.Models;

namespace SkillTest.Controllers
{
    [Route("User")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /user
        [HttpGet]
        public ActionResult<User> Index()
        {
            var allUsers = _context.Users.Where(x => x.Active == true).ToList();
            if(allUsers.Count != 0)
            {
                return Ok(allUsers);
            }
            return BadRequest("Error getting all users.");
        }

        // GET: /user/details/1
        [HttpGet("details/{id}")]
        public ActionResult<User> Details(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.ID == id);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound("User does not exist.");
            }
        }

        [HttpPost("create")]
        public ActionResult Create(string username, string Email, string password, bool Active)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = new User();
                    user.UserName = username;
                    user.Email = Email;
                    user.Password = password;
                    user.Active = Active;
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    return Ok();
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest("Erro creating new user.");
            }
        }

        // POST:
        [HttpPost("edit")]
        public ActionResult Edit(int id, string username, string Email, string password, bool Active)
        {
            try
            {
                var user = _context.Users.Where(x => x.ID == id).FirstOrDefault();
                if (user != null)
                {
                    user.UserName = username;
                    user.Email = Email;
                    user.Password = password;
                    user.Active = Active;
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound("User cannot be found.");
            }
            catch
            {
                return BadRequest("Error editing user");
            }
        }

        // DELETE:
        [HttpDelete("{id}")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                var user = _context.Users.Where(x => x.ID == id).FirstOrDefault();
                if (user != null)
                {
                    user.Active = false;
                    _context.SaveChanges();
                    return Ok();
                }
                return BadRequest("Error deleting user");
            }
            catch
            {
                return BadRequest("Error deleting user");
            }
        }
    }
}
