using HtmlAgilityPack;
using System;
using System.Data;
using System.Net;
using System.Xml;
using Microsoft.SyndicationFeed;
using System.ServiceModel.Syndication;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;

namespace Webscraper{
    class Program
    {
        static void Main(string[] args)
        {

            string fullURL = "https://www.ndbc.noaa.gov/data/5day2/41004_5day.spec";

            //string RssUrl = "https://www.ndbc.noaa.gov/rss/ndbc_obs_search.php?lat=40N&lon=73W&radius=1000";
            var httpclient = new HttpClient();
            var html = httpclient.GetStringAsync(fullURL).Result;
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            string fileName = @"A:\Rogue_Waves.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    foreach (var item in htmlDocument.DocumentNode.InnerText.Skip(141))
                    {
                        writer.Write(item.ToString());                        
                    }
                }                    
            }
            catch (Exception exp)
            {
                Console.Write(exp.Message);
            }

        }         

    }
}