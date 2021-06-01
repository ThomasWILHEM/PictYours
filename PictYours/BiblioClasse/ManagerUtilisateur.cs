using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace BiblioClasse
{
    public class ManagerUtilisateur : INotifyPropertyChanged
    {
        /// <summary>
        /// Utilisateur actuellement connecté sur l'application
        /// </summary>
        public Utilisateur UtilisateurActuel { get; set; }

        /// <summary>
        /// Utilisateur sélectionné dans la liste de profil
        /// </summary>
        public Utilisateur UtilisateurSelectionne
        {
            get => utilisateurSelectionne;
            set
            {
                if (utilisateurSelectionne != value)
                {
                    utilisateurSelectionne = value;
                    OnPropertyChanged(nameof(UtilisateurSelectionne));
                    if (value is Commercial commercial)
                    {
                        commercial.NombreDeVisites++;
                    }
                }
            }
        }
        private Utilisateur utilisateurSelectionne;

        /// <summary>
        /// Liste d'utilisateurs qui est encapsulé en lecture seule
        /// </summary>
        public ReadOnlyCollection<Utilisateur> ListeUtilisateur { get; }
        private List<Utilisateur> listeUtilisateur;

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public ManagerUtilisateur()
        {
            listeUtilisateur = new List<Utilisateur>();
            ListeUtilisateur = new ReadOnlyCollection<Utilisateur>(listeUtilisateur);
        }

        public void SeConnecter(Utilisateur utilisateur)
        {
            if (utilisateur == null) throw new ArgumentNullException("L'utilisateur passé en paramètre est nul");
            if (UtilisateurActuel != null) throw new InvalidUserException("Un utilisateur est déja connecté");
            UtilisateurActuel = utilisateur;
            UtilisateurSelectionne = utilisateur;
            utilisateur.EstConnecte = true;
        }

        public void SeDeconnecter()
        {
            if (!UtilisateurActuel.EstConnecte) throw new InvalidUserException("L'utilisateur n'est pas connecté");
            UtilisateurActuel.EstConnecte = false;
            UtilisateurActuel = null;
        }

        public void CreerUnCompte(Utilisateur utilisateur)
        {
            if (utilisateur == null) throw new InvalidUserException("L'utilisateur passé en paramètre est nul");
            if (listeUtilisateur.Exists(u => u.Pseudo.Equals(utilisateur.Pseudo))) throw new InvalidUserException("Un utilisateur avec un pseudo identique existe déjà");
            listeUtilisateur.Add(utilisateur);
            SeConnecter(utilisateur);
        }

        public void SupprimerCompte()
        {
            if (UtilisateurActuel == null) throw new InvalidUserException("Aucun utilisateur est connecté");
            if (!listeUtilisateur.Remove(UtilisateurActuel))
            {
                Debug.WriteLine($"L'UtilisateurActuel {UtilisateurActuel} n'est pas présent dans la liste d'utilisateurs");
                throw new InvalidUserException($"L'UtilisateurActuel {UtilisateurActuel} n'est pas présent dans la liste d'utilisateurs");
            }
            Debug.WriteLine($"L'UtilisateurActuel {UtilisateurActuel} est bien supprimé");
            UtilisateurActuel = null;
        }

        /// <summary>
        /// Verifie si le pseudo passé en paramètre n'est pas déjà présent dans la liste d'utilisateur
        /// </summary>
        /// <param name="pseudo">pseudo à vérifier</param>
        /// <returns>Renvoie vrai si le pseudo existe sinon faux</returns>
        public bool VerifierPseudo(string pseudo)
        {
            return listeUtilisateur.Exists(u => u.Pseudo == pseudo);
        }

        /// <summary>
        /// Vérifie si le mot de passe passé en paramètre est celui de l'utilisateur actuel
        /// </summary>
        /// <returns></returns>
        public bool VerifierMotDePasse(string motDePasse)
        {
            UtilisateurPrive utilisateurPrive = UtilisateurActuel as UtilisateurPrive;
            return utilisateurPrive.MotDePasse.Equals(motDePasse);
        }

        public void ModifierNom(string nouveauNom)
        {
            if (UtilisateurActuel == null) throw new InvalidUserException("L'utilisateur actuel est nul");
            UtilisateurActuel.Nom = nouveauNom ?? throw new ArgumentNullException(nameof(nouveauNom), "Le nouveau nom est nul");
        }

        public void ModifierPrenom(string nouveauPrenom)
        {
            if (UtilisateurActuel is not Amateur amateur) throw new InvalidUserException("L'utilisateur actuel n'est pas un amteur, on ne peut pas modifier le prénom");
            amateur.Prenom = nouveauPrenom ?? throw new ArgumentNullException(nameof(nouveauPrenom), "Le nouveau prénom est nul");
        }

        public void ModifierDateDeNaissance(DateTime nouvelleDateDeNaissance)
        {
            if (UtilisateurActuel is Amateur amateur) { amateur.DateDeNaissance = nouvelleDateDeNaissance; }
        }

        public void ModifierMDP(string nouveauMDP)
        {
            if (UtilisateurActuel == null) throw new InvalidUserException("L'utilisateur actuel est nul");
            if (nouveauMDP == null) throw new ArgumentNullException(nameof(nouveauMDP), "Le nouveau mot de passe est nul");
            (UtilisateurActuel as UtilisateurPrive).ModifierMDP(nouveauMDP);
        }

        public void ModifierDescription(string nouvelleDescription)
        {
            if (UtilisateurActuel == null) throw new InvalidUserException("L'utilisateur actuel est nul");
            UtilisateurActuel.Description = nouvelleDescription ?? throw new ArgumentNullException(nameof(nouvelleDescription), "La nouvelle description est nulle");
        }

        public void ModifierPhotoDeProfil(string nouvellePhotoDeProfil)
        {
            if (UtilisateurActuel == null) throw new InvalidUserException("L'utilisateur actuel est nul");
            UtilisateurActuel.PhotoDeProfil = nouvellePhotoDeProfil ?? throw new ArgumentNullException(nameof(nouvellePhotoDeProfil), "La nouvelle photo de profil est nulle");
        }

        public void ModifierSiteWeb(string nouveauSiteWeb)
        {
            if (UtilisateurActuel == null) throw new InvalidUserException("L'utilisateur actuel est nul");
            if (UtilisateurActuel is not Commercial commercial) throw new InvalidUserException("L'utilisateur actuel n'est pas un commercial donc ne possède pas de site web");
            commercial.SiteWeb = nouveauSiteWeb ?? throw new ArgumentNullException(nameof(nouveauSiteWeb), "Le nouveau site est nul");
        }

        public void ChargeDonnees(List<Utilisateur> utilisateurs)
        {
            if (utilisateurs == null) return;
            foreach (var utilisateur in utilisateurs)
            {
                listeUtilisateur.Add(utilisateur);
            }
        }

        public List<Utilisateur> SauvegardeDonnees()
        {
            return new List<Utilisateur>(listeUtilisateur);
        }
    }
}
