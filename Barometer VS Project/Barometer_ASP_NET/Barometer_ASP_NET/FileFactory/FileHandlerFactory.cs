using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Barometer_ASP_NET.FileFactory
{
	public static class FileHandlerFactory<Product>
	{
		private static Dictionary<string, Type> map;

		/// <summary>
		/// Method that returns an object of that what you want
		/// </summary>
		/// <param name="productName"></param>
		/// <returns></returns>
		public static Product create(string productName)
		{
			if (map == null)
			{
				map = new Dictionary<string, Type>();
				Type[] types = Assembly.GetAssembly(typeof(Product)).GetTypes();

				foreach (Type type in types)
				{
					if (!typeof(Product).IsAssignableFrom(type) || type == typeof(Product))
					{
						continue;
					}
					map.Add(type.Name, type);
				}
			}
			return (Product)Activator.CreateInstance(map[productName]);
		}
	}
}