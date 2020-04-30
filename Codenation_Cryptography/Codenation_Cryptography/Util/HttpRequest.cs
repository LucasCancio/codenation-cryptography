using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public static async Task<string> Upload(string endpoint, string filePath, ByteArrayContent byteArrayContent)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            using var form = new MultipartFormDataContent();
            using var fileContent = byteArrayContent;

            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

            var filename = Path.GetFileName(filePath);

            form.Add(fileContent, filename, filename);

            var response = await httpClient.PostAsync(endpoint, form);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

    }
}
