using Microsoft.AspNetCore.Mvc;

namespace NetCore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly NetCoreDemoContext _context;
        public StatesController(NetCoreDemoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> OnGetAsync()
        {
            return Ok(await _context.States.ToListAsync());
        }
    }
}
