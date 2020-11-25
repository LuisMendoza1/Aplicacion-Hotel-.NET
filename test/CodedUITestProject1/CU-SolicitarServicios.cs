using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace CodedUITestProject1
{
    /// <summary>
    /// Descripción resumida de CU_SolicitarServicios
    /// </summary>
    [CodedUITest]
    public class CU_SolicitarServicios
    {
        public CU_SolicitarServicios()
        {
        }

        [TestMethod]
        public void CodedUITest_CU_SolicitarServicios()
        {

           this.UIMap.AbrirInternetExplorer();

            // Para el log-in del recepcionista
            this.UIMap.EntrarLogin();
            this.UIMap.AssertLogInShown();
            this.UIMap.RealizarLoginRecepcionista();
            this.UIMap.AssertVerificarLoginRecepcionista();

            /*
             * Flujo normal
             * */
            //Para solicitar un servicio (flujo básico)
            this.UIMap.EntrarMenuHabitaciones();
            this.UIMap.AccesoDetallesHabitacionOcupada();
            this.UIMap.AssertHabitacionOcupada();
            this.UIMap.AccesoSolicitarServicio();
            this.UIMap.AssertExisteSaunaParaSeleccionar();
            this.UIMap.SeleccionarSaunaComoServicio();
            this.UIMap.ConfimarServicioSauna();


            /*
             * Para flujo alternativo
             * */

            this.UIMap.EntrarMenuHabitaciones();
            this.UIMap.AccederHabitacionNoOcupada();
            this.UIMap.VerificarHabicaionNoOcupada();
            this.UIMap.SolicitarServicioHabitacionNoOcupada();
            this.UIMap.AssertVueltaMenuMsgNoOcupada();


            /*
             * Flujo Cancelar 'SolicitarServicio'
             * */
            this.UIMap.EntrarMenuHabitaciones();
            this.UIMap.AccesoDetallesHabitacionOcupada();
            this.UIMap.AssertHabitacionOcupada();
            this.UIMap.AccesoSolicitarServicio();
            this.UIMap.CancelarSolicitarServicio();

            /*
             * Flujo Cancelar 'ServicioSeleccionado'
            * */

            //Para solicitar un servicio (flujo básico)
            this.UIMap.EntrarMenuHabitaciones();
            this.UIMap.AccesoDetallesHabitacionOcupada();
            this.UIMap.AssertHabitacionOcupada();
            this.UIMap.AccesoSolicitarServicio();
            this.UIMap.AssertExisteSaunaParaSeleccionar();
            this.UIMap.SeleccionarSaunaComoServicio();
            this.UIMap.CancelarDesdeSeleccionarServiciosFechas();
            this.UIMap.VerificarUrlListado();
            
            this.UIMap.RealizarLogOut();

        }

        #region Atributos de prueba adicionales

        // Puede usar los siguientes atributos adicionales conforme escribe las pruebas:

        ////Use TestInitialize para ejecutar el código antes de ejecutar cada prueba 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // Para generar código para esta prueba, seleccione "Generar código para prueba de IU codificada" en el menú contextual y seleccione uno de los elementos de menú.
        //}

        ////Use TestCleanup para ejecutar el código después de ejecutar cada prueba
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // Para generar código para esta prueba, seleccione "Generar código para prueba de IU codificada" en el menú contextual y seleccione uno de los elementos de menú.
        //}

        #endregion

        /// <summary>
        ///Obtiene o establece el contexto de las pruebas que proporciona
        ///información y funcionalidad para la serie de pruebas actual.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;

        public UIMap UIMap
        {
            get
            {
                if ((this.map == null))
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
