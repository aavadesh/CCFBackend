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

        public async Task<CompetencyFramework> CreateAsync(CompetencyFramework entity)
        {
            entity.CreatedBy = 1;
            entity.CreatedDate = DateTime.Today;
            var res = await ctx.CompetencyFramework.AddAsync(entity);
            await ctx.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<CompetencyFramework> UpdateAsync(int id, CompetencyFramework entity)
        {
            ctx.Entry(entity).State = EntityState.Modified;
            ctx.Entry(entity);
            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            return entity;
        }
    }
}

