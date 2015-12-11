using System;

namespace SCS.SsisDashboard.Models
{
    public class Message
    {
        public long Id { get; set; }
        public string MessageText { get; set; }
        public DateTime Time { get; set; }
        public string Source { get; set; }
        public string Component { get; set; }
        public string TimeString
        {
            get
            {
                return this.Time.ToShortTimeString();
            }
        }
    }
}

