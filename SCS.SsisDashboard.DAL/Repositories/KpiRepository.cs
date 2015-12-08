using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Linq.Dynamic;
using System.Data.Entity;
//using AgileApps.Common;
using SCS.SsisDashboard.Models;

namespace SCS.SissDashboard.DAL
{
    public class KpiRepository
    {
        private class KpiDTO
        {
            public int StatusId { get; set; }
            public int RowCount { get; set; }
        }

        protected EfContext context = new EfContext();

        protected virtual IQueryable<KPI> Query()
        {
            return context.Set<KPI>();
        }

        public virtual List<KPI> Fetch()
        {
            var sql = @"
                SELECT 0 StatusId, COUNT(*) [RowCount] 
                FROM [catalog].[executions] 
                UNION ALL SELECT [Status] StatusId, COUNT(*) [RowCount] 
                FROM [catalog].[executions] GROUP BY [status]";
            var data = context.Database.SqlQuery<KpiDTO>(sql).Select(k => new KPI
            {
                RowCount = k.RowCount,
                ExecutionStatus = Enum.GetName(typeof(ExecutionStatus), k.StatusId)
            })
            .ToList();
            return data;
        }
    }
}
