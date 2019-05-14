using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ValidarIBAN
{

    public class LongitudIncorrectaException : Exception { }
    public class ParametroFormatoIncorrecto : Exception { }

// este .cs tiene todas las clases publicas para poder hacerle las pruebas unitarias
    public class IBAN
    {
        
        public static bool ValidarCC(string Cuenta)
        {
            return (CalcularNumeroControl(Cuenta) == Cuenta.Substring(8, 2));
        }

        public static string CalcularNumeroControl(string cadena)
        {
            int[] multiplicador = new int[] { 4, 8, 5, 10, 9, 7, 3, 6, 0, 0, 1, 2, 4, 8, 5, 10, 9, 7, 3, 6 };
            string resultado;

            int acumulador1 = 0;
            int acumulador2 = 0;


            if (cadena.Length != 20)
            {
                throw new LongitudIncorrectaException();
            }

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    acumulador1 += int.Parse(cadena[i].ToString()) * multiplicador[i];

                    acumulador2 += int.Parse(cadena[i + 10].ToString()) * multiplicador[i + 10];
                }
                catch (FormatException)
                {
                    throw new ParametroFormatoIncorrecto();
                }
            }
            resultado = CalculoAculmulador(acumulador1);
            resultado += CalculoAculmulador(acumulador2);


            return resultado;
        }

        public static string CalculoAculmulador(int acumulador)
        {
            int resultadoTmp = 11 - (acumulador % 11);
            if (resultadoTmp == 10)
                return "1";
            else if (resultadoTmp == 11)
                return "0";
            else
                return resultadoTmp.ToString();
        }

        public static string[] dividirIBAN(string cadena)
        {
            string[] IBAN = new string[5];
            IBAN[0] = cadena.Substring(0, 5);
            IBAN[1] = cadena.Substring(5, 5);
            IBAN[2] = cadena.Substring(10, 5);
            IBAN[3] = cadena.Substring(15, 5);
            IBAN[4] = cadena.Substring(20, 6);

            return IBAN;
        }

        public static string CalcularNumeroControlIBAN(string cadena)
        {
            string ES = "142800";
            string IBAN = cadena + ES;
            string[] IBANDividido = dividirIBAN(IBAN);
            string NumeroControl = string.Empty;

            for (int i = 0; i < IBANDividido.Length; i++)
            {
                NumeroControl += IBANDividido[i];
                NumeroControl = (int.Parse(NumeroControl) % 97).ToString();
            }

            NumeroControl = (98 - (int.Parse(NumeroControl))).ToString();
            if (NumeroControl.Length == 1)
            {
                NumeroControl = "0" + NumeroControl;
            }
            return NumeroControl;
        }



        public static bool ValidarIBAN(string Cadena)
        {
            string CC = Cadena.Substring(4, 20);
            string ControlIBAN = Cadena.Substring(2, 2);

            if (!ValidarCC(CC))
            {
                return false;
            }

            return ControlIBAN == CalcularNumeroControlIBAN(CC);
        }
    }
}
