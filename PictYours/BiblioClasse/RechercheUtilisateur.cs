using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioClasse
{
    public static class RechercheUtilisateur
    {
        public static Utilisateur RechercheUnUtilisateur(List<Utilisateur> liste, string pseudo)
        {
            return liste.Find(utilisateur => utilisateur.Pseudo.Equals(pseudo));//Where(u => u.Pseudo.Equals(pseudo)).FirstOrDefault();
        }

        public static List<Utilisateur> RechercheParPseudo(List<Utilisateur> liste,string pattern)
        {
            return liste.Where(utilisateur => utilisateur.Pseudo.Contains(pattern)).ToList();
        }

        public static List<Utilisateur> RechercheParNomEtPrenom(List<Utilisateur> liste, string pattern)
        {
            //return liste.Where(utilisateur => $"{utilisateur.Nom} {utilisateur.Prenom}".Contains(pattern)).ToList();
            List<Utilisateur> listeFiltre = new List<Utilisateur>();
            foreach(Utilisateur utilisateur in liste)
            {
                if(utilisateur is Amateur amateur)
                {
                    if ($"{amateur.Nom}{amateur.Prenom}".Contains(pattern))
                    {
                        listeFiltre.Add(amateur);
                    }
                }
                if(utilisateur is Commercial commercial)
                {
                    if ($"{commercial.Nom}".Contains(pattern))
                    {
                        listeFiltre.Add(commercial);
                    }
                }
            }
            return listeFiltre;
        }
    }
}
