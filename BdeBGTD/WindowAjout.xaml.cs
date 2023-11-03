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
        // routed commande pour pouvoir fermer la fenetre 
        public static RoutedCommand ConfirmerCmd = new RoutedCommand();

        private void Confirmer_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {   // est seulement possible quand le nom n'est pas vide
            if (!string.IsNullOrEmpty(nomAjout.Text))
            { 
            e.CanExecute = true;
            }else { e.CanExecute = false; }
        }

        private void Confirmer_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ElementGTD nouvelleentree = new ElementGTD(nomAjout.Text,descriptionAjout.Text,"Entree");
            
        }
        // routed commande pour pouvoir fermer la fenetre 
        public static RoutedCommand AnnulerCmd = new RoutedCommand();

        private void Annuler_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Annuler_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            nomAjout.Text ="";
            descriptionAjout.Text = "";
            this.Close();
        }


    }
}
