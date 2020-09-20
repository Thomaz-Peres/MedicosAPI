using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioMedicos.Data;
using DesafioMedicos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesafioMedicos.Validations;

namespace DesafioMedicos.Controllers
{
    [Route("medico")]
    public class DoctorController : ControllerBase
    {
        private readonly DataContext _context;

        public DoctorController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Doctors>>> Get()
        {
            var doutores = await _context
                .Doctors
                .AsNoTracking()
                .ToListAsync();
            
            return Ok(doutores);
        }

        [HttpGet]
        [Route("{especialidade}")]
        public async Task<ActionResult<List<Doctors>>> GetByEspecialidade(string especialidade)
        {
            var especialidades = await _context
                .Doctors
                .AsNoTracking()
                .Where(x => x.Especialidades == especialidade)
                .ToListAsync();

            return Ok(especialidades);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Doctors>> Post(
            [FromBody]Doctors model)
        {
            if(ModelState.IsValid)
            {
                _context.Doctors.Add(model);
                await _context.SaveChangesAsync();
                return Ok(model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}