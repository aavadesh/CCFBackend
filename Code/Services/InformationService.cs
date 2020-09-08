using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class InformationService : IService<Information, int>
    {
        private readonly _DbContext ctx;
        /// <summary>
        /// Injecting the DbContext class in the Service
        /// </summary>
        /// <param name="ctx"></param>
        public InformationService(_DbContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<Information> CreateAsync(Information entity)
        {
            var res = await ctx.Information.AddAsync(entity);
            await ctx.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var res = await ctx.Information.FindAsync(id);
            if (res == null) return false;

            ctx.Information.Remove(res);
            await ctx.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Information>> GetAsync()
        {
            var res = await ctx.Information.ToListAsync();
            return res;
        }

        public async Task<Information> GetAsync(int id)
        {
            var res = await ctx.Information.FindAsync(id);
            return res;
        }

        public Task<Information> UpdateAsync(int id, Information entity)
        {
            throw new NotImplementedException();
        }
    }
}

