using System;
using System.Collections.Generic;
using ButterCMS;
using ButterCMS.Models;
using Newtonsoft.Json;

public class PageFields
{
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }
}


namespace ButterCMSDemo 
{
    class Program
    {
        static void Main(string[] args)
        {
            var apiKey = Environment.GetEnvironmentVariable("API_KEY");
            if (string.IsNullOrEmpty(apiKey))
            {
                Console.WriteLine("API_KEY environment variable is not set");
                return;
            }

            var client = new ButterCMSClient(apiKey);

            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("preview", "1");

                var pageResponse = client.RetrievePage<PageFields>("*", "test-page-1", parameters);
                var page = pageResponse.Data;
                Console.WriteLine("Page retrieved successfully");
                Console.WriteLine($"Page: {page}");
                Console.WriteLine($"Page Slug: {page.Slug}");
                Console.WriteLine($"Page Status: {page.Status}");
                Console.WriteLine($"Page Scheduled: {page.Scheduled}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching page: {ex.Message}");
            }
        }
    }
}
