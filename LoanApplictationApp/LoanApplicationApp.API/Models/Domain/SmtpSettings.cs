﻿namespace LoanApplictationApp.API.Models.Domain
{
    public class SmtpSettings
    {
        public string Host { get; set; } = "";
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
        public string User { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
