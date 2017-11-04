using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiScaffoldNorthwind.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiSample.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly PostgresContext _dbContext;

        public ValuesController(PostgresContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Value> GetValues()
        {
            return _dbContext.Values;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Value value = await _dbContext.Values.SingleOrDefaultAsync(m => m.Id == id);

            if (value == null)
            {
                return NotFound();
            }

            return Ok(value);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> PostValue([FromBody]Value value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dbContext.Values.Add(value);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ValueExists(value.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetValue", new { id = value.Id }, value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutValue([FromRoute]int id, [FromBody]Value value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != value.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(value).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ValueExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteValue([FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Value value = await _dbContext.Values.SingleOrDefaultAsync(m => m.Id == id);
            if (value == null)
            {
                return NotFound();
            }

            _dbContext.Values.Remove(value);
            await _dbContext.SaveChangesAsync();

            return Ok(value);
        }

        private bool ValueExists(int id)
        {
            return _dbContext.Values.Any(e => e.Id == id);
        }
    }
}
