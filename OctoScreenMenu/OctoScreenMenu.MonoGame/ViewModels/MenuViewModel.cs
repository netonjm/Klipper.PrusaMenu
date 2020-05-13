using System.Linq;
using OctoScreenMenu;

namespace TestApplication
{
    public class KMenuModel
    {
       
        MainKCfgFile configFile;

        MenuSectionFile actualMenuConfig;
        public MenuSectionFile Actual {
            get => actualMenuConfig;
            set
            {
                actualMenuConfig = value;
                Parent = configFile.GetParentSectionMenu(value);
                Children = configFile.GetChildren(value)
                    .ToArray();
            }
        }

        public bool IsMain => Main == actualMenuConfig;

        public MenuSectionFile Main { get; private set; }
        public MenuSectionFile[] Children { get; private set; }
        public MenuSectionFile Parent { get; private set; }

        public KMenuModel(string filePath)
        {
            configFile = new MainKCfgFile();
            configFile.Load(filePath);

            Main = configFile.MainMenuSectionFile;
            Actual = Main;
        }
    }
}
