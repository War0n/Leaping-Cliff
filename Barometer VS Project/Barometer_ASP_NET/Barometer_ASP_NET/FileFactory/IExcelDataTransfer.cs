using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barometer_ASP_NET.FileFactory
{
	public interface IExcelDataTransfer
	{
		void Export();
		void Import();
	}
}
