using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace BTOAssistApp.Models
{
    public class PhoneInfo
    {
        [PrimaryKey]
        public string deviceID { get; set; }

        public string accessToken { get; set; }
    }
}
