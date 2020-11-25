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
    /// Descripción resumida de CodedUITest1
    /// </summary>
    [CodedUITest]
    public class CU_HacerReserva_test
    {
        public CU_HacerReserva_test()
        {
        }

        [TestMethod]
        public void CodedUITest_CU_HacerReserva_FB()
        {
            // Arrange
            this.UIMap.InicioInternetExplorer();
            this.UIMap.LogInRecepcionistaMethod();
            this.UIMap.ClickReservasMethod();
            this.UIMap.CrearNuevaReservaMethod();
            this.UIMap.ListaClienteAssert();
            // Paso 1
            this.UIMap.FilterClientByDNIMethod();
            // Paso 2
            this.UIMap.ClienteDNIEncontradoAssert();
            // Paso 3
            this.UIMap.ViewClientDetailsMethod();
            // Paso 4
            this.UIMap.DatosClienteCorrectosAssert();
            // Paso 5
            this.UIMap.HacerReservaClienteMethod();
            // Paso 6
            this.UIMap.FormularioReservaAssert();
            // Paso 7
            this.UIMap.IntroducirDatosReservaMethodParams.UIFechaInicioEditText = DateTime.Today.ToString("yyyy-MM-dd");
            this.UIMap.IntroducirDatosReservaMethodParams.UIFechaFinEditText = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
            this.UIMap.IntroducirDatosReservaMethod();
            // Paso 8
            this.UIMap.FormularioDescHabitacionesAssert();
            // Paso 9
            this.UIMap.SeleccionHabitacionDescMethod();
            // Paso 10
            this.UIMap.ListaClienteAssert();
        }

        [TestMethod]
        public void CodedUITest_CU_HacerReserva_FA2_DNINotFound()
        {
            this.UIMap.InicioInternetExplorer();
            this.UIMap.LogInRecepcionistaMethod();
            this.UIMap.ClickReservasMethod();
            this.UIMap.CrearNuevaReservaMethod();
            this.UIMap.ListaClienteAssert();
            // Paso 1
            this.UIMap.FilterClientNotFoundMethod();
            // Paso 2 Alternativo
            this.UIMap.NoClientsFoundFilterAssert();

            // Se comprueba si se esta en el listado de clientes para poder continuar por el paso 1
            this.UIMap.ListaClienteAssert();
        }

        [TestMethod]
        public void CodedUITest_CU_HacerReserva_FA8_SinDatos()
        {
            // Arrange
            this.UIMap.InicioInternetExplorer();
            this.UIMap.LogInRecepcionistaMethod();
            this.UIMap.ClickReservasMethod();
            this.UIMap.CrearNuevaReservaMethod();
            this.UIMap.ListaClienteAssert();
            // Paso 1
            this.UIMap.FilterClientByDNIMethod();
            // Paso 2
            this.UIMap.ClienteDNIEncontradoAssert();
            // Paso 3
            this.UIMap.ViewClientDetailsMethod();
            // Paso 4
            this.UIMap.DatosClienteCorrectosAssert();
            // Paso 5
            this.UIMap.HacerReservaClienteMethod();
            // Paso 6
            this.UIMap.FormularioReservaAssert();
            // Paso 7 - Sin Datos
            this.UIMap.ReservaContinuarSinRellenarMethod();
            // Paso 8 Alternativo
            this.UIMap.FormularioReservaAssert();
        }

        [TestMethod]
        public void CodedUITest_CU_HacerReserva_FA8_InicioFinInvertidos()
        {
            // Arrange
            this.UIMap.InicioInternetExplorer();
            this.UIMap.LogInRecepcionistaMethod();
            this.UIMap.ClickReservasMethod();
            this.UIMap.CrearNuevaReservaMethod();
            this.UIMap.ListaClienteAssert();
            // Paso 1
            this.UIMap.FilterClientByDNIMethod();
            // Paso 2
            this.UIMap.ClienteDNIEncontradoAssert();
            // Paso 3
            this.UIMap.ViewClientDetailsMethod();
            // Paso 4
            this.UIMap.DatosClienteCorrectosAssert();
            // Paso 5
            this.UIMap.HacerReservaClienteMethod();
            // Paso 6
            this.UIMap.FormularioReservaAssert();
            // Paso 7 - Fecha fin no superior a inicio
            this.UIMap.IntroducirDatosReservaMethodParams.UIFechaInicioEditText = DateTime.Today.ToString("yyyy-MM-dd");
            this.UIMap.IntroducirDatosReservaMethodParams.UIFechaFinEditText = DateTime.Today.ToString("yyyy-MM-dd");
            this.UIMap.IntroducirDatosReservaMethod();
            // Paso 8 Alternativo
            this.UIMap.FormularioReservaAssert();
            this.UIMap.FinMenorEqInicioAssert();
        }

        [TestMethod]
        public void CodedUITest_CU_HacerReserva_FA8_InicioAnteriorHoy()
        {
            // Arrange
            this.UIMap.InicioInternetExplorer();
            this.UIMap.LogInRecepcionistaMethod();
            this.UIMap.ClickReservasMethod();
            this.UIMap.CrearNuevaReservaMethod();
            this.UIMap.ListaClienteAssert();
            // Paso 1
            this.UIMap.FilterClientByDNIMethod();
            // Paso 2
            this.UIMap.ClienteDNIEncontradoAssert();
            // Paso 3
            this.UIMap.ViewClientDetailsMethod();
            // Paso 4
            this.UIMap.DatosClienteCorrectosAssert();
            // Paso 5
            this.UIMap.HacerReservaClienteMethod();
            // Paso 6
            this.UIMap.FormularioReservaAssert();
            // Paso 7 - Fecha inicio anterior a hoy
            this.UIMap.IntroducirDatosReservaMethodParams.UIFechaInicioEditText = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
            this.UIMap.IntroducirDatosReservaMethodParams.UIFechaFinEditText = DateTime.Today.ToString("yyyy-MM-dd");
            this.UIMap.IntroducirDatosReservaMethod();
            // Paso 8 Alternativo
            this.UIMap.FormularioReservaAssert();
            this.UIMap.FechaAnteriorAHoyAssert();
        }

        [TestMethod]
        public void CodedUITest_CU_HacerReserva_FA8_NoHayHabitacion()
        {
            // Arrange
            this.UIMap.InicioInternetExplorer();
            this.UIMap.LogInRecepcionistaMethod();
            this.UIMap.ClickReservasMethod();
            this.UIMap.CrearNuevaReservaMethod();
            this.UIMap.ListaClienteAssert();
            // Paso 1
            this.UIMap.FilterClientByDNIMethod();
            // Paso 2
            this.UIMap.ClienteDNIEncontradoAssert();
            // Paso 3
            this.UIMap.ViewClientDetailsMethod();
            // Paso 4
            this.UIMap.DatosClienteCorrectosAssert();
            // Paso 5
            this.UIMap.HacerReservaClienteMethod();
            // Paso 6
            this.UIMap.FormularioReservaAssert();
            // Paso 7 - Conflicto con otra reserva
            this.UIMap.IntroducirDatosReservaMethodParams.UIFechaInicioEditText = DateTime.Today.AddDays(3).ToString("yyyy-MM-dd");
            this.UIMap.IntroducirDatosReservaMethodParams.UIFechaFinEditText = DateTime.Today.AddDays(4).ToString("yyyy-MM-dd");
            this.UIMap.IntroducirDatosReservaMethod();
            // Paso 8 Alternativo
            this.UIMap.FormularioReservaAssert();
            this.UIMap.NoHayHabitacionReservaAssert();
        }

        [TestMethod]
        public void CodedUITest_CU_HacerReserva_FA10_HabitacionNoSeleccionada()
        {
            // Arrange
            this.UIMap.InicioInternetExplorer();
            this.UIMap.LogInRecepcionistaMethod();
            this.UIMap.ClickReservasMethod();
            this.UIMap.CrearNuevaReservaMethod();
            this.UIMap.ListaClienteAssert();
            // Paso 1
            this.UIMap.FilterClientByDNIMethod();
            // Paso 2
            this.UIMap.ClienteDNIEncontradoAssert();
            // Paso 3
            this.UIMap.ViewClientDetailsMethod();
            // Paso 4
            this.UIMap.DatosClienteCorrectosAssert();
            // Paso 5
            this.UIMap.HacerReservaClienteMethod();
            // Paso 6
            this.UIMap.FormularioReservaAssert();
            // Paso 7
            this.UIMap.IntroducirDatosReservaMethodParams.UIFechaInicioEditText = DateTime.Today.ToString("yyyy-MM-dd");
            this.UIMap.IntroducirDatosReservaMethodParams.UIFechaFinEditText = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
            this.UIMap.IntroducirDatosReservaMethod();
            // Paso 8
            this.UIMap.FormularioDescHabitacionesAssert();
            // Paso 9
            this.UIMap.GuardarReservaSinHabitacionMethod();

            // Paso 10
            this.UIMap.FormularioDescHabitacionesAssert();
            this.UIMap.ErrorNoHabitacionAssert();
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
