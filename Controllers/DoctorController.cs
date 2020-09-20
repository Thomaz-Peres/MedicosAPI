using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioMedicos.Data;
using DesafioMedicos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesafioMedicos.Validations;
using System;

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

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Doctors>> Put(
            [FromBody] Doctors model,
            int id)
        {
            if (id != model.MedicoID)
                return NotFound(new { message = "Doutor não encontrado"});

            if(ModelState.IsValid)
            {
                _context.Entry<Doctors>(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(model);
            }
            else
            {
                return BadRequest(new { message = "Não foi possivel atualizar o medico"});
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Doctors>> Delete(int id)
        {
            var doutor = await _context.Doctors.FirstOrDefaultAsync(x => x.MedicoID == id);
            if(doutor == null)
                return NotFound(new { message = "Doutor não foi encontrado"});

            try
            {
                _context.Doctors.Remove(doutor);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Doutor foi removido com sucesso"});
            }
            catch(Exception)
            {
                return BadRequest(new { message = "Não foi possivel remover o o doutor"});
            }
        }
    }
}