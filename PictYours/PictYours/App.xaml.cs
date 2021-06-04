using BiblioClasse;
using DataContractPersistance;
using JsonPersistance;
using System.Windows;

namespace PictYours
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public Manager LeManager { get; private set; } = new Manager(new JsonPers());

        public App()
        {
            LeManager.ChargeDonnees();

        }
    }
}
