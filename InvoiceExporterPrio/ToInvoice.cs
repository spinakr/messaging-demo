using System;
using System.Net.Mail;

namespace MessagingDemo.InvoiceExporterPrio
{
    public class ToInvoice
    {
        public ToInvoice(string ssn, string name, string email, int amount)
        {
            ValidateSsn(ssn);
            ValidateEmail(email);
            Ssn = ssn;
            Name = name;
            Email = email;
            Amount = amount;
        }

        private void ValidateSsn(string ssn)
        {
            if (ssn.Length > 10 || ssn.Length < 6) throw new Exception($"Invalid ssn {ssn}");
        }

        private void ValidateEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
            }
            catch (FormatException)
            {
                throw new Exception($"Invalid email {email}");
            }
        }

        public string Ssn { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Amount { get; set; }
    }
}
