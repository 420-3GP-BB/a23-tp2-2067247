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
// fait en sorte que  les Listes pointent sur les mêmes listes déclarées dans getsionnaireGTD
        public ObservableCollection<ElementGTD> ListeEntrees
        {
            get { return gestionnaire.ListeEntrees; }
        }
        public ObservableCollection<ElementGTD> ListeSuivis
        {
            get { return gestionnaire.ListeSuivis; }
        }
        public ObservableCollection<ElementGTD> ListeActions
        {
            get { return gestionnaire.ListeActions; }
        }
        public ObservableCollection<ElementGTD> ListeArchive
        {
            get { return gestionnaire.ListeArchive; }
        }
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

        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
        //Ajoute 1 jour a la date affichée
            dateAffichee = dateAffichee.AddDays(1);
            ChangerDate(dateAffichee);
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

        private void Ajout_Click(object sender, RoutedEventArgs e)
        {
            ouvrirFenetreAjout();

        }

        public static RoutedCommand TraiterCmd = new RoutedCommand();

        private void Traiter_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ListeEntrees.Count()>0)
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
              WindowTraiter windowTraiter = new WindowTraiter(ListeEntrees.FirstOrDefault());
              windowTraiter.Owner = this;
              windowTraiter.ShowDialog();
            }

          }

        private void FiltrerActionsParDate()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListeActions);
            view.Filter = item =>
            {
                ElementGTD element = item as ElementGTD;
                if (element != null && element.DateRappel != null)
                {
                    return string.Compare(element.DateRappel, date.Text) <= 0; 
                }
                return false; // No match
            };
        }



    }
}
