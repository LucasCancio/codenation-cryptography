using System;
using System.Collections.Generic;
using System.Text;

namespace Codenation_Cryptography
{
    public class Cryptography
    {
        public Cryptography(int numeroDeCasas)
        {
            this.numeroDeCasas= numeroDeCasas;
        }

        private readonly List<char> Alfabeto = new List<char>(){
            'a', 'b', 'c', 'd',
            'e', 'f', 'g', 'h',
            'i', 'j', 'k', 'l',
            'm', 'n', 'o', 'p',
            'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y', 'z'
        };

        public int numeroDeCasas { get; set; }


        public string Decifrar(string textoCifrado)
        {
            string textoDecifrado = "";

            char[] caracteres = textoCifrado.ToLower().ToCharArray();

            foreach (char caracter in caracteres)
            {
                if (Alfabeto.Contains(caracter))
                {
                    int caracterCifradoIndex = Alfabeto.IndexOf(caracter);
                    int caracterDecifradoIndex = caracterCifradoIndex - numeroDeCasas;

                    if (caracterDecifradoIndex < 0)
                    {
                        caracterDecifradoIndex = Alfabeto.Count - (caracterDecifradoIndex * -1);
                    }

                    char caracterDecifrado = Alfabeto[caracterDecifradoIndex];

                    textoDecifrado += caracterDecifrado;
                }
                else
                {
                    textoDecifrado += caracter;
                }
            }

            return textoDecifrado;
        }

        public string Cifrar(string texto)
        {
            string textoCifrado = "";

            char[] caracteres = texto.ToLower().ToCharArray();

            foreach (char caracter in caracteres)
            {
                if (Alfabeto.Contains(caracter))
                {
                    int caracterIndex = Alfabeto.IndexOf(caracter);
                    int caracterCifradoIndex = caracterIndex + numeroDeCasas;

                    if (caracterCifradoIndex >= Alfabeto.Count)
                    {
                        caracterCifradoIndex = caracterCifradoIndex - Alfabeto.Count;
                    }

                    char caracterDecifrado = Alfabeto[caracterCifradoIndex];

                    textoCifrado += caracterDecifrado;
                }
                else
                {
                    textoCifrado += caracter;
                }
            }

            return textoCifrado;
        }

        public static string GerarHashSHA1(string texto)
        {
            try
            {
                byte[] buffer = Encoding.Default.GetBytes(texto);
                System.Security.Cryptography.SHA1CryptoServiceProvider cryptoTransformSHA1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
                string hash = BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
                return hash;
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
        }
    }
}
