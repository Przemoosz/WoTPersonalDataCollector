using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WotPersonalDataCollector.WebApp.CosmosDb.Context;

namespace WotPersonalDataCollector.WebApp.Controllers
{
	public class WotController : Controller
    {
        private readonly ICosmosDatabaseContext _context;

        public WotController(ICosmosDatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<string> All()
        {
            var a = await _context.PersonalData.ToListAsync();
            StringBuilder sb = new StringBuilder();
            foreach (var wotDataCosmosDbDto in a)
            {
	            sb.AppendLine(wotDataCosmosDbDto.CreationDate);
            }
            return sb.ToString();
        }
    }
}
