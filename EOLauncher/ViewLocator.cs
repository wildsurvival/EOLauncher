using Avalonia.Controls;
using Avalonia.Controls.Templates;
using EOLauncher.ViewModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EOLauncher
{
    public class ViewLocator : IDataTemplate
    {
        private static readonly ConcurrentDictionary<Type, Type?> Cache = [];

        private static readonly Assembly CurrentAssembly = Assembly.GetExecutingAssembly();

        public Control? Build(object? param)
        {
            if (param is null)
            {
                return null;
            }

            var viewModelType = param.GetType();

            var viewType = Cache.GetOrAdd(viewModelType, type =>
            {
                var nameSpace = type.Namespace;
                if (nameSpace is null) return null;

                var name = type.Name;
                var viewNameSpace = nameSpace.Replace("ViewModel", "View", StringComparison.Ordinal);

                var viewName = name.Replace("ViewModel", "Page", StringComparison.Ordinal);
                var fullName = $"{viewNameSpace}.{viewName}";

                return CurrentAssembly.GetType(fullName);
            });

            if (viewType is null) return new TextBlock { Text = "View not found: " + viewModelType.FullName };

            var control = (Control)Activator.CreateInstance(viewType)!;
            control.DataContext = param;
            return control;
        }

        public bool Match(object? data)
        {
            return data is ViewModelBase;
        }
    }
}
