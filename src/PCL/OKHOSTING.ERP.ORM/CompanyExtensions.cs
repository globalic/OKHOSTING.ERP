using OKHOSTING.ORM;
using OKHOSTING.ORM.Operations;
using OKHOSTING.ERP.New;

namespace OKHOSTING.ERP.ORM
{
	public static class CompanyExtensions
	{
		/// <summary>
		/// Deletes all addresses and  contacts of this company
		/// <para xml:lang="es">
		/// Elimina todas las direcciones y los contanctos de esta empresa
		/// </para>
		/// </summary>
		public static void OnBeforeDelete(this Company company, DataBase sender, OperationEventArgs eventArgs)
		{
			foreach (var a in company.Locations)
			{
				sender.Delete(a);
			}

			foreach (var c in company.Contacts)
			{
				sender.Delete(c);
			}
		}
	}
}