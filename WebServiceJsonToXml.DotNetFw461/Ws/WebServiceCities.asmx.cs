using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Script.Services;
using System.Web.Services;

namespace WebServiceJsonToXml.DotNetFw461.Ws
{
    /// <summary>
    /// Summary description for WebServiceCities
    /// </summary>
    [WebService(Namespace = "http://tempuri-test-wsdl.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceCities : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public CitiesList GetCities()
        {
            var requestJson = WebRequest.Create("https://www.redesocialdecidades.org.br/cities");
            var bodyJson = requestJson.GetResponse();

            string objText;
            using (var sr = new StreamReader(bodyJson.GetResponseStream()))
            {
                objText = sr.ReadToEnd();
            }
            CitiesList cities = JsonConvert.DeserializeObject<CitiesList>(objText);

            //var node = JsonConvert.DeserializeXmlNode(jsonObj, "Root");

            return cities /*node.DocumentElement*/;
        }

        public class City
        {
            public string Longitude { get; set; }
            public string Name_uri { get; set; }
            public string Name { get; set; }
            public string Uf { get; set; }
            public string Pais { get; set; }
            public string Summary { get; set; }
            public string Latitude { get; set; }
            public int Id { get; set; }
        }
        
        public class CitiesList
        {
            public List<City> cities { get; set; }
        }

    }
}
