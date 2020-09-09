using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface ICompetencyDetail
    {
        Task<IEnumerable<CompetencyDetail>> GetCompetencyNameByFrameworkID(int competencyFrameworkID);
    }
    public class CompetencyDetailService : IService<CompetencyDetail, int>, ICompetencyDetail
    {
        private readonly _DbContext ctx;
        /// <summary>
        /// Injecting the DbContext class in the Service
        /// </summary>
        /// <param name="ctx"></param>
        public CompetencyDetailService(_DbContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var res = await ctx.CompetencyDetail.FindAsync(id);
            if (res == null) return false;

            ctx.CompetencyDetail.Remove(res);
            await ctx.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CompetencyDetail>> GetAsync()
        {
            var res = await ctx.CompetencyDetail.ToListAsync();
            return res;
        }

        public async Task<CompetencyDetail> GetAsync(int id)
        {
            var res = await ctx.CompetencyDetail.FindAsync(id);
            return res;
        }

        public async Task<CompetencyDetail> CreateAsync(CompetencyDetail entity)
        {
            entity.CreatedBy = 1;
            entity.CreatedDate = DateTime.Today;
            var res = await ctx.CompetencyDetail.AddAsync(entity);
            await ctx.SaveChangesAsync();
            return res.Entity;
        }

        public Task<CompetencyDetail> UpdateAsync(int id, CompetencyDetail entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CompetencyDetail>> GetCompetencyNameByFrameworkID(int competencyFrameworkID)
        {
            var res = await ctx.CompetencyDetail.Where(x=>x.CompetencyFrameworkID == competencyFrameworkID).ToListAsync();
            return res;
        }
    }
}

