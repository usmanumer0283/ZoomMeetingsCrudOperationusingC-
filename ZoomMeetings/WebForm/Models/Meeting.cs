using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm.Models
{
    public class Meeting
    {

        public string MeetingId { get; set; }
        public string topic { get; set; }
        public string duration { get; set; }
        public string agenda { get; set; }
        public string ApiSecrate { get; set; }
        public string ApiKey { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string type { get; set; }
        public string jwtToken { get; set; }
    }
}