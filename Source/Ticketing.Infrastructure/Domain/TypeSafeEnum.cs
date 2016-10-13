using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.Infrastructure.Domain
{
	public abstract class TypeSafeEnum<TDerived, TValue> where TDerived : TypeSafeEnum<TDerived, TValue>
	{
		private TValue _value;
		private string _caption;

		static TypeSafeEnum()
		{
			Type valueType = typeof(TValue);

			if (!valueType.IsValueType)
			{
				throw new ArgumentException("TValue must be a value type!");
			}
		}

		protected TypeSafeEnum(TValue value, string caption)
		{
			_value = value;
			_caption = caption;
		}

		public TValue Value
		{
			get { return _value; }
		}

		public string Caption
		{
			get { return _caption; }
		}

		#region Type Conversion Operators

		public static explicit operator TypeSafeEnum<TDerived, TValue>(ValueType value)
		{
			TypeSafeEnum<TDerived, TValue> conversionResult = ConvertToInstanceOfDerived(value);

			if (conversionResult != null)
			{
				return conversionResult;
			}

			throw new InvalidCastException();
		}

		public static explicit operator TypeSafeEnum<TDerived, TValue>(string caption)
		{
			TypeSafeEnum<TDerived, TValue> conversionResult = ConvertToInstanceOfDerived(caption);

			if (conversionResult != null)
			{
				return conversionResult;
			}

			throw new InvalidCastException();
		}

		#endregion

		private static TypeSafeEnum<TDerived, TValue> ConvertToInstanceOfDerived(object value)
		{
			TypeSafeEnum<TDerived, TValue> result = null;

			Type derivedType = typeof(TDerived);
			IEnumerable<FieldInfo> fields = derivedType
				.GetFields(BindingFlags.Public | BindingFlags.Static)
				.Where(f => f.IsInitOnly && f.FieldType.Equals(derivedType));

			if (fields != null && fields.Any())
			{
				foreach (var field in fields)
				{
					TDerived fieldValue = field.GetValue(null) as TDerived;

					if (fieldValue != null)
					{
						Type valueType = value.GetType();

						if (valueType.IsValueType)
						{
							// assuming the values will actually fit in four bytes
							int fieldAsInteger = (int)Convert.ChangeType(fieldValue.Value, typeof(int));
							int valueAsInteger = (int)Convert.ChangeType(value, typeof(int));

							if (fieldAsInteger.Equals(valueAsInteger))
							{
								result = fieldValue;
								break;
							}
						}
						else if (valueType == typeof(string))
						{
							if (fieldValue.Caption.Equals(value as string))
							{
								result = fieldValue;
								break;
							}
						}
					}
				}
			}

			return result;
		}
	}
}
