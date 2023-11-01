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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BdeBGTD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DateTime dateAffichee;
        public MainWindow()
        {
        InitializeComponent();

            // date.Text = DateTime.Now.ToShortDateString();
            date.Text = dateAffichee.ToShortDateString();
            InitializeComponent();
           dateAffichee = DateTime.Now;
            ChangerDate(dateAffichee);
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
        //Ajoute 2 jours a la date affichée
            dateAffichee = dateAffichee.AddDays(2);
            ChangerDate(dateAffichee);
        }
        //met à jour la date àpres l'ajout de 2 jours
        private void ChangerDate(DateTime nouvelleDate)
        {
            date.Text = nouvelleDate.ToShortDateString();
        }
    }
}
