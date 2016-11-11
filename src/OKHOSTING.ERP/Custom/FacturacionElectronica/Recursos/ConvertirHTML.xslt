<xsl:stylesheet version = "1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:cfdi="http://www.sat.gob.mx/cfd/3"  xmlns:tfd="http://www.sat.gob.mx/TimbreFiscalDigital">

	<xsl:output method="html"/>

	<xsl:template match="/cfdi:Comprobante" >

		<html>
			<head>
				<title>
					Factura <xsl:value-of select="@serie"/><xsl:value-of select="@folio"/>
				</title>
				<style>
					*
					{
					font-family: arial, verdana, tahoma;
					font-size: 12px;
					}
					.wrap
					{
					width: 800px;
					font-size: 10px;
					white-space: pre-wrap;
					word-wrap: break-word;
					word-break: break-all;
					}
					table
					{
					width: 100%;
					}
					div
					{
					margin-top: 10px;
					margin-bottom: 10px;
					}
					.empresa
					{
					width: 380px;
					}
					h1
					{
					border-bottom: 1px solid #a4a4a4;
					padding-bottom: 5px;
					font-size: 18px;
					}
					h2
					{
					font-size: 16px;
					}
					h2, p
					{
					margin-top: 5px;
					margin-bottom: 5px;
					}
					.fondo
					{
					background-color: #f5f5f5;
					border: 1px solid #a0a0a0;
					padding: 5px;
					}
					#Conceptos
					{
					padding: 0px;
					border: 0px;
					border-bottom: 1px solid #aaaaaa;
					border-right: 1px solid #aaaaaa;
					}
					#Conceptos td, #Conceptos th
					{
					border: 1px solid #aaaaaa;
					border-bottom: 0px;
					border-right: 0px;
					padding: 5px;
					}
				</style>

			</head>
			<body>

				<div style="width: 800px; border: 0px solid green">

					<!-- No. de factura -->
					<div style="border: 0px solid #f5f5f5;">
						<h1>
							Folio Fiscal: <xsl:value-of select="cfdi:Complemento/tfd:TimbreFiscalDigital/@UUID"/>
						</h1>
						<h5>
							Folio Interno: <xsl:value-of select="@serie"/><xsl:value-of select="@folio"/>
						</h5>
					</div>

					<!-- Datos generales -->
					<table class="fondo">
						<tr>
							<td>Fecha y hora de emisión:</td>
							<td>
								<xsl:value-of select="@fecha"/>
							</td>
							<td>No. de serie del certificado del Emisor:</td>
							<td>
								<xsl:value-of select="@noCertificado"/>
							</td>
						</tr>
						<tr>
							<xsl:if test="@condicionesDePago">
								<td>Condiciones de pago:</td>
								<td>
									<xsl:value-of select="@condicionesDePago"/>
								</td>
							</xsl:if>
							<xsl:if test="@motivoDescuento">
								<td>Motivo de descuento:</td>
								<td>
									<xsl:value-of select="@motivoDescuento"/>
								</td>
							</xsl:if>
						</tr>
						<tr>
							<td>Forma de pago:</td>
							<td>
								<xsl:value-of select="@formaDePago"/>
							</td>
							<td>Método de pago:</td>
							<td>
								<xsl:value-of select="@metodoDePago"/>
							</td>
						</tr>
						<tr>
							<td>Num. Cuenta de pago:</td>
							<td>
								<xsl:if test="@NumCtaPago">
									<xsl:value-of select="@NumCtaPago"/>
								</xsl:if>
							</td>
							<td>Lugar de expedicion:</td>
							<td>
								<xsl:value-of select="@LugarExpedicion"/>
							</td>
						</tr>

						<tr>
							<td>No. de serie del certificado del SAT:</td>
							<td>
								<xsl:value-of select="cfdi:Complemento/tfd:TimbreFiscalDigital/@noCertificadoSAT"/>
							</td>
							<td>Tipo de comprobante:</td>
							<td>
								<xsl:value-of select="@tipoDeComprobante"/>
							</td>
						</tr>
						<tr>
							<td>Moneda / Tipo de cambio:</td>
							<td>
								MXN / 1.00
							</td>
							<td>Fecha y hora de certificación:</td>
							<td>
								<xsl:value-of select="cfdi:Complemento/tfd:TimbreFiscalDigital/@FechaTimbrado"/>
							</td>
						</tr>

					</table>

					<!-- Emisor -->
					<div class="empresa fondo" style="float: left;">

						<h2>Emisor</h2>
						<p>
							<b>
								<xsl:value-of select="cfdi:Emisor/@nombre"/>
							</b>
							<br />
							RFC: <xsl:value-of select="cfdi:Emisor/@rfc"/>
							<br />
							Regimen Fiscal: <xsl:value-of select="cfdi:Emisor/cfdi:RegimenFiscal/@Regimen"/>
							<br />
							<!--
							<xsl:value-of select="cfdi:Emisor/cfdi:DomicilioFiscal/@calle"/>

							<xsl:if test="cfdi:Emisor/cfdi:DomicilioFiscal/@noExterior">
								#<xsl:value-of select="cfdi:Emisor/cfdi:DomicilioFiscal/@noExterior"/>
							</xsl:if>
							<xsl:if test="cfdi:Emisor/cfdi:DomicilioFiscal/@noInterior">
								Int. <xsl:value-of select="cfdi:Emisor/cfdi:DomicilioFiscal/@noInterior"/>
							</xsl:if>
							<xsl:if test="cfdi:Emisor/cfdi:DomicilioFiscal/@colonia">
								Col. <xsl:value-of select="cfdi:Emisor/cfdi:DomicilioFiscal/@colonia"/>
							</xsl:if>
							<br />
							<xsl:if test="cfdi:Emisor/cfdi:DomicilioFiscal/@localidad">
								Localidad: <xsl:value-of select="cfdi:Emisor/cfdi:DomicilioFiscal/@localidad"/>
							</xsl:if>
							<xsl:if test="cfdi:Emisor/cfdi:DomicilioFiscal/@referencia">
								Referencia: <xsl:value-of select="cfdi:Emisor/cfdi:DomicilioFiscal/@referencia"/>
							</xsl:if>
							C.P.<xsl:value-of select="cfdi:Emisor/cfdi:DomicilioFiscal/@codigoPostal"/>
							<br />
							<xsl:value-of select="cfdi:Emisor/cfdi:DomicilioFiscal/@municipio"/>
							<xsl:text> </xsl:text>
							<xsl:value-of select="cfdi:Emisor/cfdi:DomicilioFiscal/@estado"/>,
							<xsl:value-of select="cfdi:Emisor/cfdi:DomicilioFiscal/@pais"/>
							-->
							Calle San Gabriel #3103 Col. Los Arcos
							<br />
							CP 44500
							<br />
							Guadalajara, Jalisco, México
						</p>

					</div>

					<!-- Receptor -->
					<div class="empresa fondo" style="float: right;">

						<h2>Receptor</h2>
						<p>
							<xsl:if test="cfdi:Receptor/@nombre">
								<b>
									<xsl:value-of select="cfdi:Receptor/@nombre"/>
								</b>
								<br />
							</xsl:if>
							RFC: <xsl:value-of select="cfdi:Receptor/@rfc"/>
							<xsl:if test="cfdi:Receptor/cfdi:Domicilio/@calle">
								<br />
								<xsl:value-of select="cfdi:Receptor/cfdi:Domicilio/@calle"/>
							</xsl:if>
							<xsl:if test="cfdi:Receptor/cfdi:Domicilio/@noExterior">
								#<xsl:value-of select="cfdi:Receptor/cfdi:Domicilio/@noExterior"/>
							</xsl:if>
							<xsl:if test="cfdi:Receptor/cfdi:Domicilio/@noInterior">
								Int. <xsl:value-of select="cfdi:Receptor/cfdi:Domicilio/@noInterior"/>
							</xsl:if>
							<xsl:if test="cfdi:Receptor/cfdi:Domicilio/@colonia">
								Col. <xsl:value-of select="cfdi:Receptor/cfdi:Domicilio/@colonia"/>
							</xsl:if>
							<br />
							<xsl:if test="cfdi:Receptor/cfdi:Domicilio/@localidad">
								Localidad: <xsl:value-of select="cfdi:Receptor/cfdi:Domicilio/@localidad"/>
							</xsl:if>
							<xsl:if test="cfdi:Receptor/cfdi:Domicilio/@referencia">
								Referencia: <xsl:value-of select="cfdi:Receptor/cfdi:Domicilio/@referencia"/>
							</xsl:if>
							<xsl:if test="cfdi:Receptor/cfdi:Domicilio/@codigoPostal">
								C.P.<xsl:value-of select="cfdi:Receptor/cfdi:Domicilio/@codigoPostal"/>
							</xsl:if>
							<br />
							<xsl:if test="cfdi:Receptor/cfdi:Domicilio/@municipio">
								<xsl:value-of select="cfdi:Receptor/cfdi:Domicilio/@municipio"/>
							</xsl:if>
							<xsl:text> </xsl:text>
							<xsl:if test="cfdi:Receptor/cfdi:Domicilio/@estado">
								<xsl:value-of select="cfdi:Receptor/cfdi:Domicilio/@estado"/>,
							</xsl:if>
							<xsl:text> </xsl:text>
							<xsl:value-of select="cfdi:Receptor/cfdi:Domicilio/@pais"/>
						</p>

					</div>

					<!-- Conceptos -->
					<h2 style="clear: both;">Conceptos</h2>

					<table class="fondo" id="Conceptos" cellspacing="0" cellpading="0">
						<tr>
							<th>Cantidad</th>
							<th>Unidad</th>
							<th>Código</th>
							<th>Descripcion</th>
							<th>Precio</th>
							<th>Importe</th>
						</tr>

						<!-- Conceptos -->
						<xsl:for-each select="cfdi:Conceptos/cfdi:Concepto">
							<tr>
								<td align="right">
									<xsl:value-of select="@cantidad"/>
								</td>
								<td>
									<xsl:choose>
										<xsl:when test="@unidad">
											<xsl:value-of select="@unidad"/>
										</xsl:when>
										<xsl:otherwise>
											-
										</xsl:otherwise>
									</xsl:choose>
								</td>
								<td align="right">
									<xsl:choose>
										<xsl:when test="@noIdentificacion">
											<xsl:value-of select="@noIdentificacion"/>
										</xsl:when>
										<xsl:otherwise>
											-
										</xsl:otherwise>
									</xsl:choose>
								</td>
								<td>
									<xsl:value-of select="@descripcion"/>
								</td>
								<td align="right">
									$<xsl:value-of select="@valorUnitario"/>
								</td>
								<td align="right">
									$<xsl:value-of select="@importe"/>
								</td>
							</tr>
						</xsl:for-each>

						<!-- Subtotal -->
						<tr>
							<td colspan="4">
								<span style="visibility: hidden">.</span>
							</td>
							<td>
								Subtotal
							</td>
							<td align="right">
								$<xsl:value-of select="@subTotal"/>
							</td>
						</tr>

						<!-- Descuento -->
						<xsl:if test="@descuento">
							<tr>
								<td colspan="4">
									<span style="visibility: hidden">.</span>
								</td>
								<td>Descuento</td>
								<td>
									<xsl:value-of select="@descuento"/>
								</td>
							</tr>
						</xsl:if>

						<!-- Retenciones -->
						<xsl:for-each select="/cfdi:Comprobante/cfdi:Impuestos/cfdi:Retenciones/cfdi:Retencion">
							<tr>
								<td colspan="4">
									<span style="visibility: hidden">.</span>
								</td>
								<td>
									Retención <xsl:value-of select="@impuesto"/>
								</td>
								<td align="right">
									-$<xsl:value-of select="@importe"/>
								</td>
							</tr>
						</xsl:for-each>

						<!-- Traslados -->
						<xsl:for-each select="/cfdi:Comprobante/cfdi:Impuestos/cfdi:Traslados/cfdi:Traslado">
							<tr>
								<td colspan="4">
									<span style="visibility: hidden">.</span>
								</td>
								<td>
									<xsl:value-of select="@impuesto"/>
									<xsl:text> </xsl:text>
									<xsl:value-of select="@tasa"/>%
								</td>
								<td align="right">
									$<xsl:value-of select="@importe"/>
								</td>
							</tr>
						</xsl:for-each>

						<!-- Total -->
						<tr>
							<td colspan="4">
								<span style="visibility: hidden">.</span>
							</td>
							<td>
								<b>Total</b>
							</td>
							<td align="right">
								<b>
									$<xsl:value-of select="@total"/>
								</b>
							</td>
						</tr>
					</table>

					<h2>Sello Digital del Emisor</h2>
					<p class="wrap">
						<xsl:value-of select="@sello"/>
					</p>

					<h2>Sello Digital del SAT</h2>
					<p class="wrap">
						<xsl:value-of select="cfdi:Complemento/tfd:TimbreFiscalDigital/@selloSAT"/>
					</p>

					<h2>Cadena Original</h2>
					<p class="wrap">
						<CadenaOriginal />
					</p>

					<hr />

					<p>
						Este documento es una representación impresa de un CFDI
					</p>

				</div>


			</body>
		</html>

	</xsl:template>


</xsl:stylesheet>