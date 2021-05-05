using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioClasse
{
    public partial class ManagerUtilisateur
    {

        public List<Utilisateur> ListeUtilisateur { get; private set; }

        public Utilisateur UtilisateurActuel { get; private set; }

        public ManagerUtilisateur(Utilisateur u)
        {
            ListeUtilisateur = new List<Utilisateur>();
            UtilisateurActuel = u;
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

        
    }
}