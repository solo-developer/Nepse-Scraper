using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Nepse.Scraper.Core.Models;

namespace Nepse.Scraper.Core
{
    public class ScrapperService
    {
        private const string _baseUrl = "http://nepalstock.com";
        public static async Task<List<ShareCompanySymbolResponse>> ScrapShareCompanies()
        {
            var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("_limit", "5000")
                });

            var myHttpClient = new HttpClient();
            var response = await myHttpClient.PostAsync($"{_baseUrl}/company", formContent);
            var stringContent = await response.Content.ReadAsStringAsync();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(stringContent);

            var tableRows = doc.DocumentNode.SelectNodes("//div[@id='company-list']/table/tr");

            List<ShareCompanySymbolResponse> result = new List<ShareCompanySymbolResponse>();

            // -1 for pager row
            for (int i = 0; i < tableRows.Count-1; i++)
            {
                // first two rows are header and filter row
                if (i < 2)
                    continue;
                var shareDetail = tableRows[i];

                var td = shareDetail.ChildNodes.Where(a=>a.Name=="td").ToList();
                var companyUrlNode = td[5].ChildNodes.Where(a=>a.Name=="a").First();

                var companyUrl = companyUrlNode.Attributes.Single(a => a.Name == "href").Value;               
                result.Add(new ShareCompanySymbolResponse
                {
                    Id= Int32.Parse(companyUrl.Replace("http://nepalstock.com/company/display/","")),
                    StockName= td[2].InnerText,
                    Symbol= td[3].InnerText,
                    SectorName= td[4].InnerText,
                    CompanyInfoUrl= companyUrl
                });
            }

            return result;
        }
    }
}
