using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GNSSIon
{
    /// <summary>
    /// WinFileSearch.xaml 的交互逻辑
    /// </summary>
    public partial class WinFileSearch : Window
    {
        /// <summary>
        /// 根目录
        /// </summary>
        public string RootFolder = "";

        public WinFileSearch()
        {
            InitializeComponent();
        }

        public List<FileInfo> SearchOFiles()
        {
            List<FileInfo> ofiles = new List<FileInfo>();

            string obsFolder = System.IO.Path.Combine(RootFolder, "obs");
            if (Directory.Exists(obsFolder) == false)
            {
                return ofiles;
            }

            DirectoryInfo dirInfo = new DirectoryInfo(obsFolder);
            foreach (var file in dirInfo.GetFiles())
            {
                var ext = file.Extension;
                if (string.IsNullOrEmpty(ext))
                {
                    continue;
                }
                ext = ext.ToLower();
                {
                    if (ext.EndsWith("o"))
                    {
                        ofiles.Add(file);
                    }
                }
            }

            return ofiles;
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
        }
    }
}
