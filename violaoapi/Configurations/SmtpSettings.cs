using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace violaoapi.Configurations
{
    public class SmtpSettings
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}