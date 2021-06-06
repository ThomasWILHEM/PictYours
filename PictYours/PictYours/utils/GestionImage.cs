using Microsoft.Win32;
using System.IO;

namespace PictYours.utils
{
    /// <summary>
    /// Classe de gestion des images
    /// </summary>
    public static class GestionImage 
    {
        /// <summary>
        /// Chemin vers le répertoire d'images
        /// </summary>
        public static string ImagesPath;
        /// <summary>
        /// Chemin vers le répertoire des photos de profil
        /// </summary>
        public static string ProfilsPath;
        /// <summary>
        /// Chemin vers le répertoire des icones
        /// </summary>
        public static string IconesPath;

        /// <summary>
        /// Constructeur statique de GestionImage
        /// </summary>
        static GestionImage()
        {
            ImagesPath = Path.Combine(Directory.GetCurrentDirectory(), @"images\photos");
            ProfilsPath = Path.Combine(Directory.GetCurrentDirectory(), @"images\profils");
            IconesPath = Path.Combine(Directory.GetCurrentDirectory(), @"icons");
            if (!Directory.Exists(ImagesPath)) Directory.CreateDirectory(ImagesPath);
            if (!Directory.Exists(ProfilsPath)) Directory.CreateDirectory(ProfilsPath);
            if (!Directory.Exists(IconesPath)) Directory.CreateDirectory(IconesPath);
        }

        /// <summary>
        /// Enumération des types d'enregistrements
        /// </summary>
        public enum TypeEnregistrement
        {
            Images,
            Profils,
            Icones
        }

        /// <summary>
        /// Permet de choisir une image dans l'explorateur de fichier
        /// </summary>
        /// <returns>Renvoie le chemin absolu de l'image sélectionné</returns>
        public static string ChoisirImage()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = @"C:\Users\Public\Pictures";
            dlg.DefaultExt = ".jpg | .png | .gif"; // Default file extension
            dlg.Filter = "All images files (.jpg, .png, .gif)|*.jpg;*.png;*.gif|JPG files (.jpg)|*.jpg|PNG files (.png)|*.png|GIF files (.gif)|*.gif"; // Filter files by extension 

            // Show open file dialog box
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result != true)
            {
                return null;
            }

            //Renvoie le chemin source de l'image
            return dlg.FileName;
        }

        /// <summary>
        /// Permet d'enregistrer une image dans le bon répertoire
        /// </summary>
        /// <param name="fullImagePath">Chemin absolu de l'image à enregistrer</param>
        /// <param name="name">Nom de la nouvelle image</param>
        /// <param name="typeEnregistrement">Type d'enregistrement de la photo</param>
        /// <param name="nomAvecExtension">Vrai si le nom passé en paramètre est avec l'extension sinon faux</param>
        /// <returns>Renvoie le chemin de destination de l'image copiée</returns>
        public static string EnregistrerImage(string fullImagePath, string name, TypeEnregistrement typeEnregistrement, bool nomAvecExtension)
        {
            if (typeEnregistrement.Equals(TypeEnregistrement.Images) && !Directory.Exists(ImagesPath)) Directory.CreateDirectory(ImagesPath);
            if (typeEnregistrement.Equals(TypeEnregistrement.Profils) && !Directory.Exists(ProfilsPath)) Directory.CreateDirectory(ProfilsPath);
            if (typeEnregistrement.Equals(TypeEnregistrement.Icones) && !Directory.Exists(IconesPath)) Directory.CreateDirectory(IconesPath);

            if (string.IsNullOrWhiteSpace(fullImagePath)) return null;
            if (string.IsNullOrWhiteSpace(name)) return null;
            FileInfo fi = new FileInfo(fullImagePath);
            if (!nomAvecExtension)
            {
                name = $"{name}{fi.Extension}";
            }
            string cheminFinal = null;
            switch (typeEnregistrement)
            {
                case TypeEnregistrement.Images:
                    cheminFinal = VerifierChemin(Path.Combine(ImagesPath, name));
                    File.Copy(fullImagePath, cheminFinal);
                    break;
                case TypeEnregistrement.Profils:
                    cheminFinal = VerifierChemin(Path.Combine(ProfilsPath, name));
                    File.Copy(fullImagePath, cheminFinal);
                    break;
                case TypeEnregistrement.Icones:
                    cheminFinal = VerifierChemin(Path.Combine(IconesPath, name));
                    File.Copy(fullImagePath, cheminFinal);
                    break;
            }

            return cheminFinal;
        }

        /// <summary>
        /// Vérifier le chemin d'un fichier passé en paramètre 
        /// et lui donne un nouveau nom si le fichier éxiste déjà
        /// </summary>
        /// <param name="cheminFinal">Chemin à vérifier</param>
        /// <returns>Renvoie le chemin du fichier potientiellement mis à jour</returns>
        private static string VerifierChemin(string cheminFinal)
        {
            int i = 1;
            FileInfo fi = new(cheminFinal);
            string name = fi.Name;
            while (File.Exists(fi.Directory+"\\"+name))
            {
                name = $"{fi.Name.Remove(fi.Name.LastIndexOf('.'))}_{i}{fi.Extension}";
                i++;
            }
            return $"{fi.Directory}\\{name}";
        }
    }
}
