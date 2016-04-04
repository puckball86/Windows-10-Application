using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Popups;

namespace Windows_Phone_2016
{
    //class to handle getting the location
    public class Location
    {

        public async static Task<Geoposition> GetPosition()
        {
            //looking for access to the geolocator
            //asks user for access to location on their device
            var acessStatus = await Geolocator.RequestAccessAsync();
            //if the access is not allowed an exception is thrown
            if (acessStatus != GeolocationAccessStatus.Allowed) throw new Exception();

            //create a geolocator and sets its desired accuracy
            var geolocator = new Geolocator { DesiredAccuracyInMeters = 0 };

            //the GetGeopositionAsync returns a geoposition object which contains properties such as coordinate
            //coordinate is of geocoordinate type
            //geocoordinate contains properties that return the latitude and longitude
            var position = await geolocator.GetGeopositionAsync();

            //returns the position received
            return position;

        }
    }
}
