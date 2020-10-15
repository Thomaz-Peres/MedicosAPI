using System.Threading.Tasks;
using DesafioMedicos.Data;
using DesafioMedicos.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesafioMedicos.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<dynamic>> Get()
        {
            var manager = new User { Id = 1, Username = "Thomaz", Password = "xesquedale", Role = "manager"};
            _context.Users.Add(manager);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Dados configurados"});
        }
    }
}