using Microsoft.Azure.Devices.Client;
using SharedUwpLibraries.Models;
using SharedUwpLibraries.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWP
{
    public sealed partial class MainPage : Page

    {
        private static readonly string _conn = "HostName=ec-win20-iothub-oscar.azure-devices.net;DeviceId=consoleapp;SharedAccessKey=PhHHOn69OIQmjjmUQJyyiQEfq1teMYQBAQRBUODJZos=";
        private static readonly DeviceClient deviceClient = DeviceClient.CreateFromConnectionString(_conn, TransportType.Mqtt);

        public MainPage()
        {
            this.InitializeComponent();
        }

        public WeatherList weatherList = new WeatherList();

        private async void btnGetTemperature_Click(object sender, RoutedEventArgs e)
        {
            var result = await DeviceService.SendMessageAsync(deviceClient);

            try
            {
                weatherList.Add(new WeatherModel(result, ""));
                ListWeatherList.ItemsSource = weatherList;
            }
            catch { }
        }
    }
}