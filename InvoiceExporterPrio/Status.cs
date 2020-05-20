

using System;
using System.Threading;

namespace MessagingDemo.InvoiceExporterPrio
{
    public static class Status
    {
        private static int Invoices = 0;
        private static int Documents = 0;
        private static DateTime Started;
        private static bool ReportedInvoiced = false;
        private static bool ReportedDocuments = false;
        private static object _lock = new Object();

        public static void ImportStarted()
        {
            Started = DateTime.Now;
            while (Invoices + Documents < 2000)
            {
                Thread.Sleep(5000);
                System.Console.WriteLine($"{Invoices} invoices created successfully");
                System.Console.WriteLine($"{Documents} documents created successfully");
                if (Invoices >= 1000 && !ReportedInvoiced)
                {
                    Console.WriteLine($"Invoice creation finished in {(DateTime.Now - Started).TotalSeconds} seconds");
                    ReportedInvoiced = true;
                }
                if (Documents >= 1000 && !ReportedDocuments)
                {
                    Console.WriteLine($"Document creation finished in {(DateTime.Now - Started).TotalSeconds} seconds");
                    ReportedDocuments = true;
                }
            }
            System.Console.WriteLine($"Payment export finished in {(DateTime.Now - Started).TotalSeconds} seconds !");
        }

        public static void InvoiceCreated()
        {
            lock (_lock)
            {
                Invoices++;
            }
        }

        public static void DocumentCreated()
        {
            lock (_lock)
            {
                Documents++;
            }
        }
    }


}