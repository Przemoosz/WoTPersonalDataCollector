using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WotPersonalDataCollectorWebApp.CosmosDb.Context;

namespace WotPersonalDataCollectorWebApp.Controllers
{
    public class WotDataController: Controller
    {
        private readonly CosmosDatabaseContext _context;

        public WotDataController(CosmosDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<string> All()
        {
           var a = await  _context.PersonalData.ToListAsync();
           return a[0].AccountId;
        }
    }
}
