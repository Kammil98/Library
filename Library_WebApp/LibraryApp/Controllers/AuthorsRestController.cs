using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Controllers {

    [Route("api/authors")]
    [ApiController]
    public class AuthorsRestController : ControllerBase {

        private readonly LibraryContext _context;

        public AuthorsRestController(LibraryContext context) {
            _context = context;
        }

        [Produces("application/json")]
        [HttpGet("search")]
        public async Task<IActionResult> Search() {
            try {
                string term = HttpContext.Request.Query["term"].ToString().ToLower();
                if (term.Length >= 3) {
                    var names = await _context.Author
                        .ToListAsync();
                    var keywords = term.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                    var result = names
                        .Where(i => {
                            var lastName = i.LastName.ToLower();
                            var firstName = i.FirstName.ToLower();
                            foreach (var keyword in keywords) {
                                if (!(lastName.StartsWith(keyword)
                                || firstName.Contains(keyword))) {
                                    return false;
                                }
                            }
                            return true;
                        })
                        .Select(i => new {
                            label = $"{i.FirstName} {i.LastName} ({i.Country})",
                            value = i.Id
                        })
                        .OrderBy(i => i.label.ToLower().IndexOf(term))
                            .ThenBy(i => i.label)
                        .Take(10);
                    return Ok(result);
                }
                return Ok(new List<string>());
            }
            catch {
                return BadRequest();
            }
        }
    }
}
