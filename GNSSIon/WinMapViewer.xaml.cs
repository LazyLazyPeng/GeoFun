using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace GNSSIon
{
    /// <summary>
    /// WinMapViewer.xaml 的交互逻辑
    /// </summary>
    public partial class WinMapViewer : Window
    {
        public WinMapViewer()
        {
            InitializeComponent();

            webView21.Source = new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "map.html"));
            webView21.NavigationCompleted += WebView21_NavigationCompleted;
        }

        private async void WebView21_NavigationCompleted(object sender, object e)
        {
            // 只调用一次，避免重复添加
            webView21.NavigationCompleted -= WebView21_NavigationCompleted;
            await AddMarker("beijing", "北京", 39.9042, 116.4074); 
        }

        private async Task<bool> AddMarker(string id, string name,double lat, double lon)
        {
            string script2 = $"CesiumMapAPI.addMarker({lat}, {lon}, 0, {{id: '{id}', name: '{name}',pixelSize: 10,color: '#ff0000'}});";
            try
            {
                await webView21.CoreWebView2.ExecuteScriptAsync(script2);
                return true;
            }
            catch (Exception ex)
            {
                // 简单调试输出，按需替换为日志或 UI 提示
                MessageBox.Show("执行脚本失败: " + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
    }
}
