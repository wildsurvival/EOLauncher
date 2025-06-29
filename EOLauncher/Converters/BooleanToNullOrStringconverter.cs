﻿using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace EOLauncher.Converters
{
    public class BooleanToNullOrStringConverter : IValueConverter
    {
        public static readonly BooleanToNullOrStringConverter Instance = new();

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var stringValue = parameter?.ToString();

            if (value is bool booleanValue) return booleanValue ? null : stringValue;

            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
