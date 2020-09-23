using API.Models;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IEmployeeCompetency
    {
        Task<EmployeeCompetency> GetAsync(int id, int employeeID);
        Task<IEnumerable<EmployeeCompetencyViewModel>> GetEmployeeCompetencyAsync(int id);
        Task<EmployeeCompetency> FindEmployeeAsync(int employeeID);
    }
    public class EmployeeCompetencyService : IService<EmployeeCompetency, int>, IEmployeeCompetency
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

        public async Task<IEnumerable<EmployeeCompetencyViewModel>> GetEmployeeCompetencyAsync(int id)
        {
           var test= from e in ctx.EmployeeCompetency
            join c in ctx.CompetencyDetail on e.CompetencyID equals c.CompetencyID
                     join f in ctx.CompetencyFramework on c.CompetencyFrameworkID equals f.CompetencyFrameworkID
                     where e.EmployeeID == id
            select new EmployeeCompetencyViewModel {
                EmployeeCompetencyID = e.EmployeeCompetencyID,
                CompetencyID = e.CompetencyID,
                EmployeeCommnet = e.EmployeeCommnet,
                ReviewerComment = e.ReviewerComment,
                FrameworkName = f.Name,
                CompetencyName = c.CompetencyName,
                IsComplete = e.IsComplete,
                IsSave = e.IsSave,
                IsDraft = e.IsDraft,
                CompetencyFrameworkID = f.CompetencyFrameworkID
            };

            return await test.ToListAsync();
        }

        public async Task<EmployeeCompetency> GetAsync(int id, int employeeID)
        {
            var res = await ctx.EmployeeCompetency.Where(x => x.CompetencyID == id && x.EmployeeID == employeeID).FirstOrDefaultAsync();
            return res;
        }

        public async Task<EmployeeCompetency> FindEmployeeAsync(int employeeID)
        {
            var res = await ctx.EmployeeCompetency.Where(x =>x.EmployeeID == employeeID).FirstOrDefaultAsync();
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

        public async Task<EmployeeCompetency> UpdateAsync(int id, EmployeeCompetency entity)
        {
            var result = ctx.EmployeeCompetency.SingleOrDefault(b => b.EmployeeCompetencyID == id);
            result.ReviewerComment = entity.ReviewerComment;
            result.IsComplete  = entity.IsComplete;

            ctx.Entry(result).State = EntityState.Modified;
            ctx.Entry(result);
            entity.ReviewDate = DateTime.Today;
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

        public static object CheckUpdateObject(object originalObj, object updateObj)
        {
            foreach (var property in updateObj.GetType().GetProperties())
            {
                if (property.GetValue(updateObj, null) == null)
                {
                    property.SetValue(updateObj, originalObj.GetType().GetProperty(property.Name)
                    .GetValue(originalObj, null));
                }
            }
            return updateObj;
        }
    }
}

