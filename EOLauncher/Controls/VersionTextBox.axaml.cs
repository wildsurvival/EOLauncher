using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using System;
using System.Linq;

namespace EOLauncher.Controls;

public partial class VersionTextBox : TextBox, IStyleable
{
    public new Type StyleKey => typeof(TextBox);

    public VersionTextBox()
    {
        this.InitializeComponent();
        this.AddHandler(TextInputEvent, Box_TextInput, Avalonia.Interactivity.RoutingStrategies.Tunnel);
    }

    private void Box_TextInput(object? sender, Avalonia.Input.TextInputEventArgs e)
    {
        if (e.Text!.Any(char.IsNumber) || e.Text!.Any(i => i == '.'))
        {

        }
        else
        {
            e.Handled = true;
        }
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}