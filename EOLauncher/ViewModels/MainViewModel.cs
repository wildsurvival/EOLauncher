using Avalonia.Collections;
using CommunityToolkit.Mvvm.Input;
using EOLauncher.Configuration;
using EOLauncher.Models;
using ShadUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EOLauncher.ViewModels
{
    public sealed partial class MainViewModel(SettingsViewModel settingsViewModel, ConfigurationProvider configurationProvider, DialogManager dialogManager) : ViewModelBase
    {
        public readonly SettingsViewModel _settingsViewModel = settingsViewModel;
        public readonly ConfigurationProvider _configurationProvider = configurationProvider;
        public readonly DialogManager _dialogManager = dialogManager;

        public DataGridCollectionView? Servers { get; internal set; }
        public ObservableCollection<string> AvailableClients { get; set; } = new();

        public Server? SelectedServer { get; set; } = null;
        public string? SelectedClient { get; set; } = null;

        public void Initialize()
        {
            var task = Task.Run(async () => await LoadFromSLN());
            Servers = task.Result;
        }

        const string Endpoint = "https://apollo-games.com/SLN/sln.php/api";

        public async Task<DataGridCollectionView> LoadFromSLN()
        {
            var list = new List<Server>();
            var handler = new HttpClientHandler()
            {
                AllowAutoRedirect = true
            };

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "EOLauncher");

                HttpResponseMessage response = await client.GetAsync(Endpoint);

                string json = await response.Content.ReadAsStringAsync();
                list = JsonSerializer.Deserialize<List<Server>>(json) ?? new List<Server>();
            }

            var servers = new ObservableCollection<Server>(list);

            var serverView = new DataGridCollectionView(servers);

            var groupDescription = new DataGridPathGroupDescription("Zone");
            serverView.GroupDescriptions.Add(groupDescription);

            return serverView;
        }

        [RelayCommand]
        public void Launch()
        {
            var server = SelectedServer;
            var client = GetSelectedClient();
            if (server != null && client != null)
            {

                if (server.ClientUrl != null && !client.IgnoreClientWarning && !string.IsNullOrEmpty(server.ClientUrl?.ToString()))
                {
                    _dialogManager.CreateDialog(
                        "Are you sure?",
                        $"{server.Name} has provided a client link. You may want to download it.")
                    .WithPrimaryButton("Continue", () => OnSubmit(server, client))
                    .WithCancelButton("Cancel")
                    .WithMaxWidth(512)
                    .Dismissible()
                    .Show();
                }
                else
                {
                    OnSubmit(server, client);
                }
            }
        }

        private void OnSubmit(Server server, Client client)
        {
            if (Path.Exists(client?.Location))
            {
                var config = Path.Combine(client.Location, "config/setup.ini");
                List<string> names = ["Endless.exe"];

                DirectoryInfo info = new(client.Location);
                FileInfo? file = info.GetFiles().FirstOrDefault(f => names.Contains(f.Name, StringComparer.OrdinalIgnoreCase));
                if (file != null && File.Exists(config))
                {
                    // TODO: Work on this so we aren't reading the whole file every time and reformatting it
                    IniReader ini = new IniReader(config);
                    ini.Load();
                    ini.SetValue("CONNECTION", "Host", server.Host);
                    ini.SetValue("CONNECTION", "Port", server.Port.ToString());
                    ini.Save();

                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(file.FullName)
                    {
                        UseShellExecute = true,
                        WorkingDirectory = client.Location
                    });
                }
                else
                {
                    _dialogManager.CreateDialog("Error", "Could not find the executable/config for the selected client.").Show();
                }

            }
        }

        public Client? GetSelectedClient()
        {
            if (SelectedClient == null)
            {
                return null;
            }

            return _configurationProvider.Settings.Clients.FirstOrDefault(c => c.Name == SelectedClient);
        }
    }
}
