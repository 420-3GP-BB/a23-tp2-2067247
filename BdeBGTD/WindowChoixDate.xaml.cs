using GTD;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;

namespace BdeBGTD
{
    /// <summary>
    /// Logique d'interaction pour WindowChoixDate.xaml
    /// </summary>
    public partial class WindowChoixDate : Window
    {
        public WindowChoixDate( string titre)
        {
            InitializeComponent();
            title.Text = titre;
           
        }
        public string? DateString { get; private set; }
        // routed commande pour pouvoir fermer la fenetre 
        public static RoutedCommand AnnulerCmd = new RoutedCommand();

        private void Annuler_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        // si l'utilisateur clicque sur annuler lla fenêtre est fermée
        private void Annuler_Executed(object sender, ExecutedRoutedEventArgs e)
        {
           
            this.Close();
        }

        private void Calendar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (calendrier.SelectedDate.HasValue)
            {
                DateTime dateSaisie = calendrier.SelectedDate.Value;
                DateString = dateSaisie.ToShortDateString();
            }
            else
            {
                DateString = "";
            }
            this.Close();

        }
    }
}
