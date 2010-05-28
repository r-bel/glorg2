using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace Glorg2.Design
{
	public class Vector3Converter : ExpandableObjectConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(Glorg2.Vector3) || sourceType == typeof(string))
				return true;
			else
				return base.CanConvertFrom(context, sourceType);
		}
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(Glorg2.Vector3) || destinationType == typeof(string))
				return true;
			else
				return base.CanConvertTo(context, destinationType);
		}
		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			if (value is string)
			{
				var v = value as string;
				return Vector3.Parse(v);
			}
			else if (value is Glorg2.Vector3)
			{
				return value.ToString();
			}
			else
				return base.ConvertFrom(context, culture, value);
		}
		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				return value.ToString();
			}
			else if (destinationType == typeof(Vector3))
			{
				return Vector3.Parse(value as string);
			}
			else
				return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
