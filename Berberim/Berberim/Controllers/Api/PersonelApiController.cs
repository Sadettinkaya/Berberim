using Berberim.Db_context;
using Berberim.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Berberim.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonelApiController : ControllerBase
    {
        private readonly Appdb_context _context;

        public PersonelApiController(Appdb_context context)
        {
            _context = context;
        }

        // GET: api/PersonelApi
        [HttpGet]
        public async Task<IActionResult> GetPersonels()
        {
            var personnels = await _context.personnels.ToListAsync();
            return Ok(personnels);
        }

        // GET: api/PersonelApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonel(int id)
        {
            var personel = await _context.personnels.FindAsync(id);
            if (personel == null) return NotFound();
            return Ok(personel);
        }

        // POST: api/PersonelApi
        [HttpPost]
        public async Task<IActionResult> AddPersonel([FromBody] Personel personel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.personnels.Add(personel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPersonel), new { id = personel.personelID }, personel);
        }

        // PUT: api/PersonelApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersonel(int id, [FromBody] Personel updatedPersonel)
        {
            if (id != updatedPersonel.personelID) return BadRequest();

            var personel = await _context.personnels.FindAsync(id);
            if (personel == null) return NotFound();

            personel.personelName = updatedPersonel.personelName;
            personel.personelEmail = updatedPersonel.personelEmail;
            personel.musaitSaat = updatedPersonel.musaitSaat;

            _context.personnels.Update(personel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/PersonelApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonel(int id)
        {
            var personel = await _context.personnels.FindAsync(id);
            if (personel == null) return NotFound();

            _context.personnels.Remove(personel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
