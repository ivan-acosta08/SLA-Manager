
using CustomAlertBoxDemo;
using CustomControls.RJControls;
using SLA_Manager.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static SLA_Manager.Form_Main;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SLA_Manager
{
    public partial class Form_Main
    {
        string user_1, contra_1, steam64_1;
        int rad_1, rad_2;
        string TodosUsuarios = "";
        bool editado = false;
        bool trySteam64 = false;
        string imagenLocal2 = "";

        private void initEditarCuentas()
        {
            lstEditarCuentas.Items.Clear();
            pic_p3_user.BackgroundImage = Resources.user01normal;
            txb_p3_usuario.Texts = "";
            txb_p3_contraseña.Texts = "";
            txb_p3_steam64.Texts = "";
            p3_contraseña = true;

            btn_p3_editar.Enabled = false;
            
            TodosUsuarios = "";
            imagenLocal2 = "";

            user_1 = "";
            contra_1 = "";
            steam64_1 = "";
            rad_1 = 0; rad_2 = 0;
            editado = false; trySteam64 = false;

            lbl_p3_img.Visible = false;
            pnl_p3_img1.Visible = false;
            pnl_p3_img2.Visible = false;
            pnl_p3_img3.Visible = false;
            pnl_p3_img4.Visible = false;

            txb_p3_usuario.Enabled = false;
            txb_p3_contraseña.Enabled = false;
            txb_p3_steam64.Enabled = false;
            btn_p3_guardar.Visible = false;
            pic_p3_visible.BackgroundImage = Resources.visible;

            pic_p3_user.Size = new Size(75, 75);
            pic_p3_user.Location = new Point(284, 14);

            string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string fileName = tempPath + @"\SLA Manager\data\steam_data.txt";

            string[] lines = File.ReadAllLines(fileName);

            if (lines.Length == 0)
            {
                lbl_p3_noaccounts.Visible = true;
                pic_p3_noaccounts.Visible = true;
            }
            else
            {
                lbl_p3_noaccounts.Visible = false;
                pic_p3_noaccounts.Visible = false;

                for (int i = 0; i < lines.Length; i++)
                {
                    string LineaCompleta = lines[i];
                    string[] LineaCompletaArray = LineaCompleta.Split(',');

                    Image myImage = new Bitmap(Resources.no_accounts);
                    lstEditarCuentas.Items.Add("  " + LineaCompletaArray[0]);
                }
            }
        }

        public bool p3_contraseña = true;

        private void pic_p3_visible_Click(object sender, EventArgs e)
        {
            if (p3_contraseña == true)
            {
                p3_contraseña = false;
                pic_p3_visible.BackgroundImage = Properties.Resources.ocultar;
                txb_p3_contraseña.PasswordChar = false;
            }
            else
            {
                p3_contraseña = true;
                pic_p3_visible.BackgroundImage = Properties.Resources.visible;
                txb_p3_contraseña.PasswordChar = true;
            }
        }

        private void lstEditarCuentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstEditarCuentas.SelectedIndex != -1)
                {
                    lbl_p3_img.Visible = true;
                    pnl_p3_img1.Visible = true;
                    pnl_p3_img2.Visible = true;
                    pnl_p3_img3.Visible = true;
                    pnl_p3_img4.Visible = true;
                    pnl_p3_img1.Enabled = false;
                    pnl_p3_img2.Enabled = false;
                    pnl_p3_img3.Enabled = false;
                    pnl_p3_img4.Enabled = false;

                    pic_p3_user.Size = new Size(75, 75);
                    pic_p3_user.Location = new Point(347, 24);

                    btn_p3_guardar.Visible = false;
                    txb_p3_usuario.Enabled = false;
                    txb_p3_contraseña.Enabled = false;
                    txb_p3_steam64.Enabled = false;
                    txb_p3_steam64.Enabled = false;
                    btn_p3_editar.Enabled = true;

                    p3_contraseña = true;
                    pic_p3_visible.BackgroundImage = Properties.Resources.visible;
                    txb_p3_contraseña.PasswordChar = true;

                    string NombreSelect = lstEditarCuentas.Text.Remove(0, 2);

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

                            txb_p3_usuario.Texts = strUsuario;
                            txb_p3_contraseña.Texts = strContra;

                            if (LineaCompletaArray[4] != "none")
                            {
                                txb_p3_steam64.Texts = LineaCompletaArray[4];
                            }
                            else
                            {
                                txb_p3_steam64.Texts = "";
                            }

                            if (strTipo == "user-01")
                            {
                                Image imgPuesta1 = new Bitmap(Properties.Resources.user01fit);
                                pic_p3_user.BackgroundImage = imgPuesta1;

                                rad_p3_img1.Checked = true;
                                rad_p3_img2.Checked = false;
                                rad_p3_img3.Checked = false;
                                rad_p3_img4.Checked = false;
                            }
                            else if (strTipo == "user-02")
                            {
                                Image imgPuesta2 = new Bitmap(Properties.Resources.user02fit);
                                pic_p3_user.BackgroundImage = imgPuesta2;

                                rad_p3_img1.Checked = false;
                                rad_p3_img2.Checked = true;
                                rad_p3_img3.Checked = false;
                                rad_p3_img4.Checked = false;
                            }
                            else
                            {
                                if (strTipo == "user-03")
                                {
                                    rad_p3_img1.Checked = false;
                                    rad_p3_img2.Checked = false;
                                    rad_p3_img3.Checked = true;
                                    rad_p3_img4.Checked = false;
                                }
                                else if (strTipo == "user-04")
                                {
                                    rad_p3_img1.Checked = false;
                                    rad_p3_img2.Checked = false;
                                    rad_p3_img3.Checked = false;
                                    rad_p3_img4.Checked = true;

                                }

                                try
                                {
                                    FileStream fs = new System.IO.FileStream(tempPath + @"\SLA Manager\imgs\" + NombreSelect + " Full" + ".png", FileMode.Open, FileAccess.Read);
                                    pic_p3_user.BackgroundImage = Image.FromStream(fs);
                                    fs.Close();
                                }
                                catch
                                {
                                    Image imgPuesta4 = new Bitmap(Properties.Resources.user03none);
                                    pic_p3_user.BackgroundImage = imgPuesta4;
                                }
                            }
                        }

                    }
                }
            } catch
            {
                Debug.Print("Error con la lista para editar usuarios!");
            }
        }

        private void btn_p3_editar_Click(object sender, EventArgs e)
        {
            pnl_p3_img1.Enabled = true;
            pnl_p3_img2.Enabled = true;
            pnl_p3_img3.Enabled = true;
            pnl_p3_img4.Enabled = true;

            txb_p3_usuario.Enabled = true;
            txb_p3_contraseña.Enabled = true;
            txb_p3_steam64.Enabled = true;

            btn_p3_guardar.Visible = true;
            txb_p3_steam64.Enabled = true;

            user_1 = txb_p3_usuario.Texts;
            contra_1 = txb_p3_contraseña.Texts;
            steam64_1 = txb_p3_steam64.Texts;

            if (rad_p3_img1.Checked) {
                rad_1 = 1;
            } else if (rad_p3_img2.Checked) {
                rad_1 = 2;
            } else if (rad_p3_img3.Checked) {
                rad_1 = 3;
            } else {
                rad_1 = 4;
            }
        }

        private void btn_p3_guardar_Click(object sender, EventArgs e)
        {
            if (rad_p3_img1.Checked) {
                rad_2 = 1;
            } else if (rad_p3_img2.Checked) {
                rad_2 = 2;
            } else if (rad_p3_img3.Checked) {
                rad_2 = 3;
            } else {
                rad_2 = 4;
            }

            if (user_1 != txb_p3_usuario.Texts || contra_1 != txb_p3_contraseña.Texts || rad_1 != rad_2 || rad_1 == rad_2 || steam64_1 != txb_p3_steam64.Texts)
            {
                string NombreSelect = lstEditarCuentas.Text.Remove(0, 2);
                string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string editdata_file = tempPath + @"\SLA Manager\data\steam_data.txt";

                string[] lines = File.ReadAllLines(editdata_file);

                if (Preferencias.GetConfig().val_lang == "español")
                {
                    DialogResult dialogResult = MessageBox.Show("¿Desea modificar este usuario?", "SLA Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        for (int i = 0; i < lines.Length; i++)
                        {
                            string LineaCompleta = lines[i];
                            TodosUsuarios = TodosUsuarios + Seguridad.DesEncriptar(LineaCompleta.Split(",")[1]) + ", ";
                        }

                        if (Regex.Matches(TodosUsuarios, txb_p3_usuario.Texts + ",").Count > 1)
                        {
                            if (Preferencias.GetConfig().val_lang == "español")
                            {
                                this.Alert("Cuenta agregada en\nel sistema con anterioridad!", Form_Alert.enmType.Warning);
                            }
                            else
                            {
                                this.Alert("Account previously\nregistered in the system", Form_Alert.enmType.Warning);
                            }
                        }
                        else
                        {
                            File.WriteAllText(editdata_file, "");

                            for (int j = 0; j < lines.Length; j++)
                            {
                                string LineaCompleta2 = lines[j];
                                if (!LineaCompleta2.Contains(NombreSelect))
                                {
                                    File.AppendAllText(editdata_file, LineaCompleta2 + Environment.NewLine);
                                }
                                else
                                {
                                    if (rad_1 != rad_2 && steam64_1 != txb_p3_steam64.Texts)
                                    {
                                        switch (rad_2)
                                        {
                                            case 1:
                                                File.AppendAllText(editdata_file, LineaCompleta2.Split(",")[0] + "," + Seguridad.Encriptar(txb_p3_usuario.Texts) + "," + Seguridad.Encriptar(txb_p3_contraseña.Texts) + "," + "user-01" + "," + txb_p3_steam64.Texts + Environment.NewLine);
                                                trySteam64 = true;
                                                break;
                                            case 2:
                                                File.AppendAllText(editdata_file, LineaCompleta2.Split(",")[0] + "," + Seguridad.Encriptar(txb_p3_usuario.Texts) + "," + Seguridad.Encriptar(txb_p3_contraseña.Texts) + "," + "user-02" + "," + txb_p3_steam64.Texts + Environment.NewLine);
                                                trySteam64 = true;
                                                break;
                                            case 3:
                                                File.AppendAllText(editdata_file, LineaCompleta2.Split(",")[0] + "," + Seguridad.Encriptar(txb_p3_usuario.Texts) + "," + Seguridad.Encriptar(txb_p3_contraseña.Texts) + "," + "user-03" + "," + txb_p3_steam64.Texts + Environment.NewLine);
                                                trySteam64 = true;
                                                break;
                                            case 4:
                                                File.AppendAllText(editdata_file, LineaCompleta2.Split(",")[0] + "," + Seguridad.Encriptar(txb_p3_usuario.Texts) + "," + Seguridad.Encriptar(txb_p3_contraseña.Texts) + "," + "user-04" + "," + txb_p3_steam64.Texts + Environment.NewLine);
                                                trySteam64 = true;
                                                break;
                                        }
                                    }
                                    else if (rad_1 != rad_2 && steam64_1 == txb_p3_steam64.Texts)
                                    {
                                        switch (rad_2)
                                        {
                                            case 1:
                                                File.AppendAllText(editdata_file, LineaCompleta2.Split(",")[0] + "," + Seguridad.Encriptar(txb_p3_usuario.Texts) + "," + Seguridad.Encriptar(txb_p3_contraseña.Texts) + "," + "user-01" + "," + LineaCompleta2.Split(",")[4] + Environment.NewLine);
                                                break;
                                            case 2:
                                                File.AppendAllText(editdata_file, LineaCompleta2.Split(",")[0] + "," + Seguridad.Encriptar(txb_p3_usuario.Texts) + "," + Seguridad.Encriptar(txb_p3_contraseña.Texts) + "," + "user-02" + "," + LineaCompleta2.Split(",")[4] + Environment.NewLine);
                                                break;
                                            case 3:
                                                File.AppendAllText(editdata_file, LineaCompleta2.Split(",")[0] + "," + Seguridad.Encriptar(txb_p3_usuario.Texts) + "," + Seguridad.Encriptar(txb_p3_contraseña.Texts) + "," + "user-03" + "," + LineaCompleta2.Split(",")[4] + Environment.NewLine);
                                                break;
                                            case 4:
                                                File.AppendAllText(editdata_file, LineaCompleta2.Split(",")[0] + "," + Seguridad.Encriptar(txb_p3_usuario.Texts) + "," + Seguridad.Encriptar(txb_p3_contraseña.Texts) + "," + "user-04" + "," + LineaCompleta2.Split(",")[4] + Environment.NewLine);
                                                break;
                                        }
                                    }
                                    else if (rad_1 == rad_2 && steam64_1 != txb_p3_steam64.Texts)
                                    {
                                        switch (rad_2)
                                        {
                                            case 1:
                                                File.AppendAllText(editdata_file, LineaCompleta2.Split(",")[0] + "," + Seguridad.Encriptar(txb_p3_usuario.Texts) + "," + Seguridad.Encriptar(txb_p3_contraseña.Texts) + "," + LineaCompleta2.Split(",")[3] + "," + txb_p3_steam64.Texts + Environment.NewLine);
                                                trySteam64 = true;
                                                break;
                                            case 2:
                                                File.AppendAllText(editdata_file, LineaCompleta2.Split(",")[0] + "," + Seguridad.Encriptar(txb_p3_usuario.Texts) + "," + Seguridad.Encriptar(txb_p3_contraseña.Texts) + "," + LineaCompleta2.Split(",")[3] + "," + txb_p3_steam64.Texts + Environment.NewLine);
                                                trySteam64 = true;
                                                break;
                                            case 3:
                                                File.AppendAllText(editdata_file, LineaCompleta2.Split(",")[0] + "," + Seguridad.Encriptar(txb_p3_usuario.Texts) + "," + Seguridad.Encriptar(txb_p3_contraseña.Texts) + "," + LineaCompleta2.Split(",")[3] + "," + txb_p3_steam64.Texts + Environment.NewLine);
                                                trySteam64 = true;
                                                break;
                                            case 4:
                                                File.AppendAllText(editdata_file, LineaCompleta2.Split(",")[0] + "," + Seguridad.Encriptar(txb_p3_usuario.Texts) + "," + Seguridad.Encriptar(txb_p3_contraseña.Texts) + "," + LineaCompleta2.Split(",")[3] + "," + txb_p3_steam64.Texts + Environment.NewLine);
                                                trySteam64 = true;
                                                break;
                                        }
                                    }
                                    else if (rad_1 == rad_2 && steam64_1 == txb_p3_steam64.Texts)
                                    {
                                        File.AppendAllText(editdata_file, LineaCompleta2.Split(",")[0] + "," + Seguridad.Encriptar(txb_p3_usuario.Texts) + "," + Seguridad.Encriptar(txb_p3_contraseña.Texts) + "," + LineaCompleta2.Split(",")[3] + "," + LineaCompleta2.Split(",")[4] + Environment.NewLine);
                                    }

                                    if (imagenLocal2 != "")
                                    {
                                        try
                                        {
                                            using (Image image = Image.FromFile(imagenLocal2))
                                            {
                                                new Bitmap(image, 128, 128).Save(tempPath + @"\SLA Manager\imgs\" + lstEditarCuentas.Text.Remove(0, 2) + " Full.png");
                                            }

                                            using (Image image = Image.FromFile(imagenLocal2))
                                            {
                                                new Bitmap(image, 64, 64).Save(tempPath + @"\SLA Manager\imgs\" + lstEditarCuentas.Text.Remove(0, 2) + " Medium.png");
                                            }
                                        }
                                        catch
                                        {
                                            Debug.Print("Error al copiar la imagen del directorio...");
                                        }
                                    }

                                }
                            }

                            editado = true;
                        }
                    }
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("¿Do you want to modify this user?", "SLA Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        for (int i = 0; i < lines.Length; i++)
                        {
                            string LineaCompleta = lines[i];
                            TodosUsuarios = TodosUsuarios + Seguridad.DesEncriptar(LineaCompleta.Split(",")[1]) + ", ";
                        }

                        if (Regex.Matches(TodosUsuarios, txb_p3_usuario.Texts + ",").Count > 1)
                        {
                            this.Alert("Account previously\nregistered in the system", Form_Alert.enmType.Warning);
                        }
                        else
                        {
                            File.WriteAllText(editdata_file, "");

                            for (int j = 0; j < lines.Length; j++)
                            {
                                string LineaCompleta2 = lines[j];
                                if (!LineaCompleta2.Contains(NombreSelect))
                                {
                                    File.AppendAllText(editdata_file, LineaCompleta2 + Environment.NewLine);
                                }
                                else
                                {
                                    if (rad_1 != rad_2 && steam64_1 != txb_p3_steam64.Texts)
                                    {
                                        switch (rad_2)
                                        {
                                            case 1:
                                                File.AppendAllText(editdata_file, LineaCompleta2.Split(",")[0] + "," + Seguridad.Encriptar(txb_p3_usuario.Texts) + "," + Seguridad.Encriptar(txb_p3_contraseña.Texts) + "," + "user-01" + "," + txb_p3_steam64.Texts + Environment.NewLine);
                                                trySteam64 = true;
                                                break;
                                            case 2:
                                                File.AppendAllText(editdata_file, LineaCompleta2.Split(",")[0] + "," + Seguridad.Encriptar(txb_p3_usuario.Texts) + "," + Seguridad.Encriptar(txb_p3_contraseña.Texts) + "," + "user-02" + "," + txb_p3_steam64.Texts + Environment.NewLine);
                                                trySteam64 = true;
                                                break;
                                            case 3:
                                                File.AppendAllText(editdata_file, LineaCompleta2.Split(",")[0] + "," + Seguridad.Encriptar(txb_p3_usuario.Texts) + "," + Seguridad.Encriptar(txb_p3_contraseña.Texts) + "," + "user-03" + "," + txb_p3_steam64.Texts + Environment.NewLine);
                                                trySteam64 = true;
                                                break;
                                            case 4:
                                                File.AppendAllText(editdata_file, LineaCompleta2.Split(",")[0] + "," + Seguridad.Encriptar(txb_p3_usuario.Texts) + "," + Seguridad.Encriptar(txb_p3_contraseña.Texts) + "," + "user-04" + "," + txb_p3_steam64.Texts + Environment.NewLine);
                                                trySteam64 = true;
                                                break;
                                        }
                                    }
                                    else if (rad_1 != rad_2 && steam64_1 == txb_p3_steam64.Texts)
                                    {
                                        switch (rad_2)
                                        {
                                            case 1:
                                                File.AppendAllText(editdata_file, LineaCompleta2.Split(",")[0] + "," + Seguridad.Encriptar(txb_p3_usuario.Texts) + "," + Seguridad.Encriptar(txb_p3_contraseña.Texts) + "," + "user-01" + "," + LineaCompleta2.Split(",")[4] + Environment.NewLine);
                                                break;
                                            case 2:
                                                File.AppendAllText(editdata_file, LineaCompleta2.Split(",")[0] + "," + Seguridad.Encriptar(txb_p3_usuario.Texts) + "," + Seguridad.Encriptar(txb_p3_contraseña.Texts) + "," + "user-02" + "," + LineaCompleta2.Split(",")[4] + Environment.NewLine);
                                                break;
                                            case 3:
                                                File.AppendAllText(editdata_file, LineaCompleta2.Split(",")[0] + "," + Seguridad.Encriptar(txb_p3_usuario.Texts) + "," + Seguridad.Encriptar(txb_p3_contraseña.Texts) + "," + "user-03" + "," + LineaCompleta2.Split(",")[4] + Environment.NewLine);
                                                break;
                                            case 4:
                                                File.AppendAllText(editdata_file, LineaCompleta2.Split(",")[0] + "," + Seguridad.Encriptar(txb_p3_usuario.Texts) + "," + Seguridad.Encriptar(txb_p3_contraseña.Texts) + "," + "user-04" + "," + LineaCompleta2.Split(",")[4] + Environment.NewLine);
                                                break;
                                        }
                                    }
                                    else if (rad_1 == rad_2 && steam64_1 != txb_p3_steam64.Texts)
                                    {
                                        switch (rad_2)
                                        {
                                            case 1:
                                                File.AppendAllText(editdata_file, LineaCompleta2.Split(",")[0] + "," + Seguridad.Encriptar(txb_p3_usuario.Texts) + "," + Seguridad.Encriptar(txb_p3_contraseña.Texts) + "," + LineaCompleta2.Split(",")[3] + "," + txb_p3_steam64.Texts + Environment.NewLine);
                                                trySteam64 = true;
                                                break;
                                            case 2:
                                                File.AppendAllText(editdata_file, LineaCompleta2.Split(",")[0] + "," + Seguridad.Encriptar(txb_p3_usuario.Texts) + "," + Seguridad.Encriptar(txb_p3_contraseña.Texts) + "," + LineaCompleta2.Split(",")[3] + "," + txb_p3_steam64.Texts + Environment.NewLine);
                                                trySteam64 = true;
                                                break;
                                            case 3:
                                                File.AppendAllText(editdata_file, LineaCompleta2.Split(",")[0] + "," + Seguridad.Encriptar(txb_p3_usuario.Texts) + "," + Seguridad.Encriptar(txb_p3_contraseña.Texts) + "," + LineaCompleta2.Split(",")[3] + "," + txb_p3_steam64.Texts + Environment.NewLine);
                                                trySteam64 = true;
                                                break;
                                            case 4:
                                                File.AppendAllText(editdata_file, LineaCompleta2.Split(",")[0] + "," + Seguridad.Encriptar(txb_p3_usuario.Texts) + "," + Seguridad.Encriptar(txb_p3_contraseña.Texts) + "," + LineaCompleta2.Split(",")[3] + "," + txb_p3_steam64.Texts + Environment.NewLine);
                                                trySteam64 = true;
                                                break;
                                        }
                                    }
                                    else if (rad_1 == rad_2 && steam64_1 == txb_p3_steam64.Texts)
                                    {
                                        File.AppendAllText(editdata_file, LineaCompleta2.Split(",")[0] + "," + Seguridad.Encriptar(txb_p3_usuario.Texts) + "," + Seguridad.Encriptar(txb_p3_contraseña.Texts) + "," + LineaCompleta2.Split(",")[3] + "," + LineaCompleta2.Split(",")[4] + Environment.NewLine);
                                    }

                                    if (imagenLocal2 != "")
                                    {
                                        try
                                        {
                                            using (Image image = Image.FromFile(imagenLocal2))
                                            {
                                                new Bitmap(image, 128, 128).Save(tempPath + @"\SLA Manager\imgs\" + lstEditarCuentas.Text.Remove(0, 2) + " Full.png");
                                            }

                                            using (Image image = Image.FromFile(imagenLocal2))
                                            {
                                                new Bitmap(image, 64, 64).Save(tempPath + @"\SLA Manager\imgs\" + lstEditarCuentas.Text.Remove(0, 2) + " Medium.png");
                                            }
                                        }
                                        catch
                                        {
                                            Debug.Print("Error al copiar la imagen del directorio...");
                                        }
                                    }

                                }
                            }

                            editado = true;
                        }
                    }
                }

                if (editado)
                {
                    if (trySteam64)
                    {
                        string picMedium = "";
                        string picFull = "";

                        try
                        {
                            XmlDocument doc = new XmlDocument();
                            doc.Load("https://steamcommunity.com/profiles/" + txb_p3_steam64.Texts + "/?xml=1");

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

                                picMedium = dictSettings.ContainsKey("AvatarMedium") ? dictSettings["AvatarMedium"] : "";
                                picFull = dictSettings.ContainsKey("AvatarFull") ? dictSettings["AvatarFull"] : "";
                            }

                            Verificar();

                            using (WebClient webClient = new WebClient())
                            {
                                byte[] dataArr2 = webClient.DownloadData(picMedium);
                                File.WriteAllBytes(tempPath + @"\SLA Manager\imgs\" + NombreSelect + " Medium" + ".png", dataArr2);
                            }

                            using (WebClient webClient = new WebClient())
                            {
                                byte[] dataArr3 = webClient.DownloadData(picFull);
                                File.WriteAllBytes(tempPath + @"\SLA Manager\imgs\" + NombreSelect + " Full" + ".png", dataArr3);
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.Print(ex.Message + " ERROR!");
                            ErrorInternet();
                        }
                    }
                    if (Preferencias.GetConfig().val_lang == "español")
                    {
                        this.Alert("Cuenta editada\ncorrectamente!", Form_Alert.enmType.Success);
                    }
                    else
                    {
                        this.Alert("Account successfully\nedited", Form_Alert.enmType.Success);
                    }
                    initEditarCuentas();
                }
            }
        }

        private void rad_p3_img1_CheckedChanged(object sender, EventArgs e)
        {
            rad_p3_img1.Checked = true;
            rad_p3_img2.Checked = false;
            rad_p3_img3.Checked = false;
            rad_p3_img4.Checked = false;
            pic_p3_user.BackgroundImage = Properties.Resources.user01normal;
        }

        private void rad_p3_img2_CheckedChanged(object sender, EventArgs e)
        {
            rad_p3_img1.Checked = false;
            rad_p3_img2.Checked = true;
            rad_p3_img3.Checked = false;
            rad_p3_img4.Checked = false;
            pic_p3_user.BackgroundImage = Properties.Resources.user02normal;
        }

        private void rad_p3_img3_CheckedChanged(object sender, EventArgs e)
        {
            string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string fileName = tempPath + @"\SLA Manager\data\steam_data.txt";
            rad_p3_img1.Checked = false;
            rad_p3_img2.Checked = false;
            rad_p3_img3.Checked = true;
            rad_p3_img4.Checked = false;
            try{
                string[] lines = File.ReadAllLines(fileName);

                for (int i = 0; i < lines.Length; i++)
                {
                    string LineaCompleta = lines[i];
                    string[] LineaCompletaArray = LineaCompleta.Split(',');

                    if (LineaCompletaArray[1] == Seguridad.Encriptar(txb_p3_usuario.Texts))
                    {
                        FileStream fs = new System.IO.FileStream(tempPath + @"\SLA Manager\imgs\" + LineaCompletaArray[0] + " Full" + ".png", FileMode.Open, FileAccess.Read);
                        pic_p3_user.BackgroundImage = Image.FromStream(fs);
                        fs.Close();
                    }
                }
                
            } catch
            {
                pic_p3_user.BackgroundImage = Properties.Resources.user03normal;
            }
        }

        private void rad_p3_img4_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                OpenFileDialog open = new OpenFileDialog();

                open.Filter = "Archivos de Imagen (*.png; *.jpg; *.jpeg; *.bmp)|*.png; *.jpg; *.jpeg; *.bmp";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = new System.IO.FileStream(open.FileName, FileMode.Open, FileAccess.Read);
                    pic_p3_user.BackgroundImage = Image.FromStream(fs);
                    Bitmap bmp = new Bitmap((Stream)fs);
                    fs.Close();

                    imagenLocal2 = open.FileName;

                    rad_p3_img1.Checked = false;
                    rad_p3_img2.Checked = false;
                    rad_p3_img3.Checked = false;
                    rad_p3_img4.Checked = true;
                }
                else
                {
                    if (rad_p3_img1.Checked == true)
                    {
                        rad_p3_img1.Checked = true;
                        rad_p3_img4.Checked = false;
                    }
                    else if (rad_p3_img2.Checked == true)
                    {
                        rad_p3_img2.Checked = true;
                        rad_p3_img4.Checked = false;
                    }
                    else if (rad_p3_img3.Checked == true)
                    {
                        rad_p3_img3.Checked = true;
                        rad_p3_img4.Checked = false;
                    }
                }
            } catch
            {
                Debug.Print("Error al seleccionar una imagen...");
            }
        }

        private void btn_p3_ayuda_Click(object sender, EventArgs e)
        {
            this.Alert("", Form_Alert.enmType.SteamID);
        }

        private void pic_p3_MouseEnter(object sender, EventArgs e)
        {
            PictureBox btn = (PictureBox)sender;
            if (btn != null)
            {
                if (btn.Name.Contains("refresh"))
                {
                    btn.Image = new Bitmap(Resources.update_image2);
                } else if (btn.Name.Contains("newimage"))
                {
                    btn.Image = new Bitmap(Resources.add_image2);
                }
            }
        }

        private void pic_p3_MouseLeave(object sender, EventArgs e)
        {
            PictureBox btn = (PictureBox)sender;
            if (btn != null)
            {
                if (btn.Name.Contains("refresh"))
                {
                    btn.Image = new Bitmap(Resources.update_image1);
                }
                else if (btn.Name.Contains("newimage"))
                {
                    btn.Image = new Bitmap(Resources.add_image1);
                }
            }
        }

    }
}
