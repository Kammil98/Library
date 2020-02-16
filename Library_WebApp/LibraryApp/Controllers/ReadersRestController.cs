using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Controllers {

    [Route("api/readers")]
    [ApiController]
    public class ReadersRestController : ControllerBase {

        private readonly LibraryContext _context;

        public ReadersRestController(LibraryContext context) {
            _context = context;
        }

        [Produces("application/json")]
        [HttpGet("search")]
        public async Task<IActionResult> Search() {
            try {
                string term = HttpContext.Request.Query["term"].ToString().ToLower();
                if (term.Length > 3) {
                    var names = await _context.Reader
                        .Where(i => i.Login.ToLower().Contains(term))
                        .Select(i => i.Login)
                        .ToListAsync();
                    return Ok(names);
                }
                else {
                    return Ok(new List<string>());
                }
            }
            catch {
                return BadRequest();
            }
        }
    }
}
