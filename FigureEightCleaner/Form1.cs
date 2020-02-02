using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Forms;

namespace FigureEightCleaner
{
    public partial class Form1 : Form
    {


        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;

        public static void LeftMouseClick(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //webBrowser1.Navigate(new Uri("https://make.figure-eight.com/jobs"));
            webBrowser1.ScriptErrorsSuppressed = true;
            Navigate("https://make.figure-eight.com/jobs");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sleep(5, "");
            LeftMouseClick(1210, 148);
            Sleep(2, "");
            //int initid = Convert.ToInt32(textBox1.Text);

            int i = 1;
            int max = Convert.ToInt32(textBox2.Text);

            var lines = File.ReadAllLines("ids.txt").ToList();
            var newList = File.ReadAllLines("ids.txt").ToList();

            if (max > lines.Count)
            {
                max = lines.Count;
            }
            
            foreach (string line in lines)
            {
                this.Text = $"{i} z {max}";
                Application.DoEvents();

                Clipboard.SetText(line.Trim());
                LeftMouseClick(1800, 166);
                Sleep(1, "");
                LeftMouseClick(1795, 281);
                Sleep(5, "space");
                LeftMouseClick(619, 288);

                SendKeys.Send("^v");
                Sleep(1, "");
                SendKeys.Send("{ENTER}");
                Sleep(5, "id");

                Application.DoEvents();
                if (!textBox3.Text.Contains($"query={line.Trim()}"))
                {
                    File.Delete("ids.txt");
                    File.WriteAllLines("ids.txt", newList);
                    Environment.Exit(1);
                }
                Application.DoEvents();
                LeftMouseClick(1849, 390);
                Sleep(1, "");
                LeftMouseClick(1762, 505);
                Sleep(1, "");
                LeftMouseClick(1186, 625);
                Sleep(5, "finishing");

                newList = newList.Where(x => x != line).ToList();

                File.Delete("ids.txt");
                File.WriteAllLines("ids.txt", newList);

                if (i == max)
                    break;

                i++;
            }
        }

        private void Sleep (int seconds, string what)
        {
            var max = seconds * 1000;
            for (int i = 0; i <= max; i = i+10)
            {
                if (i%1000 == 0)
                {
                    
                    label3.Text = $"{(i / 1000).ToString()} / {seconds} - {what}";
                    Application.DoEvents();
                }
                Thread.Sleep(10);
                Application.DoEvents();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Navigate(textBox3.Text);
            }
        }

        private void Navigate(String address)
        {
            if (String.IsNullOrEmpty(address)) return;
            if (address.Equals("about:blank")) return;
            if (!address.StartsWith("http://") &&
                !address.StartsWith("https://"))
            {
                address = "http://" + address;
            }
            try
            {
                webBrowser1.Navigate(new Uri(address));
            }
            catch (System.UriFormatException)
            {
                return;
            }
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            textBox3.Text = webBrowser1.Url.ToString();
            Application.DoEvents();
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Navigate(textBox3.Text);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Sleep(2, "");
            LeftMouseClick(1210, 148);
            Sleep(1, "");
            //int initid = Convert.ToInt32(textBox1.Text);

            int i = 1;
            int max = Convert.ToInt32(textBox2.Text);

            var lines = File.ReadAllLines("ids.txt").ToList();
            var newLIst = File.ReadAllLines("ids.txt").ToList();

            if (max > lines.Count)
            {
                max = lines.Count;
            }
            foreach (string line in lines)
            {
                var start = DateTime.Now;

                this.Text = $"{i} z {max}";
                Application.DoEvents();

                Clipboard.SetText(line.Trim());
                LeftMouseClick(1800, 166);
                Sleep(1, "");
                LeftMouseClick(1795, 281);
                while (textBox3.Text != "https://make.figure-eight.com/jobs?scope=8b47f4bd-1349-477b-a44a-778bf5eb3eaa&query=&state=all&project=all&fields[]=tags&fields[]=title&fields[]=id&fields[]=owner_email&fields[]=alias")
                {
                    Sleep(1, "");
                }
                Sleep(1, "space");
                LeftMouseClick(619, 288);
                Sleep(1, "");

                SendKeys.Send("^v");
                Sleep(1, "");
                SendKeys.Send("{ENTER}");
                while (textBox3.Text != $"https://make.figure-eight.com/jobs?scope=8b47f4bd-1349-477b-a44a-778bf5eb3eaa&query={line.Trim()}&state=all&project=all&fields[]=tags&fields[]=title&fields[]=id&fields[]=owner_email&fields[]=alias")
                {
                    Sleep(1, "");
                }
                Sleep(1, "id");

                Application.DoEvents();
                if (!textBox3.Text.Contains($"query={line.Trim()}"))
                {
                    File.Delete("ids.txt");
                    File.WriteAllLines("ids.txt", newLIst);
                    Environment.Exit(1);
                }
                Application.DoEvents();
                LeftMouseClick(1849, 390);
                Sleep(1, "");
                LeftMouseClick(1762, 505);
                Sleep(1, "");
                LeftMouseClick(1186, 625);
                while (textBox3.Text != "https://make.figure-eight.com/jobs")
                {
                    Sleep(1, "");
                }
                Sleep(1, "finishing");

                newLIst = newLIst.Where(x => x.Trim() != line.Trim()).ToList();
                File.Delete("ids.txt");
                File.WriteAllLines("ids.txt", newLIst);

                var end = DateTime.Now;
                var timeDiff = (end - start).TotalSeconds.ToString("#.#");
                label4.Text = $"Time per job: {timeDiff}";
                Application.DoEvents();

                if (i == max)
                {
                    File.Delete("ids.txt");
                    File.WriteAllLines("ids.txt", newLIst);
                    break;
                }
                i++;
            }
        }
    }
}
