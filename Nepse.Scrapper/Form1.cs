using Nepse.Scraper.Core;

namespace Nepse.Scrapper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            var shareCompanies = await ScrapperService.ScrapShareCompanies();
            dgvShareCompanies.DataSource = shareCompanies;
        }
    }
}