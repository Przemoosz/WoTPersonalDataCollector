using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WotPersonalDataCollectorWebApp.CosmosDb.Context;
using WotPersonalDataCollectorWebApp.CosmosDb.Dto;

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
            ////var a = await _context.PersonalData.ToListAsync();
            _context.PersonalData.Add(new WotDataCosmosDbDto(){ AccountId = "232312312312", Id = "abc"});
            await _context.SaveChangesAsync(CancellationToken.None);
            return "ddsds";
        }

        [HttpGet]
        public async Task<string> All()
        {
            var a = await _context.PersonalData.ToListAsync();
            return a[0].AccountId;
        }
    }
}
