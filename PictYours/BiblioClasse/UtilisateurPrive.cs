using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioClasse
{
    public abstract class UtilisateurPrive : Utilisateur
    {
        public UtilisateurPrive(string nom, string pseudo, string motDePasse, string photoDeProfil)
            :base(nom, pseudo, photoDeProfil)
        {
            MotDePasse = string.IsNullOrWhiteSpace(motDePasse) ? throw new ArgumentNullException(nameof(motDePasse)) : motDePasse;
        }

        public UtilisateurPrive(string nom, string pseudo, string motDePasse, string photoDeProfil, string description)
            : base(nom, pseudo, photoDeProfil, description)
        {
            MotDePasse = string.IsNullOrWhiteSpace(motDePasse) ? throw new ArgumentNullException(nameof(motDePasse)) : motDePasse;
        }

        /// <summary>
        /// Mot de passe de l'utilisateur
        /// </summary>
        public string MotDePasse { get; private set; }

        internal void ModifierMDP(string nouveauMDP)
        {
            if (!EstConnecte) throw new InvalidOperationException($"L'utilisateur {ToShortString()} n'est pas connecté donc ne peut pas changer le mot de passe");
            if (nouveauMDP == null) throw new ArgumentNullException("Le nouveau mot de passe est nul");
            MotDePasse = nouveauMDP;
        }
    }
}
