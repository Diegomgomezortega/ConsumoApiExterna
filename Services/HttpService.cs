using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsumoApiExterna.Services
{
    public class HttpService: IHttpService
    {
        private readonly HttpClient httpClient;

        public HttpService(HttpClient http) //Inyecta en el HttpService Client para que me haga las peticiones Http
        {
            this.httpClient = http;
        }

        public async Task<HttpRespuesta<T>> Get<T>(string url)//Estoy haciendo un get sobre la url, la url me puede trar lo que tiene la url
        {
            var respuestaHttp = await httpClient.GetAsync(url);
            if (respuestaHttp.IsSuccessStatusCode)//Si vino correcto, deserializo la respuesta
            {
                var respuesta = await DeserializarRespuesta<T>(respuestaHttp);
                return new HttpRespuesta<T>(respuesta, false, respuestaHttp);
            }
            else//Si yo tengo error, me muestra la respuesta
            {
                return new HttpRespuesta<T>(default, true, respuestaHttp);
            }
        }
        public async Task<HttpRespuesta<object>> Post<T>(string url, T enviar)
        {
            try
            {
                var enviarJSON = JsonSerializer.Serialize(enviar);
                var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
                var respuestaHTTP = await httpClient.PostAsync(url, enviarContent); //Como parametro la url y el contenido
                return new HttpRespuesta<object>(null, !respuestaHTTP.IsSuccessStatusCode, respuestaHTTP);

            }
            catch (System.Exception e) { throw; }




        }

        public async Task<HttpRespuesta<object>> Put<T>(string url, T enviar)
        {
            try
            {
                var enviarJSON = JsonSerializer.Serialize(enviar);
                var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
                var respuestaHTTP = await httpClient.PutAsync(url, enviarContent); //Como parametro la url y el contenido
                return new HttpRespuesta<object>(null, !respuestaHTTP.IsSuccessStatusCode, respuestaHTTP);

            }
            catch (System.Exception e) { throw; }

        }

        public async Task<HttpRespuesta<object>> Delete(string url)
        {
            var respuestaHTTP = await httpClient.DeleteAsync(url);
            return new HttpRespuesta<object>(null, !respuestaHTTP.IsSuccessStatusCode, respuestaHTTP);
        }
        private async Task<T> DeserializarRespuesta<T>(HttpResponseMessage httpRespuesta)
        {
            var RespuestaString = await httpRespuesta.Content.ReadAsStringAsync();//El contenido lo convierto en string, ya que viene en json,
            return JsonSerializer.Deserialize<T>(RespuestaString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });//le decimos que no tenga en cuenta las minusculas o mayusculas para n tener probemas en la deserializacion
        }
    }
}
