using System.Collections.Generic;
using System.Linq;

namespace BiblioClasse
{
    public static class RechercheUtilisateur
    {
        public static Utilisateur RechercheUnUtilisateur(List<Utilisateur> listeUtilisateurs, string pseudo)
        {
            return listeUtilisateurs.Find(utilisateur => utilisateur.Pseudo.Equals(pseudo));
        }

        public static List<Utilisateur> RechercheParPseudo(List<Utilisateur> liste,string pattern)
        {
            return liste.Where(utilisateur => utilisateur.Pseudo.ToLower().Contains(pattern?.ToLower())).ToList();
        }

        public static List<Utilisateur> RechercheParNomEtPrenom(List<Utilisateur> liste, string pattern)
        {
            //return liste.Where(utilisateur => $"{utilisateur.Nom} {utilisateur.Prenom}".Contains(pattern)).ToList();
            List<Utilisateur> listeFiltre = new List<Utilisateur>();
            foreach(Utilisateur utilisateur in liste)
            {
                if(utilisateur is Amateur amateur)
                {
                    if ($"{amateur.Nom}{amateur.Prenom}".ToLower().Contains(pattern?.ToLower()))
                    {
                        listeFiltre.Add(amateur);
                    }
                }
                if(utilisateur is Commercial commercial)
                {
                    if ($"{commercial.Nom}".ToLower().Contains(pattern?.ToLower()))
                    {
                        listeFiltre.Add(commercial);
                    }
                }
            }
            return listeFiltre;
        }
    }
}
