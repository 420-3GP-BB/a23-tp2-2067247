﻿using ClassesAffaire;
using System.Xml;

namespace GTD
{
    public class ElementGTD : IconversionXML
    {
        /// <summary>
        /// constructeur à partir du fichier xml
        /// </summary>
        /// <param name="elementXml"> element xml à parrtir duquel l'elelment gtd est cree</param>
        public ElementGTD(XmlElement elementXml)
        {
            DeXML(elementXml);
        }
        /// <summary>
        /// creation d'une element gtd dans le programme
        /// </summary>
        /// <param name="nomTache"> nom de la tache</param>
        /// <param name="descriptionTache"> desction de la tache</param>
        /// <param name="statutTache"> statut de la taxe</param>
        public ElementGTD(string nomTache, string descriptionTache, string statutTache)
        {
            Nom = nomTache;
            Description = descriptionTache;
            Statut = statutTache;
        }

        // nom de la tache, c'est un attribut obligatoire
        public String Nom
        {
            set;
            get;
        }
        // statut de la tache, c'est un attribut obligatoire
        public String Statut
        {
            set;
            get;
        }
        // description de la tache
        public String Description
        {
            set;
            get;
        }
        // date de rappel de la tache pour les taches incubées, ou les actions planifiées
        public String DateRappel
        {
            set;
            get;
        }



        /// <summary>
        /// methode permettant de convertir un élément xml en objet
        /// </summary>
        /// <param name="elem"> represente l'elelemt xml que l'on souhaite convertir</param>
        public void DeXML(XmlElement elem)
        {
            Nom = elem.GetAttribute("nom");
            Statut = elem.GetAttribute("statut");
            // etant donnée que la description n'est pas obligatoire on verifie si elle est presente avant de la charger
            if (elem.HasChildNodes)
            {
                Description = elem.FirstChild.InnerText.Trim();
            }
            else
            {
                Description = string.Empty;
            }// etant donnée que la date n'est pas obligatoire on verifie si elle est presente avant de la charger
            if (elem.HasAttribute("dateRappel"))
            {
                DateRappel = elem.GetAttribute("dateRappel").Trim();
            }

        }

        /// <summary>
        /// methode permettant de convertir un élément xml en objet
        /// </summary>
        /// <param name="doc"> le document xml que l'on rempli</param>
        /// <returns></returns>
        public XmlElement VersXML(XmlDocument doc)
        {
            XmlElement element_gtd = doc.CreateElement("element_gtd");
            element_gtd.SetAttribute("nom", Nom);
            element_gtd.SetAttribute("statut", Statut);

            //on ajoute la date seulement si elle est présente
            if (DateRappel != null)
            {
                element_gtd.SetAttribute("dateRappel", DateRappel);
            }


            if (!string.IsNullOrEmpty(Description))
            {
                // On ajoute la description seuelemt si elle n'est pas vide
                XmlElement elementDescription = doc.CreateElement("description");
                elementDescription.InnerText = Description;
                element_gtd.AppendChild(elementDescription);
            }

            return element_gtd;
        }
        /// <summary>
        /// retourne un text ôur l'affiche de la listeSuivis
        /// </summary>
        public string SuiviText
        {
            get
            {
                return $"{Nom} ({DateRappel})";
            }
        }
    }
}