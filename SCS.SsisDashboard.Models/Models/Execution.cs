﻿using System;

namespace SCS.SsisDashboard.Models
{
    public class Execution
    {
        public long ExecutionId { get; set; }
        public string ProjectName { get; set; }
        public string PackageName { get; set; }
        public long ProjectLsn { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public string RunDateString
        {
            get
            {
                return this.StartTime.HasValue ? this.StartTime.Value.ToShortTimeString() : string.Empty;
            }
        }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string StartTimeString
        {
            get
            {
                return this.StartTime.HasValue ? this.StartTime.Value.ToShortTimeString() : string.Empty;
            }
        }
        public string EndTimeString
        {
            get
            {
                return this.EndTime.HasValue ? this.EndTime.Value.ToShortTimeString() : string.Empty;
            }
        }
        public int ElapsedTimeInMinutes { get; set; }
        public int NumberOfWarnings { get; set; }
        public int NumberOfErrors { get; set; }
    }
}

