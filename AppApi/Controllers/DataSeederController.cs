using AppData.DbContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataSeederController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DataSeederController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("seed")]
        public async Task<IActionResult> SeedDatabase()
        {
            await DataSeeder.SeedAsync(_context);
            return Ok("Database seeded successfully.");
        }
    }
}
