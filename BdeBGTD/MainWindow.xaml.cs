using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using ClassesAffaire;
using GTD;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.IO;



namespace BdeBGTD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DateTime dateAffichee;

        private static GestionnaireGTD gestionnaire = new GestionnaireGTD();
        //boolean permettant de sortir de la boucle qui affiche le traitement d'Actions 
        public bool BriserLoop
        {
            set { briserLoop = value; }
            get { return briserLoop; }
        }
        // pour que la fenetre traiter entree s'affiche
        private bool briserLoop = true;
        //chemein pour le fichierXml
        private static char DIR_SEPARATOR = System.IO.Path.DirectorySeparatorChar;
        private static string pathFichier = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}{DIR_SEPARATOR}" +
                        $"Fichiers-3GP{DIR_SEPARATOR}bdeb_gtd.xml";
        public MainWindow()
        {
            InitializeComponent();

            //Verification de la presence du fichier
            if (!File.Exists(pathFichier))
            {
                Console.Error.WriteLine($"Le fichier {pathFichier} n'existe pas");
                System.Environment.Exit(1);
            }
            ChargerEntrees(pathFichier);
            //affichage de la date actuelle au bon format
            dateAffichee = DateTime.Now;
            date.Text = dateAffichee.ToShortDateString();
            DataContext = gestionnaire;
            //affichage des bonnes actions et bonnes suivies
            FiltrerActionsParDate();
            FiltrerSuiviesParDate();
            Closing += MainWindow_Closing;

        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            //Ajoute 1 jour a la date affichée et met à jour les listes
            dateAffichee = dateAffichee.AddDays(1);
            ChangerDate(dateAffichee);
            FiltrerActionsParDate();
            miseAJourSuivi();
            FiltrerSuiviesParDate();
        }
        //met à jour la date àpres l'ajout de 1 jour
        private void ChangerDate(DateTime nouvelleDate)
        {
            date.Text = nouvelleDate.ToShortDateString();
        }
        //Ouvre la fenetre a propos
        private void ClickAPropos(object sender, RoutedEventArgs e)
        {

            WindowAPropos fenetreApropos = new WindowAPropos();
            fenetreApropos.ShowDialog();

        }

        // routed commande pour pouvoir fermer la fenetre principale
        public static RoutedCommand QuitterCmd = new RoutedCommand();

        private void Quitter_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Quitter_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
        private void Quitter_Click(object sender, RoutedEventArgs e)
        {
            //sauvegarder a definir plus tard 

        }
        // ouvrir la fenetre d'ajout 
        private void ouvrirFenetreAjout()
        {
            WindowAjout windowAjout = new WindowAjout();
            windowAjout.Owner = this;
            windowAjout.ShowDialog();

        }
        // routed commande permettant d'ouvrir la fenetre d'Ajout de'entree, elle reste toujours accessible

        public static RoutedCommand AjoutCmd = new RoutedCommand();

        private void Ajout_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

            e.CanExecute = true;

        }

        private void Ajout_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ouvrirFenetreAjout();
        }

        // routed commande permettant d'ouvrir la fenetre de traitement d'entrees, elle reste tant qu'il y a des entrees
        public static RoutedCommand TraiterCmd = new RoutedCommand();

        private void Traiter_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (gestionnaire.ListeEntrees.Count() > 0)
            {
                e.CanExecute = true;
            }
            else
            { e.CanExecute = false; }
        }

        private void Traiter_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            BriserLoop = true;
            ouvrirFenetreTraiter();
        }
        /// <summary>
        /// methode qui sert à l'ouverture de la fenetreTraiter 
        /// </summary>
        private void ouvrirFenetreTraiter()
        {// on entre la première entrée de la liste en paramètre de la nouvelle fenêtre <traiter entrée> et rentre dans un boucle qui continue tant qu'il y a des elements dans la liste
            while (gestionnaire.ListeEntrees.Count­ > 0 && BriserLoop)
            {
                WindowTraiter windowTraiter = new WindowTraiter(gestionnaire.ListeEntrees.FirstOrDefault());
                windowTraiter.Owner = this;
                windowTraiter.ShowDialog();
            }

        }
        /// <summary>
        /// Méthode qui permet de n'afficher que les actions dont la date est courrante ou passée
        ///  concept de view découvert grâce à chat gpt
        /// </summary>
        private void FiltrerActionsParDate()
        {// creation d'une vue pour la listeActions
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(gestionnaire.ListeActions);
            //mise en place du filtre
            view.Filter = item =>
            {
                ElementGTD element = item as ElementGTD;
                //validation de la non-nullité de l'element
                if (element != null && element.DateRappel != null)
                {//comparaison entre les date rappel et la date affichee
                    return string.Compare(element.DateRappel, date.Text) <= 0;
                }
                return false; // au cas ou il n'y a pas de match
            };
        }
        /// <summary>
        /// Methode fait exactement L,inverse de la méthode precedente pour les actions suivies ou incubée
        /// </summary>
        public void FiltrerSuiviesParDate()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(gestionnaire.ListeSuivis);
            view.Filter = item =>
            {
                ElementGTD element = item as ElementGTD;
                if (element != null && element.DateRappel != null)
                {
                    return string.Compare(element.DateRappel, date.Text) > 0;
                }
                return false;
            };
        }
        /// <summary>
        /// Permet d'ouvrir la fenetre de traitement d'Action en prenant l'action cliquée en paramètre
        /// </summary>

        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem action = sender as ListBoxItem;
            ElementGTD actionGTD = (ElementGTD)action.DataContext;

            WindowTraiterAction windowTraiterAction = new WindowTraiterAction(actionGTD);
            windowTraiterAction.Owner = this;
            windowTraiterAction.ShowDialog();
            FiltrerActionsParDate();
        }
        /// <summary>
        /// methode permettant de mettre à jour la  la liste suivi, et de  creer un entree correspendante
        /// </summary>
        private void miseAJourSuivi()
        {
            //creation d'Une liste temporaire qui contientiendra les elements à deplacer
            List<ElementGTD> elementADeplacer = new List<ElementGTD>();

            foreach (ElementGTD suiviElement in gestionnaire.ListeSuivis)
            {
                if (suiviElement.DateRappel == date.Text)
                {
                    // Creation d'un nouvel element GTD avec seulement un nom un statut et une description
                    ElementGTD nouvelleEntree = new ElementGTD(suiviElement.Nom, suiviElement.Description, "Entree");
                    elementADeplacer.Add(nouvelleEntree);

                }
            }

            // ajout des nouvelles entree
            foreach (ElementGTD newEntreeItem in elementADeplacer)
            {
                gestionnaire.ListeEntrees.Add(newEntreeItem);
            }

            // enlève les suivies de la liste
            foreach (ElementGTD suiviElement in elementADeplacer)
            {
                gestionnaire.ListeSuivis.Remove(suiviElement);

            }
        }

        /// <summary>
        /// methode eprmettant de charger les entrees presentes dans le fichier xml de depart
        /// </summary>
        /// <param name="nomFichier">represente le fichier à lire</param>
        private void ChargerEntrees(string nomFichier)
        {
            if (!File.Exists(nomFichier))
            {
                return;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(nomFichier);
            XmlNodeList ListeElement = doc.DocumentElement.GetElementsByTagName("element_gtd");
            // les elements sont chargés dans la liste appropriée dépendemment du statut
            foreach (XmlElement element in ListeElement)
            {
                if (element.GetAttribute("statut") == "Entree")
                {
                    gestionnaire.ListeEntrees.Add(new ElementGTD(element));
                }
                if (element.GetAttribute("statut") == "Action")
                {
                    gestionnaire.ListeActions.Add(new ElementGTD(element));
                }
                if (element.GetAttribute("statut") == "Suivi")
                {
                    gestionnaire.ListeSuivis.Add(new ElementGTD(element));

                }
                if (element.GetAttribute("statut") == "Archive")
                {
                    gestionnaire.ListeArchive.Add(new ElementGTD(element));
                }
            }
        }
        //trouvé grace à chat gpt permet de de jumeller une methode à la fermeture du fichier 
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SauvegarderFichier(pathFichier);
        }
        // Sauvegarde la liste de contacts de la liste dans un fichier xml liste par liste
        void SauvegarderFichier(string pathFichier)
        {
            XmlDocument document = new XmlDocument();
            XmlElement racine = document.CreateElement("gtd");
            document.AppendChild(racine);

            foreach (ElementGTD e in gestionnaire.ListeEntrees)
            {
                XmlElement element = e.VersXML(document);
                racine.AppendChild(element);
            }
            foreach (ElementGTD e in gestionnaire.ListeActions)
            {
                XmlElement element = e.VersXML(document);
                element.SetAttribute("dateRappel", e.DateRappel);
                racine.AppendChild(element);
            }
            foreach (ElementGTD e in gestionnaire.ListeSuivis)
            {
                XmlElement element = e.VersXML(document);
                element.SetAttribute("dateRappel", e.DateRappel);
                racine.AppendChild(element);
            }
            foreach (ElementGTD e in gestionnaire.ListeArchive)
            {
                XmlElement element = e.VersXML(document);
                racine.AppendChild(element);
            }
            document.Save(pathFichier);
        }
    }
}
