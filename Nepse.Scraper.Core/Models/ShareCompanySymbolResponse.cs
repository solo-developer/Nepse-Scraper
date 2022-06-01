using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nepse.Scraper.Core.Models
{
    public class ShareCompanySymbolResponse
    {
        public int Id { get; set; }
        public string StockName { get; set; }
        public string SectorName { get; set; }
        public string Symbol { get; set; }
        public string CompanyInfoUrl { get; set; }
    }
}
