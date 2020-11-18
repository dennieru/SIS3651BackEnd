namespace Host
{
    using Microsoft.Owin.Hosting;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string url = "http://192.168.88.160:5050";
                using (WebApp.Start<Startup>(url))
                {
                    Console.WriteLine($"Services started at: {DateTime.UtcNow:D}");
                    Console.WriteLine($"Running...at Url: {url}");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
