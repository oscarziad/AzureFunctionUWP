﻿using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;

namespace SharedLibrary.Services
{
    public static class DeviceService
    {
        private static HttpClient _client;
        public static async Task<string> SendMessageAsync(DeviceClient deviceClient)
        {
            _client = new HttpClient();

            var response = await _client.GetAsync("http://api.openweathermap.org/data/2.5/weather?q=orebro&appid=62d274c03fa45dffcda1b3b257b696ce");
            var data = JsonConvert.DeserializeObject<TemperatureModel.Temperature>(await response.Content.ReadAsStringAsync());
            var weather = new WeatherModel

            {
                Temperature = data.main.temp,
                Humidity = data.main.humidity
            };

            var json = JsonConvert.SerializeObject(weather);

            var payload = new Microsoft.Azure.Devices.Client.Message(Encoding.UTF8.GetBytes(json));
            await deviceClient.SendEventAsync(payload);
            
            Console.WriteLine($"Message sent: {json}");
            return json;
        }

        public static async Task ReceiveMessageAsync(DeviceClient deviceClient)
        {
            while (true)
            {
                var payload = await deviceClient.ReceiveAsync();
                if (payload == null)
                    continue;

                Console.WriteLine($"Message Received: {Encoding.UTF8.GetString(payload.GetBytes())}");
                await deviceClient.CompleteAsync(payload);
            }
        }

        public static async Task SendMessageToDeviceAsync(ServiceClient serviceClient, string targetDeviceId, string message)
        {
            var payload = new Microsoft.Azure.Devices.Message(Encoding.UTF8.GetBytes(message));
            await serviceClient.SendAsync(targetDeviceId, payload);
        }
    }
}