using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class CompetencyFrameworkService : IService<CompetencyFramework, int>
    {
        private readonly _DbContext ctx;
        /// <summary>
        /// Injecting the DbContext class in the Service
        /// </summary>
        /// <param name="ctx"></param>
        public CompetencyFrameworkService(_DbContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var res = await ctx.CompetencyFramework.FindAsync(id);
            if (res == null) return false;

            ctx.CompetencyFramework.Remove(res);
            await ctx.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CompetencyFramework>> GetAsync()
        {
            var res = await ctx.CompetencyFramework.ToListAsync();
            return res;
        }

        public async Task<CompetencyFramework> GetAsync(int id)
        {
            var res = await ctx.CompetencyFramework.FindAsync(id);
            return res;
        }

        public Task<CompetencyFramework> CreateAsync(CompetencyFramework entity)
        {
            throw new NotImplementedException();
        }

        public Task<CompetencyFramework> UpdateAsync(int id, CompetencyFramework entity)
        {
            throw new NotImplementedException();
        }
    }
}

