using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WotPersonalDataCollectorWebApp.CosmosDb.Context;

namespace WotPersonalDataCollectorWebApp.Controllers
{
    public class WotController : Controller
    {
        private readonly ICosmosDatabaseContext _context;

        public WotController(ICosmosDatabaseContext context)
        {
            _context = context;
        }
        public async Task<string> Index()
        {
            var a = await _context.PersonalData.ToListAsync();
            return a[0].AccountId;
        }

        [HttpGet]
        public async Task<string> All()
        {
            var a = await _context.PersonalData.ToListAsync();
            return a[0].AccountId;
        }
    }
}
