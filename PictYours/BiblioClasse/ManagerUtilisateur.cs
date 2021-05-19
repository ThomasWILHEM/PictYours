using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Amateur a = new Amateur("Pierre", "Jean", "pierre.jean", "mdp", "/img/user.png", DateTime.Now,"Je suis une grosse banane");
            Amateur a1 = new Amateur("Tulipe", "Estelle", "estelletulipe", "mdp", "/img/estelle_rond.png", DateTime.Now,"Je suis une plus grosse banane");
            Amateur a2 = new Amateur("Wilhem", "Thomas", "Atrium", "mdp", "/img/pp.jpg", DateTime.Now,"Je suis une plus grosse banane");
            Commercial c1 = new Commercial("Mozilla", "mozilla", "mdp", "/img/mozilla.png", "mozilla.fr", "Firefox - le navigateur indépendant soutenu par une organisation à but non lucratif.");

            listeUtilisateur = new List<Utilisateur> { a, a1, a2, c1 };
            ListeUtilisateur = new ReadOnlyCollection<Utilisateur>(listeUtilisateur);

            UtilisateurSelectionne = ListeUtilisateur[0];
            UtilisateurActuel = ListeUtilisateur[0];
            UtilisateurActuel.EstConnecte = true;
        }

        public void SeConnecter(Utilisateur utilisateur)
        {
            if (UtilisateurActuel == null) return;

            if (ListeUtilisateur.Contains(UtilisateurActuel))
            {
                SeDeconnecter();
                UtilisateurActuel = utilisateur;
                utilisateur.EstConnecte = true;
            }
        }

        public void SeDeconnecter()
        {
            if (!UtilisateurActuel.EstConnecte) throw new Exception("L'utilisateur n'est pas connécté");
            UtilisateurActuel.EstConnecte = false;
        }

        public void CreerUnCompte(Utilisateur utilisateur)
        {
            if (utilisateur == null) throw new ArgumentNullException("L'utilisateur passé en paramètre est nul");
            listeUtilisateur.Add(utilisateur);
        }

        public void SupprimerCompte()
        {
            throw new NotImplementedException();
        }

        public void ModifierNom(string nouveauNom)
        {
            if (UtilisateurActuel == null) throw new InvalidOperationException("L'utilisateur actuel est nul");
            if (nouveauNom == null) throw new ArgumentNullException("Le nouveau nom est nul");
            UtilisateurActuel.Nom = nouveauNom;
        }

        public void ModifierPrenom(string nouveauPrenom)
        {
            if (nouveauPrenom == null) throw new ArgumentNullException("Le nouveau prénom est nul");
            if (UtilisateurActuel is not Amateur amateur) throw new InvalidOperationException("L'utilisateur actuel n'est pas un amteur, on ne peut pas modifier le prénom");
            amateur.Prenom = nouveauPrenom;
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
                UtilisateurPrive utilisateurPrive = UtilisateurActuel as UtilisateurPrive;
                utilisateurPrive.ModifierMDP(nouveauMDP);
                return true;
            }
            return false;
        }

        public bool ModifierDescription(string nouvelleDescription)
        {
            if (UtilisateurActuel != null && nouvelleDescription != null)
            {
                UtilisateurActuel.Description = nouvelleDescription;
                return true;
            }
            return false;
        }

        public bool ModifierPhotoDeProfil(string nouvellePhotoDeProfil)
        {
            if (UtilisateurActuel != null && nouvellePhotoDeProfil != null)
            {
                UtilisateurActuel.PhotoDeProfil = nouvellePhotoDeProfil;
                return true;
            }
            return false;
        }
    }
}
