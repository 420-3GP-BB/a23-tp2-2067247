using GTD;
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
    /// Logique d'interaction pour WindowTraiterAction.xaml
    /// </summary>
    public partial class WindowTraiterAction : Window
    {
        public WindowTraiterAction(ElementGTD element)
        {
            actionSelectionne = element;
            InitializeComponent();
            titre.Text = element.Nom;
            decription.Text = element.Description;
        }
        ElementGTD actionSelectionne;

        //déclaration d'un gestionnaire patagé entre les fenêtres obtenu grâce à chat gpt
        GestionnaireGTD sharedGestionnaire = (GestionnaireGTD)Application.Current.MainWindow.DataContext;
        private void terminer_Click(object sender, RoutedEventArgs e)
        {
            if (titre.Text != "")
            {
                actionSelectionne.Nom = titre.Text;
            }
            actionSelectionne.Description = decription.Text;
            actionSelectionne.Statut = "Archive";
            sharedGestionnaire.ListeActions.Remove(actionSelectionne);
            sharedGestionnaire.ListeArchive.Add(actionSelectionne);

            this.Close();
        }
        /// <summary>
        /// Permet de modifier le nom et la description en fermant la fenetre quand on clique sur poursuivre
        /// </summary>
        private void poursuivre_Click(object sender, RoutedEventArgs e)
        {
            if (titre.Text != "")
            {
                actionSelectionne.Nom = titre.Text;
            }
            actionSelectionne.Description = decription.Text;
            this.Close();

        }


    }
}
