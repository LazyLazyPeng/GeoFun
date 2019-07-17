using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.IO
{
    public class FileChangeInformation
    {
        public string ID { get; set; } = string.Empty;

        public FileChangeType ChangeType { get; set; } = FileChangeType.Unknown;

        /// <summary>
        /// 文件原路径
        /// </summary>
        public string OldPath { get; set; }

        /// <summary>
        /// 文件新路径
        /// </summary>
        public string NewPath { get; set; }

        /// <summary>
        /// 文件原名称
        /// </summary>
        public string OldName { get; set; }

        /// <summary>
        /// 文件新名称
        /// </summary>
        public string NewName { get; set; }

        public FileChangeInformation(string id, FileChangeType changeType,
            string oldPath, string newPath, string oldName, string newName)
        {
            ID = id;
            ChangeType = changeType;
            OldPath = oldPath;
            NewPath = newPath;
            OldName = oldName;
            NewName = newName;
        }
    }
}
