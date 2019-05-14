using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ValidarIBAN;

namespace IBANTest
{
    [TestFixture]
    public class IBANTest
    {

        [Test]
        public void ElDigitoDeControlSeCalculaCorrectamente()
        {
            int acumulador1 = 6;    //debe devolver 11 - el numero insertado (5)
            int acumulador2 = 10;   // debe devolver 1
            int acumulador3 = 11;   // debe devolver 0

            Assert.AreEqual("5", IBAN.CalculoAculmulador(acumulador1));
            Assert.AreEqual("1", IBAN.CalculoAculmulador(acumulador2));
            Assert.AreEqual("0", IBAN.CalculoAculmulador(acumulador3));
        }

        [Test]
        public void ElCCEsValido()
        {
            string CC = "20852066623456789011";
            Assert.IsTrue(IBAN.ValidarCC(CC));
        }

        [Test]
        public void ElCCNoTieneLaLongitudCorrecta()
        {
            try
            {
                string CC = "123458001234567890";
                IBAN.ValidarCC(CC);
                Assert.Fail("Longitud Incorrecta");
            }
            catch (LongitudIncorrectaException)
            { 
                //algo
            }
        }

        [Test]
        public void ElCCTieneUnformatoIncorrecto()
        {
            try
            {
                string CC =  "12345j6789001234h56h";
                IBAN.ValidarCC(CC);
                Assert.Fail("Formato mal");
            }
            catch (ParametroFormatoIncorrecto)
            {
                //algo
            }
        }

        [Test]
        public void ElIBANSeCalculaCorrectamente()
        { 
            string CC =  "20852066623456789011";

            Assert.AreEqual("17", IBAN.CalcularIBAN(CC));
        }

        [Test]
        public void AlPasarleTodoJuntoCalculaCorrectamenteTantoIBANComoCC()
        {
            string CC = "ES1720852066623456789011";

            Assert.IsTrue(IBAN.Validar(CC));
        }

        [Test]
        public void DevuelveFalsoConUnNumeroDeControlInCorrectoEnELIBAN()
        {
            string CC = "ES1220852066623456789011";

            Assert.IsFalse(IBAN.Validar(CC));
        }


        [Test]
        public void DevuelveFalsoConUnNumeroDeControlInCorrectoEnELCC()
        {
            string CC = "ES1720852066223456789011";

            Assert.IsFalse(IBAN.Validar(CC));
        }
    }
}
