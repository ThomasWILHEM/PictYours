# PictYours

![Image text](./PictYours/PictYours/app.ico)



## Description du projet

 - PictYours est une application type réseau social. Elle permet de poster des photos et d'intéragir avec les différentes photos. Cette application est destinée à tout le monde, que vous soyez un simple amateur de photo ou bien un commercial cherchant à augmenter sa visibilité.


 - ### Voici la vidéo de présentation de l'application : 
<a href="https://youtu.be/hAz1yZ8xpz8">
	<div align=center>
		<img src="https://img.youtube.com/vi/hAz1yZ8xpz8/hqdefault.jpg">
	</div>
</a>

## Elements importants dans le code

- ### Accessibilité
	De nombreuses aides (ToolTip) sont présentes dans l'application, ce qui permet à l'utilisateur lorsqu'il survole certains boutons ou champs spéciaux d'afficher un message pour l'aider à s'orienter dans l'application.
	```xml
	<RadioButton Grid.Column="2" x:Name="PseudoRadioButton">
		<RadioButton.ToolTip>
			<ToolTip Style="{StaticResource ToolTipEffect}">
				<TextBlock Text="Rechercher avec le pseudo"
				Style="{StaticResource TextBlockToolTipStyle}"/>
			</ToolTip>
		</RadioButton.ToolTip>
		...
	</RadioButton>
	```

- ### DataTemplate avec DataType dans le fichier PagePrincipale.xaml
	Pour afficher un utilisateur dans la liste à gauche, nous utilisons un DataTemplate spécifique en fonction de l'utilisateur à afficher. Cela est possible grâce au DataType sur le Datatemplate.
	
	```xml
	<DataTemplate DataType="{x:Type biblioclasse:Amateur}">
		...
	</DataTemplate>
	<DataTemplate DataType="{x:Type biblioclasse:Commercial}">
		...
	</DataTemplate>
	```

- ### Polymorphisme sur le ToString() des classes
	- #### ToString() dans Utilisateur.cs
	
	```c#
	public override string ToString()
	{
		return $"{Nom}({Pseudo}) Description:{Description}";
	}
	```
	- #### ToString() dans Amateur.cs (qui hérite indirectement de Utilisateur)
	
	```c#
	public override string ToString()
	{
		return $"{Nom} {Prenom}({Pseudo},{DateDeNaissance.ToShortDateString()})";
	}
	```

- ### Evénements personnalisés
	Création d'un événement, avec la classe d'argument associé, qui permet de détecter lorsque la propriété PhotoSelectionne change.
	
	```c#
	public class SelectedPhotoChangedEventArgs
	{
		public Photo Photo { get; private set; }
		public SelectedPhotoChangedEventArgs(Photo photo) => Photo = photo;
	}

	public event EventHandler<SelectedPhotoChangedEventArgs> SelectedPhotoChanged;

	public virtual void OnSelectedPhotoChanged(Photo photo)
	{
		SelectedPhotoChanged?.Invoke(this, new SelectedPhotoChangedEventArgs(photo));
	}
	```

## Ajouts personnels

- ### Gestion de compte 
 	Nous avons ajouté un système de gestion de compte avec `création/suppression` de compte et par conséquent `connexion/déconnexion`.
 
- ### Gestion des images
	Nous avons ajouté un système de gestion des images permettant de gérer les images par rapport à l'application (`chemin relatif`) et non par un chemin absolu. <br/>
	Lorsque l'utilisateur décide de charger une image dans l'application, elle est directement copiée dans un `répertoire dédié` de l'application. 

- ### Autres fonctionnalités
	- Pour les comptes amateurs
	
		Il est possible d'`aimer une photo` postée par soit même ou par un autre utilisateur. Celle-ci se retrouvera alors dans une page où toutes les photos aimées par l'utilisateur seront listées. <br/>
		Il est bien sûr possible de `ne plus aimer une photo` ce qui l'enlevera de la page des photos aimées.
	
    - Pour les comptes commerciaux

		Un commercial peut, s'il le souhaite, `mettre en avant une de ses photos`. La photo se retrouvera alors en tête de liste sur le profil du commercial. 
 
## Comment lancer l'application
Vous pouvez dès à présent `télécharger` l'installeur de l'application en cliquant **[ici](https://github.com/ThomasWILHEM/PictYours/releases)**. 

- ### Prérequis
	- [Microsoft Windows 10 x86 ou x64](https://www.microsoft.com/fr-fr/software-download/windows10)
	- [Microsoft .NET 5.0](https://dotnet.microsoft.com/download/dotnet/5.0)
	- Droits d'administrateurs pendant l'installation

- ### Installation 
	Il suffit de **double-cliquer sur l'`executable`** pour lancer l'installation.
	<br/> Suivre les étapes de l'installation et VOILA !, PictYours est installé.

- ### Pour lancer l'application 
	Vous pouvez **double-cliquer sur le `raccourci` créé sur votre bureau** ou dans le menu démarrer Windows. <br/>
	Vous devez être en administrateur pour lancer l'application.<br/>
	
Une fois cela effectué, vous êtes libre d'explorer PictYours.

## Description du fonctionnement de l'application

Dans PictYours, vous pouvez naviguer sur votre profil, poster des photos, mais aussi aller explorer les profils des autres utilisateurs.
<br/>
- Si vous êtes un `amateur` et que l'une des photos vous plait, vous pouvez `aimer cette photo`.
- Si vous êtes un `commercial` cherchant à donner de la visibilité à votre entreprise, vous pouvez décider de `mettre en avant une photo` de votre choix pour, par exemple, promouvoir un produit.

Et  n'ayez crainte, toutes `vos données seront sauvegardées` lors de la fermeture de l'application et vous les retrouverez intactes  lors de la réouverture de l'application.

## Jeu d'essai

Un jeu d'essai
 est déjà installé dans l'application.</br>
Si vous souhaitez aller sur les comptes déja créés, voici les identifiants/mots de passe

- **Florent Marques** (Amateur)
	- Identifiant : ***flomSStaar***<br/> 
	- Mot de passe : ***IUT63***
- **Thomas Wilhem** (Amateur)
	- Identifiant : ***Atrium*** <br/> 
	- Mot de passe : ***IUT63***
- **Daniel Craig** (Amateur)
	- Identifiant : ***JamesBond***<br/> 
	- Mot de passe : ***007***
- **Estelle Tulipe** (Amateur)
	- Identifiant : ***estelletulipe***<br/> 
	- Mot de passe : ***mdp***
- **Mozilla** (Commercial)
	- Identifiant : ***mozilla***<br/> 
	- Mot de passe : ***mieuxquechrome***

## Documentation

Vous pouvez consulter la documentation de l'application **[ici](./Documents)**.

## Auteurs

- ***MARQUES Florent*** (florent.marques@etu.uca.fr)
- ***WILHEM Thomas*** (thomas.wilhem@etu.uca.fr)

## Licence

Ce projet est **libre de droit** et consultable par tous. <br/>
Vous pouvez aussi nous contacter pour toutes idées ou améliorations.
