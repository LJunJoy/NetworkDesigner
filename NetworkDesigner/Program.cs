using NetworkDesigner.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkDesigner
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            ThreadExceptionHandler handler = new ThreadExceptionHandler();//全局未捕获异常的处理
            Application.ThreadException += new ThreadExceptionEventHandler(handler.Application_ThreadException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
        /*
            System.Windows.Forms.Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);  
            System.Windows.Forms.Application.SetUnhandledExceptionMode(System.Windows.Forms.UnhandledExceptionMode.CatchException);  
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);  
  
        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)  
        {  
            MessageBox.Show(((Exception)e.ExceptionObject).Message + "\r\n" + ((Exception)e.ExceptionObject).StackTrace, "系统信息");  
        }  
  
        void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)  
        {  
            MessageBox.Show(e.Exception.Message + "\r\n" + e.Exception.StackTrace, "系统信息");  
        }
         */
    }

    internal class ThreadExceptionHandler
    {
        //实现错误异常事件
        public void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            try
            {
                //如果用户单击"Abort"则退出应用程序
                DialogResult result = ShowThreadExceptionDialog(e.Exception);
                if (result == DialogResult.Abort)
                {
                    Application.Exit();
                }
            }
            catch
            {
                try
                {
                    MessageBox.Show("严重错误", "严重错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }
        }
        private DialogResult ShowThreadExceptionDialog(Exception e)
        {
            string errorMsg = "错误信息:\t\t" + e.Message + "\t\t" + e.GetType() + "\t\t" + e.StackTrace;
            return MessageBox.Show(errorMsg, "Application Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
        }
    }
}
