using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Windows_Phone_2016
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //calls the UpdateWeather class on application load
            UpdateWeather();
        }

        //this function gets finformation from an weather api, formats and outputs
        private async void UpdateWeather()
        {

                try{ 
                //Will return a Geoposition object which we can use with its coordinate property to get the latitude and longitude
                Geoposition position = await Location.GetPosition();

                //gets the latitude
                var lat = position.Coordinate.Latitude;
                //gets the longitude
                var lon = position.Coordinate.Longitude;

                //RootObject hold json data from the api
                //WebsiteConnect.GetWeather gets json data from the server and passes it the lat and lon
                RootObject myWeather = await WebsiteConnect.GetWeather(lat, lon);

                //searches the Assets/WeatherImages folder and find the image matching the json icon
                string icon = String.Format("ms-appx:///Assets/WeatherImages/{0}.png", myWeather.weather[0].icon);

                //output the Image that mathces the icon associated with the json data
                ResultImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));


                //before creating as objects reading from the json was only working now and again
                //TemperatureTextBlock.Text = ((int)myWeather.main.temp).ToString()+" Celcius";

                //created the numeric values as objects
                //more consistent readings/less crashers
                TemperatureTextBlock.Text = Math.Round(Convert.ToDecimal(myWeather.main.temp), 0).ToString() + " Celcius";
                DescriptionTextBlock.Text = myWeather.weather[0].description;
                WindSpeedTextBlock.Text = "Wind_Speed: "+Math.Round(Convert.ToDecimal(myWeather.wind.speed), 0).ToString() + " m/s";
                LocationTextBlock.Text = myWeather.name;

            }catch
            {
                MessageDialog msgbox = new MessageDialog("Unable to obtain the weather");
            }
            

        }
    }
}
