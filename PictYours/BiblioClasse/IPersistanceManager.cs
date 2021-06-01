using System.Collections.Generic;

namespace BiblioClasse
{
    public interface IPersistanceManager
    {
        /// <summary>
        /// Méthode permettant de charger les données depuis un fichier
        /// </summary>
        /// <returns>Retourne la liste des utilisateurs, le dictoinnaire contenant tous les utilisateurs et leurs photos respectives et le dictionnaire contenant toutes les photos aimées et les utilisateurs qui les ont aimés</returns>
        (List<Utilisateur> listeUtilisateurs, Dictionary<Utilisateur, List<Photo>> photosParUtilisateurs, Dictionary<Photo, List<Amateur>> listeUtilisateursParPhotosAimees, int prochainIdentifiant) ChargeDonnees();

        /// <summary>
        /// Sauvegarde les données dans un fichier
        /// </summary>
        /// <param name="listeUtilisateur">Liste de tous les utilisateurs de l'application</param>
        /// <param name="photosParUtilisateurs">Dictoinnaire contenant tous les utilisateurs et leurs photos respectives</param>
        /// <param name="listeUtilisateursParPhotosAimees">Dictionnaire contenant toutes les photos aimées et les utilisateurs qui les ont aimés</param>
        /// <param name="prochainIdentifiant">Prochain identifiant de photo</param>
        void SauvegardeDonnees(List<Utilisateur> listeUtilisateur, Dictionary<Utilisateur, List<Photo>> photosParUtilisateurs, Dictionary<Photo, List<Amateur>> listeUtilisateursParPhotosAimees, int prochainIdentifiant);
    }
}
