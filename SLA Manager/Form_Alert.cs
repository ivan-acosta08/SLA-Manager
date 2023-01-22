using SLA_Manager;
using SLA_Manager.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomAlertBoxDemo
{
    public partial class Form_Alert : Form
    {
        public Form_Alert()
        {
            InitializeComponent();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x80;
                return cp;
            }
        }

        public enum enmAction
        {
            wait,
            start,
            close
        }

        public enum enmType
        {
            Success,
            Warning,
            Error,
            Info,
            SteamID,
            Folder,
        }
        private Form_Alert.enmAction action;

        private int x, y;

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch(this.action)
            {
                case enmAction.wait:
                    timer1.Interval = 5000;
                    action = enmAction.close;
                    break;
                case Form_Alert.enmAction.start:
                    this.timer1.Interval = 1;
                    this.Opacity += 0.1;
                    if (this.x < this.Location.X)
                    {
                        this.Left--;
                    }
                    else
                    {
                        if (this.Opacity == 1.0)
                        {
                            action = Form_Alert.enmAction.wait;
                        }
                    }
                    break;
                case enmAction.close:
                    timer1.Interval = 1;
                    this.Opacity -= 0.1;

                    this.Left += 3;
                    if (base.Opacity == 0.0)
                    {
                        base.Close();
                    }
                    break;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            timer1.Interval = 1;
            action = enmAction.close;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            var uri = "https://steamid.io/";
            var psi = new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = uri
            };
            Process.Start(psi);

            timer1.Interval = 1;
            action = enmAction.close;
        }


        public void showAlert(string msg, enmType type)
        {
            this.Opacity = 0.0;
            this.StartPosition = FormStartPosition.Manual;
            string fname;

            for (int i = 1; i < 6; i++)
            {
                fname = "alert" + i.ToString();
                Form_Alert frm = (Form_Alert)Application.OpenForms[fname];

                if (frm == null && i <= 5)
                {
                    this.Name = fname;
                    this.x = Screen.PrimaryScreen.WorkingArea.Width - this.Width + 15;
                    this.y = Screen.PrimaryScreen.WorkingArea.Height - this.Height * i - 5 * i;
                    this.Location = new Point(this.x, this.y);

                    switch (type)
                    {
                        case enmType.Success:
                            this.pictureBox1.Visible = true;
                            this.pictureBox2.Visible = true;
                            this.pictureBox3.Visible = false;
                            this.pictureBox4.Visible = false;
                            this.lblMsg.Location = new Point(65, 16);
                            this.lblMsg.Text = msg;
                            this.pictureBox1.Image = Resources.success;
                            this.BackColor = Color.SeaGreen;
                            break;
                        case enmType.Error:
                            this.pictureBox1.Visible = true;
                            this.pictureBox2.Visible = true;
                            this.pictureBox3.Visible = false;
                            this.pictureBox4.Visible = false;
                            this.lblMsg.Location = new Point(65, 16);
                            this.lblMsg.Text = msg;
                            this.pictureBox1.Image = Resources.cloud_cross_48px;
                            this.BackColor = Color.DarkRed;
                            break;
                        case enmType.Info:
                            this.pictureBox1.Visible = true;
                            this.pictureBox2.Visible = true;
                            this.pictureBox3.Visible = false;
                            this.pictureBox4.Visible = false;
                            this.lblMsg.Location = new Point(65, 16);
                            this.Size = new Size(347, 74);
                            this.lblMsg.Text = msg;
                            this.pictureBox1.Image = Resources.info;
                            this.BackColor = Color.RoyalBlue;
                            break;
                        case enmType.Warning:
                            this.pictureBox1.Visible = true;
                            this.pictureBox2.Visible = true;
                            this.pictureBox3.Visible = false;
                            this.pictureBox4.Visible = false;
                            this.lblMsg.Location = new Point(65, 16);
                            this.lblMsg.Text = msg;
                            this.pictureBox1.Image = Resources.warning;
                            this.BackColor = Color.DarkOrange;
                            break;
                        case enmType.SteamID:
                            this.pictureBox1.Visible = false;
                            this.pictureBox2.Visible = false;
                            this.pictureBox3.Visible = true;
                            this.pictureBox4.Visible = true;
                            if (Preferencias.GetConfig().val_lang == "español")
                            {
                                this.lblMsg.Text = "¿Buscar tu STEAM ID 64?";
                            } else
                            {
                                this.lblMsg.Text = "¿Search your STEAM ID 64?";
                            }
                            this.BackColor = Color.FromArgb(0, 120, 215);
                            break;
                        case enmType.Folder:
                            this.pictureBox1.Visible = true;
                            this.pictureBox2.Visible = true;
                            this.pictureBox3.Visible = false;
                            this.pictureBox4.Visible = false;
                            this.lblMsg.Location = new Point(65, 16);
                            this.lblMsg.Text = msg;
                            this.pictureBox1.Image = Resources.question_folder;
                            this.BackColor = Color.DarkRed;
                            break;
                    }

                    this.Show();
                    break;
                }

            }

            this.x = Screen.PrimaryScreen.WorkingArea.Width - base.Width - 5;

            this.action = enmAction.start;
            this.timer1.Interval = 1;
            this.timer1.Start();
        }
    }

}
