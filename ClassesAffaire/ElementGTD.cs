using ClassesAffaire;
using System.Xml;

namespace GTD
{
    public class ElementGTD : IconversionXML
    {

        public ElementGTD(XmlElement elementXml) 
        { 
        
        }
        public String Nom
        {
            private set;
            get;
        }

        public String Statut
        {
            private set;
            get;
        }

        public String Description
        {
            private set;
            get;
        }
        public DateTime DateRappel
        {
            private set;
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
            XmlElement description = elem["element_gtd"];
            Description = description.InnerText.Trim();
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
            element_gtd.SetAttribute("dateRappel", DateRappel.ToShortDateString());

            XmlElement elementDescription = doc.CreateElement("description");
            elementDescription.InnerText = Description;
            element_gtd.AppendChild(elementDescription);

            return element_gtd;
        }
    }
}