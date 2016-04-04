using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.UI.Popups;

namespace Windows_Phone_2016
{
    class WebsiteConnect
    {
        public async static Task<RootObject> GetWeather(double lat, double lon)
        {
            //initilizing a Httpclient object which will used for client related functionality e.g GET request
            var http = new HttpClient();
            
            //Formatting the api
            var url = String.Format("http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&units=metric&appid=f07b60f259100321ededf40eec92c304", lat, lon);
            
            //Sends a GET request to the url as an asynchronous operation
            var response = await http.GetAsync(url);
            //creates a variable to hold the serialized HTTP content in a String
            var result = await response.Content.ReadAsStringAsync();

            //creating a Object graph of type RootObject
            var serializer = new DataContractJsonSerializer(typeof(RootObject));

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
           
            var data = (RootObject)serializer.ReadObject(ms);
           

            return data;
        }
    }

    //Classes build to hold data from a json source("http://api.openweathermap.org/data/2.5/weather.......")
    [DataContract]
    public class Coord
    {
        //IS a member of the DataContract and is serializable by DataContractSerializer
        [DataMember]
        public double lon { get; set; }
        [DataMember]
        public double lat { get; set; }
    }
    [DataContract]
    public class Weather
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string main { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string icon { get; set; }
    }
    [DataContract]
    public class Main
    {
        //converted to an object for ease of outputting
        //as a type double temp would not output
        [DataMember]
        public object temp { get; set; }
        [DataMember]
        public double pressure { get; set; }
        [DataMember]
        public int humidity { get; set; }
        [DataMember]
        public double temp_min { get; set; }
        [DataMember]
        public double temp_max { get; set; }
        [DataMember]
        public double sea_level { get; set; }
        [DataMember]
        public double grnd_level { get; set; }
    }
    [DataContract]
    public class Wind
    {
        [DataMember]
        public object speed { get; set; }
        [DataMember]
        public double deg { get; set; }
    }
    [DataContract]
    public class Clouds
    {
        [DataMember]
        public int all { get; set; }
    }
    [DataContract]
    public class Sys
    {
        [DataMember]
        public double message { get; set; }
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public int sunrise { get; set; }
        [DataMember]
        public int sunset { get; set; }
    }

    [DataContract]
    public class RootObject
    {
        [DataMember]
        public Coord coord { get; set; }
        [DataMember]
        public List<Weather> weather { get; set; }
        [DataMember]
        public string @base { get; set; }
        [DataMember]
        public Main main { get; set; }
        [DataMember]
        public Wind wind { get; set; }
        [DataMember]
        public Clouds clouds { get; set; }
        [DataMember]
        public int dt { get; set; }
        [DataMember]
        public Sys sys { get; set; }
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int cod { get; set; }
    }


}
