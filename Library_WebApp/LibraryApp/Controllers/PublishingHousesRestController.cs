using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Controllers {

    [Route("api/publishing-houses")]
    [ApiController]
    public class PublishingHousesRestController : ControllerBase {

        private readonly LibraryContext _context;

        public PublishingHousesRestController(LibraryContext context) {
            _context = context;
        }

        [Produces("application/json")]
        [HttpGet("search")]
        public async Task<IActionResult> Search() {
            try {
                string term = HttpContext.Request.Query["term"].ToString().ToLower();
                if (term.Length >= 3) {
                    var names = await _context.PublishingHouse
                        .Where(i => i.Name.ToLower().Contains(term))
                        .Select(i => i.Name)
                        .OrderBy(i => i.ToLower().IndexOf(term))
                            .ThenBy(i => i)
                        .Take(10)
                        .ToListAsync();
                    return Ok(names);
                }
                else if (term.Length > 0) {
                    var names = await _context.PublishingHouse
                        .Where(i => i.Name.ToLower().StartsWith(term))
                        .Select(i => i.Name)
                        .OrderBy(i => i)
                        .Take(10)
                        .ToListAsync();
                    return Ok(names);
                }
                return Ok(new List<string>());
            }
            catch {
                return BadRequest();
            }
        }
    }
}
