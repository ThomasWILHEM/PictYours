using System.Collections.Generic;
using System.Linq;

namespace BiblioClasse
{
    public static class RechercheUtilisateur
    {
        /// <summary>
        /// Cette méthode permet de rechercher un utilsateur grâce à un pseudo (utilisé dans la connexion/création de compte)
        /// </summary>
        /// <param name="listeUtilisateurs">Liste de tous les utilisateurs</param>
        /// <param name="pseudo">Pseudo utilisé pour rechercher un utilisateur</param>
        /// <returns>Retourne un utilisateur</returns>
        public static Utilisateur RechercheUnUtilisateur(List<Utilisateur> listeUtilisateurs, string pseudo)
        {
            return listeUtilisateurs.Find(utilisateur => utilisateur.Pseudo.Equals(pseudo));
        }

        /// <summary>
        /// Cette méthode permet de rechercher un utilsateur grâce à un pattern ressemblant à un pseudo
        /// </summary>
        /// <param name="liste">Liste de tous les utilisateurs</param>
        /// <param name="pattern">Pattern recherché</param>
        /// <returns>Retourne un utilisateur</returns>
        public static List<Utilisateur> RechercheParPseudo(List<Utilisateur> liste,string pattern)
        {
            return liste.Where(utilisateur => utilisateur.Pseudo.ToLower().Contains(pattern?.ToLower())).ToList();
        }

        /// <summary>
        /// Cette méthode permet de rechercher un utilsateur grâce à un pattern ressemblant au nom et/ou prénom
        /// </summary>
        /// <param name="liste">Liste de tous les utilisateurs</param>
        /// <param name="pattern">Pattern recherché</param>
        /// <returns>Retourne un utilisateur</returns>
        public static List<Utilisateur> RechercheParNomEtPrenom(List<Utilisateur> liste, string pattern)
        {
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
