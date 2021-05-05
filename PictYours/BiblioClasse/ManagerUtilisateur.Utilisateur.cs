using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioClasse
{
    public partial class ManagerUtilisateur
    {
        public void SeConnecter(Utilisateur u)
        {
            if (u == null)
            {
                return;
            }
            if (ListeUtilisateur.Contains(u))
            {
                if (UtilisateurActuel != null) SeDeconnecter();
                UtilisateurActuel = u;
                u.EstConnecte  =  true;
            }
        }

        public bool SeDeconnecter()
        {
            if (UtilisateurActuel.EstConnecte)
            {
                UtilisateurActuel.EstConnecte = false;
                return true;
            }
            return false;
        }

        //public bool SeConnecter()
        //{
        //    Utilisateur util = null;
        //    Console.WriteLine("=========Connexion=========");
        //    Console.Write("Entrez votre nom d'utlisateur : ");
        //    string pseudo = Console.ReadLine();
        //    foreach (Utilisateur u in ListeUtilisateur)
        //    {
        //        if (u.Pseudo.Equals(pseudo))
        //        {
        //            util = u;
        //            break;
        //        }
        //        return false;
        //    }
        //    Console.Write("Entrez votre mot de passe: ");
        //    string mdp = Console.ReadLine();
        //    if (mdp.Equals(util.MotDePasse))
        //    {
        //        UtilisateurActuel = util;
        //        UtilisateurActuel.EstConnecte = true;
        //        return true;
        //    }
        //    return false;
        //}


        public bool ManModifierNom(string nouveauNom)
        {
            if (UtilisateurActuel != null && nouveauNom != null)
            {
                UtilisateurActuel.Nom = nouveauNom;
                return true;
            }
            return false;
        }

        public bool ManModifierPrenom(string nouveauPrenom)
        {
            Amateur amateur = UtilisateurActuel as Amateur;
            if (amateur != null && nouveauPrenom != null)
            {
                amateur.Prenom = nouveauPrenom;
                return true;
            }
            return false;
        }

        public bool ManModifierPseudo(string nouveauPseudo)
        {
            if (UtilisateurActuel != null && nouveauPseudo != null)
            {
                UtilisateurActuel.Pseudo = nouveauPseudo;
                return true;
            }
            return false;
        }

        public bool ManModifierDateDeNaissance(DateTime nouvelleDateDeNaissance)
        {
            Amateur amateur = UtilisateurActuel as Amateur;
            if (amateur != null)
            {
                amateur.DateDeNaissance = nouvelleDateDeNaissance;
                return true;
            }
            return false;
        }

        public bool ManModifierMDP(string nouveauMDP)
        {
            if (UtilisateurActuel != null && nouveauMDP != null)
            {
                UtilisateurActuel.MotDePasse = nouveauMDP;
                return true;
            }
            return false;
        }
    }
}
