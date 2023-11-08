using GTD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Xml.Linq;

namespace BdeBGTD
{
    /// <summary>
    /// Logique d'interaction pour WindowTraiter.xaml
    /// </summary>
    public partial class WindowTraiter : Window
    {
        private ElementGTD elementAffiche;

        public WindowTraiter(ElementGTD firstElement)
        {
            InitializeComponent();
            elementAffiche = firstElement;
            nomTraiter.Text = firstElement.Nom;
            descriptionTraiter.Text = firstElement.Description;
            bool fermerFenetre = ((MainWindow)Application.Current.MainWindow).BriserLoop;
            // j'ai du enlever les boutons car sinon on serait coicés dans une boucles tant qu'il ya des élémemnts dans la listeEntrées
            WindowStyle = WindowStyle.None;


        }

        //déclaration d'un gestionnaire patagé entre les fenêtres obtenu grâce à chat gpt
        GestionnaireGTD sharedGestionnaire = (GestionnaireGTD)Application.Current.MainWindow.DataContext;

        //ferme la fenêtre 
        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            modifierNomDescription(); 
            ((MainWindow)Application.Current.MainWindow).BriserLoop = false;
        }



        /// <summary>
        /// Permet de mettre l'entrée dans la liste de suivies en changeant son type
        /// </summary>
        public static RoutedCommand IncuberCmd = new RoutedCommand();

        private void Incuber_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;

        }

        private void Incuber_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            WindowChoixDate windowChoixDate = new WindowChoixDate("Date pour suivi");
            windowChoixDate.Owner = this;
            windowChoixDate.ShowDialog();
            // on ne fait rien tant que une date n'a pas été sélectionnée
            if (!string.IsNullOrEmpty(windowChoixDate.DateString))
            {

                elementAffiche.DateRappel = windowChoixDate.DateString;
                elementAffiche.Statut = "Suivi";
                sharedGestionnaire.ListeEntrees.Remove(elementAffiche);
                sharedGestionnaire.ListeSuivis.Add(elementAffiche);
                modifierNomDescription();
                this.Close();
            }
            else
            {
                modifierNomDescription();
                this.Close();
            }


        }




        /// <summary>
        /// Permet d'effacer de laliste d'entrées et de l'archiver 
        /// </summary>
        public static RoutedCommand ActionRapideCmd = new RoutedCommand();

        private void ActionRapide_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ActionRapide_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            sharedGestionnaire.ListeEntrees.Remove(elementAffiche);
            elementAffiche.Statut = "Archive";
            modifierNomDescription();
            sharedGestionnaire.ListeArchive.Add(elementAffiche);
            this.Close();
        }
        /// <summary>
        /// elle sera transformer en action et ne sera affichéé que lorsque la date sera passée
        /// </summary>
        public static RoutedCommand PlanifierCmd = new RoutedCommand();

        private void Planifier_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;

        }

        private void Planifier_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            WindowChoixDate windowChoixDate = new WindowChoixDate("Planifier une Action");
            windowChoixDate.Owner = this;
            windowChoixDate.ShowDialog();
            // on ne fait rien tant que une date n'a pas été sélectionnée
            if (!string.IsNullOrEmpty(windowChoixDate.DateString))
            {
                elementAffiche.DateRappel = windowChoixDate.DateString;
                elementAffiche.Statut = "Action";
                sharedGestionnaire.ListeEntrees.Remove(elementAffiche);
                modifierNomDescription();
                sharedGestionnaire.ListeActions.Add(elementAffiche);
                this.Close();
            }
            else
            {
                this.Close();
            }

        }
        /// <summary>
        /// destruction de l'entrée sans archivage
        /// </summary>
        public static RoutedCommand PoubelleCmd = new RoutedCommand();

        private void Poubelle_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Poubelle_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            sharedGestionnaire.ListeEntrees.Remove(elementAffiche);
            this.Close();
        }

        /// <summary>
        /// methode permettant de modifier le nom et la description de l'entree
        /// </summary>
        private void modifierNomDescription()
        {
            if (nomTraiter.Text != "")
            {
                elementAffiche.Nom = nomTraiter.Text;
            }
            elementAffiche.Description = descriptionTraiter.Text;
        }
    }
}
