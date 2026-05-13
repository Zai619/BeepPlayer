using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.Windows.Forms;

namespace BeepPlayer
{
    public partial class frmBeepPlayer : Form
    {
        int initWidth = 0;
        int initHeight = 0;
        Dictionary<string, Rect> initControl = new Dictionary<string, Rect>();

        [DllImport("kernel32.dll")]
        public static extern bool Beep(int frequency, int duration);
        int[] freq = { 523, 587, 659, 698, 784, 880, 988, 1046 };
        int[] beeSong = { 4, 2, 2, 3, 1, 1, 0, 1, 2, 3, 4, 4, 4 };

        public frmBeepPlayer()
        {
            InitializeComponent();
            InitializeButton();
        }
        private void InitializeButton()
        {
            // 讓btn1~btn8共用同一個事件處理函式
            btn2.Click += btn1_Click;
            btn3.Click += btn1_Click;
            btn4.Click += btn1_Click;
            btn5.Click += btn1_Click;
            btn6.Click += btn1_Click;
            btn7.Click += btn1_Click;
            btn8.Click += btn1_Click;
        }
        private void btn1_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Enabled = false;
            Beep(freq[btn.TabIndex], 300);
            btn.Enabled = true;
        }

        private void frmBeepPlayer_Load(object sender, EventArgs e)
        {
            this.initWidth = this.palMain.Width;
            this.initHeight = this.palMain.Height;
            foreach (Control ctl in this.palMain.Controls)
            {
                this.initControl.Add(ctl.Name, new Rect(ctl.Left, ctl.Top,
                ctl.Width, ctl.Height));
            }
        }

        private void frmBeepPlayer_SizeChanged(object sender, EventArgs e)
        {
            //if (initControl == null || initControl.Count == 0 || this.WindowState == FormWindowState.Minimized)
            //{
              //  return;
            //}
            double width = this.palMain.Width;
            double height = this.palMain.Height;
            double iRatioWith = width / this.initWidth;
            double iRatioHeight = height / this.initHeight;
            foreach (Control ctl in this.palMain.Controls)
            {
                if (initControl.ContainsKey(ctl.Name))
                {

                    ctl.Left = (int)(initControl[ctl.Name].Left * iRatioWith);
                    ctl.Top = (int)(initControl[ctl.Name].Top * iRatioHeight);
                    ctl.Width = (int)(initControl[ctl.Name].Width * iRatioWith);
                    ctl.Height = (int)(initControl[ctl.Name].Height *
                    iRatioHeight);
                }
            }
        }

        private void frmBeepPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("確定要關閉應用程式嗎？", "關閉確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true; // 取消關閉
            }
        }

    

        private void frmBeepPlayer_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    btn1.PerformClick();
                    break;
                case Keys.S:
                    btn2.PerformClick();
                    break;
                case Keys.D: // 對應 Mi
                    btn3.PerformClick();
                    break;
                case Keys.F: // 對應 Fa
                    btn4.PerformClick();
                    break;
                case Keys.G: // 對應 Sol
                    btn5.PerformClick();
                    break;
                case Keys.H: // 對應 La
                    btn6.PerformClick();
                    break;
                case Keys.J: // 對應 Si
                    btn7.PerformClick();
                    break;
                case Keys.K: // 對應 高音 Do
                    btn8.PerformClick();
                    break;
            }
        }

        private async void btnPlayBee_Click(object sender, EventArgs e)
        {
            btnPlayBee.Enabled = false;

            foreach (int noteIndex in beeSong)
            {
                // 1. 發出聲音 (使用你現有的 Beep 函式)
                // 頻率來自你的 freq 陣列，持續時間設為 300 毫秒
                Beep(freq[noteIndex], 300);

                // 2. 每個音符之間的間隔 (等待 100 毫秒，聽起來比較自然)
                await Task.Delay(100);
            }

            btnPlayBee.Enabled = true;
        }
    }
}
