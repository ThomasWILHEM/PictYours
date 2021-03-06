using System;
using System.Runtime.Serialization;

namespace BiblioClasse
{
    [DataContract, KnownType(typeof(Amateur)), KnownType(typeof(Commercial))]
    public abstract class UtilisateurPrive : Utilisateur
    {
        /// <summary>
        /// Mot de passe de l'utilisateur
        /// </summary>
        [DataMember]
        public string MotDePasse { get; private set; }


        public UtilisateurPrive()
        {
             
        }

        public UtilisateurPrive(string nom, string pseudo, string motDePasse, string photoDeProfil, string description = "")
            : base(nom, pseudo, photoDeProfil, description)
        {
            MotDePasse = string.IsNullOrWhiteSpace(motDePasse) ? throw new ArgumentNullException(nameof(motDePasse), "Le mot de passe ne peut pas être nul") : motDePasse;
        }

        /// <summary>
        /// Méthode permettant de modifier le mot de passe de l'utilisateur
        /// </summary>
        /// <param name="nouveauMDP"></param>
        internal void ModifierMDP(string nouveauMDP)
        {
            if (!EstConnecte) throw new InvalidUserException($"L'utilisateur {ToShortString()} n'est pas connecté donc ne peut pas changer le mot de passe");
            MotDePasse = nouveauMDP ?? throw new ArgumentNullException(nameof(nouveauMDP), "Le nouveau mot de passe est nul");
        }
    }
}
