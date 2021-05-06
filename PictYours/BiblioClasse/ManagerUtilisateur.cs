using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioClasse
{
    public class ManagerUtilisateur
    {
        public Utilisateur UtilisateurActuel { get; set; }

        public ReadOnlyCollection<Utilisateur> ListeUtilisateur { get; }
        private List<Utilisateur> listeUtilisateur;

        public ManagerUtilisateur()
        {
            listeUtilisateur = new List<Utilisateur>
            {
                new Amateur("Pierre","Jean","pierre.jean","mdp","/img/user.png",DateTime.Now),
                new Amateur("Tulipe","Estelle","estelletulipe","mdp","/img/estelle_rond.png",DateTime.Now),
                new Amateur("Wilhem","Thomas","Atrium","mdp","/img/pp.jpg",DateTime.Now)
            };
        }

        public void SeConnecter(Utilisateur utilisateur)
        {
            if (UtilisateurActuel == null) return;
            
            if (ListeUtilisateur.Contains(UtilisateurActuel))
            {
                SeDeconnecter();
                UtilisateurActuel = utilisateur;
                utilisateur.EstConnecte  =  true;
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

        public bool CreerUnCompte()
        {
            throw new NotImplementedException();
        }

        public bool SupprimerCompte()
        {
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
            if(UtilisateurActuel is Amateur amateur && nouveauPrenom != null)
            {
                amateur.Prenom = nouveauPrenom;
                return true;
            }
            return false;
        }

        public bool ModifierDateDeNaissance(DateTime nouvelleDateDeNaissance)
        {
            if (UtilisateurActuel is Amateur amateur)
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
