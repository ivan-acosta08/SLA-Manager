
using CustomAlertBoxDemo;
using CustomControls.RJControls;
using SLA_Manager.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static SLA_Manager.Form_Main;

namespace SLA_Manager
{
    public partial class Form_Main
    {
        private void initBorrarCuentas()
        {
            string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string fileName = tempPath + @"\SLA Manager\data\steam_data.txt";

            string[] lines = File.ReadAllLines(fileName);

            if (lines.Length == 0)
            {
                lbl_p4_noaccounts.Visible = true;
                pic_p4_noaccounts.Visible = true;
            }
            else
            {
                lbl_p4_noaccounts.Visible = false;
                pic_p4_noaccounts.Visible = false;

                for (int i = 0; i < lines.Length; i++)
                {
                    string LineaCompleta = lines[i];
                    string[] LineaCompletaArray = LineaCompleta.Split(',');

                    Image myImage = new Bitmap(Resources.no_accounts);
                    lstBorrarCuentas.Items.Add("  " + LineaCompletaArray[0]);
                }
            }
        }

        private void lstBorrarCuentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstBorrarCuentas.SelectedItem != null)
            {
                btn_p4_delete.Enabled = true;

                p4_contraseña = true;
                pic_p4_visible.BackgroundImage = Properties.Resources.visible;
                txb_p4_contraseña.PasswordChar = true;

                txb_p4_usuario.Enabled = true;
                txb_p4_contraseña.Enabled = true;
                pic_p4_visible.Enabled = true;

                string NombreSelect = lstBorrarCuentas.Text.Remove(0, 2);

                string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string fileName = tempPath + @"\SLA Manager\data\steam_data.txt";

                string[] lines = File.ReadAllLines(fileName);

                for (int i = 0; i < lines.Length; i++)
                {
                    string LineaCompleta = lines[i];
                    string[] LineaCompletaArray = LineaCompleta.Split(',');

                    if (LineaCompletaArray[0] == NombreSelect)
                    {
                        string strUsuario = Seguridad.DesEncriptar(LineaCompletaArray[1]);
                        string strContra = Seguridad.DesEncriptar(LineaCompletaArray[2]);
                        string strTipo = LineaCompletaArray[3];

                        txb_p4_usuario.Texts = strUsuario;
                        txb_p4_contraseña.Texts = strContra;

                        if (strTipo == "user-01")
                        {
                            Image imgPuesta1 = new Bitmap(Properties.Resources.user01fit);
                            pic_p4_user.BackgroundImage = imgPuesta1;
                        }
                        else if (strTipo == "user-02")
                        {
                            Image imgPuesta2 = new Bitmap(Properties.Resources.user02fit);
                            pic_p4_user.BackgroundImage = imgPuesta2;
                        }
                        else
                        {
                            try
                            {
                                FileStream fs = new System.IO.FileStream(tempPath + @"\SLA Manager\imgs\" + NombreSelect + " Full" + ".png", FileMode.Open, FileAccess.Read);
                                pic_p4_user.BackgroundImage = Image.FromStream(fs);
                                fs.Close();
                            }
                            catch
                            {
                                Image imgPuesta4 = new Bitmap(Properties.Resources.user03none);
                                pic_p4_user.BackgroundImage = imgPuesta4;
                            }
                        }
                    }

                }
            } else
            {
                btn_p4_delete.Enabled = false;
            }
        }

        public bool p4_contraseña = true;

        private void pic_p4_visible_Click(object sender, EventArgs e)
        {
            if (p4_contraseña == true)
            {
                p4_contraseña = false;
                pic_p4_visible.BackgroundImage = Properties.Resources.ocultar;
                txb_p4_contraseña.PasswordChar = false;
            }
            else
            {
                p4_contraseña = true;
                pic_p4_visible.BackgroundImage = Properties.Resources.visible;
                txb_p4_contraseña.PasswordChar = true;
            }
        }
        private void btn_p4_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstBorrarCuentas.SelectedItem != null)
                {
                    string NombreSelect = lstBorrarCuentas.Text.Remove(0, 2);

                    string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    string deletedata_file = tempPath + @"\SLA Manager\data\steam_data.txt";

                    string[] lines = File.ReadAllLines(deletedata_file);

                    for (int i = 0; i < lines.Length; i++)
                    {
                        string LineaCompleta = lines[i];
                        if (LineaCompleta.Contains(NombreSelect))
                        {
                            string cuenta = LineaCompleta.Split(",")[0];

                            if (Preferencias.GetConfig().val_lang == "español")
                            {
                                DialogResult dialogResult = MessageBox.Show("¿Desea eliminar este usuario?", "SLA Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    File.WriteAllText(deletedata_file, "");

                                    for (int j = 0; j < lines.Length; j++)
                                    {
                                        string LineaCompleta2 = lines[j];

                                        if (!LineaCompleta2.Contains(NombreSelect))
                                        {
                                            File.AppendAllText(deletedata_file, LineaCompleta2 + Environment.NewLine);
                                        }

                                        if (j == lines.Length - 1)
                                        {
                                            lstBorrarCuentas.Items.Clear();

                                            pic_p4_user.BackgroundImage = Properties.Resources.user01normal;
                                            txb_p4_usuario.Texts = "";
                                            txb_p4_contraseña.Texts = "";
                                            p4_contraseña = true;
                                            txb_p4_usuario.Enabled = false;
                                            txb_p4_contraseña.Enabled = false;
                                            pic_p4_visible.Enabled = false;
                                            btn_p4_delete.Enabled = false;
                                            pic_p4_visible.BackgroundImage = Properties.Resources.visible;

                                            if (File.Exists(tempPath + @"\SLA Manager\imgs\" + cuenta + " Full.png"))
                                            {
                                                try
                                                {
                                                    File.Delete(tempPath + @"\SLA Manager\imgs\" + cuenta + " Full.png");
                                                }
                                                catch
                                                {
                                                    Debug.WriteLine("Error al borrar la imagen Full");
                                                }
                                            }

                                            if (File.Exists(tempPath + @"\SLA Manager\imgs\" + cuenta + " Medium.png"))
                                            {
                                                try
                                                {
                                                    File.Delete(tempPath + @"\SLA Manager\imgs\" + cuenta + " Medium.png");
                                                }
                                                catch
                                                {
                                                    Debug.WriteLine("Error al borrar la imagen Medium");
                                                }
                                            }


                                            initBorrarCuentas();
                                            
                                            this.Alert("Cuenta eliminada\nsatisfactoriamente", Form_Alert.enmType.Success);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                DialogResult dialogResult = MessageBox.Show("¿Do you want to delete this user?", "SLA Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    File.WriteAllText(deletedata_file, "");

                                    for (int j = 0; j < lines.Length; j++)
                                    {
                                        string LineaCompleta2 = lines[j];

                                        if (!LineaCompleta2.Contains(NombreSelect))
                                        {
                                            File.AppendAllText(deletedata_file, LineaCompleta2 + Environment.NewLine);
                                        }

                                        if (j == lines.Length - 1)
                                        {
                                            lstBorrarCuentas.Items.Clear();

                                            pic_p4_user.BackgroundImage = Properties.Resources.user01normal;
                                            txb_p4_usuario.Texts = "";
                                            txb_p4_contraseña.Texts = "";
                                            p4_contraseña = true;
                                            txb_p4_usuario.Enabled = false;
                                            txb_p4_contraseña.Enabled = false;
                                            pic_p4_visible.Enabled = false;
                                            btn_p4_delete.Enabled = false;
                                            pic_p4_visible.BackgroundImage = Properties.Resources.visible;

                                            if (File.Exists(tempPath + @"\SLA Manager\imgs\" + cuenta + " Full.png"))
                                            {
                                                try
                                                {
                                                    File.Delete(tempPath + @"\SLA Manager\imgs\" + cuenta + " Full.png");
                                                }
                                                catch
                                                {
                                                    Debug.WriteLine("Error al borrar la imagen Full");
                                                }
                                            }

                                            if (File.Exists(tempPath + @"\SLA Manager\imgs\" + cuenta + " Medium.png"))
                                            {
                                                try
                                                {
                                                    File.Delete(tempPath + @"\SLA Manager\imgs\" + cuenta + " Medium.png");
                                                }
                                                catch
                                                {
                                                    Debug.WriteLine("Error al borrar la imagen Medium");
                                                }
                                            }


                                            initBorrarCuentas();

                                            this.Alert("Account successfully\ndeleted", Form_Alert.enmType.Success);
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            } catch
            {
                Debug.WriteLine("Error al borrar una cuenta!");
            }
        }

    }
}
