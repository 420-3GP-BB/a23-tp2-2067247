using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ClassesAffaire
{/// <summary>
/// interface permettant de convertir des element xml en objet et vise verça
/// </summary>
    public interface IconversionXML
    {
        // convertit un objet en element xml
        public XmlElement VersXML(XmlDocument doc);
        // convertit un élément xml en objet
        public void DeXML(XmlElement elem);
    }
}
