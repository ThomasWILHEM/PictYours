using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioClasse
{
    public interface IPersistanceManager
    {
        (List<Utilisateur> listeUtilisateurs, Dictionary<Utilisateur, List<Photo>> dico) ChargeDonnées();
        void SauvegardeDonnées(List<Utilisateur> listeUtilisateur);
    }
}
