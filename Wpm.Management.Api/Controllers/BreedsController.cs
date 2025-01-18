using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Wpm.Management.Api.DataAccess;
using Wpm.Management.Api.Model;

namespace Wpm.Management.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreedsController : ControllerBase
    {

        // Inject the ManagementDbContext directly
        private readonly ManagementDbContext _context;
        private readonly ILogger _logger;

        // Constructor with dependency injection
        public BreedsController(ManagementDbContext dbContext, ILogger<BreedsController> logger)
        {
            _context = dbContext;
            _logger = logger;
        }

        // Get all Breeds with their breeds included
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var allBreeds = await _context.Breeds.ToListAsync();
            return allBreeds != null ? Ok(allBreeds) : NotFound();
        }

        [HttpGet("{id}", Name = nameof(GetBreedById))]
        public async Task<IActionResult> GetBreedById(int id)
        {
            var allBreeds = await _context.Breeds.FindAsync(id);
            return allBreeds != null ? Ok(allBreeds) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewBreed newBreed)
        {
            try
            {
                var Breed = newBreed.ToBreed();
                await _context.Breeds.AddAsync(Breed);
                await _context.SaveChangesAsync();
                return CreatedAtRoute(nameof(GetBreedById), new { id = Breed.Id }, newBreed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
