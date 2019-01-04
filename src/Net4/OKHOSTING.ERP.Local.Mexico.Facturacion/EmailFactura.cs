using System;
using OKHOSTING.Core.Net.Mail;

namespace OKHOSTING.ERP.Local.Mexico.Facturacion
{
	/// <summary>
	/// Plantilla de correo que se manda al generar una factura electronica automaticamente
		/// <para xml:lang="en">
		/// Mail Template sent when an e-invoice is automatically generated
		/// </para>
	/// </summary>
	public class EmailFactura : MailTemplate
	{
		/// <summary>
		/// En el futuro puede definir tags personalizados que se escriban en la plantilla, por hoy esta vacio
		/// <para xml:lang="en">
		/// Can define custom tags for the template, but actually is empty.
		/// </para>
		/// </summary>
		public override void Init()
		{
			//base.ReplaceTags();
		}
	}
}