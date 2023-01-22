
using CustomAlertBoxDemo;
using CustomControls.RJControls;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.LinkLabel;

namespace SLA_Manager
{
    public partial class Form_Main
    {
        string appName = Process.GetCurrentProcess().ProcessName;
        RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        public void initAjustes()
        {
            Preferencias.GetConfig();

            if (Preferencias.GetConfig().val_lang == "español")
            {
                cbox_p5_idioma.SelectedText = "Español";
                pic_p5_bandera.BackgroundImage = Properties.Resources.mexico_24px;
            }
            else {
                cbox_p5_idioma.SelectedText = "English";
                pic_p5_bandera.BackgroundImage = Properties.Resources.usa_24px;
            }

            if (Preferencias.GetConfig().val_IniciarWindows == true)
            {
                chb_p5_iniciar.Checked = true;
            }
            else { chb_p5_iniciar.Checked = false; }

            if (Preferencias.GetConfig().val_IconoSeguimiento == true)
            {
                chb_p5_icono.Checked = true;
            }
            else { chb_p5_icono.Checked = false; }

            if (Preferencias.GetConfig().val_MinimizarInicio == true)
            {
                chb_p5_minimizarAlIniciar.Checked = true;
            }
            else { chb_p5_minimizarAlIniciar.Checked = false; }

            if (Preferencias.GetConfig().val_MinimizarProgramas == true)
            {
                chb_p5_minimizarConPrograma.Checked = true;
            }
            else { chb_p5_minimizarConPrograma.Checked = false; }

            if (Preferencias.GetConfig().val_PreguntarInicio == true)
            {
                chb_p5_preguntar_iniciar.Checked = true;
            }
            else { chb_p5_preguntar_iniciar.Checked = false; }

            if (Preferencias.GetConfig().val_WinAuthPath != "")
            {
                pic_p5_delete1.Visible = true;
                txb_p5_dir_winauth.Texts = Preferencias.GetConfig().val_WinAuthPath;
            }
            else { 
                pic_p5_delete1.Visible = false;
                txb_p5_dir_winauth.Texts = Preferencias.GetConfig().val_WinAuthPath;
            }

            if (Preferencias.GetConfig().val_SteamPath != "")
            {
                pic_p5_delete2.Visible = true;
                txb_p5_dir_steam.Texts = Preferencias.GetConfig().val_SteamPath;
            }
            else
            {
                pic_p5_delete2.Visible = false;
                txb_p5_dir_steam.Texts = Preferencias.GetConfig().val_SteamPath;
            }

            if (Preferencias.GetConfig().val_OcultarUsuario == true)
            {
                chb_p5_ocultar_user.Checked = true;
            }
            else { chb_p5_ocultar_user.Checked = false; }
        }
        
        private void cbox_p5_idioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbox_p5_idioma.SelectedItem.ToString() == "Español")
            {
                pic_p5_bandera.BackgroundImage = Properties.Resources.mexico_24px;
                Preferencias.SetConfig("lang", "español");
                idioma.cambiarIdioma("español");
                actualizarIdioma();
            } else
            {
                pic_p5_bandera.BackgroundImage = Properties.Resources.usa_24px;
                Preferencias.SetConfig("lang", "english");
                idioma.cambiarIdioma("english");
                actualizarIdioma();
            }
        }

        private void chb_p5_iniciar_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_p5_iniciar.Checked == true)
            {
                if (Preferencias.GetConfig().val_MinimizarInicio == true)
                {
                    RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    key.SetValue("SLA Manager", Application.ExecutablePath + " --start-minimized");
                    Preferencias.SetConfig("IniciarWindows", "true");
                } else
                {
                    RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    key.SetValue("SLA Manager", Application.ExecutablePath);
                    Preferencias.SetConfig("IniciarWindows", "true");
                }
            }
            else
            {
                if (chb_p5_minimizarAlIniciar.Checked == true)
                {
                    chb_p5_minimizarAlIniciar.Checked = false;
                }

                RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                key.DeleteValue("SLA Manager", false);
                Preferencias.SetConfig("IniciarWindows", "false");
            }
        }

        private void chb_p5_icono_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_p5_icono.Checked == true)
            {
                Preferencias.SetConfig("IconoSeguimiento", "true");
                Preferencias.SetConfig("MensajeMinimizar", "false");
            }
            else
            {
                Preferencias.SetConfig("IconoSeguimiento", "false");
            }
        }

        private void chb_p5_minimizarAlIniciar_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_p5_minimizarAlIniciar.Checked == true)
            {
                if (chb_p5_iniciar.Checked != true)
                {
                    chb_p5_iniciar.Checked = true;
                }

                RegistryKey key2 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                key2.SetValue("SLA Manager", Application.ExecutablePath + " --start-minimized");
                Preferencias.SetConfig("MinimizarInicio", "true");
            }
            else
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                key.SetValue("SLA Manager", Application.ExecutablePath);
                Preferencias.SetConfig("MinimizarInicio", "false");
            }
        }

        private void chb_p5_minimizarConPrograma_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_p5_minimizarConPrograma.Checked == true)
            {
                Preferencias.SetConfig("MinimizarProgramas", "true");
            }
            else
            {
                Preferencias.SetConfig("MinimizarProgramas", "false");
            }
        }

        private void chb_p5_preguntar_iniciar_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_p5_preguntar_iniciar.Checked == true)
            {
                Preferencias.SetConfig("PreguntarInicio", "true");
            }
            else
            {
                Preferencias.SetConfig("PreguntarInicio", "false");
            }
        }

        private void chb_p5_ocultar_user_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_p5_ocultar_user.Checked == true)
            {
                Preferencias.SetConfig("OcultarUser", "true");
            }
            else
            {
                Preferencias.SetConfig("OcultarUser", "false");
            }
        }

        private void pic_p5_bandera_Click(object sender, EventArgs e)
        {
            cbox_p5_idioma.Focus();
            cbox_p5_idioma.DroppedDown = true;
        }

        private void txb_p5_dir_winauth_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.Filter = "Archivos Ejecutables (*.exe)|*.exe";
            if (open.ShowDialog() == DialogResult.OK)
            {
                txb_p5_dir_winauth.Texts = open.FileName;
                pic_p5_delete1.Visible = true;

                Preferencias.SetConfig("WinAuthPath", open.FileName);
            } else {
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

        private void txb_p5_dir_steam_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.Filter = "Archivos Ejecutables (*.exe)|*.exe";
            if (open.ShowDialog() == DialogResult.OK)
            {
                txb_p5_dir_steam.Texts = open.FileName;
                pic_p5_delete2.Visible = true;

                Preferencias.SetConfig("SteamPath", open.FileName);
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

        private void pic_p5_delete1_Click(object sender, EventArgs e)
        {
            txb_p5_dir_winauth.Texts = "";
            pic_p5_delete1.Visible = false;

            Preferencias.SetConfig("WinAuthPath", "");
        }

        private void pic_p5_delete2_Click(object sender, EventArgs e)
        {
            txb_p5_dir_steam.Texts = "";
            pic_p5_delete2.Visible = false;

            Preferencias.SetConfig("SteamPath", "");
        }

        private void btn_p5_reiniciar_Click(object sender, EventArgs e)
        {
            if (Preferencias.GetConfig().val_lang == "español")
            {
                DialogResult dialogResult = MessageBox.Show("¿Esta seguro en reiniciar todos los ajustes?", "SLA Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    Preferencias.resetSettings();
                    
                    if (Preferencias.GetConfig().val_lang == "español")
                    {
                        cbox_p5_idioma.Text = "Español";
                    }
                    else
                    {
                        cbox_p5_idioma.Text = "English";
                    }

                    initAjustes();

                    this.Alert("Los ajustes se\nhan reiniciado!", Form_Alert.enmType.Success);

                    this.Hide();
                    var form_pass = new Form_Password("Nueva");
                    form_pass.Closed += (s, args) => this.Close();
                    form_pass.Show();
                }
            } else
            {
                DialogResult dialogResult = MessageBox.Show("¿Are you sure about resetting all settings?", "SLA Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    Preferencias.resetSettings();

                    if (Preferencias.GetConfig().val_lang == "español")
                    {
                        cbox_p5_idioma.Text = "Español";
                    }
                    else
                    {
                        cbox_p5_idioma.Text = "English";
                    }

                    initAjustes();

                    this.Alert("The settings have been reset!", Form_Alert.enmType.Success);

                    this.Hide();
                    var form_pass = new Form_Password("Nueva");
                    form_pass.Closed += (s, args) => this.Close();
                    form_pass.Show();
                }
            }
        }
    }
}
