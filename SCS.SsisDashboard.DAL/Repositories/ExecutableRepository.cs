using SCS.SsisDashboard.Models;
using System.Collections.Generic;
using System.Linq;

namespace SCS.SissDashboard.DAL
{
    public class ExecutableRepository
    {
        protected EfContext context = new EfContext();

        protected virtual IQueryable<Execution> Query()
        {
            return context.Set<Execution>();
        }

        public virtual List<Executable> Fetch(int executionId)
        {
            var sql = @"
                    SELECT e.executable_id	ExecutableId
	                    ,e.executable_name	Name
	                    ,e.package_name		PackageName
	                    ,e.package_path		PackagePath
	                    ,CAST(s.start_time AS DATETIME) StartTime
	                    ,CAST(s.end_time AS DATETIME) EndTime
	                    ,ISNULL(s.[execution_duration] /60000,0)	Duration
	                    ,CAST(s.[execution_result] AS INT)	ExecutionResult
	                    ,CAST(s.[execution_value] AS VARCHAR)	ExecutionValue
                    FROM [catalog].[executables] e
	                    LEFT JOIN [catalog].[executable_statistics] s ON s.executable_id = e.executable_id AND s.execution_id = e.execution_id
                    WHERE e.execution_id = {0}
                    ORDER BY e.executable_id ASC
                ";
            sql = string.Format(sql, executionId);
            var data = context.Database.SqlQuery<Executable>(sql).ToList(); ;
            return data;
        }
    }
}
