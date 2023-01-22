using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLA_Manager
{
    static class idioma
    {
        public static Dictionary<string, string> dataPanel0 = new Dictionary<string, string>();
        public static Dictionary<string, string> dataPanel2 = new Dictionary<string, string>();
        public static Dictionary<string, string> dataPanel3 = new Dictionary<string, string>();
        public static Dictionary<string, string> dataPanel4 = new Dictionary<string, string>();
        public static Dictionary<string, string> dataPanel5 = new Dictionary<string, string>();

        private static void cargarIdioma(string archivo)
        {
            dataPanel0.Clear();
            dataPanel2.Clear();
            dataPanel3.Clear();
            dataPanel4.Clear();
            dataPanel5.Clear();
            int panel = 0;

            string esp = Properties.Resources.español;
            IEnumerable<string> lines_esp = esp.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            string eng = Properties.Resources.english;
            IEnumerable<string> lines_eng = eng.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();

            if (Preferencias.GetConfig().val_lang == "español")
            {
                foreach (string linea in lines_esp)
                {
                    if (linea.Contains("="))
                    {
                        switch (panel)
                        {
                            case 1:
                                string[] s1 = linea.Split(new char[] { '=' });
                                dataPanel0.Add(s1[0], s1[1]);
                                break;
                            case 2:
                                string[] s2 = linea.Split(new char[] { '=' });
                                dataPanel2.Add(s2[0], s2[1]);
                                break;
                            case 3:
                                string[] s3 = linea.Split(new char[] { '=' });
                                dataPanel3.Add(s3[0], s3[1]);
                                break;
                            case 4:
                                string[] s4 = linea.Split(new char[] { '=' });
                                dataPanel4.Add(s4[0], s4[1]);
                                break;
                            case 5:
                                string[] s5 = linea.Split(new char[] { '=' });
                                dataPanel5.Add(s5[0], s5[1]);
                                break;
                        }
                    }
                    else
                    {
                        panel++;
                    }
                }
            } else
            {
                foreach (string linea in lines_eng)
                {
                    if (linea.Contains("="))
                    {
                        switch (panel)
                        {
                            case 1:
                                string[] s1 = linea.Split(new char[] { '=' });
                                dataPanel0.Add(s1[0], s1[1]);
                                break;
                            case 2:
                                string[] s2 = linea.Split(new char[] { '=' });
                                dataPanel2.Add(s2[0], s2[1]);
                                break;
                            case 3:
                                string[] s3 = linea.Split(new char[] { '=' });
                                dataPanel3.Add(s3[0], s3[1]);
                                break;
                            case 4:
                                string[] s4 = linea.Split(new char[] { '=' });
                                dataPanel4.Add(s4[0], s4[1]);
                                break;
                            case 5:
                                string[] s5 = linea.Split(new char[] { '=' });
                                dataPanel5.Add(s5[0], s5[1]);
                                break;
                        }
                    }
                    else
                    {
                        panel++;
                    }
                }
            }
        }

        public static void cambiarIdioma(string archivo)
        {
            Preferencias.SetConfig("lang", archivo);

            cargarIdioma(archivo);
        }

        public static void controles(Form p0, Panel p2, Panel p3, Panel p4, Panel p5)
        {
            foreach (string control in dataPanel0.Keys)
            {
                try
                {
                    p0.Controls[control].Text = dataPanel0[control];
                }
                catch (Exception e)
                {
                    Debug.Print(e.Message);
                }
            }
            foreach (string control in dataPanel2.Keys)
            {
                try
                {
                    p2.Controls[control].Text = dataPanel2[control];
                }
                catch (Exception e)
                {
                    Debug.Print(e.Message);
                }
            }
            foreach (string control in dataPanel3.Keys)
            {
                try
                {
                    p3.Controls[control].Text = dataPanel3[control];
                }
                catch (Exception e)
                {
                    Debug.Print(e.Message);
                }
            }
            foreach (string control in dataPanel4.Keys)
            {
                try
                {
                    p4.Controls[control].Text = dataPanel4[control];
                }
                catch (Exception e)
                {
                    Debug.Print(e.Message);
                }
            }
            foreach (string control in dataPanel5.Keys)
            {
                try
                {
                    p5.Controls[control].Text = dataPanel5[control];
                }
                catch (Exception e)
                {
                    Debug.Print(e.Message);
                }
            }
        }
    }
}
