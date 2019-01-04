using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OKHOSTING.ERP.Local.Mexico.Facturacion.UI.Console
{
	public class SolicitudDescarga
	{
		/// <summary>
		/// RFC para entrar al SAT
		/// <para xml:lang="en">
		/// RFC To Login the SAT
		/// </para>
		/// </summary>
		public string RFC;

		/// <summary>
		/// Contraseña para entrar al SAT
		/// <para xml:lang="en">
		/// Password to login the SAT
		/// </para>
		/// </summary>
		public string Contrasena;

		/// <summary>
		/// Email donde se mandarán las facturas
		/// <para xml:lang="en">
		/// Email which the invoices will be sent to
		/// </para>
		/// </summary>
		public string Email;

		/// <summary>
		/// Desde que fecha descargar (solo se usan mes y año, el dia se ignora)
		/// <para xml:lang="en">
		/// Starting date (only month and year, day will be ignored)
		/// </para>
		/// </summary>
		public DateTime FechaDesde;

		/// <summary>
		/// Hasta que fecha descargar (solo se usan mes y año, el dia se ignora)
		/// <para xml:lang="en">
		/// Ending Date (only use month and year, day will ber ignored)
		/// </para>
		/// </summary>
		public DateTime FechaHasta;

		/// <summary>
		/// Define si descargar facturas emitidas o recibidas
		/// <para xml:lang="en">
		/// Defines to download sent o received invoices. 
		/// </para>
		/// </summary>
		public OKHOSTING.ERP.Local.Mexico.Facturacion.Descargador.TipoBusqueda Busqueda;
	}
}