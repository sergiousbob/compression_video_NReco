using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace compression_video
{

    public partial class Form1 : Form
    {
        int w_video;
        int h_video;
        string format_codec;
        string codec;

        public Form1()
        {
            InitializeComponent();
            resolBox.SelectedItem = "854×480 (480p)";
            formatBox.SelectedItem = "avi";
            codecBox.SelectedItem = "AVI";

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {


            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Choose path" })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                    webBrowser1.Url = new Uri(fbd.SelectedPath);
                txtPath.Text = fbd.SelectedPath;

            }
        }



        void asyncBtn()
        {
            if (resolBox.SelectedItem == "1280×720 (720p)")
            {
                w_video = 1280;
                h_video = 720;

            }

            if (resolBox.SelectedItem == "854×480 (480p)")
            {
                w_video = 854;
                h_video = 480;

            }
            if (resolBox.SelectedItem == "640×360 (360p)")
            {
                w_video = 640;
                h_video = 360;

            }
            if (resolBox.SelectedItem == "426×240 (240p)")
            {
                w_video = 426;
                h_video = 240;

            }


            if (codecBox.SelectedItem == "AVI")
            {
                format_codec = "avi";


            }

            if (codecBox.SelectedItem == "MP4")
            {
                format_codec = "mp4";


            }



            string directoryPath = $"{txtPath.Text}";

            string[] aviFiles = Directory.GetFiles(directoryPath, $"*.{formatBox.SelectedItem}");
            int a = 1;






            string kolvo = new DirectoryInfo(directoryPath).GetFiles($"*.{formatBox.SelectedItem}").Length.ToString();

            label8.Text = $"{kolvo}";


            btnCompress.Enabled = false;

            foreach (string aviFile in aviFiles)

            {

                label6.Text = $"{a}";
                var setting = new NReco.VideoConverter.ConvertSettings();

                setting.SetVideoFrameSize(w_video, h_video);

      
               setting.VideoCodec = "mpeg4";

                var converter = new NReco.VideoConverter.FFMpegConverter();


                converter.ConvertMedia(aviFile, $"{formatBox.SelectedItem}", $@"{txtOutput.Text}\VideoConvert{a}.{format_codec}", $"{format_codec}", setting);
    

                a++;

                int abc = 100/int.Parse(kolvo);
             

                for (int i = 1; i <= int.Parse(kolvo); i++)
                {
       
                    comprBar.Increment(abc);
           

                }

            }
            MessageBox.Show("All videos have been processed!!!");
            comprBar.Value = 0;
            btnCompress.Enabled = true;

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoBack)
                webBrowser1.GoBack();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoForward)
                webBrowser1.GoForward();
        
        }

        private async void btnCompress_Click(object sender, EventArgs e)
        {
            await Task.Run(() => asyncBtn());

            label6.Visible = true;
            label8.Visible = true;
            label7.Visible = true;
       
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void bntOutput_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Choose path" })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                    webBrowser2.Url = new Uri(fbd.SelectedPath);
                txtOutput.Text = fbd.SelectedPath;

            }
        }

        private void btnBackOut_Click(object sender, EventArgs e)
        {
            if (webBrowser2.CanGoBack)
                webBrowser2.GoBack();

        }

        private void btnForwarOut_Click(object sender, EventArgs e)
        {
            if (webBrowser2.CanGoForward)
                webBrowser2.GoForward();

        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure wanna be exit ?", "Exit Application", MessageBoxButtons.YesNoCancel);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
       ;
                Process[] p = Process.GetProcessesByName("ffmpeg");
                if (p.Length > 0) p[0].Kill();
                Process[] x = Process.GetProcessesByName("compression_video");
                if (x.Length > 0) x[0].Kill();
            }
        }
    }
}
