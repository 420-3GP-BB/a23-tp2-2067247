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
using ClassesAffaire;
using GTD;
using static System.Runtime.InteropServices.JavaScript.JSType;



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

        private bool briserLoop = true;
        public MainWindow()
        {
        InitializeComponent();



          
        dateAffichee = DateTime.Now;
           date.Text = dateAffichee.ToShortDateString();
            DataContext = gestionnaire;
            FiltrerActionsParDate();
            FiltrerSuiviesParDate();




        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
        //Ajoute 1 jour a la date affichée
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
        private void ClickAPropos(object sender, RoutedEventArgs e)
        {
            //Ouvre la fenetre a propos
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


        public static RoutedCommand AjoutCmd = new RoutedCommand();

        private void Ajout_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            
                e.CanExecute = true;
           
        }

        private void Ajout_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ouvrirFenetreAjout();
        }
     

        public static RoutedCommand TraiterCmd = new RoutedCommand();

        private void Traiter_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (gestionnaire.ListeEntrees.Count()>0)
            {
                e.CanExecute = true;
            }else
            { e.CanExecute = false; }
        }

        private void Traiter_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            BriserLoop = true;
            ouvrirFenetreTraiter();
        }
       
          private void ouvrirFenetreTraiter()
          {// on entre la première entrée de la liste en paramètre de la nouvelle fenêtre <traiter entrée> et rentre dans un boucle qui continue tant qu'il y a des elements dans la liste
            while (gestionnaire.ListeEntrees.Count­>0 && BriserLoop) { 
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
        private void FiltrerSuiviesParDate()
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
           ElementGTD actionGTD= (ElementGTD)action.DataContext;

            WindowTraiterAction windowTraiterAction = new WindowTraiterAction(actionGTD);
            windowTraiterAction.Owner = this;
            windowTraiterAction.ShowDialog();
            FiltrerActionsParDate();
        }

        private void miseAJourSuivi()
        {
           //creation d'Une liste temporaire qui contientiendra les elements à deplacer
            List<ElementGTD> elementADeplacer = new List<ElementGTD>();

            foreach (ElementGTD suiviElement in gestionnaire.ListeSuivis)
            {
                if (suiviElement.DateRappel == date.Text)
                {
                    // Creation d'un nouvel element GTD avec seulement un nom un statut et une description
                    ElementGTD newEntreeItem = new ElementGTD(suiviElement.Nom, suiviElement.Description, "Entree");
                    elementADeplacer.Add(newEntreeItem);

                }
            }

            // Add the new elements to ListeEntrees
            foreach (ElementGTD newEntreeItem in elementADeplacer)
            {
               gestionnaire.ListeEntrees.Add(newEntreeItem);
            }

            // Remove the matching items from ListeSuivi
            foreach (ElementGTD suiviItem in elementADeplacer)
            {
                gestionnaire.ListeSuivis.Remove(suiviItem);
                
            }
        }
    }
}
