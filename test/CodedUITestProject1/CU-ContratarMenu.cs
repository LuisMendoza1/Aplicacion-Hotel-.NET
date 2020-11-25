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
using CodedUITestProject1;

namespace TeamCatHotel.test
{
    /// <summary>
    /// Descripción resumida de CodedUITest2
    /// </summary>
    /// Caso de Uso: Contratar Menu. Realizado por: Hernan Indibil de la Cruz Calvo
    /// Pruebas creadas por: Alejandro Moya Moya.
    [CodedUITest]
    public class CodedUITest2
    {

        public CodedUITest2()
        {
        }

        [TestMethod]
        public void CodedUITest_CU_ContratarMenu()
        {
            // Para generar código para esta prueba, seleccione "Generar código para prueba de IU codificada" en el menú contextual y seleccione uno de los elementos de menú.

            // OJO, para la ejecución de este método de prueba hay que poner la hora del sistema a las 14:00

            // Arranque del navegador e inicio de sesión como camarero
            this.UIMap.InicioInternetExplorer();
            this.UIMap.InicioSesionCamarero();
            // Assert
            this.UIMap.ComprobacionInicioSesionCamarero();



            // Contratar menu correcto. UCI-1
            this.UIMap.ContratarMenuConExito();
            // Assert
            this.UIMap.ComprobacionMensajeCorrectoContratacionMenu();
            this.UIMap.ComprobacionTextBoxNumHabitacionVacio();
            this.UIMap.ComprobacionWebContratarMenu();



            // Contratar menu fuera de su hora establecida. UCI-2
            this.UIMap.ContratarMenuFueraDeHora();

            // Assert
            this.UIMap.ComprobacionMensajeErrorFueraDeHoraMenu();
            this.UIMap.ComprobacionWebContratarMenu();



            // Contratar menu con un num.Habitación sin reserva en proceso asociada. UCI-3
            this.UIMap.ContratarMenuConReservaNoValida();
            // Assert
            this.UIMap.ComprobacionMensajeErrorReservaNoEncontrada();
            this.UIMap.ComprobacionWebContratarMenu();



            // Contratar menu con un num.Habitación sin un regimen de comidas oportuno (sin regimen completo). UCI-4
            this.UIMap.ContratarMenuSinRegimenOportuno();
            // Assert
            this.UIMap.ComprobacionMensajeCorrectoContratacionMenu();
            this.UIMap.ComprobacionTextBoxNumHabitacionVacio();



            // Cancelación del proceso cuando se quiera. UCI-5
            // Cancelar proceso sin darle a confirmar, FB + FA Cancelar
            this.UIMap.ContratarMenuCancelarPrimerContratar();
            this.UIMap.ComprobacionTextBoxNumHabitacionVacio();
            this.UIMap.ComprobacionWebContratarMenu();



            // Cancelación del proceso cuando se quiera. UCI-6
            // Cancelar proceso error fuera de hora, FB + FA Paso 2 / Fuera de Hora + FA Cancelacion
            this.UIMap.ContratarMenuFueraDeHora();
            this.UIMap.ClickCancelarContratarMenu();
            this.UIMap.ComprobacionTextBoxNumHabitacionVacio();
            this.UIMap.ComprobacionWebContratarMenu();



            // Cancelación del proceso cuando se quiera. UCI-7
            // Cancelar proceso error num.Habitacion sin reserva, FB + FA Paso 2 / num.Habitación sin reserva + FA Cancelacion
            this.UIMap.ContratarMenuConReservaNoValida();
            this.UIMap.ClickCancelarContratarMenu();
            this.UIMap.ComprobacionTextBoxNumHabitacionVacio();
            this.UIMap.ComprobacionWebContratarMenu();



            // Cancelación del proceso cuando se quiera. UCI-8
            // Cancelar proceso en la ventana del número de comensales, FB + FA Paso 4 + FA Cancelacion
            this.UIMap.ContratarMenuCancelarNumeroComensales();
            this.UIMap.ComprobacionTextBoxNumHabitacionVacio();
            this.UIMap.ComprobacionWebContratarMenu();

        }

        
        [TestMethod]
        public void CodedUITest_CU_ContratarMenu_UCI1()
        {
            // Arranque del navegador e inicio de sesión como camarero
            this.UIMap.InicioInternetExplorer();
            this.UIMap.InicioSesionCamarero();
            // Assert
            this.UIMap.ComprobacionInicioSesionCamarero();

            // Contratar menu correcto. UCI-1
            this.UIMap.ContratarMenuConExito();
            // Assert
            this.UIMap.ComprobacionMensajeCorrectoContratacionMenu();
            this.UIMap.ComprobacionTextBoxNumHabitacionVacio();
            this.UIMap.ComprobacionWebContratarMenu();

        }

        [TestMethod]
        public void CodedUITest_CU_ContratarMenu_UCI2()
        {
            // Arranque del navegador e inicio de sesión como camarero
            this.UIMap.InicioInternetExplorer();
            this.UIMap.InicioSesionCamarero();
            // Assert
            this.UIMap.ComprobacionInicioSesionCamarero();

            // Contratar menu fuera de su hora establecida. UCI-2
            this.UIMap.ContratarMenuFueraDeHora();

            // Assert
            this.UIMap.ComprobacionMensajeErrorFueraDeHoraMenu();
            this.UIMap.ComprobacionWebContratarMenu();
        }

        [TestMethod]
        public void CodedUITest_CU_ContratarMenu_UCI3()
        {
            // Arranque del navegador e inicio de sesión como camarero
            this.UIMap.InicioInternetExplorer();
            this.UIMap.InicioSesionCamarero();
            // Assert
            this.UIMap.ComprobacionInicioSesionCamarero();

            // Contratar menu con un num.Habitación sin reserva en proceso asociada. UCI-3
            this.UIMap.ContratarMenuConReservaNoValida();
            // Assert
            this.UIMap.ComprobacionMensajeErrorReservaNoEncontrada();
            this.UIMap.ComprobacionWebContratarMenu();
        }

        [TestMethod]
        public void CodedUITest_CU_ContratarMenu_UCI4()
        {
            // Arranque del navegador e inicio de sesión como camarero
            this.UIMap.InicioInternetExplorer();
            this.UIMap.InicioSesionCamarero();
            // Assert
            this.UIMap.ComprobacionInicioSesionCamarero();

            // Contratar menu con un num.Habitación sin un regimen de comidas oportuno (sin regimen completo). UCI-4
            this.UIMap.ContratarMenuSinRegimenOportuno();
            // Assert
            this.UIMap.ComprobacionMensajeCorrectoContratacionMenu();
            this.UIMap.ComprobacionTextBoxNumHabitacionVacio();
        }

        [TestMethod]
        public void CodedUITest_CU_ContratarMenu_UCI5()
        {
            // Arranque del navegador e inicio de sesión como camarero
            this.UIMap.InicioInternetExplorer();
            this.UIMap.InicioSesionCamarero();
            // Assert
            this.UIMap.ComprobacionInicioSesionCamarero();

            // Cancelación del proceso cuando se quiera. UCI-5
            // Cancelar proceso sin darle a confirmar, FB + FA Cancelar
            this.UIMap.ContratarMenuCancelarPrimerContratar();
            this.UIMap.ComprobacionTextBoxNumHabitacionVacio();
            this.UIMap.ComprobacionWebContratarMenu();
        }

        [TestMethod]
        public void CodedUITest_CU_ContratarMenu_UCI6()
        {
            // Arranque del navegador e inicio de sesión como camarero
            this.UIMap.InicioInternetExplorer();
            this.UIMap.InicioSesionCamarero();
            // Assert
            this.UIMap.ComprobacionInicioSesionCamarero();

            // Cancelación del proceso cuando se quiera. UCI-6
            // Cancelar proceso error fuera de hora, FB + FA Paso 2 / Fuera de Hora + FA Cancelacion
            this.UIMap.ContratarMenuFueraDeHora();
            this.UIMap.ClickCancelarContratarMenu();
            this.UIMap.ComprobacionTextBoxNumHabitacionVacio();
            this.UIMap.ComprobacionWebContratarMenu();
        }

        [TestMethod]
        public void CodedUITest_CU_ContratarMenu_UCI7()
        {
            // Arranque del navegador e inicio de sesión como camarero
            this.UIMap.InicioInternetExplorer();
            this.UIMap.InicioSesionCamarero();
            // Assert
            this.UIMap.ComprobacionInicioSesionCamarero();

            // Cancelación del proceso cuando se quiera. UCI-7
            // Cancelar proceso error num.Habitacion sin reserva, FB + FA Paso 2 / num.Habitación sin reserva + FA Cancelacion
            this.UIMap.ContratarMenuConReservaNoValida();
            this.UIMap.ClickCancelarContratarMenu();
            this.UIMap.ComprobacionTextBoxNumHabitacionVacio();
            this.UIMap.ComprobacionWebContratarMenu();
        }

        [TestMethod]
        public void CodedUITest_CU_ContratarMenu_UCI8()
        {
            // Arranque del navegador e inicio de sesión como camarero
            this.UIMap.InicioInternetExplorer();
            this.UIMap.InicioSesionCamarero();
            // Assert
            this.UIMap.ComprobacionInicioSesionCamarero();

            // Cancelación del proceso cuando se quiera. UCI-8
            // Cancelar proceso en la ventana del número de comensales, FB + FA Paso 4 + FA Cancelacion
            this.UIMap.ContratarMenuCancelarNumeroComensales();
            this.UIMap.ComprobacionTextBoxNumHabitacionVacio();
            this.UIMap.ComprobacionWebContratarMenu();
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
