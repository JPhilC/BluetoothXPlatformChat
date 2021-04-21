using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BluetoothXPlatformChat.WPF.Views
{
    /// <summary>
    /// A custom boolean to visibility converter that allows a parameter to be passed to
    /// control which boolean represent Visibility.Visible.
    /// </summary>
    [ValueConversion(typeof(Boolean), typeof(Visibility))]
    public class FlexiBooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts a Boolean value to a System.Windows.Visibility enumeration value.
        /// </summary>
        /// <param name="value">The Boolean value to convert. This value can be a standard Boolean value or a nullable Boolean value.</param>
        /// <param name="targetType">This parameter is not used.</param>
        /// <param name="parameter">This parameter is not used.</param>
        /// <param name="culture">This parameter is not used.</param>
        /// <returns> System.Windows.Visibility.Collapsed if value is true; otherwise, System.Windows.Visibility.Visible.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool incomingValue = value != null && (bool)value;
            bool visible = true; // By default behave like the standard BooleanToVisiblity converter
            if (parameter != null)
            {
                string stringValue = parameter as string;
                if (stringValue != null)
                {
                    visible = bool.Parse(stringValue);
                }
                else
                {
                    visible = (bool)parameter;
                }
            }

            if (incomingValue == visible)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Converts a System.Windows.Visibility enumeration value to a Boolean value.
        /// </summary>
        /// <param name="value">A System.Windows.Visibility enumeration value.</param>
        /// <param name="targetType">This parameter is not used.</param>
        /// <param name="parameter">This parameter is not used.</param>
        /// <param name="culture">true if value is System.Windows.Visibility.Collapsed or Hidden; otherwise, false.</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool visible = true; // By default behave like the standard BooleanToVisiblity converter
            if (parameter != null)
            {
                visible = (bool)parameter;
            }

            if (value != null)
            {
                Visibility incoming = (Visibility)value;
                if (incoming == Visibility.Visible)
                {
                    return visible;
                }
                else
                {
                    return !visible;
                }
            }
            else
            {
                return !visible;
            }
        }

    }
}
