using Newtonsoft.Json;
using System.IO;

namespace GeneralLib.Usr
{
    public static class UserDataReader
    {
        private static string jsonUri = "user.json";

        public static UserData UserData = null!;

        public static void Set()
        {
            if (File.Exists(jsonUri))
            {
                UserData = JsonConvert.DeserializeObject<UserData>(File.ReadAllText(jsonUri))!;
            }
            else UserData = new();
        }

        public static void Save()
        {
            File.WriteAllText(jsonUri, JsonConvert.SerializeObject(UserData));
        }
    }
}
