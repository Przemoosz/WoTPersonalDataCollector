using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WotPersonalDataCollectorWebApp.CosmosDb.Context;

namespace WotPersonalDataCollectorWebApp.Controllers
{
    public class WotDataController: Controller
    {
        private readonly ICosmosDatabaseContext _context;

        public WotDataController(ICosmosDatabaseContext context)
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
