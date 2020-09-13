using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class EmployeeCompetencyService : IService<EmployeeCompetency, int>
    {
        private readonly _DbContext ctx;
        /// <summary>
        /// Injecting the DbContext class in the Service
        /// </summary>
        /// <param name="ctx"></param>
        public EmployeeCompetencyService(_DbContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var res = await ctx.EmployeeCompetency.FindAsync(id);
            if (res == null) return false;

            ctx.EmployeeCompetency.Remove(res);
            await ctx.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<EmployeeCompetency>> GetAsync()
        {
            var res = await ctx.EmployeeCompetency.ToListAsync();
            return res;
        }

        public async Task<EmployeeCompetency> GetAsync(int id)
        {
            var res = await ctx.EmployeeCompetency.FindAsync(id);
            return res;
        }

        public async Task<EmployeeCompetency> CreateAsync(EmployeeCompetency entity)
        {
            try
            {
                entity.CreatedID = 1;
                entity.CreatedDate = DateTime.Today;
                entity.ReviewID = 0;
                entity.ReviewerComment = string.Empty;
                entity.IsComplete = false;
                var res = await ctx.EmployeeCompetency.AddAsync(entity);
                await ctx.SaveChangesAsync();

                return res.Entity;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return null;
        }

        public Task<EmployeeCompetency> UpdateAsync(int id, EmployeeCompetency entity)
        {
            throw new NotImplementedException();
        }
    }
}

