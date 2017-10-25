using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;

namespace WebApiRebootSample.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private IApplicationLifetime _life;
        public ValuesController(IApplicationLifetime life)
        {
            this._life = life;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        // GET api/values/safeRestart
        [HttpGet]
        [Route("safeRestart")]
        public void GetSafeRestart()
        {
            var process = Process.GetCurrentProcess();
            var processId = process.Id;
            using (var newprocess = new Process())
            {
                var startinfo = process.StartInfo;
                Console.WriteLine(@"running...?{0}", process.ExitCode);
                newprocess.StartInfo = startinfo;
                process.Refresh();
                Console.WriteLine(@"stop {0}", process.ExitCode);
                process.Kill();
                //process.WaitForExit();
                //Environment.Exit(0);
                //Program.Main();
                if (process.HasExited)
                {
                    Console.WriteLine(@"restart {0}", process.ExitCode);
                    newprocess.Start();
                    Console.WriteLine(@"restart {0}", process.ExitCode);
                }
            }
            //this._life.StopApplication();
            //this._life.ApplicationStarted;

            return ;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
