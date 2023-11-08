using GTD;
using System.Xml;
using BdeBGTD;

namespace Tests
{
    public class TestGestionnaireGTD
    {
        private GestionnaireGTD _gestionnaireGTD;
        private string date = "2023-11-05";
        private string element_gtd;
        private string nom = "test1";
        private string statut = "Suivi";
        private string description = "Ceci est un test";
        private string dateRappel = "2023-11-07";
        private ElementGTD? elem;
        [SetUp]
        public void Setup()
        {
            _gestionnaireGTD = new GestionnaireGTD();
            element_gtd = @$"<element_gtd nom=""{nom}"" statut=""{statut}"">
                            <dateRappel= ""{dateRappel}""  />
                            <description>{description}</description>
                         </element_gtd>";


        }
        void ChargerElementXML()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(element_gtd);
            elem = new ElementGTD(doc.DocumentElement);
        }

        // Premier test de l'énoncé
        /// <summary>
        /// Mes actions postérieures sont dans la liste, c'est jusye qu'elle ne seront affichées que lorsque la date est passée
        /// </summary>
        [Test]
        public void TestActionPosterieurePasDansListe()
        {
            // ARRANGE
            // ACT
            ChargerElementXML();

            // ASSERT


            Assert.Pass();
        }

        // Deuxième test de l'énoncé
        [Test]
        public void TestActionVientDansProchaineAction()
        {
            Assert.Pass();
        }

        // Troisième test de l'énoncé
        [Test]
        public void TestSuiviPasseAEntree()
        {
            // ARRANGE
            ChargerElementXML();

            // ACT
            _gestionnaireGTD.ListeSuivis.Add(elem); // Add the element to ListeSuivis initially
            elem.DateRappel = "2023-11-08";


            // ASSERT
            Assert.IsFalse(_gestionnaireGTD.ListeSuivis.Contains(elem)); // elem ne devrait plus être dans listeSuivis
            Assert.IsTrue(_gestionnaireGTD.ListeEntrees.Contains(elem)); // elem devrait être dans listeEntrees
        }
    }

}