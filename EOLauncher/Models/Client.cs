using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EOLauncher.Models
{
    public enum VersionComparison
    {
        [Description(">")]
        GreaterThan,
        [Description("<")]
        LessThan,
        [Description(">=")]
        GreaterThanOrEqual,
        [Description("<=")]
        LessThanOrEqual
    }

    public class Client
    {
        public string Name { get; set; }
        public string Location { get; set; }
        // TODO: Better version handling
        public string Version { get; set; }
        public VersionComparison Comparison { get; set; } = VersionComparison.GreaterThanOrEqual;

        public bool IgnoreClientWarning { get; set; } = false;

        // TODO: Find a way to get this out of the model
        [JsonIgnore]
        public ObservableCollection<VersionComparison> VersionComparisonOptions { get; } = new ObservableCollection<VersionComparison>(Enum.GetValues(typeof(VersionComparison)).Cast<VersionComparison>());
    }
}
