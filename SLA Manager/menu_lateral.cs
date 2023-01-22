
using CustomControls.RJControls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLA_Manager
{
    public partial class Form_Main
    {
        private void borrarCuentas()
        {
            panel_Menu1.Controls.Clear();
        }

        private void Menu1_Cuentas_Click(object sender, EventArgs e)
        {
            Verificar();
            Preferencias.GetConfig();
            if (panel_Menu1.Visible == false)
            {
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
            }

        }

        private void Menu2_Agregar_Click(object sender, EventArgs e)
        {
            Verificar();
            Preferencias.GetConfig();
            if (panel_Menu2.Visible == false)
            {
                pic_p0_steam.Visible = false;
                pic_p0_winauth.Visible = false;
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

                //RESET DE PANTALLA
                pic_p2_main.BackgroundImage = Properties.Resources.user01normal;
                rad_p2_user1.Checked = true;
                rad_p2_user2.Checked = false;
                rad_p2_user3.Checked = false;
                rad_p2_user4.Checked = false;
                lbl_p2_chbSelected.Text = "user-01";
                lbl_p2_steam64.Visible = false;
                txb_p2_steam64.Visible = false;
                pic_p2_ayuda.Visible = false;
                txb_p2_nombrelocal.Texts = "";
                txb_p2_usuario.Texts = "";
                txb_p2_contraseña.Texts = "";
                txb_p2_steam64.Texts = "";
                p2_contraseña = true;
                pic_p2_visible.BackgroundImage = Properties.Resources.visible;
                txb_p2_contraseña.PasswordChar = true;
            }
        }
        private void Menu3_Editar_Click(object sender, EventArgs e)
        {
            Verificar();
            Preferencias.GetConfig();
            if (panel_Menu3.Visible == false)
            {
                lstEditarCuentas.Items.Clear();
                initEditarCuentas();
                pic_p0_steam.Visible = false;
                pic_p0_winauth.Visible = false;
                panel_Menu1.Visible = false;
                panel_Menu2.Visible = false;
                panel_Menu3.Visible = true;
                panel_Menu4.Visible = false;
                panel_Menu5.Visible = false;
                lbl_p1_main.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p2_main.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p3_main.BackColor = Color.FromArgb(0, 174, 217);
                lbl_p4_main.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p5_main.BackColor = Color.FromArgb(21, 22, 25);

                lbl_p1_ico.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p2_ico.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p3_ico.BackColor = Color.FromArgb(0, 174, 217);
                lbl_p4_ico.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p5_ico.BackColor = Color.FromArgb(21, 22, 25);
                
                lbl_p1_title.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p2_title.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p3_title.BackColor = Color.FromArgb(0, 174, 217);
                lbl_p4_title.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p5_title.BackColor = Color.FromArgb(21, 22, 25);
            }
            
        }

        private void Menu4_Eliminar_Click(object sender, EventArgs e)
        {
            Verificar();
            Preferencias.GetConfig();
            if (panel_Menu4.Visible == false)
            {
                borrarCuentas();
                lstBorrarCuentas.Items.Clear();
                initBorrarCuentas();
                pic_p0_steam.Visible = false;
                pic_p0_winauth.Visible = false;
                panel_Menu1.Visible = false;
                panel_Menu2.Visible = false;
                panel_Menu3.Visible = false;
                panel_Menu4.Visible = true;
                panel_Menu5.Visible = false;
                lbl_p1_main.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p2_main.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p3_main.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p4_main.BackColor = Color.FromArgb(0, 174, 217);
                lbl_p5_main.BackColor = Color.FromArgb(21, 22, 25);

                lbl_p1_ico.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p2_ico.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p3_ico.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p4_ico.BackColor = Color.FromArgb(0, 174, 217);
                lbl_p5_ico.BackColor = Color.FromArgb(21, 22, 25);

                lbl_p1_title.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p2_title.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p3_title.BackColor = Color.FromArgb(21, 22, 25);
                lbl_p4_title.BackColor = Color.FromArgb(0, 174, 217);
                lbl_p5_title.BackColor = Color.FromArgb(21, 22, 25);

                //RESET DE PANTALLA
                pic_p4_user.BackgroundImage = Properties.Resources.user01normal;
                txb_p4_usuario.Texts = "";
                txb_p4_contraseña.Texts = "";
                p4_contraseña = true;
                txb_p4_usuario.Enabled = false;
                txb_p4_contraseña.Enabled = false;
                pic_p4_visible.Enabled = false;
                btn_p4_delete.Enabled = false;
                pic_p4_visible.BackgroundImage = Properties.Resources.visible;
            }
        }
        private void Menu5_Ajustes_Click(object sender, EventArgs e)
        {
            Verificar();
            Preferencias.GetConfig();
            cbox_p5_idioma.Items.Clear();
            cbox_p5_idioma.Items.Add("Español");
            cbox_p5_idioma.Items.Add("English");

            if (Preferencias.GetConfig().val_lang == "español")
            {
                cbox_p5_idioma.Text = "Español";
            }
            else
            {
                cbox_p5_idioma.Text = "English";
            }

            pic_p0_steam.Visible = false;
            pic_p0_winauth.Visible = false;
            panel_Menu1.Visible = false;
            panel_Menu2.Visible = false;
            panel_Menu3.Visible = false;
            panel_Menu4.Visible = false;
            panel_Menu5.Visible = true;
            lbl_p1_main.BackColor = Color.FromArgb(21, 22, 25);
            lbl_p2_main.BackColor = Color.FromArgb(21, 22, 25);
            lbl_p3_main.BackColor = Color.FromArgb(21, 22, 25);
            lbl_p4_main.BackColor = Color.FromArgb(21, 22, 25);
            lbl_p5_main.BackColor = Color.FromArgb(0, 174, 217); 

            lbl_p1_ico.BackColor = Color.FromArgb(21, 22, 25);
            lbl_p2_ico.BackColor = Color.FromArgb(21, 22, 25); 
            lbl_p3_ico.BackColor = Color.FromArgb(21, 22, 25);
            lbl_p4_ico.BackColor = Color.FromArgb(21, 22, 25);
            lbl_p5_ico.BackColor = Color.FromArgb(0, 174, 217);

            lbl_p1_title.BackColor = Color.FromArgb(21, 22, 25);
            lbl_p2_title.BackColor = Color.FromArgb(21, 22, 25); 
            lbl_p3_title.BackColor = Color.FromArgb(21, 22, 25);
            lbl_p4_title.BackColor = Color.FromArgb(21, 22, 25);
            lbl_p5_title.BackColor = Color.FromArgb(0, 174, 217);

            initAjustes();
        }
    }
}
