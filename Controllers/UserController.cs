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
        [AllowAnonymous]
        // [Authorize(Roles = "employee")]
        public async Task<ActionResult<List<User>>> Get()
        {
            var users = await _context
                .Users
                .AsNoTracking()
                .ToListAsync();

            return Ok(users);
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Post(
            [FromBody] User model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // forçando o usuario a ser cadastrado como funcionario ja
                model.Role = "employee";

                _context.Users.Add(model);
                await _context.SaveChangesAsync();

                //escondendo a senha
                model.Password = "";

                return Ok(model);
            }
            catch(System.Exception)
            {
                return BadRequest(new { message = "Não foi possivel cadastrar o usuario"});
            }
        }
    }
}