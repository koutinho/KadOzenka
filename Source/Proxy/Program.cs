using System;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Configuration;
using Microsoft.Extensions.Configuration;

using Serilog;
using Serilog.Configuration;

namespace Proxy
{

    class Program
    {

        static readonly ILogger _log = Log.ForContext<Program>();

        static void initializeSeq()
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom
                .Configuration(new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build())
                .Enrich
                .FromLogContext()
                .CreateLogger();
        }

        static void Main(string[] args)
        {
            initializeSeq();

            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://*:8888/");
            listener.Start();

            while (true)
            {
                try
                {
                    HttpListenerContext context = listener.GetContext();
                    HttpListenerResponse httpListenerResponse = context.Response;
                    var queryString = context.Request.QueryString;
                    string request = string.Empty, it = string.Empty, rt = string.Empty;
                    bool addressRequest = false;

                    _log.ForContext("QueryString", context.Request.QueryString.AllKeys.ToDictionary(t => t, t => context.Request.QueryString[t]))
                        .Debug("Запрос от клиента {Host} получен", context.Request.RemoteEndPoint);

                    if (queryString != null && queryString["key"] != null)
                    {
                        request = $"{queryString["protocol"]}://api-maps.yandex.ru/{queryString["version"]}/?apikey={queryString["key"]}&lang={queryString["lang"]}";
                        it = "https://api-maps.yandex.ru/services/search/";
                        rt = $"http://{ConfigurationManager.AppSettings["ProxyOwnIp"]}:{ConfigurationManager.AppSettings["ProxyOwnPort"]}?networkProtocol=https&initialType=services&subtype=search";
                        _log.ForContext("Request", request)
                            .ForContext("ProxyRequestType", "Получение скриптов карты")
                            .Debug("Сформирован запрос для прокси для получения скриптов карты");
                        addressRequest = false;
                    }
                    else if (queryString != null && queryString["initialType"] != null)
                    {
                        request =
                            $"{queryString["networkProtocol"]}://api-maps.yandex.ru/{queryString["initialType"]}/{queryString["subtype"].Replace("/v", "/v")}/?" +
                            $"callback={queryString["callback"]}&text={queryString["text"].Replace(",", "%2c")}&format={queryString["format"]}&rspn={queryString["rspn"]}&lang={queryString["lang"]}&" +
                            $"token={queryString["token"]}&type={queryString["type"]}&properties={queryString["properties"]}&geocoder_sco={queryString["geocoder_sco"]}&" +
                            $"origin={queryString["origin"]}&apikey={queryString["apikey"]}";
                        _log.ForContext("Request", request)
                            .ForContext("ProxyRequestType", "Получение адреса")
                            .Debug("Сформирован запрос для прокси для адреса");
                        addressRequest = true;
                    }

                    string response = proxymiseRequest(request, context.Request.Headers, new List<string> { "Host", "Accept-Encoding" }, it, rt, false, addressRequest);
                    sendResponse(httpListenerResponse, response);

                    _log.ForContext("Response", response)
                        .Verbose("Ответ отправлен клиенту {Host}", context.Request.RemoteEndPoint);

                }
                catch (Exception ex) { _log.Error(ex, ex.Message); }
            }

            //http://localhost:8888/?protocol=https&version=2.1&lang=ru_RU&key=5b7a0369-63be-4edd-ac27-716d52c64d46
            //http://localhost:8888/?networkProtocol=https&initialType=services&subtype=search/v2&callback=id_159405526793713781766&text=55.651440230317235%2c37.55142272949218&format=json&rspn=0&lang=ru_RU&token=a622a6257ce664dc89e2116b59d49856&type=geo&properties=addressdetails&geocoder_sco=latlong&origin=jsapi2Geocoder&apikey=5b7a0369-63be-4edd-ac27-716d52c64d46
        }

        static string proxymiseRequest(string request, NameValueCollection headers, List<string> rHeaders, string it, string rt, bool useHash, bool addressRequest)
        {

            string responseData = string.Empty;
            if (useHash && !addressRequest)
            {
                _log.ForContext("FilePath", ConfigurationManager.AppSettings["HashPath"])
                    .Information("Получение скриптов из хэша");

                responseData = GetHash().Replace(it, rt);

                _log.ForContext("ResponseLength", responseData.Length)
                    .ForContext("InitialData", it)
                    .ForContext("ResultData", rt)
                    .Information("Сформирован ответ от прокси");
            }
            else
            {
                _log.ForContext("WebRequestRemovingHeaders", headers.AllKeys.Where(x => rHeaders.Any(y => x == y)).ToDictionary(t => t, t => headers[t]))
                    .Information("Проксификация запроса с параметрами");

                WebRequest webRequest = WebRequest.Create(request);
                WebProxy proxy = new WebProxy(ConfigurationManager.AppSettings["ProxyIp"], int.Parse(ConfigurationManager.AppSettings["ProxyPort"]));
                proxy.BypassProxyOnLocal = bool.Parse(ConfigurationManager.AppSettings["ProxyBypass"]);
                proxy.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["ProxyLogin"], ConfigurationManager.AppSettings["ProxyPassword"]);
                webRequest.Proxy = proxy;

                foreach (string header in headers) webRequest.Headers[header] = headers[header];
                foreach (string remove in rHeaders) webRequest.Headers.Remove(remove);

                _log.ForContext("WebRequestProxyIPAddress", ConfigurationManager.AppSettings["ProxyIp"])
                    .ForContext("WebRequestProxyIPPort", ConfigurationManager.AppSettings["ProxyPort"])
                    .ForContext("WebRequestHeaders", webRequest.Headers.AllKeys.ToDictionary(key => key, key => webRequest.Headers[key]))
                    .Debug("Запрос на адрес {request} от прокси", request);

                WebResponse webResponse = webRequest.GetResponse();

                using (Stream stream = webResponse.GetResponseStream()) using (StreamReader reader = new StreamReader(stream)) responseData = reader.ReadToEnd();
                if (it != string.Empty && rt != string.Empty) responseData = responseData.Replace(it, rt);

                _log.ForContext("ResponseLength", responseData.Length)
                    .ForContext("ContentLength", webResponse.ContentLength)
                    .ForContext("ResponseUri", webResponse.ResponseUri)
                    .ForContext("InitialData", it)
                    .ForContext("ResultData", rt)
                    .Information("Сформирован ответ от прокси");

                webResponse.Close();
            }
            return responseData;
        }

        static void sendResponse(HttpListenerResponse httpListenerResponse, string response)
        {
            Stream output = httpListenerResponse.OutputStream;
            byte[] buffer = Encoding.UTF8.GetBytes(response);
            httpListenerResponse.ContentLength64 = buffer.Length;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }

        static string GetHash() => File.ReadAllText(ConfigurationManager.AppSettings["HashPath"]);

    }

}
