using CustomAlertBoxDemo;
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
using static System.Windows.Forms.DataFormats;

namespace SLA_Manager
{
    public partial class Form_Password : Form
    {
        public string general = "";
        public Form_Password(string modo)
        {
            Debug.Print(modo);
            InitializeComponent();
            general = modo;
            if (modo == "Nueva")
            {
                if (Preferencias.GetConfig().val_lang == "español")
                {
                    lbl_pf_label.Text = "Ingresa una contraseña para futuras acciones...";
                }
                else
                {
                    lbl_pf_label.Text = "Enter a password for future actions...";
                }
            } else
            {
                if (Preferencias.GetConfig().val_lang == "español")
                {
                    lbl_pf_label.Text = "Ingresa la contraseña general para seguir...";
                }
                else
                {
                    lbl_pf_label.Text = "Enter the general password to continue...";
                }
            }
        }

        private void txb_fp_password_MouseEnter(object sender, EventArgs e)
        {
            txb_fp_password.BackColor = System.Drawing.Color.FromArgb(57, 60, 68);
        }

        private void txb_fp_password_MouseLeave(object sender, EventArgs e)
        {
            txb_fp_password.BackColor = System.Drawing.Color.FromArgb(50, 53, 60);
        }

        public int xClick = 0, yClick = 0;

        private void btn_fp_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            btn_fp_close.BackgroundImage = Properties.Resources.close_enter;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btn_fp_close.BackgroundImage = Properties.Resources.close_leave;
        }

        private void btn_pf_aceptar_Click(object sender, EventArgs e)
        {
            irContra(general);
        }

        public void irContra(string modo)
        {
            if (modo == "Nueva")
            {
                if (txb_fp_password.Texts != "")
                {
                    if (Preferencias.GetConfig().val_lang == "español")
                    {
                        DialogResult dialogResult = MessageBox.Show("¿Esta seguro de utilizar esta contraseña?" + Environment.NewLine + Environment.NewLine + "ADVERTENCIA: Usted no podra usar el programa si se olvida de la contraseña!", "SLA Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dialogResult == DialogResult.Yes)
                        {
                            Preferencias.SetConfig("Contraseña", Seguridad.Encriptar(txb_fp_password.Texts));

                            this.Hide();
                            var form_main = new Form_Main("Normal");
                            form_main.Closed += (s, args) => this.Close();
                            form_main.Show();
                        }
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show("Are you sure to set this password?" + Environment.NewLine + Environment.NewLine + "WARNING: You will not be able to start this program if you forget the password!", "SLA Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dialogResult == DialogResult.Yes)
                        {
                            Preferencias.SetConfig("Contraseña", Seguridad.Encriptar(txb_fp_password.Texts));

                            this.Hide();
                            var form_main = new Form_Main("Normal");
                            form_main.Closed += (s, args) => this.Close();
                            form_main.Show();
                        }
                    }
                }
            } else
            {
                if (txb_fp_password.Texts != "" && txb_fp_password.Texts == Seguridad.DesEncriptar(Preferencias.GetConfig().val_Contraseña))
                {
                    this.Hide();
                    var form_main = new Form_Main("Normal");
                    form_main.Closed += (s, args) => this.Close();
                    form_main.Show();
                }
                else
                {
                    if (Preferencias.GetConfig().val_lang == "español")
                    {
                        this.Alert("Ingrese la contraseña\ngeneral correcta...", Form_Alert.enmType.Error);
                    }
                    else
                    {
                        this.Alert("Enter the\ncorrect password...", Form_Alert.enmType.Error);
                    }
                }
            }
            
        }

        public void Alert(string msg, Form_Alert.enmType type)
        {
            Form_Alert frm = new Form_Alert();
            frm.showAlert(msg, type);
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            { xClick = e.X; yClick = e.Y; }
            else
            { this.Left = this.Left + (e.X - xClick); this.Top = this.Top + (e.Y - yClick); }
        }

        private void txb_fp_password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13){
                irContra(general);
            }
        }

        bool fp_contraseña = false;

        private void pic_fp_visible_Click(object sender, EventArgs e)
        {
            if (fp_contraseña == true)
            {
                fp_contraseña = false;
                pic_fp_visible.BackgroundImage = Properties.Resources.ocultar;
                txb_fp_password.PasswordChar = false;
            }
            else
            {
                fp_contraseña = true;
                pic_fp_visible.BackgroundImage = Properties.Resources.visible;
                txb_fp_password.PasswordChar = true;
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

    }
}
