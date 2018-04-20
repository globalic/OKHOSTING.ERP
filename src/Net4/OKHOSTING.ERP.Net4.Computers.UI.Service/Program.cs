using System.ServiceProcess;

namespace OKHOSTING.ERP.Net4.Computers.UI.Service
{
	static class Program
	{
		/// <summary>
		/// Punto de entrada principal para la aplicación.
		/// </summary>
		static void Main()
		{
			ServiceBase[] ServicesToRun;

			ServicesToRun = new ServiceBase[]
			{
				new ComputerTaskExecuter()
			};

			ServiceBase.Run(ServicesToRun);
		}
	}
}