﻿using BiblioClasse;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Stub
{
    public class Stub : IPersistanceManager
    {
        Amateur pierre = new Amateur("Pierre", "Jean", "pierre.jean", "mdp", "user.png", "Description de Jean Pierre", DateTime.Now.AddYears(-20));
        Amateur tulipe = new Amateur("Tulipe", "Estelle", "estelletulipe", "mdp", "estelle_rond.png", "Description d'Estelle Tulipe", DateTime.Now.AddYears(-15));
        Amateur thomas = new Amateur("Wilhem", "Thomas", "Atrium", "mdp", "pp.jpg", "Description de Thomas Wilhem", DateTime.Now.AddYears(-18));
        Commercial mozilla = new Commercial("Mozilla", "mozilla", "mdp", "mozilla.png", "mozilla.fr", "Firefox - le navigateur indépendant soutenu par une organisation à but non lucratif.");

    public (List<Utilisateur> listeUtilisateurs, Dictionary<Utilisateur,List<Photo>> dico) ChargeDonnées()
        {
            return (ChargeUtilisateur(), ChargePhoto());
        }

        public void SauvegardeDonnées(List<Utilisateur> listeUtilisateur)
        {
            Debug.WriteLine("Sauvegarde demandée");
        }

        private List<Utilisateur> ChargeUtilisateur()
        {
            return new List<Utilisateur> { pierre, tulipe, thomas, mozilla };             
        }

        private Dictionary<Utilisateur, List<Photo>> ChargePhoto() {
            Dictionary<Utilisateur, List<Photo>> Dico = new Dictionary<Utilisateur, List<Photo>>();
            Dico.Add(pierre, new List<Photo> { 
                new Photo("/img/pp.jpg", "test", "test", pierre, DateTime.Today, ECategorie.Animal), 
                new Photo("/img/estelle_rond.png", "test2", "test1532", pierre, DateTime.Today, ECategorie.Animal) 
            });

            Dico.Add(tulipe, new List<Photo> { 
                new Photo("/img/vacances1.jpg", "test2", "test1532", tulipe, DateTime.Today, ECategorie.Animal), 
                new Photo("/img/vacances2.jpg", "test2", "test1532", tulipe, DateTime.Today, ECategorie.Animal), 
                new Photo("/img/vacances3.jpg", "test2", "test1532", tulipe, DateTime.Today, ECategorie.Animal)
            }) ;

            Dico.Add(thomas, new List<Photo> { new Photo("/img/pp.jpg", "test", "test", thomas, DateTime.Today, ECategorie.Animal) });

            Dico.Add(mozilla, new List<Photo> { 
                new Photo("/img/vacances4.jpg", "test2", "test1532", mozilla, DateTime.Today, ECategorie.Animal),
                new Photo("/img/vacances5.jpg", "test2", "test1532", mozilla, DateTime.Today, ECategorie.Animal), 
                new Photo("/img/user.png", "test156", "test123", mozilla, DateTime.Today, ECategorie.Animal), 
                new Photo("/img/estelle_rond.png", "test2", "test1532", mozilla, DateTime.Today, ECategorie.Animal) 
            });
            return Dico;
        }
    }
}

