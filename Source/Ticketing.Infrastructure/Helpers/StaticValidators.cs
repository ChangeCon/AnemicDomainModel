using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Ticketing.Infrastructure.Configuration;

namespace Ticketing.Infrastructure.Helpers
{
	public static class StaticValidators
	{
		private static IEnumerable<Type> _systemTypes;

		static StaticValidators()
		{
			Type assemblyType = Assembly.GetExecutingAssembly().GetType();
			Type[] exportedTypes = assemblyType.Module.Assembly.GetExportedTypes();

			_systemTypes = Array.AsReadOnly(exportedTypes);
		}

		public static bool IsValidOIB(this string value)
		{
			if (!string.IsNullOrEmpty(value) && value.Length == 11)
			{
				int residue = 10;

				for (int i = 0; i < value.Length - 1; i++)
				{
					int subscore = (((Convert.ToInt32(Convert.ToString(value[i]))) + residue) % 10);
					subscore = (subscore == 0 ? 10 : subscore);
					residue = subscore * 2 % 11;
				}

				if (Convert.ToString(11 - residue) == Convert.ToString(value[value.Length - 1]))
				{
					return true;
				}
				else if (Convert.ToString(11 - residue) == "10" && "0" == Convert.ToString(value[value.Length - 1]))
				{
					return true;
				}
			}

			return false;
		}

		public static string GetLast(this string value, int num)
		{
			if (num > value.Length)
				return value;

			return value.Substring(value.Length - num);
		}

		public static bool IsValid(this DateTime value)
		{
			if (value > DateTime.MinValue && value < DateTime.Now.AddYears(-150))
				return false;

			return true;
		}

		public static bool IsValid(this DateTime? value)
		{
			if (value.HasValue && value > DateTime.MinValue && value < DateTime.Now.AddYears(-150))
				return false;

			return true;
		}

	    public static bool IsDefault(this DateTime value)
	    {
	        return value == default(DateTime) || value == DateTime.MinValue;
	    }

        public static int? CalculateAge(this DateTime? value)
        {
            if (value == null) return null;

            var today = DateTime.Today;

            var age = today.Year - value.Value.Year;
            if (value > today.AddYears(-age)) 
                age--;

	        return age;
	    }

        public static int CalculateAge(this DateTime value)
        {
            var today = DateTime.Today;

            var age = today.Year - value.Year;
            if (value > today.AddYears(-age))
                age--;

            return age;
        }

		public static Tuple<bool, string> ValidateConfiguration(this IConfigurationProvider configurationProvider, string[] requiredKeys, out NameValueCollection configuration)
		{
			bool isValid = true;
			StringBuilder errors = new StringBuilder();
			configuration = null;

			if (configurationProvider == null)
			{
				isValid = false;
				errors.AppendLine("The required configuration has not been initialized!");
			}
			else
			{
				configuration = configurationProvider.AcquireConfiguration();

				foreach (var key in requiredKeys)
				{
					if (!configuration.AllKeys.Contains(key))
					{
						isValid = false;
						errors.AppendLine(String.Format("A required configuration key ({0}) is missing!", key));
					}
				}
			}

			return new Tuple<bool, string>(isValid, errors.ToString());
		}

		public static bool IsDefinedAsDataContract(this Type rootType, List<Type> previouslyCheckedTypes = null)
		{
			bool result = rootType.GetCustomAttributes(typeof(DataContractAttribute), true).Any();

			if (result)
			{
				// in case of the original call
				if (previouslyCheckedTypes == null)
				{
					previouslyCheckedTypes = new List<Type>() { rootType };
				}

				var properties = rootType.GetProperties().Where(p => p.MemberType == MemberTypes.Property);

				// firstly checking whether or not all of the properties are decorated with a required attribute
				foreach (var property in properties)
				{
					// skip explicitly ignored properties
					if (property.GetCustomAttributes(typeof(IgnoreDataMemberAttribute), true).Any())
					{
						continue;
					}

					if (!property.GetCustomAttributes(typeof(DataMemberAttribute), true).Any())
					{
						result = false;
						break;
					}
				}

				// proceed with checking types using which the root type's properties have been declared
				if (result)
				{
					// don't check already checked types (thus also avoiding possible circular references)
					List<Type> typesToBeChecked = properties.Select(p => p.PropertyType).Except(previouslyCheckedTypes).ToList();
					List<Type> typesToBeIgnored = new List<Type>();
					List<Type> underlyingTypes = new List<Type>();

					// handle underlying types used to declare nullables, arrays and generic types (collections and tuples, usually)
					foreach (var propertyType in typesToBeChecked)
					{
						if (Nullable.GetUnderlyingType(propertyType) != null)
						{
							typesToBeIgnored.Add(propertyType);
							underlyingTypes.Add(Nullable.GetUnderlyingType(propertyType));
						}
						else if (propertyType.IsArray)
						{
							typesToBeIgnored.Add(propertyType);
							underlyingTypes.Add(propertyType.GetElementType());
						}
						else if (propertyType.IsGenericType)
						{
							typesToBeIgnored.Add(propertyType);
							underlyingTypes.AddRange(propertyType.GetTypeInfo().GenericTypeArguments);
						}
					}

					typesToBeChecked.AddRange(underlyingTypes);

					// ignore types that are part of the framework (usually primitives and strings, others are assumed to be serializable using the DataContractSerializer)
					typesToBeChecked = typesToBeChecked.Except(_systemTypes.Union(typesToBeIgnored)).ToList();

					// further checking user-defined types (enums and classes)
					foreach (var propertyType in typesToBeChecked)
					{
						previouslyCheckedTypes.Add(propertyType);

						// iteratively check the enum's members
						if (propertyType.IsEnum)
						{
							var members = propertyType.GetEnumValues();

							foreach (var member in members)
							{
								var memberInfo = propertyType.GetMember(member.ToString()).First();

								// skip explicitly ignored enum members
								if (memberInfo.GetCustomAttributes(typeof(IgnoreDataMemberAttribute), true).Any())
								{
									continue;
								}

								if (!memberInfo.GetCustomAttributes(typeof(EnumMemberAttribute), true).Any())
								{
									result = false;
									break;
								}
							}
						}
						// recursively check the class's members
						else if (propertyType.IsClass)
						{
							if (!propertyType.IsDefinedAsDataContract(previouslyCheckedTypes))
							{
								result = false;
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
