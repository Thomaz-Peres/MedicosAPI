using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioMedicos.Data;
using DesafioMedicos.Models;
using DesafioMedicos.Services;
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

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<User>> Put(
            int id,
            [FromBody] User model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(id != model.Id)
                return BadRequest(new { message = "Não foi possivel encontrar o usuario"});

            try
            {
                _context.Entry(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(model);
            }
            catch
            {
                return BadRequest(new { message = "Não foi possivel alterar o usuario"});
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate(
            [FromBody] User model)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Where(x => x.Username == model.Username && x.Password == model.Password)
                .FirstOrDefaultAsync();

            if(user == null)
                return NotFound(new { message = "Usuario ou senha invalidás"});

            var token = TokenService.GenerateToken(user);
            user.Password = "";
            return new 
            {
                user = user,
                token = token
            };
        } 
    }
}