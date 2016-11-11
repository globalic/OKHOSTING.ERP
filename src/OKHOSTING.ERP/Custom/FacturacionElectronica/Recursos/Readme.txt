INSTRUCCIONES PARA CONFIGURAR LA FACTURACION ELECTRONICA

Debes contar previamente con la llave publica de tu sello digital (archivo.key) junto con su contraseña, y tu sello digital (archivo.cer). Para tramitar el sello digital sigue estos pasos:

Primero descargas e instalas el Solcedi, que te genera la solicitud para el sello digital. Para descargar el solcedi ve a:

http://www.sat.gob.mx/sitio_internet/e_sat/comprobantes_fiscales/15_15564.html

Una vez hecha tu solicitud de sello te vas al CertiSAT con tu FIEL y ahi mandas la soliicitud. Para entrar al CertiSAT es:

https://www.acceso.sat.gob.mx/Acceso/CertiSAT.asp

(si te sale un mensaje de error de certificado, dale "Ir a este sitio")

Ahi mandas la solicitud del sello y ahi mismo te descarga ya el sello digital. 

Ahora ya con tu sello debes solicitar los folios. Te recomiendo solicitar una cantidad de folios que te duren 2 años ya que cuando se venza tu certificado de sello (en 2 años) deberas reportar los folios que te sobren como cancelados y solicitar una nueva serie. 
Recuerda que debes especificar una "Serie", que puede ser A, B, o cualquier letra, yo te recomiendo que trabajes con la seria A por default para todos los casos, para no meterte en mayor complejidad.

https://www.consulta.sat.gob.mx/SICOFI_WEB/moduloECFD_Plus/acceso.asp

Una vez que el usuario tenga el sello digital, se colocan los archivos de llave publica del sello (.key) y sello digital (.cer) en la carpeta "Custom/FacturacionElectronica/Recursos" y se realizan estos pasos:

1- Ejecutar "1- Convertir certificado a PEM.BAT" cambiando previamente el nombre del sello a "tucertificado.cer" y de la llame a "tullave.key"
2- Ejecutar "2- Extraer num de certificado.BAT" lo cual generará un archivo llamado "tucertificado.txt". 
3- Abrir "tucertificado.txt". De este archivo se extrae el certificado que se usará en Factura.Certificado. Adicionalmente, copiar el "Serial Number", quitarle todos los "3" y ":" y usarlo como Factura.NoCertificado (OJO si en el numero de serie hay un numero 33 solo elimina el primer 3). Debe quedar un numeo de 20 digitos.
4- Ejecutar "3- Convertir llave a PEM.bat" cambiando previamente de nombre la llave de tu firma electronica a "tullave.key" (en este paso el programa te pedirá la contraseña de la llave)

Con eso termina la configuración

5- Llenar todas las propiedades de Factura con los valores que se desee (ver la documentacion de cada propiedad)
6- Una vez que el objeto Factura tenga todas las propiedades asignadas, llamar Factura.GuardarXml para generar la factura en formato Xml (el formato oficial) y Factura.GuardarHtml para generar el archivo HTML para imprimir

Con eso se genera la factura. Finalmente, cada mes se debe mandar un informe a la SHCP usando la funcion InformeMensual.CrearInforme o algun equivalente.

Utiliza la siguiente pagina para validar tus facturas:

http://www.sat.gob.mx/sitio_internet/e_sat/comprobantes_fiscales/15_15565.html