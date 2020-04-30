using Codenation_Cryptography.Model;
using Codenation_Cryptography.Util;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Codenation_Cryptography
{
    public class Program
    {
        private static string token;

        public static void Main(string[] args)
        {
            try
            {
                //await IniciarDesafio();

                var cripto = new Cryptography(numeroDeCasas: 3);
                var texto = "measuring programming progress by lines of code is like measuring aircraft building progress by weight. bill gates";

                var textoCifrado = cripto.Cifrar(texto);

                var respostaCifrada = "phdvxulqj surjudpplqj surjuhvv eb olqhv ri frgh lv olnh phdvxulqj dlufudiw exloglqj surjuhvv eb zhljkw. eloo jdwhv";

                bool estaCorreto = textoCifrado == respostaCifrada;

                Console.WriteLine($"texto cifrado: {textoCifrado}");
                Console.WriteLine($"resposta cifrada: {respostaCifrada}");
                Console.WriteLine($"texto está cifrado corretamente? {estaCorreto}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static async Task IniciarDesafio()
        {
            Console.WriteLine("Digite seu token: ");
            token = Console.ReadLine();

            var desafio = await GetDesafio();

            Console.WriteLine($"\n>>> Desafio: {desafio.toJson()}");

            var cryptography = new Cryptography(numeroDeCasas: desafio.numero_casas);

            string textoCifrado = desafio.cifrado;

            Console.WriteLine($"\nTexto cifrado: {textoCifrado}");

            var textoDecifrado = cryptography.Decifrar(textoCifrado);

            Console.WriteLine($"\nTexto decifrado: {textoDecifrado}");

            string resumoCriptografico = Cryptography.GerarHashSHA1(textoDecifrado).ToLower();

            Console.WriteLine($"\nResumo criptográfico: {resumoCriptografico}");

            var desafioCompleto = desafio;
            desafioCompleto.decifrado = textoDecifrado;
            desafioCompleto.resumo_criptografico = resumoCriptografico;

            Console.WriteLine($"\n>>> Desafio Completo: {desafioCompleto.toJson()}");

            await PostDesafio(desafioCompleto);
        }

        private static async Task<Desafio> GetDesafio()
        {
            var endpoint = $"challenge/dev-ps/generate-data?token={token}";

            var json = await HttpRequest.GetData(endpoint);

            Console.WriteLine($"\nJson: {json}");

            var desafio = JsonConvert.DeserializeObject<Desafio>(json);

            return desafio;
        }

        private static async Task PostDesafio(Desafio desafio)
        {
            var endpoint = $"challenge/dev-ps/submit-solution?token={token}";

            var byteArray = Encoding.ASCII.GetBytes(desafio.toJson());

            var byteArrayContent = new ByteArrayContent(byteArray);

            Console.WriteLine($"\nByteArray: {byteArray}");

            var result = await HttpRequest.Upload(endpoint, "answer", byteArrayContent);

            Console.WriteLine($"\nResultado: {result}");

        }
    }
}
