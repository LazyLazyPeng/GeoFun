using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GeoFun.Controls
{
    public partial class OpenFileDialogEx : Form
    {
        private TreeNode selectedNode = null;

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value;
            }
        }

        /// <summary>
        /// 过滤器
        /// </summary>
        public List<Filter> Filters = new List<Filter>();

        /// <summary>
        /// 用户选择的过滤器
        /// </summary>
        public Filter SelectedFilter
        {
            get
            {
                if (cbxFilter.SelectedIndex < 0) return null;
                else
                {
                    return Filters[cbxFilter.SelectedIndex];
                }
            }
        }

        /// <summary>
        /// 是否允许选择多个
        /// </summary>
        public bool MultiSelect
        {
            get
            {
                return lvFiles.MultiSelect;
            }
            set
            {
                lvFiles.MultiSelect = value;
            }
        }

        /// <summary>
        /// 当前的文件夹
        /// </summary>
        public DirectoryInfo CurrentDirectory { get; set; }

        public IEnumerable<string> SelectedPaths
        {
            get
            {
                foreach (var item in lvFiles.SelectedItems)
                {
                    if (item is null) continue;
                    ListViewItem lvItem = item as ListViewItem;

                    if (lvItem.Tag is null) continue;
                    if (lvItem.Tag is FileInfo)
                    {
                        FileInfo file = lvItem.Tag as FileInfo;
                        if (file != null)
                        {
                            yield return file.FullName;
                        }
                    }
                    else if(lvItem.Tag is DirectoryInfo)
                    {
                        DirectoryInfo dir = lvItem.Tag as DirectoryInfo;
                        if(dir != null)
                        {
                            yield return dir.FullName;
                        }
                    }
                }
            }
        }

        public void InitView()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (var drive in drives)
            {
                TreeNode node = new TreeNode();
                node.Text = drive.Name;
                node.Tag = drive.RootDirectory.FullName;
                treeNav.Nodes.Add(node);
            }

            for (int i = 0; i < treeNav.Nodes.Count; i++)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(treeNav.Nodes[i].Tag.ToString());
                try
                {
                    foreach (var dir in dirInfo.GetDirectories())
                    {
                        TreeNode node = new TreeNode();
                        node.Text = dir.Name;
                        node.Tag = dir;
                        treeNav.Nodes[i].Nodes.Add(node);
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }
            }

            cbxFilter.DataSource = Filters;
            cbxFilter.DisplayMember = "FilterStr";

            if (Filters.Count > 0)
            {
                cbxFilter.SelectedIndex = 0;
            }
        }

        public OpenFileDialogEx()
        {
            InitializeComponent();
        }

        private void TreeNav_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // 记录节点
            selectedNode = e.Node;

            lvFiles.Items.Clear();
            DirectoryInfo dirInfo = e.Node.Tag as DirectoryInfo;
            if (dirInfo is null) return;

            string curFilter = "所有文件(*.*)|*.*";
            if (SelectedFilter != null)
            {
                curFilter = SelectedFilter.FilterStr;
            }
            curFilter = curFilter.Split('|')[1];

            try
            {
                if (SelectedFilter is null || SelectedFilter.IsFolder)
                {
                    foreach (var dir in dirInfo.GetDirectories(curFilter, SearchOption.TopDirectoryOnly))
                    {
                        //// 跳过隐藏文件
                        if ((dir.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) continue;
                        ListViewItem item = new ListViewItem(new string[] { dir.Name, dir.LastWriteTime.ToString("yyyy年MM月dd日 HH:mm:ss"), "文件夹", "" });
                        item.Tag = dir;
                        lvFiles.Items.Add(item);
                    }
                }
            }
            catch { }


            try
            {
                if (SelectedFilter is null || !SelectedFilter.IsFolder)
                {
                    foreach (var fileInfo in dirInfo.GetFiles(curFilter))
                    {
                        //// 跳过隐藏文件
                        if ((fileInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) continue;
                        ListViewItem item = new ListViewItem(new string[] { fileInfo.Name, fileInfo.LastWriteTime.ToString("yyyy年MM月dd日 HH:mm:ss"), fileInfo.Extension, (fileInfo.Length / 1024).ToString() + "KB" });
                        item.Tag = fileInfo;
                        lvFiles.Items.Add(item);
                    }
                }
            }
            catch { }

        }

        private void TreeNav_AfterExpand(object sender, TreeViewEventArgs e)
        {
            for (int i = 0; i < e.Node.Nodes.Count; i++)
            {
                DirectoryInfo dirInfo = e.Node.Nodes[i].Tag as DirectoryInfo;
                if (dirInfo is null) continue;

                try
                {
                    foreach (var child in dirInfo.GetDirectories())
                    {
                        TreeNode node = new TreeNode();
                        node.Text = child.Name;
                        node.Tag = child;
                        e.Node.Nodes[i].Nodes.Add(node);
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }

            }
            e.Node.Expand();
        }

        private void LvFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvFiles.SelectedItems.Count <= 0) return;
            if (lvFiles.SelectedItems[0] is null) return;

            if (lvFiles.SelectedItems[0].Tag is FileInfo)
            {
                var selectedFile = lvFiles.SelectedItems[0].Tag as FileInfo;
                if (selectedFile is null) return;

                tbxSelectedFile.Text = selectedFile.Name;
            }
            else
            {
                var selectedDir = lvFiles.SelectedItems[0].Tag as DirectoryInfo;
                if (selectedDir is null) return;

                tbxSelectedFile.Text = selectedDir.Name;
            }
        }

        private void CbxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedNode is null) return;

            TreeNav_AfterSelect(treeNav, new TreeViewEventArgs(selectedNode));
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
