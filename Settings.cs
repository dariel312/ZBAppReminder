using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Threading.Tasks;

namespace AppointmentReminder
{
    public class Settings
    {
        public static Settings Instance;

        public string AccountSID;
        public string AuthToken;
        public string TwilioPhone;
        public string DataBasePath;
        public DateTime TimeOfDay;

        public static bool LoadSettings()
        {
            bool success = true;
            if (File.Exists("Config.xml"))
            {
                try
                {
                    using (FileStream str = File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "Config.xml"))
                    {
                        XmlSerializer xSer = new XmlSerializer(typeof(Settings));
                        Instance = (Settings)xSer.Deserialize(str);
                    }

                    if (Instance.AccountSID == "")
                        success = false;
                    if (Instance.AuthToken == "")
                        success = false;
                    if (Instance.TwilioPhone == "")
                        success = false;
                    if (Instance.DataBasePath == "")
                        success = false;
                }
                catch { success = false; }
            }
            else 
            {
                WriteDefaultSettings();
                success = false;
            }

            return success;
        }
        public static Settings WriteDefaultSettings()
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Config.xml"))
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Config.xml");

            Settings defSettings = new Settings()
            {
                AccountSID = "",
                AuthToken = "",
                TwilioPhone = "",
                DataBasePath = "ApptDB.mdb",
                TimeOfDay = new DateTime(2016, 1 ,1, 4, 0, 0)
            };

            using (FileStream file = File.Create(AppDomain.CurrentDomain.BaseDirectory + "Config.xml"))
            {
                XmlSerializer xSer = new XmlSerializer(typeof(Settings));
                xSer.Serialize(file, defSettings);
            }

            return defSettings;
        }
    }
}
