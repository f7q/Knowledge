using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace WebApiFileUploadSample.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> PostFile(IFormFile uploadedFile)
        {
            try {
                // full path to file in temp location
                //var filePath = Path.GetTempFileName(); Path.PathSeparator
                var filePath = Directory.GetCurrentDirectory() + "\\" + uploadedFile.FileName;

                if (uploadedFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(stream);
                    }
                }

                // process uploaded files
                // Don't rely on or trust the FileName property without validation.

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
