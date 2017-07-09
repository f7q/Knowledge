using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WebApiFileUploadSample.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiFileUploadSample.Controllers
{
    [Route("api/[controller]")]
    public class FilesController : Controller
    {
        private readonly SQLiteDbContext _dbContext;
        public FilesController(SQLiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            try {
                var list = _dbContext.Values.ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        // GET api/values/5
        [HttpGet("api/Image/{name}")]
        public IActionResult Get(string name)
        {
            try
            {
                var val = _dbContext.Values.FirstOrDefault(d => d.Name == name);
                return File(val.Image, "image/png");
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        // POST api/values
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> PostFile(IFormFile uploadedFile)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    var val = new Value();
                    val.Name = uploadedFile.FileName;
                    await uploadedFile.CopyToAsync(memoryStream);
                    val.Image = memoryStream.ToArray();
                    _dbContext.Values.Add(val);
                    _dbContext.SaveChanges();
                }
                // process uploaded files
                // Don't rely on or trust the FileName property without validation.

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // DELETE api/values/5
        [HttpDelete("api/Image/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var val = _dbContext.Values.FirstOrDefault(d => d.Id == id);
                _dbContext.Remove(val);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        // GET api/values/5
        [HttpDelete("api/Image/{name}")]
        public IActionResult Delete(string name)
        {
            try
            {
                var val = _dbContext.Values.FirstOrDefault(d => d.Name == name);
                _dbContext.Remove(val);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
