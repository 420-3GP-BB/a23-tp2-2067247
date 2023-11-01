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
        void fermerAide(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
