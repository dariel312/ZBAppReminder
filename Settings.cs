using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AppointmentReminder
{
    public class Settings
    {
        public static Settings Instance;
        public static string DIR = AppDomain.CurrentDomain.BaseDirectory + "Config.xml";

        public string AccountSID;
        public string AuthToken;
        public string TwilioPhone;
        public string DataBasePath;
        public byte TimeOfDay;

        public static bool LoadSettings()
        {
            bool success = true;
            if (File.Exists(DIR))
            {
                try
                {
                    using (FileStream str = File.OpenRead(DIR))
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
                catch(Exception ex)
                {
                    success = false;
                }
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
            if (File.Exists(DIR))
                File.Delete(DIR);

            Settings defSettings = new Settings()
            {
                AccountSID = "ACcd26dab9ceaed04e5a820bf5e99129ee",
                AuthToken = "a90d919fb94d053692f86da12d1759fb",
                TwilioPhone = "3059026826",
                DataBasePath = @"C:\FULLPATH\ApptDB.mdb",
                TimeOfDay = 13
            };

            using (FileStream file = File.Create(DIR))
            {
                XmlSerializer xSer = new XmlSerializer(typeof(Settings));
                xSer.Serialize(file, defSettings);
            }

            return defSettings;
        }
    }
}
