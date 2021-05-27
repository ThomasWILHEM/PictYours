using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioClasse
{
    public class Manager
    {

        public ManagerUtilisateur ManagerUtilisateur { get; private set; }
        public ManagerPhoto ManagerPhoto { get; private set; }
       

        public Manager(IPersistanceManager persistance)
        {
            ManagerUtilisateur = new ManagerUtilisateur(persistance);
            ManagerUtilisateur.ChargeDonnées();
            ManagerPhoto = new ManagerPhoto(persistance);
            ManagerPhoto.ChargeDonnees();

        }
          
        
    }
}