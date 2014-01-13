using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Barometer_ASP_NET.FileFactory
{
    public static class FileHandlerFactory<IExcelDataTransfer>
	{
		private static Dictionary<string, Type> map;

		/// <summary>
		/// Method that returns an object of that what you want
		/// </summary>
		/// <param name="productName"></param>
		/// <returns></returns>
		public static IExcelDataTransfer create(string productName)
		{
			if (map == null)
			{
				map = new Dictionary<string, Type>();
                Type[] types = Assembly.GetAssembly(typeof(IExcelDataTransfer)).GetTypes();

				foreach (Type type in types)
				{
                    if (!typeof(IExcelDataTransfer).IsAssignableFrom(type) || type == typeof(IExcelDataTransfer))
					{
						continue;
					}
					map.Add(type.Name, type);
				}
			}
            return (IExcelDataTransfer)Activator.CreateInstance(map[productName]);
		}
	}
}