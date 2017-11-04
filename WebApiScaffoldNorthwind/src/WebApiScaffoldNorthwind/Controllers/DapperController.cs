using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiScaffoldNorthwind.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace WebApiScaffoldNorthwind.Controllers
{
    public class TestDTO
    {
        public Employees Emp { get; set; }
        public Territories Ter { get; set; }
    }
    [Produces("application/json")]
    [Route("api/Dapper")]
    public class DapperController : Controller
    {
        private readonly PostgresContext _dbContext;

        public DapperController(PostgresContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<dynamic> GetValues()
        {
            using (var connection = _dbContext.Database.GetDbConnection())
            {
                string sql = @"
select
  e.*,
  t.*
from
  employees e
inner join
  employeeterritories et
on
  e.employeeid = et.employeeid
inner join
  territories t
on
  et.territoryid = t.territoryid
";
                /* NG
                return connection.Query(sql, 
                    (Employees e, Territories t) => new {
                        Employees = e.Employeeid,
                        Territories = t.Territoryid
                    });*/
                /* NG
                SqlMapper.GridReader multiQueryResult = connection.QueryMultiple(sql);
                return multiQueryResult.Read(
                    (Employees e, Territories t) => new {
                        Employees = e.Employeeid,
                        Territories = t.Territoryid });*/
                //* OK
                var result = connection.Query<dynamic>(sql);
                return result;

                /* NG è„éËÇ≠Ç¢Ç©Ç»Ç¢
                var result = connection.Query<Employees, Territories, TestDTO>(sql,
                    (Employees emp, Territories ter, TestDTO dto) => { dto.Ter = ter; return dto },
                     splitOn: "employeeid").SingleOrDefault();
                return result;*/

                /* dynamic åãâ ÇÃéÊìæ
                var fieldList = ((IDictionary<string, object>)result.First())
                    .Select(x => new {x.Key, x.Value})
                    .ToList();
                return fieldList;
                */
            }
        }
    }
}