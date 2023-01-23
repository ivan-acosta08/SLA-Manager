using CustomAlertBoxDemo;
using CustomControls.RJControls;
using Microsoft.VisualBasic;
using SLA_Manager.Properties;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SLA_Manager
{
    public partial class Form_Main : System.Windows.Forms.Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRigthRect,
            int nBottomRect,
            int nWigthEllipse,
            int nHeightEllipse
        );

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                // turn on WS_EX_TOOLWINDOW style bit
                cp.ExStyle |= 0x80;
                return cp;
            }
        }

        bool iniciado = false;

        public Form_Main(string modoInicio)
        {
            Preferencias.SetConfig("MensajeMinimizar", "false");

            InitializeComponent();
            Verificar();
            actualizarIdioma();

            if (Preferencias.GetConfig().val_lang == "español")
            {
                cbox_p5_idioma.Text = "Español";
            }
            else
            {
                cbox_p5_idioma.Text = "English";
            }

            this.Show();

            if (modoInicio == "Minimizado")
            {
                this.WindowState = FormWindowState.Minimized;
            }

            iniciado = true;

            this.Size = new Size(735, 400);

            ctxt_icontry.ForeColor = Color.FromArgb(168, 172, 179);

            string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string fileName = tempPath + @"\SLA Manager\data\steam_data.txt";

            string[] lines = File.ReadAllLines(fileName);

            if (lines.Length == 0)
            {
                panel_Menu1.Visible = false;
                panel_Menu2.Visible = true;
                panel_Menu3.Visible = false;
                panel_Menu4.Visible = false;
                panel_Menu5.Visible = false;
                lbl_p1_main.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p2_main.BackColor = Color.FromArgb(0, 174, 217);
                lbl_p3_main.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p4_main.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p5_main.BackColor = Color.FromArgb(21, 22, 25);

                lbl_p1_ico.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p2_ico.BackColor = Color.FromArgb(0, 174, 217);
                lbl_p3_ico.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p4_ico.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p5_ico.BackColor = Color.FromArgb(21, 22, 25);

                lbl_p1_title.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p2_title.BackColor = Color.FromArgb(0, 174, 217);
                lbl_p3_title.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p4_title.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p5_title.BackColor = Color.FromArgb(21, 22, 25);
            }
            else
            {
                borrarCuentas();
                IrCuentas();
                panel_Menu1.Visible = true;
                panel_Menu2.Visible = false;
                panel_Menu3.Visible = false;
                panel_Menu4.Visible = false;
                panel_Menu5.Visible = false;
                lbl_p1_main.BackColor = Color.FromArgb(0, 174, 217);
                lbl_p2_main.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p3_main.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p4_main.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p5_main.BackColor = Color.FromArgb(21, 22, 25);

                lbl_p1_ico.BackColor = Color.FromArgb(0, 174, 217);
                lbl_p2_ico.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p3_ico.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p4_ico.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p5_ico.BackColor = Color.FromArgb(21, 22, 25);

                lbl_p1_title.BackColor = Color.FromArgb(0, 174, 217);
                lbl_p2_title.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p3_title.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p4_title.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p5_title.BackColor = Color.FromArgb(21, 22, 25);
            }
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btnClose.BackgroundImage = Properties.Resources.close_enter;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackgroundImage = Properties.Resources.close_leave;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void btnMinimize_MouseEnter(object sender, EventArgs e)
        {
            btnMinimize.BackgroundImage = Properties.Resources.minimize_enter;
        }

        private void btnMinimize_MouseLeave(object sender, EventArgs e)
        {
            btnMinimize.BackgroundImage = Properties.Resources.minimize_leave;
        }


        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            Preferencias.SetConfig("MensajeMinimizar", "true");
        }

        private void Form_Main_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                if (Preferencias.GetConfig().val_IconoSeguimiento == true)
                {
                    this.Hide();
                    Notificacion.Visible = true;
                    
                    if (Preferencias.GetConfig().val_lang == "español")
                    {
                        Notificacion.BalloonTipText = "El programa se minimizó en la bandeja";
                    }
                    else
                    {
                        Notificacion.BalloonTipText = "This program has been minimized to the tray";
                    }

                    if (Preferencias.GetConfig().val_mensajeMinimizar == false && iniciado)
                    {
                        Notificacion.ShowBalloonTip(500);
                    }
                }
                else
                {
                    Notificacion.Visible = false;
                    this.ShowInTaskbar = true;
                }
            }
        }

        private void Forma_Paint(object sender, PaintEventArgs e)
        {
            Graphics mgraphics = e.Graphics;
            Pen pen = new Pen(Color.FromArgb(33, 35, 40), 1);
            Rectangle area = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush lgb = new LinearGradientBrush(area, Color.FromArgb(33, 35, 40), Color.FromArgb(25, 26, 30), LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(lgb, area);
            e.Graphics.DrawRectangle(pen, area);
        }

        public int xClick = 0, yClick = 0;

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            { xClick = e.X; yClick = e.Y; }
            else
            { this.Left = this.Left + (e.X - xClick); this.Top = this.Top + (e.Y - yClick); }
        }

        private void p_main_enter(object sender, EventArgs e)
        {
            Label lbl1 = (Label) sender;
            if (lbl1 != null)
            {
                if (lbl1.Name.Contains("1") && lbl_p1_title.BackColor != Color.FromArgb(0, 174, 217))
                {
                    lbl_p1_title.BackColor = Color.FromArgb(42, 42, 42);
                    lbl_p1_ico.BackColor = Color.FromArgb(42, 42, 42);
                    lbl_p1_main.BackColor = Color.FromArgb(42, 42, 42);
                } else if (lbl1.Name.Contains("2") && lbl_p2_title.BackColor != Color.FromArgb(0, 174, 217))
                {
                    lbl_p2_title.BackColor = Color.FromArgb(42, 42, 42);
                    lbl_p2_ico.BackColor = Color.FromArgb(42, 42, 42);
                    lbl_p2_main.BackColor = Color.FromArgb(42, 42, 42);
                } else if(lbl1.Name.Contains("3") && lbl_p3_title.BackColor != Color.FromArgb(0, 174, 217))
                {
                    lbl_p3_title.BackColor = Color.FromArgb(42, 42, 42);
                    lbl_p3_ico.BackColor = Color.FromArgb(42, 42, 42);
                    lbl_p3_main.BackColor = Color.FromArgb(42, 42, 42);
                } else if(lbl1.Name.Contains("4") && lbl_p4_title.BackColor != Color.FromArgb(0, 174, 217))
                {
                    lbl_p4_title.BackColor = Color.FromArgb(42, 42, 42);
                    lbl_p4_ico.BackColor = Color.FromArgb(42, 42, 42);
                    lbl_p4_main.BackColor = Color.FromArgb(42, 42, 42);
                } else if(lbl1.Name.Contains("5") && lbl_p5_title.BackColor != Color.FromArgb(0, 174, 217))
                {
                    lbl_p5_title.BackColor = Color.FromArgb(42, 42, 42);
                    lbl_p5_ico.BackColor = Color.FromArgb(42, 42, 42);
                    lbl_p5_main.BackColor = Color.FromArgb(42, 42, 42);
                } 
            }
        }

        private void p_main_leave(object sender, EventArgs e)
        {
            Label lbl1 = (Label)sender;
            if (lbl1 != null)
            {
                if (lbl1.Name.Contains("1") && lbl_p1_title.BackColor != Color.FromArgb(0, 174, 217))
                {
                    lbl_p1_title.BackColor = Color.FromArgb(21, 22, 25);
                    lbl_p1_ico.BackColor = Color.FromArgb(21, 22, 25);
                    lbl_p1_main.BackColor = Color.FromArgb(21, 22, 25);
                }
                else if (lbl1.Name.Contains("2") && lbl_p2_title.BackColor != Color.FromArgb(0, 174, 217))
                {
                    lbl_p2_title.BackColor = Color.FromArgb(21, 22, 25);
                    lbl_p2_ico.BackColor = Color.FromArgb(21, 22, 25);
                    lbl_p2_main.BackColor = Color.FromArgb(21, 22, 25);
                }
                else if (lbl1.Name.Contains("3") && lbl_p3_title.BackColor != Color.FromArgb(0, 174, 217))
                {
                    lbl_p3_title.BackColor = Color.FromArgb(21, 22, 25);
                    lbl_p3_ico.BackColor = Color.FromArgb(21, 22, 25);
                    lbl_p3_main.BackColor = Color.FromArgb(21, 22, 25);
                }
                else if (lbl1.Name.Contains("4") && lbl_p4_title.BackColor != Color.FromArgb(0, 174, 217))
                {
                    lbl_p4_title.BackColor = Color.FromArgb(21, 22, 25);
                    lbl_p4_ico.BackColor = Color.FromArgb(21, 22, 25);
                    lbl_p4_main.BackColor = Color.FromArgb(21, 22, 25);
                }
                else if (lbl1.Name.Contains("5") && lbl_p5_title.BackColor != Color.FromArgb(0, 174, 217))
                {
                    lbl_p5_title.BackColor = Color.FromArgb(21, 22, 25);
                    lbl_p5_ico.BackColor = Color.FromArgb(21, 22, 25);
                    lbl_p5_main.BackColor = Color.FromArgb(21, 22, 25);
                }
            }
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.FillRoundedRectangle(new SolidBrush(Color.FromArgb(50, 53, 60)), 10, 10, this.Width - 40, this.Height - 60, 10);
            SolidBrush brush = new SolidBrush(Color.FromArgb(50, 53, 60));
            g.FillRoundedRectangle(brush, 12, 12, this.Width - 44, this.Height - 64, 10);
            g.DrawRoundedRectangle(new Pen(ControlPaint.Light(Color.FromArgb(50, 53, 60), 0.00f)), 12, 12, this.Width - 44, this.Height - 64, 10);
            g.FillRoundedRectangle(new SolidBrush(Color.FromArgb(50, 53, 60)), 12, 12 + ((this.Height - 64) / 2), this.Width - 44, (this.Height - 64) / 2, 10);
        }


        private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Notificacion.Visible = false;

            string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Verificar();
            string fileName = tempPath + @"\SLA Manager\data\steam_userID64.txt";

            File.WriteAllText(fileName, "");
        }

        List<string> filas = new List<string>();

        private void Notificacion_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ctxt_icontry.Show();
                ctxt_icontry.Items.Clear();
                filas.Clear();
                
                filas.Add("SLA Manager");
                filas.Add("-");
                filas.Add("Steam");
                filas.Add("WinAuth");
                filas.Add("-");

                string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string fileName = tempPath + @"\SLA Manager\data\steam_data.txt";

                string[] lines = File.ReadAllLines(fileName);

                if (lines.Length > 0)
                {
                    string cuenta = lines[0];
                    string[] cuentaArray = cuenta.Split(',');

                    for (int i = 0; i < lines.Length; i++)
                    {
                        string cuenta2 = lines[i];
                        string[] cuentaArray2 = cuenta2.Split(',');

                        filas.Add(cuentaArray2[0]);
                    }
                    
                    filas.Add("-");
                }

                if (Preferencias.GetConfig().val_lang == "español")
                {
                    filas.Add("Salir");
                }
                else
                {
                    filas.Add("Exit");
                }

                for (int z = 0; z < filas.Count; z++)
                {
                    if (filas[z] == "SLA Manager")
                    {
                        ToolStripMenuItem tm = new ToolStripMenuItem();
                        tm.Text = filas[z];
                        tm.ForeColor = Color.FromArgb(0, 174, 217);
                        ctxt_icontry.Items.Add(tm);
                    } else if (filas[z] == "Salir" || filas[z] == "Exit")
                    {
                        ToolStripMenuItem tm = new ToolStripMenuItem();
                        tm.Text = filas[z];
                        tm.ForeColor = Color.White;
                        ctxt_icontry.Items.Add(tm);
                    }
                    else if (filas[z] == "-")
                    {
                        ctxt_icontry.Items.Add(new ToolStripSeparator());
                    } else 
                    {
                        ToolStripMenuItem tm = new ToolStripMenuItem();
                        tm.Text = filas[z];
                        tm.ForeColor = Color.White;
                        tm.Image = Resources.ayuda;
                        ctxt_icontry.Items.Add(tm); 
                    }
                }

                ctxt_icontry.ItemClicked += new ToolStripItemClickedEventHandler(MenuNotificacion_ItemClicked);
            }
            else if (e.Button == MouseButtons.Left)
            {
                Verificar();
                borrarCuentas();
                IrCuentas();
                pic_p0_steam.Visible = true;
                pic_p0_winauth.Visible = true;
                panel_Menu1.Visible = true;
                panel_Menu2.Visible = false;
                panel_Menu3.Visible = false;
                panel_Menu4.Visible = false;
                panel_Menu5.Visible = false;
                lbl_p1_main.BackColor = Color.FromArgb(0, 174, 217);
                lbl_p2_main.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p3_main.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p4_main.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p5_main.BackColor = Color.FromArgb(21, 22, 25);

                lbl_p1_ico.BackColor = Color.FromArgb(0, 174, 217);
                lbl_p2_ico.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p3_ico.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p4_ico.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p5_ico.BackColor = Color.FromArgb(21, 22, 25);

                lbl_p1_title.BackColor = Color.FromArgb(0, 174, 217);
                lbl_p2_title.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p3_title.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p4_title.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p5_title.BackColor = Color.FromArgb(21, 22, 25);

                Show();
                WindowState = FormWindowState.Normal;
                ShowInTaskbar = true;
                Notificacion.Visible = false;
            }
        }

        private void MenuNotificacion_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;

            if (item.Text == "SLA Manager")
            {
                Verificar();
                borrarCuentas();
                IrCuentas();
                pic_p0_steam.Visible = true;
                pic_p0_winauth.Visible = true;
                panel_Menu1.Visible = true;
                panel_Menu2.Visible = false;
                panel_Menu3.Visible = false;
                panel_Menu4.Visible = false;
                panel_Menu5.Visible = false;
                lbl_p1_main.BackColor = Color.FromArgb(0, 174, 217);
                lbl_p2_main.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p3_main.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p4_main.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p5_main.BackColor = Color.FromArgb(21, 22, 25);

                lbl_p1_ico.BackColor = Color.FromArgb(0, 174, 217);
                lbl_p2_ico.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p3_ico.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p4_ico.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p5_ico.BackColor = Color.FromArgb(21, 22, 25);

                lbl_p1_title.BackColor = Color.FromArgb(0, 174, 217);
                lbl_p2_title.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p3_title.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p4_title.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p5_title.BackColor = Color.FromArgb(21, 22, 25);

                this.Size = new Size(735, 400);
                Show();
                this.FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Normal;
                ShowInTaskbar = true;
                Notificacion.Visible = false;
            }
            else if (item.Text == "Steam")
            {
                try
                {
                    Process.Start(Preferencias.GetConfig().val_SteamPath);
                }
                catch
                {
                    if (Preferencias.GetConfig().val_lang == "español")
                    {
                        this.Alert("Ingrese un directorio\nde Steam correctamente", Form_Alert.enmType.Folder);
                    }
                    else
                    {
                        this.Alert("Put an correct\nSteam directory", Form_Alert.enmType.Folder);
                    }
                }
            }
            else if (item.Text == "WinAuth")
            {
                try
                {
                    Process.Start(Preferencias.GetConfig().val_WinAuthPath);
                }
                catch
                {
                    if (Preferencias.GetConfig().val_lang == "español")
                    {
                        this.Alert("Ingrese un directorio\nde WinAuth correctamente", Form_Alert.enmType.Folder);
                    }
                    else
                    {
                        this.Alert("Put an correct\nWinAuth directory", Form_Alert.enmType.Folder);
                    }
                }
            }
            else if (item.Text == "Salir" || item.Text == "Exit")
            {
                Notificacion.Visible = false;
                Process.GetCurrentProcess().Kill();
            }
            else
            {
                Verificar();

                if (Preferencias.GetConfig().val_SteamPath != "")
                {
                    string SteamPath = Preferencias.GetConfig().val_SteamPath;

                    try
                    {
                        Process[] _proceses = null;
                        _proceses = Process.GetProcessesByName("steam");
                        foreach (Process proces in _proceses)
                        {
                            proces.Kill();
                        }
                    }
                    catch
                    {
                        Debug.WriteLine("No se pudo cerrar Steam");
                    }

                    try
                    {
                        string usuario = "", contra = "";
                        Thread.Sleep(100);
                        string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                        string fileData = tempPath + @"\SLA Manager\data\steam_data.txt";

                        string[] linesData = File.ReadAllLines(fileData); //data


                        for (int i = 1; i < linesData.Length; i++)
                        {
                            if (linesData[i].Contains(item.ToString()))
                            {
                                usuario = Seguridad.DesEncriptar(linesData[i].Split(",")[1]);
                                contra = Seguridad.DesEncriptar(linesData[i].Split(",")[2]);
                            }
                        }

                        string quote = "\"";
                        string PathInicio = quote + SteamPath + quote;
                        string PathParamentros = " -login " + usuario + " " + contra;
                        Process.Start(PathInicio, PathParamentros);
                    }
                    catch
                    {
                        Debug.WriteLine("No se pudo abrir Steam");
                    }
                }
                else
                {
                    if (Preferencias.GetConfig().val_lang == "español")
                    {
                        this.Alert("Ingrese un directorio\nde Steam correctamente", Form_Alert.enmType.Folder);
                    }
                    else
                    {
                        this.Alert("Put an correct\nSteam directory", Form_Alert.enmType.Folder);
                    }
                }

            }

            ctxt_icontry.Items.Add(new ToolStripSeparator());
        }

        private void pic_p0_steam_Click(object sender, EventArgs e)
        {

            if (Preferencias.GetConfig().val_SteamPath != "")
            {
                try
                {
                    if (Preferencias.GetConfig().val_MinimizarProgramas == true)
                    {
                        this.WindowState = FormWindowState.Minimized;
                    }

                    Process.Start(Preferencias.GetConfig().val_SteamPath);
                }
                catch
                {
                    Debug.Print("Error al abrir Steam.exe");
                }
            }
            else
            {
                if (Preferencias.GetConfig().val_lang == "español")
                {
                    this.Alert("Ingrese un directorio\nde Steam correctamente", Form_Alert.enmType.Folder);
                }
                else
                {
                    this.Alert("Put an correct\nSteam directory", Form_Alert.enmType.Folder);
                }
            }
        }

        private void pic_p0_winauth_Click(object sender, EventArgs e)
        {
            if (Preferencias.GetConfig().val_WinAuthPath != "")
            {
                try
                {
                    if (Preferencias.GetConfig().val_MinimizarProgramas == true)
                    {
                        this.WindowState = FormWindowState.Minimized;
                    }

                    Process.Start(Preferencias.GetConfig().val_WinAuthPath);
                }
                catch
                {
                    Debug.Print("Error al abrir WinAuth.exe");
                }
            }
            else
            {
                if (Preferencias.GetConfig().val_lang == "español")
                {
                    this.Alert("Ingrese un directorio\nde WinAuth correctamente", Form_Alert.enmType.Folder);
                }
                else
                {
                    this.Alert("Put an correct\nWinAuth directory", Form_Alert.enmType.Folder);
                }
            }
        }

        private void pic_p0_steam_MouseEnter(object sender, EventArgs e)
        {
            pic_p0_steam.BackgroundImage = Properties.Resources.steam_logo3;
        }

        private void pic_p0_steam_MouseLeave(object sender, EventArgs e)
        {
            pic_p0_steam.BackgroundImage = Properties.Resources.steam_logo2;
        }

        private void pic_p0_winauth_MouseEnter(object sender, EventArgs e)
        {
            pic_p0_winauth.BackgroundImage = Properties.Resources.winauth_logo3;
        }

        private void pic_p0_winauth_MouseLeave(object sender, EventArgs e)
        {
            pic_p0_winauth.BackgroundImage = Properties.Resources.winauth_logo2;
        }

        public void actualizarIdioma()
        {
            idioma.controles(this, panel_Menu2, panel_Menu3, panel_Menu4, panel_Menu5);
        }

        public void lbl_toolTip_MouseEnter(object sender, EventArgs e)
        {
            if (sender.GetType().ToString().Contains("Label"))
            {
                Label lbl = (Label)sender;
                if (lbl != null)
                {
                    if (Preferencias.GetConfig().val_lang == "español")
                    {
                        string esp = Properties.Resources.español_tooltip;
                        IEnumerable<string> lines_esp = esp.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        foreach (string linea in lines_esp)
                        {
                            if (linea.Contains(lbl.Name))
                            {
                                if (linea.Contains("|"))
                                {
                                    toolTip.ToolTipTitle = linea.Split("=")[1].Split("+")[0];
                                    toolTip.SetToolTip(lbl, linea.Split("+")[1].Replace("|", Environment.NewLine));
                                }
                                else
                                {
                                    toolTip.ToolTipTitle = linea.Split("=")[1].Split("+")[0];
                                    toolTip.SetToolTip(lbl, linea.Split("+")[1]);
                                }
                            }
                        }
                    }
                    else
                    {
                        string eng = Properties.Resources.english_tooltip;
                        IEnumerable<string> lines_eng = eng.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        foreach (string linea in lines_eng)
                        {
                            if (linea.Contains(lbl.Name))
                            {
                                toolTip.ToolTipTitle = linea.Split("=")[1].Split("+")[0];
                                toolTip.SetToolTip(lbl, linea.Split("+")[1]);
                            }
                        }
                    }

                }
            } else if (sender.GetType().ToString().Contains("CheckBox"))
            {
                CheckBox chb = (CheckBox)sender;
                if (chb != null)
                {
                    if (Preferencias.GetConfig().val_lang == "español")
                    {
                        string esp = Properties.Resources.español_tooltip;
                        IEnumerable<string> lines_esp = esp.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        foreach (string linea in lines_esp)
                        {
                            if (linea.Contains(chb.Name + "2"))
                            {
                                if (linea.Contains("|"))
                                {
                                    toolTip.ToolTipTitle = linea.Split("=")[1].Split("+")[0];
                                    toolTip.SetToolTip(chb, linea.Split("+")[1].Replace("|", Environment.NewLine));
                                }
                                else
                                {
                                    toolTip.ToolTipTitle = linea.Split("=")[1].Split("+")[0];
                                    toolTip.SetToolTip(chb, linea.Split("+")[1]);
                                }
                            }
                        }
                    }
                    else
                    {
                        string eng = Properties.Resources.english_tooltip;
                        IEnumerable<string> lines_eng = eng.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        foreach (string linea in lines_eng)
                        {
                            if (linea.Contains(chb.Name + "2"))
                            {
                                toolTip.ToolTipTitle = linea.Split("=")[1].Split("+")[0];
                                toolTip.SetToolTip(chb, linea.Split("+")[1]);
                            }
                        }
                    }

                }
            }
        }

        public void Verificar()
        {
            //No abrir mas de una ventana
            string currPrsName = Process.GetCurrentProcess().ProcessName;
            Process[] allProcessWithThisName = Process.GetProcessesByName(currPrsName);

            try
            {
                if (allProcessWithThisName.Length > 1)
                {
                    Process.GetCurrentProcess().Kill();
                }
            }
            catch
            {
                Debug.WriteLine("Error en allProcessWithThisName > 1");
            }
            //No abrir mas de una ventana

            //Verificar carpetas en %appdata%
            string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            bool pathMain = Directory.Exists(tempPath + @"\SLA Manager");
            bool pathImgMain = Directory.Exists(tempPath + @"\SLA Manager\imgs");
            bool pathDatos = Directory.Exists(tempPath + @"\SLA Manager\data");

            if (!pathMain)
                Directory.CreateDirectory(tempPath + @"\SLA Manager");
            if (!pathImgMain)
                Directory.CreateDirectory(tempPath + @"\SLA Manager\imgs");
            if (!pathDatos)
                Directory.CreateDirectory(tempPath + @"\SLA Manager\data");

            if (!File.Exists(tempPath + @"\SLA Manager\data\steam_userID64.txt"))
                File.Create(tempPath + @"\SLA Manager\data\steam_userID64.txt").Dispose();

            if (!File.Exists(tempPath + @"\SLA Manager\data\steam_data.txt"))
                File.Create(tempPath + @"\SLA Manager\data\steam_data.txt").Dispose();

            if (!File.Exists(tempPath + @"\SLA Manager\data\data_delete.txt"))
                File.Create(tempPath + @"\SLA Manager\data\data_delete.txt").Dispose();

            if (!File.Exists(tempPath + @"\SLA Manager\data\settings.txt"))
            {
                File.Create(tempPath + @"\SLA Manager\data\settings.txt").Dispose();
                string settingsPath = tempPath + @"\SLA Manager\data\settings.txt";

                File.AppendAllText(settingsPath, "lang,español" + Environment.NewLine);
                File.AppendAllText(settingsPath, "IniciarWindows,true" + Environment.NewLine);
                File.AppendAllText(settingsPath, "IconoSeguimiento,false" + Environment.NewLine);
                File.AppendAllText(settingsPath, "MinimizarInicio,false" + Environment.NewLine);
                File.AppendAllText(settingsPath, "MinimizarProgramas,false" + Environment.NewLine);
                File.AppendAllText(settingsPath, "PreguntarInicio,true" + Environment.NewLine);
                File.AppendAllText(settingsPath, "WinAuthPath," + Environment.NewLine);
                File.AppendAllText(settingsPath, "SteamPath," + Environment.NewLine);
                File.AppendAllText(settingsPath, "Contraseña," + Environment.NewLine);
                File.AppendAllText(settingsPath, "OcultarUser,false" + Environment.NewLine);
                File.AppendAllText(settingsPath, "MensajeMinimizar,false" + Environment.NewLine);
            }

            Preferencias.GetConfig();
            //Verificar carpetas en %appdata%
        }
    }

    public static class Seguridad
    {
        public static string Encriptar(string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        public static string DesEncriptar(string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
            result = Encoding.Unicode.GetString(decryted);
            return result;
        }
    }

    static class GraphicsExtension
    {
        private static GraphicsPath GenerateRoundedRectangle(
            this Graphics graphics,
            RectangleF rectangle,
            float radius)
        {
            float diameter;
            GraphicsPath path = new GraphicsPath();
            if (radius <= 0.0F)
            {
                path.AddRectangle(rectangle);
                path.CloseFigure();
                return path;
            }
            else
            {
                if (radius >= (Math.Min(rectangle.Width, rectangle.Height)) / 2.0)
                    return graphics.GenerateCapsule(rectangle);
                diameter = radius * 2.0F;
                SizeF sizeF = new SizeF(diameter, diameter);
                RectangleF arc = new RectangleF(rectangle.Location, sizeF);
                path.AddArc(arc, 180, 90);
                arc.X = rectangle.Right - diameter;
                path.AddArc(arc, 270, 90);
                arc.Y = rectangle.Bottom - diameter;
                path.AddArc(arc, 0, 90);
                arc.X = rectangle.Left;
                path.AddArc(arc, 90, 90);
                path.CloseFigure();
            }
            return path;
        }
        private static GraphicsPath GenerateCapsule(
            this Graphics graphics,
            RectangleF baseRect)
        {
            float diameter;
            RectangleF arc;
            GraphicsPath path = new GraphicsPath();
            try
            {
                if (baseRect.Width > baseRect.Height)
                {
                    diameter = baseRect.Height;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 90, 180);
                    arc.X = baseRect.Right - diameter;
                    path.AddArc(arc, 270, 180);
                }
                else if (baseRect.Width < baseRect.Height)
                {
                    diameter = baseRect.Width;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 180, 180);
                    arc.Y = baseRect.Bottom - diameter;
                    path.AddArc(arc, 0, 180);
                }
                else path.AddEllipse(baseRect);
            }
            catch { path.AddEllipse(baseRect); }
            finally { path.CloseFigure(); }
            return path;
        }

        /// <summary>
        /// Draws a rounded rectangle specified by a pair of coordinates, a width, a height and the radius 
        /// for the arcs that make the rounded edges.
        /// </summary>
        /// <param name="brush">System.Drawing.Pen that determines the color, width and style of the rectangle.</param>
        /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to draw.</param>
        /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to draw.</param>
        /// <param name="width">Width of the rectangle to draw.</param>
        /// <param name="height">Height of the rectangle to draw.</param>
        /// <param name="radius">The radius of the arc used for the rounded edges.</param>

        public static void DrawRoundedRectangle(
            this Graphics graphics,
            Pen pen,
            float x,
            float y,
            float width,
            float height,
            float radius)
        {
            RectangleF rectangle = new RectangleF(x, y, width, height);
            GraphicsPath path = graphics.GenerateRoundedRectangle(rectangle, radius);
            SmoothingMode old = graphics.SmoothingMode;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawPath(pen, path);
            graphics.SmoothingMode = old;
        }

        /// <summary>
        /// Draws a rounded rectangle specified by a pair of coordinates, a width, a height and the radius 
        /// for the arcs that make the rounded edges.
        /// </summary>
        /// <param name="brush">System.Drawing.Pen that determines the color, width and style of the rectangle.</param>
        /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to draw.</param>
        /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to draw.</param>
        /// <param name="width">Width of the rectangle to draw.</param>
        /// <param name="height">Height of the rectangle to draw.</param>
        /// <param name="radius">The radius of the arc used for the rounded edges.</param>

        public static void DrawRoundedRectangle(
            this Graphics graphics,
            Pen pen,
            int x,
            int y,
            int width,
            int height,
            int radius)
        {
            graphics.DrawRoundedRectangle(
                pen,
                Convert.ToSingle(x),
                Convert.ToSingle(y),
                Convert.ToSingle(width),
                Convert.ToSingle(height),
                Convert.ToSingle(radius));
        }

        /// <summary>
        /// Fills the interior of a rounded rectangle specified by a pair of coordinates, a width, a height
        /// and the radius for the arcs that make the rounded edges.
        /// </summary>
        /// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
        /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to fill.</param>
        /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to fill.</param>
        /// <param name="width">Width of the rectangle to fill.</param>
        /// <param name="height">Height of the rectangle to fill.</param>
        /// <param name="radius">The radius of the arc used for the rounded edges.</param>

        public static void FillRoundedRectangle(
            this Graphics graphics,
            Brush brush,
            float x,
            float y,
            float width,
            float height,
            float radius)
        {
            RectangleF rectangle = new RectangleF(x, y, width, height);
            GraphicsPath path = graphics.GenerateRoundedRectangle(rectangle, radius);
            SmoothingMode old = graphics.SmoothingMode;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.FillPath(brush, path);
            graphics.SmoothingMode = old;
        }

        /// <summary>
        /// Fills the interior of a rounded rectangle specified by a pair of coordinates, a width, a height
        /// and the radius for the arcs that make the rounded edges.
        /// </summary>
        /// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
        /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to fill.</param>
        /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to fill.</param>
        /// <param name="width">Width of the rectangle to fill.</param>
        /// <param name="height">Height of the rectangle to fill.</param>
        /// <param name="radius">The radius of the arc used for the rounded edges.</param>

        public static void FillRoundedRectangle(
            this Graphics graphics,
            Brush brush,
            int x,
            int y,
            int width,
            int height,
            int radius)
        {
            graphics.FillRoundedRectangle(
                brush,
                Convert.ToSingle(x),
                Convert.ToSingle(y),
                Convert.ToSingle(width),
                Convert.ToSingle(height),
                Convert.ToSingle(radius));
        }
    }
}