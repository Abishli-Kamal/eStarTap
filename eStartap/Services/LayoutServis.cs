using eStartap.DAL;
using eStartap.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eStartap.Services
{
    public class LayoutServis
    {
        private readonly AppDbContext _context;

        public LayoutServis(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Setting>> GetDatas()
        {
            List<Setting> setting = await _context.Settings.ToListAsync();
            return setting;
        }
    }
}
