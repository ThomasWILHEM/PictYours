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
        /// <summary>
        /// Liste de tous les utilisateurs
        /// </summary>
        private List<Utilisateur> listeUtilisateur;

        /// <summary>
        /// Evenement pour notifier à la vue qu'une propriété à changer
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Méthode associé à PropertyChanged
        /// </summary>
        /// <param name="propertyName">Nom de la propriété changée</param>
        void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Constructeur du ManageurUtilisateur
        /// </summary>
        public ManagerUtilisateur()
        {
            listeUtilisateur = new List<Utilisateur>();
            ListeUtilisateur = new ReadOnlyCollection<Utilisateur>(listeUtilisateur);
        }

        /// <summary>
        /// Cpnnecte un utilisateur
        /// </summary>
        /// <param name="utilisateur">Utilisateur</param>
        public void SeConnecter(Utilisateur utilisateur)
        {
            if (utilisateur == null) throw new ArgumentNullException("L'utilisateur passé en paramètre est nul");
            if (UtilisateurActuel != null) throw new InvalidUserException("Un utilisateur est déja connecté");
            UtilisateurActuel = utilisateur;
            UtilisateurSelectionne = utilisateur;
            utilisateur.EstConnecte = true;
        }

        /// <summary>
        /// Déconnecte l'utilisateur actuel
        /// </summary>
        public void SeDeconnecter()
        {
            if (!UtilisateurActuel.EstConnecte) throw new InvalidUserException("L'utilisateur n'est pas connecté");
            UtilisateurActuel.EstConnecte = false;
            UtilisateurActuel = null;
        }

        /// <summary>
        /// Créé un compte avec un utilisateur
        /// </summary>
        /// <param name="utilisateur">Utilisateur</param>
        public void CreerUnCompte(Utilisateur utilisateur)
        {
            if (utilisateur == null) throw new InvalidUserException("L'utilisateur passé en paramètre est nul");
            if (listeUtilisateur.Exists(u => u.Pseudo.Equals(utilisateur.Pseudo))) throw new InvalidUserException("Un utilisateur avec un pseudo identique existe déjà");
            listeUtilisateur.Add(utilisateur);
            SeConnecter(utilisateur);
        }

        /// <summary>
        /// Supprime le compte de l'utilisateur actuel
        /// </summary>
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

        /// <summary>
        /// Modifie le nom de l'utilisateur
        /// </summary>
        /// <param name="nouveauNom">Nouveau nom de l'utilisateur</param>
        public void ModifierNom(string nouveauNom)
        {
            if (UtilisateurActuel == null) throw new InvalidUserException("L'utilisateur actuel est nul");
            UtilisateurActuel.Nom = nouveauNom ?? throw new ArgumentNullException(nameof(nouveauNom), "Le nouveau nom est nul");
        }

        /// <summary>
        /// Modifie le prénom de l'amateur
        /// </summary>
        /// <param name="nouveauPrenom">Nouveau prénom de l'amateur</param>
        public void ModifierPrenom(string nouveauPrenom)
        {
            if (UtilisateurActuel is not Amateur amateur) throw new InvalidUserException("L'utilisateur actuel n'est pas un amteur, on ne peut pas modifier le prénom");
            amateur.Prenom = nouveauPrenom ?? throw new ArgumentNullException(nameof(nouveauPrenom), "Le nouveau prénom est nul");
        }

        /// <summary>
        /// Modifie la date de naissance de l'utilisateur
        /// </summary>
        /// <param name="nouvelleDateDeNaissance">Nouvelle date de naissance de l'utilisateur</param>
        public void ModifierDateDeNaissance(DateTime nouvelleDateDeNaissance)
        {
            if (UtilisateurActuel is Amateur amateur) { amateur.DateDeNaissance = nouvelleDateDeNaissance; }
        }

        /// <summary>
        /// Modifie le mot de passe de l'utilisateur
        /// </summary>
        /// <param name="nouveauMDP">Nouveau mot de passe de l'utilisateur</param>
        public void ModifierMDP(string nouveauMDP)
        {
            if (UtilisateurActuel == null) throw new InvalidUserException("L'utilisateur actuel est nul");
            if (nouveauMDP == null) throw new ArgumentNullException(nameof(nouveauMDP), "Le nouveau mot de passe est nul");
            (UtilisateurActuel as UtilisateurPrive).ModifierMDP(nouveauMDP);
        }

        /// <summary>
        /// Modifie la description de l'utilisateur
        /// </summary>
        /// <param name="nouvelleDescription">Nouvelle description de l'utilisateur</param>
        public void ModifierDescription(string nouvelleDescription)
        {
            if (UtilisateurActuel == null) throw new InvalidUserException("L'utilisateur actuel est nul");
            UtilisateurActuel.Description = nouvelleDescription ?? throw new ArgumentNullException(nameof(nouvelleDescription), "La nouvelle description est nulle");
        }

        /// <summary>
        /// Modifie la photo de profil de l'utilisateur
        /// </summary>
        /// <param name="nouvellePhotoDeProfil">Chemin de la nouvelle photo de profil</param>
        public void ModifierPhotoDeProfil(string nouvellePhotoDeProfil)
        {
            if (UtilisateurActuel == null) throw new InvalidUserException("L'utilisateur actuel est nul");
            UtilisateurActuel.PhotoDeProfil = nouvellePhotoDeProfil ?? throw new ArgumentNullException(nameof(nouvellePhotoDeProfil), "La nouvelle photo de profil est nulle");
        }

        /// <summary>
        /// Modifie le site web du commercial
        /// </summary>
        /// <param name="nouveauSiteWeb">Nouveau site web du commercial</param>
        public void ModifierSiteWeb(string nouveauSiteWeb)
        {
            if (UtilisateurActuel == null) throw new InvalidUserException("L'utilisateur actuel est nul");
            if (UtilisateurActuel is not Commercial commercial) throw new InvalidUserException("L'utilisateur actuel n'est pas un commercial donc ne possède pas de site web");
            commercial.SiteWeb = nouveauSiteWeb ?? throw new ArgumentNullException(nameof(nouveauSiteWeb), "Le nouveau site est nul");
        }

        /// <summary>
        /// Charge les données et remplit la liste des utilisateurs
        /// </summary>
        /// <param name="utilisateurs">Liste de tous les utilisateurs</param>
        public void ChargeDonnees(List<Utilisateur> utilisateurs)
        {
            if (utilisateurs == null) return;
            foreach (var utilisateur in utilisateurs)
            {
                listeUtilisateur.Add(utilisateur);
            }
        }

        /// <summary>
        /// Sauvegarde les données (liste des utilisateurs)
        /// </summary>
        /// <returns>Retourne la liste des utilisateurs</returns>
        public List<Utilisateur> SauvegardeDonnees()
        {
            return new List<Utilisateur>(listeUtilisateur);
        }
    }
}
