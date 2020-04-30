using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Codenation_Cryptography
{
    public static class HttpRequest
    {
        private static HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://api.codenation.dev/v1/")
        };

        public static async Task<string> GetData(string endpoint)
        {
            //httpClient.DefaultRequestHeaders.Add(); //Se tiver header

            System.Net.Http.HttpResponseMessage response = await httpClient.GetAsync(endpoint);//Endpoint

            response.EnsureSuccessStatusCode();//Lança uma excessão, se der errado

            return await response.Content.ReadAsStringAsync();
        }


        public static async Task<string> PostData(string data, string endpoint)
        {
            //httpClient.DefaultRequestHeaders.Add(); //Se tiver header

            var stringContent = new StringContent(data, UnicodeEncoding.UTF8, "application/json");

            System.Net.Http.HttpResponseMessage response =
                    await httpClient.PostAsync(endpoint, stringContent);

            response.EnsureSuccessStatusCode();//Lança uma excessão, se der errado

            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<Stream> Upload(string endpoint, string paramString, Stream paramFileStream, byte[] paramFileBytes)
        {
            HttpContent stringContent = new StringContent(paramString);
            HttpContent fileStreamContent = new StreamContent(paramFileStream);
            HttpContent bytesContent = new ByteArrayContent(paramFileBytes);

            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(stringContent, "param1", "param1");
                formData.Add(fileStreamContent, "file1", "file1");
                formData.Add(bytesContent, "file2", "file2");

                var response = await httpClient.PostAsync(endpoint, formData);

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStreamAsync();
            }
        }

    }
}
