﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCS.SissDashboard.DAL
{
	public enum ExecutionStatus
	{
         all,
		 created,
		 running,
		 cancelled,
		 failed,
		 pending,
		 ended_unexpectedly,
		 succeeded,
		 stopping,
		 completed,
	}
}
