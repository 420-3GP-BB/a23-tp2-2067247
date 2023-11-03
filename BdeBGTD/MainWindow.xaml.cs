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
        public static ObservableCollection<ElementGTD> ListeEntrees = gestionnaire.ListeEntrees;
        public static ObservableCollection<ElementGTD> ListeActions = gestionnaire.ListeActions;
        public static ObservableCollection<ElementGTD> ListeSuivis = gestionnaire.ListeSuivis;
        public static ObservableCollection<ElementGTD> ListeArchive = gestionnaire.ListeArchive;
        public MainWindow()
        {
        InitializeComponent();

            
           
           dateAffichee = DateTime.Now;
           date.Text = dateAffichee.ToShortDateString();
           



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

        public static void UpdateListCount()
        {
            affichageListe.Text = ListeEntrees.Count.ToString();
        }
    }
}
