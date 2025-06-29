using Avalonia;
using EOLauncher.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOLauncher.Configuration
{
    public class SettingsConfiguration
    {
        public List<Client> Clients { get; set; } = new();
    }

    public class ConfigurationProvider
    {
        public SettingsConfiguration Settings { get; set; } = new();

        public ConfigurationProvider(Application app)
        {
            Load();
        }

        public void Save()
        {
            string json = System.Text.Json.JsonSerializer.Serialize(Settings, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            try
            {
                System.IO.File.WriteAllText("./settings.json", json);
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., log the error or show a message to the user
                Console.WriteLine($"Error saving clients: {ex.Message}");
            }
        }

        public ConfigurationProvider Load()
        {
            try
            {
                if (System.IO.File.Exists("./settings.json"))
                {
                    string json = System.IO.File.ReadAllText("./settings.json");
                    var settings = System.Text.Json.JsonSerializer.Deserialize<SettingsConfiguration>(json);

                    if (settings != null)
                    {
                        Settings = settings;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., log the error or show a message to the user
                Console.WriteLine($"Error loading clients: {ex.Message}");
            }

            return this;
        }
    }
}
