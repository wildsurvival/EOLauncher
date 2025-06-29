using Avalonia;
using Jab;
using ShadUI.Themes;
using EOLauncher.ViewModels;
using EOLauncher.Configuration;
using ShadUI.Dialogs;

namespace EOLauncher
{
    [ServiceProvider]
    [Transient<SettingsViewModel>]
    [Transient<MainViewModel>]
    [Transient<MainWindowViewModel>]
    [Singleton(typeof(DialogManager))]
    [Singleton(typeof(ConfigurationProvider), Factory = nameof(ConfigurationProviderFactory))]
    [Singleton(typeof(ThemeWatcher), Factory = nameof(ThemeWatcherFactory))]
    public partial class ServiceProvider
    {
        public ThemeWatcher ThemeWatcherFactory()
        {
            return new ThemeWatcher(Application.Current!);
        }

        public ConfigurationProvider ConfigurationProviderFactory()
        {
            return new ConfigurationProvider(Application.Current!).Load();
        }
    }
}