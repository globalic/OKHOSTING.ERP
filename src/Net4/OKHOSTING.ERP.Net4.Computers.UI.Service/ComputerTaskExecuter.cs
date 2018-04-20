using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace OKHOSTING.ERP.Net4.Computers.UI.Service
{
	public partial class ComputerTaskExecuter : ServiceBase
	{
		public ComputerTaskExecuter()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
		}

		protected override void OnStop()
		{
		}
	}
}