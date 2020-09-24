using System.Collections.Generic;
using System.Threading.Tasks;
using DesafioMedicos.Data;
using DesafioMedicos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesafioMedicos.Controllers
{
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        [Route("")]
        [Authorize(Roles = "employee")]
        public async Task<ActionResult<List<User>>> Get()
        {
            var users = await _context
                .Users
                .AsNoTracking()
                .ToListAsync();

            return users;
        }

        
    }
}