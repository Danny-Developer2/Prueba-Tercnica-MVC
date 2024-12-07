namespace Universidad.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Universidad.Data;
    using Universidad.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class AlumnosController : ControllerBase
    {
        private readonly UniversidadContext _context;

        public AlumnosController(UniversidadContext context)
        {
            _context = context;
        }

        // GET: api/Alumnos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alumno>>> GetAlumnos()
        {
            try
            {
                var alumnos = await _context.Alumnos.ToListAsync();
                return Ok(alumnos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al recuperar los alumnos", error = ex.Message });
            }
        }

        // GET: api/Alumnos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Alumno>> GetAlumno(int id)
        {
            try
            {
                var alumno = await _context.Alumnos.FindAsync(id);

                if (alumno == null)
                {
                    return NotFound(new { message = "Alumno no encontrado" });
                }

                return Ok(alumno);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al recuperar el alumno", error = ex.Message });
            }
        }

        // POST: api/Alumnos
        [HttpPost]
        public async Task<ActionResult<Alumno>> PostAlumno(Alumno alumno)
        {
            try
            {
                _context.Alumnos.Add(alumno);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAlumno), new { id = alumno.Id }, alumno);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear el alumno", error = ex.Message });
            }
        }

        // PUT: api/Alumnos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlumno(int id, Alumno alumno)
        {
            if (id != alumno.Id)
            {
                return BadRequest(new { message = "ID mismatch" });
            }

            try
            {
                _context.Entry(alumno).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Alumnos.Any(e => e.Id == id))
                {
                    return NotFound(new { message = "Alumno no encontrado" });
                }
                else
                {
                    return StatusCode(500, new { message = "Error al actualizar el alumno" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error inesperado", error = ex.Message });
            }

            return NoContent();
        }

        // DELETE: api/Alumnos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlumno(int id)
        {
            try
            {
                var alumno = await _context.Alumnos.FindAsync(id);
                if (alumno == null)
                {
                    return NotFound(new { message = "Alumno no encontrado" });
                }

                _context.Alumnos.Remove(alumno);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Alumno eliminado con éxito" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar el alumno", error = ex.Message });
            }
        }
    }
}
