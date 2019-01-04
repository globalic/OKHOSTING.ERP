using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OKHOSTING.ERP.Local.Mexico.Facturacion
{
	/// <summary>
	/// Contiene los datos por default que se usaran en la creacion automatica de facturas electronicas
	/// <para xml:lang="en">
	/// Cointains default data of automatic invoices
	/// </para>
	/// </summary>
	/// <remarks>Esta configuracion es opcional y puede ser utilizada u omitida por el programador
	/// <para xml:lang="en">
	/// This configuration is optional and can be omitted by the programmer
	/// </para>
	/// </remarks>
	[Serializable]
	public class Configuracion: OKHOSTING.Core.ConfigurationBase
	{
		/// <summary>
		/// Crea una nueva instancia
		/// <para xml:lang="en">
		/// Creates a new instance
		/// </para>
		/// </summary>
		public Configuracion()
		{
		}

		/// <summary>
		/// Empresa que emite la factura
		/// <para xml:lang="en">
		/// Company issuing the invoice
		/// </para>
		/// </summary>
		public Empresa Emisor;

		/// <summary>
		/// Domicilio fiscal del Emisor
		/// <para xml:lang="en">
		/// Sender's Address
		/// </para>
		/// </summary>
		public Domicilio DomicilioFiscalEmisor;

		/// <summary>
		/// Version del XML, debe ser 2.0
		/// <para xml:lang="en">
		/// XML version, must be 2.0
		/// </para>
		/// </summary>
		public string Version;
		
		/// <summary>
		/// Namespace del archivo XML
		/// <para xml:lang="en">
		/// Namespace of XML Document
		/// </para>
		/// </summary>
		/// <example>http://www.sat.gob.mx/cfd/2</example>
		public string Xmlns;
		
		/// <summary>
		/// Esquema del XML
		/// <para xml:lang="en">
		/// XML Schema
		/// </para>
		/// </summary>
		/// <example>http://www.w3.org/2001/XMLSchema-instance</example>
		public string Xmlns_Xsi;
		
		/// <summary>
		/// Ubicación del esquema del XML
		/// <para xml:lang="en">
		/// XML Location
		/// </para>
		/// </summary>
		/// <example>http://www.sat.gob.mx/cfd/2 http://www.sat.gob.mx/sitio_internet/cfd/2/cfdv2.xsd</example>
		public string Xsi_SchemaLocation;
		
		/// <summary>
		/// El número del certificado con el que se autentifica la factura
		/// <para xml:lang="en">
		/// Number of autentification certificate for the invoice
		/// </para>
		/// </summary>
		/// <remarks>Leer /Custom/FacturacionElectronica/Recursos/Readme.txt para obtenerlo</remarks>
		/// <example>00001000000001655555</example>
		public string NoCertificado;
		
		/// <summary>
		/// El certificado con el que se autentifica la facturas
		/// <para xml:lang="en">
		/// Certificate that autentificates the invoices
		/// </para>
		/// </summary>
		/// <remarks>Leer /Custom/FacturacionElectronica/Recursos/Readme.txt para obtenerlo</remarks>
		/// <example>
		/// MIICXgIBAAKBgQDKRVP186zuDWHP9BDOGPAOfJaqBlKKaNN6FV0mkO6iyG7TlpWr
		/// O3IBRBX4lw5k5MEDBwLxFmRQJ68ZHkaPDdBfGi3SO6VA+rkt50tlH5bLcSycWDAk
		/// CJ7U72TWDypx69TcafQwpr2vrfXPRmEz/kie5vF0H3tVkVxn5WQ6YUAMeQIDAQAB
		/// AoGAH3sazjTWvVYn2w3Jb8pB0n9hk6TYQ+J8x4t7q/zypzM6zIOrV7Mw0zGdmcso
		/// 2lsgDzCQLKWnhzIl9mrX4Hvt6hhDm/eAWLQ7JDYx2sQ2OO/HmizYKU4QF8pInxCu
		/// vlFxt/a4t9+KEf2smgbJUR6wgvhq76UzJigbwy1S27DRiAECQQD2Kr2hMGa1DIpI
		/// x+nufzhgXcxBoR3IpGkKhUx6oEKswMSiInhuyNp/MvYJgJbMwxT77mXBNoobH6a6
		/// rMD3+YBZAkEA0lm2judLZk5CZ/Lkc4bha/NOeHuYQ8IOEW/eE+RcnnM6tLpDWwt7
		/// o6cl2jVY7TbKJUq8CxLqgG3dP9kuOcJpIQJBAKRKVDLu1a1BiE0Yt0TIPXz7POYU
		/// PId7SuuNmURCDx2yrckzzkLJ5CF+hnxDCOHx1OBq9BhmaPe/QQxXXZZiO0kCQQC0
		/// PnWFHEJqprKWWfZR3Aj7NGBQMy/1F6pwXJhCGVMX3ws148llkYBfahGwWjgaA/HR
		/// ZKmfH5VbeUi1tka67ZChAkEAzaiCnVtXMIuXsIuMLXgn9GKRvjKfU7y6M1jGDlha
		/// KkRe3/2W5yODqZT1CgNJK8GuHgUWXeqKeYQEEGvQUE5cVg==
		/// </example>
		public string Certificado;
		
		/// <summary>
		/// Año de aprobación de los folios. Este dato lo otorga la SHCP al solicitar los folios.
		/// <para xml:lang="en">
		/// Year of folio's approbation. The SHCP gives this info when you ask for folios. 
		/// </para>
		/// </summary>
		/// <example>2008</example>
		public string AnoAprobacion;
		
		/// <summary>
		/// Número de aprobación de los folios. Este dato lo otorga la SHCP al solicitar los folios.
		/// <para xml:lang="en">
		/// Number of folio's approbation. The SCHP gives this info when you ask for folios. 
		/// </para>
		/// </summary>
		/// <example>2000</example>
		public string NoAprobacion;
		
		/// <summary>
		/// Serie de la factura que se va a crear
		/// <para xml:lang="en">
		/// Serial number of the invoice that is going to be made
		/// </para>
		/// </summary>
		/// <example>A, B, C o cualquier cadena</example>
		public string Serie;
		
		/// <summary>
		/// La forma de pago para la factura
		/// <para xml:lang="en">
		/// Payment method for the invoice
		/// </para>
		/// </summary>
		/// <remarks>Obligatorio</remarks>
		/// <example>Pago en una sola excibición</example>
		public string FormaDePago;
		
		/// <summary>
		/// Condiciones para el pago
		/// <para xml:lang="en">
		/// Payment conditions
		/// </para>
		/// </summary>
		/// <remarks>Opcional</remarks>
		public string CondicionesDePago;
		
		/// <summary>
		/// Método de pago
		/// <para xml:lang="en">
		/// Payment method
		/// </para>
		/// </summary>
		/// <example>Deposito en cuenta, Efectivo, Cheque, etc.</example>
		/// <remarks>Opcional</remarks>
		public string MetodoDePago;

		/// <summary>
		/// Lugar donde se expide la factura
		/// <para xml:lang="en">
		/// Place where the invoice is issued
		/// </para>
		/// </summary>
		/// <example>Guadalajara, Jalisco</example>
		/// <remarks>Obligatorio</remarks>
		public string LugarExpedicion;

		/// <summary>
		/// Copia los valores almacenados en el objeto COnfiguracion a al objeto Factura
		/// <para xml:lang="en">
		/// Copies stored values from configuration objecto to invoice object
		/// </para>
		/// </summary>
		/// <param name="f">Factura a la cual se le asignarán los valores del objeto Configuracion
		/// <para xml:lang="en">
		/// Invoice which the object configuration values will be assigned 
		/// </para>
		/// </param>
		public void CargarConfiguracion(Factura f)
		{
			Empresa emisor;
			Domicilio domicilioFiscalEmisor;

			f.Version = Version;
			f.Serie = Serie;
			//f.NoAprobacion = NoAprobacion;
			//f.AnoAprobacion = AnoAprobacion;
			f.FormaDePago = FormaDePago;
			f.NoCertificado = NoCertificado;
			f.Certificado = Certificado;
			f.CondicionesDePago = CondicionesDePago;
			f.MetodoDePago = MetodoDePago;
			f.LugarExpedicion = LugarExpedicion;

			f.Xmlns = Xmlns;
			f.Xmlns_Xsi = Xmlns_Xsi;
			f.Xsi_SchemaLocation = Xsi_SchemaLocation;

			emisor = new Empresa();
			emisor.Nombre = Emisor.Nombre;
			emisor.RFC = Emisor.RFC;
			emisor.RegimenFiscal = Emisor.RegimenFiscal;
			f.Emisor = emisor;

			domicilioFiscalEmisor = new Domicilio();
			domicilioFiscalEmisor.Calle = DomicilioFiscalEmisor.Calle;
			domicilioFiscalEmisor.NoExterior = DomicilioFiscalEmisor.NoExterior;
			domicilioFiscalEmisor.NoInterior = DomicilioFiscalEmisor.NoInterior;
			domicilioFiscalEmisor.Colonia = DomicilioFiscalEmisor.Colonia;
			domicilioFiscalEmisor.Localidad = DomicilioFiscalEmisor.Localidad;
			domicilioFiscalEmisor.Referencia = DomicilioFiscalEmisor.Referencia;
			domicilioFiscalEmisor.Municipio = DomicilioFiscalEmisor.Municipio;
			domicilioFiscalEmisor.Estado = DomicilioFiscalEmisor.Estado;
			domicilioFiscalEmisor.Pais = DomicilioFiscalEmisor.Pais;
			domicilioFiscalEmisor.CodigoPostal = DomicilioFiscalEmisor.CodigoPostal;
			f.DomicilioFiscalEmisor = domicilioFiscalEmisor;
		}

		/// <summary>
		/// Current configuration
		/// <para xml:lang="es">
		/// Configuración actual
		/// </para>
		/// </summary>
		public static Configuracion Current;

		/// <summary>
		/// Loads the current configuration
		/// <para xml:lang="es">
		/// Carga la configuración actual
		/// </para>
		/// </summary>
		static Configuracion()
		{
			Current = (Configuracion)OKHOSTING.Core.ConfigurationBase.Load(typeof(Configuracion));
		}
	}
}