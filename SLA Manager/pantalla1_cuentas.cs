
using CustomAlertBoxDemo;
using CustomControls.RJControls;
using SLA_Manager.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SLA_Manager
{
    public partial class Form_Main
    {
        public void IrCuentas()
        {
            try
            {
                Verificar();

                string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string fileName = tempPath + @"\SLA Manager\data\steam_data.txt";

                bool oculto = Preferencias.GetConfig().val_OcultarUsuario;

                string[] lines = File.ReadAllLines(fileName);

                panel_Menu1.HorizontalScroll.Enabled = false;
                panel_Menu1.AutoScroll = true;

                if (lines.Length == 0)
                {
                    Label lbl_p1_no_accounts = new Label();
                    lbl_p1_no_accounts.Left = 0;
                    lbl_p1_no_accounts.Top = 195;
                    lbl_p1_no_accounts.ForeColor = Color.White;
                    lbl_p1_no_accounts.AutoSize = false;
                    lbl_p1_no_accounts.Size = new Size(490, 60);
                    lbl_p1_no_accounts.Font = new Font("Segoe UI", 14, FontStyle.Regular);
                    lbl_p1_no_accounts.Text = "No existen\r\ncuentas registradas";
                    lbl_p1_no_accounts.TextAlign = ContentAlignment.MiddleCenter;
                    lbl_p1_no_accounts.MouseMove += new MouseEventHandler(this.Form_MouseMove);
                    panel_Menu1.Controls.Add(lbl_p1_no_accounts);

                    PictureBox pic_p1_no_accounts = new PictureBox();
                    pic_p1_no_accounts.Left = 195;
                    pic_p1_no_accounts.Top = 86;
                    pic_p1_no_accounts.Image = Resources.no_accounts;
                    pic_p1_no_accounts.SizeMode = PictureBoxSizeMode.Zoom;
                    pic_p1_no_accounts.Size = new Size(100, 100);
                    pic_p1_no_accounts.MouseMove += new MouseEventHandler(this.Form_MouseMove);
                    panel_Menu1.Controls.Add(pic_p1_no_accounts);
                }
                else if (lines.Length == 1)
                {
                    string cuenta = lines[0];
                    string[] cuentaArray = cuenta.Split(',');

                    string strName = cuentaArray[0];
                    string strUsuario = Seguridad.DesEncriptar(cuentaArray[1]);
                    string strContra = Seguridad.DesEncriptar(cuentaArray[2]);
                    string Esimagen = cuentaArray[3];

                    Button b = new Button();
                    b.Location = new Point(379, 142);
                    b.Tag = "dynamic";
                    b.Name = "button_1";
                    b.Text = "";
                    b.FlatStyle = FlatStyle.Flat;
                    b.BackColor = Color.Transparent;
                    b.FlatAppearance.MouseDownBackColor = Color.Transparent;
                    b.FlatAppearance.MouseOverBackColor = Color.Transparent;
                    b.FlatAppearance.BorderSize = 0;
                    b.Size = new Size(50, 50);
                    b.Padding = new Padding(0);
                    b.Image = new Bitmap(Resources.play_button_solo);
                    b.MouseEnter += new EventHandler(this.playButton_Enter);
                    b.MouseLeave += new EventHandler(this.playButton_Leave);
                    b.Click += new EventHandler(this.button_click);
                    panel_Menu1.Controls.Add(b);

                    RJTextBox txblbl = new RJTextBox();
                    txblbl.Tag = "dynamic";
                    txblbl.Left = 155;
                    txblbl.Top = 136;
                    if (Preferencias.GetConfig().val_OcultarUsuario)
                    {
                        txblbl.Texts = strName;
                    } else
                    {
                        txblbl.Texts = strUsuario;
                    }
                    txblbl.Font = new Font("Microsoft Sans Serif", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
                    txblbl.Size = new Size(204, 150);
                    txblbl.ReadOnly = true;
                    txblbl.BackColor = Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
                    txblbl.BorderColor = Color.Transparent;
                    txblbl.BorderFocusColor = Color.Transparent;
                    txblbl.BorderRadius = 5;
                    txblbl.BorderSize = 2;
                    txblbl.ForeColor = Color.White;
                    txblbl.Margin = new Padding(4);
                    txblbl.Multiline = false;
                    txblbl.Padding = new Padding(10, 7, 10, 7);
                    txblbl.PasswordChar = false;
                    txblbl.PlaceholderColor = Color.DarkGray;
                    txblbl.MouseEnter += new EventHandler(this.tbx_MouseEnter);
                    txblbl.MouseLeave += new EventHandler(this.tbx_MouseLeave);
                    panel_Menu1.Controls.Add(txblbl);

                    RJTextBox txb = new RJTextBox();
                    txb.Tag = "dynamic";
                    txb.Left = 155;
                    txb.Top = 169;
                    txb.Texts = strContra;
                    txb.Font = new Font("Microsoft JhengHei UI", 7, FontStyle.Regular);
                    txb.Size = new Size(204, 150);
                    txb.ReadOnly = true;
                    txb.PasswordChar = true;
                    txb.BackColor = Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
                    txb.BorderColor = Color.Transparent;
                    txb.BorderFocusColor = Color.Transparent;
                    txb.BorderRadius = 5;
                    txb.BorderSize = 2;
                    txb.ForeColor = Color.White;
                    txb.Margin = new Padding(4);
                    txb.Multiline = false;
                    txb.Padding = new Padding(10, 7, 10, 7);
                    txb.PasswordChar = true;
                    txb.PlaceholderColor = Color.DarkGray;
                    txb.MouseEnter += new EventHandler(this.tbx_MouseEnter);
                    txb.MouseLeave += new EventHandler(this.tbx_MouseLeave);
                    panel_Menu1.Controls.Add(txb);

                    Label lblIMG = new Label();
                    lblIMG.Name = "dynamic";
                    lblIMG.Tag = "dynamic";
                    lblIMG.Left = 64;
                    lblIMG.Top = 137;
                    lblIMG.Size = new Size(60, 60);
                    lblIMG.Font = new Font("Microsoft JhengHei UI", 12, FontStyle.Regular);
                    lblIMG.MouseMove += new MouseEventHandler(this.Form_MouseMove);
                    panel_Menu1.Controls.Add(lblIMG);

                    if (Esimagen == "user-01")
                    {
                        lblIMG.Image = new Bitmap(Resources.user01none);
                    }
                    else if (Esimagen == "user-02")
                    {
                        lblIMG.Image = new Bitmap(Resources.user02none);
                    }
                    else
                    {
                        try
                        {
                            FileStream fs = new System.IO.FileStream(tempPath + @"\SLA Manager\imgs\" + cuentaArray[0] + " Medium" + ".png", FileMode.Open, FileAccess.Read);
                            lblIMG.Image = Image.FromStream(fs);
                            fs.Close();
                        }
                        catch
                        {
                            lblIMG.Image = Resources.user03none;
                        }
                    }
                }
                else if (lines.Length == 2)
                {
                    int j = 0;
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string cuenta = lines[i];
                        string[] cuentaArray = cuenta.Split(',');

                        string strName = cuentaArray[0];
                        string strUsuario = Seguridad.DesEncriptar(cuentaArray[1]);
                        string strContra = Seguridad.DesEncriptar(cuentaArray[2]);
                        string Esimagen = cuentaArray[3];

                        Button b = new Button();
                        b.Location = new Point(379, 97 + j);
                        b.Tag = "dynamic";
                        b.Name = "button_" + (i).ToString();
                        b.Text = "";
                        b.FlatStyle = FlatStyle.Flat;
                        b.BackColor = Color.Transparent;
                        b.FlatAppearance.MouseDownBackColor = Color.Transparent;
                        b.FlatAppearance.MouseOverBackColor = Color.Transparent;
                        b.FlatAppearance.BorderSize = 0;
                        b.Size = new Size(50, 50);
                        b.Padding = new Padding(0);
                        b.Image = new Bitmap(Resources.play_button_solo);
                        b.MouseEnter += new EventHandler(this.playButton_Enter);
                        b.MouseLeave += new EventHandler(this.playButton_Leave);
                        b.Click += new EventHandler(this.button_click);
                        panel_Menu1.Controls.Add(b);

                        RJTextBox txblbl = new RJTextBox();
                        txblbl.Tag = "dynamic";
                        txblbl.Left = 155;
                        txblbl.Top = 91 + j;
                        if (Preferencias.GetConfig().val_OcultarUsuario)
                        {
                            txblbl.Texts = strName;
                        } else
                        {
                            txblbl.Texts = strUsuario;
                        }
                        txblbl.Font = new Font("Microsoft Sans Serif", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
                        txblbl.Size = new Size(204, 27);
                        txblbl.ReadOnly = true;
                        txblbl.BackColor = Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
                        txblbl.BorderColor = Color.Transparent;
                        txblbl.BorderFocusColor = Color.Transparent;
                        txblbl.BorderRadius = 5;
                        txblbl.BorderSize = 2;
                        txblbl.ForeColor = Color.White;
                        txblbl.Margin = new Padding(4);
                        txblbl.Multiline = false;
                        txblbl.Padding = new Padding(10, 7, 10, 7);
                        txblbl.PasswordChar = false;
                        txblbl.PlaceholderColor = Color.DarkGray;
                        txblbl.MouseEnter += new EventHandler(this.tbx_MouseEnter);
                        txblbl.MouseLeave += new EventHandler(this.tbx_MouseLeave);
                        panel_Menu1.Controls.Add(txblbl);

                        RJTextBox txb = new RJTextBox();
                        txb.Tag = "dynamic";
                        txb.Left = 155;
                        txb.Top = 124 + j;
                        txb.Texts = strContra;
                        txb.Font = new Font("Microsoft JhengHei UI", 7, FontStyle.Regular);
                        txb.Size = new Size(204, 27);
                        txb.ReadOnly = true;
                        txb.PasswordChar = true;
                        txb.BackColor = Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
                        txb.BorderColor = Color.Transparent;
                        txb.BorderFocusColor = Color.Transparent;
                        txb.BorderRadius = 5;
                        txb.BorderSize = 2;
                        txb.ForeColor = Color.White;
                        txb.Margin = new Padding(4);
                        txb.Multiline = false;
                        txb.Padding = new Padding(10, 7, 10, 7);
                        txb.PasswordChar = true;
                        txb.PlaceholderColor = Color.DarkGray;
                        txb.MouseEnter += new EventHandler(this.tbx_MouseEnter);
                        txb.MouseLeave += new EventHandler(this.tbx_MouseLeave);
                        panel_Menu1.Controls.Add(txb);

                        Label lblIMG = new Label();
                        lblIMG.Name = "dynamic";
                        lblIMG.Tag = "dynamic";
                        lblIMG.Left = 64;
                        lblIMG.Top = 92 + j;
                        lblIMG.Size = new Size(60, 60);
                        lblIMG.Font = new Font("Microsoft JhengHei UI", 12, FontStyle.Regular);
                        lblIMG.MouseMove += new MouseEventHandler(this.Form_MouseMove);
                        panel_Menu1.Controls.Add(lblIMG);

                        if (Esimagen == "user-01")
                        {
                            lblIMG.Image = new Bitmap(Resources.user01none);
                        }
                        else if (Esimagen == "user-02")
                        {
                            lblIMG.Image = new Bitmap(Resources.user02none);
                        }
                        else
                        {
                            try
                            {
                                FileStream fs = new System.IO.FileStream(tempPath + @"\SLA Manager\imgs\" + cuentaArray[0] + " Medium" + ".png", FileMode.Open, FileAccess.Read);
                                lblIMG.Image = Image.FromStream(fs);
                                fs.Close();
                            }
                            catch
                            {
                                lblIMG.Image = Resources.user03none;
                            }
                        }

                        j = j + 90;
                    }
                }
                else if (lines.Length == 3)
                {
                    int j = 0;
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string cuenta = lines[i];
                        string[] cuentaArray = cuenta.Split(',');

                        string strName = cuentaArray[0];
                        string strUsuario = Seguridad.DesEncriptar(cuentaArray[1]);
                        string strContra = Seguridad.DesEncriptar(cuentaArray[2]);
                        string Esimagen = cuentaArray[3];

                        Button b = new Button();
                        b.Location = new Point(379, 52 + j);
                        b.Tag = "dynamic";
                        b.Name = "button_" + (i).ToString();
                        b.Text = "";
                        b.FlatStyle = FlatStyle.Flat;
                        b.BackColor = Color.Transparent;
                        b.FlatAppearance.MouseDownBackColor = Color.Transparent;
                        b.FlatAppearance.MouseOverBackColor = Color.Transparent;
                        b.FlatAppearance.BorderSize = 0;
                        b.Size = new Size(50, 50);
                        b.Padding = new Padding(0);
                        b.Image = new Bitmap(Resources.play_button_solo);
                        b.MouseEnter += new EventHandler(this.playButton_Enter);
                        b.MouseLeave += new EventHandler(this.playButton_Leave);
                        b.Click += new EventHandler(this.button_click);
                        panel_Menu1.Controls.Add(b);

                        RJTextBox txblbl = new RJTextBox();
                        txblbl.Tag = "dynamic";
                        txblbl.Left = 155;
                        txblbl.Top = 46 + j;
                        if (Preferencias.GetConfig().val_OcultarUsuario)
                        {
                            txblbl.Texts = strName;
                        } else
                        {
                            txblbl.Texts = strUsuario;
                        }
                        txblbl.Font = new Font("Microsoft Sans Serif", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
                        txblbl.Size = new Size(204, 27);
                        txblbl.ReadOnly = true;
                        txblbl.BackColor = Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
                        txblbl.BorderColor = Color.Transparent;
                        txblbl.BorderFocusColor = Color.Transparent;
                        txblbl.BorderRadius = 5;
                        txblbl.BorderSize = 2;
                        txblbl.ForeColor = Color.White;
                        txblbl.Margin = new Padding(4);
                        txblbl.Multiline = false;
                        txblbl.Padding = new Padding(10, 7, 10, 7);
                        txblbl.PasswordChar = false;
                        txblbl.PlaceholderColor = Color.DarkGray;
                        txblbl.MouseEnter += new EventHandler(this.tbx_MouseEnter);
                        txblbl.MouseLeave += new EventHandler(this.tbx_MouseLeave);
                        panel_Menu1.Controls.Add(txblbl);

                        RJTextBox txb = new RJTextBox();
                        txb.Tag = "dynamic";
                        txb.Left = 155;
                        txb.Top = 79 + j;
                        txb.Texts = strContra;
                        txb.Font = new Font("Microsoft JhengHei UI", 7, FontStyle.Regular);
                        txb.Size = new Size(204, 27);
                        txb.ReadOnly = true;
                        txb.PasswordChar = true;
                        txb.BackColor = Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
                        txb.BorderColor = Color.Transparent;
                        txb.BorderFocusColor = Color.Transparent;
                        txb.BorderRadius = 5;
                        txb.BorderSize = 2;
                        txb.ForeColor = Color.White;
                        txb.Margin = new Padding(4);
                        txb.Multiline = false;
                        txb.Padding = new Padding(10, 7, 10, 7);
                        txb.PasswordChar = true;
                        txb.PlaceholderColor = Color.DarkGray;
                        txb.MouseEnter += new EventHandler(this.tbx_MouseEnter);
                        txb.MouseLeave += new EventHandler(this.tbx_MouseLeave);
                        panel_Menu1.Controls.Add(txb);

                        Label lblIMG = new Label();
                        lblIMG.Name = "dynamic";
                        lblIMG.Tag = "dynamic";
                        lblIMG.Left = 64;
                        lblIMG.Top = 47 + j;
                        lblIMG.Size = new Size(60, 60);
                        lblIMG.Font = new Font("Microsoft JhengHei UI", 12, FontStyle.Regular);
                        lblIMG.MouseMove += new MouseEventHandler(this.Form_MouseMove);
                        panel_Menu1.Controls.Add(lblIMG);

                        if (Esimagen == "user-01")
                        {
                            lblIMG.Image = new Bitmap(Resources.user01none);
                        }
                        else if (Esimagen == "user-02")
                        {
                            lblIMG.Image = new Bitmap(Resources.user02none);
                        }
                        else
                        {
                            try
                            {
                                FileStream fs = new System.IO.FileStream(tempPath + @"\SLA Manager\imgs\" + cuentaArray[0] + " Medium" + ".png", FileMode.Open, FileAccess.Read);
                                lblIMG.Image = Image.FromStream(fs);
                                fs.Close();
                            }
                            catch
                            {
                                lblIMG.Image = Resources.user03none;
                            }
                        }

                        j = j + 90;
                    }
                }
                else if (lines.Length == 4)
                {
                    int j = 0;
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string cuenta = lines[i];
                        string[] cuentaArray = cuenta.Split(',');

                        string strName = cuentaArray[0];
                        string strUsuario = Seguridad.DesEncriptar(cuentaArray[1]);
                        string strContra = Seguridad.DesEncriptar(cuentaArray[2]);
                        string Esimagen = cuentaArray[3];

                        Button b = new Button();
                        b.Location = new Point(379, 7 + j);
                        b.Tag = "dynamic";
                        b.Name = "button_" + (i).ToString();
                        b.Text = "";
                        b.FlatStyle = FlatStyle.Flat;
                        b.BackColor = Color.Transparent;
                        b.FlatAppearance.MouseDownBackColor = Color.Transparent;
                        b.FlatAppearance.MouseOverBackColor = Color.Transparent;
                        b.FlatAppearance.BorderSize = 0;
                        b.Size = new Size(50, 50);
                        b.Padding = new Padding(0);
                        b.Image = new Bitmap(Resources.play_button_solo);
                        b.MouseEnter += new EventHandler(this.playButton_Enter);
                        b.MouseLeave += new EventHandler(this.playButton_Leave);
                        b.Click += new EventHandler(this.button_click);
                        panel_Menu1.Controls.Add(b);

                        RJTextBox txblbl = new RJTextBox();
                        txblbl.Tag = "dynamic";
                        txblbl.Left = 155;
                        txblbl.Top = 1 + j;
                        if (Preferencias.GetConfig().val_OcultarUsuario)
                        {
                            txblbl.Texts = strName;
                        } else
                        {
                            txblbl.Texts = strUsuario;
                        }
                        txblbl.Font = new Font("Microsoft Sans Serif", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
                        txblbl.Size = new Size(204, 27);
                        txblbl.ReadOnly = true;
                        txblbl.BackColor = Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
                        txblbl.BorderColor = Color.Transparent;
                        txblbl.BorderFocusColor = Color.Transparent;
                        txblbl.BorderRadius = 5;
                        txblbl.BorderSize = 2;
                        txblbl.ForeColor = Color.White;
                        txblbl.Margin = new Padding(4);
                        txblbl.Multiline = false;
                        txblbl.Padding = new Padding(10, 7, 10, 7);
                        txblbl.PasswordChar = false;
                        txblbl.PlaceholderColor = Color.DarkGray;
                        txblbl.MouseEnter += new EventHandler(this.tbx_MouseEnter);
                        txblbl.MouseLeave += new EventHandler(this.tbx_MouseLeave);
                        panel_Menu1.Controls.Add(txblbl);

                        RJTextBox txb = new RJTextBox();
                        txb.Tag = "dynamic";
                        txb.Left = 155;
                        txb.Top = 34 + j;
                        txb.Texts = strContra;
                        txb.Font = new Font("Microsoft JhengHei UI", 7, FontStyle.Regular);
                        txb.Size = new Size(204, 27);
                        txb.ReadOnly = true;
                        txb.PasswordChar = true;
                        txb.BackColor = Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
                        txb.BorderColor = Color.Transparent;
                        txb.BorderFocusColor = Color.Transparent;
                        txb.BorderRadius = 5;
                        txb.BorderSize = 2;
                        txb.ForeColor = Color.White;
                        txb.Margin = new Padding(4);
                        txb.Multiline = false;
                        txb.Padding = new Padding(10, 7, 10, 7);
                        txb.PasswordChar = true;
                        txb.PlaceholderColor = Color.DarkGray;
                        txb.MouseEnter += new EventHandler(this.tbx_MouseEnter);
                        txb.MouseLeave += new EventHandler(this.tbx_MouseLeave);
                        panel_Menu1.Controls.Add(txb);

                        Label lblIMG = new Label();
                        lblIMG.Name = "dynamic";
                        lblIMG.Tag = "dynamic";
                        lblIMG.Left = 64;
                        lblIMG.Top = 2 + j;
                        lblIMG.Size = new Size(60, 60);
                        lblIMG.Font = new Font("Microsoft JhengHei UI", 12, FontStyle.Regular);
                        lblIMG.MouseMove += new MouseEventHandler(this.Form_MouseMove);
                        panel_Menu1.Controls.Add(lblIMG);

                        if (Esimagen == "user-01")
                        {
                            lblIMG.Image = new Bitmap(Resources.user01none);
                        }
                        else if (Esimagen == "user-02")
                        {
                            lblIMG.Image = new Bitmap(Resources.user02none);
                        }
                        else
                        {
                            try
                            {
                                FileStream fs = new System.IO.FileStream(tempPath + @"\SLA Manager\imgs\" + cuentaArray[0] + " Medium" + ".png", FileMode.Open, FileAccess.Read);
                                lblIMG.Image = Image.FromStream(fs);
                                fs.Close();
                            }
                            catch
                            {
                                lblIMG.Image = Resources.user03none;
                            }
                        }

                        j = j + 90;
                    }
                }
                else
                {
                    int j = 0;
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string cuenta = lines[i];
                        string[] cuentaArray = cuenta.Split(',');

                        string strName = cuentaArray[0];
                        string strUsuario = Seguridad.DesEncriptar(cuentaArray[1]);
                        string strContra = Seguridad.DesEncriptar(cuentaArray[2]);
                        string Esimagen = cuentaArray[3];

                        Button b = new Button();
                        b.Location = new Point(379, 7 + j);
                        b.Tag = "dynamic";
                        b.Name = "button_" + (i).ToString();
                        b.Text = "";
                        b.FlatStyle = FlatStyle.Flat;
                        b.BackColor = Color.Transparent;
                        b.FlatAppearance.MouseDownBackColor = Color.Transparent;
                        b.FlatAppearance.MouseOverBackColor = Color.Transparent;
                        b.FlatAppearance.BorderSize = 0;
                        b.Size = new Size(50, 50);
                        b.Padding = new Padding(0);
                        b.Image = new Bitmap(Resources.play_button_solo);
                        b.MouseEnter += new EventHandler(this.playButton_Enter);
                        b.MouseLeave += new EventHandler(this.playButton_Leave);
                        b.Click += new EventHandler(this.button_click);
                        panel_Menu1.Controls.Add(b);

                        RJTextBox txblbl = new RJTextBox();
                        txblbl.Tag = "dynamic";
                        txblbl.Left = 155;
                        txblbl.Top = 1 + j;
                        if (Preferencias.GetConfig().val_OcultarUsuario)
                        {
                            txblbl.Texts = strName;
                        } else
                        {
                            txblbl.Texts = strUsuario;
                        }
                        txblbl.Font = new Font("Microsoft Sans Serif", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
                        txblbl.Size = new Size(204, 27);
                        txblbl.ReadOnly = true;
                        txblbl.BackColor = Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
                        txblbl.BorderColor = Color.Transparent;
                        txblbl.BorderFocusColor = Color.Transparent;
                        txblbl.BorderRadius = 5;
                        txblbl.BorderSize = 2;
                        txblbl.ForeColor = Color.White;
                        txblbl.Margin = new Padding(4);
                        txblbl.Multiline = false;
                        txblbl.Padding = new Padding(10, 7, 10, 7);
                        txblbl.PasswordChar = false;
                        txblbl.PlaceholderColor = Color.DarkGray;
                        txblbl.MouseEnter += new EventHandler(this.tbx_MouseEnter);
                        txblbl.MouseLeave += new EventHandler(this.tbx_MouseLeave);
                        panel_Menu1.Controls.Add(txblbl);

                        RJTextBox txb = new RJTextBox();
                        txb.Tag = "dynamic";
                        txb.Left = 155;
                        txb.Top = 34 + j;
                        txb.Texts = strContra;
                        txb.Font = new Font("Microsoft JhengHei UI", 7, FontStyle.Regular);
                        txb.Size = new Size(204, 27);
                        txb.ReadOnly = true;
                        txb.PasswordChar = true;
                        txb.BackColor = Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
                        txb.BorderColor = Color.Transparent;
                        txb.BorderFocusColor = Color.Transparent;
                        txb.BorderRadius = 5;
                        txb.BorderSize = 2;
                        txb.ForeColor = Color.White;
                        txb.Margin = new Padding(4);
                        txb.Multiline = false;
                        txb.Padding = new Padding(10, 7, 10, 7);
                        txb.PasswordChar = true;
                        txb.PlaceholderColor = Color.DarkGray;
                        txb.MouseEnter += new EventHandler(this.tbx_MouseEnter);
                        txb.MouseLeave += new EventHandler(this.tbx_MouseLeave);
                        panel_Menu1.Controls.Add(txb);

                        Label lblIMG = new Label();
                        lblIMG.Name = "dynamic";
                        lblIMG.Tag = "dynamic";
                        lblIMG.Left = 64;
                        lblIMG.Top = 2 + j;
                        lblIMG.Size = new Size(60, 60);
                        lblIMG.Font = new Font("Microsoft JhengHei UI", 12, FontStyle.Regular);
                        lblIMG.MouseMove += new MouseEventHandler(this.Form_MouseMove);
                        panel_Menu1.Controls.Add(lblIMG);

                        if (Esimagen == "user-01")
                        {
                            lblIMG.Image = new Bitmap(Resources.user01none);
                        }
                        else if (Esimagen == "user-02")
                        {
                            lblIMG.Image = new Bitmap(Resources.user02none);
                        }
                        else
                        {
                            try
                            {
                                FileStream fs = new System.IO.FileStream(tempPath + @"\SLA Manager\imgs\" + cuentaArray[0] + " Medium" + ".png", FileMode.Open, FileAccess.Read);
                                lblIMG.Image = Image.FromStream(fs);
                                fs.Close();
                            }
                            catch
                            {
                                lblIMG.Image = Resources.user03none;
                            }
                        }

                        j = j + 90;
                    }
                }
            } catch
            {
                Debug.WriteLine("Error al crear las cuentas dinamicas!");
            }

        }

        private void playButton_Enter(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Image = new Bitmap(Resources.play_button_shadow);
        }

        private void playButton_Leave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Image = new Bitmap(Resources.play_button_solo);
        }
        
        void button_click(object sender, EventArgs e)
        {
            Verificar();

            if (Preferencias.GetConfig().val_SteamPath != "")
            {
                Button btn = sender as Button;
                lblIniciado.Text = btn.Name;

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
                    Thread.Sleep(100);
                    string btns = lblIniciado.Text;
                    string numero = System.Text.RegularExpressions.Regex.Match(btns, @"\d+").Value;

                    string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    string fileData = tempPath + @"\SLA Manager\data\steam_data.txt";

                    string[] linesData = File.ReadAllLines(fileData); //data

                    string[] cuentaArray2 = linesData[Int32.Parse(numero)].Split(',');

                    string strUsuario = Seguridad.DesEncriptar(cuentaArray2[1]);
                    string strContra = Seguridad.DesEncriptar(cuentaArray2[2]);

                    string quote = "\"";
                    string PathInicio = quote + SteamPath + quote;
                    string PathParamentros = " -login " + strUsuario + " " + strContra;
                    Process.Start(PathInicio, PathParamentros);
                }
                catch
                {
                    Debug.WriteLine("No se pudo abrir Steam");
                }

                if (Preferencias.GetConfig().val_MinimizarProgramas == true)
                {
                    this.WindowState = FormWindowState.Minimized;
                    Preferencias.SetConfig("MensajeMinimizar", "true");
                } else
                {
                    this.WindowState = FormWindowState.Normal;
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

    }
}
