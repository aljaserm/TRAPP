using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace TRAPP.ViewModel.Converter
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTimeOffset dt = (DateTimeOffset)value;
            DateTimeOffset now = DateTimeOffset.Now;
            var timeDiffernce = now - dt;
            if (timeDiffernce.TotalDays > 1)
            {
               return $"{dt:d}";
            }
            else
            {
                if (timeDiffernce.TotalSeconds < 60)
                {
                    return $"{timeDiffernce.TotalSeconds:0} seconds ago";
                }
                if (timeDiffernce.TotalMinutes < 60)
                {
                    return $"{timeDiffernce.TotalMinutes:0} minutes ago";
                }
                if (timeDiffernce.TotalHours < 24)
                {
                    return $"{timeDiffernce.TotalHours:0} hours ago";
                }
                return "yesterday";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTimeOffset.Now;
        }
    }
}
