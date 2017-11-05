using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiScaffoldNorthwind.Models;
using Microsoft.EntityFrameworkCore;
using LinqKit;

namespace WebApiScaffoldNorthwind.Controllers
{
    public class TestDTO
    {
        public Employees Emp { get; set; }
        public Territories Ter { get; set; }
    }
    [Produces("application/json")]
    [Route("api/LinqKit")]
    public class LinqKitController : Controller
    {
        private readonly PostgresContext _dbContext;

        public LinqKitController(PostgresContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        // GET api/values
        [HttpGet]
        public IActionResult GetLinqKitValues(
            [FromQuery] string lastname,
            [FromQuery] string firstname,
            [FromQuery] string title)
        {
            var predicate = PredicateBuilder.New<Employees>(true);
            if (!string.IsNullOrWhiteSpace(lastname))
            {
                predicate = predicate.And(x => x.Lastname == lastname);
            }
            if (!string.IsNullOrWhiteSpace(firstname))
            {
                predicate = predicate.And(x => x.Firstname == firstname);
            }
            if (!string.IsNullOrWhiteSpace(title))
            {
                // LikeŒn‚ÍãŽè‚­‚¢‚©‚È‚¢
                predicate = predicate.And(x => x.Title.StartsWith(title));
            }

            if (predicate.Parameters.Count > 0)
            {
                return Ok(_dbContext.Employees.AsExpandable().Where(predicate).ToList());
            }
            else
            {
                return NotFound();
            }
        }
    }
}