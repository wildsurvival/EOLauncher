using Avalonia.Collections;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using EOLauncher.Configuration;
using EOLauncher.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOLauncher.ViewModels
{
    public sealed partial class SettingsViewModel(ConfigurationProvider configurationProvider) : ViewModelBase
    {
        private readonly ConfigurationProvider _configurationProvider = configurationProvider;

        public ObservableCollection<Client> Clients { get; set; } = new ();

        public void Initialize()
        {
            _configurationProvider.Load();
            Clients.Clear();
            Clients.AddRange(_configurationProvider.Settings.Clients);
        }

        [RelayCommand]
        public void RemoveClient(Client client)
        {
            Clients.Remove(client);
        }

        [RelayCommand]
        public void AddNewClient()
        {
            Clients.Add(new Client());
        }

        [RelayCommand]
        public void SaveClients()
        {
            _configurationProvider.Settings.Clients = Clients.ToList();
            _configurationProvider.Save();
        }

    }
}
