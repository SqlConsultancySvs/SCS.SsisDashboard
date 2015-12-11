using SCS.SsisDashboard.Models;
using System.Collections.Generic;
using System.Linq;
using SCS.SsisDashboard.Models;

namespace SCS.SissDashboard.DAL
{
    public class MessageRepository
    {
        protected EfContext context = new EfContext();

        public virtual List<Message> Fetch(int executionId, MessageType messageType)
        {
            var sql = @"
                    SELECT 
	                    event_message_id                        Id
	                    ,CAST(message_time AS DATETIME)			Time
	                    ,message				                MessageText
	                    ,event_name				                EventName
	                    ,message_source_name	                Source
	                    ,ISNULL(subcomponent_name,'')	        Component
                    FROM catalog.event_messages m
                    WHERE operation_id = {0}
	                    AND m.message_type = {1}
                ";
            sql = string.Format(sql, executionId, (int)messageType);
            var data = context.Database.SqlQuery<Message>(sql).ToList(); ;
            return data;
        }
    }
}
