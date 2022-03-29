using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using Thread_Racing_Game.Core.Helpers;
using Thread_Racing_Game.Core.Models;

namespace Thread_Racing_Game.Helpers
{
    public static class Utility
    {
        private static readonly string CountryPath = @"Assets\Json\countries.json";

        public static async Task<List<Country>> GetCountries()
        {
            // Gets the current package's path in the original install folder for the current package.
            StorageFolder folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            // Gets the file from desired destination
            StorageFile file = await folder.GetFileAsync(CountryPath);
            // Reads json data
            string json = await FileIO.ReadTextAsync(file);
            // Deserializing object
            List<Country> countries = await Json.ToObjectAsync<List<Country>>(json);

            return countries;
        }
    }
}
