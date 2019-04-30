using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Diagnostics;

namespace GeoFun.Sys
{
    public class CMDHelper
    {
        public int SkipMessNum = 4;
        public int messNum = 0;
        public event EventHandler<DataReceivedEventArgs> OnOutputDataReceived;
        public event EventHandler<DataReceivedEventArgs> OnErrorDataReceived;

        /// <summary>
        /// 异步执行命令
        /// </summary>
        /// <param name="cmd"></param>
        public virtual void Execute(string cmd)
        {
            using (Process proc = new Process())
            {
                //// 程序名称
                proc.StartInfo.FileName = "cmd.exe";
                //// 程序参数
                ////proc.StartInfo.Arguments = cmd;
                //// 必须禁用操作系统外壳程序
                proc.StartInfo.UseShellExecute = false;
                //// 不显示命令行窗口
                proc.StartInfo.CreateNoWindow = true;
                //// 重定向输入
                proc.StartInfo.RedirectStandardInput = true;
                //// 重定向输出
                proc.StartInfo.RedirectStandardOutput = true;
                //// 重定向错误输出
                proc.StartInfo.RedirectStandardError = true;

                //proc.StartInfo.StandardOutputEncoding = Encoding.GetEncoding("gbk");
                //proc.StartInfo.StandardErrorEncoding = Encoding.GetEncoding("gbk");

                if (proc.Start())
                {
                    proc.OutputDataReceived += Proc_OutputDataReceived;
                    proc.ErrorDataReceived += Proc_ErrorDataReceived;

                    //// 异步获取命令行内容
                    proc.BeginOutputReadLine();
                    proc.BeginErrorReadLine();

                    //proc.StandardInput.WriteLine("@echo off");//proc.StandardInput.Write("chcp 936");
                    proc.StandardInput.WriteLine(cmd);

                    //// 没过1秒查询程序是否退出
                    while (!proc.HasExited)
                    {
                        System.Threading.Thread.Sleep(1000);
                    }
                }
            }
        }

        /// <summary>
        /// 同步执行命令，无返回结果
        /// </summary>
        /// <param name="cmd"></param>
        public virtual void ExecuteNoWait(string cmd)
        {
            //调用外部程序导cmd命令行
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            //// 必须禁用操作系统外壳程序
            p.StartInfo.UseShellExecute = false;
            //// 不显示命令行窗口
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            p.StandardInput.WriteLine(cmd);
            //cmd又调用了ociuldr.ex
            //string output = p.StandardOutput.ReadToEnd(); 这句可以用来获取执行命令的输出结果
        }

        public virtual void ExecuteExe(string path)
        {
            //调用外部程序导cmd命令行
            Process p = new Process();
            p.StartInfo.FileName =path;
            p.Start();
        }

        public virtual void Proc_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            OnErrorDataReceived?.Invoke(sender, e);
        }

        public virtual void Proc_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            messNum++;
            if (messNum <= SkipMessNum) return;
            //if (string.IsNullOrWhiteSpace(e.Data)) return;

            OnOutputDataReceived?.Invoke(sender, e);
        }
    }
}
