using System.Collections.Generic;
using System.IO;

namespace Assets.Scripts.Gameobjects.Level {
    public class PackLoader {
        public const string LEVEL_FOLDER = "Levels/";

        private static Pack LoadPackByName(string name) {
            return new Pack(name);
        }

        public static List<Pack> GetPackList() {
            List<Pack> list = new List<Pack>();
            DirectoryInfo[] directories = new DirectoryInfo(LEVEL_FOLDER).GetDirectories();
            foreach (var dir in directories) {
                list.Add(LoadPackByName(dir.Name));
            }
            return list;
        }
    }
}
