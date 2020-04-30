using Codenation_Cryptography.Model;
using Codenation_Cryptography.Util;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Codenation_Cryptography
{
    public class Program
    {
        private static readonly string token = "e9ced2b2925b45238eb65d17f9ff94c191abe3fa";

        public static async Task Main(string[] args)
        {
            try
            {
                var desafio = await GetDesafio();

                Console.WriteLine($"\n>>> Desafio: {desafio.toJson()}");

                var cryptography = new Cryptography(numeroDeCasas: desafio.numero_casas);

                string textoCifrado = desafio.cifrado;

                Console.WriteLine($"\nTexto cifrado: {textoCifrado}");

                var textoDecifrado = cryptography.Decifrar(textoCifrado);

                Console.WriteLine($"\nTexto decifrado: {textoDecifrado}");

                string resumoCriptografico = Cryptography.GerarHashSHA1(textoDecifrado);

                Console.WriteLine($"\nResumo criptográfico: {resumoCriptografico}");

                var desafioCompleto = desafio;
                desafioCompleto.decifrado = textoDecifrado;
                desafioCompleto.resumo_criptografico = resumoCriptografico;

                Console.WriteLine($"\n>>> Desafio Completo: {desafioCompleto.toJson()}");

                await PostDesafio(desafioCompleto);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static async Task<Desafio> GetDesafio()
        {
            var endpoint = $"challenge/dev-ps/generate-data?token={token}";

            var json = await HttpRequest.GetData(endpoint);

            Console.WriteLine($"\nJson: {json}");

            var desafio = JsonConvert.DeserializeObject<Desafio>(json);

            return desafio;
        }

        public static async Task PostDesafio(Desafio desafio)
        {
            var endpoint = $"/challenge/dev-ps/submit-solution?token={token}";

            byte[] byteArray = Encoding.ASCII.GetBytes(desafio.toJson());

            MemoryStream stream = new MemoryStream(byteArray);

            Console.WriteLine($"\nByteArray: {byteArray}");

            Console.WriteLine($"\nStream: {stream}");

            await HttpRequest.Upload(endpoint, "answer", stream, byteArray);

        }
    }
}
