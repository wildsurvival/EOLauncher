using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Interactivity;
using EOLauncher.Models;
using EOLauncher.ViewModels;
using ShadUI.Breakpoints;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace EOLauncher.Views;

public partial class MainPage : UserControl
{
    public MainPage()
    {
        InitializeComponent();

        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    private void OnUnloaded(object? sender, RoutedEventArgs e)
    {

    }

    private MainViewModel _viewModel = null!;

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not MainViewModel vm) return;

        _viewModel = vm;

        ServerList.SelectionChanged += OnServerListSelectionChanged;
    }

    // Get this out of the viewmodel
    private void OnServerListSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (ServerList.SelectedItem is Server server)
        {
            Version v1 = new Version(server.Version.Replace("x", "0"));

            _viewModel.AvailableClients.Clear();

            // How dirty is this comparison?
            foreach (var item in _viewModel._configurationProvider.Settings.Clients)
            {
                var comparison = item.Comparison switch
                {
                    VersionComparison.GreaterThan => v1 > new Version(item.Version),
                    VersionComparison.LessThan => v1 < new Version(item.Version),
                    VersionComparison.GreaterThanOrEqual => v1 >= new Version(item.Version),
                    VersionComparison.LessThanOrEqual => v1 <= new Version(item.Version),
                    _ => false
                };

                if (comparison)
                {
                    _viewModel.AvailableClients.Add(item.Name);
                }
            }

            ClientComboBox.SelectedIndex = 0;
        }
    }

    
}
