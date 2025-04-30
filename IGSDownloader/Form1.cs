using FluentFTP;

namespace IGSDownloader
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void nudYear_ValueChanged(object sender, EventArgs e)
        {
            int year = (int)nudYear.Value;
            int doy = (int)nuDOY.Value;
            if (DateTime.IsLeapYear(year))
            {
                nuDOY.Maximum = 366;

                if (lblLeapYear.Text != "闰年")
                {
                    lblLeapYear.Text = "闰年";
                }
            }
            else
            {
                nuDOY.Maximum = 365;

                if (nuDOY.Value > 365)
                {
                    nuDOY.Value = 365;
                }

                if (lblLeapYear.Text != "平年")
                {
                    lblLeapYear.Text = "平年";
                }
            }
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
        }

        public async Task<List<FtpListItem>> GetFtpFileList()
        {
            // create an FTP client and specify the host, username and password
            // (delete the credentials to use the "anonymous" account)
            FtpClient client = new FtpClient("ftp://igs.gnsswhu.cn/pub/gps/data/daily/%Y/%n/%yd/%s%n0.%yd.Z", "Anonymous", "YOUR_EMAIL@example.com");

            // connect to the server and automatically detect working FTP settings
            await client.AutoConnectAsync();

            List<FtpListItem> list = new List<FtpListItem>();
            // get a list of files and directories in the "/htdocs" folder

            foreach (FtpListItem item in await client.GetListingAsync("/pub/gps/data/daily"))
            {

                // if this is a file
                if (item.Type == FtpFileSystemObjectType.File)
                {

                    // get the file size
                    long size = await client.GetFileSizeAsync(item.FullName);

                    list.Add(item);
                }
            }

            return list;
        }
    }
}