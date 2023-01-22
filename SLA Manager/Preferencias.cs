using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SLA_Manager
{
    public class Preferencias
    {
        private string lang = "español";
        private bool IniciarWindows = true;
        private bool IconoSeguimiento = false;
        private bool MinimizarInicio = false;
        private bool MinimizarProgramas = false;
        private bool PreguntarInicio = true;
        private string WinAuthPath = "";
        private string SteamPath = "";
        private string Contraseña = "";
        private bool OcultarUsuario = false;
        private bool mensajeMinimizar = false;

        public string val_lang { get { return lang; } }
        public bool val_IniciarWindows { get { return IniciarWindows; } }
        public bool val_IconoSeguimiento { get { return IconoSeguimiento; } }
        public bool val_MinimizarInicio { get { return MinimizarInicio; } }
        public bool val_MinimizarProgramas { get { return MinimizarProgramas; } }
        public bool val_PreguntarInicio { get { return PreguntarInicio; } }
        public string val_WinAuthPath { get { return WinAuthPath; } }
        public string val_SteamPath { get { return SteamPath; } }
        public string val_Contraseña { get { return Contraseña; } }
        public bool val_OcultarUsuario { get { return OcultarUsuario; } }
        public bool val_mensajeMinimizar { get { return mensajeMinimizar; } }

        #region cnstr

        public Preferencias()
        {
            LoadValuesFromConfigurationTXT();
        }

        #endregion

        public static Preferencias GetConfig()
        {
            return new Preferencias();
        }

        internal void LoadValuesFromConfigurationTXT()
        {
            string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string fileName = tempPath + @"\SLA Manager\data\settings.txt";

            string[] lines = File.ReadAllLines(fileName);

            for(int i = 0; i < lines.Length; i++){
                switch (i)
                {
                    case 0:
                        lang = lines[i].Split(",")[1];
                        break;
                    case 1:
                        if (lines[i].Contains("true")){
                            IniciarWindows = true;
                        } else {
                            IniciarWindows = false;
                        }
                        break;
                    case 2:
                        if (lines[i].Contains("true")){
                            IconoSeguimiento = true;
                        } else {
                            IconoSeguimiento = false;
                        }
                        break;
                    case 3:
                        if (lines[i].Contains("true")){
                            MinimizarInicio = true;
                        } else {
                            MinimizarInicio = false;
                        }
                        break;
                    case 4:
                        if (lines[i].Contains("true")){
                            MinimizarProgramas = true;
                        } else {
                            MinimizarProgramas = false;
                        }
                        break;
                    case 5:
                        if (lines[i].Contains("true")){
                            PreguntarInicio = true;
                        } else {
                            PreguntarInicio = false;
                        }
                        break;
                    case 6:
                        WinAuthPath = lines[i].Split(",")[1];
                        break;
                    case 7:
                        SteamPath = lines[i].Split(",")[1];
                        break;
                    case 8:
                        Contraseña = lines[i].Split(",")[1];
                        break;
                    case 9:
                        if (lines[i].Contains("true")){
                            OcultarUsuario = true;
                        } else {
                            OcultarUsuario = false;
                        }
                        break;
                    case 10:
                        if (lines[i].Contains("true")){
                            mensajeMinimizar = true;
                        } else {
                            mensajeMinimizar = false;
                        }
                        break;
                }
            }
        }

        public static Preferencias resetSettings()
        {
            string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string settingsPath = tempPath + @"\SLA Manager\data\settings.txt";

            if (!File.Exists(tempPath + @"\SLA Manager\data\settings.txt"))
                File.Create(tempPath + @"\SLA Manager\data\settings.txt").Dispose();

            File.WriteAllText(settingsPath, "");
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
            return new Preferencias();
        }

        public static void SetConfig(string key, string value)
        {
            string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string settingsPath = tempPath + @"\SLA Manager\data\settings.txt";

            if (File.Exists(tempPath + @"\SLA Manager\data\settings.txt"))
            {
                string[] lines = File.ReadAllLines(settingsPath);
                File.WriteAllText(settingsPath, "");
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains(key))
                    {
                        File.AppendAllText(settingsPath, key + "," + value + Environment.NewLine);
                    } else
                    {
                        File.AppendAllText(settingsPath, lines[i].Split(",")[0] + "," + lines[i].Split(",")[1] + Environment.NewLine);
                    }
                }
            }
            else
            {
                resetSettings();
                string[] lines = File.ReadAllLines(settingsPath);
                File.WriteAllText(settingsPath, "");
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains(key))
                    {
                        File.AppendAllText(settingsPath, key + "," + value + Environment.NewLine);
                    }
                    else
                    {
                        File.AppendAllText(settingsPath, lines[i].Split(",")[0] + "," + lines[i].Split(",")[1] + Environment.NewLine);
                    }
                }
            }
        }
    }
}
