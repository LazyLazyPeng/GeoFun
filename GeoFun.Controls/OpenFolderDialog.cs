using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace GeoFun.Controls
{
    public class OpenFolderDialog
    {
        private FolderBrowserDialog fbd = new FolderBrowserDialog();

        /// <summary>
        /// 记录的历史路径的最多数量
        /// </summary>
        public static readonly int MAX_HISTORY_COUNT = 5;

        /// <summary>
        /// 记录的历史路径
        /// </summary>
        public static List<string> HISTORY_PATH = new List<string>();

        /// <summary>
        /// 选中的文件夹
        /// </summary>
        public string SelectedPath
        {
            get
            {
                return fbd.SelectedPath;
            }
            set
            {
                fbd.SelectedPath = value;
            }
        }

        /// <summary>
        /// 显示选择文件夹对话框
        /// </summary>
        /// <returns></returns>
        public DialogResult ShowDialog()
        {
            DialogResult result;

            //// 初始化为最近一次的路径
            if(HISTORY_PATH.Count>0)
            {
                fbd.SelectedPath = HISTORY_PATH.Last();
            }

            result = fbd.ShowDialog();
            if(result == DialogResult.OK)
            {
                if(HISTORY_PATH.Count>=5)
                {
                    HISTORY_PATH.RemoveAt(4);
                    HISTORY_PATH.Insert(0, fbd.SelectedPath);
                }
                else
                {
                    HISTORY_PATH.Add(fbd.SelectedPath);
                }
            }

            return result;
        }
    }
}
