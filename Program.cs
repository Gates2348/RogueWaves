using HtmlAgilityPack;
using System;
using System.Data;
using System.Net;
using System.Xml;
using Microsoft.SyndicationFeed;
using System.ServiceModel.Syndication;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using System.Text;

namespace Webscraper{
    class Program
    {
        static void Main(string[] args)
        {

            string fullURL = "https://www.ndbc.noaa.gov/data/5day2/41004_5day.spec";
            string testURL = "https://www.ndbc.noaa.gov/data/realtime2/51101.spec";            

            //List<string> list = new List<string>();
            string[] list = Directory.GetFiles(@"A:\New\Y"); 

            bool headerAdded = false;

            foreach (var list1 in list)
            {
                Console.WriteLine(list1);
                string test_List = "https://www.ndbc.noaa.gov/data/realtime2/" + Path.GetFileName(list1);

                var httpclient = new HttpClient();

                //Change URL below
                var html = httpclient.GetStringAsync(testURL).Result;
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);

                string fileName = @"A:\Rogue_Waves.csv";

                try
                {
                    using (StreamWriter writer = new StreamWriter(fileName, true))
                    {
                        if (headerAdded == false)
                        {
                            StringBuilder header = new StringBuilder();
                            header.AppendLine("year, Month, Day, Hour, Minute, Wave_Height, Swell_Height, SWP, WWH, WWP, SWD, WWD, Steepness, APD, MWD");
                            writer.Write(header.ToString());
                            headerAdded = true;
                        }

                        string y = "";

                        foreach (var item in htmlDocument.DocumentNode.InnerText.Skip(141))
                        {
                            if (item.ToString() != " ")
                            {
                                y += item.ToString();
                            }
                            else if (y != ",")
                            {
                                writer.Write(y);
                                y = ",";
                            }
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
}