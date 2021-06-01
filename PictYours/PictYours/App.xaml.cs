using BiblioClasse;
using DataContractPersistance;
using System.Windows;

namespace PictYours
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public Manager LeManager { get; private set; } = new Manager(new DataContractPers());

        public App()
        {
            LeManager.ChargeDonnees();
        }
    }
}
