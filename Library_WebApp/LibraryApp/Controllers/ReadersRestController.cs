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
                        .Include(i => i.LoginNavigation)
                        .ToListAsync();
                    var keywords = term.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                    var result = names
                        .Where(i => {
                            var login = i.Login.ToLower();
                            var lastName = i.LoginNavigation.LastName.ToLower();
                            var firstName = i.LoginNavigation.FirstName.ToLower();
                            foreach (var keyword in keywords) {
                                if (!(login.StartsWith(keyword)
                                || lastName.StartsWith(keyword)
                                || firstName.StartsWith(keyword))) {
                                    return false;
                                }
                            }
                            return true;
                        })
                        .Select(i => new {
                            label = $"{i.LoginNavigation.LastName} {i.LoginNavigation.FirstName} ({i.Login})",
                            value = i.Login
                        })
                        .OrderBy(i => i.label)
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
