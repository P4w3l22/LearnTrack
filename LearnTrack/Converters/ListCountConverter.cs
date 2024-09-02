using System.Globalization;

namespace LearnTrack.Converters;

public class ListCountConverter : IValueConverter
{
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		List<object> coll = new ((IEnumerable<object>)value);
		return coll.Count();
	}


	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
		throw new NotSupportedException();
}
