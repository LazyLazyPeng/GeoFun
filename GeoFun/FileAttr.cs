using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun
{
    /// <summary>
    /// 数据文件的属性类
    /// Attributes of a data file.
    /// </summary>
    public class FileAttr
    {
        /// <summary>
        /// 标识是否包含文件头，默认为false
        /// A mark to identify whether the file has some header lines.
        /// </summary>
        public bool HasHeader { get; set; } = false;

        /// <summary>
        /// 文件头总行数，默认为0
        /// Header line numbers of the file.
        /// </summary>
        public int HeaderNum { get; set; } = 0;

        /// <summary>
        /// 文件头，每一个string表示一行
        /// Headers of the file that one string for each line.
        /// </summary>
        public List<string> Headers { get; set; } = new List<string>();

        /// <summary>
        /// 数据列数,默认为0
        /// Column number of the file if it containing data.
        /// </summary>
        public int ColNum { get; set; } = 0;
        /// <summary>
        /// 数据行数
        /// Row number of the file if it containing data.
        /// </summary>
        public int RowNum { get; set; } = 0;

        /// <summary>
        /// 数据是否包含序号
        /// A mark to identify whether a data row has order.
        /// </summary>
        public bool HasOrder { get; set; } = true;

        /// <summary>
        /// 每行数据一个序号
        /// List of orders for each data row.
        /// </summary>
        public List<string> Orders { get; set; }

        /// <summary>
        /// 注释行的第一个字符，例如"#"
        /// Some remarks to identify whether a line is annotation.
        /// </summary>
        public List<string> Annotation { get; set; }

        /// <summary>
        /// 数据分隔符
        /// Seperate char or data.
        /// </summary>
        public char Seperator { get; set; }

        /// <summary>
        /// 角度的格式
        /// Format of angle.
        /// </summary>
        public enumAngleFormat AngleFormat = enumAngleFormat.DD;
    }
}
