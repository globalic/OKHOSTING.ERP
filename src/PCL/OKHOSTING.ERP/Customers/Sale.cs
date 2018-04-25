using OKHOSTING.Data.Validation;
using OKHOSTING.ORM;
using OKHOSTING.ORM.Operations;

namespace OKHOSTING.ERP.New.Customers
{
	/// <summary>
	/// A sale made for a customer
	/// </summary>
	public class Sale : Invoice
	{
		public SalesPerson SalesPerson
		{
			get;
			set;
		}

		public CommissionLevel CommissionLevel
		{
			get;
			set;
		}

		[RequiredValidator]
		public Customer Customer
		{
			get;
			set;
		}

		///// <summary>
		///// Método de pago
		///// </summary>
		//public MetodoPago MetodoDePago
		//{
		//		  get;
		//		  set;
		//}

		///// <summary>
		///// Ultimos 4 digitos de la cuenta a donde se realizó el pago
		///// </summary>
		///// <example>0546</example>
		///// <remarks>Opcional, se usa solo en caso de que el metodo de pago sea "Deposito bancario"</remarks>
		//public string NumCtaPago
		//{
		//		  get;
		//		  set;
		//}

		//public void CrearFactura()
		//{
		//	OKHOSTING.ERP.New.Local.Mexico.FacturacionElectronica.Factura.Sale_AfterInsert(this);
		//}

		//public void ReenviarFactura()
		//{
		//	OKHOSTING.ERP.New.Local.Mexico.FacturacionElectronica.Factura.EnviarFactura(this);
		//}

		//public void RegenerarPDF()
		//{
		//	OKHOSTING.ERP.New.Local.Mexico.FacturacionElectronica.Factura.RegenerarPDF(this);
		//}

		//public void Cancelar()
		//{
		//	OKHOSTING.ERP.New.Local.Mexico.FacturacionElectronica.Factura.CancelarFactura(this);
		//}

		//[Action]
		//public void RegenerarFactura()
		//{
		//	OKHOSTING.ERP.New.Local.Mexico.FacturacionElectronica.Factura.RegenerarFactura(this);
		//}
		/// <summary>
		/// Re-calculates customer's balance
		/// </summary>
		public override void OnAfterInsert(DataBase sender, OperationEventArgs eventArgs)
		{
			//re-calculate customer balance
			sender.Select(Customer);
			Customer.CalculateStatistics();
			sender.Update(Customer);
		}

		/// <summary>
		/// Re-calculates customer's balance
		/// </summary>
		public void OnAfterUpdate(DataBase sender, OperationEventArgs eventArgs)
		{
			//re-calculate customer balance
			sender.Select(Customer);
			Customer.CalculateStatistics();
			sender.Update(Customer);
		}

		/// <summary>
		/// Re-calculates customer's balance
		/// </summary>
		public void OnAfterDelete(DataBase sender, OperationEventArgs eventArgs)
		{
			//re-calculate customer balance
			sender.Select(Customer);
			Customer.CalculateStatistics();
			sender.Update(Customer);
		}
	}
}