 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wpm.Management.Api.DataAccess;  // Make sure you have the correct namespace
using Wpm.Management.Api.Model;

namespace Wpm.Management.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        // Inject the ManagementDbContext directly
        private readonly ManagementDbContext _context;

        // Constructor with dependency injection
        public PetsController(ManagementDbContext dbContext)
        {
            _context = dbContext;
        }

        // Get all pets with their breeds included
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var allPets = await _context.Pets.Include(p => p.Breed).ToListAsync();
            return Ok(allPets);
        }

        [HttpGet("{id}",Name =nameof(GetById))]
        public async Task<IActionResult> GetById(int id)
        {
            var allPets = await _context.Pets.Include(p => p.Breed).Where(p=>p.Id ==id).FirstOrDefaultAsync();
            return Ok(allPets);
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewPet newPet)
        {
            var pet = newPet.ToPet();
            await _context.Pets.AddAsync(pet);
            await _context.SaveChangesAsync();
            return CreatedAtRoute(nameof(GetById),new {id =pet.Id},newPet);
        }


    }
}

