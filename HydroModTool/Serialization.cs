using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Controls;

namespace HydroModTool
{
    public static class Serialization
    {
        internal static string[][] BaseGuids =
        {
            new [] {"AEB01D6208A47946A53E561D2B21B017", "8D738624717D4344829E0B260558CDCA", "BP_ParentItem StaticMesh"},
            new [] {"50134F0A7D55204EAB399A0AF0654415", "78E26F20A83B04479AFBB815A028A5E0", "BP_ParentPower LegsDown"},
            new [] {"4E1DD9065E1D6046822468D7AD914559", "1ED2DBA1FBC69C46B221BB7BD6FC916A", "BP_ParentPower LegsUp"},
            new [] {"7A8C1BDFBFF5EF4295CDDF9D608439E6", "873478DC327F1D4C8C69269EA9071625", "BP_ParentPower LegsLeft"},
            new [] {"899B81E396B185458FA58F3CC5998841", "85E32DA9AE41A445AFF74300E641A555", "BP_ParentPower LegsRight"},
            new [] {"23C9A4C659B20E499E07AD12E1F1269A", "E95747E625376F4FA558DAEB2C2B3118", "BP_ParentLogic Lgk1"},
            new [] {"F8728AF4F637B745AF96726DA513481C", "CC8F4A1F4B4F824FB1C221FD037EDC25", "BP_ParentLogic Lgk2"},
            new [] {"2BB95406E8497C448A317D714F116DD8", "4E4F43B4A026B546ABE189831564FF76", "BP_ParentLogic Lgk3"}
        };

        public static MainConfig LoadMain(string path) => LoadObject<MainConfig>(path);

        public static void SaveMain(string path, MainConfig config) => SaveObject(path, config);

        public static ProjectListing LoadProject(string path) => LoadObject<ProjectListing>(path);

        public static void SaveProject(string path, ProjectListing config) => SaveObject(path, config);

        //I like generics, leave me alone
        private static T LoadObject<T>(string path) where T : class, new()
        {
            if (!File.Exists(path))
                return null;

            using (var fi = File.OpenRead(path))
            {
                var ser = new XmlSerializer(typeof(T));
                return (T)ser.Deserialize(fi);
            }
        }

        private static void SaveObject<T>(string path, T obj) where T : class, new()
        {
            File.Delete(path);
            using (var fo = File.OpenWrite(path))
            {
                var ser = new XmlSerializer(typeof(T));
                ser.Serialize(fo, obj);
                fo.Flush();
                fo.Close();
            }
        }
    }

    [Serializable]
    public class MainConfig
    {
        public List<ProjectListing> Projects { get; set; }
        public List<GuidEntry> Guids { get; set; }
        public bool AutoPackage { get; set; } = false;
    }

    [Serializable]
    public class GuidEntry
    {
        private string _modified;
        private string _retail;

        public string Modified
        {
            get => _modified;
            set
            {
                _modified = value;
                ModifiedBytes = Utilities.StringToByteArray(value);
            }
        }

        public string Retail
        {
            get => _retail;
            set
            {
                _retail = value;
                RetailBytes = Utilities.StringToByteArray(value);
            }
        }

        [XmlIgnore]
        public byte[] ModifiedBytes { get; private set; }
        [XmlIgnore]
        public byte[] RetailBytes { get; private set; }

        public string Name { get; set; }
    }

    [Serializable]
    public class ProjectListing
    {
        public string FriendlyName { get; set; }
        public List<string> IncludedFiles { get; set; }
        public string OutputPath { get; set; }
        public List<string> ScannedFiles { get; set; }
        public string InputPath { get; set; }

        public override string ToString() => FriendlyName ?? OutputPath;

        public ProjectListing()
        {
            IncludedFiles = new List<string>();
            ScannedFiles = new List<string>();
        }
    }
}
