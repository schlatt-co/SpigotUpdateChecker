using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace SpigotUpdateChecker
{
    public partial class Form1 : Form
    {
        public static string url = "https://hub.spigotmc.org/versions/";
        public static string currentUrl = "https://hub.spigotmc.org/versions/latest.json";
        public static string currentVerison = "3094";
        public static string currentIndex = null;
        public static bool found = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            check();
            timer1.Interval = 5000;
            timer1.Start();
            this.FormClosing += Form1_FormClosing;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        void check()
        {
            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("User-Agent: Other");
                wc.DownloadStringAsync(new Uri(currentUrl));
                wc.DownloadStringCompleted += indexDownloadComplete;
            }
            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("User-Agent: Other");
                wc.DownloadStringAsync(new Uri(url));
                wc.DownloadStringCompleted += versionDownloadComplete;
            }
        }
        private void versionDownloadComplete(object sender, DownloadStringCompletedEventArgs e)
        {
            var text = e.Result;
            if (currentIndex == null)
            {
                currentIndex = text;
            }
            else
            {
                if (text != currentIndex)
                {
                    baka();
                }
                currentIndex = text;
            }
        }

        private void baka()
        {
            if (!found)
            {
                TestBeeps();
                playSimpleSound();
            }
            Notifications.ShowToast("NEW SPIGOT BUILD!!!");
            found = true;
        }

        private void indexDownloadComplete(object sender, DownloadStringCompletedEventArgs e)
        {
            var text = e.Result;
            var obj = JsonConvert.DeserializeObject<Dictionary<string, object>>(text);
            string name = obj["name"].ToString();
            versionLabel.Text = name;
            if (name != currentVerison)
            {
                baka();
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            check();
        }


        [DllImport("kernel32.dll")]
        public static extern bool Beep(int freq, int duration);
        public static void TestBeeps()
        {
            new Thread(() =>
            {
                while (true)
                {

                    Beep(3000, 200);
                    Beep(2000, 200);
                }
            }).Start();
        }
        private void playSimpleSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.spigotout);
            simpleSound.PlayLooping();
        }


        int i = 0;
        const string title = "sussy baka ";
        private void timer2_Tick(object sender, EventArgs e)
        {
            this.Text += title[i];
            i++;
            if (i == title.Length)
            {
                i = 0;
                this.Text = "";
            }
        }
    }
}
