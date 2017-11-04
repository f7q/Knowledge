﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiSample.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Dapper;
using System.Data; //use Query();
using Microsoft.EntityFrameworkCore; //use GetConnection();

namespace WebApiSample.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        //private InMemoryDbContext _dbContext { get; set; }
        private SampleDbContext _dbContext { get; set; }
        // private SQLServerDbContext _dbContext { get; set; }
        //private PostgreSQLDbContext _dbContext { get; set; }
        private ILogger _logger { get; set; }

        /*
        public ValuesController(InMemoryDbContext dbContext, ILogger<ValuesController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public ValuesController(PostgreSQLDbContext dbContext, ILogger<ValuesController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        */
        public ValuesController(SampleDbContext dbContext, ILogger<ValuesController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        /*
        public ValuesController(SQLServerDbContext dbContext, ILogger<ValuesController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        */
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            IDbConnection connection = _dbContext.Database.GetDbConnection();
            dynamic result = connection.Query("select * from \"Values\"");
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            IDbConnection connection = _dbContext.Database.GetDbConnection();
            DynamicParameters dyna = new DynamicParameters();
            dyna.Add(nameof(id), id);
            dynamic result = connection.Query("select * from \"Values\" where id = @id", dyna);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Value value)
        {
            try
            {
                _dbContext.Values.Add(value);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return new NotFoundResult();
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Value value)
        {
            try
            {
                var result = _dbContext.Values.Where(i => i.Id == id).First();
                if(result != null) {
                    _dbContext.Values.Update(value);
                    _dbContext.SaveChanges();
                }
                else
                {
                    _dbContext.Values.Add(value);
                    _dbContext.SaveChanges();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return new NotFoundResult();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try { 
                var value = _dbContext.Values.Where(i => i.Id == id).First();
                _dbContext.Values.Remove(value);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return new NotFoundResult();

            }
        }
    }
}
