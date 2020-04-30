using Codenation_Cryptography.Model;
using Newtonsoft.Json;

namespace Codenation_Cryptography.Util
{
    public static class Extensions
    {
        public static string toJson(this Desafio desafio)
        {
            return JsonConvert.SerializeObject(desafio);
        }
    }
}
