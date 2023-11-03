using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClassesAffaire;
using GTD;
using BdeBGTD;
using System.Collections.ObjectModel;

namespace BdeBGTD 
{
    /// <summary>
    /// Logique d'interaction pour WindowAjout.xaml
    /// </summary>
    public partial class WindowAjout : Window
    {
        public WindowAjout()
        {
            InitializeComponent();
            
        }
       private Boolean checkee= false;
        //déclaration d'un gestionnaire patagé entre les fenêtres obtenu grâce à chat gpt
        GestionnaireGTD sharedGestionnaire = (GestionnaireGTD)Application.Current.MainWindow.DataContext;
        // routed commande pour pouvoir fermer la fenetre 
        public static RoutedCommand ConfirmerCmd = new RoutedCommand();

        private void Confirmer_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {   // est seulement possible quand le nom n'est pas vide
            if (!string.IsNullOrEmpty(nomAjout.Text))
            { 
            e.CanExecute = true;
            }else { e.CanExecute = false; }
        }
//commande qui s'execure quand on appuie sur confirmer
        private void Confirmer_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ElementGTD nouvelleentree = new ElementGTD(nomAjout.Text,descriptionAjout.Text,"Entree");
           sharedGestionnaire.ListeEntrees.Add(nouvelleentree);
            nomAjout.Text = "";
            descriptionAjout.Text = "";
            // la fenêtre reste ouverte seulement si la case est cochée
            if(checkee==false)
            { this.Close(); }

        }
        // routed commande pour pouvoir fermer la fenetre 
        public static RoutedCommand AnnulerCmd = new RoutedCommand();

        private void Annuler_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
// si l'utilisateur clicque sur annuler les textbox sont vidées et la fenêtre est fermée
        private void Annuler_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            nomAjout.Text ="";
            descriptionAjout.Text = "";
            this.Close();
        }
// les deux méthodes changent la valeur du boolean checkée dépendemment de l'état de la checkbox
        private void checkAjout_Checked(object sender, RoutedEventArgs e)
        {
            checkee = true;
        }

        private void checkAjout_Unchecked(object sender, RoutedEventArgs e)
        {
            checkee=false;
        }
    }
}
