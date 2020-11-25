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
    /// Descripción resumida de CodedUITest2
    /// </summary>
    [CodedUITest]
    public class CU_HacerPedido
    {
        public CU_HacerPedido()
        {
        }
        
        [TestMethod]
        public void CodedUITest_CU_HacerPedido_FB()
        {
            // Para generar código para esta prueba, seleccione "Generar código para prueba de IU codificada" en el menú contextual y seleccione uno de los elementos de menú.
            this.UIMap.IniciarIEyWebApp();
            this.UIMap.InicioSesionEAlmacen();
            this.UIMap.ComprobarInicioSesionEAlmacen();

            // CU - Hacer pedido FB
            this.UIMap.SeleccionarHacerPedido();
            this.UIMap.ComprobarComboBoxProveedores();
            this.UIMap.SeleccionarProveedorPedroSL();
            this.UIMap.IntroducirCantidadProductosNormal();
            this.UIMap.VerResumenPedido();
            this.UIMap.ComprobarResumenPedido();
            this.UIMap.PedidoRealizado();
            
            // Se cierra el navegador
            this.UIMap.CloseInternetExplorer();
        }

        [TestMethod]
        public void CodedUITest_CU_HacerPedido_FA1_ProveedorSinProductos()
        {
            // Para generar código para esta prueba, seleccione "Generar código para prueba de IU codificada" en el menú contextual y seleccione uno de los elementos de menú.
            this.UIMap.IniciarIEyWebApp();
            this.UIMap.InicioSesionEAlmacen();
            this.UIMap.ComprobarInicioSesionEAlmacen();

            // Se selecciona un proveedor sin productos FA1
            this.UIMap.SeleccionarHacerPedido();
            this.UIMap.ComprobarComboBoxProveedores();
            this.UIMap.SeleccionarProveedorFranciscoSLSinProd();
            this.UIMap.SeleccionarProductosSinProductos();
            this.UIMap.ErrorProveedorSinProductos();

            // Se cierra el navegador
            this.UIMap.CloseInternetExplorer();
        }

        [TestMethod]
        public void CodedUITest_CU_HacerPedido_FA2_CantidadErronea()
        {
            // Para generar código para esta prueba, seleccione "Generar código para prueba de IU codificada" en el menú contextual y seleccione uno de los elementos de menú.
            this.UIMap.IniciarIEyWebApp();
            this.UIMap.InicioSesionEAlmacen();
            this.UIMap.ComprobarInicioSesionEAlmacen();

            // Si un articulo se pide con cantidad menor que 0 o mayor que 100 muestra un mensaje de error FA2
            this.UIMap.SeleccionarHacerPedido();
            this.UIMap.ComprobarComboBoxProveedores();
            this.UIMap.SeleccionarProveedorPedroSL();
            this.UIMap.EscribirNumNegativoEnCantidades();
            this.UIMap.VerResumenNegativo();
            this.UIMap.RevisarErrorMenorCero();
            this.UIMap.SeleccionarHacerPedido();
            this.UIMap.ComprobarComboBoxProveedores();
            this.UIMap.SeleccionarProveedorPedroSL();
            this.UIMap.IntroducirCantidadProductosMayorCien();
            this.UIMap.VerResumenMayorCien();
            this.UIMap.ComprobarErrorMayorCien();

            // Se cierra el navegador
            this.UIMap.CloseInternetExplorer();
        }
        

        [TestMethod]
        public void CodedUITest_CU_HacerPedido_FA3_Cancelacion()
        {
            // Para generar código para esta prueba, seleccione "Generar código para prueba de IU codificada" en el menú contextual y seleccione uno de los elementos de menú.
            this.UIMap.IniciarIEyWebApp();
            this.UIMap.InicioSesionEAlmacen();
            this.UIMap.ComprobarInicioSesionEAlmacen();            

            // Flujo cancelacion FA3
            this.UIMap.SeleccionarHacerPedido();
            this.UIMap.ComprobarComboBoxProveedores();
            this.UIMap.SeleccionarProveedorPedroSL();
            this.UIMap.IntroducirCantidadProductosNormal();
            this.UIMap.VerResumenPedido();
            this.UIMap.ComprobarResumenPedido();
            this.UIMap.CancelarPedido();
            
            // Se cierra el navegador
            this.UIMap.CloseInternetExplorer();
        }
        
        /*
        [TestMethod]
        public void CodedUITest_CU_HacerPedido()
        {
            // Para generar código para esta prueba, seleccione "Generar código para prueba de IU codificada" en el menú contextual y seleccione uno de los elementos de menú.
            this.UIMap.IniciarIEyWebApp();
            this.UIMap.InicioSesionEAlmacen();
            this.UIMap.ComprobarInicioSesionEAlmacen();

            // CU - Hacer pedido FB
            this.UIMap.SeleccionarHacerPedido();
            this.UIMap.ComprobarComboBoxProveedores();
            this.UIMap.SeleccionarProveedorPedroSL();
            this.UIMap.IntroducirCantidadProductosNormal();
            this.UIMap.VerResumenPedido();
            this.UIMap.ComprobarResumenPedido();
            this.UIMap.PedidoRealizado();


            // Flujo cancelacion FA3
            this.UIMap.SeleccionarHacerPedido();
            this.UIMap.ComprobarComboBoxProveedores();
            this.UIMap.SeleccionarProveedorPedroSL();
            this.UIMap.IntroducirCantidadProductosNormal();
            this.UIMap.VerResumenPedido();
            this.UIMap.ComprobarResumenPedido();
            this.UIMap.CancelarPedido();


            // Se selecciona un proveedor sin productos FA1
            this.UIMap.SeleccionarHacerPedido();
            this.UIMap.ComprobarComboBoxProveedores();
            this.UIMap.SeleccionarProveedorFranciscoSLSinProd();
            this.UIMap.SeleccionarProductosSinProductos();
            this.UIMap.ErrorProveedorSinProductos();

            // Si un articulo se pide con cantidad menor que 0 o mayor que 100 muestra un mensaje de error FA2
            this.UIMap.SeleccionarHacerPedido();
            this.UIMap.ComprobarComboBoxProveedores();
            this.UIMap.SeleccionarProveedorPedroSL();
            this.UIMap.EscribirNumNegativoEnCantidades();
            this.UIMap.VerResumenNegativo();
            this.UIMap.RevisarErrorMenorCero();
            this.UIMap.IntroducirCantidadProductosMayorCien();
            this.UIMap.VerResumenMayorCien();
            this.UIMap.ComprobarErrorMayorCien();

            // Se cierra el navegador
            this.UIMap.CloseInternetExplorer();


        }
        */
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
