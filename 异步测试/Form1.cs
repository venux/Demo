using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace 异步测试
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static string Print(string str = "测试")
        {
            Thread.Sleep(3000);
            return str;
        }

        private Task<string> PrintAsync(string str = "测试")
        {
            return Task.Run<string>(() =>
            {
                Thread.Sleep(3000);
                return str;
            });
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            string str = Print();
            string str2 = Print("测试2");
            this.richTextBox1.Text += str + "\n";
            this.richTextBox1.Text += str2 + "\n";
        }

        private async void btnAsync_Click(object sender, EventArgs e)
        {
            //string str = await PrintAsync();
            //string str2 = await PrintAsync("测试2");
            ////await不用进行任何操作即可访问UI线程
            //this.richTextBox1.Text += str + "\n";
            //this.richTextBox1.Text += str2 + "\n";


            //Task<string> t1 = PrintAsync();
            //Task<string> t2 = PrintAsync("测试2");

            //string[] result = await Task<string>.WhenAll(t1, t2);

            ////await不用进行任何操作即可访问UI线程
            //this.richTextBox1.Text += result[0] + "\n";
            //this.richTextBox1.Text += result[1] + "\n";

            string str=await NewPrintAsync();
            this.richTextBox1.Text += str + "\n";
        }

        private void btnAsync2_Click(object sender, EventArgs e)
        {
            /*
             * 上个btnAsync内部实现如下，编译器将await后所有代码置于ContinueWith中，接受一个task参数的返回类型为void的委托。
             */
            Task<string> task = PrintAsync();

            task.ContinueWith((t) =>
            {
                string str = t.Result;//Task的Result保持结果


                //此处报错，由于不是从创建该空间的线程访问导致
                this.richTextBox1.Text += str + "\n";
            });
        }

        #region 将旧版改为新版

        private static Func<string, string> func = Print;

        private IAsyncResult BeginPrint(string str, AsyncCallback callback, object obj)
        {
            return func.BeginInvoke(str, callback, obj);
        }

        private string EndPrint(IAsyncResult result)
        {
            return func.EndInvoke(result);
        }

        private Task<string> NewPrintAsync()
        {
          return Task<string>.Factory.FromAsync(BeginPrint, EndPrint, "测试", null);
        }

        #endregion
    }
}
