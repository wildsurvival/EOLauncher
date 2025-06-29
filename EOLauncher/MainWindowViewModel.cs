using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EOLauncher.ViewModels;
using ShadUI.Dialogs;
using ShadUI.Themes;
using ShadUI.Toasts;
using Splat;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace EOLauncher;

public sealed partial class MainWindowViewModel(ThemeWatcher themeWatcher, MainViewModel mainViewModel, SettingsViewModel settingsViewModel, DialogManager dialogManager) : ViewModelBase
{
    [ObservableProperty]
    private DialogManager _dialogManager = dialogManager;

    [ObservableProperty]
    private object? _selectedPage;

    private async Task<bool> SwitchPageAsync(object page)
    {
        if (SelectedPage == page) return false;

        await Task.Delay(200); // prevent animate delay
        SelectedPage = page;
        return true;
    }

    [RelayCommand]
    private async Task OpenMain()
    {
        if (await SwitchPageAsync(mainViewModel))
        {
            mainViewModel.Initialize();
        }
    }

    [RelayCommand]
    private async Task OpenSettings()
    {
        if (await SwitchPageAsync(settingsViewModel))
        {
            settingsViewModel.Initialize();
        }
    }

    public void Initialize()
    {
        SelectedPage = mainViewModel;
        mainViewModel.Initialize();
    }

    private ThemeMode _currentTheme;

    public ThemeMode CurrentTheme
    {
        get => _currentTheme;
        private set => SetProperty(ref _currentTheme, value);
    }

    [RelayCommand]
    private void SwitchTheme()
    {
        CurrentTheme = CurrentTheme switch
        {
            ThemeMode.Dark => ThemeMode.Light,
            ThemeMode.Light => ThemeMode.Dark,
            _ => ThemeMode.Light
        };

        themeWatcher.SwitchTheme(CurrentTheme);
    }
}
