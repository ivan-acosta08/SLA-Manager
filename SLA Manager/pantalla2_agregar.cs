
using CustomAlertBoxDemo;
using CustomControls.RJControls;
using SLA_Manager.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        string imagenLocal;

        private void tbx_MouseEnter(object sender, EventArgs e)
        {
            RJTextBox btn = (RJTextBox)sender;
            if (btn != null)
            {
                btn.BackColor = System.Drawing.Color.FromArgb(57, 60, 68);
            }
        }

        private void tbx_MouseLeave(object sender, EventArgs e)
        {
            RJTextBox btn = (RJTextBox)sender;
            if (btn != null)
            {
                btn.BackColor = System.Drawing.Color.FromArgb(50, 53, 60);
            }
        }

        void accept_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char pressedKey = e.KeyChar;
            if (Char.IsControl(pressedKey) || Char.IsLetter(pressedKey) || Char.IsSeparator(pressedKey) || Char.IsDigit(pressedKey) || pressedKey.ToString() == "_" || pressedKey.ToString() == "-")
            {
                e.Handled = false;
            }
            else
            { 
                e.Handled = true;
            }
        }

        private void rad_p2_user1_Click(object sender, EventArgs e)
        {
            pic_p2_main.BackgroundImage = Properties.Resources.user01normal;
            rad_p2_user1.Checked = true;
            rad_p2_user2.Checked = false;
            rad_p2_user3.Checked = false;
            rad_p2_user4.Checked = false;
            lbl_p2_steam64.Visible = false;
            txb_p2_steam64.Visible = false;
            pic_p2_ayuda.Visible = false;
            lbl_p2_chbSelected.Text = "user-01";
        }

        private void rad_p2_user2_Click(object sender, EventArgs e)
        {
            pic_p2_main.BackgroundImage = Properties.Resources.user02normal;
            rad_p2_user1.Checked = false;
            rad_p2_user2.Checked = true;
            rad_p2_user3.Checked = false;
            rad_p2_user4.Checked = false;
            lbl_p2_steam64.Visible = false;
            txb_p2_steam64.Visible = false;
            pic_p2_ayuda.Visible = false;
            lbl_p2_chbSelected.Text = "user-02";
        }

        private void rad_p2_user3_Click(object sender, EventArgs e)
        {
            pic_p2_main.BackgroundImage = Properties.Resources.user03normal;
            rad_p2_user1.Checked = false;
            rad_p2_user2.Checked = false;
            rad_p2_user3.Checked = true;
            rad_p2_user4.Checked = false;
            lbl_p2_steam64.Visible = true;
            txb_p2_steam64.Visible = true;
            pic_p2_ayuda.Visible = true;
            lbl_p2_chbSelected.Text = "user-03";
        }

        private void pic_p2_user4_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.Filter = "Archivos de Imagen (*.png; *.jpg; *.jpeg; *.bmp)|*.png; *.jpg; *.jpeg; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pic_p2_main.BackgroundImage = new Bitmap(open.FileName);
                imagenLocal = open.FileName;

                rad_p2_user1.Checked = false;
                rad_p2_user2.Checked = false;
                rad_p2_user3.Checked = false;
                rad_p2_user4.Checked = true;
                lbl_p2_steam64.Visible = false;
                txb_p2_steam64.Visible = false;
                pic_p2_ayuda.Visible = false;
                lbl_p2_chbSelected.Text = "user-04";
            }
        }

        public bool p2_contraseña = true;

        private void pic_p2_visible_Click(object sender, EventArgs e)
        {
            if (p2_contraseña == true)
            {
                p2_contraseña = false;
                pic_p2_visible.BackgroundImage = Properties.Resources.ocultar;
                txb_p2_contraseña.PasswordChar = false;
            }
            else
            {
                p2_contraseña = true;
                pic_p2_visible.BackgroundImage = Properties.Resources.visible;
                txb_p2_contraseña.PasswordChar = true;
            }
        }

        private void btn_p2_agregar_Click(object sender, EventArgs e)
        {
            Verificar();

            string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string fileName = tempPath + @"\SLA Manager\data\steam_data.txt";

            string[] lines = File.ReadAllLines(fileName);
            string Cuentas = "";
            string Cuentas2 = "";

            for (int i = 0; i < lines.Length; i++)
            {
                string LineaCompleta = lines[i];
                string[] LineaCompletaArray = LineaCompleta.Split(',');

                Cuentas = Cuentas + ", " + LineaCompletaArray[0];
                Cuentas2 = Cuentas2 + ", " + Seguridad.DesEncriptar(LineaCompletaArray[1]);
            }

            if (!Cuentas.Contains(txb_p2_nombrelocal.Texts) && !Cuentas2.Contains(txb_p2_usuario.Texts))
            {
                if (txb_p2_nombrelocal.Texts != "" && txb_p2_usuario.Texts != "" && txb_p2_contraseña.Texts != "")
                {
                    string codUsuario = Seguridad.Encriptar(txb_p2_usuario.Texts);
                    string codContra = Seguridad.Encriptar(txb_p2_contraseña.Texts);

                    switch (lbl_p2_chbSelected.Text)
                    {
                        case "user-01":
                            File.AppendAllText(fileName, txb_p2_nombrelocal.Texts + "," + codUsuario + "," + codContra + "," + lbl_p2_chbSelected.Text + "," + "none" + Environment.NewLine);
                            if (Preferencias.GetConfig().val_lang == "español")
                            {
                                this.Alert("Cuenta agregada\ncorrectamente!", Form_Alert.enmType.Success);
                            }
                            else
                            {
                                this.Alert("Account successfully\nregistered", Form_Alert.enmType.Success);
                            }

                            txb_p2_nombrelocal.Texts = "";
                            txb_p2_steam64.Texts = "";
                            txb_p2_usuario.Texts = "";
                            txb_p2_contraseña.Texts = "";
                            break;

                        case "user-02":
                            File.AppendAllText(fileName, txb_p2_nombrelocal.Texts + "," + codUsuario + "," + codContra + "," + lbl_p2_chbSelected.Text + "," + "none" + Environment.NewLine);
                            if (Preferencias.GetConfig().val_lang == "español")
                            {
                                this.Alert("Cuenta agregada\ncorrectamente!", Form_Alert.enmType.Success);
                            }
                            else
                            {
                                this.Alert("Account successfully\nregistered", Form_Alert.enmType.Success);
                            }

                            txb_p2_nombrelocal.Texts = "";
                            txb_p2_steam64.Texts = "";
                            txb_p2_usuario.Texts = "";
                            txb_p2_contraseña.Texts = "";
                            break;

                        case "user-03":
                            try
                            {
                                if (txb_p2_steam64.Texts != "")
                                {
                                    XmlDocument doc = new XmlDocument();
                                    doc.Load("https://steamcommunity.com/profiles/" + txb_p2_steam64.Texts + "/?xml=1");

                                    StringBuilder sb = new StringBuilder();
                                    foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                                    {
                                        sb.Append(char.ToUpper(node.Name[0]));
                                        sb.Append(node.Name.Substring(1));
                                        sb.Append(' ');
                                        sb.AppendLine(node.InnerText);

                                        string filePath = tempPath + @"\SLA Manager\data\steam_userID64.txt";

                                        using (StreamWriter file = new StreamWriter(filePath))
                                        {
                                            file.WriteLine(sb.ToString());
                                        }

                                        File.WriteAllText(filePath, sb.ToString());

                                        Dictionary<string, string> dictSettings = new Dictionary<string, string>();

                                        var lines2 = File.ReadAllLines(filePath);
                                        for (var i = 2; i < lines2.Length; i += 1)
                                        {
                                            var line2 = lines2[i];

                                            if (!string.IsNullOrEmpty(line2) && line2.Contains(":"))
                                            {
                                                string settingKey = line2.Split(' ')[0];
                                                string settingValue = line2.Split(' ')[1];
                                                dictSettings.Add(settingKey, settingValue);
                                            }
                                        }

                                        string picMedium = dictSettings.ContainsKey("AvatarMedium") ? dictSettings["AvatarMedium"] : "";
                                        string picFull = dictSettings.ContainsKey("AvatarFull") ? dictSettings["AvatarFull"] : "";

                                        txbURLMedium.Text = picMedium;
                                        txbURLFull.Text = picFull;
                                    }

                                    if (!File.Exists(tempPath + @"\SLA Manager\imgs\" + txb_p2_nombrelocal.Texts + " Medium" + ".png"))
                                    {
                                        using (WebClient webClient = new WebClient())
                                        {
                                            byte[] dataArr2 = webClient.DownloadData(txbURLMedium.Text);
                                            File.WriteAllBytes(tempPath + @"\SLA Manager\imgs\" + txb_p2_nombrelocal.Texts + " Medium" + ".png", dataArr2);
                                        }
                                    }

                                    if (!File.Exists(tempPath + @"\SLA Manager\imgs\" + txb_p2_nombrelocal.Texts + " Full" + ".png"))
                                    {
                                        using (WebClient webClient = new WebClient())
                                        {
                                            byte[] dataArr3 = webClient.DownloadData(txbURLFull.Text);
                                            File.WriteAllBytes(tempPath + @"\SLA Manager\imgs\" + txb_p2_nombrelocal.Texts + " Full" + ".png", dataArr3);
                                        }
                                    }

                                    File.AppendAllText(fileName, txb_p2_nombrelocal.Texts + "," + codUsuario + "," + codContra + "," + lbl_p2_chbSelected.Text + "," + txb_p2_steam64.Texts + Environment.NewLine);

                                    FileStream fs = new System.IO.FileStream(tempPath + @"\SLA Manager\imgs\" + txb_p2_nombrelocal.Texts + " Full" + ".png", FileMode.Open, FileAccess.Read);
                                    pic_p2_main.BackgroundImage = Image.FromStream(fs);
                                    fs.Close();

                                    txb_p2_nombrelocal.Texts = "";
                                    txb_p2_steam64.Texts = "";
                                    txb_p2_usuario.Texts = "";
                                    txb_p2_contraseña.Texts = "";

                                    if (Preferencias.GetConfig().val_lang == "español")
                                    {
                                        this.Alert("Cuenta agregada\ncorrectamente!", Form_Alert.enmType.Success);
                                    }
                                    else
                                    {
                                        this.Alert("Account successfully\nregistered", Form_Alert.enmType.Success);
                                    }

                                    try
                                    {

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                        ErrorInternet();
                                    }
                                }
                                else
                                {
                                    if (Preferencias.GetConfig().val_lang == "español")
                                    {
                                        this.Alert("Ingresa un STEAM ID\ncorrecto...", Form_Alert.enmType.Error);
                                    }
                                    else
                                    {
                                        this.Alert("Please enter a\nvalid STEAM ID", Form_Alert.enmType.Error);
                                    }
                                }
                            }
                            catch
                            {
                                Debug.WriteLine("Error al agregar cuenta con steam64");
                                ErrorInternet();
                            }
                            break;

                        case "user-04":
                            try
                            {
                                using (Image image = Image.FromFile(imagenLocal))
                                {
                                    new Bitmap(image, 128, 128).Save(tempPath + @"\SLA Manager\imgs\" + txb_p2_nombrelocal.Texts + " Full.png");
                                }

                                using (Image image = Image.FromFile(imagenLocal))
                                {
                                    new Bitmap(image, 64, 64).Save(tempPath + @"\SLA Manager\imgs\" + txb_p2_nombrelocal.Texts + " Medium.png");
                                }

                                Thread.Sleep(1000);

                                FileStream fs = new System.IO.FileStream(tempPath + @"\SLA Manager\imgs\" + txb_p2_nombrelocal.Texts + " Full" + ".png", FileMode.Open, FileAccess.Read);
                                pic_p2_main.BackgroundImage = Image.FromStream(fs);
                                fs.Close();

                                File.AppendAllText(fileName, txb_p2_nombrelocal.Texts + "," + codUsuario + "," + codContra + "," + lbl_p2_chbSelected.Text + ",none" + Environment.NewLine);
                                if (Preferencias.GetConfig().val_lang == "español")
                                {
                                    this.Alert("Cuenta agregada\ncorrectamente!", Form_Alert.enmType.Success);
                                }
                                else
                                {
                                    this.Alert("Account successfully\nregistered", Form_Alert.enmType.Success);
                                }

                                txb_p2_nombrelocal.Texts = "";
                                txb_p2_steam64.Texts = "";
                                txb_p2_usuario.Texts = "";
                                txb_p2_contraseña.Texts = "";
                            }
                            catch
                            {
                                Debug.WriteLine("Error al copiar imagen del directorio!");
                            }
                            break;
                    }
                }
                else
                {
                    if (Preferencias.GetConfig().val_lang == "español")
                    {
                        this.Alert("Cuenta agregada en\nel sistema con anterioridad!", Form_Alert.enmType.Warning);
                    } else
                    {
                        this.Alert("Account already\nadded to the system", Form_Alert.enmType.Warning);
                    }
                }
            } else
            {
                if (Preferencias.GetConfig().val_lang == "español")
                {
                    this.Alert("Ingresa los datos\ncorrectamente...", Form_Alert.enmType.Warning);
                }
                else
                {
                    this.Alert("Enter your data\ncorrectly", Form_Alert.enmType.Warning);
                }
            }

        }

        public void ErrorInternet()
        {
            if (Preferencias.GetConfig().val_lang == "español")
            {
                MessageBox.Show("Hubo un problema con la descarga del perfil de Steam..." + Environment.NewLine + Environment.NewLine + "¿Ingresó correctamente el Steam ID y tiene una conexión estable a internet?", "SLA Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("There was a problem downloading the Steam profile..." + Environment.NewLine + Environment.NewLine + "¿You have correctly filled your Steam ID and you have a stable internet connection?", "SLA Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pic_p2_ayuda_Click(object sender, EventArgs e)
        {
            this.Alert("", Form_Alert.enmType.SteamID);
        }

        private void txb_p2_steam64_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public void Alert(string msg, Form_Alert.enmType type)
        {
            Form_Alert frm = new Form_Alert();
            frm.showAlert(msg, type);
        }
    }
}