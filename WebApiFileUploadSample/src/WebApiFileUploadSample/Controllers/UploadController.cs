﻿using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WebApiFileUploadSample.Controllers
{
    using WebApiFileUploadSample.Models;

    [Route("api/[controller]")]
    public class UploadController : Controller
    {
        private readonly ILogger logger;
        private readonly SQLiteDbContext _dbContext;
        public UploadController(SQLiteDbContext dbContext, ILogger<UploadController> logger)
        {
            this.logger = logger;
            _dbContext = dbContext;
        }
        // GET: api/values
        [HttpGet("text")]
        public IActionResult Get()
        {
            try
            {
                var list = _dbContext.Files.Select(x => new { Name = x.Name }).ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning("", ex);
                return NotFound();
            }
        }
        [HttpGet("download/{name}")]
        public IActionResult GetDownload(string name)
        {
            try
            {
                var val = _dbContext.Files.FirstOrDefault(d => d.Name == name);
                Response.ContentType = "application/octet-stream";
                return Ok(new MemoryStream(Encoding.UTF8.GetBytes(val.Text))); //File(val.Text, "text/xml");
            }
            catch (Exception ex)
            {
                this.logger.LogWarning("", ex);
                return NotFound();
            }
        }

        [HttpGet("text/{name}")]
        [Produces("text/xml")]
        public IActionResult Get(string name)
        {
            try
            {
                var val = _dbContext.Files.FirstOrDefault(d => d.Name == name);
                return Ok(new MemoryStream(Encoding.UTF8.GetBytes(val.Text))); //File(val.Text, "text/xml");
            }
            catch (Exception ex)
            {
                this.logger.LogWarning("", ex);
                return NotFound();
            }
        }

        // POST api/values
        [HttpPost]
        [Route("upload/{id}/")]
        public async Task<IActionResult> PostFile([FromRoute]short id, IFormFile uploadedFile)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    var val = new File();
                    val.Name = uploadedFile.FileName;
                    await uploadedFile.CopyToAsync(memoryStream);
                    val.Text = Encoding.UTF8.GetString(memoryStream.ToArray());
                    _dbContext.Files.Add(val);
                    _dbContext.SaveChanges();
                }
                // process uploaded files
                // Don't rely on or trust the FileName property without validation.

                return Ok();
            }
            catch (Exception ex)
            {
                this.logger.LogWarning("", ex);
                return BadRequest();
            }
        }

        // DELETE api/values/5
        [HttpDelete("textid/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var val = _dbContext.Files.FirstOrDefault(d => d.Id == id);
                _dbContext.Remove(val);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                this.logger.LogWarning("", ex);
                return NotFound();
            }
        }
        // GET api/values/5
        [HttpDelete("filename/{name}")]
        public IActionResult Delete(string name)
        {
            try
            {
                var val = _dbContext.Files.FirstOrDefault(d => d.Name == name);
                _dbContext.Remove(val);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                this.logger.LogWarning("", ex);
                return NotFound();
            }
        }
    }
}
