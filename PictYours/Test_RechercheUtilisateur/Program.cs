using System;
using System.Collections.Generic;
using BiblioClasse;

namespace Test_RechercheUtilisateur
{
    class Program
    {
        static void Affichage(List<Utilisateur> liste, string pattern)
        {
            Console.WriteLine($"\tPattern recherché: {pattern}\n");
            foreach (Utilisateur utilisateur in liste)
            {
                Console.WriteLine(utilisateur.ToString());
            }
            Console.WriteLine("\n\t-------------------\n");
        }

        static void Affichage(Utilisateur utilisateur, string pattern)
        {
            Console.WriteLine($"\tPattern recherché: {pattern}\n");
            Console.WriteLine(utilisateur?.ToString());
            Console.WriteLine("\n\t-------------------\n");
        }

        static void Main(string[] args)
        {
            Test_RechercheParPseudo();
            Console.WriteLine("====================================\n");
            Test_RechercheParNomEtPrenom();
            Console.WriteLine("====================================\n");
            Test_RechercheUnUtilisateur();
        }

        static List<Utilisateur> listeUtilisateur = new List<Utilisateur>
            {
                new Amateur("Pierre","Jean","pierre.jean","mdp","/img/user.png",DateTime.Now),
                new Amateur("Tulipe","Estelle","estelletulipe","mdp","/img/estelle_rond.png",DateTime.Now),
                new Amateur("Wilhem","Thomas","Atrium","mdp","/img/pp.jpg",DateTime.Now),
                new Commercial("Mozilla","mozilla","mdp","/img/mozilla.png","mozilla.fr")
            };

        static void Test_RechercheParPseudo()
        {
            Console.WriteLine(nameof(Test_RechercheParPseudo));
            Console.WriteLine();

            //Tous les utilisateurs affichés car ils correspondent tous au pattern
            List<Utilisateur> listeFiltre = RechercheUtilisateur.RechercheParPseudo(listeUtilisateur, "i");
            Affichage(listeFiltre, "i");

            //Un seul utilisateur affiché
            listeFiltre = RechercheUtilisateur.RechercheParPseudo(listeUtilisateur, "estelle");
            Affichage(listeFiltre, "estelle");

            //Aucun affichage car le pattern ne correspond pas
            listeFiltre = RechercheUtilisateur.RechercheParPseudo(listeUtilisateur, "paul");
            Affichage(listeFiltre, "paul");
        }

        static void Test_RechercheParNomEtPrenom()
        {
            Console.WriteLine(nameof(Test_RechercheParNomEtPrenom));
            Console.WriteLine();

            //2 utilisateurs correspondent
            List<Utilisateur> listeFiltre = RechercheUtilisateur.RechercheParNomEtPrenom(listeUtilisateur, "il");
            Affichage(listeFiltre,"il");

            //Un seul correspond
            listeFiltre = RechercheUtilisateur.RechercheParNomEtPrenom(listeUtilisateur, "Thomas");
            Affichage(listeFiltre, "Thomas");

            //Aucun correspond
            listeFiltre = RechercheUtilisateur.RechercheParNomEtPrenom(listeUtilisateur, "paul");
            Affichage(listeFiltre, "paul");
        }

        static void Test_RechercheUnUtilisateur()
        {
            Console.WriteLine(nameof(Test_RechercheUnUtilisateur));
            Console.WriteLine();

            //L'utilisateur est bien affiché car présent dans la liste
            Utilisateur utilisateur = RechercheUtilisateur.RechercheUnUtilisateur(listeUtilisateur, "Atrium");
            Affichage(utilisateur, "Atrium");

            //L'utilisateur recherché n'est pas présent dans la liste donc pas affiché
            utilisateur = RechercheUtilisateur.RechercheUnUtilisateur(listeUtilisateur, "flomSStaar");
            Affichage(utilisateur, "flomSStaar");

        }
    }
}
