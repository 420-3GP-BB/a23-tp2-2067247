using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour WindowAPropos.xaml
    /// </summary>
    public partial class WindowAPropos : Window
    {
        public WindowAPropos()
        {
//méthode permettant de fermer la fenetre a propos en cliquant sur le bouton OK
            InitializeComponent();
             
        }

// routed commande pour pouvoir fermer la fenetre 
        public static RoutedCommand AProposCmd = new RoutedCommand();

        private void APropos_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void APropos_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
       
    }
}
