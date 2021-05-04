using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioClasse
{
    public class ManagerUtilisateur
    {

        public List<Utilisateur> ListeUtilisateur { get; private set; }

        public Utilisateur UtilisateurActuel { get; private set; }

        public ManagerUtilisateur(Utilisateur u)
        {
            ListeUtilisateur = new List<Utilisateur>();
            UtilisateurActuel = u;
        }

        /*public void SeConnecter(Utilisateur u)
        {
            if (u == null)
            {
                return;
            }
            if (ListeUtilisateur.Contains(u))
            {
                UtilisateurActuel = u;
                u.EstConnecte  =  true;
            }
        }*/

        public bool SeConnecter()
        {
            Utilisateur util = null;
            Console.WriteLine("=========Connexion=========");
            Console.Write("Entrez votre nom d'utlisateur : ");
            string pseudo = Console.ReadLine();
            foreach (Utilisateur u in ListeUtilisateur)
            {
                if (u.Pseudo.Equals(pseudo))
                {
                    util = u;
                    break;
                }
                return false;
            }
            Console.Write("Entrez votre mot de passe: ");
            string mdp = Console.ReadLine();
            if (mdp.Equals(util.MotDePasse))
            {
                UtilisateurActuel = util;
                UtilisateurActuel.EstConnecte = true;
                return true;
            }
            return false;
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

        public bool CreerUnCompte()
        {
            //Console.WriteLine("=========Création de compte=========");
            //Console.Write("Entrez votre nom : ");
            //string nom = Console.ReadLine();
            //Console.Write("Entrez votre prénom : ");
            //string prénom = Console.ReadLine();
            //Console.Write("Entrez votre nom d'utilisateur : ");
            //string pseudo = Console.ReadLine();
            //Console.Write("Entrez votre date de naissance (JJ/MM/YYYY) : ");
            //string date = Console.ReadLine();
            //Console.Write("Entrez une description : ");
            //string desc = Console.ReadLine();
            //Console.Write("Entrez votre mot de passe : ");
            //string mdp = Console.ReadLine();
            //Utilisateur u = new Utilisateur(nom, prénom, pseudo, Convert.ToDateTime(date), mdp);
            //ListeUtilisateur.Add(u);
            throw new NotImplementedException();
        }

        public bool SupprimerCompte()
        {
            //Console.WriteLine("=========Supression de compte=========");
            //Console.Write("Veuillez entrer votre mot de passe : ");
            //string mdp = Console.ReadLine();
            //if (mdp.Equals(UtilisateurActuel.MotDePasse))
            //{
            //    ListeUtilisateur.Remove(UtilisateurActuel);
            //    UtilisateurActuel = null;
            //    Console.WriteLine("Suppersion effectuée");
            //    return true;
            //}
            //Console.WriteLine("Mauvais mot de passe.");
            throw new NotImplementedException();
        }

        public bool ModifierNom(string nouveauNom)
        {
            if (UtilisateurActuel != null && nouveauNom != null)
            {
                UtilisateurActuel.Nom = nouveauNom;
                return true;
            }
            return false;
        }

        public bool ModifierPrenom(string nouveauPrenom)
        {
            Amateur amateur=UtilisateurActuel as Amateur;
            if (amateur != null && nouveauPrenom != null)
            {
                amateur.Prenom = nouveauPrenom;
                return true;
            }
            return false;
        }

        public bool ModifierPseudo(string nouveauPseudo)
        {
            if (UtilisateurActuel != null && nouveauPseudo != null)
            {
                UtilisateurActuel.Pseudo = nouveauPseudo;
                return true;
            }
            return false;
        }

        public bool ModifierDateDeNaissance(DateTime nouvelleDateDeNaissance)
        {
            Amateur amateur = UtilisateurActuel as Amateur;
            if (amateur != null)
            {
                amateur.DateDeNaissance = nouvelleDateDeNaissance;
                return true;
            }
            return false;
        }

        public bool ModifierMDP(string nouveauMDP)
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