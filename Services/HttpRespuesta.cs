using System.Net.Http;
using System.Threading.Tasks;

namespace ConsumoApiExterna.Services
{
    public class HttpRespuesta<T>//Clase que va a darme distinas respuestas de acuerdo  a los metodos que posee

    {
        public T Respuesta { get; }//Respuesta correcta, me trae lo que pide el get
        public bool Error { get; }
        public HttpResponseMessage httpResponseMessage { get; }
        public HttpRespuesta(T respuesta, bool error, HttpResponseMessage httpResponseMessage)//Al ser una respuesta generica,trae lo que le pida, sirve para traer cualquier objeto,Si es Ok me trae la respuesta, si es falso me trar el mensaje de respuesta
        {
            Respuesta = respuesta;
            Error = error;
            this.httpResponseMessage = httpResponseMessage;
        }
        public async Task<string> GetBody()
        {
            var resp = await httpResponseMessage.Content.ReadAsStringAsync();//El contenido lo convierto en string para poder saber cuando e error sea verdadero, pueda leerlo
            return resp;
        }


    }
}
