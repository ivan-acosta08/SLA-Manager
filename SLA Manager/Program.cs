using System.Diagnostics;

namespace SLA_Manager
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

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
            string settingsPath = tempPath + @"\SLA Manager\data\settings.txt";

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

            string[] lines = File.ReadAllLines(settingsPath);

            Preferencias.GetConfig();

            ApplicationConfiguration.Initialize();

            try
            {
                if (lines[0].Contains("español")){
                    idioma.cambiarIdioma("español");
                } else {
                    idioma.cambiarIdioma("english");
                }
                
                if (lines[8].Split(",")[1] != "")
                {
                    if (lines[3].Split(",")[1] == "true")
                    {
                        if (args.Length != 0){ 
                            Application.Run(new Form_Main("Minimizado"));
                        } else {
                            Application.Run(new Form_Main("Normal"));
                        } 
                    } else
                    {
                        Application.Run(new Form_Main("Normal"));
                    }
                } else {
                    Application.Run(new Form_Password("Nueva"));
                }

            } catch
            {
                idioma.cambiarIdioma("español");
                Application.Run(new Form_Password("Nueva"));
            }

        }

    }
}