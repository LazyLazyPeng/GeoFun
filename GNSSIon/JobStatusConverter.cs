using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GNSSIon
{
    public class JobStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int type = (int)value;

            string typeStr = "未知";
            switch (type)
            {
                case 0:
                    typeStr = "新任务";
                    break;
                case 1:
                    typeStr = "正在运行";
                    break;
                case 9:
                    typeStr = "已完成";
                    break;
                case -1:
                    typeStr = "已暂停";
                    break;
                case -99:
                    typeStr = "运行错误";
                    break;
            }

            return typeStr;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

