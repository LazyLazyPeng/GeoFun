using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.Threading.Tasks;

namespace GeoFun.Controls
{
    public partial class OpenFileDialogEx : Form
    {
        /// <summary>
        /// 上次选择的路径
        /// </summary>
        private static DirectoryInfo LAST_DIRECTORY = null;

        private TreeNode selectedNode = null;

        private int skipMessNum = 0;

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

        private bool Initializing = false;

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
                    else if (lvItem.Tag is DirectoryInfo)
                    {
                        DirectoryInfo dir = lvItem.Tag as DirectoryInfo;
                        if (dir != null)
                        {
                            yield return dir.FullName;
                        }
                    }
                }
            }
        }

        public void InitView()
        {
            Initializing = true;

            cbxFilter.DataSource = Filters;
            cbxFilter.DisplayMember = "FilterStr";

            if (Filters.Count > 0)
            {
                cbxFilter.SelectedIndex = 0;
            }

            //// 加载当前用户桌面
            try
            {
                TreeNode nodeDesktop = new TreeNode();
                nodeDesktop.Text = "桌面";
                nodeDesktop.ImageIndex = 1;
                nodeDesktop.SelectedImageIndex = 1;
                nodeDesktop.Tag = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                treeNav.Nodes.Add(nodeDesktop);
            }
            catch { }


            //// 加载所有磁盘
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (var drive in drives)
            {
                TreeNode node = new TreeNode();
                node.Text = drive.Name;
                node.Tag = drive.RootDirectory;
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                treeNav.Nodes.Add(node);
            }


            TreeNode lastNode = null;

            //// 加载第一层子目录
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

            if (LAST_DIRECTORY != null)
            {
                bool flag = false;
                TreeNode curNode = null;
                while (true)
                {
                    TreeNodeCollection nodes = treeNav.Nodes;
                    if (curNode != null)
                    {
                        nodes = curNode.Nodes;
                    }

                    flag = false;
                    for (int i = nodes.Count - 1; i > -1; i--)
                    {
                        if (nodes[i].Tag is null) continue;
                        if (!(nodes[i].Tag is DirectoryInfo)) continue;
                        var curDir = nodes[i].Tag as DirectoryInfo;
                        if (curDir is null) continue;

                        if (LAST_DIRECTORY.FullName.StartsWith(curDir.FullName))
                        {
                            flag = true;
                            curNode = nodes[i];

                            try
                            {
                                if (nodes[i].Nodes.Count > 0) nodes[i].Nodes.Clear();
                                foreach (var dir in curDir.GetDirectories())
                                {
                                    //// 跳过隐藏文件
                                    if ((dir.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) continue;

                                    TreeNode curChild = new TreeNode();
                                    curChild.Text = dir.Name;
                                    curChild.Tag = dir;
                                    nodes[i].Nodes.Add(curChild);
                                }
                            }
                            catch { }
                        }
                        else
                        {
                            if (nodes[i].Text == "桌面") continue;
                            foreach (var dir in curDir.GetDirectories())
                            {
                                //// 跳过隐藏文件
                                if ((dir.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) continue;

                                TreeNode curChild = new TreeNode();
                                curChild.Text = dir.Name;
                                curChild.Tag = dir;
                                nodes[i].Nodes.Add(curChild);
                            }
                        }
                    }

                    if (!flag) break;
                }

                //// 展开到上次所选的目录
                lastNode = curNode;
                Stack<TreeNode> nodePath = new Stack<TreeNode>();
                while (curNode.Parent != null)
                {
                    nodePath.Push(curNode.Parent);
                    curNode = curNode.Parent;
                }

                skipMessNum = nodePath.Count;
                while (nodePath.Count > 0)
                {
                    var node = nodePath.Pop();
                    if (node != null) node.Expand();
                }
            }

            Initializing = false;

            if (lastNode != null)
            {
                treeNav.SelectedNode = lastNode;
            }
        }

        public OpenFileDialogEx()
        {
            InitializeComponent();
        }

        public new DialogResult ShowDialog()
        {
            InitView();
            return base.ShowDialog();
        }

        private void TreeNav_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (Initializing) return;

            LAST_DIRECTORY = e.Node?.Tag as DirectoryInfo;

            // 记录节点
            if (selectedNode != null)
            {
                selectedNode.BackColor = Color.White;
            }
            selectedNode = e.Node;
            e.Node.BackColor = Color.Orange;

            lvFiles.Items.Clear();
            DirectoryInfo dirInfo = e.Node.Tag as DirectoryInfo;
            if (dirInfo is null) return;

            var isFolder = false;
            var curFilter = "所有文件(*.*)|*.*";
            var filters = new List<string>();
            if (SelectedFilter != null)
            {
                curFilter = SelectedFilter.FilterStr;
                isFolder = SelectedFilter.IsFolder;
            }

            var filterSegs = curFilter.Split('|');
            for (int i = 1; i < filterSegs.Length; i += 2)
            {
                filters.Add(filterSegs[i]);
            }

            Task task = new Task(() =>
            {
                try
                {
                    if (isFolder)
                    {
                        foreach (var filter in filters)
                        {
                            foreach (var dir in dirInfo.GetDirectories(filter, SearchOption.TopDirectoryOnly))
                            {
                                //// 跳过隐藏文件
                                if ((dir.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) continue;

                                lvFiles.BeginInvoke(new Action(() =>
                                {
                                    ListViewItem item = new ListViewItem(new string[] { dir.Name, dir.LastWriteTime.ToString("yyyy年MM月dd日 HH:mm:ss"), "文件夹", "" });
                                    item.Tag = dir;
                                    lvFiles.Items.Add(item);
                                }));
                            }
                        }
                    }
                }
                catch { }


                try
                {
                    if (!isFolder)
                    {
                        foreach (var filter in filters)
                        {
                            foreach (var fileInfo in dirInfo.GetFiles(filter))
                            {
                                //// 跳过隐藏文件
                                if ((fileInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) continue;

                                lvFiles.BeginInvoke(new Action(() =>
                                {
                                    ListViewItem item = new ListViewItem(new string[] { fileInfo.Name, fileInfo.LastWriteTime.ToString("yyyy年MM月dd日 HH:mm:ss"), fileInfo.Extension, (fileInfo.Length / 1024).ToString() + "KB" });
                                    item.Tag = fileInfo;
                                    lvFiles.Items.Add(item);
                                }));
                            }
                        }
                    }
                }
                catch { }
            });

            task.Start();
        }

        private void TreeNav_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (Initializing) return;
            if (skipMessNum > 0)
            {
                skipMessNum--;
                return;
            }

            //LAST_DIRECTORY = e.Node?.Tag as DirectoryInfo;
            //if(e.Node.Nodes.Count == 0)
            //{
            //    try
            //    {
            //        DirectoryInfo parentDir = e.Node.Tag as DirectoryInfo;
            //        if (parentDir != null)
            //        {
            //            foreach (var childDir in parentDir.GetDirectories())
            //            {
            //                TreeNode node = new TreeNode();
            //                node.Text = childDir.Name;
            //                node.Tag = childDir;
            //                e.Node.Nodes.Add(node);
            //            }
            //        }
            //    }
            //    catch { }
            //}

            for (int i = 0; i < e.Node.Nodes.Count; i++)
            {
                DirectoryInfo dirInfo = e.Node.Nodes[i].Tag as DirectoryInfo;
                if (dirInfo is null) continue;

                try
                {
                    e.Node.Nodes[i].Nodes.Clear();
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
            //e.Node.Expand();
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
