USE [DB_Snat_v_prod]
GO
/****** Object:  StoredProcedure [dbo].[BUSQUEDA_AUTORIZACION_DE_PAGO]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/********************************************************************************************    
NOMBRE DEL PROCEDIMIENTO  : BUSQUEDA_AUTORIZACION_DE_PAGO    
FECHA DE CREACIÓN        : 16/06/2015             
USUARIO DE CREACIÓN       : Daniel Orozco Anticipa.  
VERSIÓN               : 1.0                           

Fecha de modificación   : 18-02-2019  
Usuario de modificación : cfajardo
Motivo de modificación  : regularización de procedimientos almacenados entre ambientes.   
  
* Visado por DBA                :  Mirto Blanco 
* Fecha Aprobación DBA          :  20190220 
            
OBJETIVO         : Obtiene las Autorizaciones de pago segun filtros de busqueda   
    
TABLAS          : AUTORIZACION A  
                        SOLICITUD_PAGO S  
                        CARACTERISTICAS_ESPECIALES   
                        INFORMACION_PROYECTO IP  
                        DIRECCION D  
                        MAESTRO_PROGRAMA MP  
                        MAESTRO_TIPOLOGIA MT  
                        MAESTRO_MODALIDAD MM  
                        TIPO_PROVEEDOR_INFORMACION_PROYECTO TP  
                        PROVEEDOR P  
                        MAESTRO_TIPO_PROVEEDOR MTP  
                        MAESTRO_ESTADO_AUTORIZACION E  
                        MAESTRO_CLASE MC  
            
    
QUE RETORNA        : A.IDAUTORIZACION  
         D.CODIGOREGIONDIRECCION  
         A.FECHAINGRESOAUTORIZACION  
            MP.NOMBREMAESTROPROGRAMA,  
                     MT.NOMBREMAESTROTIPOLOGIA,  
                     MM.NOMBREMAESTROMODALIDAD,  
                     MTP.NOMBREMAESTROTIPOPROVEEDOR,  
                     RutProveedor,  
                     P.NOMBREPROVEEDOR,  
                     A.MONTOTOTALAUTORIZACION,    
                     E.NOMBREMAESTROESTADOAUTORIZACION,   
                     CLASE  
    
PARAMETROS         :@CODIGOPROYECTO int,  
                          @IDMAESTROPROGRAMA BigInt,  
                          @IDMAESTROTIPOLOGIA BigInt,  
                          @IDAUTORIZACION BigInt,  
                          @NOMBREPROVEEDOR VarChar(200),  
                          @IDMAESTROMODALIDAD BigInt,  
                          @IDMAESTROLLAMADO BigInt,  
                          @CODIGOREGIONDIRECCION Int,  
                          @CODIGOPROVINCIADIRECCION Int,  
                          @CODIGOCOMUNADIRECCION Int,  
                          @IDMAESTROSERVICIO BigInt,  
                          @IDMAESTROTIPOPROVEEDOR BigInt,  
                          @IDMAESTROTITULO BigInt,  
                          @IDMAESTROESTADOAUTORIZACION BigInt  
              
             
PROYECTO         : SNAT SIMPLIFICADO  
RESPONSABLE        : DINFO    
           
********************************************************************************************/    
CREATE PROCEDURE [dbo].[BUSQUEDA_AUTORIZACION_DE_PAGO]  
 @CODIGOPROYECTO int,  
 @IDMAESTROPROGRAMA BigInt,  
 @IDMAESTROTIPOLOGIA BigInt,  
 @IDAUTORIZACION BigInt,  
 @NOMBREPROVEEDOR VarChar(200),  
 @IDMAESTROMODALIDAD BigInt,  
 @IDMAESTROLLAMADO BigInt,  
 @CODIGOREGIONDIRECCION Int,  
 @CODIGOPROVINCIADIRECCION Int,  
 @CODIGOCOMUNADIRECCION Int,  
 @IDMAESTROSERVICIO BigInt,  
 @IDMAESTROTIPOPROVEEDOR BigInt,  
 @IDMAESTROTITULO BigInt,  
 @IDMAESTROESTADOAUTORIZACION BigInt  
AS  
BEGIN  
 --Tabla Principal datos de salida  
 DECLARE @AUTORIZACIONES TABLE(NROAUTORIZACION int,  
         IDAUTORIZACION BigInt,  
         CODIGOREGIONDIRECCION int,  
         NROPROYECTOS int,  
         NOMBREMAESTROPROGRAMA VarChar(200),  
         NOMBREMAESTROTIPOLOGIA VarChar(200),  
         NOMBREMAESTROMODALIDAD VarChar(200),  
         CLASE VarChar(200),  
         NOMBREMAESTROTIPOPROVEEDOR VarChar(200),  
         RUTPROVEEDOR VarChar(20),  
         NOMBREPROVEEDOR VarChar(200),  
         MONTOTOTALAUTORIZACION BigInt,  
         NOMBREMAESTROESTADOAUTORIZACION VarChar(200),  
         STRINGSOLICITUD VarChar(1000),  
         STRINGPROYECTO VarChar(1000))  
  
 INSERT INTO @AUTORIZACIONES (NROAUTORIZACION,  
        IDAUTORIZACION,  
        CODIGOREGIONDIRECCION,  
        NROPROYECTOS,  
        NOMBREMAESTROPROGRAMA,  
        NOMBREMAESTROTIPOLOGIA,  
        NOMBREMAESTROMODALIDAD,  
        NOMBREMAESTROTIPOPROVEEDOR,  
        RUTPROVEEDOR,  
        NOMBREPROVEEDOR,  
        MONTOTOTALAUTORIZACION,  
        NOMBREMAESTROESTADOAUTORIZACION,  
        CLASE)  
 SELECT DISTINCT  
   NROAUTORIZACION = CONVERT(VarChar,A.IDAUTORIZACION) +   
        (CASE  
         WHEN LEN(CONVERT(VarChar,D.CODIGOREGIONDIRECCION)) = 1 THEN '0' + CONVERT(VarChar,D.CODIGOREGIONDIRECCION)  
        ELSE  
         CONVERT(VarChar,D.CODIGOREGIONDIRECCION)  
        END) +  
        SUBSTRING(CONVERT(VarChar,A.FECHAINGRESOAUTORIZACION,112), 1, 4),--N°Autotizacion / ID Autorizacion + IDRegion + Año  
   A.IDAUTORIZACION,     --ID Autorizacion  
   D.CODIGOREGIONDIRECCION,   --Region  
   NROPROYECTOS = (SELECT COUNT(1)  
       FROM INFORMACION_PROYECTO IP  
        INNER JOIN CARACTERISTICAS_ESPECIALES C  
         ON C.IDINFORMACIONPROYECTO = IP.IDINFORMACIONPROYECTO  
        INNER JOIN SOLICITUD_PAGO S  
         ON S.IDCARACTERISTICASESPECIALES = C.IDCARACTERISTICASESPECIALES  
        INNER JOIN TIPO_AUTORIZACION TA  
         ON TA.IDSOLICITUDPAGO = S.IDSOLICITUDPAGO  
       WHERE TA.IDAUTORIZACION = A.IDAUTORIZACION),--N°Proyectos  
   MP.NOMBREMAESTROPROGRAMA,   --PROGRAMA  
   MT.NOMBREMAESTROTIPOLOGIA,   --Tipologia  
   MM.NOMBREMAESTROMODALIDAD,--Modalidad  
   MTP.NOMBREMAESTROTIPOPROVEEDOR,     --TipoProveedor  
   RutProveedor = P.RUTPROVEEDOR + '-' + P.DVPROVEDIGITOVERIFICADORPROVEEDOR,--RutProveedor  
   P.NOMBREPROVEEDOR,     --Nombre Proveedor  
   A.MONTOTOTALAUTORIZACION,   --MontoAutorizacion  
   E.NOMBREMAESTROESTADOAUTORIZACION, --EstadoAutorizacion  
   CLASE = CASE  
      WHEN ISNULL(MC.NOMBREMAESTROCLASE, 0) = 0 THEN 'NO APLICA'  
     ELSE  
      MC.NOMBREMAESTROCLASE  
     END--Clase  
 FROM AUTORIZACION A  
  INNER JOIN TIPO_AUTORIZACION TA  
   ON TA.IDAUTORIZACION = A.IDAUTORIZACION  
  INNER JOIN SOLICITUD_PAGO S  
   ON S.IDSOLICITUDPAGO = TA.IDSOLICITUDPAGO  
  INNER JOIN CARACTERISTICAS_ESPECIALES C  
   ON C.IDCARACTERISTICASESPECIALES = S.IDCARACTERISTICASESPECIALES  
  INNER JOIN INFORMACION_PROYECTO IP  
   ON IP.IDINFORMACIONPROYECTO = C.IDINFORMACIONPROYECTO  
  INNER JOIN DIRECCION D  
   ON D.IDDIRECCION = IP.IDDIRECCION  
  INNER JOIN MAESTRO_PROGRAMA MP  
   ON MP.IDMAESTROPROGRAMA = IP.IDMAESTROPROGRAMA  
  INNER JOIN MAESTRO_TIPOLOGIA MT  
   ON MT.IDMAESTROTIPOLOGIA = C.IDMAESTROTIPOLOGIA  
  INNER JOIN MAESTRO_MODALIDAD MM  
   ON MM.IDMAESTROMODALIDAD = IP.IDMAESTROMODALIDAD  
  INNER JOIN TIPO_PROVEEDOR_INFORMACION_PROYECTO TP  
   ON TP.IDINFORMACIONPROYECTO = IP.IDINFORMACIONPROYECTO  
  INNER JOIN PROVEEDOR P  
   ON P.IDPROVEEDOR = TP.IDPROVEEDOR  
  INNER JOIN MAESTRO_TIPO_PROVEEDOR MTP  
   ON MTP.IDMAESTROTIPOPROVEEDOR = P.IDMAESTROTIPOPROVEEDOR  
  INNER JOIN MAESTRO_ESTADO_AUTORIZACION E  
   ON E.IDMAESTROESTADOAUTORIZACION = A.IDMAESTROESTADOAUTORIZACION  
  LEFT JOIN MAESTRO_CLASE MC  
   ON MC.IDMAESTROCLASE = C.IDMAESTROCLASE  
 WHERE ((@CODIGOPROYECTO = 0) OR (IP.CODIGOPROYECTOINFORMACIONPROYECTO = @CODIGOPROYECTO))  
  AND ((@IDMAESTROPROGRAMA = 0) OR (IP.IDMAESTROPROGRAMA = @IDMAESTROPROGRAMA))  
  AND ((@IDMAESTROTIPOLOGIA = 0) OR (MT.IDMAESTROTIPOLOGIA = @IDMAESTROTIPOLOGIA))  
  AND ((@IDAUTORIZACION = 0) OR (A.IDAUTORIZACION = @IDAUTORIZACION))  
  AND ((@NOMBREPROVEEDOR = '') OR (P.NOMBREPROVEEDOR LIKE(@NOMBREPROVEEDOR)))  
  AND ((@IDMAESTROMODALIDAD = 0) OR (MM.IDMAESTROMODALIDAD = @IDMAESTROMODALIDAD))  
  AND ((@CODIGOREGIONDIRECCION = 0) OR (D.CODIGOREGIONDIRECCION = @CODIGOREGIONDIRECCION))  
  AND ((@CODIGOPROVINCIADIRECCION = 0) OR (D.CODIGOPROVINCIADIRECCION = @CODIGOPROVINCIADIRECCION))  
  AND ((@CODIGOCOMUNADIRECCION = 0) OR (D.CODIGOCOMUNADIRECCION = @CODIGOCOMUNADIRECCION))  
  AND ((@IDMAESTROTIPOPROVEEDOR = 0) OR (MTP.IDMAESTROTIPOPROVEEDOR = @IDMAESTROTIPOPROVEEDOR))  
  AND ((@IDMAESTROESTADOAUTORIZACION = 0) OR (E.IDMAESTROESTADOAUTORIZACION = @IDMAESTROESTADOAUTORIZACION))  
  
 ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
  
 --Creacion de tabla para recorrer cada Autorizacion  
 DECLARE @TABLAPASO TABLE(NROAUTORIZACION int,  
       IDAUTORIZACION BigInt,  
       CODIGOREGIONDIRECCION int,  
       NROPROYECTOS int,  
       NOMBREMAESTROPROGRAMA VarChar(200),  
       NOMBREMAESTROTIPOLOGIA VarChar(200),  
       NOMBREMAESTROMODALIDAD VarChar(200),  
       CLASE VarChar(200),  
       NOMBREMAESTROTIPOPROVEEDOR VarChar(200),  
       RUTPROVEEDOR VarChar(20),  
       NOMBREPROVEEDOR VarChar(200),  
       MONTOTOTALAUTORIZACION BigInt,  
       NOMBREMAESTROESTADOAUTORIZACION VarChar(200),  
       STRINGSOLICITUD VarChar(1000),  
       STRINGPROYECTO VarChar(1000))  
 INSERT INTO @TABLAPASO  
 SELECT * FROM @AUTORIZACIONES  
  
 --Creacion de Tablas para obtener Solicitudes y Proyectos contenidos para cada Autorizacion  
 DECLARE @TABLASOLICITUDES TABLE (IDSOLICITUD BigInt, UFSOLICITUD DECIMAL, IDAUTORIZACION BigInt)  
 DECLARE @TABLAPROYECTOS TABLE (IDPROYECTO BigInt, CODPROYECTO int, NOMBREPROYECTO VarChar(200), IDAUTORIZACION BigInt)  
  
 --Variable que contendra IDAUTORIZACION de cada paso en el bucle  
 DECLARE @IDAUTPASO BigInt  
  
 WHILE((SELECT COUNT(1) FROM @TABLAPASO) > 0)  
 BEGIN  
  --------------------------Obtencion Solicitudes y Proyectos--------------------------  
  --Se obtiene IDAUTORIZACION  
  SET @IDAUTPASO = (SELECT TOP 1 IDAUTORIZACION FROM @TABLAPASO)  
  
  --Obtencion de Solicitudes de Pago para el IDAUTORIZACION  
  INSERT INTO @TABLASOLICITUDES(IDSOLICITUD, UFSOLICITUD, IDAUTORIZACION)  
  SELECT SP.IDSOLICITUDPAGO,  
    SP.MONTOTOTALPROYECTOSOLICITUDPAGO,  
    TN.IDAUTORIZACION  
  FROM SOLICITUD_PAGO SP  
   INNER JOIN TIPO_AUTORIZACION TN  
    ON SP.IDSOLICITUDPAGO = TN.IDSOLICITUDPAGO  
  WHERE TN.IDAUTORIZACION = @IDAUTPASO  
  
  --Obtencion de Proyectos para el IDAUTORIZACION  
  INSERT INTO @TABLAPROYECTOS(IDPROYECTO, CODPROYECTO, NOMBREPROYECTO, IDAUTORIZACION)  
  SELECT DISTINCT  
    IP.IDINFORMACIONPROYECTO,  
    IP.CODIGOPROYECTOINFORMACIONPROYECTO,  
    IP.NOMBREPROYECTOINFORMACIONPROYECTO,  
    TN.IDAUTORIZACION  
  FROM INFORMACION_PROYECTO IP  
   INNER JOIN CARACTERISTICAS_ESPECIALES CE  
    ON IP.IDINFORMACIONPROYECTO = CE.IDINFORMACIONPROYECTO  
   INNER JOIN SOLICITUD_PAGO SP  
    ON CE.IDCARACTERISTICASESPECIALES = SP.IDCARACTERISTICASESPECIALES  
   INNER JOIN TIPO_AUTORIZACION TN  
    ON SP.IDSOLICITUDPAGO = TN.IDSOLICITUDPAGO  
  WHERE TN.IDAUTORIZACION = @IDAUTPASO  
  
  --Eliminacion del registro ya utilizado  
  DELETE FROM @TABLAPASO WHERE IDAUTORIZACION = @IDAUTPASO  
  --------------------------Obtencion Solicitudes y Proyectos--------------------------  
  
  ----------------------------------------------------Obtencion de STRING con Leyenda----------------------------------------------------  
  DECLARE @IDCODPASO BigInt  
  
  ---------------------------------------------------OBTENCION STRING SOLICITUDES---------------------------------------------------  
  DECLARE @STRINGSOLICITUDES VarChar(1000)  
  SET @STRINGSOLICITUDES = ''  
  
  WHILE((SELECT COUNT(1) FROM @TABLASOLICITUDES) > 0)  
  BEGIN  
   --Se obtiene IDAUTORIZACION  
   SET @IDCODPASO = (SELECT TOP 1 IDSOLICITUD FROM @TABLASOLICITUDES WHERE IDAUTORIZACION = @IDAUTPASO)  
   
   --Obtencion de Solicitudes de Pago para el IDAUTORIZACION  
   SET @STRINGSOLICITUDES = (SELECT @STRINGSOLICITUDES) + ' + ' + 'ID ' +  
         CONVERT(varchar,(SELECT IDSOLICITUD FROM @TABLASOLICITUDES WHERE IDSOLICITUD = @IDCODPASO)) + ' = ' +  
         CONVERT(varchar,(SELECT UFSOLICITUD FROM @TABLASOLICITUDES WHERE IDSOLICITUD = @IDCODPASO)) + 'UF'  
   
   --Eliminacion del registro ya utilizado  
   DELETE FROM @TABLASOLICITUDES WHERE IDSOLICITUD = @IDCODPASO  
  END  
  
  SET @STRINGSOLICITUDES = 'SOLICITUDES:' + SUBSTRING(@STRINGSOLICITUDES, 3, LEN(@STRINGSOLICITUDES))  
  --SELECT @STRINGSOLICITUDES  
  ---------------------------------------------------OBTENCION STRING SOLICITUDES---------------------------------------------------  
  
  ---------------------------------------------------OBTENCION STRING PROYECTOS---------------------------------------------------  
  DECLARE @STRINGPROYECTOS VarChar(1000)  
  SET @STRINGPROYECTOS = ''  
  
  WHILE((SELECT COUNT(1) FROM @TABLAPROYECTOS) > 0)  
  BEGIN  
   --Se obtiene IDAUTORIZACION  
   SET @IDCODPASO = (SELECT TOP 1 IDPROYECTO FROM @TABLAPROYECTOS)  
   
   --Obtencion de Solicitudes de Pago para el IDAUTORIZACION  
   SET @STRINGPROYECTOS = (SELECT @STRINGPROYECTOS) + ' / ' + 'COD. ' +  
         CONVERT(varchar,(SELECT CODPROYECTO FROM @TABLAPROYECTOS WHERE IDPROYECTO = @IDCODPASO)) + ': ' +  
         CONVERT(varchar,(SELECT NOMBREPROYECTO FROM @TABLAPROYECTOS WHERE IDPROYECTO = @IDCODPASO))  
   
   --Eliminacion del registro ya utilizado  
   DELETE FROM @TABLAPROYECTOS WHERE IDPROYECTO = @IDCODPASO  
  END  
  
  SET @STRINGPROYECTOS = SUBSTRING(@STRINGPROYECTOS, 4, LEN(@STRINGPROYECTOS))  
  --SELECT @STRINGPROYECTOS  
  ---------------------------------------------------OBTENCION STRING PROYECTOS---------------------------------------------------  
  IF(LEN(@STRINGSOLICITUDES) > 12)  
  BEGIN  
   UPDATE @AUTORIZACIONES  
   SET STRINGSOLICITUD = @STRINGSOLICITUDES,  
    STRINGPROYECTO = @STRINGPROYECTOS  
   WHERE IDAUTORIZACION = @IDAUTPASO  
  END  
  ----------------------------------------------------Obtencion de STRING con Leyenda----------------------------------------------------  
 END  
  
 SELECT NROAUTORIZACION,  
   IDAUTORIZACION,  
   CODIGOREGIONDIRECCION,  
   NROPROYECTOS,  
   NOMBREMAESTROPROGRAMA,  
   NOMBREMAESTROTIPOLOGIA,  
   NOMBREMAESTROMODALIDAD,  
   CLASE,  
   NOMBREMAESTROTIPOPROVEEDOR,  
   RUTPROVEEDOR,  
   NOMBREPROVEEDOR,  
   MONTOTOTALAUTORIZACION,  
   NOMBREMAESTROESTADOAUTORIZACION,  
   STRINGSOLICITUD,  
   STRINGPROYECTO  
 FROM @AUTORIZACIONES  
END  
  

GO
/****** Object:  StoredProcedure [dbo].[BUSQUEDA_DETALLE_AUTORIZACION_DE_PAGO]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/********************************************************************************************        
NOMBRE DEL PROCEDIMIENTO  : BUSQUEDA_DETALLE_AUTORIZACION_DE_PAGO        
FECHA DE CREACIÓN        : 23-08-2018      
USUARIO DE CREACIÓN       : Daniel Orozco - Anticipa.      
VERSIÓN               : 1.0           
  
Fecha de modificación   : 27-03-2019    
Usuario de modificación : cfajardo  
Motivo de modificación  : extracción de montos servicios.                           
      
Visado por DBA              : Iván Frade Cortés     
Fecha Aprobación DBA        : 20190527       
Comentarios DBA             :   ---    
                
OBJETIVO         : Obtiene el detalle en resumen de las Autorizaciones de pago      
                     segun filtros de busqueda      
        
TABLAS          : AUTORIZACION A      
         TIPO_AUTORIZACION TA      
         SOLICITUD_PAGO S      
         CARACTERISTICAS_ESPECIALES C      
         INFORMACION_PROYECTO IP      
         DIRECCION D      
         TIPO_PROVEEDOR_INFORMACION_PROYECTO TP      
         PROVEEDOR P      
         MAESTRO_TIPO_PROVEEDOR MTP      
         MAESTRO_ESTADO_AUTORIZACION E      
         MAESTRO_LLAMADO ML      
         MAESTRO_CLASE MC      
        
                
        
QUE RETORNA        : A.IDAUTORIZACION,      
                     D.CODIGOREGIONDIRECCION,      
                     NROAUTORIZACION      
                     A.FECHAINGRESOAUTORIZACION,      
                     NROPROYECTOS       
         ML.NOMBREMAESTROLLAMADO,      
                     CANTIDADSOLICITUDES       
                     P.NOMBREPROVEEDOR,      
                     RutProveedor,      
                     E.NOMBREMAESTROESTADOAUTORIZACION,      
                     A.MONTOTOTALAUTORIZACION,      
                     MTP.NOMBREMAESTROTIPOPROVEEDOR,      
                     S.USUARIORESPONSABLESOLICITUDPAGO      
        
PARAMETROS         :@IDAUTORIZACION BigInt      
                  
                 
PROYECTO         : SNAT SIMPLIFICADO      
RESPONSABLE        : DINFO        
               
********************************************************************************************/        
CREATE PROCEDURE [dbo].[BUSQUEDA_DETALLE_AUTORIZACION_DE_PAGO]      
 @IDAUTORIZACION BigInt      
AS      
BEGIN      
 SELECT DISTINCT      
   A.IDAUTORIZACION,      
   D.CODIGOREGIONDIRECCION,      
   NROAUTORIZACION = CONVERT(VarChar,A.IDAUTORIZACION) +       
        (CASE      
         WHEN LEN(CONVERT(VarChar,D.CODIGOREGIONDIRECCION)) = 1 THEN '0' + CONVERT(VarChar,D.CODIGOREGIONDIRECCION)      
        ELSE      
         CONVERT(VarChar,D.CODIGOREGIONDIRECCION)      
        END) +      
        SUBSTRING(CONVERT(VarChar,A.FECHAINGRESOAUTORIZACION,112), 1, 4),      
  Convert(varchar,FECHAINGRESOAUTORIZACION,105) as FECHAINGRESOAUTORIZACION,      
   NROPROYECTOS = A.CANTIDADPROYECTOSAUTORIZACION,      
   ML.NOMBREMAESTROLLAMADO,      
   CANTIDADSOLICITUDES = (SELECT COUNT(1)      
         FROM TIPO_AUTORIZACION      
         WHERE IDAUTORIZACION = A.IDAUTORIZACION),      
   P.NOMBREPROVEEDOR,      
   RutProveedor = Convert(varchar,P.RUTPROVEEDOR) + ' - ' + Convert(varchar,P.DVPROVEDIGITOVERIFICADORPROVEEDOR),      
   E.NOMBREMAESTROESTADOAUTORIZACION,      
   A.MONTOTOTALAUTORIZACION,      
   MTP.NOMBREMAESTROTIPOPROVEEDOR,      
   S.USUARIORESPONSABLESOLICITUDPAGO,      
   MP.NOMBREMAESTROPROGRAMA    ,
   A.USUARIORESPONSABLEAUTORIZACION  
 FROM AUTORIZACION A      
  INNER JOIN TIPO_AUTORIZACION TA      
   ON TA.IDAUTORIZACION = A.IDAUTORIZACION      
  INNER JOIN SOLICITUD_PAGO S      
   ON S.IDSOLICITUDPAGO = TA.IDSOLICITUDPAGO      
  INNER JOIN CARACTERISTICAS_ESPECIALES C      
   ON C.IDCARACTERISTICASESPECIALES = S.IDCARACTERISTICASESPECIALES      
  INNER JOIN INFORMACION_PROYECTO IP      
   ON IP.IDINFORMACIONPROYECTO = C.IDINFORMACIONPROYECTO      
  INNER JOIN DIRECCION D      
   ON D.IDDIRECCION = IP.IDDIRECCION      
  INNER JOIN TIPO_PROVEEDOR_INFORMACION_PROYECTO TP      
   ON TP.IDINFORMACIONPROYECTO = IP.IDINFORMACIONPROYECTO      
  INNER JOIN PROVEEDOR P      
   ON P.IDPROVEEDOR = A.IDPROVEEDOR      
  INNER JOIN MAESTRO_TIPO_PROVEEDOR MTP      
   ON MTP.IDMAESTROTIPOPROVEEDOR = P.IDMAESTROTIPOPROVEEDOR      
  INNER JOIN MAESTRO_ESTADO_AUTORIZACION E      
   ON E.IDMAESTROESTADOAUTORIZACION = A.IDMAESTROESTADOAUTORIZACION      
  INNER JOIN MAESTRO_LLAMADO ML      
   ON ML.IDMAESTROLLAMADO = IP.IDMAESTROLLAMADO      
  INNER JOIN MAESTRO_PROGRAMA MP      
   ON MP.IDMAESTROPROGRAMA = IP.IDMAESTROPROGRAMA      
  LEFT JOIN MAESTRO_CLASE MC      
   ON MC.IDMAESTROCLASE = C.IDMAESTROCLASE      
 WHERE ((@IDAUTORIZACION = 0) OR (A.IDAUTORIZACION = @IDAUTORIZACION))      
END

GO
/****** Object:  StoredProcedure [dbo].[BUSQUEDA_DETALLE_AUTORIZACION_DE_PAGO_GRILLA]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/********************************************************************************************              
NOMBRE DEL PROCEDIMIENTO  : [BUSQUEDA_DETALLE_AUTORIZACION_DE_PAGO_GRILLA]              
FECHA DE CREACIÓN         : 23-08-2018            
USUARIO DE CREACIÓN       : Daniel Orozco - Anticipa.            
VERSIÓN               : 1.0                                        
            
Fecha de modificación   : 27-03-2019          
Usuario de modificación : cfajardo        
Motivo de modificación  : regularización de servicios.      
  
Fecha de modificación   : 27-03-2019          
Usuario de modificación : cfajardo        
Motivo de modificación  : Se ingresan las columnas codigoRegionDireccion y CodigoComuna.

Visado por DBA			: Iván Frade Cortés          
Fecha					: 24042019
Observación				: ---

                      
OBJETIVO         : Obtiene el detalle de todos los Proyectos de la(s)            
                              Autorizacion(es) de pago segun filtros de busqueda            
              
TABLAS          : AUTORIZACION A            
         SOLICITUD_PAGO S            
         CARACTERISTICAS_ESPECIALES C            
         INFORMACION_PROYECTO IP            
         DIRECCION D            
         MAESTRO_PROGRAMA MP            
         MAESTRO_TIPOLOGIA MT            
         MAESTRO_CLASE MC            
         TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA SC            
         MAESTRO_ESTADO_SOLICITUD ES            
              
                      
              
QUE RETORNA        : C.IDCARACTERISTICASESPECIALES,            
         IP.IDINFORMACIONPROYECTO,            
         IP.CODIGOPROYECTOINFORMACIONPROYECTO,            
         IP.NOMBREPROYECTOINFORMACIONPROYECTO,            
         D.CODIGOREGIONDIRECCION,            
         MP.NOMBREMAESTROPROGRAMA,            
         MT.NOMBREMAESTROTIPOLOGIA,            
         CLASE,            
         A.MONTOTOTALAUTORIZACION,            
         MontoaPagar,            
         S.IDSOLICITUDPAGO            
              
PARAMETROS         :@IDAUTORIZACION BigInt            
                        
                       
PROYECTO         : SNAT SIMPLIFICADO            
RESPONSABLE        : DINFO              
                     BUSQUEDA_DETALLE_AUTORIZACION_DE_PAGO_GRILLA 30243, 7
********************************************************************************************/              
CREATE PROCEDURE [dbo].[BUSQUEDA_DETALLE_AUTORIZACION_DE_PAGO_GRILLA]       
@IDAUTORIZACION BigInt,            
@IDTIPOPROVEEDOR BigInt            
AS            
BEGIN            
            
    DECLARE @TablaResult TABLE(IDCARACTERISTICASESPECIALES BigInt,            
        IDINFORMACIONPROYECTO BigInt,            
        CODIGOPROYECTOINFORMACIONPROYECTO int,            
        NOMBREPROYECTOINFORMACIONPROYECTO VarChar(200),            
        CODIGOREGIONDIRECCION int,            
        CODIGOCOMUNA VarChar(200),            
        CODIGOPROVINCIA VarChar(200),            
        NOMBREMAESTROPROGRAMA VarChar(200),            
        NOMBREMAESTROTIPOLOGIA VarChar(200),            
        CLASE VarChar(200),            
        S1 decimal(18,3) NULL,            
        S2 decimal(18,3) NULL,            
        S3 decimal(18,3) NULL,            
        S4 decimal(18,3) NULL,            
        S5 decimal(18,3) NULL,            
        S6 decimal(18,3) NULL,            
        S7 decimal(18,3) NULL,            
        S8 decimal(18,3) NULL,            
        S9 decimal(18,3) NULL,            
        S10 decimal(18,3) NULL,            
        MONTOFTO decimal(18,3) NULL,            
        SALDOFTO decimal(18,3) NULL,            
        PAGOARANCEL decimal(18,3) NULL,            
        MONTOTOTALPROYECTO decimal(18,3),            
        MontoaPagar decimal(18,3),            
        IDSOLICITUDPAGO BigInt)            
            
    INSERT INTO @TablaResult(IDCARACTERISTICASESPECIALES,            
        IDINFORMACIONPROYECTO,            
        CODIGOPROYECTOINFORMACIONPROYECTO,            
        NOMBREPROYECTOINFORMACIONPROYECTO,            
        CODIGOREGIONDIRECCION,            
        CODIGOCOMUNA,      
        CODIGOPROVINCIA ,          
        NOMBREMAESTROPROGRAMA,            
        NOMBREMAESTROTIPOLOGIA,            
        CLASE,            
        MONTOTOTALPROYECTO,            
        MontoaPagar,            
        IDSOLICITUDPAGO)            
    SELECT DISTINCT            
        C.IDCARACTERISTICASESPECIALES,            
        IP.IDINFORMACIONPROYECTO,            
        IP.CODIGOPROYECTOINFORMACIONPROYECTO,            
        IP.NOMBREPROYECTOINFORMACIONPROYECTO,            
        D.CODIGOREGIONDIRECCION,            
        D.CODIGOCOMUNADIRECCION,            
        D.CODIGOPROVINCIADIRECCION,            
        MP.NOMBREMAESTROPROGRAMA,            
        MT.NOMBREABREVIADOMAESTROTIPOLOGIA,            
        CLASE = CASE            
        WHEN MC.NOMBREMAESTROCLASE = NULL THEN 'NO APLICA'            
        ELSE            
            MC.NOMBREMAESTROCLASE            
        END,            
        0,            
        MontoaPagar = 0,            
        S.IDSOLICITUDPAGO            
    FROM AUTORIZACION A            
        INNER JOIN TIPO_AUTORIZACION TA                       ON TA.IDAUTORIZACION = A.IDAUTORIZACION            
        INNER JOIN SOLICITUD_PAGO S                           ON S.IDSOLICITUDPAGO = TA.IDSOLICITUDPAGO            
        INNER JOIN CARACTERISTICAS_ESPECIALES C               ON C.IDCARACTERISTICASESPECIALES = S.IDCARACTERISTICASESPECIALES            
        INNER JOIN INFORMACION_PROYECTO IP                    ON IP.IDINFORMACIONPROYECTO = C.IDINFORMACIONPROYECTO            
        INNER JOIN DIRECCION D                                ON D.IDDIRECCION = IP.IDDIRECCION            
        INNER JOIN MAESTRO_PROGRAMA MP                        ON MP.IDMAESTROPROGRAMA = IP.IDMAESTROPROGRAMA            
        INNER JOIN MAESTRO_TIPOLOGIA MT                       ON MT.IDMAESTROTIPOLOGIA = C.IDMAESTROTIPOLOGIA            
        LEFT JOIN MAESTRO_CLASE MC                            ON MC.IDMAESTROCLASE = C.IDMAESTROCLASE            
        LEFT JOIN TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA SC ON SC.IDCARACTERISTICASESPECIALES = C.IDCARACTERISTICASESPECIALES            
        LEFT JOIN MAESTRO_ESTADO_SOLICITUD ES                 ON ES.IDMAESTROESTADOSOLICITUD = S.IDMAESTROESTADOSOLICITUD            
    WHERE ((@IDAUTORIZACION = 0) OR (A.IDAUTORIZACION = @IDAUTORIZACION))            
    AND ES.IDMAESTROESTADOSOLICITUD NOT IN(2)  --Se excluyen los rechazados            
          
    -- select * from @TablaResult
	            
 --------------Creacion y poblado de tabla Caracteristicas especiales--------------            
    DECLARE @CaracSol TABLE            
    (            
        IDCARACSOL BigInt IDENTITY,            
        IDCARACTERISTICASESPECIALES BigInt,            
        IDSOLICITUDPAGO BigInt            
    )            
            
    INSERT INTO @CaracSol(IDCARACTERISTICASESPECIALES, IDSOLICITUDPAGO)            
    SELECT IDCARACTERISTICASESPECIALES, IDSOLICITUDPAGO            
    FROM @TablaResult

	-- select * from @CaracSol 
    
--------------Creacion y poblado de tabla Caracteristicas especiales--------------            

--------------Creacion de tabla Monto Servicio--------------            
    DECLARE @MONTOSERV TABLE
    (
        IDTIPOSERVICIOPARCIALIDADCARACTERISTICA bigint, 
		IDMAESTROSERVICIO BigInt,            
        MONTOSERVICIO decimal(18,3) NULL,            
        MONTOTOTALPARCIALIDAD decimal(18,3) NULL,            
        IDCARACTERISTICASESPECIALES BigInt,            
        IDSOLICITUDPAGO BigInt
	)
--------------Creacion de tabla Monto Servicio--------------     
            
--  Variable que contendra el IDCARACTERISTICAESPECIAL y IDSOLICITUDPAGO en el bucle--            
    DECLARE @IDCARESP BigInt            
    DECLARE @IDSOLPAG BigInt            
 ------------------------------------------------------------------------------------            
            
--------------------------Recorre cada CaracteristicaEspecial--------------------------            
    WHILE ((SELECT COUNT(1) FROM @CaracSol) > 0)            
    BEGIN            
        ------------------------------Obtencios de ID------------------------------            
        SET @IDCARESP = (SELECT TOP 1 IDCARACTERISTICASESPECIALES FROM @CaracSol)            
        SET @IDSOLPAG = (SELECT TOP 1 IDSOLICITUDPAGO FROM @CaracSol)            
        ------------------------------Obtencios de ID------------------------------            
            
        ------------------------Actualiza Monto Total Proyecto------------------------            
        UPDATE @TablaResult            
        SET MONTOTOTALPROYECTO =   (SELECT SALDOPORPAGARSOLICITUDPAGO            
             FROM SOLICITUD_PAGO            
            WHERE IDCARACTERISTICASESPECIALES = @IDCARESP            
              AND IDSOLICITUDPAGO = @IDSOLPAG            
              AND IDMAESTROESTADOSOLICITUD NOT IN(4))            
        WHERE IDCARACTERISTICASESPECIALES = @IDCARESP            
         AND IDSOLICITUDPAGO = @IDSOLPAG              
        ------------------------Actualiza Monto Total Proyecto------------------------            
            
        ------------------------------Monto a Pagar------------------------------            
        UPDATE @TablaResult            
        SET  MontoaPagar = (SELECT MONTOSOLICITUDSOLICITUDPAGO            
            FROM SOLICITUD_PAGO            
            WHERE IDCARACTERISTICASESPECIALES = @IDCARESP            
              AND IDSOLICITUDPAGO = @IDSOLPAG)            
        WHERE IDCARACTERISTICASESPECIALES = @IDCARESP            
         AND IDSOLICITUDPAGO = @IDSOLPAG     
        ------------------------------Monto a Pagar------------------------------            
             
        --------------------------------------------------------------SELECCIONA PLANTILLA-------------------------------------------------------------- 
        
		--SELECCIONA PLANTILLA CON SERVICIOS Y VALORES SEGUN IDCARACTERISTICA E IDSOLICITUD            
        INSERT INTO @MONTOSERV(IDTIPOSERVICIOPARCIALIDADCARACTERISTICA,IDMAESTROSERVICIO,MONTOSERVICIO,MONTOTOTALPARCIALIDAD,IDCARACTERISTICASESPECIALES,IDSOLICITUDPAGO)            
        SELECT           
            PL.IDTIPOSERVICIOPARCIALIDADCARACTERISTICA,          
            MS.IDMAESTROSERVICIO,            
            MONTOSERVICIO = ISNULL(PL.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA, 0) + ISNULL(PL.MONTOASIGNACIONDIRECTATIPOSERVICIOPARCIALIDADCARACTERISTICA, 0),            
            MONTOTOTALPARCIALIDAD = ISNULL(MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA,0),          
            @IDCARESP,            
            @IDSOLPAG            
        FROM TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA PL            
            INNER JOIN SERVICIO_PARCIALIDAD SP   ON PL.IDSERVICIOPARCIALIDAD = SP.IDSERVICIOPARCIALIDAD            
            INNER JOIN TIPOLOGIA_SERVICIO TS     ON SP.IDTIPOLOGIASERVICIO = TS.IDTIPOLOGIASERVICIO            
            INNER JOIN  MAESTRO_SERVICIO MS      ON TS.IDMAESTROSERVICIO = MS.IDMAESTROSERVICIO            
        WHERE PL.IDCARACTERISTICASESPECIALES = @IDCARESP            
          AND PL.IDSOLICITUDPAGO = @IDSOLPAG            
          -- AND PL.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA IS NOT NULL 
        ORDER BY 2
		             
        --------------------------------------------------------------SELECCIONA PLANTILLA--------------------------------------------------------------    

		 --select * from  @MONTOSERV

        ------------------------------------------Actualizacion de Montos por Servicio------------------------------------------            
        DECLARE @IDSERVICIO bigint,
		    @IDSERVICIOANT bigint,
		    @IDTIPOSERVICIOPARCIALIDADCARACTERISTICA bigint,            
            @IDMONTO int,            
            @CONT int            
        
		SET @IDSERVICIOANT = 0          
        SET @CONT = 0             
                    
        WHILE ((SELECT COUNT(1) FROM @MONTOSERV) > 0)            
        BEGIN            
            SET @IDCARESP = (SELECT TOP 1 IDCARACTERISTICASESPECIALES FROM @MONTOSERV ORDER BY IDTIPOSERVICIOPARCIALIDADCARACTERISTICA)            
            SET @IDSOLPAG = (SELECT TOP 1 IDSOLICITUDPAGO FROM @MONTOSERV ORDER BY IDTIPOSERVICIOPARCIALIDADCARACTERISTICA)            
            SET @IDSERVICIO = (SELECT TOP 1 IDMAESTROSERVICIO FROM @MONTOSERV ORDER BY IDTIPOSERVICIOPARCIALIDADCARACTERISTICA)            
            SET @IDTIPOSERVICIOPARCIALIDADCARACTERISTICA = (SELECT TOP 1 IDTIPOSERVICIOPARCIALIDADCARACTERISTICA FROM @MONTOSERV ORDER BY IDTIPOSERVICIOPARCIALIDADCARACTERISTICA)

			-- select @CONT, @IDCARESP, @IDSOLPAG, @IDSERVICIO, @IDTIPOSERVICIOPARCIALIDADCARACTERISTICA
            --Proveedor EC, EP Servicios, para Servicios FTO ni Pago Aranceles            
            IF(((@IDTIPOPROVEEDOR = 1) OR (@IDTIPOPROVEEDOR = 2)) AND (@IDSERVICIO NOT IN(5,6)))            
            BEGIN
			
			IF (@IDSERVICIOANT <> @IDSERVICIO) SET @CONT = @CONT + 1
			
			SET @IDSERVICIOANT = @IDSERVICIO 
			            
            IF(@CONT = 1)            
            BEGIN            
                UPDATE @TablaResult            
                SET S1 = (SELECT SUM(MONTOTOTALPARCIALIDAD)            
                    FROM @MONTOSERV            
                   WHERE IDMAESTROSERVICIO = @IDSERVICIO            
                    AND IDCARACTERISTICASESPECIALES = @IDCARESP            
                    AND IDSOLICITUDPAGO = @IDSOLPAG)            
                WHERE IDCARACTERISTICASESPECIALES = @IDCARESP            
                  AND IDSOLICITUDPAGO = @IDSOLPAG            
            END            
            ELSE IF(@CONT = 2)            
            BEGIN            
                UPDATE @TablaResult            
                SET S2 = (SELECT SUM(MONTOTOTALPARCIALIDAD)            
                    FROM @MONTOSERV            
                   WHERE IDMAESTROSERVICIO = @IDSERVICIO            
                    AND IDCARACTERISTICASESPECIALES = @IDCARESP            
                    AND IDSOLICITUDPAGO = @IDSOLPAG)            
                WHERE IDCARACTERISTICASESPECIALES = @IDCARESP            
                  AND IDSOLICITUDPAGO = @IDSOLPAG            
            END    
            ELSE IF(@CONT = 3)            
            BEGIN            
                UPDATE @TablaResult            
                SET S3 = (SELECT SUM(MONTOTOTALPARCIALIDAD)            
                    FROM @MONTOSERV            
                   WHERE IDMAESTROSERVICIO = @IDSERVICIO            
                     AND IDCARACTERISTICASESPECIALES = @IDCARESP            
                     AND IDSOLICITUDPAGO = @IDSOLPAG)            
                WHERE IDCARACTERISTICASESPECIALES = @IDCARESP            
                  AND IDSOLICITUDPAGO = @IDSOLPAG            
            END  
            ELSE IF(@CONT = 4)            
            BEGIN            
                UPDATE @TablaResult            
                SET S4 = (SELECT SUM(MONTOTOTALPARCIALIDAD)            
                    FROM @MONTOSERV            
                   WHERE IDMAESTROSERVICIO = @IDSERVICIO            
                     AND IDCARACTERISTICASESPECIALES = @IDCARESP            
                     AND IDSOLICITUDPAGO = @IDSOLPAG)            
                WHERE IDCARACTERISTICASESPECIALES = @IDCARESP            
                  AND IDSOLICITUDPAGO = @IDSOLPAG            
            END            
            ELSE IF(@CONT = 5)            
            BEGIN            
                UPDATE @TablaResult            
                SET S5 = (SELECT SUM(MONTOTOTALPARCIALIDAD)            
                    FROM @MONTOSERV            
                   WHERE IDMAESTROSERVICIO = @IDSERVICIO            
                     AND IDCARACTERISTICASESPECIALES = @IDCARESP            
                     AND IDSOLICITUDPAGO = @IDSOLPAG)            
                WHERE IDCARACTERISTICASESPECIALES = @IDCARESP            
                  AND IDSOLICITUDPAGO = @IDSOLPAG            
            END            
            /*ELSE IF(@CONT = 6)            
            BEGIN            
                UPDATE @TablaResult            
                SET S6 = (SELECT SUM(MONTOTOTALPARCIALIDAD)            
                    FROM @MONTOSERV            
                   WHERE IDMAESTROSERVICIO = @IDSERVICIO            
                    AND IDCARACTERISTICASESPECIALES = @IDCARESP            
                    AND IDSOLICITUDPAGO = @IDSOLPAG)            
                WHERE IDCARACTERISTICASESPECIALES = @IDCARESP            
                  AND IDSOLICITUDPAGO = @IDSOLPAG            
            END */           
            ELSE IF(@CONT = 7)            
            BEGIN            
                UPDATE @TablaResult            
                SET S7 = (SELECT SUM(MONTOTOTALPARCIALIDAD)            
                    FROM @MONTOSERV            
                   WHERE IDMAESTROSERVICIO = @IDSERVICIO            
                     AND IDCARACTERISTICASESPECIALES = @IDCARESP            
                     AND IDSOLICITUDPAGO = @IDSOLPAG)            
                WHERE IDCARACTERISTICASESPECIALES = @IDCARESP            
                  AND IDSOLICITUDPAGO = @IDSOLPAG            
            END            
            ELSE IF(@CONT = 8)            
            BEGIN            
                UPDATE @TablaResult            
                SET S8 = (SELECT SUM(MONTOTOTALPARCIALIDAD)            
                    FROM @MONTOSERV            
                   WHERE IDMAESTROSERVICIO = @IDSERVICIO            
                     AND IDCARACTERISTICASESPECIALES = @IDCARESP            
                     AND IDSOLICITUDPAGO = @IDSOLPAG)            
                WHERE IDCARACTERISTICASESPECIALES = @IDCARESP            
                  AND IDSOLICITUDPAGO = @IDSOLPAG            
            END            
            ELSE IF(@CONT = 9)            
            BEGIN            
                UPDATE @TablaResult            
                SET S9 = (SELECT SUM(MONTOTOTALPARCIALIDAD)            
                    FROM @MONTOSERV            
                   WHERE IDMAESTROSERVICIO = @IDSERVICIO            
                     AND IDCARACTERISTICASESPECIALES = @IDCARESP            
                     AND IDSOLICITUDPAGO = @IDSOLPAG)            
                WHERE IDCARACTERISTICASESPECIALES = @IDCARESP            
                  AND IDSOLICITUDPAGO = @IDSOLPAG            
            END            
            ELSE IF(@CONT = 10)            
            BEGIN            
                UPDATE @TablaResult            
                SET S10 = (SELECT SUM(MONTOTOTALPARCIALIDAD)            
                    FROM @MONTOSERV            
                   WHERE IDMAESTROSERVICIO = @IDSERVICIO            
                     AND IDCARACTERISTICASESPECIALES = @IDCARESP            
                     AND IDSOLICITUDPAGO = @IDSOLPAG)            
                WHERE IDCARACTERISTICASESPECIALES = @IDCARESP            
                  AND IDSOLICITUDPAGO = @IDSOLPAG            
            END            
            END            

            --FTO            
            IF(((@IDTIPOPROVEEDOR = 6) AND (@IDSERVICIO = 5)) OR ((@IDTIPOPROVEEDOR = 7) AND (@IDSERVICIO = 6)))
            BEGIN            
             UPDATE @TablaResult            
             SET MONTOFTO = (SELECT SUM(MONTOTOTALPARCIALIDAD)            
                  FROM @MONTOSERV            
                 WHERE IDMAESTROSERVICIO = @IDSERVICIO            
                   AND IDCARACTERISTICASESPECIALES = @IDCARESP            
                   AND IDSOLICITUDPAGO = @IDSOLPAG)            
             WHERE IDCARACTERISTICASESPECIALES = @IDCARESP            
              AND IDSOLICITUDPAGO = @IDSOLPAG            
                     
             --UPDATE @TablaResult            
             --SET SALDOFTO = (SELECT SUM(MONTOSERVICIO)            
             --     FROM @MONTOSERV            
             --    WHERE IDMONTOSERV = (@IDFTO + 1)            
             --      AND IDCARACTERISTICASESPECIALES = @IDCARESP            
             --      AND IDSOLICITUDPAGO = @IDSOLPAG)            
             --WHERE IDCARACTERISTICASESPECIALES = @IDCARESP            
            END            
                     
            --Pago Arancel            
            IF((@IDTIPOPROVEEDOR = 7) AND (@IDSERVICIO = 5))            
            BEGIN            
             UPDATE @TablaResult            
             SET PAGOARANCEL = (SELECT SUM(MONTOTOTALPARCIALIDAD)            
                   FROM @MONTOSERV            
                  WHERE IDMAESTROSERVICIO = @IDSERVICIO            
                    AND IDCARACTERISTICASESPECIALES = @IDCARESP            
                    AND IDSOLICITUDPAGO = @IDSOLPAG)            
             WHERE IDCARACTERISTICASESPECIALES = @IDCARESP            
               AND IDSOLICITUDPAGO = @IDSOLPAG            
            END            


            ------------------Se agregan MONTO y SALDO FTO si existe------------------            

            DELETE FROM @MONTOSERV WHERE IDMAESTROSERVICIO = @IDSERVICIO            
        END            
          
        ------------------------------------------Actualizacion de Montos por Servicio------------------------------------------ 

        --select * from @TablaResult 

        DELETE FROM @CaracSol WHERE IDCARACTERISTICASESPECIALES = @IDCARESP AND IDSOLICITUDPAGO = @IDSOLPAG            
    END            
    --------------------------Recorre cada CaracteristicaEspecial--------------------------            
             
          
    --Seleccion de Resultado            
    SELECT   
        t.IDCARACTERISTICASESPECIALES,            
        t.IDINFORMACIONPROYECTO,            
        t.CODIGOPROYECTOINFORMACIONPROYECTO,            
        t.NOMBREPROYECTOINFORMACIONPROYECTO,            
        t.CODIGOREGIONDIRECCION,      
        (select  Comuna from DB_SNATV_VS_CONSULTA_REGION_COMUNA_PSSIM where CodigoComuna = t.CODIGOCOMUNA and CodigoProvincia = t.CODIGOPROVINCIA) AS CODIGOCOMUNA,            
        (select  Provincia from DB_SNATV_VS_CONSULTA_REGION_COMUNA_PSSIM where CodigoProvincia = t.CODIGOPROVINCIA and CodigoComuna = t.CODIGOCOMUNA) AS CODIGOPROVINCIA,                
        t.NOMBREMAESTROPROGRAMA,            
        t.NOMBREMAESTROTIPOLOGIA,            
        t.CLASE,            
        ISNULL(t.S1,0) as S1,            
        ISNULL(t.S2,0) as S2,            
        ISNULL(t.S3,0) as S3,            
        ISNULL(t.S4,0) as S4,            
        ISNULL(t.S5,0) as S5,            
        ISNULL(t.S6,0) as S6,            
        ISNULL(t.S7,0) as S7,            
        ISNULL(t.S8,0) as S8,            
        ISNULL(t.S9,0) as S9,            
        ISNULL(t.S10,0) as S10,            
        ISNULL(t.MONTOFTO,0) as MONTOFTO,            
        ISNULL(t.SALDOFTO,0) as SALDOFTO,            
        t.MONTOTOTALPROYECTO,            
        t.MontoaPagar,            
        t.IDSOLICITUDPAGO            
    FROM @TablaResult  t         
END

GO
/****** Object:  StoredProcedure [dbo].[BUSQUEDA_DETALLE_AUTORIZACION_DE_PAGO_GRILLA_PDF]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

    
/********************************************************************************************                      
NOMBRE DEL PROCEDIMIENTO  : [BUSQUEDA_DETALLE_AUTORIZACION_DE_PAGO_GRILLA_PDF]                      
FECHA DE CREACIÓN         : 12-04-2019                    
USUARIO DE CREACIÓN       : cfajardo.                    
VERSIÓN       : 1.0                                                
                    
          
Fecha de modificación   :                  
Usuario de modificación :                 
Motivo de modificación  :       

Mirto Blanco
20190510


                              
OBJETIVO         : Obtiene el detalle de todos los Proyectos de la(s)                    
                              Autorizacion(es) de pago segun filtros de busqueda                    
                      
TABLAS          : SOLICITUD_AUTORIZACION                   
                      
                              
                      
QUE RETORNA        :  NUMEROSOLICITUDSOLICITUDAUTORIZACION    
  ,CODIGOPROYECTOSOLICITUDAUTORIZACION     
  ,NOMBREPROYECTOSOLICITUDAUTORIZACION     
  ,UBICACIONCOMUNASOLICITUDAUTORIZACION     
  ,S1SOLICITUDAUTORIZACION     
  ,S2SOLICITUDAUTORIZACION     
  ,S3SOLICITUDAUTORIZACION     
  ,S4SOLICITUDAUTORIZACION     
  ,S5SOLICITUDAUTORIZACION     
  ,S6SOLICITUDAUTORIZACION     
  ,S7SOLICITUDAUTORIZACION     
  ,S8SOLICITUDAUTORIZACION     
  ,S9SOLICITUDAUTORIZACION     
  ,S10SOLICITUDAUTORIZACION     
  ,FTOSOLICITUDAUTORIZACION     
  ,MONTOFTOTOTALSOLICITUDAUTORIZACION     
  ,MONTOATSOLICITUDAUTORIZACION     
  ,MONTOAPAGOSOLICITUDAUTORIZACION                  
                      
PARAMETROS         :@IDAUTORIZACION BigInt                    
                                
                               
PROYECTO         : SNAT SIMPLIFICADO                    
RESPONSABLE        : DINFO                      
                     BUSQUEDA_DETALLE_AUTORIZACION_DE_PAGO_GRILLA_PDF        
********************************************************************************************/                      
CREATE PROCEDURE [dbo].[BUSQUEDA_DETALLE_AUTORIZACION_DE_PAGO_GRILLA_PDF]               
@IDAUTORIZACION BigInt                
              
AS                    
BEGIN                    
               
 select      
   NUMEROSOLICITUDSOLICITUDAUTORIZACION    
  ,CODIGOPROYECTOSOLICITUDAUTORIZACION     
  ,NOMBREPROYECTOSOLICITUDAUTORIZACION     
  ,UBICACIONCOMUNASOLICITUDAUTORIZACION     
  ,REPLACE(ISNULL(S1SOLICITUDAUTORIZACION ,0),'.',',') AS S1SOLICITUDAUTORIZACION    
  ,REPLACE(ISNULL(S2SOLICITUDAUTORIZACION ,0),'.',',') AS S2SOLICITUDAUTORIZACION    
  ,REPLACE(ISNULL(S3SOLICITUDAUTORIZACION ,0),'.',',') AS S3SOLICITUDAUTORIZACION    
  ,REPLACE(ISNULL(S4SOLICITUDAUTORIZACION ,0),'.',',') AS S4SOLICITUDAUTORIZACION    
  ,REPLACE(ISNULL(S5SOLICITUDAUTORIZACION ,0),'.',',') AS S5SOLICITUDAUTORIZACION    
  ,REPLACE(ISNULL(S6SOLICITUDAUTORIZACION ,0),'.',',') AS S6SOLICITUDAUTORIZACION    
  ,REPLACE(ISNULL(S7SOLICITUDAUTORIZACION ,0),'.',',') AS S7SOLICITUDAUTORIZACION    
  ,REPLACE(ISNULL(S8SOLICITUDAUTORIZACION ,0),'.',',') AS S8SOLICITUDAUTORIZACION    
  ,REPLACE(ISNULL(S9SOLICITUDAUTORIZACION ,0),'.',',') AS S9SOLICITUDAUTORIZACION    
  ,REPLACE(ISNULL(S10SOLICITUDAUTORIZACION,0),'.',',') AS S10SOLICITUDAUTORIZACION    
  ,REPLACE(ISNULL(FTOSOLICITUDAUTORIZACION,0),'.',',') AS FTOSOLICITUDAUTORIZACION    
  ,REPLACE(ISNULL(MONTOFTOTOTALSOLICITUDAUTORIZACION,0 ),'.',',') AS MONTOFTOTOTALSOLICITUDAUTORIZACION    
  ,REPLACE(ISNULL(MONTOATSOLICITUDAUTORIZACION,0),'.',',') AS MONTOATSOLICITUDAUTORIZACION    
  ,REPLACE(ISNULL(MONTOAPAGOSOLICITUDAUTORIZACION,0),'.',',') AS MONTOAPAGOSOLICITUDAUTORIZACION         
       
       from SOLICITUD_AUTORIZACION where NUMEROAUTORIZACIONSOLICITUDAUTORIZACION = @IDAUTORIZACION    
         
            
END    
    


GO
/****** Object:  StoredProcedure [dbo].[BUSQUEDA_SUMA_SOLICITUD_AUTORIZACION_DE_PAGO_GRILLA_PDF]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/********************************************************************************************                    
NOMBRE DEL PROCEDIMIENTO  : [BUSQUEDA_SUMA_SOLICITUD_AUTORIZACION_DE_PAGO_GRILLA_PDF]                    
FECHA DE CREACIÓN         : 12-04-2019                  
USUARIO DE CREACIÓN       : cfajardo.                  
VERSIÓN       : 1.0                                              
                  
        
Fecha de modificación   :                
Usuario de modificación :               
Motivo de modificación  :     

Visado por DBA		: Iván Frade Cortés
Fecha				: 09052019
Observación			: ---         
                            
OBJETIVO         : Obtiene el detalle de todos los Proyectos de la(s)                  
                              Autorizacion(es) de pago segun filtros de busqueda                  
                    
TABLAS          : SOLICITUD_AUTORIZACION                 
                    
                            
                    
QUE RETORNA        :  NUMEROSOLICITUDSOLICITUDAUTORIZACION  
  ,CODIGOPROYECTOSOLICITUDAUTORIZACION   
  ,NOMBREPROYECTOSOLICITUDAUTORIZACION   
  ,UBICACIONCOMUNASOLICITUDAUTORIZACION   
  ,S1SOLICITUDAUTORIZACION   
  ,S2SOLICITUDAUTORIZACION   
  ,S3SOLICITUDAUTORIZACION   
  ,S4SOLICITUDAUTORIZACION   
  ,S5SOLICITUDAUTORIZACION   
  ,S6SOLICITUDAUTORIZACION   
  ,S7SOLICITUDAUTORIZACION   
  ,S8SOLICITUDAUTORIZACION   
  ,S9SOLICITUDAUTORIZACION   
  ,S10SOLICITUDAUTORIZACION   
  ,FTOSOLICITUDAUTORIZACION   
  ,MONTOFTOTOTALSOLICITUDAUTORIZACION   
  ,MONTOATSOLICITUDAUTORIZACION   
  ,MONTOAPAGOSOLICITUDAUTORIZACION                
                    
PARAMETROS         :@IDAUTORIZACION BigInt                  
                              
                             
PROYECTO         : SNAT SIMPLIFICADO                  
RESPONSABLE        : DINFO                    
                     BUSQUEDA_SUMA_SOLICITUD_AUTORIZACION_DE_PAGO_GRILLA_PDF      
********************************************************************************************/                    
CREATE PROCEDURE [dbo].[BUSQUEDA_SUMA_SOLICITUD_AUTORIZACION_DE_PAGO_GRILLA_PDF]             
@IDAUTORIZACION BigInt              
            
AS                  
BEGIN                  
             
 select    
    
   REPLACE(ISNULL(SUM(S1SOLICITUDAUTORIZACION ),0),'.',',') AS S1SOLICITUDAUTORIZACION  
  ,REPLACE(ISNULL(SUM(S2SOLICITUDAUTORIZACION ),0),'.',',') AS S2SOLICITUDAUTORIZACION  
  ,REPLACE(ISNULL(SUM(S3SOLICITUDAUTORIZACION ),0),'.',',') AS S3SOLICITUDAUTORIZACION  
  ,REPLACE(ISNULL(SUM(S4SOLICITUDAUTORIZACION ),0),'.',',')  AS S4SOLICITUDAUTORIZACION  
  ,REPLACE(ISNULL(SUM(S5SOLICITUDAUTORIZACION ),0),'.',',') AS S5SOLICITUDAUTORIZACION  
  ,REPLACE(ISNULL(SUM(S6SOLICITUDAUTORIZACION ),0),'.',',') AS S6SOLICITUDAUTORIZACION  
  ,REPLACE(ISNULL(SUM(S7SOLICITUDAUTORIZACION ),0),'.',',') AS S7SOLICITUDAUTORIZACION  
  ,REPLACE(ISNULL(SUM(S8SOLICITUDAUTORIZACION ),0),'.',',') AS S8SOLICITUDAUTORIZACION  
  ,REPLACE(ISNULL(SUM(S9SOLICITUDAUTORIZACION ),0),'.',',') AS S9SOLICITUDAUTORIZACION  
  ,REPLACE(ISNULL(SUM(S10SOLICITUDAUTORIZACION),0),'.',',') AS S10SOLICITUDAUTORIZACION  
  ,REPLACE(ISNULL(SUM(FTOSOLICITUDAUTORIZACION),0),'.',',') AS FTOSOLICITUDAUTORIZACION  
  ,REPLACE(ISNULL(SUM(MONTOFTOTOTALSOLICITUDAUTORIZACION),0),'.',',') AS MONTOFTOTOTALSOLICITUDAUTORIZACION  
  ,REPLACE(ISNULL(SUM(MONTOATSOLICITUDAUTORIZACION),0),'.',',') AS MONTOATSOLICITUDAUTORIZACION  
  ,REPLACE(ISNULL(SUM(MONTOAPAGOSOLICITUDAUTORIZACION),0),'.',',') AS MONTOAPAGOSOLICITUDAUTORIZACION  
     
       from SOLICITUD_AUTORIZACION where NUMEROAUTORIZACIONSOLICITUDAUTORIZACION = @IDAUTORIZACION  
          
          
END


GO
/****** Object:  StoredProcedure [dbo].[CABECERA_AUTORIZACION_PAGO]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/********************************************************************************************                  
NOMBRE DEL PROCEDIMIENTO  : [CABECERA_AUTORIZACION_PAGO]                  
FECHA DE CREACIÓN         : 23-04-2019               
USUARIO DE CREACIÓN       : cfajardo   
VERSIÓN                   : 1.0                                            
                
Fecha de modificación   :   
Usuario de modificación :   
Motivo de modificación  :   

Visado por DBA			: Iván Frade Cortés
Fecha					: 09052019
Observación				: ---

                          
OBJETIVO         : cabecera de la tabla autorizacion              
                  
TABLAS          : AUTORIZACION A                
         SOLICITUD_PAGO S                
         CARACTERISTICAS_ESPECIALES C                
         INFORMACION_PROYECTO IP                
         DIRECCION D                
         MAESTRO_PROGRAMA MP                
         MAESTRO_TIPOLOGIA MT                
         MAESTRO_CLASE MC                
         TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA SC                
         MAESTRO_ESTADO_SOLICITUD ES                
                  
                          
                  
QUE RETORNA        :  TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA   
                      SERVICIO_PARCIALIDAD SP                 
                      TIPOLOGIA_SERVICIO TS               
                      MAESTRO_SERVICIO MS        
             TIPO_AUTORIZACION TA   
                  
PARAMETROS         :@IDAUTORIZACION BigInt                
                            
                           
PROYECTO         : SNAT SIMPLIFICADO                
RESPONSABLE        : DINFO                  
                     CABECERA_AUTORIZACION_PAGO 30216  
********************************************************************************************/                  
create PROCEDURE [dbo].[CABECERA_AUTORIZACION_PAGO]           
@IDAUTORIZACION BigInt                
AS                
BEGIN      

DECLARE @IDMAESTROTIPOPROVEEDOR BIGINT  
DECLARE @0 DECIMAL

SET @0 = 0

declare @Solicitudes table(
	IDSOLICITUDPAGO BIGINT NULL,
	CODIGOPROYECTOINFORMACIONPROYECTO VARCHAR (255) NULL,
	NOMBREPROYECTOINFORMACIONPROYECTO VARCHAR (255) NULL,
	CODIGOPROVINCIADIRECCION INT NULL,
	CODIGOCOMUNADIRECCION INT NULL,
	NOMBRE_SERVICIO VARCHAR (100),
	CANTIDAD_PARCIALIDAD_POR_SERVICIO INT NULL,
	PAGO_TOTAL_POR_SERVICIO DECIMAL(18,3) NULL,
	MONTO_FTO_TOTAL_UF DECIMAL (18,3) NULL,
	MONTO_A_PAGO_FTO DECIMAL (18,3) NULL,
	PAGO_ARANCELES_UF DECIMAL (18,3) NULL,
	MONTO_A_PAGO_ARANCELES_UF DECIMAL (18,3) NULL,
	MONTO_AT_UF DECIMAL (18,3) NULL,
	MONTO_A_PAGO DECIMAL (18,3) NULL,
	IDMAESTROTIPOPROVEEDOR BIGINT
)

INSERT INTO @Solicitudes   
   SELECT 
      
		  PL.IDSOLICITUDPAGO,
		  IPP.CODIGOPROYECTOINFORMACIONPROYECTO,
		  IPP.NOMBREPROYECTOINFORMACIONPROYECTO,
		  D.CODIGOPROVINCIADIRECCION,
		  D.CODIGOCOMUNADIRECCION,
          MS.NOMBREABREVIADOMAESTROSERVICIO AS NOMBRE_SERVICIO, 
          COUNT(MS.NOMBREABREVIADOMAESTROSERVICIO) AS CANTIDAD_PARCIALIDAD_POR_SERVICIO, 
          --PL.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA ,
	 	  SUM(PL.MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA) AS PAGO_TOTAL_POR_SERVICIO,
		  ISNULL((SELECT SUM(PL2.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA)    FROM TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA PL2 INNER JOIN SOLICITUD_PAGO SS ON PL2.IDSOLICITUDPAGO = SS.IDSOLICITUDPAGO   INNER JOIN PROVEEDOR P ON SS.IDPROVEEDOR = P.IDPROVEEDOR INNER JOIN SERVICIO_PARCIALIDAD SP2   ON PL2.IDSERVICIOPARCIALIDAD = SP2.IDSERVICIOPARCIALIDAD INNER JOIN TIPOLOGIA_SERVICIO TS2 ON SP2.IDTIPOLOGIASERVICIO = TS2.IDTIPOLOGIASERVICIO INNER JOIN MAESTRO_SERVICIO MS2 ON TS2.IDMAESTROSERVICIO = MS2.IDMAESTROSERVICIO INNER JOIN TIPO_AUTORIZACION  TA2  ON PL2.IDSOLICITUDPAGO = TA2.IDSOLICITUDPAGO   WHERE  MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA > @0 AND PL2.IDSOLICITUDPAGO = PL.IDSOLICITUDPAGO  AND  NOMBREABREVIADOMAESTROSERVICIO = 'FTO' AND P.IDMAESTROTIPOPROVEEDOR = 6), @0) AS MONTO_FTO_TOTAL_UF,
		  ISNULL((SELECT SUM(PL2.MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA) FROM TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA PL2 INNER JOIN SOLICITUD_PAGO SS ON PL2.IDSOLICITUDPAGO = SS.IDSOLICITUDPAGO   INNER JOIN PROVEEDOR P ON SS.IDPROVEEDOR = P.IDPROVEEDOR INNER JOIN SERVICIO_PARCIALIDAD SP2   ON PL2.IDSERVICIOPARCIALIDAD = SP2.IDSERVICIOPARCIALIDAD INNER JOIN TIPOLOGIA_SERVICIO TS2  ON SP2.IDTIPOLOGIASERVICIO = TS2.IDTIPOLOGIASERVICIO INNER JOIN MAESTRO_SERVICIO MS2 ON TS2.IDMAESTROSERVICIO = MS2.IDMAESTROSERVICIO INNER JOIN TIPO_AUTORIZACION  TA2 ON PL2.IDSOLICITUDPAGO = TA2.IDSOLICITUDPAGO   WHERE  MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA > @0 AND PL2.IDSOLICITUDPAGO = PL.IDSOLICITUDPAGO  AND  NOMBREABREVIADOMAESTROSERVICIO = 'FTO' AND P.IDMAESTROTIPOPROVEEDOR = 6), @0) AS MONTO_A_PAGO_FTO,
		  ISNULL((SELECT SUM(PL2.MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA) FROM TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA PL2 INNER JOIN SOLICITUD_PAGO SS ON PL2.IDSOLICITUDPAGO = SS.IDSOLICITUDPAGO   INNER JOIN PROVEEDOR P ON SS.IDPROVEEDOR = P.IDPROVEEDOR INNER JOIN SERVICIO_PARCIALIDAD SP2   ON PL2.IDSERVICIOPARCIALIDAD = SP2.IDSERVICIOPARCIALIDAD INNER JOIN TIPOLOGIA_SERVICIO TS2  ON SP2.IDTIPOLOGIASERVICIO = TS2.IDTIPOLOGIASERVICIO INNER JOIN MAESTRO_SERVICIO MS2 ON TS2.IDMAESTROSERVICIO = MS2.IDMAESTROSERVICIO INNER JOIN TIPO_AUTORIZACION  TA2 ON PL2.IDSOLICITUDPAGO = TA2.IDSOLICITUDPAGO   WHERE  MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA > @0 AND PL2.IDSOLICITUDPAGO = PL.IDSOLICITUDPAGO  AND  NOMBREABREVIADOMAESTROSERVICIO = 'P.A.' AND P.IDMAESTROTIPOPROVEEDOR = 7),@0) AS PAGO_ARANCELES_UF,
		  ISNULL((SELECT SUM(PL2.MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA) FROM TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA PL2 INNER JOIN SOLICITUD_PAGO SS ON PL2.IDSOLICITUDPAGO = SS.IDSOLICITUDPAGO   INNER JOIN PROVEEDOR P ON SS.IDPROVEEDOR = P.IDPROVEEDOR INNER JOIN SERVICIO_PARCIALIDAD SP2   ON PL2.IDSERVICIOPARCIALIDAD = SP2.IDSERVICIOPARCIALIDAD INNER JOIN TIPOLOGIA_SERVICIO TS2  ON SP2.IDTIPOLOGIASERVICIO = TS2.IDTIPOLOGIASERVICIO INNER JOIN MAESTRO_SERVICIO MS2 ON TS2.IDMAESTROSERVICIO = MS2.IDMAESTROSERVICIO INNER JOIN TIPO_AUTORIZACION  TA2 ON PL2.IDSOLICITUDPAGO = TA2.IDSOLICITUDPAGO   WHERE  MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA > @0 AND PL2.IDSOLICITUDPAGO = PL.IDSOLICITUDPAGO  AND  NOMBREABREVIADOMAESTROSERVICIO = 'P.A.' AND P.IDMAESTROTIPOPROVEEDOR = 7),@0) AS MONTO_A_PAGO_ARANCELES_UF,
		  ISNULL((SELECT SUM(PL2.MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA)    FROM TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA PL2 INNER JOIN SOLICITUD_PAGO SS ON PL2.IDSOLICITUDPAGO = SS.IDSOLICITUDPAGO   INNER JOIN PROVEEDOR P ON SS.IDPROVEEDOR = P.IDPROVEEDOR INNER JOIN SERVICIO_PARCIALIDAD SP2   ON PL2.IDSERVICIOPARCIALIDAD = SP2.IDSERVICIOPARCIALIDAD INNER JOIN TIPOLOGIA_SERVICIO TS2  ON SP2.IDTIPOLOGIASERVICIO = TS2.IDTIPOLOGIASERVICIO INNER JOIN MAESTRO_SERVICIO MS2 ON TS2.IDMAESTROSERVICIO = MS2.IDMAESTROSERVICIO INNER JOIN TIPO_AUTORIZACION  TA2 ON PL2.IDSOLICITUDPAGO = TA2.IDSOLICITUDPAGO   WHERE  PL2.IDSOLICITUDPAGO = PL.IDSOLICITUDPAGO  AND MS2.NOMBREABREVIADOMAESTROSERVICIO  NOT IN ('FTO') ),@0) AS MONTO_AT_UF, 
		  ISNULL((SELECT SUM(PL2.MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA) FROM TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA PL2 INNER JOIN SOLICITUD_PAGO SS ON PL2.IDSOLICITUDPAGO = SS.IDSOLICITUDPAGO   INNER JOIN PROVEEDOR P ON SS.IDPROVEEDOR = P.IDPROVEEDOR INNER JOIN SERVICIO_PARCIALIDAD SP2   ON PL2.IDSERVICIOPARCIALIDAD = SP2.IDSERVICIOPARCIALIDAD INNER JOIN TIPOLOGIA_SERVICIO TS2  ON SP2.IDTIPOLOGIASERVICIO = TS2.IDTIPOLOGIASERVICIO INNER JOIN MAESTRO_SERVICIO MS2 ON TS2.IDMAESTROSERVICIO = MS2.IDMAESTROSERVICIO INNER JOIN TIPO_AUTORIZACION  TA2 ON PL2.IDSOLICITUDPAGO = TA2.IDSOLICITUDPAGO   WHERE  MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA > @0 AND PL2.IDSOLICITUDPAGO = PL.IDSOLICITUDPAGO  AND MS2.NOMBREABREVIADOMAESTROSERVICIO  NOT IN ('FTO') AND ((P.IDMAESTROTIPOPROVEEDOR IN(1,2,3,4,5) AND MS2.NOMBREABREVIADOMAESTROSERVICIO NOT IN('P.A.'))OR(P.IDMAESTROTIPOPROVEEDOR = 7 AND MS2.NOMBREABREVIADOMAESTROSERVICIO IN('P.A.')))),@0) AS MONTO_A_PAGO,
		  ( SELECT TOP 1 P.IDMAESTROTIPOPROVEEDOR FROM TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA PL2 INNER JOIN SOLICITUD_PAGO SS ON PL2.IDSOLICITUDPAGO = SS.IDSOLICITUDPAGO   INNER JOIN PROVEEDOR P ON SS.IDPROVEEDOR = P.IDPROVEEDOR WHERE PL2.IDSOLICITUDPAGO = PL.IDSOLICITUDPAGO) AS IDMAESTROTIPOPROVEEDOR
		  																																																																																																																																																		 
		  FROM TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA PL          
		    
		       INNER JOIN CARACTERISTICAS_ESPECIALES CE ON PL.IDCARACTERISTICASESPECIALES = CE.IDCARACTERISTICASESPECIALES
			   INNER JOIN INFORMACION_PROYECTO IPP ON CE.IDINFORMACIONPROYECTO = IPP.IDINFORMACIONPROYECTO
			   INNER JOIN DIRECCION D ON IPP.IDDIRECCION = D.IDDIRECCION      
               INNER JOIN SERVICIO_PARCIALIDAD SP   ON PL.IDSERVICIOPARCIALIDAD = SP.IDSERVICIOPARCIALIDAD                
               INNER JOIN TIPOLOGIA_SERVICIO TS     ON SP.IDTIPOLOGIASERVICIO = TS.IDTIPOLOGIASERVICIO                
               INNER JOIN MAESTRO_SERVICIO MS      ON TS.IDMAESTROSERVICIO = MS.IDMAESTROSERVICIO       
			   INNER JOIN TIPO_AUTORIZACION  TA ON PL.IDSOLICITUDPAGO = TA.IDSOLICITUDPAGO  
			   WHERE  MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA > @0 AND TA.IDAUTORIZACION = @IDAUTORIZACION  

   GROUP BY MS.NOMBREABREVIADOMAESTROSERVICIO , PL.IDSOLICITUDPAGO,  IPP.CODIGOPROYECTOINFORMACIONPROYECTO,  IPP.NOMBREPROYECTOINFORMACIONPROYECTO, D.CODIGOPROVINCIADIRECCION,  D.CODIGOCOMUNADIRECCION
   ORDER BY PL.IDSOLICITUDPAGO,MS.NOMBREABREVIADOMAESTROSERVICIO 
   
   SET @IDMAESTROTIPOPROVEEDOR = (SELECT TOP 1 IDMAESTROTIPOPROVEEDOR FROM @Solicitudes)
 

   IF(@IDMAESTROTIPOPROVEEDOR IN(1,2,3,4,5))
   BEGIN
   SELECT 
    IDSOLICITUDPAGO,
	CODIGOPROYECTOINFORMACIONPROYECTO,
	NOMBREPROYECTOINFORMACIONPROYECTO,
	CODIGOPROVINCIADIRECCION,
	CODIGOCOMUNADIRECCION,
	NOMBRE_SERVICIO ,
	CANTIDAD_PARCIALIDAD_POR_SERVICIO ,
	PAGO_TOTAL_POR_SERVICIO ,
	@0 as MONTO_FTO_TOTAL_UF ,
	@0 as MONTO_A_PAGO_FTO ,
	@0 as PAGO_ARANCELES_UF ,
	@0 as MONTO_A_PAGO_ARANCELES_UF,
	MONTO_AT_UF ,
	MONTO_A_PAGO 
	FROM @Solicitudes WHERE NOMBRE_SERVICIO NOT IN ('FTO','P.A.')
	ORDER BY IDSOLICITUDPAGO,NOMBRE_SERVICIO 

   END
   ELSE IF(@IDMAESTROTIPOPROVEEDOR IN(6))
      BEGIN
   SELECT 
    IDSOLICITUDPAGO,
	CODIGOPROYECTOINFORMACIONPROYECTO,
	NOMBREPROYECTOINFORMACIONPROYECTO,
	CODIGOPROVINCIADIRECCION,
	CODIGOCOMUNADIRECCION,
	NOMBRE_SERVICIO ,
	CANTIDAD_PARCIALIDAD_POR_SERVICIO ,
	PAGO_TOTAL_POR_SERVICIO ,
	MONTO_FTO_TOTAL_UF ,
	MONTO_A_PAGO_FTO ,
	@0 as PAGO_ARANCELES_UF ,
	@0 as MONTO_A_PAGO_ARANCELES_UF,
	@0 as MONTO_AT_UF ,
	@0 as MONTO_A_PAGO 
	FROM @Solicitudes WHERE NOMBRE_SERVICIO IN ('FTO')
	ORDER BY IDSOLICITUDPAGO,NOMBRE_SERVICIO
   END   
   ELSE IF(@IDMAESTROTIPOPROVEEDOR IN(7))
      BEGIN
		SELECT 
		       IDSOLICITUDPAGO,
	           CODIGOPROYECTOINFORMACIONPROYECTO,
	           NOMBREPROYECTOINFORMACIONPROYECTO,
	           CODIGOPROVINCIADIRECCION,
	           CODIGOCOMUNADIRECCION,
	           NOMBRE_SERVICIO ,
	           CANTIDAD_PARCIALIDAD_POR_SERVICIO ,
	           PAGO_TOTAL_POR_SERVICIO,
	           @0 as MONTO_FTO_TOTAL_UF,
	           @0 as MONTO_A_PAGO_FTO,
	           @0 as PAGO_ARANCELES_UF,
	           @0 as MONTO_A_PAGO_ARANCELES_UF,
	           MONTO_AT_UF ,
	           MONTO_A_PAGO 
	FROM @Solicitudes WHERE NOMBRE_SERVICIO IN ('P.A.')
	ORDER BY IDSOLICITUDPAGO,NOMBRE_SERVICIO

		END  
	


END  

GO
/****** Object:  StoredProcedure [dbo].[CONSULTA_ACTIVIDADES_CONTRATO]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/********************************************************************************************          
* Nombre del procedimiento      : CONSULTA_ACTIVIDADES_CONTRATO  
* Fecha de creación             : 23/11/2018          
* Usuario de creación           : Anticipa         
* Versión.                      : V.0.0.0.1          

Fecha de modificación   : 18-02-2019  
Usuario de modificación : cfajardo
Motivo de modificación  : regularización de procedimientos almacenados entre ambientes.
  
* Visado por DBA                :  Mirto Blanco 
* Fecha Aprobación DBA          :  20190220 
  
* Parametros     : @IDCONTRATO    
  
          
* Objetivo                      : Procedimiento que lista las actividades asociadas al contrato          
* Prueba      : exec CONSULTA_ACTIVIDADES_CONTRATO 11  
**********************************************************************************************/       
CREATE PROCEDURE [dbo].[CONSULTA_ACTIVIDADES_CONTRATO]          
@IDCONTRATO BIGINT  
  
AS  
  
BEGIN  
  SELECT  MA.NOMBREABREVIADOMAESTROSERVICIO,  
    MA.NOMBREMAESTROSERVICIO,  
    MA.IDMAESTROSERVICIO,  
    SO.IDCONTRATOATP,  
    MA.ESTADOMAESTROSERVICIO,  
    TA.MONTOPAGOTIPOACTIVIDADMONTO  
  FROM   
   TIPO_ACTIVIDAD_MONTO TA INNER JOIN  
   SOLICITUD_PAGO_ATP SO ON SO.IDSOLICITUDATP = TA.IDSOLICITUDATP INNER JOIN  
   MAESTRO_SERVICIO MA ON MA.IDMAESTROSERVICIO = TA.IDMAESTROSERVICIO  
  WHERE SO.IDCONTRATOATP=@IDCONTRATO  
END  
  

GO
/****** Object:  StoredProcedure [dbo].[CONSULTA_CONTRATO]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/********************************************************************************************          
* Nombre del procedimiento      : CONSULTA_CONTRATO  
* Fecha de creación             : 28/12/2018          
* Usuario de creación           : Anticipa         
* Versión.                      : V.0.0.0.1          
  
Fecha de modificación   : 18-02-2019  
Usuario de modificación : cfajardo
Motivo de modificación  : regularización de procedimientos almacenados entre ambientes.

* Visado por DBA                :  Mirto Blanco 
* Fecha Aprobación DBA          :  20190220 
  
* Parametros     : @IDCONTRATO              
          
* Objetivo                      : Parocedimiento que lista los contratos        
* Prueba      : exec CONSULTA_CONTRATO 95  
**********************************************************************************************/  
CREATE PROCEDURE [dbo].[CONSULTA_CONTRATO]          
@IDCONTRATO INT  
  
AS  
BEGIN  
  
 SELECT CO.IDCONTRATOATP,  
 PR.RUTPROVEEDOR,  
 PR.DVPROVEDIGITOVERIFICADORPROVEEDOR,  
 PR.NOMBREPROVEEDOR,  
 MAP.NOMBREMAESTROTIPOPROVEEDOR,  
 CO.AGNOPRESUPUESTOCONTRATOATP,  
 TE.CODIGOREGIONTERRITORIOCONTRATO,  
 TE.CODIGOPROVINCIATERRITORIOCONTRATO,  
 TE.CODIGOCOMUNATERRITORIOCONTRATO,  
 CO.NOMBREARCHIVOCONTRATOATP,  
 PRO.NOMBREPROPIEDADTERRENO,  
 CO.NUMERORESOLUCIONCONTRATOATP,  
 CO.FECHARESOLUCIONCONTRATOATP,  
 CO.FECHAINICIOCONTRATOATP,  
 CO.PLAZOEJECUCIONCONTRATOATP,  
 MA.NOMBREMAESTROTIPOSERVICIOIGT,  
 CO.PRODUCTOCONTRATOATP,  
 CO.DESCRIPCIONPRODUCTOCONTRATOATP,  
 CO.MONTOCONTRATOCONTRATOATP,  
 CO.OBSERVACIONCONTRATOATP   
 FROM CONTRATO_ATP CO INNER JOIN  
 TERRITORIO_CONTRATO TE ON CO.IDTERRITORIOCONTRATO = TE.IDTERRITORIOCONTRATO INNER JOIN   
 PROPIEDAD_TERRENO PRO ON CO.IDPROPIEDADTERRENO = PRO.IDPROPIEDADTERRENO INNER JOIN  
 MAESTRO_TIPO_PROVEEDOR MAP ON CO.IDMAESTROTIPOPROVEEDOR = MAP.IDMAESTROTIPOPROVEEDOR INNER JOIN  
 PROVEEDOR PR ON CO.IDPROVEEDOR = PR.IDPROVEEDOR INNER JOIN  
 MAESTRO_TIPO_SERVICIO_IGT MA ON CO.IDMAESTROTIPOSERVICIOIGT = MA.IDMAESTROTIPOSERVICIOIGT  
 WHERE CO.IDCONTRATOATP = @IDCONTRATO  
  
END

GO
/****** Object:  StoredProcedure [dbo].[CONSULTA_LISTADO_CONTRATO]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
/********************************************************************************************                
* Nombre del procedimiento      : CONSULTA_LISTADO_CONTRATO        
* Fecha de creación             : 13/12/2018                
* Usuario de creación           : Anticipa               
* Versión.                      : V.0.0.0.1                
  
Fecha de modificación   : 18-02-2019    
Usuario de modificación : cfajardo  
Motivo de modificación  : regularización de procedimientos almacenados entre ambientes.    
                                
Fecha de Modificacion : 09/01/2019      
Usuario de creación   : cfajardo      
Motivo de modificacion: Se agrega columna estado contrato      
        
Visado por DBA              :  Iván Frade Cortés                                     
Fecha Aprobación DBA        :  20190407                                      
Comentarios DBA             :     ---                                  
        
* Parametros     : @ACCION        
          @ANNOPRESUPUESTO        
          @CODIGOREGION        
          @TIPOSERVICIO        
                
* Objetivo                      : Procedimiento que lista las solicitudes             
* Prueba      : exec CONSULTA_LISTADO_CONTRATO 1,2018,13,2        
**********************************************************************************************/             
CREATE PROCEDURE [dbo].[CONSULTA_LISTADO_CONTRATO]            
@ACCION INT,              
@ANNOPRESUPUESTO INT,        
@CODIGOREGION INT,        
@TIPOSERVICIO BIGINT        
        
AS        
        
DECLARE @CONSULTATODOS INT        
DECLARE @CONSULTASERVICIO INT        
DECLARE @CONSULTAREGION INT        
DECLARE @CONSULTAANNO INT        
        
SET @CONSULTATODOS = 1        
SET @CONSULTASERVICIO = 2        
SET @CONSULTAREGION = 3        
SET @CONSULTAANNO = 4        
        
IF @ACCION = @CONSULTATODOS        
BEGIN        
  SELECT CO.IDCONTRATOATP,        
    TE.CODIGOREGIONTERRITORIOCONTRATO,        
    PRO.RUTPROVEEDOR,         
    PRO.DVPROVEDIGITOVERIFICADORPROVEEDOR,        
    PRO.NOMBREPROVEEDOR,        
    CO.MONTOCONTRATOCONTRATOATP,        
    MA.TIPOSERVICIOMAESTROTIPOSERVICIOIGT,        
    CO.FECHACREACIONCONTRATOATP,        
    CONTRATOS = (SELECT COUNT(IDCONTRATOATP)        
       FROM SOLICITUD_PAGO_ATP        
       WHERE IDCONTRATOATP = CO.IDCONTRATOATP),        
    CO.AGNOPRESUPUESTOCONTRATOATP,        
    CO.NUMERORESOLUCIONCONTRATOATP,      
 (select NOMBREMAESTROESTADOCONTRATO from MAESTRO_ESTADO_CONTRATO where IDMAESTROESTADOCONTRATO = CO.IDMAESTROESTADOCONTRATO) as NOMBREMAESTROESTADOCONTRATO      
  FROM CONTRATO_ATP CO INNER JOIN        
    TERRITORIO_CONTRATO TE ON CO.IDTERRITORIOCONTRATO = TE.IDTERRITORIOCONTRATO INNER JOIN        
    PROVEEDOR PRO ON CO.IDPROVEEDOR = PRO.IDPROVEEDOR INNER JOIN        
    MAESTRO_TIPO_SERVICIO_IGT MA ON CO.IDMAESTROTIPOSERVICIOIGT = MA.IDMAESTROTIPOSERVICIOIGT        
  WHERE CO.AGNOPRESUPUESTOCONTRATOATP = @ANNOPRESUPUESTO AND        
    TE.CODIGOREGIONTERRITORIOCONTRATO = @CODIGOREGION AND        
    MA.IDMAESTROTIPOSERVICIOIGT = @TIPOSERVICIO        
END        
        
IF @ACCION = @CONSULTASERVICIO        
BEGIN        
  SELECT CO.IDCONTRATOATP,        
    TE.CODIGOREGIONTERRITORIOCONTRATO,        
    PRO.RUTPROVEEDOR,         
    PRO.DVPROVEDIGITOVERIFICADORPROVEEDOR,        
    PRO.NOMBREPROVEEDOR,        
    CO.MONTOCONTRATOCONTRATOATP,        
    MA.TIPOSERVICIOMAESTROTIPOSERVICIOIGT,        
    CO.FECHACREACIONCONTRATOATP,        
    CONTRATOS = (SELECT COUNT(IDCONTRATOATP)        
       FROM SOLICITUD_PAGO_ATP        
       WHERE IDCONTRATOATP = CO.IDCONTRATOATP),        
    CO.AGNOPRESUPUESTOCONTRATOATP,        
    CO.NUMERORESOLUCIONCONTRATOATP ,      
 (select NOMBREMAESTROESTADOCONTRATO from MAESTRO_ESTADO_CONTRATO where IDMAESTROESTADOCONTRATO = CO.IDMAESTROESTADOCONTRATO)  as NOMBREMAESTROESTADOCONTRATO      
  FROM CONTRATO_ATP CO INNER JOIN        
    TERRITORIO_CONTRATO TE ON CO.IDTERRITORIOCONTRATO = TE.IDTERRITORIOCONTRATO INNER JOIN            PROVEEDOR PRO ON CO.IDPROVEEDOR = PRO.IDPROVEEDOR INNER JOIN        
    MAESTRO_TIPO_SERVICIO_IGT MA ON CO.IDMAESTROTIPOSERVICIOIGT = MA.IDMAESTROTIPOSERVICIOIGT        
  WHERE CO.AGNOPRESUPUESTOCONTRATOATP = @ANNOPRESUPUESTO AND                
    MA.IDMAESTROTIPOSERVICIOIGT = @TIPOSERVICIO        
END        
        
IF @ACCION = @CONSULTAREGION        
BEGIN        
  SELECT CO.IDCONTRATOATP,        
    TE.CODIGOREGIONTERRITORIOCONTRATO,        
    PRO.RUTPROVEEDOR,         
    PRO.DVPROVEDIGITOVERIFICADORPROVEEDOR,        
    PRO.NOMBREPROVEEDOR,        
    CO.MONTOCONTRATOCONTRATOATP,        
    MA.TIPOSERVICIOMAESTROTIPOSERVICIOIGT,        
    CO.FECHACREACIONCONTRATOATP,        
    CONTRATOS = (SELECT COUNT(IDCONTRATOATP)        
       FROM SOLICITUD_PAGO_ATP        
       WHERE IDCONTRATOATP = CO.IDCONTRATOATP),        
    CO.AGNOPRESUPUESTOCONTRATOATP,        
    CO.NUMERORESOLUCIONCONTRATOATP,      
 (select NOMBREMAESTROESTADOCONTRATO from MAESTRO_ESTADO_CONTRATO where IDMAESTROESTADOCONTRATO = CO.IDMAESTROESTADOCONTRATO)  as NOMBREMAESTROESTADOCONTRATO      
  FROM CONTRATO_ATP CO INNER JOIN        
    TERRITORIO_CONTRATO TE ON CO.IDTERRITORIOCONTRATO = TE.IDTERRITORIOCONTRATO INNER JOIN        
    PROVEEDOR PRO ON CO.IDPROVEEDOR = PRO.IDPROVEEDOR INNER JOIN        
    MAESTRO_TIPO_SERVICIO_IGT MA ON CO.IDMAESTROTIPOSERVICIOIGT = MA.IDMAESTROTIPOSERVICIOIGT        
  WHERE CO.AGNOPRESUPUESTOCONTRATOATP = @ANNOPRESUPUESTO AND                
    TE.CODIGOREGIONTERRITORIOCONTRATO = @CODIGOREGION        
END        
IF @ACCION = @CONSULTAANNO        
BEGIN        
  SELECT CO.IDCONTRATOATP,        
    TE.CODIGOREGIONTERRITORIOCONTRATO,        
    PRO.RUTPROVEEDOR,         
    PRO.DVPROVEDIGITOVERIFICADORPROVEEDOR,        
    PRO.NOMBREPROVEEDOR,        
    CO.MONTOCONTRATOCONTRATOATP,        
    MA.TIPOSERVICIOMAESTROTIPOSERVICIOIGT,        
    CO.FECHACREACIONCONTRATOATP,        
    CONTRATOS = (SELECT COUNT(IDCONTRATOATP)        
       FROM SOLICITUD_PAGO_ATP        
       WHERE IDCONTRATOATP = CO.IDCONTRATOATP),        
    CO.AGNOPRESUPUESTOCONTRATOATP,        
    CO.NUMERORESOLUCIONCONTRATOATP ,      
 (select NOMBREMAESTROESTADOCONTRATO from MAESTRO_ESTADO_CONTRATO where IDMAESTROESTADOCONTRATO = CO.IDMAESTROESTADOCONTRATO)  as NOMBREMAESTROESTADOCONTRATO      
  FROM CONTRATO_ATP CO INNER JOIN        
    TERRITORIO_CONTRATO TE ON CO.IDTERRITORIOCONTRATO = TE.IDTERRITORIOCONTRATO INNER JOIN        
    PROVEEDOR PRO ON CO.IDPROVEEDOR = PRO.IDPROVEEDOR INNER JOIN        
    MAESTRO_TIPO_SERVICIO_IGT MA ON CO.IDMAESTROTIPOSERVICIOIGT = MA.IDMAESTROTIPOSERVICIOIGT        
  WHERE CO.AGNOPRESUPUESTOCONTRATOATP = @ANNOPRESUPUESTO         
END   

GO
/****** Object:  StoredProcedure [dbo].[CONSULTA_LISTADO_SOLICITUD]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/********************************************************************************************                  
* Nombre del procedimiento      : CONSULTA_LISTADO_SOLICITUD          
* Fecha de creación             : 11/12/2018                  
* Usuario de creación           : Anticipa                 
* Versión.                      : V.0.0.0.1                  
          
Fecha de Modificacion : 09/01/2019          
Usuario de creación   : cfajardo          
Motivo de modificacion: Se agrega distinct        

Fecha de Modificacion : 09/01/2019          
Usuario de creación   : cfajardo          
Motivo de modificacion: Se agrega UPDATE al estado de solicitudes con tipo pago historicas
        
        
* Visado por DBA                :   Mirto Blanco
* Fecha Aprobación DBA          :   20190906    
          
* Parametros     : @NUMERORESOLUCION          
          @CODIGOREGION          
          @ANNOPRESUPUESTO          
                  
* Objetivo                      : Procedimiento que lista las solicitudes               
* Prueba      : exec CONSULTA_LISTADO_SOLICITUD 1, 721,13,2018          
**********************************************************************************************/               
CREATE PROCEDURE [dbo].[CONSULTA_LISTADO_SOLICITUD]                  
@ACCION INT,          
@NUMERORESOLUCION BIGINT,          
@CODIGOREGION INT,          
@ANNOPRESUPUESTO INT          
AS          
          
declare @1 INT          
declare @6 INT          
declare @3 INT          
DECLARE @CONSULTATODOS INT          
DECLARE @CONSULTARESOLUCION INT          
DECLARE @CONSULTAREGION INT          
DECLARE @CONSULTAANNO INT          
          
SET @CONSULTATODOS = 1          
SET @CONSULTARESOLUCION = 2          
SET @CONSULTAREGION = 3          
SET @CONSULTAANNO = 4 
SET @1 = 1
SET @6 = 6
SET @3 = 3



UPDATE SOLICITUD_PAGO_ATP
SET IDMAESTROESTADOSOLICITUD  = @3
WHERE IDMAESTROESTADOSOLICITUD = @6 AND IDMAESTROTIPOPAGO = @1
          
IF @ACCION = @CONSULTATODOS          
BEGIN          
   SELECT distinct SO.IDSOLICITUDATP,          
       CO.NUMERORESOLUCIONCONTRATOATP,          
       SO.CODIGOREGIONSOLICITUDPAGOATP,          
       PRO.RUTPROVEEDOR,          
       PRO.DVPROVEDIGITOVERIFICADORPROVEEDOR,          
       PRO.NOMBREPROVEEDOR,          
       SO.MONTOTOTALAPAGARSOLICITUDPAGOATP,          
       TI.TIPOSERVICIOTIPOACTIVIDADMONTO,          
       MA.NOMBREMAESTROESTADOSOLICITUD,          
       SO.FECHACREACIONSOLICITUDPAGOATP      
    FROM SOLICITUD_PAGO_ATP SO INNER JOIN           
     CONTRATO_ATP CO ON SO.IDCONTRATOATP = CO.IDCONTRATOATP INNER JOIN          
     PROVEEDOR PRO ON PRO.IDPROVEEDOR = CO.IDPROVEEDOR INNER JOIN          
     TIPO_ACTIVIDAD_MONTO TI ON TI.IDSOLICITUDATP = SO.IDSOLICITUDATP INNER JOIN          
     MAESTRO_ESTADO_SOLICITUD MA ON MA.IDMAESTROESTADOSOLICITUD = SO.IDMAESTROESTADOSOLICITUD            
    WHERE CO.NUMERORESOLUCIONCONTRATOATP = @NUMERORESOLUCION AND          
      SO.CODIGOREGIONSOLICITUDPAGOATP = @CODIGOREGION AND          
      SO.AGNOPRESUPUESTOSOLICITUDPAGOATP = @ANNOPRESUPUESTO          
END          
IF @ACCION = @CONSULTAANNO          
BEGIN          
   SELECT  distinct SO.IDSOLICITUDATP,          
       CO.NUMERORESOLUCIONCONTRATOATP,          
       SO.CODIGOREGIONSOLICITUDPAGOATP,          
       PRO.RUTPROVEEDOR,          
       PRO.DVPROVEDIGITOVERIFICADORPROVEEDOR,          
       PRO.NOMBREPROVEEDOR,          
       SO.MONTOTOTALAPAGARSOLICITUDPAGOATP,          
       TI.TIPOSERVICIOTIPOACTIVIDADMONTO,          
       MA.NOMBREMAESTROESTADOSOLICITUD,          
       SO.FECHACREACIONSOLICITUDPAGOATP      
    FROM SOLICITUD_PAGO_ATP SO INNER JOIN           
     CONTRATO_ATP CO ON SO.IDCONTRATOATP = CO.IDCONTRATOATP INNER JOIN          
     PROVEEDOR PRO ON PRO.IDPROVEEDOR = CO.IDPROVEEDOR INNER JOIN          
     TIPO_ACTIVIDAD_MONTO TI ON TI.IDSOLICITUDATP = SO.IDSOLICITUDATP INNER JOIN          
     MAESTRO_ESTADO_SOLICITUD MA ON MA.IDMAESTROESTADOSOLICITUD = SO.IDMAESTROESTADOSOLICITUD            
    WHERE SO.AGNOPRESUPUESTOSOLICITUDPAGOATP = @ANNOPRESUPUESTO          
END          
IF @ACCION = @CONSULTARESOLUCION          
BEGIN          
   SELECT  distinct SO.IDSOLICITUDATP,          
       CO.NUMERORESOLUCIONCONTRATOATP,          
       SO.CODIGOREGIONSOLICITUDPAGOATP,          
       PRO.RUTPROVEEDOR,          
       PRO.DVPROVEDIGITOVERIFICADORPROVEEDOR,          
       PRO.NOMBREPROVEEDOR,          
       SO.MONTOTOTALAPAGARSOLICITUDPAGOATP,          
       TI.TIPOSERVICIOTIPOACTIVIDADMONTO,          
       MA.NOMBREMAESTROESTADOSOLICITUD,          
 SO.FECHACREACIONSOLICITUDPAGOATP      
    FROM SOLICITUD_PAGO_ATP SO INNER JOIN           
     CONTRATO_ATP CO ON SO.IDCONTRATOATP = CO.IDCONTRATOATP INNER JOIN          
     PROVEEDOR PRO ON PRO.IDPROVEEDOR = CO.IDPROVEEDOR INNER JOIN          
     TIPO_ACTIVIDAD_MONTO TI ON TI.IDSOLICITUDATP = SO.IDSOLICITUDATP INNER JOIN          
     MAESTRO_ESTADO_SOLICITUD MA ON MA.IDMAESTROESTADOSOLICITUD = SO.IDMAESTROESTADOSOLICITUD            
    WHERE SO.AGNOPRESUPUESTOSOLICITUDPAGOATP = @ANNOPRESUPUESTO AND          
       CO.NUMERORESOLUCIONCONTRATOATP = @NUMERORESOLUCION                 
END          
IF @ACCION = @CONSULTAREGION          
BEGIN          
   SELECT  distinct SO.IDSOLICITUDATP,          
       CO.NUMERORESOLUCIONCONTRATOATP,          
       SO.CODIGOREGIONSOLICITUDPAGOATP,          
       PRO.RUTPROVEEDOR,          
       PRO.DVPROVEDIGITOVERIFICADORPROVEEDOR,          
       PRO.NOMBREPROVEEDOR,          
       SO.MONTOTOTALAPAGARSOLICITUDPAGOATP,          
       TI.TIPOSERVICIOTIPOACTIVIDADMONTO,          
       MA.NOMBREMAESTROESTADOSOLICITUD,          
       SO.FECHACREACIONSOLICITUDPAGOATP       
    FROM SOLICITUD_PAGO_ATP SO INNER JOIN           
      CONTRATO_ATP CO ON SO.IDCONTRATOATP = CO.IDCONTRATOATP INNER JOIN          
      PROVEEDOR PRO ON PRO.IDPROVEEDOR = CO.IDPROVEEDOR INNER JOIN          
      TIPO_ACTIVIDAD_MONTO TI ON TI.IDSOLICITUDATP = SO.IDSOLICITUDATP INNER JOIN          
      MAESTRO_ESTADO_SOLICITUD MA ON MA.IDMAESTROESTADOSOLICITUD = SO.IDMAESTROESTADOSOLICITUD            
    WHERE SO.CODIGOREGIONSOLICITUDPAGOATP = @CODIGOREGION AND          
       SO.AGNOPRESUPUESTOSOLICITUDPAGOATP = @ANNOPRESUPUESTO          
END      
      
GO
/****** Object:  StoredProcedure [dbo].[CONSULTA_PRESUPUESTO_REGIONAL_IGTD]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************            
NOMBRE DEL PROCEDIMIENTO  : CONSULTA_PRESUPUESTO_REGIONAL_IGTD          
FECHA DE CREACIÓN        : 04/09/2018          
USUARIO DE CREACIÓN       : Fernando Quezada - Anticipa.          
VERSIÓN               : 1.0                                      
          
      
* Fecha de modificación         :  18-01-2019      
* Usuarios de modificación      :  cfajardo      
* Motivo de modificación        :  condicion sobre numero de presupuesto mayor 0      
      
      
* Visado por DBA                :   Mirto Blanco
* Fecha Aprobación DBA          :   20190906    
                    
OBJETIVO         : Procedimiento que lista presupuestos regionales            
            
TABLAS          : RESOLUCION_PRESUPUESTARIA           
                     PRESUPUESTO_REGIONAL           
                     MAESTRO_TIPO_RESOLUCION_PRESUPUESTARIA           
                         
            
                    
            
QUE RETORNA        : RP.NUMERORESOLUCIONPRESUPUESTARIA,          
                     RP.ANORESOLUCIONPRESUPUESTARIA,          
                     PR.CODIGOREGIONPRESUPUESTOREGIONAL,          
                     PR.MONTOPRESUPUESTOREGIONAL,          
                     PR.FECHAPRESUPUESTOREGIONAL,          
                     MP.VIGENCIAMAESTROTIPORESOLUCION,          
                     RP.NOMBREARCHIVOPRESUPUESTO          
            
PARAMETROS         : @ACCION            
         @CONSULTA_PRESUPUESTO_NUMERO           
         @CONSULTA_PRESUPUESTO_ANNO          
         @CONSULTA_PRESUPUESTO_REGION          
                      
PRUEBA      : exec CONSULTA_PRESUPUESTO_REGIONAL_IGTD 5, 83, 2018, 0                      
          
                     
PROYECTO         : SNAT SIMPLIFICADO          
RESPONSABLE        : DINFO            
                 CONSULTA_PRESUPUESTO_REGIONAL_IGTD 7,0,2019,0    
********************************************************************************************/              
CREATE PROCEDURE [dbo].[CONSULTA_PRESUPUESTO_REGIONAL_IGTD] 
@ACCION INT,            
@CONSULTA_PRESUPUESTO_NUMERO INT=NULL,          
@CONSULTA_PRESUPUESTO_ANNO INT=NULL,          
@CONSULTA_PRESUPUESTO_REGION INT=NULL          
          
AS          
DECLARE @VARCERO INT, @VARUNO INT, @VARDOS INT, @VARTRES INT          
DECLARE @EXTERNO VARCHAR(15), @MUNI VARCHAR(15), @SERVIU VARCHAR(15)            
DECLARE @CONSULTARESOLUCION INT, @CONSULTANNO INT, @CONSULTAREGION INT, @CONSULTATODO INT          
SET @VARCERO = 0                  
SET @VARUNO = 1           
SET @VARDOS = 2               
SET @VARTRES = 3             
SET @CONSULTARESOLUCION = 4          
SET @CONSULTANNO = 5          
SET @CONSULTAREGION = 6          
SET @CONSULTATODO = 7          
          
BEGIN   
UPDATE SOLICITUD_PAGO_ATP
SET IDMAESTROESTADOSOLICITUD  = @VARTRES
WHERE IDMAESTROESTADOSOLICITUD = @CONSULTAREGION AND IDMAESTROTIPOPAGO = @VARUNO

 
declare @Solicitudes table (IDSOLICITUDATP bigint, CODIGOREGIONSOLICITUDPAGOATP bigint, NUMERORESOLUCIONCONTRATOATP bigint,     
                              MONTOTOTALAPAGARSOLICITUDPAGOATP DECIMAL (18,3),TIPOSERVICIOTIPOACTIVIDADMONTO varchar(100), NOMBREMAESTROESTADOSOLICITUD varchar(100),     
         FECHACREACIONSOLICITUDPAGOATP date)    
             
insert into @Solicitudes                   
 SELECT  distinct SO.IDSOLICITUDATP,          
       SO.CODIGOREGIONSOLICITUDPAGOATP,         
    CO.NUMERORESOLUCIONCONTRATOATP,          
       SO.MONTOTOTALAPAGARSOLICITUDPAGOATP,          
       TI.TIPOSERVICIOTIPOACTIVIDADMONTO,          
       MA.NOMBREMAESTROESTADOSOLICITUD,          
       SO.FECHACREACIONSOLICITUDPAGOATP      
    FROM SOLICITUD_PAGO_ATP SO INNER JOIN           
     CONTRATO_ATP CO ON SO.IDCONTRATOATP = CO.IDCONTRATOATP INNER JOIN          
     PROVEEDOR PRO ON PRO.IDPROVEEDOR = CO.IDPROVEEDOR INNER JOIN          
     TIPO_ACTIVIDAD_MONTO TI ON TI.IDSOLICITUDATP = SO.IDSOLICITUDATP INNER JOIN          
     MAESTRO_ESTADO_SOLICITUD MA ON MA.IDMAESTROESTADOSOLICITUD = SO.IDMAESTROESTADOSOLICITUD            
     
    
   declare @MontoContratoATP table (IDCONTRATOATP bigint, CODIGOREGIONTERRITORIOCONTRATO bigint, MONTOCONTRATOCONTRATOATP bigint,     
                              TIPOSERVICIOMAESTROTIPOSERVICIOIGT varchar(255),FECHACREACIONCONTRATOATP date, CONTRATOS int,     
         AGNOPRESUPUESTOCONTRATOATP int,NUMERORESOLUCIONCONTRATOATP int, NOMBREMAESTROESTADOCONTRATO varchar(100))    
    
insert @MontoContratoATP     
    SELECT CO.IDCONTRATOATP,          
    TE.CODIGOREGIONTERRITORIOCONTRATO,          
    CO.MONTOCONTRATOCONTRATOATP,          
    MA.TIPOSERVICIOMAESTROTIPOSERVICIOIGT,          
    CO.FECHACREACIONCONTRATOATP,          
    CONTRATOS = (SELECT COUNT(IDCONTRATOATP)          
       FROM SOLICITUD_PAGO_ATP          
       WHERE IDCONTRATOATP = CO.IDCONTRATOATP),          
    CO.AGNOPRESUPUESTOCONTRATOATP,          
    CO.NUMERORESOLUCIONCONTRATOATP ,        
 (select NOMBREMAESTROESTADOCONTRATO from MAESTRO_ESTADO_CONTRATO where IDMAESTROESTADOCONTRATO = CO.IDMAESTROESTADOCONTRATO)  as NOMBREMAESTROESTADOCONTRATO        
  FROM CONTRATO_ATP CO INNER JOIN          
    TERRITORIO_CONTRATO TE ON CO.IDTERRITORIOCONTRATO = TE.IDTERRITORIOCONTRATO INNER JOIN          
    PROVEEDOR PRO ON CO.IDPROVEEDOR = PRO.IDPROVEEDOR INNER JOIN          
    MAESTRO_TIPO_SERVICIO_IGT MA ON CO.IDMAESTROTIPOSERVICIOIGT = MA.IDMAESTROTIPOSERVICIOIGT          
    
      
END    
    
    
IF @ACCION = @CONSULTARESOLUCION            
BEGIN          
     
    SELECT       
   RP.NUMERORESOLUCIONPRESUPUESTARIA,          
   RP.ANORESOLUCIONPRESUPUESTARIA,          
   PR.FECHAPRESUPUESTOREGIONAL,          
   RP.ESTADORESOLUCIONPRESUPUESTARIA,          
   RP.NOMBREARCHIVOPRESUPUESTO,        
   PR.CODIGOREGIONPRESUPUESTOREGIONAL,          
  PR.MONTOPRESUPUESTOREGIONAL,    
  ISNULL(( select sum(aux1.MONTOCONTRATOCONTRATOATP) from @MontoContratoATP aux1 where YEAR(aux1.FECHACREACIONCONTRATOATP) = @CONSULTA_PRESUPUESTO_ANNO and   aux1.TIPOSERVICIOMAESTROTIPOSERVICIOIGT = 'SAT' and aux1.CODIGOREGIONTERRITORIOCONTRATO =  PR.CODIGOREGIONPRESUPUESTOREGIONAL),0) as MONTOCONTRATOSAT,    
  ISNULL(( select sum(aux1.MONTOCONTRATOCONTRATOATP) from @MontoContratoATP aux1 where YEAR(aux1.FECHACREACIONCONTRATOATP) = @CONSULTA_PRESUPUESTO_ANNO and   aux1.TIPOSERVICIOMAESTROTIPOSERVICIOIGT = 'SET' and aux1.CODIGOREGIONTERRITORIOCONTRATO =  PR.CODIGOREGIONPRESUPUESTOREGIONAL),0) as MONTOCONTRATOSET,    
  ISNULL(( PR.MONTOPRESUPUESTOREGIONAL - (select sum(aux1.MONTOCONTRATOCONTRATOATP) from @MontoContratoATP aux1 where YEAR(aux1.FECHACREACIONCONTRATOATP) = @CONSULTA_PRESUPUESTO_ANNO and aux1.CODIGOREGIONTERRITORIOCONTRATO =  PR.CODIGOREGIONPRESUPUESTOREGIONAL)) ,PR.MONTOPRESUPUESTOREGIONAL) as PRESUPUESTODISPONIBLE,    
  ISNULL((select sum(aux1.MONTOTOTALAPAGARSOLICITUDPAGOATP) from @Solicitudes aux1   where  YEAR(aux1.FECHACREACIONSOLICITUDPAGOATP) =  @CONSULTA_PRESUPUESTO_ANNO  and aux1.CODIGOREGIONSOLICITUDPAGOATP =  PR.CODIGOREGIONPRESUPUESTOREGIONAL and aux1.TIPOSERVICIOTIPOACTIVIDADMONTO = 'SAT'and aux1.NOMBREMAESTROESTADOSOLICITUD in( 'Creada', 'Ratificada', 'En Reparo')),0) as SATSOLICITUDESCOMPROMETIDAS,    
  ISNULL((select sum(aux1.MONTOTOTALAPAGARSOLICITUDPAGOATP) from @Solicitudes aux1   where  YEAR(aux1.FECHACREACIONSOLICITUDPAGOATP) =  @CONSULTA_PRESUPUESTO_ANNO  and aux1.CODIGOREGIONSOLICITUDPAGOATP =  PR.CODIGOREGIONPRESUPUESTOREGIONAL and aux1.TIPOSERVICIOTIPOACTIVIDADMONTO = 'SET'and aux1.NOMBREMAESTROESTADOSOLICITUD in( 'Creada', 'Ratificada', 'En Reparo')) ,0)as SETSOLICITUDESCOMPROMETIDAS,    
  ISNULL((select sum(aux1.MONTOTOTALAPAGARSOLICITUDPAGOATP) from @Solicitudes aux1   where  YEAR(aux1.FECHACREACIONSOLICITUDPAGOATP) =  @CONSULTA_PRESUPUESTO_ANNO  and aux1.CODIGOREGIONSOLICITUDPAGOATP =  PR.CODIGOREGIONPRESUPUESTOREGIONAL and aux1.TIPOSERVICIOTIPOACTIVIDADMONTO = 'SAT'and aux1.NOMBREMAESTROESTADOSOLICITUD in( '(SIGFE) Devengado', 'Histórica')),0) as    SATSOLICITUDESPAGADAS  ,    
  ISNULL((select sum(aux1.MONTOTOTALAPAGARSOLICITUDPAGOATP) from @Solicitudes aux1   where  YEAR(aux1.FECHACREACIONSOLICITUDPAGOATP) =  @CONSULTA_PRESUPUESTO_ANNO  and aux1.CODIGOREGIONSOLICITUDPAGOATP =  PR.CODIGOREGIONPRESUPUESTOREGIONAL and aux1.TIPOSERVICIOTIPOACTIVIDADMONTO = 'SET'and aux1.NOMBREMAESTROESTADOSOLICITUD in( '(SIGFE) Devengado', 'Histórica')),0) as    SETSOLICITUDESPAGADAS  
    
 FROM RESOLUCION_PRESUPUESTARIA RP INNER JOIN          
   PRESUPUESTO_REGIONAL PR ON PR.IDRESOLUCIONPRESUPUESTARIA= RP.IDRESOLUCIONPRESUPUESTARIA --INNER JOIN          
   --MAESTRO_TIPO_RESOLUCION_PRESUPUESTARIA MP ON MP.IDMAESTROTIPORESOLUCIONPRESUPUESTARIA = RP.IDMAESTROTIPORESOLUCIONPRESUPUESTARIA          
 WHERE RP.NUMERORESOLUCIONPRESUPUESTARIA = @CONSULTA_PRESUPUESTO_NUMERO AND           
   RP.ANORESOLUCIONPRESUPUESTARIA = @CONSULTA_PRESUPUESTO_ANNO  AND          
   PR.CODIGOREGIONPRESUPUESTOREGIONAL = @CONSULTA_PRESUPUESTO_REGION            
END          
          
IF @ACCION = @CONSULTANNO            
BEGIN          
     
  SELECT       
   RP.NUMERORESOLUCIONPRESUPUESTARIA,          
   RP.ANORESOLUCIONPRESUPUESTARIA,          
   PR.FECHAPRESUPUESTOREGIONAL,          
   RP.ESTADORESOLUCIONPRESUPUESTARIA,          
   RP.NOMBREARCHIVOPRESUPUESTO,        
   PR.CODIGOREGIONPRESUPUESTOREGIONAL,          
   PR.MONTOPRESUPUESTOREGIONAL,    
  ISNULL(( select sum(aux1.MONTOCONTRATOCONTRATOATP) from @MontoContratoATP aux1 where YEAR(aux1.FECHACREACIONCONTRATOATP) = @CONSULTA_PRESUPUESTO_ANNO and   aux1.TIPOSERVICIOMAESTROTIPOSERVICIOIGT = 'SAT' and aux1.CODIGOREGIONTERRITORIOCONTRATO =  PR.CODIGOREGIONPRESUPUESTOREGIONAL),0) as MONTOCONTRATOSAT,    
  ISNULL(( select sum(aux1.MONTOCONTRATOCONTRATOATP) from @MontoContratoATP aux1 where YEAR(aux1.FECHACREACIONCONTRATOATP) = @CONSULTA_PRESUPUESTO_ANNO and   aux1.TIPOSERVICIOMAESTROTIPOSERVICIOIGT = 'SET' and aux1.CODIGOREGIONTERRITORIOCONTRATO =  PR.CODIGOREGIONPRESUPUESTOREGIONAL),0) as MONTOCONTRATOSET,    
  ISNULL(( PR.MONTOPRESUPUESTOREGIONAL - (select sum(aux1.MONTOCONTRATOCONTRATOATP) from @MontoContratoATP aux1 where YEAR(aux1.FECHACREACIONCONTRATOATP) = @CONSULTA_PRESUPUESTO_ANNO and aux1.CODIGOREGIONTERRITORIOCONTRATO =  PR.CODIGOREGIONPRESUPUESTOREGIONAL)) ,PR.MONTOPRESUPUESTOREGIONAL) as PRESUPUESTODISPONIBLE,    
  ISNULL((select sum(aux1.MONTOTOTALAPAGARSOLICITUDPAGOATP) from @Solicitudes aux1   where  YEAR(aux1.FECHACREACIONSOLICITUDPAGOATP) =  @CONSULTA_PRESUPUESTO_ANNO  and aux1.CODIGOREGIONSOLICITUDPAGOATP =  PR.CODIGOREGIONPRESUPUESTOREGIONAL and aux1.TIPOSERVICIOTIPOACTIVIDADMONTO = 'SAT'and aux1.NOMBREMAESTROESTADOSOLICITUD in( 'Creada', 'Ratificada', 'En Reparo')),0) as SATSOLICITUDESCOMPROMETIDAS,    
  ISNULL((select sum(aux1.MONTOTOTALAPAGARSOLICITUDPAGOATP) from @Solicitudes aux1   where  YEAR(aux1.FECHACREACIONSOLICITUDPAGOATP) =  @CONSULTA_PRESUPUESTO_ANNO  and aux1.CODIGOREGIONSOLICITUDPAGOATP =  PR.CODIGOREGIONPRESUPUESTOREGIONAL and aux1.TIPOSERVICIOTIPOACTIVIDADMONTO = 'SET'and aux1.NOMBREMAESTROESTADOSOLICITUD in( 'Creada', 'Ratificada', 'En Reparo')) ,0)as SETSOLICITUDESCOMPROMETIDAS,    
  ISNULL((select sum(aux1.MONTOTOTALAPAGARSOLICITUDPAGOATP) from @Solicitudes aux1   where  YEAR(aux1.FECHACREACIONSOLICITUDPAGOATP) =  @CONSULTA_PRESUPUESTO_ANNO  and aux1.CODIGOREGIONSOLICITUDPAGOATP =  PR.CODIGOREGIONPRESUPUESTOREGIONAL and aux1.TIPOSERVICIOTIPOACTIVIDADMONTO = 'SAT'and aux1.NOMBREMAESTROESTADOSOLICITUD in( '(SIGFE) Devengado', 'Histórica')),0) as    SATSOLICITUDESPAGADAS  ,    
  ISNULL((select sum(aux1.MONTOTOTALAPAGARSOLICITUDPAGOATP) from @Solicitudes aux1   where  YEAR(aux1.FECHACREACIONSOLICITUDPAGOATP) =  @CONSULTA_PRESUPUESTO_ANNO  and aux1.CODIGOREGIONSOLICITUDPAGOATP =  PR.CODIGOREGIONPRESUPUESTOREGIONAL and aux1.TIPOSERVICIOTIPOACTIVIDADMONTO = 'SET'and aux1.NOMBREMAESTROESTADOSOLICITUD in( '(SIGFE) Devengado', 'Histórica')),0) as    SETSOLICITUDESPAGADAS  
    
 FROM RESOLUCION_PRESUPUESTARIA RP INNER JOIN           
   PRESUPUESTO_REGIONAL PR ON PR.IDRESOLUCIONPRESUPUESTARIA= RP.IDRESOLUCIONPRESUPUESTARIA --INNER JOIN          
   --MAESTRO_TIPO_RESOLUCION_PRESUPUESTARIA MP ON MP.IDMAESTROTIPORESOLUCIONPRESUPUESTARIA = RP.IDMAESTROTIPORESOLUCIONPRESUPUESTARIA          
 WHERE --MP.VIGENCIAMAESTROTIPORESOLUCION = 1 AND           
   RP.ANORESOLUCIONPRESUPUESTARIA = @CONSULTA_PRESUPUESTO_ANNO AND          
   RP.NUMERORESOLUCIONPRESUPUESTARIA = @CONSULTA_PRESUPUESTO_NUMERO          
   --PR.CODIGOREGIONPRESUPUESTOREGIONAL = @CONSULTA_PRESUPUESTO_REGION          
END          
      
IF (@ACCION = @CONSULTAREGION  and  @CONSULTA_PRESUPUESTO_NUMERO = 0)         
BEGIN          
     
    SELECT       
   RP.NUMERORESOLUCIONPRESUPUESTARIA,          
   RP.ANORESOLUCIONPRESUPUESTARIA,          
   PR.FECHAPRESUPUESTOREGIONAL,          
   RP.ESTADORESOLUCIONPRESUPUESTARIA,          
   RP.NOMBREARCHIVOPRESUPUESTO,        
   PR.CODIGOREGIONPRESUPUESTOREGIONAL,          
   PR.MONTOPRESUPUESTOREGIONAL,    
  ISNULL(( select sum(aux1.MONTOCONTRATOCONTRATOATP) from @MontoContratoATP aux1 where YEAR(aux1.FECHACREACIONCONTRATOATP) = @CONSULTA_PRESUPUESTO_ANNO and   aux1.TIPOSERVICIOMAESTROTIPOSERVICIOIGT = 'SAT' and aux1.CODIGOREGIONTERRITORIOCONTRATO =  PR.CODIGOREGIONPRESUPUESTOREGIONAL),0) as MONTOCONTRATOSAT,    
  ISNULL(( select sum(aux1.MONTOCONTRATOCONTRATOATP) from @MontoContratoATP aux1 where YEAR(aux1.FECHACREACIONCONTRATOATP) = @CONSULTA_PRESUPUESTO_ANNO and   aux1.TIPOSERVICIOMAESTROTIPOSERVICIOIGT = 'SET' and aux1.CODIGOREGIONTERRITORIOCONTRATO =  PR.CODIGOREGIONPRESUPUESTOREGIONAL),0) as MONTOCONTRATOSET,    
  ISNULL(( PR.MONTOPRESUPUESTOREGIONAL - (select sum(aux1.MONTOCONTRATOCONTRATOATP) from @MontoContratoATP aux1 where YEAR(aux1.FECHACREACIONCONTRATOATP) = @CONSULTA_PRESUPUESTO_ANNO and aux1.CODIGOREGIONTERRITORIOCONTRATO =  PR.CODIGOREGIONPRESUPUESTOREGIONAL)) ,PR.MONTOPRESUPUESTOREGIONAL) as PRESUPUESTODISPONIBLE,    
  ISNULL((select sum(aux1.MONTOTOTALAPAGARSOLICITUDPAGOATP) from @Solicitudes aux1   where  YEAR(aux1.FECHACREACIONSOLICITUDPAGOATP) =  @CONSULTA_PRESUPUESTO_ANNO  and aux1.CODIGOREGIONSOLICITUDPAGOATP =  PR.CODIGOREGIONPRESUPUESTOREGIONAL and aux1.TIPOSERVICIOTIPOACTIVIDADMONTO = 'SAT'and aux1.NOMBREMAESTROESTADOSOLICITUD in( 'Creada', 'Ratificada', 'En Reparo')),0) as SATSOLICITUDESCOMPROMETIDAS,    
  ISNULL((select sum(aux1.MONTOTOTALAPAGARSOLICITUDPAGOATP) from @Solicitudes aux1   where  YEAR(aux1.FECHACREACIONSOLICITUDPAGOATP) =  @CONSULTA_PRESUPUESTO_ANNO  and aux1.CODIGOREGIONSOLICITUDPAGOATP =  PR.CODIGOREGIONPRESUPUESTOREGIONAL and aux1.TIPOSERVICIOTIPOACTIVIDADMONTO = 'SET'and aux1.NOMBREMAESTROESTADOSOLICITUD in( 'Creada', 'Ratificada', 'En Reparo')) ,0)as SETSOLICITUDESCOMPROMETIDAS,    
  ISNULL((select sum(aux1.MONTOTOTALAPAGARSOLICITUDPAGOATP) from @Solicitudes aux1   where  YEAR(aux1.FECHACREACIONSOLICITUDPAGOATP) =  @CONSULTA_PRESUPUESTO_ANNO  and aux1.CODIGOREGIONSOLICITUDPAGOATP =  PR.CODIGOREGIONPRESUPUESTOREGIONAL and aux1.TIPOSERVICIOTIPOACTIVIDADMONTO = 'SAT'and aux1.NOMBREMAESTROESTADOSOLICITUD in( '(SIGFE) Devengado', 'Histórica')),0) as    SATSOLICITUDESPAGADAS,    
  ISNULL((select sum(aux1.MONTOTOTALAPAGARSOLICITUDPAGOATP) from @Solicitudes aux1   where  YEAR(aux1.FECHACREACIONSOLICITUDPAGOATP) =  @CONSULTA_PRESUPUESTO_ANNO  and aux1.CODIGOREGIONSOLICITUDPAGOATP =  PR.CODIGOREGIONPRESUPUESTOREGIONAL and aux1.TIPOSERVICIOTIPOACTIVIDADMONTO = 'SET'and aux1.NOMBREMAESTROESTADOSOLICITUD in( '(SIGFE) Devengado', 'Histórica')),0) as    SETSOLICITUDESPAGADAS    
    
 FROM RESOLUCION_PRESUPUESTARIA RP INNER JOIN           
   PRESUPUESTO_REGIONAL PR ON PR.IDRESOLUCIONPRESUPUESTARIA= RP.IDRESOLUCIONPRESUPUESTARIA --INNER JOIN          
   --MAESTRO_TIPO_RESOLUCION_PRESUPUESTARIA MP ON MP.IDMAESTROTIPORESOLUCIONPRESUPUESTARIA = RP.IDMAESTROTIPORESOLUCIONPRESUPUESTARIA          
 WHERE RP.ESTADORESOLUCIONPRESUPUESTARIA = 1 AND          
   PR.CODIGOREGIONPRESUPUESTOREGIONAL = @CONSULTA_PRESUPUESTO_REGION AND           
   RP.ANORESOLUCIONPRESUPUESTARIA = @CONSULTA_PRESUPUESTO_ANNO          
              
END   
          
IF (@ACCION = @CONSULTAREGION   and  @CONSULTA_PRESUPUESTO_NUMERO > 0)          
BEGIN          
     
    SELECT       
   RP.NUMERORESOLUCIONPRESUPUESTARIA,          
   RP.ANORESOLUCIONPRESUPUESTARIA,          
   PR.FECHAPRESUPUESTOREGIONAL,          
   RP.ESTADORESOLUCIONPRESUPUESTARIA,          
   RP.NOMBREARCHIVOPRESUPUESTO,        
   PR.CODIGOREGIONPRESUPUESTOREGIONAL,          
   PR.MONTOPRESUPUESTOREGIONAL,    
  ISNULL(( select sum(aux1.MONTOCONTRATOCONTRATOATP) from @MontoContratoATP aux1 where  YEAR(aux1.FECHACREACIONCONTRATOATP) = @CONSULTA_PRESUPUESTO_ANNO and   aux1.TIPOSERVICIOMAESTROTIPOSERVICIOIGT = 'SAT' and aux1.CODIGOREGIONTERRITORIOCONTRATO =  PR.CODIGOREGIONPRESUPUESTOREGIONAL),0) as MONTOCONTRATOSAT,    
  ISNULL(( select sum(aux1.MONTOCONTRATOCONTRATOATP) from @MontoContratoATP aux1 where YEAR(aux1.FECHACREACIONCONTRATOATP) = @CONSULTA_PRESUPUESTO_ANNO and   aux1.TIPOSERVICIOMAESTROTIPOSERVICIOIGT = 'SET' and aux1.CODIGOREGIONTERRITORIOCONTRATO =  PR.CODIGOREGIONPRESUPUESTOREGIONAL),0) as MONTOCONTRATOSET,    
  ISNULL(( PR.MONTOPRESUPUESTOREGIONAL - (select sum(aux1.MONTOCONTRATOCONTRATOATP) from @MontoContratoATP aux1 where YEAR(aux1.FECHACREACIONCONTRATOATP) = @CONSULTA_PRESUPUESTO_ANNO and aux1.CODIGOREGIONTERRITORIOCONTRATO =  PR.CODIGOREGIONPRESUPUESTOREGIONAL)) ,PR.MONTOPRESUPUESTOREGIONAL) as PRESUPUESTODISPONIBLE,    
  ISNULL((select sum(aux1.MONTOTOTALAPAGARSOLICITUDPAGOATP) from @Solicitudes aux1   where  YEAR(aux1.FECHACREACIONSOLICITUDPAGOATP) =  @CONSULTA_PRESUPUESTO_ANNO  and aux1.CODIGOREGIONSOLICITUDPAGOATP =  PR.CODIGOREGIONPRESUPUESTOREGIONAL and aux1.TIPOSERVICIOTIPOACTIVIDADMONTO = 'SAT'and aux1.NOMBREMAESTROESTADOSOLICITUD in( 'Creada', 'Ratificada', 'En Reparo')),0) as SATSOLICITUDESCOMPROMETIDAS,    
  ISNULL((select sum(aux1.MONTOTOTALAPAGARSOLICITUDPAGOATP) from @Solicitudes aux1   where  YEAR(aux1.FECHACREACIONSOLICITUDPAGOATP) =  @CONSULTA_PRESUPUESTO_ANNO  and aux1.CODIGOREGIONSOLICITUDPAGOATP =  PR.CODIGOREGIONPRESUPUESTOREGIONAL and aux1.TIPOSERVICIOTIPOACTIVIDADMONTO = 'SET'and aux1.NOMBREMAESTROESTADOSOLICITUD in( 'Creada', 'Ratificada', 'En Reparo')) ,0)as SETSOLICITUDESCOMPROMETIDAS,    
  ISNULL((select sum(aux1.MONTOTOTALAPAGARSOLICITUDPAGOATP) from @Solicitudes aux1   where  YEAR(aux1.FECHACREACIONSOLICITUDPAGOATP) =  @CONSULTA_PRESUPUESTO_ANNO  and aux1.CODIGOREGIONSOLICITUDPAGOATP =  PR.CODIGOREGIONPRESUPUESTOREGIONAL and aux1.TIPOSERVICIOTIPOACTIVIDADMONTO = 'SAT'and aux1.NOMBREMAESTROESTADOSOLICITUD in( '(SIGFE) Devengado', 'Histórica')),0) as    SATSOLICITUDESPAGADAS,    
  ISNULL((select sum(aux1.MONTOTOTALAPAGARSOLICITUDPAGOATP) from @Solicitudes aux1   where  YEAR(aux1.FECHACREACIONSOLICITUDPAGOATP) =  @CONSULTA_PRESUPUESTO_ANNO  and aux1.CODIGOREGIONSOLICITUDPAGOATP =  PR.CODIGOREGIONPRESUPUESTOREGIONAL and aux1.TIPOSERVICIOTIPOACTIVIDADMONTO = 'SET'and aux1.NOMBREMAESTROESTADOSOLICITUD in( '(SIGFE) Devengado', 'Histórica')),0) as    SETSOLICITUDESPAGADAS    
 FROM RESOLUCION_PRESUPUESTARIA RP INNER JOIN           
   PRESUPUESTO_REGIONAL PR ON PR.IDRESOLUCIONPRESUPUESTARIA= RP.IDRESOLUCIONPRESUPUESTARIA --INNER JOIN          
   --MAESTRO_TIPO_RESOLUCION_PRESUPUESTARIA MP ON MP.IDMAESTROTIPORESOLUCIONPRESUPUESTARIA = RP.IDMAESTROTIPORESOLUCIONPRESUPUESTARIA          
 WHERE          
   PR.CODIGOREGIONPRESUPUESTOREGIONAL = @CONSULTA_PRESUPUESTO_REGION AND           
   RP.ANORESOLUCIONPRESUPUESTARIA = @CONSULTA_PRESUPUESTO_ANNO  and         
   RP.NUMERORESOLUCIONPRESUPUESTARIA = @CONSULTA_PRESUPUESTO_NUMERO        
              
END          
          
IF @ACCION = @CONSULTATODO          
BEGIN          
     
   SELECT       
   RP.NUMERORESOLUCIONPRESUPUESTARIA,          
   RP.ANORESOLUCIONPRESUPUESTARIA,          
   PR.FECHAPRESUPUESTOREGIONAL,          
   RP.ESTADORESOLUCIONPRESUPUESTARIA,          
   RP.NOMBREARCHIVOPRESUPUESTO,        
   PR.CODIGOREGIONPRESUPUESTOREGIONAL,          
  PR.MONTOPRESUPUESTOREGIONAL,    
  ISNULL(( select sum(aux1.MONTOCONTRATOCONTRATOATP) from @MontoContratoATP aux1 where YEAR(aux1.FECHACREACIONCONTRATOATP) = @CONSULTA_PRESUPUESTO_ANNO and   aux1.TIPOSERVICIOMAESTROTIPOSERVICIOIGT = 'SAT' and aux1.CODIGOREGIONTERRITORIOCONTRATO =  PR.CODIGOREGIONPRESUPUESTOREGIONAL),0) as MONTOCONTRATOSAT,    
  ISNULL(( select sum(aux1.MONTOCONTRATOCONTRATOATP) from @MontoContratoATP aux1 where YEAR(aux1.FECHACREACIONCONTRATOATP) = @CONSULTA_PRESUPUESTO_ANNO and   aux1.TIPOSERVICIOMAESTROTIPOSERVICIOIGT = 'SET' and aux1.CODIGOREGIONTERRITORIOCONTRATO =  PR.CODIGOREGIONPRESUPUESTOREGIONAL),0) as MONTOCONTRATOSET,    
  ISNULL(( PR.MONTOPRESUPUESTOREGIONAL - (select sum(aux1.MONTOCONTRATOCONTRATOATP) from @MontoContratoATP aux1 where YEAR(aux1.FECHACREACIONCONTRATOATP) = @CONSULTA_PRESUPUESTO_ANNO and aux1.CODIGOREGIONTERRITORIOCONTRATO =  PR.CODIGOREGIONPRESUPUESTOREGIONAL)) ,PR.MONTOPRESUPUESTOREGIONAL) as PRESUPUESTODISPONIBLE,    
  ISNULL((select sum(aux1.MONTOTOTALAPAGARSOLICITUDPAGOATP) from @Solicitudes aux1   where  YEAR(aux1.FECHACREACIONSOLICITUDPAGOATP) =  @CONSULTA_PRESUPUESTO_ANNO  and aux1.CODIGOREGIONSOLICITUDPAGOATP =  PR.CODIGOREGIONPRESUPUESTOREGIONAL and aux1.TIPOSERVICIOTIPOACTIVIDADMONTO = 'SAT'and aux1.NOMBREMAESTROESTADOSOLICITUD in( 'Creada', 'Ratificada', 'En Reparo')),0) as SATSOLICITUDESCOMPROMETIDAS,    
  ISNULL((select sum(aux1.MONTOTOTALAPAGARSOLICITUDPAGOATP) from @Solicitudes aux1   where  YEAR(aux1.FECHACREACIONSOLICITUDPAGOATP) =  @CONSULTA_PRESUPUESTO_ANNO  and aux1.CODIGOREGIONSOLICITUDPAGOATP =  PR.CODIGOREGIONPRESUPUESTOREGIONAL and aux1.TIPOSERVICIOTIPOACTIVIDADMONTO = 'SET'and aux1.NOMBREMAESTROESTADOSOLICITUD in( 'Creada', 'Ratificada', 'En Reparo')) ,0)as SETSOLICITUDESCOMPROMETIDAS,    
  ISNULL((select sum(aux1.MONTOTOTALAPAGARSOLICITUDPAGOATP) from @Solicitudes aux1   where  YEAR(aux1.FECHACREACIONSOLICITUDPAGOATP) =  @CONSULTA_PRESUPUESTO_ANNO  and aux1.CODIGOREGIONSOLICITUDPAGOATP =  PR.CODIGOREGIONPRESUPUESTOREGIONAL and aux1.TIPOSERVICIOTIPOACTIVIDADMONTO = 'SAT'and aux1.NOMBREMAESTROESTADOSOLICITUD in( '(SIGFE) Devengado', 'Histórica')),0) as    SATSOLICITUDESPAGADAS,    
  ISNULL((select sum(aux1.MONTOTOTALAPAGARSOLICITUDPAGOATP) from @Solicitudes aux1   where  YEAR(aux1.FECHACREACIONSOLICITUDPAGOATP) =  @CONSULTA_PRESUPUESTO_ANNO  and aux1.CODIGOREGIONSOLICITUDPAGOATP =  PR.CODIGOREGIONPRESUPUESTOREGIONAL and aux1.TIPOSERVICIOTIPOACTIVIDADMONTO = 'SET'and aux1.NOMBREMAESTROESTADOSOLICITUD in( '(SIGFE) Devengado', 'Histórica')),0) as    SETSOLICITUDESPAGADAS    
 FROM RESOLUCION_PRESUPUESTARIA RP INNER JOIN           
   PRESUPUESTO_REGIONAL PR ON PR.IDRESOLUCIONPRESUPUESTARIA= RP.IDRESOLUCIONPRESUPUESTARIA --INNER JOIN          
   --MAESTRO_TIPO_RESOLUCION_PRESUPUESTARIA MP ON MP.IDMAESTROTIPORESOLUCIONPRESUPUESTARIA = RP.IDMAESTROTIPORESOLUCIONPRESUPUESTARIA          
 WHERE RP.ESTADORESOLUCIONPRESUPUESTARIA = 1 AND           
   RP.ANORESOLUCIONPRESUPUESTARIA = @CONSULTA_PRESUPUESTO_ANNO          
END     
GO
/****** Object:  StoredProcedure [dbo].[ELIMINA_CONTRATO]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/********************************************************************************************          
* Nombre del procedimiento      : ELIMINA_CONTRATO  
* Fecha de creación             : 17/12/2018          
* Usuario de creación           : Anticipa         
* Versión.                      : V.0.0.0.1          
  
Fecha de modificación   : 18-02-2019  
Usuario de modificación : cfajardo
Motivo de modificación  : regularización de procedimientos almacenados entre ambientes.

Visado por DBA              :  Mirto Blanco                                   
Fecha Aprobación DBA        :  20190220                                    
Comentarios DBA             :                                     
  
* Parametros     : @IDCONTRATO  
          
* Objetivo                      : Procedimiento que elimina el contrato  
* Prueba      : exec ELIMINA_CONTRATO 13  
**********************************************************************************************/       
CREATE PROCEDURE [dbo].[ELIMINA_CONTRATO]          
@IDCONTRATO BIGINT  
  
AS  
SET NOCOUNT ON  
DECLARE @CERO INT  
DECLARE @IDTERRITORIOCONTRATO BIGINT  
--DECLARE @IDPROPIEDADTERRENO BIGINT  
   
SET @CERO = 0   
  
/****** Control Errores ***********/      
DECLARE @ERR INT, @MSG VARCHAR(250)      
SET @MSG = 'Contrato eliminado correctamente.'      
SET @ERR = 0      
/**********************************/   
  
BEGIN TRANSACTION  
  
BEGIN  
  
 BEGIN  
 SET @IDTERRITORIOCONTRATO = (SELECT IDTERRITORIOCONTRATO FROM CONTRATO_ATP WHERE IDCONTRATOATP = @IDCONTRATO)  
 --SET @IDPROPIEDADTERRENO = (SELECT IDPROPIEDADTERRENO FROM CONTRATO_ATP WHERE IDCONTRATOATP = @IDCONTRATO)  
  
 BEGIN  
   DELETE FROM TIPO_SERVICIOS_CONTRATO  
   WHERE IDCONTRATOATP = @IDCONTRATO  
   if @@rowcount <= @CERO      
    begin      
     set @err = -1      
     set @msg = 'Error al Eliminar Contrato'      
     goto error      
    end   
 END  
  
 BEGIN  
   DELETE FROM CONTRATO_ATP  
   WHERE IDCONTRATOATP = @IDCONTRATO  
   if @@rowcount <= @CERO      
    begin      
     set @err = -1  
     set @msg = 'Error al Eliminar Contrato'      
     goto error      
    end   
 END  
  
 BEGIN  
   DELETE FROM TERRITORIO_CONTRATO  
   WHERE IDTERRITORIOCONTRATO = @IDTERRITORIOCONTRATO  
   if @@rowcount <= @CERO      
    begin      
     set @err = -1      
     set @msg = 'Error al Eliminar Contrato'      
     goto error      
    end   
 END  
 --BEGIN  
 --  DELETE FROM PROPIEDAD_TERRENO  
 --  WHERE IDPROPIEDADTERRENO = @IDPROPIEDADTERRENO  
 --  if @@rowcount <= @CERO      
 --   begin      
 --    set @err = -1      
 --    set @msg = 'Error al Eliminar Contrato '  
 --    goto error      
 --   end   
 --END  
  
 END  
END  
  
SET NOCOUNT OFF    
COMMIT TRANSACTION    
 SELECT @ERR AS err, @MSG AS MSG    
 RETURN    
ERROR: ROLLBACK  TRANSACTION    
    SELECT @ERR AS err, @MSG AS MSG

GO
/****** Object:  StoredProcedure [dbo].[ELIMINA_SOLICITUD]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/********************************************************************************************          
* Nombre del procedimiento      : ELIMINA_SOLICITUD  
* Fecha de creación             : 17/12/2018          
* Usuario de creación           : Anticipa         
* Versión.                      : V.0.0.0.1          
  
Fecha de modificación   : 18-02-2019  
Usuario de modificación : cfajardo
Motivo de modificación  : regularización de procedimientos almacenados entre ambientes.

Visado por DBA              :  Mirto Blanco                                   
Fecha Aprobación DBA        :  20190220                                    
Comentarios DBA             :                                     
  
* Parametros     : @IDSOLICITUD  
          
* Objetivo                      : Procedimiento que elimina la solicitud  
* Prueba      : exec ELIMINA_SOLICITUD 153  
**********************************************************************************************/       
CREATE PROCEDURE [dbo].[ELIMINA_SOLICITUD]          
@IDSOLICITUD BIGINT  
  
AS  
SET NOCOUNT ON  
DECLARE @CERO INT  
  
SET @CERO = 0   
  
/****** Control Errores ***********/      
DECLARE @ERR INT, @MSG VARCHAR(250)      
SET @MSG = 'Contrato eliminado correctamente.'      
SET @ERR = 0      
/**********************************/   
  
BEGIN TRANSACTION  
  
BEGIN  
  
  BEGIN  
   DELETE FROM TIPO_ACTIVIDAD_MONTO  
   WHERE IDSOLICITUDATP = @IDSOLICITUD  
   if @@rowcount <= @CERO      
    begin      
     set @err = -1      
     set @msg = 'Error al Eliminar Solicitud'      
     goto error      
    end   
  END  
  
  BEGIN  
   DELETE FROM SOLICITUD_PAGO_ATP  
   WHERE IDSOLICITUDATP = @IDSOLICITUD   
   if @@rowcount <= @CERO      
    begin      
     set @err = -1      
     set @msg = 'Error al Eliminar Solicitud'      
     goto error      
    end    
  END  
END  
  
SET NOCOUNT OFF    
COMMIT TRANSACTION    
 SELECT @ERR AS err, @MSG AS MSG    
 RETURN    
ERROR: ROLLBACK  TRANSACTION    
    SELECT @ERR AS err, @MSG AS MSG  

GO
/****** Object:  StoredProcedure [dbo].[INSERTA_BENEFICIARIOS_SNATDS49]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/********************************************************************************************  
NOMBRE DEL PROCEDIMIENTO 	: [INSERTA_BENEFICIARIOS_SNATDS49]
FECHA DE CREACIÓN      		: 29-06-2018
USUARIO DE CREACIÓN      	: Daniel Orozco - Anticipa.
VERSIÓN            			: 1.0                            

Visado por DBA              : José López
Fecha Aprobación DBA        : 20181029 
Comentarios DBA             : 
          
OBJETIVO     				: Inserta en SNAT Simplificado los proyectos obtenidos desde Rukan 
  
TABLAS      				: BENEFICIARIO
							 					  
		
          
  
QUE RETORNA     			: --
  
PARAMETROS     				: @RUTBENEFICIARIO Int,
							  @DIGITOVERIFICADORBENEFICIARIO Char(1),
							  @NOMBREBENEFICIARIO VarChar(100),
							  @APELLIDOPATERNOBENEFICIARIO VarChar(100),
							  @APELLIDOMATERNOBENEFICIARIO VarChar(100),
							  @ESTADOBENEFICIARIO Bit,
							  @IDMAESTROALTERNATIVAPOSTULACION BigInt,
							  @FECHAINICIOVIGENCIASUBSIDIOBENEFICIARIO DateTime,
							  @FECHATERMINOVIGENCIASUBSIDIOBENEFICIARIO DateTime,
							  @FECHAADSCRIPCIONPROYECTOBENEFICIARIO DateTime,
							  @CODIGOPROYECTO BigInt,
							  @CODIGOCOMUNADIRECCION Int,
							  @CODIGOPROVINCIADIRECCION Int,
							  @CODIGOREGIONDIRECCION Int
            
PRUEBA						:           

           
PROYECTO     				: SNAT SIMPLIFICADO
RESPONSABLE     			: DINFO  
							  
********************************************************************************************/  
CREATE PROCEDURE [dbo].[INSERTA_BENEFICIARIOS_SNATDS49]
	@RUTBENEFICIARIO Int,
	@DIGITOVERIFICADORBENEFICIARIO Char(1),
	@NOMBREBENEFICIARIO VarChar(100),
	@APELLIDOPATERNOBENEFICIARIO VarChar(100),
	@APELLIDOMATERNOBENEFICIARIO VarChar(100),
	@ESTADOBENEFICIARIO Bit,
	@IDMAESTROALTERNATIVAPOSTULACION BigInt,
	@FECHAINICIOVIGENCIASUBSIDIOBENEFICIARIO DateTime,
	@FECHATERMINOVIGENCIASUBSIDIOBENEFICIARIO DateTime,
	@FECHAADSCRIPCIONPROYECTOBENEFICIARIO DateTime,
	@CODIGOPROYECTO BigInt,
	@CODIGOCOMUNADIRECCION Int,
	@CODIGOPROVINCIADIRECCION Int,
	@CODIGOREGIONDIRECCION Int
AS
BEGIN
	BEGIN TRAN
	BEGIN
		--INICIO -- Inserta Direccion
		IF NOT((@CODIGOCOMUNADIRECCION IS NULL) OR (@CODIGOPROVINCIADIRECCION IS NULL) OR (@CODIGOREGIONDIRECCION IS NULL))
		BEGIN
			DECLARE @IDDIRECCION BigInt
			SET @IDDIRECCION = (SELECT MAX(IDDIRECCION) FROM DIRECCION) + 1

			INSERT INTO DIRECCION(IDDIRECCION,NUMERODIRECCION,CODIGOCOMUNADIRECCION,CODIGOPROVINCIADIRECCION,CODIGOREGIONDIRECCION)
			VALUES(@IDDIRECCION,NULL,@CODIGOCOMUNADIRECCION,@CODIGOPROVINCIADIRECCION,@CODIGOREGIONDIRECCION)

			IF(@@ERROR <> 0)
			BEGIN
				ROLLBACK SELECT 'ERROR EN INSERTAR DIRECCION'
			END
		END
		--FIN -- Inserta Direccion

		--INICIO -- Obtencion de IDINFORMACIONPROYECTO
		DECLARE @IDINFORMACIONPROYECTO BigInt

		SET @IDINFORMACIONPROYECTO = (SELECT	IDINFORMACIONPROYECTO
										FROM	INFORMACION_PROYECTO
										WHERE	CODIGOPROYECTOINFORMACIONPROYECTO = @CODIGOPROYECTO
											AND IDMAESTROPROGRAMA = 3)--3 DS49
		--FIN -- Obtencion de IDINFORMACIONPROYECTO

		--INICIO -- Ingreso Beneficiario
		IF(@IDINFORMACIONPROYECTO > 0)
		BEGIN
			DECLARE @IDBENEFICIARIO BigInt
		
			SET @IDBENEFICIARIO = (IsNULL((SELECT MAX(IDBENEFICIARIO) FROM BENEFICIARIO),0)) + 1

			INSERT INTO BENEFICIARIO(IDBENEFICIARIO,
									IDDIRECCION,
									IDINFORMACIONPROYECTO,
									RUTBENEFICIARIO,
									DIGITOVERIFICADORBENEFICIARIO,
									NOMBREBENEFICIARIO,
									APELLIDOPATERNOBENEFICIARIO,
									APELLIDOMATERNOBENEFICIARIO,
									ESTADOBENEFICIARIO,
									IDMAESTROALTERNATIVAPOSTULACION,
									FECHAINICIOVIGENCIASUBSIDIOBENEFICIARIO,
									FECHATERMINOVIGENCIASUBSIDIOBENEFICIARIO,
									FECHAADSCRIPCIONPROYECTOBENEFICIARIO)
			VALUES(@IDBENEFICIARIO,
					@IDDIRECCION,
					@IDINFORMACIONPROYECTO,
					@RUTBENEFICIARIO,
					@DIGITOVERIFICADORBENEFICIARIO,
					@NOMBREBENEFICIARIO,
					@APELLIDOPATERNOBENEFICIARIO,
					@APELLIDOMATERNOBENEFICIARIO,
					@ESTADOBENEFICIARIO,
					@IDMAESTROALTERNATIVAPOSTULACION,
					@FECHAINICIOVIGENCIASUBSIDIOBENEFICIARIO,
					@FECHATERMINOVIGENCIASUBSIDIOBENEFICIARIO,
					@FECHAADSCRIPCIONPROYECTOBENEFICIARIO)

			IF(@@ERROR <> 0)
			BEGIN
				ROLLBACK SELECT 'ERROR AL INSERTAR BENEFICIARIO'
			END
		END
		--FIN -- Ingreso Beneficiario

		COMMIT TRAN
			SELECT 'OK'
	END
END




GO
/****** Object:  StoredProcedure [dbo].[INSERTA_CONTRATO_SAT_SET]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/********************************************************************************************          
* Nombre del procedimiento      : INSERTA_CONTRATO_SAT_SET  
* Fecha de creación             : 09/10/2018          
* Usuario de creación           : Anticipa         
* Versión.                      : V.0.0.0.1     

Fecha de modificación   : 18-02-2019  
Usuario de modificación : cfajardo
Motivo de modificación  : regularización de procedimientos almacenados entre ambientes.     
  
Visado por DBA              :  Iván Frade Cortés                                   
Fecha Aprobación DBA        :  20190320                                    
Comentarios DBA             :                                     
  
* Parametros     : @RUTPROVEEDOR  
         @DVPROVEEDOR   
         @NOMBREPROVEEDOR  
         @TIPOPROVEEDOR  
         @ANNOCONTRATO  
         @CODIGOREGION  
         @CODIGOPROVINCIA  
         @CODIGOCOMUNA  
         @PLAZOEJECUCION  
         @PROPIEDADTERRENO  
         @NUMERORESOLUCION  
         @FECHARESOLUCIONCONTRATO  
         @FECHAINICIOCONTRATO  
         @NOMBREARCHIVO  
         @IDTIPOSERVICIO  
         @ESTADOTIPOSERVICIO1  
         @ESTADOTIPOSERVICIO2  
         @ESTADOTIPOSERVICIO3  
         @ESTADOTIPOSERVICIO4  
         @ESTADOTIPOSERVICIO5  
         @ESTADOTIPOSERVICIO6  
         @ESTADOTIPOSERVICIO7  
         @NOMBRETIPOSERVICIO8  
         @PRODUCTOCONTRATO  
         @DESCRIPCIONPRODUCTO  
         @MONTOCONTRATO  
         @OBSERVACIONCONTRATO  
         @USUARIO  
          
* Objetivo                      : Procedimiento que inserta Presupuesto Regional IGTD        
* Prueba      : exec INSERTA_CONTRATO_SAT_SET 1,9,'Razon Social',5,2018,11,1,1107,30,2,550,'20180824','20180824','archivo.pdf','SAT',1,1,0,0,0,0,0,'','Prueba','Prueba',10000,null,'usuario'  
**********************************************************************************************/       
  
CREATE PROCEDURE [dbo].[INSERTA_CONTRATO_SAT_SET]          
@RUTPROVEEDOR INT,    
@DVPROVEEDOR VARCHAR(10),  
@NOMBREPROVEEDOR VARCHAR(100),    
@TIPOPROVEEDOR INT,  
@ANNOCONTRATO INT,  
@CODIGOREGION INT,  
@CODIGOPROVINCIA INT,  
@CODIGOCOMUNA INT,  
@PLAZOEJECUCION INT,  
@PROPIEDADTERRENO VARCHAR(200),  
@NUMERORESOLUCION BIGINT,  
@FECHARESOLUCIONCONTRATO DATETIME,  
@FECHAINICIOCONTRATO DATETIME,  
@NOMBREARCHIVO VARCHAR(200),  
@IDTIPOSERVICIO BIGINT,  
@ESTADOTIPOSERVICIO1 INT,  
@ESTADOTIPOSERVICIO2 INT,  
@ESTADOTIPOSERVICIO3 INT,  
@ESTADOTIPOSERVICIO4 INT,  
@ESTADOTIPOSERVICIO5 INT,  
@ESTADOTIPOSERVICIO6 INT,  
@ESTADOTIPOSERVICIO7 INT,  
@NOMBRETIPOSERVICIO8 VARCHAR(200),  
@PRODUCTOCONTRATO VARCHAR(200),  
@DESCRIPCIONPRODUCTO VARCHAR(200),  
@MONTOCONTRATO BIGINT,  
@OBSERVACIONCONTRATO VARCHAR(200),  
@USUARIO VARCHAR(50)  
  
AS  
  
BEGIN  
  
/****** Control Errores ***********/      
DECLARE @ERR INT, @MSG VARCHAR(250)      
SET @MSG = 'Contrato ingresado correctamente.'      
SET @ERR = 0      
/**********************************/   
  
DECLARE @IDTERRITORIOCONTRATO BIGINT  
DECLARE @IDPROPIEDADTERRENO BIGINT  
DECLARE @IDPROVEEDOR BIGINT  
DECLARE @IDTIPOSERVICIOSCONTRATO BIGINT  
  
DECLARE @IDCONTRATO BIGINT  
  
DECLARE @IDMAESTROSERVICIO BIGINT  
  
DECLARE @IDRESOLUCIONPRESUPUESTARIA BIGINT  
DECLARE @IDPRESUPUESTOREGIONAL BIGINT  
DECLARE @VIGENCIA BIT  
DECLARE @NUMVIGENCIA INT  
  
  
DECLARE @MAESTROPROVEEDOR INT  
DECLARE @CERO INT  
  
SET @VIGENCIA = 1  
SET @CERO = 0   
--SET @TIPOPROVEEDOR = 1  
  
BEGIN TRANSACTION  
  
IF NOT EXISTS (SELECT NUMERORESOLUCIONCONTRATOATP FROM CONTRATO_ATP WHERE NUMERORESOLUCIONCONTRATOATP = @NUMERORESOLUCION)   
  
    BEGIN   
        --SET @IDTERRITORIOCONTRATO = (SELECT MAX(IDTERRITORIOCONTRATO) FROM TERRITORIO_CONTRATO) + 1  
  
        INSERT INTO [dbo].[TERRITORIO_CONTRATO]  
            ([CODIGOPROVINCIATERRITORIOCONTRATO]  
            ,[CODIGOREGIONTERRITORIOCONTRATO]  
            ,[CODIGOCOMUNATERRITORIOCONTRATO]  
            ,[ESTADOTERRITORIOCONTRATO]  
            ,[FECHACREACIONTERRITORIOCONTRATO]  
            ,[FECHAACTUALIZACIONTERRITORIOCONTRATO]  
            ,[USUARIOTERRITORIOCONTRATO])  
        VALUES (@CODIGOPROVINCIA  
            ,@CODIGOREGION  
            ,@CODIGOCOMUNA  
            ,@VIGENCIA  
            ,@FECHARESOLUCIONCONTRATO  
            ,@FECHARESOLUCIONCONTRATO  
            ,@USUARIO)  
         
        IF @@rowcount <= @CERO      
        BEGIN      
            SET @err = -1      
            SET @msg = 'TERRITORIO_CONTRATO.'  
            GOTO error      
        END
		
		SET @IDTERRITORIOCONTRATO = CONVERT(INT,@@Identity)   
  
        IF NOT EXISTS (SELECT IDPROVEEDOR FROM PROVEEDOR WHERE RUTPROVEEDOR = @RUTPROVEEDOR)   
        BEGIN  
            INSERT INTO [dbo].[PROVEEDOR]  
                ([IDMAESTROTIPOPROVEEDOR]  
                ,[NOMBREPROVEEDOR]  
                ,[RUTPROVEEDOR]  
                ,[DVPROVEDIGITOVERIFICADORPROVEEDOR])  
            VALUES  
                (@TIPOPROVEEDOR  
                ,@NOMBREPROVEEDOR  
                ,@RUTPROVEEDOR  
                ,@DVPROVEEDOR)  
             
            IF @@rowcount <= @CERO      
            BEGIN      
                SET @err = -1      
                SET @msg = 'ERROR EN INSERTAR PROVEEDOR.'  
                GOTO error      
            END   
        END  
          
        BEGIN  
            SET @IDPROVEEDOR = (SELECT TOP 1 IDPROVEEDOR FROM PROVEEDOR WHERE RUTPROVEEDOR = @RUTPROVEEDOR)  
           
            INSERT INTO [dbo].[CONTRATO_ATP]  
                ([IDTERRITORIOCONTRATO]  
                ,[IDPROPIEDADTERRENO]  
                ,[IDPROVEEDOR]  
                ,[IDMAESTROTIPOSERVICIOIGT]  
                ,[NUMERORESOLUCIONCONTRATOATP]  
                ,[ARCHIVOCONTRATOATP]  
                ,[AGNOPRESUPUESTOCONTRATOATP]  
                ,[OBSERVACIONCONTRATOATP]  
                ,[FECHACREACIONCONTRATOATP]  
                ,[FECHAACTUALIZACIONCONTRATOATP]  
                ,[PRODUCTOCONTRATOATP]  
                ,[DESCRIPCIONPRODUCTOCONTRATOATP]  
                ,[USUARIOCONTRATOATP]  
                ,[PLAZOEJECUCIONCONTRATOATP]  
                ,[FECHAINICIOCONTRATOATP]  
                ,[NOMBREARCHIVOCONTRATOATP]  
                ,[FECHARESOLUCIONCONTRATOATP]  
                ,[MONTOCONTRATOCONTRATOATP]  
                ,[IDMAESTROTIPOPROVEEDOR]  
                ,[IDMAESTROESTADOCONTRATO])  
            VALUES  
                (@IDTERRITORIOCONTRATO  
                ,@PROPIEDADTERRENO  
                ,@IDPROVEEDOR  
                ,@IDTIPOSERVICIO  
                ,@NUMERORESOLUCION  
                ,NULL  
                ,@ANNOCONTRATO  
                ,@OBSERVACIONCONTRATO  
                ,@FECHAINICIOCONTRATO  
                ,@FECHAINICIOCONTRATO  
                ,@PRODUCTOCONTRATO  
                ,@DESCRIPCIONPRODUCTO  
                ,@USUARIO  
                ,@PLAZOEJECUCION  
                ,@FECHAINICIOCONTRATO  
                ,@NOMBREARCHIVO  
                ,@FECHARESOLUCIONCONTRATO  
                ,@MONTOCONTRATO  
                ,@TIPOPROVEEDOR  
                ,@VIGENCIA)  
          
            IF @@rowcount <= @CERO      
            BEGIN      
                SET @err = -1      
                SET @msg = 'ERROR EN INSERTAR CONTRATO.'  
                GOTO error      
            END
	 
	        SELECT @IDCONTRATO = CONVERT(INT,@@Identity)    
        END  

        BEGIN  
  
            INSERT INTO [dbo].[TIPO_SERVICIOS_CONTRATO]  
                             ([IDMAESTROSERVICIO]  
                             ,[IDCONTRATOATP]  
                             ,[ESTADOTIPOSERVICIOSCONTRATO])  
            VALUES  
                             (49  
                             ,@IDCONTRATO  
                             ,@ESTADOTIPOSERVICIO1)  
  
            IF @@rowcount <= @CERO      
            BEGIN      
                SET @err = -1      
                SET @msg = 'ERROR EN INSERTAR TIPO_SERVICIOS_CONTRATO.'  
                GOTO error      
            END   
            
            INSERT INTO [dbo].[TIPO_SERVICIOS_CONTRATO]  
                 ([IDMAESTROSERVICIO]  
                 ,[IDCONTRATOATP]  
                 ,[ESTADOTIPOSERVICIOSCONTRATO])        
              VALUES  
                 (50  
                 ,@IDCONTRATO  
                 ,@ESTADOTIPOSERVICIO2)  
            
               IF @@rowcount <= @CERO      
                BEGIN      
                 SET @err = -1      
                 SET @msg = 'ERROR EN INSERTAR TIPO_SERVICIOS_CONTRATO.'  
                 GOTO error      
                END   
            
            INSERT INTO [dbo].[TIPO_SERVICIOS_CONTRATO]  
                 ([IDMAESTROSERVICIO]  
                 ,[IDCONTRATOATP]  
                 ,[ESTADOTIPOSERVICIOSCONTRATO])         
              VALUES  
                 (51  
                 ,@IDCONTRATO  
                 ,@ESTADOTIPOSERVICIO3)  
            
               IF @@rowcount <= @CERO      
                BEGIN      
                 SET @err = -1      
                 SET @msg = 'ERROR EN INSERTAR TIPO_SERVICIOS_CONTRATO.'  
                 GOTO error      
                END   
            
            INSERT INTO [dbo].[TIPO_SERVICIOS_CONTRATO]  
                 ([IDMAESTROSERVICIO]  
                 ,[IDCONTRATOATP]  
                 ,[ESTADOTIPOSERVICIOSCONTRATO])        
              VALUES  
                 (52  
                 ,@IDCONTRATO  
                 ,@ESTADOTIPOSERVICIO4)  
            
               IF @@rowcount <= @CERO      
                BEGIN      
                 SET @err = -1      
                 SET @msg = 'ERROR EN INSERTAR TIPO_SERVICIOS_CONTRATO.'  
                 GOTO error      
                END   
            
            INSERT INTO [dbo].[TIPO_SERVICIOS_CONTRATO]  
                 ([IDMAESTROSERVICIO]  
                 ,[IDCONTRATOATP]  
                 ,[ESTADOTIPOSERVICIOSCONTRATO])         
              VALUES  
                 (53  
                 ,@IDCONTRATO  
                 ,@ESTADOTIPOSERVICIO5)  
            
               IF @@rowcount <= @CERO      
                BEGIN      
                 SET @err = -1      
                 SET @msg = 'ERROR EN INSERTAR TIPO_SERVICIOS_CONTRATO.'  
                 GOTO error      
                END   
            
            SET @IDTIPOSERVICIOSCONTRATO = (SELECT MAX(IDTIPOSERVICIOSCONTRATO) FROM TIPO_SERVICIOS_CONTRATO) + 1  
            
            INSERT INTO [dbo].[TIPO_SERVICIOS_CONTRATO]  
                 ([IDMAESTROSERVICIO]  
                 ,[IDCONTRATOATP]  
                 ,[ESTADOTIPOSERVICIOSCONTRATO])         
              VALUES  
                 (54  
                 ,@IDCONTRATO  
                 ,@ESTADOTIPOSERVICIO6)  
            
               IF @@rowcount <= @CERO      
                BEGIN      
                 SET @err = -1      
                 SET @msg = 'ERROR EN INSERTAR TIPO_SERVICIOS_CONTRATO.'  
                 GOTO error      
                END   
            
            SET @IDTIPOSERVICIOSCONTRATO = (SELECT MAX(IDTIPOSERVICIOSCONTRATO) FROM TIPO_SERVICIOS_CONTRATO) + 1  
            
            INSERT INTO [dbo].[TIPO_SERVICIOS_CONTRATO]  
                 ([IDMAESTROSERVICIO]  
                 ,[IDCONTRATOATP]  
                 ,[ESTADOTIPOSERVICIOSCONTRATO])          
              VALUES  
                 (55  
                 ,@IDCONTRATO  
                 ,@ESTADOTIPOSERVICIO7)  
            
               IF @@rowcount <= @CERO      
                BEGIN      
                 SET @err = -1      
                 SET @msg = 'ERROR EN INSERTAR TIPO_SERVICIOS_CONTRATO.'  
                 GOTO error      
                END   
            
            IF (@NOMBRETIPOSERVICIO8 <> '')  
            BEGIN  
             --SET @IDMAESTROSERVICIO = (SELECT MAX(IDMAESTROSERVICIO) FROM MAESTRO_SERVICIO) + 1  
            
             INSERT INTO [dbo].[MAESTRO_SERVICIO]  
                  ([NOMBREMAESTROSERVICIO]  
                  ,[ESTADOMAESTROSERVICIO]  
                  ,[NOMBREABREVIADOMAESTROSERVICIO])         
               VALUES  
                  (@NOMBRETIPOSERVICIO8  
                  ,1  
                  ,'SET')  
            
                IF @@rowcount <= @CERO      
                 BEGIN      
                  SET @err = -1      
                  SET @msg = 'ERROR EN INSERTAR MAESTRO_SERVICIO.'  
                  GOTO error      
                 END

			SELECT @IDMAESTROSERVICIO = CONVERT(INT,@@Identity)   	    
            
             INSERT INTO [dbo].[TIPO_SERVICIOS_CONTRATO]  
                  ([IDMAESTROSERVICIO]  
                  ,[IDCONTRATOATP]  
                  ,[ESTADOTIPOSERVICIOSCONTRATO])         
               VALUES  
                  (@IDMAESTROSERVICIO  
                  ,@IDCONTRATO  
                  ,1)  
            
                IF @@rowcount <= @CERO      
                 BEGIN      
                  SET @err = -1      
                  SET @msg = 'ERROR EN INSERTAR TIPO_SERVICIOS_CONTRATO.'  
                  GOTO error      
                 END   
            END  
 END  
END  
  
ELSE  
  
BEGIN      
 SET @MSG = 'Contrato ' +CAST(@NUMERORESOLUCION as varchar(6))  + ' ya fue ingresado previamente. '      
 SET @ERR = -2         
 GOTO error      
END  
  
END  
  
SET NOCOUNT OFF    
COMMIT TRANSACTION    
 SELECT @ERR AS err, @MSG AS MSG    
 RETURN    
ERROR: ROLLBACK  TRANSACTION    
    SELECT @ERR AS err, @MSG AS MSG

GO
/****** Object:  StoredProcedure [dbo].[INSERTA_GENERACION_AUTORIZACION_PAGO]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

  /********************************************************************************************          
* Nombre del procedimiento      : INSERTA_GENERACION_AUTORIZACION_PAGO  
* Fecha de creación             : 04/10/2018          
* Usuario de creación           :  Daniel Orozco            
* Versión.                      : V.0.0.0.1          

Fecha de modificación   : 18-02-2019  
Usuario de modificación : cfajardo
Motivo de modificación  : regularización de procedimientos almacenados entre ambientes.
  
Visado por DBA              :  Mirto Blanco                                   
Fecha Aprobación DBA        :  20190220                                    
Comentarios DBA             :                                     
  
* Parametros     : @SOLICITUDPAGOLIST varchar(max),    
                   @USUARIORESPONSABLE varchar(200)    
          
* Objetivo                      : Crea Autorizaciones segun Solicitudes    
* Prueba      : 
**********************************************************************************************/       


CREATE PROCEDURE [dbo].[INSERTA_GENERACION_AUTORIZACION_PAGO]    
 @SOLICITUDPAGOLIST varchar(max),    
 @USUARIORESPONSABLE varchar(200)    
AS    
BEGIN    
 BEGIN TRAN    
 BEGIN    
  ------------------------------------SEPARACION DE CADENA ID SOLICITUD------------------------------------    
  DECLARE @SOLICITUDES TABLE (IDSOLICITUDPAGO bigint)    
    
  DECLARE @IDSOLICITUDPAGO bigint    
  DECLARE @pos INT    
    
  WHILE CHARINDEX('|', @SOLICITUDPAGOLIST) > 0    
  BEGIN    
   SELECT @pos = CHARINDEX('|', @SOLICITUDPAGOLIST)      
   SELECT @IDSOLICITUDPAGO = SUBSTRING(@SOLICITUDPAGOLIST, 1, @pos-1)    
    
   INSERT INTO @SOLICITUDES     
   SELECT @IDSOLICITUDPAGO    
    
   SELECT @SOLICITUDPAGOLIST = SUBSTRING(@SOLICITUDPAGOLIST, @pos+1, LEN(@SOLICITUDPAGOLIST)-@pos)    
  END    
     
  INSERT INTO @SOLICITUDES    
  SELECT @SOLICITUDPAGOLIST    
  ------------------------------------SEPARACION DE CADENA ID SOLICITUD------------------------------------    
    
  ------------------------------------OBTENCION DE DATOS SOLICITUD PAGO------------------------------------    
  DECLARE @DATOSSOLICITUD TABLE (IDSOLICITUDPAGO bigint,    
          CODIGOREGION int,    
          IDMAESTROPROGRAMA bigint,    
          IDPROVEEDOR bigint,    
          IDMAESTROTIPOPROVEEDOR bigint,    
          IDMAESTROLLAMADO bigint,    
          IDMAESTROTIPOLOGIA bigint,    
          IDMAESTROSUBMODALIDAD bigint)    
    
  INSERT INTO @DATOSSOLICITUD (IDSOLICITUDPAGO,    
         CODIGOREGION,    
         IDMAESTROPROGRAMA,    
         IDPROVEEDOR,    
         IDMAESTROTIPOPROVEEDOR,    
         IDMAESTROLLAMADO,    
         IDMAESTROTIPOLOGIA,    
         IDMAESTROSUBMODALIDAD)    
  SELECT S.IDSOLICITUDPAGO,    
    CODIGOREGION = D.CODIGOREGIONDIRECCION,    
    IDMAESTROPROGRAMA = P.IDMAESTROPROGRAMA,    
    IDPROVEEDOR = S.IDPROVEEDOR,    
    IDTIPOPROVEEDOR = PV.IDMAESTROTIPOPROVEEDOR,    
    IDMAESTROLLAMADO = LL.IDMAESTROLLAMADO,    
    IDMAESTROTIPOLOGIA = T.IDMAESTROTIPOLOGIA,    
    IDMAESTROSUBMODALIDAD = MSM.IDMAESTROSUBMODALIDAD    
  FROM SOLICITUD_PAGO S    
  INNER JOIN TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA TS    
   ON S.IDSOLICITUDPAGO = TS.IDSOLICITUDPAGO    
    AND S.IDCARACTERISTICASESPECIALES = TS.IDCARACTERISTICASESPECIALES    
  INNER JOIN SERVICIO_PARCIALIDAD SP    
   ON TS.IDSERVICIOPARCIALIDAD = SP.IDSERVICIOPARCIALIDAD    
  INNER JOIN TIPOLOGIA_SERVICIO TP    
   ON SP.IDTIPOLOGIASERVICIO = TP.IDTIPOLOGIASERVICIO    
  INNER JOIN MAESTRO_TIPOLOGIA T    
   ON TP.IDMAESTROTIPOLOGIA = T.IDMAESTROTIPOLOGIA    
  INNER JOIN CARACTERISTICAS_ESPECIALES CE    
   ON S.IDCARACTERISTICASESPECIALES = CE.IDCARACTERISTICASESPECIALES    
  INNER JOIN INFORMACION_PROYECTO IP    
   ON CE.IDINFORMACIONPROYECTO = IP.IDINFORMACIONPROYECTO    
  INNER JOIN MAESTRO_LLAMADO LL    
   ON IP.IDMAESTROLLAMADO = LL.IDMAESTROLLAMADO    
  INNER JOIN MAESTRO_PROGRAMA P    
   ON IP.IDMAESTROPROGRAMA = P.IDMAESTROPROGRAMA    
  INNER JOIN DIRECCION D    
   ON IP.IDDIRECCION = D.IDDIRECCION    
  INNER JOIN SUB_MODALIDAD_PARCIALIDAD SMP    
   ON SP.IDSUBMODALIDADPARCIALIDAD = SMP.IDSUBMODALIDADPARCIALIDAD    
  INNER JOIN MAESTRO_SUB_MODALIDAD MSM    
   ON SMP.IDMAESTROPARCIALIDAD = MSM.IDMAESTROSUBMODALIDAD    
  INNER JOIN PROVEEDOR PV    
   ON S.IDPROVEEDOR = PV.IDPROVEEDOR    
  WHERE S.IDSOLICITUDPAGO IN(SELECT IDSOLICITUDPAGO FROM @SOLICITUDES)    
  ------------------------------------OBTENCION DE DATOS SOLICITUD PAGO------------------------------------    
    
    
  ------------------------CAMBIO ESTADO SOLICITUDES------------------------    
  UPDATE SOLICITUD_PAGO    
  SET IDMAESTROESTADOSOLICITUD = 5 --En Autorización de pago    
  WHERE IDSOLICITUDPAGO IN(SELECT IDSOLICITUDPAGO FROM @DATOSSOLICITUD)    
    
  IF(@@ERROR <> 0)    
  BEGIN    
   ROLLBACK SELECT 0 as ERR,'ERROR AL MODIFICAR ESTADO SOLICITUD' as MSG    
  END    
  ------------------------CAMBIO ESTADO SOLICITUDES------------------------    
    
  --------------------------------------------OBTENCION DATOS CON MODALIDAD--------------------------------------------    
  --CON SUBMODALIDAD    
  DECLARE @CONSUBMODALIDAD TABLE (IDCONSUBMODALIDAD bigint IDENTITY,    
          IDSOLICITUDPAGO bigint,    
          CODIGOREGION int,    
          IDMAESTROPROGRAMA bigint,    
          IDPROVEEDOR bigint,    
          IDMAESTROTIPOPROVEEDOR bigint,    
          IDMAESTROLLAMADO bigint,    
          IDMAESTROTIPOLOGIA bigint)    
    
  INSERT INTO @CONSUBMODALIDAD    
  SELECT DISTINCT IDSOLICITUDPAGO, CODIGOREGION, IDMAESTROPROGRAMA, IDPROVEEDOR, IDMAESTROTIPOPROVEEDOR, IDMAESTROLLAMADO, IDMAESTROTIPOLOGIA    
  FROM @DATOSSOLICITUD    
  WHERE IDSOLICITUDPAGO IN(SELECT IDSOLICITUDPAGO    
         FROM @DATOSSOLICITUD    
         WHERE IDMAESTROSUBMODALIDAD NOT IN(1))    
    
      
  --SIN SUBMODALIDAD    
  DECLARE @SINSUBMODALIDAD TABLE (IDSINSUBMODALIDAD bigint IDENTITY,    
          IDSOLICITUDPAGO bigint,    
          CODIGOREGION int,    
          IDMAESTROPROGRAMA bigint,    
          IDPROVEEDOR bigint,    
          IDMAESTROTIPOPROVEEDOR bigint,    
          IDMAESTROLLAMADO bigint,    
          IDMAESTROTIPOLOGIA bigint)    
    
  INSERT INTO @SINSUBMODALIDAD    
  SELECT DISTINCT IDSOLICITUDPAGO, CODIGOREGION, IDMAESTROPROGRAMA, IDPROVEEDOR, IDMAESTROTIPOPROVEEDOR, IDMAESTROLLAMADO, IDMAESTROTIPOLOGIA    
  FROM @DATOSSOLICITUD    
  WHERE IDSOLICITUDPAGO NOT IN(SELECT IDSOLICITUDPAGO FROM @CONSUBMODALIDAD)    
  --------------------------------------------OBTENCION DATOS CON MODALIDAD--------------------------------------------    
     
  --Declaracion de variables para recorrer e insertar    
  DECLARE @IDSUBMODALIDAD bigint,    
    @IDAUTORIZACION bigint    
    
  DECLARE @INSERTS TABLE(IDSOLICITUDPAGO bigint,    
        CODIGOREGION int,    
        IDMAESTROPROGRAMA bigint,    
        IDPROVEEDOR bigint,    
        IDMAESTROTIPOPROVEEDOR bigint,    
        IDMAESTROLLAMADO bigint,    
        IDMAESTROTIPOLOGIA bigint,    
        CANTIDADPROYECTOSAUTORIZACION int,    
        CANTIDADSOLICITUDPAGOAUTORIZACION int,    
        MONTOTOTALAUTORIZACION decimal(18,3),    
        USUARIORESPONSABLE varchar(200),    
        NUMEROAUTORIZACION bigint,    
        IDMAESTROMODALIDAD bigint,    
        IDMAESTROTITULO bigint)    
     
  ----------------------------------------------------------TRATAMIENTO DATOS CON SUBMODALIDAD----------------------------------------------------------    
  WHILE ((SELECT COUNT(1) FROM @CONSUBMODALIDAD) > 0)    
  BEGIN    
   DELETE FROM @INSERTS --Reseteo Tabla con Datos para Insertar    
   SET @IDSUBMODALIDAD = (SELECT TOP 1 IDCONSUBMODALIDAD FROM @CONSUBMODALIDAD) --Obtengo el Primer Dato    
    
   --------------------------------------Obtengo todas las Solicitudes agupadas por variables--------------------------------------    
   INSERT INTO @INSERTS    
   SELECT CS.IDSOLICITUDPAGO,    
     CS.CODIGOREGION,    
     CS.IDMAESTROPROGRAMA,    
     CS.IDPROVEEDOR,    
     CS.IDMAESTROTIPOPROVEEDOR,    
     CS.IDMAESTROLLAMADO,    
     CS.IDMAESTROTIPOLOGIA,    
     CANTIDADPROYECTOSAUTORIZACION = 0,    
     CANTIDADSOLICITUDPAGOAUTORIZACION = 0,    
     MONTOTOTALAUTORIZACION = S.MONTOSOLICITUDSOLICITUDPAGO,    
     USUARIORESPONSABLE = @USUARIORESPONSABLE,    
     NUMEROAUTORIZACION = 0,    
     C.IDMAESTROMODALIDAD,    
     IP.IDMAESTROTITULO    
   FROM @CONSUBMODALIDAD CS    
    INNER JOIN SOLICITUD_PAGO S    
     ON S.IDSOLICITUDPAGO = CS.IDSOLICITUDPAGO    
    INNER JOIN CARACTERISTICAS_ESPECIALES C    
     ON S.IDCARACTERISTICASESPECIALES = C.IDCARACTERISTICASESPECIALES    
    INNER JOIN INFORMACION_PROYECTO IP    
     ON C.IDINFORMACIONPROYECTO = IP.IDINFORMACIONPROYECTO    
   WHERE CS.CODIGOREGION = (SELECT CODIGOREGION FROM @CONSUBMODALIDAD WHERE IDCONSUBMODALIDAD = @IDSUBMODALIDAD)    
    AND CS.IDMAESTROPROGRAMA = (SELECT IDMAESTROPROGRAMA FROM @CONSUBMODALIDAD WHERE IDCONSUBMODALIDAD = @IDSUBMODALIDAD)    
    AND CS.IDPROVEEDOR = (SELECT IDPROVEEDOR FROM @CONSUBMODALIDAD WHERE IDCONSUBMODALIDAD = @IDSUBMODALIDAD)    
    AND CS.IDMAESTROTIPOPROVEEDOR = (SELECT IDMAESTROTIPOPROVEEDOR FROM @CONSUBMODALIDAD WHERE IDCONSUBMODALIDAD = @IDSUBMODALIDAD)    
    AND CS.IDMAESTROLLAMADO = (SELECT IDMAESTROLLAMADO FROM @CONSUBMODALIDAD WHERE IDCONSUBMODALIDAD = @IDSUBMODALIDAD)    
    AND CS.IDMAESTROTIPOLOGIA = (SELECT IDMAESTROTIPOLOGIA FROM @CONSUBMODALIDAD WHERE IDCONSUBMODALIDAD = @IDSUBMODALIDAD)    
    
   --Actualizo Cantidades    
   UPDATE @INSERTS    
   SET CANTIDADSOLICITUDPAGOAUTORIZACION = (SELECT COUNT(1) FROM @INSERTS),    
    CANTIDADPROYECTOSAUTORIZACION = (SELECT COUNT(DISTINCT CODIGOPROYECTOINFORMACIONPROYECTO)    
            FROM INFORMACION_PROYECTO IP    
             INNER JOIN CARACTERISTICAS_ESPECIALES C    
              ON IP.IDINFORMACIONPROYECTO = C.IDINFORMACIONPROYECTO    
             INNER JOIN SOLICITUD_PAGO S    
              ON C.IDCARACTERISTICASESPECIALES = S.IDCARACTERISTICASESPECIALES    
            WHERE S.IDSOLICITUDPAGO IN(SELECT IDSOLICITUDPAGO FROM @INSERTS))    
   --------------------------------------Obtengo todas las Solicitudes agupadas por variables--------------------------------------    
      
   --------------------------------------------------------------INGRESO AUTORIZACION--------------------------------------------------------------    
   --INSERTA AUTORIZACION    
   INSERT INTO AUTORIZACION (IDMAESTROESTADOAUTORIZACION,    
         ESPECIALAUTORIZACION,    
         CANTIDADPROYECTOSAUTORIZACION,    
         CANTIDADSOLICITUDPAGOAUTORIZACION,    
         MONTOTOTALAUTORIZACION,    
         USUARIORESPONSABLEAUTORIZACION,    
         FECHAINGRESOAUTORIZACION,    
         IDMAESTROPROGRAMA,    
         IDMAESTROTIPOLOGIA,    
         IDPROVEEDOR,    
         CODIGOREGIONAUTORIZACION,    
         NUMEROAUTORIZACION,    
         IDMAESTROMODALIDAD,    
         IDMAESTROTITULO)    
   SELECT DISTINCT    
     1,--(En Ratificación --1 --ESTADO AUTORIZACION)    
     0,    
     CANTIDADPROYECTOSAUTORIZACION,    
     CANTIDADSOLICITUDPAGOAUTORIZACION,    
     (SELECT SUM(I.MONTOTOTALAUTORIZACION) FROM @INSERTS I),    
     USUARIORESPONSABLE,    
     GETDATE(),    
     IDMAESTROPROGRAMA,    
     IDMAESTROTIPOLOGIA,    
     IDPROVEEDOR,    
     CODIGOREGION,    
     NUMEROAUTORIZACION,    
     IDMAESTROMODALIDAD,    
     IDMAESTROTITULO    
   FROM @INSERTS    
    
   SET @IDAUTORIZACION = @@IDENTITY    
    
   IF(@@ERROR <> 0)    
   BEGIN    
    ROLLBACK SELECT 0 AS ERR,'ERROR AL INGRESAR AUTORIZACION CON' AS MSG    
   END    
    
   --Actualizacion de NUMEROAUTORIZACION    
   UPDATE AUTORIZACION    
   SET NUMEROAUTORIZACION = (CONVERT(VarChar, IDAUTORIZACION) +     
          (CASE    
           WHEN LEN(CONVERT(VarChar, CODIGOREGIONAUTORIZACION)) = 1 THEN '0' + CONVERT(VarChar, CODIGOREGIONAUTORIZACION)    
          ELSE    
           CONVERT(VarChar, CODIGOREGIONAUTORIZACION)    
          END) +    
          SUBSTRING(CONVERT(VarChar, FECHAINGRESOAUTORIZACION,112), 1, 4)),--N°Autotizacion / ID Autorizacion + IDRegion + Año    
    ESPECIALAUTORIZACION = (CASE    
           WHEN MONTOTOTALAUTORIZACION < 0 THEN 1    
          ELSE    
           0    
          END)    
   WHERE IDAUTORIZACION = @IDAUTORIZACION    
    
   IF(@@ERROR <> 0)    
   BEGIN    
    ROLLBACK SELECT 0 AS ERR,'ERROR AL INGRESAR ACTUALIZAR AUTORIZACION CON' AS MSG    
   END    
   --------------------------------------------------------------INGRESO AUTORIZACION--------------------------------------------------------------    
    
   ------------------------INSERTA REALACION SOLICITUD AUTORIZACION------------------------    
   INSERT INTO TIPO_AUTORIZACION (IDSOLICITUDPAGO,IDAUTORIZACION,ESTADOTIPOAUTORIZACION)    
   SELECT IDSOLICITUDPAGO,    
     @IDAUTORIZACION,    
1    
   FROM @INSERTS    
    
   IF(@@ERROR <> 0)    
   BEGIN    
    ROLLBACK SELECT 0 AS ERR,'ERROR AL INGRESAR TIPO AUTORIZACION CON' AS MSG    
   END    
   ------------------------INSERTA REALACION SOLICITUD AUTORIZACION------------------------    
    
   --Se eliminan registros ya ingresados    
   DELETE FROM @CONSUBMODALIDAD WHERE IDSOLICITUDPAGO IN(SELECT IDSOLICITUDPAGO FROM @INSERTS)    
  END    
  ----------------------------------------------------------TRATAMIENTO DATOS CON SUBMODALIDAD----------------------------------------------------------    
      
  ----------------------------------------------------------TRATAMIENTO DATOS SIN SUBMODALIDAD----------------------------------------------------------    
  WHILE ((SELECT COUNT(1) FROM @SINSUBMODALIDAD) > 0)    
  BEGIN    
   DELETE FROM @INSERTS --Reseteo Tabla con Datos para Insertar    
   SET @IDSUBMODALIDAD = (SELECT TOP 1 IDSINSUBMODALIDAD FROM @SINSUBMODALIDAD) --Obtengo el Primer Dato    
    
   --Obtengo todas las Solicitudes agupadas por variables    
   INSERT INTO @INSERTS    
   SELECT CS.IDSOLICITUDPAGO,    
     CS.CODIGOREGION,    
     CS.IDMAESTROPROGRAMA,    
     CS.IDPROVEEDOR,    
     CS.IDMAESTROTIPOPROVEEDOR,    
     CS.IDMAESTROLLAMADO,    
     CS.IDMAESTROTIPOLOGIA,    
     CANTIDADPROYECTOSAUTORIZACION = 0,    
     CANTIDADSOLICITUDPAGOAUTORIZACION = 0,    
     MONTOTOTALAUTORIZACION = S.MONTOTOTALPROYECTOSOLICITUDPAGO,    
     USUARIORESPONSABLE = @USUARIORESPONSABLE,    
     NUMEROAUTORIZACION = 0,    
     C.IDMAESTROMODALIDAD,    
     IP.IDMAESTROTITULO    
   FROM @SINSUBMODALIDAD CS    
    INNER JOIN SOLICITUD_PAGO S    
     ON S.IDSOLICITUDPAGO = CS.IDSOLICITUDPAGO    
    INNER JOIN CARACTERISTICAS_ESPECIALES C    
     ON S.IDCARACTERISTICASESPECIALES = C.IDCARACTERISTICASESPECIALES    
    INNER JOIN INFORMACION_PROYECTO IP    
     ON C.IDINFORMACIONPROYECTO = IP.IDINFORMACIONPROYECTO    
   WHERE CS.CODIGOREGION = (SELECT CODIGOREGION FROM @SINSUBMODALIDAD WHERE IDSINSUBMODALIDAD = @IDSUBMODALIDAD)    
    AND CS.IDMAESTROPROGRAMA = (SELECT IDMAESTROPROGRAMA FROM @SINSUBMODALIDAD WHERE IDSINSUBMODALIDAD = @IDSUBMODALIDAD)    
    AND CS.IDPROVEEDOR = (SELECT IDPROVEEDOR FROM @SINSUBMODALIDAD WHERE IDSINSUBMODALIDAD = @IDSUBMODALIDAD)    
    AND CS.IDMAESTROTIPOPROVEEDOR = (SELECT IDMAESTROTIPOPROVEEDOR FROM @SINSUBMODALIDAD WHERE IDSINSUBMODALIDAD = @IDSUBMODALIDAD)    
    AND CS.IDMAESTROLLAMADO = (SELECT IDMAESTROLLAMADO FROM @SINSUBMODALIDAD WHERE IDSINSUBMODALIDAD = @IDSUBMODALIDAD)    
    AND CS.IDMAESTROTIPOLOGIA = (SELECT IDMAESTROTIPOLOGIA FROM @SINSUBMODALIDAD WHERE IDSINSUBMODALIDAD = @IDSUBMODALIDAD)    
    
   --Actualizo Cantidades    
   UPDATE @INSERTS    
   SET CANTIDADSOLICITUDPAGOAUTORIZACION = (SELECT COUNT(1) FROM @INSERTS),    
    CANTIDADPROYECTOSAUTORIZACION = (SELECT COUNT(DISTINCT CODIGOPROYECTOINFORMACIONPROYECTO)    
            FROM INFORMACION_PROYECTO IP    
             INNER JOIN CARACTERISTICAS_ESPECIALES C    
              ON IP.IDINFORMACIONPROYECTO = C.IDINFORMACIONPROYECTO    
             INNER JOIN SOLICITUD_PAGO S    
              ON C.IDCARACTERISTICASESPECIALES = S.IDCARACTERISTICASESPECIALES    
            WHERE S.IDSOLICITUDPAGO IN(SELECT IDSOLICITUDPAGO FROM @INSERTS))    
    
   --------------------------------------------------------------INGRESO AUTORIZACION--------------------------------------------------------------    
   INSERT INTO AUTORIZACION (IDMAESTROESTADOAUTORIZACION,    
          ESPECIALAUTORIZACION,    
          CANTIDADPROYECTOSAUTORIZACION,    
          CANTIDADSOLICITUDPAGOAUTORIZACION,    
          MONTOTOTALAUTORIZACION,    
          USUARIORESPONSABLEAUTORIZACION,    
          FECHAINGRESOAUTORIZACION,    
          IDMAESTROPROGRAMA,    
          IDMAESTROTIPOLOGIA,    
          IDPROVEEDOR,    
          CODIGOREGIONAUTORIZACION,    
          NUMEROAUTORIZACION,    
          IDMAESTROMODALIDAD,    
          IDMAESTROTITULO)    
   SELECT DISTINCT    
     1,--(En Ratificación --1 --ESTADO AUTORIZACION)    
     1,    
     CANTIDADPROYECTOSAUTORIZACION,    
     CANTIDADSOLICITUDPAGOAUTORIZACION,    
     (SELECT SUM(I.MONTOTOTALAUTORIZACION) FROM @INSERTS I),    
     USUARIORESPONSABLE,    
     GETDATE(),    
     IDMAESTROPROGRAMA,    
     IDMAESTROTIPOLOGIA,    
     IDPROVEEDOR,    
     CODIGOREGION,    
     NUMEROAUTORIZACION,    
     IDMAESTROMODALIDAD,    
     IDMAESTROTITULO    
   FROM @INSERTS    
    
   SET @IDAUTORIZACION = @@IDENTITY    
    
   IF(@@ERROR <> 0)    
   BEGIN    
    ROLLBACK SELECT 0 AS ERR,'ERROR AL INGRESAR AUTORIZACION SIN' AS MSG    
   END    
    
   --Actualizacion de NUMEROAUTORIZACION    
   UPDATE AUTORIZACION    
   SET NUMEROAUTORIZACION = (CONVERT(VarChar, IDAUTORIZACION) +     
          (CASE    
           WHEN LEN(CONVERT(VarChar, CODIGOREGIONAUTORIZACION)) = 1 THEN '0' + CONVERT(VarChar, CODIGOREGIONAUTORIZACION)    
          ELSE    
           CONVERT(VarChar, CODIGOREGIONAUTORIZACION)    
          END) +    
          SUBSTRING(CONVERT(VarChar, FECHAINGRESOAUTORIZACION,112), 1, 4)),--N°Autotizacion / ID Autorizacion + IDRegion + Año    
    ESPECIALAUTORIZACION = (CASE    
           WHEN MONTOTOTALAUTORIZACION < 0 THEN 1    
          ELSE    
           0    
          END)    
   WHERE IDAUTORIZACION = @IDAUTORIZACION    
       
   IF(@@ERROR <> 0)    
   BEGIN    
    ROLLBACK SELECT 0 AS ERR,'ERROR AL INGRESAR ACTUALIZAR AUTORIZACION SIN' AS MSG    
   END    
   --------------------------------------------------------------INGRESO AUTORIZACION--------------------------------------------------------------    
    
   ------------------------INSERTA REALACION SOLICITUD AUTORIZACION------------------------    
   INSERT INTO TIPO_AUTORIZACION (IDSOLICITUDPAGO,IDAUTORIZACION,ESTADOTIPOAUTORIZACION)    
   SELECT IDSOLICITUDPAGO,    
     @IDAUTORIZACION,    
     1    
   FROM @INSERTS    
    
   IF(@@ERROR <> 0)    
   BEGIN    
    ROLLBACK SELECT 0 AS ERR,'ERROR AL INGRESAR TIPO AUTORIZACION SIN' AS MSG    
   END    
   ------------------------INSERTA REALACION SOLICITUD AUTORIZACION------------------------    
      
   --Se eliminan registros ya ingresados    
   DELETE FROM @SINSUBMODALIDAD WHERE IDSOLICITUDPAGO IN(SELECT IDSOLICITUDPAGO FROM @INSERTS)    
  END    
  ----------------------------------------------------------TRATAMIENTO DATOS SIN SUBMODALIDAD----------------------------------------------------------    
    
  --RESULTADO EXITOSO    
  COMMIT TRAN    
   SELECT 1 AS ERR,'OK' AS MSG    
 END    
END 

GO
/****** Object:  StoredProcedure [dbo].[INSERTA_GENERACION_SOLICITUD_PAGO]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

  /********************************************************************************************          
NOMBRE DEL PROCEDIMIENTO  : [INSERTA_GENERACION_SOLICITUD_PAGO]        
FECHA DE CREACIÓN        : 11/10/2018        
USUARIO DE CREACIÓN       : Daniel Orozco - Anticipa.        
        
VERSIÓN               : 1.1        
        
Modificación    : Se agregan dos parametros de entrada para el guardado de la solicitud de pago        
Fecha modificación          : 18/12/2018        
usuario de modificación     : cfajardo        
        
Visado por DBA              : Iván Frade Cortés
Fecha Aprobación DBA        : 20190407         
Comentarios DBA             :   ---      
                  
OBJETIVO         : Crea una Solicitud de Pago        
          
TABLAS          : SOLICITUD_PAGO        
                              
          
                  
          
QUE RETORNA        : --        
          
PARAMETROS         : @IDMAESTROESTADOSOLICITUD bigint,        
         @FOLIOBOLETAGARANTIASOLICITUDPAGO varchar(200),        
         @NUMEROFAMILIASPAGARSOLICITUDPAGO int,        
         @USUARIORESPONSABLESOLICITUDPAGO varchar(100),        
         @IDCARACTERISTICASESPECIALES bigint,        
         @IDMAESTROTIPOPAGO bigint,        
         @FECHAREALCREACIONSOLICITUDPAGO date,        
         @FECHACREACIONSOLICITUDPAGO date,        
         @IDMAESTROTIPODESTINOPAGO bigint,        
         @MONTOTOTALPROYECTOSOLICITUDPAGO decimal(18,3),        
         @MONTOPAGADOSOLICITUDPAGO decimal(18,3),        
         @MONTOCOMPROMETIDOSOLICITUDPAGO decimal(18,3),        
         @SALDOPORPAGARSOLICITUDPAGO decimal(18,3),        
         @AVANCEOBRASOLICITUDPAGO decimal(18,3),        
         @NUMERORESOLUCIONCONTRATOSOLICITUDPAGO bigint,        
         @MONTOSOLICITUDSOLICITUDPAGO decimal(18,3),        
         @IDPROVEEDOR bigint,        
         @NUMEROVIVIENDASSOLICITUDPAGO int,        
         @IDMAESTROTIPOPROVEEDOR bigint,        
         @NOMBREPROVEEDOR varchar(100),        
         @RUTPROVEEDOR int,        
         @DVPROVEDIGITOVERIFICADORPROVEEDOR char(1)        
         @AVANCEOBRAMUNIN decimal(18,3) null,          
         @ESTADOAVANCEOBRA varchar(255) null          
                    
PRUEBA      :         
        
                   
PROYECTO         : SNAT SIMPLIFICADO        
RESPONSABLE        : DINFO          
                 
********************************************************************************************/          
CREATE PROCEDURE [dbo].[INSERTA_GENERACION_SOLICITUD_PAGO]          
 @IDMAESTROESTADOSOLICITUD bigint,          
 @FOLIOBOLETAGARANTIASOLICITUDPAGO varchar(200),          
 @NUMEROFAMILIASPAGARSOLICITUDPAGO int,          
 @USUARIORESPONSABLESOLICITUDPAGO varchar(100),          
 @IDCARACTERISTICASESPECIALES bigint,          
 @IDMAESTROTIPOPAGO bigint,          
 @FECHAREALCREACIONSOLICITUDPAGO date,          
 @FECHACREACIONSOLICITUDPAGO date,          
 @IDMAESTROTIPODESTINOPAGO bigint,          
 @MONTOTOTALPROYECTOSOLICITUDPAGO decimal(18,3),          
 @MONTOPAGADOSOLICITUDPAGO decimal(18,3),          
 @MONTOCOMPROMETIDOSOLICITUDPAGO decimal(18,3),          
 @SALDOPORPAGARSOLICITUDPAGO decimal(18,3),          
 @NUMERORESOLUCIONCONTRATOSOLICITUDPAGO bigint,          
 @MONTOSOLICITUDSOLICITUDPAGO decimal(18,3),          
 @IDPROVEEDOR bigint,          
 @NUMEROVIVIENDASSOLICITUDPAGO int,          
 @OBSERVACIONESSOLICITUDPAGO varchar(200),          
 @IDMAESTROTIPOPROVEEDOR bigint,          
 @NOMBREPROVEEDOR varchar(100),          
 @RUTPROVEEDOR int,          
 @DVPROVEDIGITOVERIFICADORPROVEEDOR char(1),          
 @AVANCEOBRAMUNIN decimal(18,3) null,          
 @ESTADOAVANCEOBRA varchar(255) null,      
 @FECHARESOLUCIONSOLICITUDPAGO date,    
 @FECHABOLETAGARANTIA   DATE  ,  
 @IDPROVEEDORMANDATO bigint  
AS          
BEGIN          
 BEGIN TRAN          
 BEGIN          
  DECLARE @IDSOLICITUDPAGO bigint          
  DECLARE @0 BIGINT  
  SET @0 =0       
  ----------------------------------OBTENCION IDINFORMACIONPROYECTO----------------------------------          
  DECLARE @IDINFORMACIONPROYECTO bigint          
  SET @IDINFORMACIONPROYECTO = (SELECT IDINFORMACIONPROYECTO          
          FROM CARACTERISTICAS_ESPECIALES          
          WHERE IDCARACTERISTICASESPECIALES = @IDCARACTERISTICASESPECIALES)          
  ----------------------------------OBTENCION IDINFORMACIONPROYECTO----------------------------------          
          
          
  ----------------------------------------------------INGRESO PROVEEDOR----------------------------------------------------          
  --SI NO VIENE PROVEEDOR          
  IF(@IDPROVEEDOR = 0)          
  BEGIN          
   --INSERT PROVEEDOR          
   INSERT INTO PROVEEDOR(IDMAESTROTIPOPROVEEDOR, NOMBREPROVEEDOR, RUTPROVEEDOR, DVPROVEDIGITOVERIFICADORPROVEEDOR)          
   VALUES(@IDMAESTROTIPOPROVEEDOR, @NOMBREPROVEEDOR, @RUTPROVEEDOR, @DVPROVEDIGITOVERIFICADORPROVEEDOR)          
          
   SET @IDPROVEEDOR = @@IDENTITY--ID Ingresado          
          
   --INSERT PROVEEDOR_INFORMACION_PROYECTO          
   INSERT INTO TIPO_PROVEEDOR_INFORMACION_PROYECTO(IDPROVEEDOR,IDINFORMACIONPROYECTO,ESTADOTIPOPROVEEDORINFORMACIONPROYECTO)          
   VALUES(@IDPROVEEDOR, @IDINFORMACIONPROYECTO, 1)          
          
   IF(@@ERROR <> 0)          
   BEGIN          
    ROLLBACK SELECT 0 as ERR,          
        'ERROR AL INGRESAR PROVEEDOR' as MSG,          
        @IDSOLICITUDPAGO AS IDSOLICITUDPAGO          
   END          
  END

  if(SELECT COUNT(IDPROVEEDOR) FROM TIPO_PROVEEDOR_INFORMACION_PROYECTO WHERE IDPROVEEDOR = @IDPROVEEDOR AND IDINFORMACIONPROYECTO = @IDINFORMACIONPROYECTO) = @0

   BEGIN
   INSERT INTO TIPO_PROVEEDOR_INFORMACION_PROYECTO(IDPROVEEDOR,IDINFORMACIONPROYECTO,ESTADOTIPOPROVEEDORINFORMACIONPROYECTO)          
      VALUES(@IDPROVEEDOR, @IDINFORMACIONPROYECTO, 1)          
   END
  ----------------------------------------------------INGRESO PROVEEDOR----------------------------------------------------          
          
          
  ----------------------INGRESO SOLICITUDES DE PAGO----------------------          
  INSERT INTO SOLICITUD_PAGO(IDMAESTROESTADOSOLICITUD,          
         FOLIOBOLETAGARANTIASOLICITUDPAGO,          
         NUMEROFAMILIASPAGARSOLICITUDPAGO,          
         USUARIORESPONSABLESOLICITUDPAGO,          
         IDCARACTERISTICASESPECIALES,          
         IDMAESTROTIPOPAGO,          
         FECHAREALCREACIONSOLICITUDPAGO,          
         FECHACREACIONSOLICITUDPAGO,          
         IDMAESTROTIPODESTINOPAGO,          
         MONTOTOTALPROYECTOSOLICITUDPAGO,          
         MONTOPAGADOSOLICITUDPAGO,          
         MONTOCOMPROMETIDOSOLICITUDPAGO,          
         SALDOPORPAGARSOLICITUDPAGO,          
         NUMERORESOLUCIONCONTRATOSOLICITUDPAGO,          
         MONTOSOLICITUDSOLICITUDPAGO,          
         IDPROVEEDOR,          
         NUMEROVIVIENDASSOLICITUDPAGO,          
         OBSERVACIONESSOLICITUDPAGO,      
   FECHARESOLUCIONSOLICITUDPAGO,    
   FECHABOLETAGARANTIASOLICITUDPAGO ,  
   IDMANDANTOPROVEEDORSOLICITUDPAGO   
    )          
  VALUES(@IDMAESTROESTADOSOLICITUD,          
    @FOLIOBOLETAGARANTIASOLICITUDPAGO,          
    @NUMEROFAMILIASPAGARSOLICITUDPAGO,          
    @USUARIORESPONSABLESOLICITUDPAGO,          
    @IDCARACTERISTICASESPECIALES,          
    @IDMAESTROTIPOPAGO,          
    @FECHAREALCREACIONSOLICITUDPAGO,          
    @FECHACREACIONSOLICITUDPAGO,          
    @IDMAESTROTIPODESTINOPAGO,          
    @MONTOTOTALPROYECTOSOLICITUDPAGO,          
    @MONTOPAGADOSOLICITUDPAGO,          
    @MONTOCOMPROMETIDOSOLICITUDPAGO,          
    @SALDOPORPAGARSOLICITUDPAGO,          
    @NUMERORESOLUCIONCONTRATOSOLICITUDPAGO,          
    @MONTOSOLICITUDSOLICITUDPAGO,          
    @IDPROVEEDOR,          
    @NUMEROVIVIENDASSOLICITUDPAGO,          
    @OBSERVACIONESSOLICITUDPAGO,      
 @FECHARESOLUCIONSOLICITUDPAGO,    
 @FECHABOLETAGARANTIA,@IDPROVEEDORMANDATO )          
          
  SET @IDSOLICITUDPAGO = @@IDENTITY          
          
  IF(@@ERROR <> 0)          
  BEGIN          
   ROLLBACK SELECT 0 as ERR,          
       'ERROR AL INGRESAR SOLICITUD DE PAGO' as MSG,          
       @IDSOLICITUDPAGO AS IDSOLICITUDPAGO          
  END          
  ----------------------INGRESO SOLICITUDES DE PAGO----------------------          
          
          
  ------------------------------INGRESO EN INFORMACION_PROYECTO_SOLICITUD------------------------------        
  INSERT INTO INFORMACION_PROYECTO_SOLICITUD(IDINFORMACIONPROYECTO,        
            IDMAESTROPROGRAMA,        
            IDMAESTROLLAMADO,        
            IDMAESTROMODALIDAD,        
            IDMAESTROTITULO,        
            IDMAESTRORESOLUCION,        
            IDDIRECCION,        
            IDMAESTROTIPOLOGIA,        
            IDMAESTROALTERNATIVAPOSTULACION,        
            CODIGOPROYECTOINFORMACIONPROYECTOSOLICITUD,      
            NOMBREPROYECTOINFORMACIONPROYECTOSOLICITUD,        
            FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTOSOLICITUD,        
            RESOLUCIONATINFORMACIONPROYECTOSOLICITUD,        
            FECHARESOLUCIONATINFORMACIONPROYECTOSOLICITUD,        
            CANTIDADVIVIENDASINFORMACIONPROYECTOSOLICITUD,        
            CANTIDADBENEFICIARIOSINFORMACIONPROYECTOSOLICITUD,        
            MONTOSUBSIDIOBASEINFORMACIONPROYECTOSOLICITUD,        
            MONTOSUBSIDIOBASEFACTIBILIDADINFORMACIONPROYECTOSOLICITUD,        
            MARCALOCALIZACIONINFORMACIONPROYECTOSOLICITUD,        
            MARCAFACTIBILIZACIONINFORMACIONPROYECTOSOLICITUD,        
            FECHAFACTIBILIZACIONINFORMACIONPROYECTOSOLICITUD,        
            NUMEROFAMILIASADSCRITASINFORMACIONPROYECTOSOLICITUD,        
            AGNOFACTIBILIDADINFORMACIONPROYECTOSOLICITUD,        
            IDMAESTROESTADOPROYECTO,        
            FECHAINGRESOINFORMACIONPROYECTOSOLICITUD,        
            IDMAESTROESTADOBENEFICIO,        
            IDMAESTROBANCO,        
            AVANCEOBRAINFORMACIONPROYECTOSOLICITUD,        
            ESTADOAVANCEOBRAINFORMACIONPROYECTOSOLICITUD,        
            PERMISOUSUARIOADMINISTRATIVOINFORMACIONPROYECTOSOLICITUD,        
            NOMBREUSUARIOOTORGAPERMISOINFORMACIONPROYECTOSOLICITUD      
   )        
 SELECT DISTINCT          
   IP.IDINFORMACIONPROYECTO,          
   IP.IDMAESTROPROGRAMA,          
   IP.IDMAESTROLLAMADO,          
   IP.IDMAESTROMODALIDAD,          
   IP.IDMAESTROTITULO,          
   IP.IDMAESTRORESOLUCION,          
   IP.IDDIRECCION,          
   MT.IDMAESTROTIPOLOGIA,          
   IP.IDMAESTROALTERNATIVAPOSTULACION,          
   IP.CODIGOPROYECTOINFORMACIONPROYECTO,          
   IP.NOMBREPROYECTOINFORMACIONPROYECTO,          
   IP.FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTO,          
   IP.RESOLUCIONATINFORMACIONPROYECTO,          
   IP.FECHARESOLUCIONATINFORMACIONPROYECTO,          
   IP.CANTIDADVIVIENDASINFORMACIONPROYECTO,          
   IP.CANTIDADBENEFICIARIOSINFORMACIONPROYECTO,          
   IP.MONTOSUBSIDIOBASEINFORMACIONPROYECTO,          
   IP.MONTOSUBSIDIOBASEFACTIBILIDADINFORMACIONPROYECTO,          
   IP.MARCALOCALIZACIONINFORMACIONPROYECTO,          
   IP.MARCAFACTIBILIZACIONINFORMACIONPROYECTO,          
   IP.FECHAFACTIBILIZACIONINFORMACIONPROYECTO,          
   IP.NUMEROFAMILIASADSCRITASINFORMACIONPROYECTO,          
   IP.AGNOFACTIBILIDADINFORMACIONPROYECTO,          
   IP.IDMAESTROESTADOPROYECTO,          
   GETDATE(),          
   IP.IDMAESTROESTADOBENEFICIO,          
   IP.IDMAESTROBANCO,        
   @AVANCEOBRAMUNIN,        
   @ESTADOAVANCEOBRA,        
   IP.PERMISOUSUARIOADMINISTRATIVOINFORMACIONPROYECTO,        
   IP.NOMBREUSUARIOOTORGAPERMISOINFORMACIONPROYECTO        
 FROM INFORMACION_PROYECTO IP INNER JOIN        
   CARACTERISTICAS_ESPECIALES CE ON IP.IDINFORMACIONPROYECTO = CE.IDINFORMACIONPROYECTO INNER JOIN        
   TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA TSPC ON CE.IDCARACTERISTICASESPECIALES = TSPC.IDCARACTERISTICASESPECIALES INNER JOIN        
   SERVICIO_PARCIALIDAD SP ON TSPC.IDSERVICIOPARCIALIDAD = SP.IDSERVICIOPARCIALIDAD INNER JOIN        
   TIPOLOGIA_SERVICIO TS ON SP.IDTIPOLOGIASERVICIO = TS.IDTIPOLOGIASERVICIO INNER JOIN        
   MAESTRO_TIPOLOGIA MT ON TS.IDMAESTROTIPOLOGIA = MT.IDMAESTROTIPOLOGIA        
 WHERE IP.IDINFORMACIONPROYECTO = @IDINFORMACIONPROYECTO        
         
 DECLARE @IDINFORMACIONPROYECTOSOLICITUD bigint        
 SET @IDINFORMACIONPROYECTOSOLICITUD = @@IDENTITY        
  
 IF(@@ERROR <> 0)        
 BEGIN        
  ROLLBACK SELECT 0 as ERR,        
  'ERROR AL INGRESAR INFORMACION CONGELADA' as MSG,        
  @IDSOLICITUDPAGO AS IDSOLICITUDPAGO        
 END          
 ------------------------------INGRESO EN INFORMACION_PROYECTO_SOLICITUD------------------------------        
          
          
  ------------------------ACTUALIZACION SOLICITUD_PAGO------------------------          
  UPDATE SOLICITUD_PAGO          
  SET IDINFORMACIONPROYECTOSOLICITUD = @IDINFORMACIONPROYECTOSOLICITUD          
  WHERE IDSOLICITUDPAGO = @IDSOLICITUDPAGO          
          
  IF(@@ERROR <> 0)          
  BEGIN          
   ROLLBACK SELECT 0 as ERR,          
       'ERROR AL ACTUALIZAR SOLICITUD DE PAGO' as MSG,          
       @IDSOLICITUDPAGO AS IDSOLICITUDPAGO          
  END          
  ------------------------ACTUALIZACION SOLICITUD_PAGO------------------------          
          
            
  --RESULTADO EXITOSO          
  DECLARE @CODIGOPROYECTO int          
  SET @CODIGOPROYECTO = (SELECT CODIGOPROYECTOINFORMACIONPROYECTOSOLICITUD FROM INFORMACION_PROYECTO_SOLICITUD WHERE IDINFORMACIONPROYECTOSOLICITUD = @IDINFORMACIONPROYECTOSOLICITUD)          
          
  DECLARE @NOMBREPROGRAMA varchar(200)          
  SET @NOMBREPROGRAMA = (SELECT NOMBREMAESTROPROGRAMA         
        FROM MAESTRO_PROGRAMA          
        WHERE IDMAESTROPROGRAMA = (SELECT IDMAESTROPROGRAMA FROM INFORMACION_PROYECTO_SOLICITUD WHERE IDINFORMACIONPROYECTOSOLICITUD = @IDINFORMACIONPROYECTOSOLICITUD))          
          
  COMMIT TRAN          
   SELECT 1 AS ERR,          
     'La solicitud de pago fue guardada para el proyecto '+ CONVERT(varchar, @CODIGOPROYECTO) + ', programa: ' + @NOMBREPROGRAMA + ', id de la solicitud: ' + CONVERT(varchar, @IDSOLICITUDPAGO) +'.' AS MSG,          
     @IDSOLICITUDPAGO AS IDSOLICITUDPAGO          
 END        
END

GO
/****** Object:  StoredProcedure [dbo].[INSERTA_MODIFICACION_ESTADO_SOLICITUD]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/********************************************************************************************  
NOMBRE DEL PROCEDIMIENTO 	: [INSERTA_MODIFICACION_ESTADO_SOLICITUD]
FECHA DE CREACIÓN      		: 02/10/2018
USUARIO DE CREACIÓN      	: Daniel Orozco - Anticipa.
VERSIÓN            			: 1.0                            

Visado por DBA              : 
Fecha Aprobación DBA        :  
Comentarios DBA             : 
          
OBJETIVO     				: Cambia estado de Solicitud e ingresa registro del mismo cambio
  
TABLAS      				: MODIFICACION_ESTADO_SOLICITUD
							  					 					  
		
          
  
QUE RETORNA     			: --
  
PARAMETROS     				:@IDSOLICITUDPAGO bigint,
							 @IDMAESTROESTADOSOLICITUD bigint,
							 @USUARIORESPONSABLEMODIFICACIONESTADOSOLICITUD varchar(100)
            
PRUEBA						: 

           
PROYECTO     				: SNAT SIMPLIFICADO
RESPONSABLE     			: DINFO  
							  
********************************************************************************************/  
CREATE PROCEDURE [dbo].[INSERTA_MODIFICACION_ESTADO_SOLICITUD]
	@IDSOLICITUDPAGO bigint,
    @IDMAESTROESTADOSOLICITUD bigint,
    @USUARIORESPONSABLEMODIFICACIONESTADOSOLICITUD varchar(100)
AS
BEGIN
	BEGIN TRAN
	BEGIN
		UPDATE	SOLICITUD_PAGO
		SET IDMAESTROESTADOSOLICITUD = @IDMAESTROESTADOSOLICITUD
		WHERE	IDSOLICITUDPAGO = @IDSOLICITUDPAGO

		IF(@@ERROR <> 0)
		BEGIN
			ROLLBACK SELECT 0 as ERR,'ERROR AL ACTUALIZAR SOLICITUD' as MSG
		END
		
		INSERT INTO MODIFICACION_ESTADO_SOLICITUD(IDSOLICITUDPAGO, IDMAESTROESTADOSOLICITUD, FECHAMODIFICACIONESTADOSOLICITUD, USUARIORESPONSABLEMODIFICACIONESTADOSOLICITUD)
		VALUES(@IDSOLICITUDPAGO, @IDMAESTROESTADOSOLICITUD, GETDATE(), @USUARIORESPONSABLEMODIFICACIONESTADOSOLICITUD)

		IF(@@ERROR <> 0)
		BEGIN
			ROLLBACK SELECT 0 as ERR,'ERROR AL INGRESAR MODIFICACIÓN' as MSG
		END

		COMMIT TRAN
			SELECT 1 as ERR,'Solicitud, eliminada correctamente' as MSG
	END
END



GO
/****** Object:  StoredProcedure [dbo].[INSERTA_PRESUPUESTO_REGIONAL_IGTD]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/********************************************************************************************        
* Nombre del procedimiento      : INSERTA_PRESUPUESTO_REGIONAL_IGTD
* Fecha de creación             : 24/08/2018        
* Usuario de creación           : Anticipa       
* Versión.                      : V.0.0.0.1        

* Visado por DBA                : 
* Fecha Aprobación DBA          : 

* Parametros					: @ANNORESOLUCION
								  @NUMERORESOLUCION
								  @FECHARESOLUCION
							      @CODIGOREGION
								  @MONTOPRESUPUESTOREGIONAL
								  @OBSERVACION
								  @NOMBREARCHIVO
								  @USUARIO
        
* Objetivo                      : Procedimiento que inserta Presupuesto Regional IGTD      
* Prueba						: exec INSERTA_PRESUPUESTO_REGIONAL_IGTD 2019,7,'20180824',1,100,'prueba','test','usuario'                   
**********************************************************************************************/     

CREATE PROCEDURE [dbo].[INSERTA_PRESUPUESTO_REGIONAL_IGTD]        
@ANNORESOLUCION SMALLINT,  
@NUMERORESOLUCION INT,  
@FECHARESOLUCION DATETIME,
@CODIGOREGION INT,
@MONTOPRESUPUESTOREGIONAL BIGINT,
@OBSERVACION VARCHAR(200),
@NOMBREARCHIVO VARCHAR(50),
@USUARIO VARCHAR(50)

AS

BEGIN

/****** Control Errores ***********/    
DECLARE @ERR INT, @MSG VARCHAR(250)    
SET @MSG = 'Presupuesto ingresado correctamente.'    
SET @ERR = 0    
/**********************************/ 

/* DECLARE @IDESTADORESOLUCIONPRESUPUESTARIA BIGINT */
DECLARE @IDMAESTROTIPORESOLUCIONPRESUPUESTARIA BIGINT
DECLARE @IDRESOLUCIONPRESUPUESTARIA BIGINT
DECLARE @IDRESOLUCIONPRESUPUESTARIAANTERIOR BIGINT
DECLARE @IDPRESUPUESTOREGIONAL BIGINT
DECLARE @VIGENTE BIT
DECLARE @NOVIGENTE BIT
DECLARE @NUMVIGENCIA INT
DECLARE @CERO INT

SET @VIGENTE = 1
SET @NOVIGENTE = 0
SET @CERO = 0	

BEGIN TRANSACTION

IF NOT EXISTS (SELECT RP.NUMERORESOLUCIONPRESUPUESTARIA FROM RESOLUCION_PRESUPUESTARIA RP INNER JOIN PRESUPUESTO_REGIONAL PR ON PR.IDRESOLUCIONPRESUPUESTARIA= RP.IDRESOLUCIONPRESUPUESTARIA
WHERE RP.NUMERORESOLUCIONPRESUPUESTARIA = @NUMERORESOLUCION AND PR.CODIGOREGIONPRESUPUESTOREGIONAL = @CODIGOREGION AND RP.ANORESOLUCIONPRESUPUESTARIA = @ANNORESOLUCION)

BEGIN 

IF NOT EXISTS (SELECT RP.NUMERORESOLUCIONPRESUPUESTARIA FROM RESOLUCION_PRESUPUESTARIA RP INNER JOIN PRESUPUESTO_REGIONAL PR ON PR.IDRESOLUCIONPRESUPUESTARIA= RP.IDRESOLUCIONPRESUPUESTARIA
WHERE RP.NUMERORESOLUCIONPRESUPUESTARIA = @NUMERORESOLUCION AND RP.ANORESOLUCIONPRESUPUESTARIA = @ANNORESOLUCION)	
BEGIN 

	BEGIN
		SET @IDRESOLUCIONPRESUPUESTARIAANTERIOR = (SELECT IDRESOLUCIONPRESUPUESTARIA FROM RESOLUCION_PRESUPUESTARIA WHERE ANORESOLUCIONPRESUPUESTARIA = @ANNORESOLUCION AND 
													ESTADORESOLUCIONPRESUPUESTARIA = 1)

			IF (@IDRESOLUCIONPRESUPUESTARIAANTERIOR IS NOT NULL)
			BEGIN
				UPDATE	RESOLUCION_PRESUPUESTARIA
						SET ESTADORESOLUCIONPRESUPUESTARIA = @NOVIGENTE
						WHERE IDRESOLUCIONPRESUPUESTARIA = @IDRESOLUCIONPRESUPUESTARIAANTERIOR AND
						ANORESOLUCIONPRESUPUESTARIA = @ANNORESOLUCION
			END

	END

	INSERT INTO [dbo].[RESOLUCION_PRESUPUESTARIA]
			   ([ESTADORESOLUCIONPRESUPUESTARIA]
			   ,[ANORESOLUCIONPRESUPUESTARIA]
			   ,[NUMERORESOLUCIONPRESUPUESTARIA]
			   ,[FECHARESOLUCIONPRESUPUESTARIA]
			   ,[USUARIORESOLUCIONPRESUPUESTARIA]
			   ,[NOMBREARCHIVOPRESUPUESTO])
		 VALUES
			   (@VIGENTE
			   ,@ANNORESOLUCION
			   ,@NUMERORESOLUCION
			   ,@FECHARESOLUCION
			   ,@USUARIO
			   ,@NOMBREARCHIVO)

				IF @@rowcount <= @CERO    
				 BEGIN    
				  SET @err = -1    
				  SET @msg = 'ERROR EN INSERTAR RESOLUCION_PRESUPUESTARIA.'
				  GOTO error    
				 END 

	SET @IDRESOLUCIONPRESUPUESTARIA = CAST(SCOPE_IDENTITY() AS INT)  	

END

BEGIN

	IF (@IDRESOLUCIONPRESUPUESTARIA IS NULL)
	BEGIN
		SET @IDRESOLUCIONPRESUPUESTARIA = (SELECT MAX(IDRESOLUCIONPRESUPUESTARIA) FROM RESOLUCION_PRESUPUESTARIA)
	END

	INSERT INTO [dbo].[PRESUPUESTO_REGIONAL]
			   ([IDRESOLUCIONPRESUPUESTARIA]
			   ,[CODIGOREGIONPRESUPUESTOREGIONAL]
			   ,[MONTOPRESUPUESTOREGIONAL]
			   ,[USUARIOPRESUPUESTOREGIONAL]
			   ,[FECHAPRESUPUESTOREGIONAL])
		 VALUES
			   (@IDRESOLUCIONPRESUPUESTARIA
			   ,@CODIGOREGION
			   ,@MONTOPRESUPUESTOREGIONAL
			   ,@USUARIO
			   ,@FECHARESOLUCION)

				IF @@rowcount <= @CERO    
				 BEGIN    
				  SET @err = -1    
				  SET @msg = 'ERROR EN INSERTAR PRESUPUESTO_REGIONAL.'
				  GOTO error    
				 END 
END


END

ELSE

BEGIN    
	SET @MSG = 'Presupuesto ' +CAST(@NUMERORESOLUCION as varchar(6))  + ' ya fue ingresado previamente. '    
	SET @ERR = -2       
	GOTO error    
END

END

SET NOCOUNT OFF  
COMMIT TRANSACTION  
 SELECT @ERR AS err, @MSG AS MSG  
 RETURN  
ERROR: ROLLBACK  TRANSACTION  
    SELECT @ERR AS err, @MSG AS MSG


GO
/****** Object:  StoredProcedure [dbo].[INSERTA_PROYECTO_SNAT]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
      
/********************************************************************************************                                          
NOMBRE DEL PROCEDIMIENTO  : [INSERTA_PROYECTO_SNAT]                                        
FECHA DE CREACIÓN        : 24/01/2019                                        
USUARIO DE CREACIÓN       : CFAJARDO                                        
VERSIÓN               : 1.0                                                                    
                                        
Visado por DBA              : Iván Frade Cortés                                        
Fecha Aprobación DBA        : 03072019                                         
Comentarios DBA             :  ---                                       
                                                  
OBJETIVO         : Inserta en SNAT Simplificado los proyectos obtenidos desde Rukan DS49,DA105,DS174                                        
                                          
TABLAS          : DIRECCION                                        
         PROVEEDOR                                        
         INFORMACION_PROYECTO                                        
                                                  
                                          
QUE RETORNA        : --                                        
                                          
PARAMETROS         :    @CODIGOPROYECTO INT NULL,                                              
         @NOMBREPROYECTO VARCHAR NULL,                                        
         @ESTADOPROYECTO VARCHAR NULL,                                        
         @IDREGION INT NULL,                                        
         @IDPROVINCIA INT NULL,                                        
         @IDCOMUNA INT NULL,                                        
         @PROGRAMA INT NULL,                                        
         @TIPOLOGIA INT NULL,                                        
         @TIPOBANCO VARCHAR NULL,                                        
         @TIPOPROYECTO VARCHAR NULL,                                        
         @MODALIDAD VARCHAR NULL,                                        
         @VIVIENDASTOTALPROYECTO INT NULL,                                        
         @FAMILIASDS49PROYECTO INT NULL,                                        
         @NUMEROFAMILIASADSCRITAS INT NULL,                                        
         @ROLENTIDAD INT NULL,                                        
         @RUTENTIDAD INT NULL,                                        
         @DIGITOENTIDAD VARCHAR NULL,                                        
         @NOMBREENTIDAD VARCHAR NULL,                                        
         @MONTOSUBSIDIOBASE DECIMAL NULL,                                        
         @MONTOSUBSIDIO DECIMAL NULL,                                        
         @MARCALOCALIZACION BIT NULL,                                        
         @MARCAFACTIBILIZACION BIT NULL,                                        
         @MONTOTOTALSUBSIDIOUF DECIMAL NULL,                                        
         @MARCAASOCIACIONGRUPO BIT NULL,                                        
         @NOMBREEMPRESACONSTRUCTORA VARCHAR NULL,                                             
         @RUTEMPRESACONSTRUCTORA INT NULL,                                        
         @DIGITOEMPRESACONSTRUCTORA CHAR NULL,                                        
         @FECHACALIFICACIONDIFINITIVA DATE NULL,                                              
         @MONTOSUBSIDIOBASECONFACTIBILIZACION DATE NULL,                                          
         @FECHACPI DATE NULL,                                        
         @TIPOENTIDADPATROCINANTE INT NULL,                                        
         @ANOCPI INT NULL,                                        
         @FECHAFACTIBILIDADPROYECTO DATE NULL,                                        
         @AGNOFACTIBILIDADINFORMACIONPROYECTO INT NULL,                                        
         @INDIVIDUALGRUPAL VARCHAR           
                                                    
PRUEBA      :                 
                                        
                                                   
PROYECTO         : SNAT SIMPLIFICADO                                        
RESPONSABLE        : DINFO                                          
                                      
********************************************************************************************/                                          
CREATE PROCEDURE [dbo].[INSERTA_PROYECTO_SNAT]                                        
                           
@CODIGOPROYECTO INT NULL,                                              
@NOMBREPROYECTO VARCHAR (200) null,                    
@ESTADOPROYECTO VARCHAR (200) null,                                        
@IDREGION INT NULL,                                        
@IDPROVINCIA INT NULL,                                        
@IDCOMUNA INT NULL,                                        
@PROGRAMA VARCHAR (200) null,                                         
@TIPOLOGIA INT NULL,                                        
@TIPOBANCO VARCHAR (200) null,                                        
@TIPOPROYECTO VARCHAR (200) null,                                        
@MODALIDAD VARCHAR (200) null,                                        
@VIVIENDASTOTALPROYECTO INT NULL,                                        
@FAMILIASDS49PROYECTO INT NULL,                                        
@NUMEROFAMILIASADSCRITAS INT NULL,                                        
@ROLENTIDAD INT NULL,                                        
@RUTENTIDAD INT NULL,                                        
@DIGITOENTIDAD VARCHAR  null,                                        
@NOMBREENTIDAD VARCHAR (200) null,                                        
@MONTOSUBSIDIOBASE DECIMAL NULL,                                        
@MONTOSUBSIDIO DECIMAL NULL,                               
@MARCALOCALIZACION BIT NULL,                                        
@MARCAFACTIBILIZACION BIT NULL,                                        
@MONTOTOTALSUBSIDIOUF DECIMAL NULL,                                        
@MARCAASOCIACIONGRUPO BIT NULL,                                        
@NOMBREEMPRESACONSTRUCTORA VARCHAR (200) null,                                             
@RUTEMPRESACONSTRUCTORA INT NULL,                                        
@DIGITOEMPRESACONSTRUCTORA VARCHAR null,                                        
@FECHACALIFICACIONDIFINITIVA DATE NULL,                                              
@MONTOSUBSIDIOBASECONFACTIBILIZACION DECIMAL NULL,                                          
@FECHACPI DATE NULL,                                        
@TIPOENTIDADPATROCINANTE INT NULL,                                        
@ANOCPI INT NULL,                                        
@FECHAFACTIBILIDADPROYECTO DATE NULL,                                        
@AGNOFACTIBILIDADINFORMACIONPROYECTO INT NULL,                                        
@INDIVIDUALGRUPAL VARCHAR  (200) null,                                     
@SUBMODALIDAD VARCHAR  (200) null                      
AS                                        
------------------------------------------------------------INICIO_DECLARAR_VARIABLES------------------------------------------------------------                                        
BEGIN                                         
    set dateformat dmy                                  
    DECLARE @PROGRAMA_DS49 bigint                                        
    DECLARE @PROGRAMA_DS174 bigint                                        
    DECLARE @PROGRAMA_DS49res_1875 bigint                                        
    DECLARE @PROGRAMA_ELECCION bigint                                        
    DECLARE @ID_RUKAN_PROGRAMA_DS49 bigint                                        
    DECLARE @ID_RUKAN_PROGRAMA_DS174 bigint                      
    DECLARE @ID_RUKAN_PROGRAMA_DS49res_1875 bigint                                        
    DECLARE @IDINFORMACIONPROYECTO BIGINT                                        
    DECLARE @IDDIRECCION BIGINT                                        
    DECLARE @IDMAESTROLLAMADO BIGINT                                        
    DECLARE @IDMAESTROALTERNATIVAPOSTULACION BIGINT                                      
    DECLARE @IDMAESTROPROGRAMA BIGINT           
    DECLARE @IDMAESTRORESOLUCION BIGINT                                        
    DECLARE @IDMAESTROESTADOPROYECTO BIGINT                                        
    DECLARE @IDMAESTROESTADOBENEFICIO BIGINT                                        
    DECLARE @IDMAESTROBANCO BIGINT                                        
    DECLARE @IDMAESTROMODALIDAD BIGINT                                        
    DECLARE @IDCARACTERISTICASESPECIALES BIGINT                                        
    DECLARE @IDMAESTROTIPOLOGIA INT                           
    DECLARE @SCOPE_IDENTITY INT                                        
    DECLARE @PROYECTOEXISTENTE INT                                        
    DECLARE @NUMPTLLA INT                                        
    DECLARE @SININFORMACION VARCHAR (200)               
    DECLARE @IDMAESTROSUBMODALIDAD INT                        
    DECLARE @REGULAR VARCHAR (200)                                    
    DECLARE @RECONSTRUCCION VARCHAR (200)                         
    DECLARE @Aca   VARCHAR (200)                         
    DECLARE @NULL VARCHAR (200)                         
    DECLARE @PagoPorAvance VARCHAR (200)                         
    DECLARE @ViviendaTipo VARCHAR (200)                         
                                            
    DECLARE @174 VARCHAR (200)                                         
    DECLARE @BANCO_PROYECTO VARCHAR (200)                                             
    DECLARE @BANCO_POSTULACION VARCHAR  (200)                                               
    DECLARE @CONSTRUCCION_DE_VIVIENDAS_EN_SITIO_PROPIO VARCHAR (200)                                       
    DECLARE @GRUPAL VARCHAR (200)                                       
    DECLARE @CONSTRUCCION_EN_NUEVOS_TERRENOS VARCHAR (200)                                        
    DECLARE @CONSTRUCCION_SITIO_PROPIO VARCHAR (200)                                       
    DECLARE @INDIVIDUAL VARCHAR (200)                                       
    DECLARE @DESINFICACION_PREDIAL VARCHAR (200)                                       
    DECLARE @MEGAPROYECTO VARCHAR (200)                                       
    DECLARE @PEQUEÑOS_CONDOMINIOS VARCHAR (200)
	DECLARE @IDSERVPARC BigInt  	                                        
                                             
    DECLARE @0 INT                                        
    DECLARE @1 INT                                        
    DECLARE @2 INT                                        
    DECLARE @3 INT                                        
    DECLARE @4 INT                                        
    DECLARE @5 INT                             
    DECLARE @6 INT                                        
    DECLARE @7 INT                                        
    DECLARE @8 INT                                        
    DECLARE @9 INT                                        
    DECLARE @10 INT                                        
    DECLARE @11 INT                                        
    DECLARE @12 INT                                        
    DECLARE @13 INT                                         
    DECLARE @14 INT                       
    DECLARE @15 INT                                       
    DECLARE @16 INT                     
    DECLARE @17 INT                    
    DECLARE @18 INT                    
    DECLARE @19 INT                    
    DECLARE @20 INT                    
    DECLARE @21 INT                    
    DECLARE @22 INT                    
    DECLARE @23 INT                    
    DECLARE @24 INT                    
    DECLARE @25 INT                    
    DECLARE @26 INT                    
    DECLARE @27 INT                    
    DECLARE @28 INT                    
    DECLARE @29 INT                    
    DECLARE @30 INT                    
    DECLARE @31 INT                    
    DECLARE @32 INT                    
    DECLARE @33 INT                    
    DECLARE @34 INT                    
    DECLARE @35 INT                    
    DECLARE @36 INT                    
    DECLARE @37 INT
	
	-- Declaración de tablas
    DECLARE @SERVICIOPARCIALIDAD TABLE (IDSERVPARC BigInt) 	                    
                               
    SET @PROGRAMA_DS49 = 3                                        
    SET @PROGRAMA_DS174 = 2           
    SET @PROGRAMA_DS49res_1875 = 8                                        
    SET @ID_RUKAN_PROGRAMA_DS49 = 0                                        
    SET @ID_RUKAN_PROGRAMA_DS174 = 0                                        
    SET @ID_RUKAN_PROGRAMA_DS49res_1875 = 0                                        
                                       SET @IDMAESTROLLAMADO = NULL                                       
    SET @IDINFORMACIONPROYECTO = NULL                                        
    SET @IDDIRECCION = NULL                                         
    SET @IDMAESTROALTERNATIVAPOSTULACION = NULL                                        
    SET @IDMAESTROPROGRAMA  = NULL                              
    SET @IDMAESTRORESOLUCION  = NULL                                        
    SET @IDMAESTROESTADOPROYECTO   = NULL                                        
    SET @IDMAESTROESTADOBENEFICIO  = NULL                                        
    SET @IDMAESTROBANCO  = NULL                                        
    SET @IDMAESTROMODALIDAD = NULL                                        
    SET @IDCARACTERISTICASESPECIALES = NULL                                        
    SET @IDMAESTROTIPOLOGIA = NULL                                        
    SET @SCOPE_IDENTITY = NULL                                        
    SET @PROYECTOEXISTENTE = 0                                        
    SET @NUMPTLLA = NULL                                        
    SET @SININFORMACION = 'SIN INFORMACIÓN'                           
    SET @IDMAESTROSUBMODALIDAD = 1                        
    SET @REGULAR = 'Regular'                       
    SET @RECONSTRUCCION = 'Reconstruccion'                               
    set @Aca = 'Aca'                
    set @NULL = 'NULL'                
    set @PagoPorAvance = 'PagoPorAvance'                
    set @ViviendaTipo = 'ViviendaTipo'                
                          
                         
    SET @174 = '174'                                        
    SET @BANCO_PROYECTO = 'BancoProyecto'                                        
    SET @BANCO_POSTULACION = 'BancoPostulacion'                                        
    SET @CONSTRUCCION_DE_VIVIENDAS_EN_SITIO_PROPIO= 'Construcción de Viviendas en Sitio Propio'                                        
    SET @GRUPAL = 'Grupal'                                     
    SET @CONSTRUCCION_EN_NUEVOS_TERRENOS = 'Construcción en Nuevos Terrenos'                                        
    SET @CONSTRUCCION_SITIO_PROPIO = 'Construcción Sitio Propio'                                        
    SET @INDIVIDUAL = 'Individual'                                        
    SET @DESINFICACION_PREDIAL = 'Densificación Predial'                                        
    SET @MEGAPROYECTO = 'Mega Proyecto'                                        
    SET @PEQUEÑOS_CONDOMINIOS = 'Pequeños Condominios'                                        
                                           
    SET @0 = 0                                   
    SET @1 = 1                                        
    SET @2 = 2                                        
    SET @3 = 3                                        
    SET @4 = 4                                        
    SET @5 = 5                                        
    SET @6 = 6        
    SET @7 = 7                          
    SET @8 = 8                                        
    SET @9 = 9                                        
    SET @10 = 10                                        
    SET @11 = 11                                        
    SET @12 = 12                                        
    SET @13 = 13                                        
    SET @14 = 14                      
    SET @15 = 15                                      
    SET @16 = 16                     
    SET @17 = 17                                       
    SET @18 = 18                                        
    SET @19 = 19                                        
    SET @20 = 20                    
    SET @21 = 21                    
    SET @22 = 22                    
    SET @23 = 23                    
    SET @24 = 24                    
    SET @25 = 25                    
    SET @26 = 26                    
    SET @27 = 27                  
    SET @28 = 28                    
    SET @29 = 29                    
    SET @30 = 30                    
    SET @31 = 31                    
    SET @32 = 32                    
    SET @33 = 33                    
    SET @34 = 34                    
    SET @35 = 35                    
    SET @36 = 36                    
    SET @37 = 37                                        
END                                         
------------------------------------------------------------FIN_DECLARAR_VARIABLES------------------------------------------------------------                                        
BEGIN 
    BEGIN TRAN   
    -----------------------------------1) INICIO AGRUPACION_DE_PROYECTOS - TIPOLOGIA , MODALIDAD, SUB MODALIDAD Y PROGRAMA, LLAMADO----------------------------------------------------------------                                          
    BEGIN                         
        -- SUB MODALIDAD                
        IF( UPPER(LTRIM(RTRIM(@SUBMODALIDAD))) = UPPER(LTRIM(RTRIM(@Aca))))                      
        BEGIN                      
            SET @IDMAESTROSUBMODALIDAD = @2                      
        END                      
        ELSE IF(UPPER(LTRIM(RTRIM(@SUBMODALIDAD))) = UPPER(LTRIM(RTRIM(@PagoPorAvance))))                      
        BEGIN                      
            SET @IDMAESTROSUBMODALIDAD = @3                    
        END                  
        ELSE IF(UPPER(LTRIM(RTRIM(@SUBMODALIDAD))) = UPPER(LTRIM(RTRIM(@ViviendaTipo))))                      
        BEGIN                      
            SET @IDMAESTROSUBMODALIDAD = @4                    
        END                  
        ELSE                
        BEGIN                
            SET @IDMAESTROSUBMODALIDAD = @1                
        END                
                
        -- LLAMADO                
        IF( UPPER(LTRIM(RTRIM(@TIPOPROYECTO))) = UPPER(LTRIM(RTRIM(@REGULAR))))                      
        BEGIN                      
            SET @IDMAESTROLLAMADO = @1                      
        END                   
        ELSE IF(UPPER(LTRIM(RTRIM(@TIPOPROYECTO))) = UPPER(LTRIM(RTRIM(@RECONSTRUCCION))))                      
        BEGIN                      
            SET @IDMAESTROLLAMADO = @2                      
        END                      
                                        
        -- INICIO_FILTRO_PROGRAMA                                        
        IF(@TIPOBANCO = @174)                                        
        BEGIN                                  
            SET @IDMAESTROPROGRAMA = @2                      
        END                                        
        ELSE IF(@TIPOBANCO = @BANCO_PROYECTO )                                        
        BEGIN                                        
            SET @IDMAESTROPROGRAMA = @3                                        
        END                      
        ELSE IF(@TIPOBANCO = @BANCO_POSTULACION )                             
        BEGIN                                        
            SET @IDMAESTROPROGRAMA = @8                         
        END                                        
        -- FIN_FILTRO_PROGRAMA                                        
                                 
        -- INICIO_FILTRO_TIPOLOGIA_Y_MODALIDAD                                        
        IF(@IDMAESTROPROGRAMA = @2 ) --DS174                                        
        BEGIN                                        
            IF(UPPER(LTRIM(RTRIM(@MODALIDAD))) = UPPER(LTRIM(RTRIM(@CONSTRUCCION_DE_VIVIENDAS_EN_SITIO_PROPIO)))) --En SNAT : Construcción En Sitio De Propiedad De Los Residentes                                        
            BEGIN                       
                SET @IDMAESTROTIPOLOGIA = @18                    
            END               
            IF(@MODALIDAD = @CONSTRUCCION_EN_NUEVOS_TERRENOS) --En SNAT : Construcción De Nuevos Terrenos                                        
            BEGIN                                        
                SET @IDMAESTROTIPOLOGIA = @8                     
            END                                        
            -- SE INGRESA MODALIDAD Construcción DEBIDO A SU TIPOLOGIA                                        
            SET @IDMAESTROMODALIDAD = @2                                        
        END                                        
      
        IF(@IDMAESTROPROGRAMA = @3 ) --DS49                                        
        BEGIN                                        
            IF(@MODALIDAD = @CONSTRUCCION_EN_NUEVOS_TERRENOS) --En SNAT : Construcción De Nuevos Terrenos                                        
            BEGIN                                        
                SET @IDMAESTROTIPOLOGIA = @8                     
            END                                        
            IF(@MODALIDAD =@CONSTRUCCION_SITIO_PROPIO and @INDIVIDUALGRUPAL =@INDIVIDUAL) --En SNAT : Construcción En Sitio Del Residente                                        
            BEGIN                                        
                SET @IDMAESTROTIPOLOGIA = @13                     
            END                                        
            IF(@MODALIDAD = @CONSTRUCCION_SITIO_PROPIO and @INDIVIDUALGRUPAL =@GRUPAL) --En SNAT : Construcción En Sitio Del Residente                                        
            BEGIN                                        
                SET @IDMAESTROTIPOLOGIA = @14                    
            END                                        
            IF(@MODALIDAD = @DESINFICACION_PREDIAL and @INDIVIDUALGRUPAL =@GRUPAL ) -- En SNAT : Densificación Predial (Proyectos Colectivos)                                         
            BEGIN                                        
                SET @IDMAESTROTIPOLOGIA = @16                     
            END                 
            IF(@MODALIDAD = @DESINFICACION_PREDIAL and @INDIVIDUALGRUPAL =@INDIVIDUAL ) -- En SNAT : Densificación Predial (Proyectos individual)              
            BEGIN                                        
                SET @IDMAESTROTIPOLOGIA = @15                     
            END               
            IF(@MODALIDAD = @MEGAPROYECTO)              
            BEGIN                                        
                SET @IDMAESTROTIPOLOGIA = @11                     
            END               
            -- SE INGRESA MODALIDAD Construcción DEBIDO A SU TIPOLOGIA                                        
            SET @IDMAESTROMODALIDAD = @2                                        
        END                                             
                                          
        IF(@IDMAESTROPROGRAMA = @8 ) --DS105                                        
        BEGIN                                        
            IF(@MODALIDAD =@MEGAPROYECTO) --En SNAT : Construcción De Nuevos Terrenos                                        
            BEGIN      
                SET @IDMAESTROTIPOLOGIA = @11                     
            END                                        
            IF(@MODALIDAD = @CONSTRUCCION_SITIO_PROPIO)  --En SNAT : Construcción En Sitio Del Residente                                        
            BEGIN                                        
                SET @IDMAESTROTIPOLOGIA = @9                    
            END                                        
            IF(@MODALIDAD = @DESINFICACION_PREDIAL ) -- En SNAT : Densificación Predial                                         
            BEGIN                                        
                SET @IDMAESTROTIPOLOGIA = @10                    
            END           
            IF(@MODALIDAD = @PEQUEÑOS_CONDOMINIOS) -- En SNAT : Densificación Predial                                         
            BEGIN                                        
                SET @IDMAESTROTIPOLOGIA = @12                     
            END               
            IF(@MODALIDAD = @CONSTRUCCION_EN_NUEVOS_TERRENOS) -- En SNAT : Densificación Predial                                         
            BEGIN                                        
                SET @IDMAESTROTIPOLOGIA = @8                    
            END                                        
            -- SE INGRESA MODALIDAD Construcción DEBIDO A SU TIPOLOGIA                                        
			SET @IDMAESTROMODALIDAD = @2                                       
        END                                                                                                                                                                                                 
    END   

  ----------------------------------------------------------FIN AGRUPACION_DE_PROYECTOS - TIPOLOGIA , MODALIDAD Y PROGRAMA----------------------------------------------------------------                                          
                                          
  ----------------------------------------------------------2) INICIO_IDDIRECCION----------------------------------------------------------                                          
    BEGIN                                        
        --1) VERIFICAMOS EXISTENCIA DE PROYECTO                                        
	    IF(EXISTS(SELECT 1 FROM INFORMACION_PROYECTO WHERE CODIGOPROYECTOINFORMACIONPROYECTO = @CODIGOPROYECTO AND IDMAESTROPROGRAMA = @IDMAESTROPROGRAMA))                                      
        BEGIN                                 
		    SET @PROYECTOEXISTENTE = @1                    
	    END                                     
        --2) SI EL PROYECTO EXISTE LO ACTUALIZAMOS                                        
	    IF(@PROYECTOEXISTENTE = @1 )                                           
	    BEGIN                                        
            -- A) ACTUALIZAMOS DIRECCIÓN                                        
		    SET @IDDIRECCION =  (select IDDIRECCION FROM INFORMACION_PROYECTO WHERE CODIGOPROYECTOINFORMACIONPROYECTO = @CODIGOPROYECTO AND IDMAESTROPROGRAMA = @IDMAESTROPROGRAMA)                                     
                                              
		    UPDATE [DIRECCION]                                        
		       SET [CODIGOCOMUNADIRECCION] =  @IDCOMUNA                                        
			      ,[CODIGOPROVINCIADIRECCION] = @IDPROVINCIA                                       
			      ,[CODIGOREGIONDIRECCION] = @IDREGION                                     
		    WHERE IDDIRECCION = @IDDIRECCION                                        
                              
                                        
		    IF(@@ERROR <> 0)                                          
		    BEGIN                                          
			    ROLLBACK SELECT 'ERROR AL ACTUALIZAR DIRECCION PARA EL PROYECTO' + @CODIGOPROYECTO;                                        
		    END                           
	    END                                        
	    ELSE                                     
	    BEGIN                                        
	        INSERT INTO [dbo].[DIRECCION]                                        
			    	([NUMERODIRECCION]                                        
				    ,[CODIGOCOMUNADIRECCION]                                        
				    ,[CODIGOPROVINCIADIRECCION]                                        
				    ,[CODIGOREGIONDIRECCION]                                        
				    ,[DESCRIPCIONDIRECCION])                                        
	        VALUES                                        
				    (@SININFORMACION                                        
				    ,@IDCOMUNA                                        
				    ,@IDPROVINCIA                                        
				    ,@IDREGION                                        
				    ,@SININFORMACION)                                
		    SET @IDDIRECCION = @@IDENTITY                           
	    END                                        
	    
		IF(@@ERROR <> 0)                                          
	    BEGIN                                          
	        ROLLBACK SELECT 'ERROR AL INSERTAR DIRECCION PARA EL PROYECTO' + @CODIGOPROYECTO;                                        
	    END                                                                               
    END                                          
    ----------------------------------------------------------FIN_IDDIRECCION----------------------------------------------------------                                          
                                          
    ----------------------------------------------------------3) INICIO_IDMAESTROESTADOPROYECTO--------------------------------------------------------------                                          
	BEGIN                                        
		IF EXISTS(SELECT 1 FROM MAESTRO_ESTADO_PROYECTO WHERE UPPER(LTRIM(RTRIM(NOMBREMAESTROESTADOPROYECTO))) = UPPER(LTRIM(RTRIM(@ESTADOPROYECTO))))                                          
		BEGIN                                          
			SET @IDMAESTROESTADOPROYECTO = (SELECT IDMAESTROESTADOPROYECTO                                          
		         FROM MAESTRO_ESTADO_PROYECTO                                          
		         WHERE UPPER(LTRIM(RTRIM(NOMBREMAESTROESTADOPROYECTO))) = UPPER(LTRIM(RTRIM(@ESTADOPROYECTO))))                                          
		                                        
			IF(@@ERROR <> 0)                                          
			BEGIN                                          
				ROLLBACK SELECT 'ERROR AL OBTENER ESTADO PROYECTO'                                          
			END                                          
		END                                          
		ELSE                                          
		BEGIN                                        
			INSERT INTO MAESTRO_ESTADO_PROYECTO(NOMBREMAESTROESTADOPROYECTO,ESTADOMAESTROESTADOPROYECTO)                                          
			VALUES(LTRIM(RTRIM(@ESTADOPROYECTO)),1)                                          
		                                        
			SET @IDMAESTROESTADOPROYECTO = @@IDENTITY                                          
		                                        
			IF(@@ERROR <> 0)                        
			BEGIN                                          
				ROLLBACK SELECT 'ERROR AL INSERTAR ESTADO PROYECTO'                                         
			END                                          
		END                                          
	END                                          
--  ----------------------------------------------------------FIN_IDMAESTROESTADOPROYECTO--------------------------------------------------------------                                          
                                          
--  ----------------------------------------------------------4) INICIO_IDMAESTROBANCO------------------------------------------------------                                          
	BEGIN                                        
		SET @IDMAESTROBANCO =  NULL                                         
		--IF EXISTS(SELECT 1 FROM MAESTRO_BANCO WHERE UPPER(LTRIM(RTRIM(NOMBREMAESTROBANCO))) = UPPER(LTRIM(RTRIM(@))))                               
		--BEGIN                                          
		-- SET @IDMAESTROBANCO = (SELECT IDMAESTROBANCO                                          
		--       FROM MAESTRO_BANCO                          
		--       WHERE UPPER(LTRIM(RTRIM(NOMBREMAESTROBANCO))) = UPPER(LTRIM(RTRIM(@NOMBREBANCO))))                                          
		--END                                          
		--ELSE                                          
		--BEGIN                                          
		-- INSERT INTO MAESTRO_BANCO(NOMBREMAESTROBANCO,ESTADOMAESTROBANCO)                                          
		-- VALUES(LTRIM(RTRIM(@NOMBREBANCO)),1)                                          
		                                        
		-- SET @IDMAESTROBANCO = @@IDENTITY                                          
		                                        
		-- IF(@@ERROR <> 0)                                          
		-- BEGIN                                          
		--  ROLLBACK SELECT 'ERROR EN INSERTAR BANCO'                                          
		-- END                                          
		--END                                          
	END                                        
--  ----------------------------------------------------------FIN_IDMAESTROMODALIDAD------------------------------------------------------                                          
                                          
--  ----------------------------------------------------------5) INICIO_ACTUALIZAR_PROYECTO--------------------------------------------------                                            
	BEGIN                                        
	--VERIFICA SI PROYECTO EXISTE Y MODIFICAR DATOS SI ASI ES, SI NO INSERTA NUEVO                                          
		IF(EXISTS(SELECT 1 FROM INFORMACION_PROYECTO WHERE CODIGOPROYECTOINFORMACIONPROYECTO = @CODIGOPROYECTO AND IDMAESTROPROGRAMA = @IDMAESTROPROGRAMA))                                          
		BEGIN                                        
			SET @PROYECTOEXISTENTE = @1                    
                                          
			SELECT	@IDINFORMACIONPROYECTO = IDINFORMACIONPROYECTO                                   
			FROM	INFORMACION_PROYECTO                                          
			WHERE	CODIGOPROYECTOINFORMACIONPROYECTO = @CODIGOPROYECTO                                          
			 AND	IDMAESTROPROGRAMA = @IDMAESTROPROGRAMA  
			 
			UPDATE	INFORMACION_PROYECTO                                          
			SET	IDDIRECCION = @IDDIRECCION,                                          
				IDMAESTROMODALIDAD = @IDMAESTROMODALIDAD,                                          
				IDMAESTROALTERNATIVAPOSTULACION = @IDMAESTROALTERNATIVAPOSTULACION,                                          
				NOMBREPROYECTOINFORMACIONPROYECTO = @NOMBREPROYECTO,                                          
				FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTO = @FECHACALIFICACIONDIFINITIVA,                                          
				CANTIDADVIVIENDASINFORMACIONPROYECTO = @VIVIENDASTOTALPROYECTO,                                          
				CANTIDADBENEFICIARIOSINFORMACIONPROYECTO = @FAMILIASDS49PROYECTO,                                          
				MONTOSUBSIDIOBASEINFORMACIONPROYECTO = @MONTOSUBSIDIOBASE,                               
				MONTOSUBSIDIOBASEFACTIBILIDADINFORMACIONPROYECTO = @MONTOSUBSIDIOBASECONFACTIBILIZACION,                                        
				MARCALOCALIZACIONINFORMACIONPROYECTO = @MARCALOCALIZACION,                                          
				MARCAFACTIBILIZACIONINFORMACIONPROYECTO =  @MARCAFACTIBILIZACION,                                          
				FECHAFACTIBILIZACIONINFORMACIONPROYECTO = NULL,         
				NUMEROFAMILIASADSCRITASINFORMACIONPROYECTO = @NUMEROFAMILIASADSCRITAS,                                          
				AGNOFACTIBILIDADINFORMACIONPROYECTO = @AGNOFACTIBILIDADINFORMACIONPROYECTO,                                          
				IDMAESTROESTADOPROYECTO = @IDMAESTROESTADOPROYECTO,                                          
				IDMAESTROESTADOBENEFICIO = @IDMAESTROESTADOBENEFICIO,                                          
				IDMAESTROBANCO = @IDMAESTROBANCO,                                        
				IDMAESTROLLAMADO = @IDMAESTROLLAMADO                                        
			WHERE IDINFORMACIONPROYECTO = @IDINFORMACIONPROYECTO                                          
                                             
			IF(@@ERROR <> 0)              
			BEGIN                                          
				ROLLBACK SELECT 'ERROR AL ACTUALIZAR PROYECTO' + @CODIGOPROYECTO;                                          
			END                                        
		END                                        
	END                                          
   ---------------------------------------------------------FIN_ACTUALIZAR_PROYECTO--------------------------------------------------                                        
                                          
  ----------------------------------------------------------6) INICIO_ACTUALIZAR_CARACTERISTICAS_ESPECIALES--------------------------------------------------  
                                            
	if(@PROYECTOEXISTENTE != 0)
	BEGIN
                               
		UPDATE CARACTERISTICAS_ESPECIALES                                          
		SET IDMAESTROMODALIDAD = @IDMAESTROMODALIDAD,                                         
			IDMAESTROSUBMODALIDAD= @IDMAESTROSUBMODALIDAD      
		WHERE IDINFORMACIONPROYECTO = @IDINFORMACIONPROYECTO                                          
                                         
		SELECT @IDCARACTERISTICASESPECIALES = IDCARACTERISTICASESPECIALES                                          
		FROM CARACTERISTICAS_ESPECIALES                                          
		WHERE IDINFORMACIONPROYECTO = @IDINFORMACIONPROYECTO                                          
                                         
		IF(@@ERROR <> 0)                                          
		BEGIN                                          
			ROLLBACK SELECT 'ERROR AL INSERTAR CARACTERISTICAS_ESPECIALES'                                          
		END
--=================== MMIOLA

    --  SELECT @IDMAESTROPROGRAMA AS PROGRAMA, @MODALIDAD AS MODALIDAD, @IDMAESTROTIPOLOGIA AS TIPOLOGIA, @IDMAESTROSUBMODALIDAD AS SUBMODALIDAD, @IDSERVPARC, @IDCARACTERISTICASESPECIALES	
    -- INSERTAMOS PLANTILLAS                 
        IF(@IDMAESTROPROGRAMA = @2 and @IDMAESTROTIPOLOGIA = @18)                     
        begin          
            SET @NUMPTLLA = @32          
        end                 
        ELSE IF(@IDMAESTROPROGRAMA != @8)                                           
        BEGIN                                                          
            SET @NUMPTLLA = (SELECT DISTINCT sp.NUMEROPLANTILLASERVICIOPARCIALIDAD                                          
            FROM SERVICIO_PARCIALIDAD sp                                          
            INNER JOIN TIPOLOGIA_SERVICIO ts  ON sp.IDTIPOLOGIASERVICIO = ts.IDTIPOLOGIASERVICIO                      
            INNER JOIN MAESTRO_TIPOLOGIA mt   ON ts.IDMAESTROTIPOLOGIA = mt.IDMAESTROTIPOLOGIA                                          
            INNER JOIN MAESTRO_SERVICIO ms    ON ts.IDMAESTROSERVICIO = ms.IDMAESTROSERVICIO                             
            INNER JOIN SUB_MODALIDAD_PARCIALIDAD smp ON sp.IDSUBMODALIDADPARCIALIDAD = smp.IDSUBMODALIDADPARCIALIDAD                                          
            INNER JOIN MAESTRO_SUB_MODALIDAD msm     ON smp.IDMAESTROSUBMODALIDAD = msm.IDMAESTROSUBMODALIDAD                                          
            INNER JOIN MAESTRO_PARCIALIDAD mp        ON smp.IDMAESTROPARCIALIDAD = mp.IDMAESTROPARCIALIDAD                                          
            WHERE sp.IDMAESTROPROGRAMA = @IDMAESTROPROGRAMA                                          
              AND ts.IDMAESTROTIPOLOGIA = @IDMAESTROTIPOLOGIA)                                          
        end                                     
        ELSE IF(@IDMAESTROPROGRAMA = @8)                         
        BEGIN                      
            SET @NUMPTLLA = (SELECT DISTINCT sp.NUMEROPLANTILLASERVICIOPARCIALIDAD                                          
            FROM SERVICIO_PARCIALIDAD sp                                          
            INNER JOIN TIPOLOGIA_SERVICIO ts  ON sp.IDTIPOLOGIASERVICIO = ts.IDTIPOLOGIASERVICIO                                          
            INNER JOIN MAESTRO_TIPOLOGIA mt   ON ts.IDMAESTROTIPOLOGIA = mt.IDMAESTROTIPOLOGIA                                          
            INNER JOIN MAESTRO_SERVICIO ms    ON ts.IDMAESTROSERVICIO = ms.IDMAESTROSERVICIO                                          
            INNER JOIN SUB_MODALIDAD_PARCIALIDAD smp  ON sp.IDSUBMODALIDADPARCIALIDAD = smp.IDSUBMODALIDADPARCIALIDAD                                          
            INNER JOIN MAESTRO_SUB_MODALIDAD msm      ON smp.IDMAESTROSUBMODALIDAD = msm.IDMAESTROSUBMODALIDAD                                          
            INNER JOIN MAESTRO_PARCIALIDAD mp         ON smp.IDMAESTROPARCIALIDAD = mp.IDMAESTROPARCIALIDAD                                          
            WHERE sp.IDMAESTROPROGRAMA = @IDMAESTROPROGRAMA                          
            AND ts.IDMAESTROTIPOLOGIA = @IDMAESTROTIPOLOGIA                      
            AND  msm.IDMAESTROSUBMODALIDAD = @IDMAESTROSUBMODALIDAD)                        
        END  		
                                             
        INSERT INTO @SERVICIOPARCIALIDAD (IDSERVPARC)                                          
        SELECT IDSERVICIOPARCIALIDAD                                          
        FROM SERVICIO_PARCIALIDAD                                          
        WHERE NUMEROPLANTILLASERVICIOPARCIALIDAD = @NUMPTLLA                                          
        ORDER BY IDSERVICIOPARCIALIDAD
		
		--select IDSERVPARC from  @SERVICIOPARCIALIDAD                                          
  --    ------------------------------------------------------------------------                                          
                                             
        WHILE((SELECT COUNT(@1) FROM @SERVICIOPARCIALIDAD) > @0)                                          
        BEGIN
		    
                                        
            SET @IDSERVPARC = (SELECT TOP 1 IDSERVPARC FROM @SERVICIOPARCIALIDAD)

            --SELECT @IDMAESTROPROGRAMA AS PROGRAMA, @MODALIDAD AS MODALIDAD, @IDMAESTROTIPOLOGIA AS TIPOLOGIA, @IDMAESTROSUBMODALIDAD AS SUBMODALIDAD, @IDSERVPARC, @IDCARACTERISTICASESPECIALES	

			
			IF ((SELECT COUNT(1) FROM TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA
			     WHERE IDSERVICIOPARCIALIDAD = @IDSERVPARC
				   AND IDCARACTERISTICASESPECIALES = @IDCARACTERISTICASESPECIALES) = 0)
			BEGIN                                                           
                INSERT INTO TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA(IDSERVICIOPARCIALIDAD,                                          
                      IDCARACTERISTICASESPECIALES,                                          
                      ESTADOTIPOSERVICIOPARCIALIDADCARACTERISTICA,                                          
                      MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA,                                          
                      MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA,                   
                      PORCENTAJEPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA,                                          
                      IDSOLICITUDPAGO)                                          
                VALUES(@IDSERVPARC, @IDCARACTERISTICASESPECIALES, 1, NULL, NULL, NULL, NULL)                                          
            END
                       
            DELETE FROM @SERVICIOPARCIALIDAD WHERE IDSERVPARC = @IDSERVPARC                                          
        END                                          
                                           
	    IF(@@ERROR <> 0)                                          
	    BEGIN                                          
	    	ROLLBACK SELECT 'ERROR AL INSERTAR TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA'                                          
	    END 

--==================
		
	END                                          
 -----------------------------------------------------------FIN_ACTUALIZAR_CARACTERISTICAS_ESPECIALES--------------------------------------------------                                            
                                        
 -----------------------------------------------------------7) INICIO_INSERT_PROYECTO-----------------------------------------------------------------------                                            
    BEGIN                                        
        IF(@PROYECTOEXISTENTE =@0)                                    
        BEGIN                                        
            INSERT INTO INFORMACION_PROYECTO(IDDIRECCION,                                          
            IDMAESTROTITULO,                                          
            IDMAESTROMODALIDAD,                                          
            IDMAESTROLLAMADO,                                          
            IDMAESTROALTERNATIVAPOSTULACION,                                          
            CODIGOPROYECTOINFORMACIONPROYECTO,                                          
            NOMBREPROYECTOINFORMACIONPROYECTO,                                          
            FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTO,                                          
            ESTADOPROYECTOINFORMACIONPROYECTO,                                          
            RESOLUCIONATINFORMACIONPROYECTO,                                          
            FECHARESOLUCIONATINFORMACIONPROYECTO,           
            CANTIDADVIVIENDASINFORMACIONPROYECTO,                                          
            CANTIDADBENEFICIARIOSINFORMACIONPROYECTO,                                          
            IDMAESTROPROGRAMA,                                          
            MONTOSUBSIDIOBASEINFORMACIONPROYECTO,                                          
            MONTOSUBSIDIOBASEFACTIBILIDADINFORMACIONPROYECTO,                                          
            MARCALOCALIZACIONINFORMACIONPROYECTO,                                          
            MARCAFACTIBILIZACIONINFORMACIONPROYECTO,                                          
            FECHAFACTIBILIZACIONINFORMACIONPROYECTO,                                          
            NUMEROFAMILIASADSCRITASINFORMACIONPROYECTO,                                          
            IDMAESTRORESOLUCION,                                          
            AGNOFACTIBILIDADINFORMACIONPROYECTO,                                          
            IDMAESTROESTADOPROYECTO,                                          
            IDMAESTROESTADOBENEFICIO,         
            IDMAESTROBANCO,                                          
            AVANCEOBRAINFORMACIONPROYECTO,                            
            ESTADOAVANCEOBRAINFORMACIONPROYECTO,                                          
            PERMISOUSUARIOADMINISTRATIVOINFORMACIONPROYECTO,                                          
            NOMBREUSUARIOOTORGAPERMISOINFORMACIONPROYECTO                                        
            )                                          
            VALUES (@IDDIRECCION,                                          
            NULL,--Maestro Titulo                                          
            @IDMAESTROMODALIDAD,                                          
            @IDMAESTROLLAMADO,--MAESTROLLAMADO                                          
            @IDMAESTROALTERNATIVAPOSTULACION,                                          
            @CODIGOPROYECTO,                                          
            @NOMBREPROYECTO,                                          
            @FECHACALIFICACIONDIFINITIVA,                                          
            1,--Estado Proyecto                                          
            NULL,--@RESOLUCIONATINFORMACIONPROYECTO                                          
            NULL,--@FECHARESOLUCIONATINFORMACIONPROYECTO                                          
            @VIVIENDASTOTALPROYECTO,                                          
            @NUMEROFAMILIASADSCRITAS,                                          
            @IDMAESTROPROGRAMA,                 
            @MONTOSUBSIDIOBASE,                                          
            @MONTOSUBSIDIOBASECONFACTIBILIZACION,                                          
            @MARCALOCALIZACION,                                          
            @MARCAFACTIBILIZACION,                                          
            @FECHAFACTIBILIDADPROYECTO,                                          
            @NUMEROFAMILIASADSCRITAS,                                          
            NULL,--@IDMAESTRORESOLUCION                                          
            @AGNOFACTIBILIDADINFORMACIONPROYECTO,                                          
            @IDMAESTROESTADOPROYECTO,                                          
            @IDMAESTROESTADOBENEFICIO,                                   
            @IDMAESTROBANCO,                                          
            NULL,--AVANCEOBRAINFORMACIONPROYECTO,                                          
            NULL,--ESTADOAVANCEOBRAINFORMACIONPROYECTO,                                          
            NULL,--PERMISOUSUARIOADMINISTRATIVOINFORMACIONPROYECTO,                                          
            NULL)--NOMBREUSUARIOOTORGAPERMISOINFORMACIONPROYECTO                                          
                                       
            SET @IDINFORMACIONPROYECTO = @@IDENTITY                                          
                                       
            IF(@@ERROR <> 0)                                          
            BEGIN                                          
                ROLLBACK SELECT 'ERROR AL INSERTAR PROYECTO '  +@CODIGOPROYECTO;                                        
            END                                        
        END                                        
    END                                        
   ---------------------------------------------------------FIN_INSERT_PROYECTO-----------------------------------------------------------------------                                            
                                      
   ---------------------------------------------------------8) INICIO_INSERT_CARACTERISTICAS_ESPECIALES--------------------------------------                                          
    BEGIN                                        
        IF(@PROYECTOEXISTENTE =@0)                                    
        BEGIN                                                      
            INSERT INTO CARACTERISTICAS_ESPECIALES(IDMAESTROPROGRAMA,                                          
                    IDMAESTROMODALIDAD,                                          
                    IDMAESTROTIPOLOGIA,                                          
                    IDINFORMACIONPROYECTO,                                          
                    IDMAESTROCLASE,    
                    IDMAESTROSUBMODALIDAD)--1 No aplica                                          
            VALUES(@IDMAESTROPROGRAMA, @IDMAESTROMODALIDAD, @IDMAESTROTIPOLOGIA , @IDINFORMACIONPROYECTO, 1 ,@IDMAESTROSUBMODALIDAD)                                          
                                              
                                               
            SET @IDCARACTERISTICASESPECIALES = @@IDENTITY                                          
                                               
                                              
            IF(@@ERROR <> 0)                        
            BEGIN                                          
                ROLLBACK SELECT 'ERROR AL INSERTAR CARACTERISTICAS_ESPECIALES'                                          
            END                                          
        END                                          
    END                                          
   ---------------------------------------------------------FIN_INSERT_CARACTERISTICAS_ESPECIALES----------------------------------------                                          
                                       
   ---------------------------------------------------------9) INICIO_PROVEEDOR_Y_CONSTRUCTORA_UPDATE_Y_INSERT------------------------------------------------                                          
    BEGIN                                        
        -- ACTUALIZAMOS PROVEEDOR EXISTENTE    
                                                 
        DECLARE @PROVEEDOR TABLE(                                        
            [IDPROVEEDOR] [bigint]  NOT NULL,                                        
            [IDMAESTROTIPOPROVEEDOR] [bigint] NULL,                                        
            [NOMBREPROVEEDOR] [varchar](200) NULL,                                        
            [RUTPROVEEDOR] [int] NULL,                                        
            [DVPROVEDIGITOVERIFICADORPROVEEDOR] [char](1) NULL)                                        
                                     
        INSERT INTO @PROVEEDOR([IDPROVEEDOR], [IDMAESTROTIPOPROVEEDOR], [NOMBREPROVEEDOR], [RUTPROVEEDOR],[DVPROVEDIGITOVERIFICADORPROVEEDOR])                                        
                                          
        SELECT DISTINCT TOP 1 P.IDPROVEEDOR, P.IDMAESTROTIPOPROVEEDOR, P.NOMBREPROVEEDOR, P.RUTPROVEEDOR, P.DVPROVEDIGITOVERIFICADORPROVEEDOR                                          
        FROM [INFORMACION_PROYECTO] IPR INNER JOIN [TIPO_PROVEEDOR_INFORMACION_PROYECTO] TPIP ON                                         
            IPR.IDINFORMACIONPROYECTO = TPIP.IDINFORMACIONPROYECTO  INNER JOIN                                        
            PROVEEDOR P ON TPIP.IDPROVEEDOR = P.IDPROVEEDOR WHERE IPR.IDINFORMACIONPROYECTO = @IDINFORMACIONPROYECTO                                        
 
        if(@RUTENTIDAD is not null and(LTRIM(RTRIM(@NOMBREENTIDAD))  != '' AND (SELECT COUNT(IDPROVEEDOR) FROM @PROVEEDOR)>0))                                      
        begin                                
            -- PROVEEDOR                                        
            UPDATE [PROVEEDOR]                                         
            SET [NOMBREPROVEEDOR] = @NOMBREENTIDAD                                        
            ,[DVPROVEDIGITOVERIFICADORPROVEEDOR] = @DIGITOENTIDAD                                        
            WHERE IDPROVEEDOR = (SELECT IDPROVEEDOR FROM  @PROVEEDOR P WHERE P.RUTPROVEEDOR = @RUTENTIDAD)                                     
            IF(@@ERROR <> 0)                                          
            BEGIN                                          
                ROLLBACK SELECT 'ERROR AL ACTUALIZAR PROVEEDOR '  + @CODIGOPROYECTO                                        
            END                                          
        end    

        if(@RUTEMPRESACONSTRUCTORA is not null and( LTRIM(RTRIM(@NOMBREEMPRESACONSTRUCTORA))  != '' AND (SELECT COUNT(IDPROVEEDOR) FROM @PROVEEDOR)>0))                                      
        begin                                      
            -- CONSTRUCTORA       
            UPDATE [PROVEEDOR]                                         
            SET [NOMBREPROVEEDOR] = @NOMBREEMPRESACONSTRUCTORA                                        
            ,[DVPROVEDIGITOVERIFICADORPROVEEDOR] = @DIGITOEMPRESACONSTRUCTORA                                       
            WHERE IDPROVEEDOR = (SELECT IDPROVEEDOR FROM  @PROVEEDOR P WHERE P.RUTPROVEEDOR = @RUTEMPRESACONSTRUCTORA)                                        
      
		    IF(@@ERROR <> 0)                                          
            BEGIN                                          
                ROLLBACK SELECT 'ERROR AL ACTUALIZAR CONSTRUCTORA '  + @CODIGOPROYECTO                                        
            END                                        
        end    
  
        -- RELACIONAMOS UNA CONSTRUCTORA SI ES QUE EXISTE                              
        IF(@RUTEMPRESACONSTRUCTORA is not null and((SELECT COUNT(IDPROVEEDOR) FROM PROVEEDOR WHERE RUTPROVEEDOR = @RUTEMPRESACONSTRUCTORA AND IDMAESTROTIPOPROVEEDOR =@2) > @0))                                    
        BEGIN                               
            DECLARE @IDPROVEEDORCONSTRUCTORA INT                               
            SET @IDPROVEEDORCONSTRUCTORA  = (SELECT TOP 1 IDPROVEEDOR FROM PROVEEDOR WHERE RUTPROVEEDOR = @RUTEMPRESACONSTRUCTORA)                              
                              
            INSERT INTO [dbo].[TIPO_PROVEEDOR_INFORMACION_PROYECTO]                                    
               ([IDPROVEEDOR]                                    
               ,[IDINFORMACIONPROYECTO]                                    
               ,[ESTADOTIPOPROVEEDORINFORMACIONPROYECTO])                                    
            VALUES                                    
               (@IDPROVEEDORCONSTRUCTORA                                    
               ,@IDINFORMACIONPROYECTO                                    
               ,@1)                                   
        END                              
	    -- AGREGAMOS UNA CONSTRUCTORA NUEVA EN CASO DE QUE NO EXISTA                                    
        ELSE IF(@RUTEMPRESACONSTRUCTORA is not null and( (SELECT COUNT(IDPROVEEDOR) FROM @PROVEEDOR WHERE RUTPROVEEDOR = @RUTEMPRESACONSTRUCTORA) = @0 and LTRIM(RTRIM(@NOMBREEMPRESACONSTRUCTORA))  != ''))                                    
        BEGIN                                     
            INSERT INTO [dbo].[PROVEEDOR]                                    
                          ([IDMAESTROTIPOPROVEEDOR]                                    
                          ,[NOMBREPROVEEDOR]                                    
                          ,[RUTPROVEEDOR]                                    
                          ,[DVPROVEDIGITOVERIFICADORPROVEEDOR])                                    
                VALUES                                    
                         (@2                                    
                         ,@NOMBREEMPRESACONSTRUCTORA                                    
                         ,@RUTEMPRESACONSTRUCTORA                                    
                         ,@DIGITOEMPRESACONSTRUCTORA)                                  
                                
            SET @SCOPE_IDENTITY = SCOPE_IDENTITY()                                    
                                           
            INSERT INTO [dbo].[TIPO_PROVEEDOR_INFORMACION_PROYECTO]                                    
                        ([IDPROVEEDOR]                                    
                        ,[IDINFORMACIONPROYECTO]                                    
                        ,[ESTADOTIPOPROVEEDORINFORMACIONPROYECTO])                                    
                VALUES                                    
                        (@SCOPE_IDENTITY                                    
                        ,@IDINFORMACIONPROYECTO                                    
                        ,@1)                                   
                                  
            IF(@@ERROR <> 0)                                      
            BEGIN                                      
                ROLLBACK SELECT 'ERROR AL INSERTAR PROVEEDOR '  + @CODIGOPROYECTO                                    
            END                                      
        END                  
     
	    -- RELACIONAMOS UNA entidad SI ES QUE EXISTE                              
	    IF(@RUTENTIDAD is not null and ((SELECT COUNT(IDPROVEEDOR) FROM PROVEEDOR WHERE RUTPROVEEDOR = @RUTENTIDAD AND IDMAESTROTIPOPROVEEDOR =@1) > @0))                                    
        BEGIN                               
            DECLARE @IDPROVEEDORENTIDAD INT                               
            SET @IDPROVEEDORENTIDAD  = (SELECT TOP 1 IDPROVEEDOR FROM PROVEEDOR WHERE RUTPROVEEDOR = @RUTENTIDAD)                              
                              
            INSERT INTO [dbo].[TIPO_PROVEEDOR_INFORMACION_PROYECTO]                                    
                ([IDPROVEEDOR]                                    
                ,[IDINFORMACIONPROYECTO]                                    
                ,[ESTADOTIPOPROVEEDORINFORMACIONPROYECTO])                                    
            VALUES                                    
                (@IDPROVEEDORENTIDAD                                    
                ,@IDINFORMACIONPROYECTO                                    
                ,@1)                                   
        END                              
	    -- AGREGAMOS UNA ENTIDAD NUEVA EN CASO DE QUE NO EXISTA                                    
        ELSE IF( @RUTENTIDAD is not null and ((SELECT COUNT(IDPROVEEDOR) FROM @PROVEEDOR WHERE RUTPROVEEDOR = @RUTENTIDAD) = @0 and LTRIM(RTRIM(@NOMBREENTIDAD))  != ''))                                    
        BEGIN                               
            INSERT INTO [dbo].[PROVEEDOR]                                    
                        ([IDMAESTROTIPOPROVEEDOR]                                    
                        ,[NOMBREPROVEEDOR]                                    
                        ,[RUTPROVEEDOR]                                    
                        ,[DVPROVEDIGITOVERIFICADORPROVEEDOR])                                    
            VALUES                                    
                        (@1                   
                        ,@NOMBREENTIDAD                                    
                        ,@RUTENTIDAD                                    
                        ,@DIGITOENTIDAD)                                  
                                
            SET @SCOPE_IDENTITY = SCOPE_IDENTITY()                                    
                                           
            INSERT INTO [dbo].[TIPO_PROVEEDOR_INFORMACION_PROYECTO]                                    
                        ([IDPROVEEDOR]                                    
                        ,[IDINFORMACIONPROYECTO]                                    
                        ,[ESTADOTIPOPROVEEDORINFORMACIONPROYECTO])   
            VALUES                                    
                        (@SCOPE_IDENTITY                                    
                        ,@IDINFORMACIONPROYECTO                                    
                        ,@1)                                   
                                  
            IF(@@ERROR <> 0)                                      
            BEGIN                                      
                ROLLBACK SELECT 'ERROR AL INSERTAR PROVEEDOR '  + @CODIGOPROYECTO                                    
            END                                      
        END                                    
    END                                        
  ----------------------------------------------------------FIN_PROVEEDOR_Y_CONSTRUCTORA_UPDATE_Y_INSERT------------------------------------------------                                          
                                        
  ---------------------------------------------------------10) INICIO_ASIGNACION_PLANTILLAS-------------------------------------------------                                          
    BEGIN                                        
        IF(@PROYECTOEXISTENTE = @0 )                      
        BEGIN                      
            -- INERTAMOS INCREMENTOS                    
            DECLARE @LISTA_INCREMENTO TABLE                    
            (         
                [IDTIPOINCREMENTO] [bigint] IDENTITY(1,1) NOT NULL,                    
                [IDMAESTROINCREMENTO] [bigint] NULL,                    
                [IDCARACTERISTICASESPECIALES] [bigint] NULL,                    
                [IDSOLICITUDPAGO] [bigint] NULL,                    
                [SELECCIONADOTIPOINCREMENTO] [bit] NULL,                    
                [ESTADOTIPOINCREMENTO] [bit] NULL                    
            )                     
                  
            
        IF(@IDMAESTROPROGRAMA = @8)                    
        Begin                    
            -- 105  (Construcción De Nuevos Terrenos, Megaproyecto)                    
            IF((@IDMAESTROTIPOLOGIA = @8 OR @IDMAESTROTIPOLOGIA = @11  ))                    
            BEGIN                     
                IF(@IDMAESTROTIPOLOGIA = @8)       
                BEGIN  
                    INSERT INTO @LISTA_INCREMENTO (IDMAESTROINCREMENTO, IDCARACTERISTICASESPECIALES ,IDSOLICITUDPAGO, SELECCIONADOTIPOINCREMENTO, ESTADOTIPOINCREMENTO)                    
                    SELECT IDMAESTROINCREMENTO, @IDCARACTERISTICASESPECIALES, NULL, @0, @1  FROM MAESTRO_INCREMENTO WHERE IDMAESTROINCREMENTO IN (                    
                            @10                    
                           ,@11                    
                           ,@12                    
                           ,@13                    
                           ,@14                    
                           ,@15                    
                           ,@17                    
                           ,@18   
                           ,@19                       
                           ,@20)                     
                END     

                IF(@IDMAESTROTIPOLOGIA = @11)       
                BEGIN   
                    INSERT INTO @LISTA_INCREMENTO (IDMAESTROINCREMENTO, IDCARACTERISTICASESPECIALES ,IDSOLICITUDPAGO, SELECCIONADOTIPOINCREMENTO, ESTADOTIPOINCREMENTO)                    
                    SELECT IDMAESTROINCREMENTO, @IDCARACTERISTICASESPECIALES, NULL, @0, @1  FROM MAESTRO_INCREMENTO WHERE IDMAESTROINCREMENTO IN (                    
                     @10                    
                    ,@11                    
                    ,@12                    
                    ,@13                    
                    ,@14                    
                    ,@15                    
                    ,@17                    
                    ,@18                        
                    ,@20)          
                END  
        END                    
             
        --               
        -- 105 ACA TIPOLOGIA 9, 10  (Construcción En Sitio Propio, Densificación Predial)                    
        IF(@IDMAESTROSUBMODALIDAD = @2 AND (@IDMAESTROTIPOLOGIA = @9 OR @IDMAESTROTIPOLOGIA = @10))                    
        BEGIN                    
            INSERT INTO @LISTA_INCREMENTO (IDMAESTROINCREMENTO, IDCARACTERISTICASESPECIALES ,IDSOLICITUDPAGO, SELECCIONADOTIPOINCREMENTO, ESTADOTIPOINCREMENTO)                    
            SELECT IDMAESTROINCREMENTO, @IDCARACTERISTICASESPECIALES, NULL, @0, @1  FROM MAESTRO_INCREMENTO WHERE IDMAESTROINCREMENTO IN (                    
                   @14)                     
        END                    
        -- 105 PAGO POR AVANCE Y VT, TIPOLOGIA 10 (Densificación Predial)                    
        IF((@IDMAESTROSUBMODALIDAD = @3 OR @IDMAESTROSUBMODALIDAD = @4 ) AND (@IDMAESTROTIPOLOGIA = @10))                    
        BEGIN                    
            INSERT INTO @LISTA_INCREMENTO (IDMAESTROINCREMENTO, IDCARACTERISTICASESPECIALES ,IDSOLICITUDPAGO, SELECCIONADOTIPOINCREMENTO, ESTADOTIPOINCREMENTO)                    
            SELECT IDMAESTROINCREMENTO, @IDCARACTERISTICASESPECIALES, NULL, @0, @1  FROM MAESTRO_INCREMENTO WHERE IDMAESTROINCREMENTO IN (                    
                           
             @11                    
            ,@13                    
            ,@14                    
            ,@16                    
            ,@18)                     
        END                  
             
        -- 105 PAGO POR AVANCE Y VT, TIPOLOGIA 9 (CONSTRUCCION EN SITIO PROPIO)                    
        IF((@IDMAESTROSUBMODALIDAD = @3 OR @IDMAESTROSUBMODALIDAD = @4 ) AND (@IDMAESTROTIPOLOGIA = @9))                    
        BEGIN                    
            INSERT INTO @LISTA_INCREMENTO (IDMAESTROINCREMENTO, IDCARACTERISTICASESPECIALES ,IDSOLICITUDPAGO, SELECCIONADOTIPOINCREMENTO, ESTADOTIPOINCREMENTO)                    
            SELECT IDMAESTROINCREMENTO, @IDCARACTERISTICASESPECIALES, NULL, @0, @1  FROM MAESTRO_INCREMENTO WHERE IDMAESTROINCREMENTO IN (                    
                           
             @11                    
            ,@13                    
            ,@14                          
            ,@18)                     
        END                    
                     
        -- 105 TIPOLOGIA 12 (Pequeños Condominios)                    
        IF(@IDMAESTROTIPOLOGIA = @12)                    
        BEGIN                    
            INSERT INTO @LISTA_INCREMENTO (IDMAESTROINCREMENTO, IDCARACTERISTICASESPECIALES ,IDSOLICITUDPAGO, SELECCIONADOTIPOINCREMENTO, ESTADOTIPOINCREMENTO)                    
            SELECT IDMAESTROINCREMENTO, @IDCARACTERISTICASESPECIALES, NULL, @0, @1  FROM MAESTRO_INCREMENTO WHERE IDMAESTROINCREMENTO IN (                    
                    @11                    
                   ,@13            
                   ,@14                    
                   ,@18)                     
        END                    
                     
        -- 105 TIPOLOGIA 7 (Adquisición De Vivienda Construida)                    
        IF(@IDMAESTROTIPOLOGIA = @7)         
        BEGIN                    
            INSERT INTO @LISTA_INCREMENTO (IDMAESTROINCREMENTO, IDCARACTERISTICASESPECIALES ,IDSOLICITUDPAGO, SELECCIONADOTIPOINCREMENTO, ESTADOTIPOINCREMENTO)                    
            SELECT IDMAESTROINCREMENTO, @IDCARACTERISTICASESPECIALES, NULL, @0, @1  FROM MAESTRO_INCREMENTO WHERE IDMAESTROINCREMENTO IN (                    
                    @32                    
                   ,@33                    
                   ,@34                    
                   ,@35                    
                   ,@36                    
                   ,@37                    
                   )                     
        END                    
    END                    
                
    IF(@IDMAESTROPROGRAMA = @3)                    
    BEGIN                    
        -- DS49 TIPOLOGIA 8,11,14,16 (Construcción De Nuevos Terrenos, Megaproyecto, Construcción En Sitio Propio  (Proyectos Colectivos), Densificación Predial (Proyectos Colectivos))                    
        IF(@IDMAESTROTIPOLOGIA = @8 OR @IDMAESTROTIPOLOGIA = @11 OR @IDMAESTROTIPOLOGIA = @14 OR @IDMAESTROTIPOLOGIA = @16)                    
        BEGIN                    
            INSERT INTO @LISTA_INCREMENTO (IDMAESTROINCREMENTO, IDCARACTERISTICASESPECIALES ,IDSOLICITUDPAGO, SELECCIONADOTIPOINCREMENTO, ESTADOTIPOINCREMENTO)                    
            SELECT IDMAESTROINCREMENTO, @IDCARACTERISTICASESPECIALES, NULL, @0, @1  FROM MAESTRO_INCREMENTO WHERE IDMAESTROINCREMENTO IN (                    
            @26                    
            ,@27        
            ,@28                    
            ,@29                    
            )                     
        END                    
    END                    
                  
    IF(@IDMAESTROPROGRAMA = @2)                    
    BEGIN                    
        -- DS174 TIPOLOGIA 8,3,10 (Construcción De Nuevos Terrenos, Construcción En Sitio Del Residente, Densificación Predial)                    
        IF(@IDMAESTROTIPOLOGIA = @8 OR @IDMAESTROTIPOLOGIA = @3 OR @IDMAESTROTIPOLOGIA = @10 OR @IDMAESTROTIPOLOGIA = @18)                    
        BEGIN                    
            INSERT INTO @LISTA_INCREMENTO (IDMAESTROINCREMENTO, IDCARACTERISTICASESPECIALES ,IDSOLICITUDPAGO, SELECCIONADOTIPOINCREMENTO, ESTADOTIPOINCREMENTO)                    
            SELECT IDMAESTROINCREMENTO, @IDCARACTERISTICASESPECIALES, NULL, @0, @1  FROM MAESTRO_INCREMENTO WHERE IDMAESTROINCREMENTO IN (                    
                    @30,            
                    @31                    
                   )                     
        END                  
    END                    
                
    INSERT INTO [dbo].[TIPO_INCREMENTO]( IDMAESTROINCREMENTO, IDCARACTERISTICASESPECIALES ,IDSOLICITUDPAGO, SELECCIONADOTIPOINCREMENTO, ESTADOTIPOINCREMENTO)                   
    SELECT IDMAESTROINCREMENTO, IDCARACTERISTICASESPECIALES ,IDSOLICITUDPAGO, SELECCIONADOTIPOINCREMENTO, ESTADOTIPOINCREMENTO FROM @LISTA_INCREMENTO                  
        
    -- INSERTAMOS PLANTILLAS                 
    IF(@PROYECTOEXISTENTE = @0 and @IDMAESTROPROGRAMA = @2 and @IDMAESTROTIPOLOGIA = @18)                     
    BEGIN          
        SET @NUMPTLLA = @32          
    END                 
    ELSE IF(@PROYECTOEXISTENTE = @0 and @IDMAESTROPROGRAMA != @8)                                           
    BEGIN                                                          
        SET @NUMPTLLA = (SELECT DISTINCT sp.NUMEROPLANTILLASERVICIOPARCIALIDAD                                          
        FROM SERVICIO_PARCIALIDAD sp                                          
        INNER JOIN TIPOLOGIA_SERVICIO ts  ON sp.IDTIPOLOGIASERVICIO = ts.IDTIPOLOGIASERVICIO                      
        INNER JOIN MAESTRO_TIPOLOGIA mt   ON ts.IDMAESTROTIPOLOGIA = mt.IDMAESTROTIPOLOGIA                                          
        INNER JOIN MAESTRO_SERVICIO ms    ON ts.IDMAESTROSERVICIO = ms.IDMAESTROSERVICIO                             
        INNER JOIN SUB_MODALIDAD_PARCIALIDAD smp ON sp.IDSUBMODALIDADPARCIALIDAD = smp.IDSUBMODALIDADPARCIALIDAD                                          
        INNER JOIN MAESTRO_SUB_MODALIDAD msm     ON smp.IDMAESTROSUBMODALIDAD = msm.IDMAESTROSUBMODALIDAD                                          
        INNER JOIN MAESTRO_PARCIALIDAD mp        ON smp.IDMAESTROPARCIALIDAD = mp.IDMAESTROPARCIALIDAD                                          
        WHERE sp.IDMAESTROPROGRAMA = @IDMAESTROPROGRAMA                                          
          AND ts.IDMAESTROTIPOLOGIA = @IDMAESTROTIPOLOGIA)                                          
    END                                     
    ELSE IF(@PROYECTOEXISTENTE = @0 and @IDMAESTROPROGRAMA = @8)                         
    BEGIN                      
        SET @NUMPTLLA = (SELECT DISTINCT sp.NUMEROPLANTILLASERVICIOPARCIALIDAD                                          
        FROM SERVICIO_PARCIALIDAD sp                                          
        INNER JOIN TIPOLOGIA_SERVICIO ts  ON sp.IDTIPOLOGIASERVICIO = ts.IDTIPOLOGIASERVICIO                                          
        INNER JOIN MAESTRO_TIPOLOGIA mt   ON ts.IDMAESTROTIPOLOGIA = mt.IDMAESTROTIPOLOGIA                                          
        INNER JOIN MAESTRO_SERVICIO ms    ON ts.IDMAESTROSERVICIO = ms.IDMAESTROSERVICIO                                          
        INNER JOIN SUB_MODALIDAD_PARCIALIDAD smp  ON sp.IDSUBMODALIDADPARCIALIDAD = smp.IDSUBMODALIDADPARCIALIDAD                                          
        INNER JOIN MAESTRO_SUB_MODALIDAD msm      ON smp.IDMAESTROSUBMODALIDAD = msm.IDMAESTROSUBMODALIDAD                                          
        INNER JOIN MAESTRO_PARCIALIDAD mp         ON smp.IDMAESTROPARCIALIDAD = mp.IDMAESTROPARCIALIDAD                                          
        WHERE sp.IDMAESTROPROGRAMA = @IDMAESTROPROGRAMA                          
        AND ts.IDMAESTROTIPOLOGIA = @IDMAESTROTIPOLOGIA                      
        AND  msm.IDMAESTROSUBMODALIDAD = @IDMAESTROSUBMODALIDAD)                        
    END                      
          
    INSERT INTO @SERVICIOPARCIALIDAD (IDSERVPARC)                                          
    SELECT IDSERVICIOPARCIALIDAD                                          
    FROM SERVICIO_PARCIALIDAD                                          
    WHERE NUMEROPLANTILLASERVICIOPARCIALIDAD = @NUMPTLLA                                          
    ORDER BY IDSERVICIOPARCIALIDAD                                          
  --------------------------------------------------------------------------                                          
                                       
    WHILE((SELECT COUNT(@1) FROM @SERVICIOPARCIALIDAD) > @0)                                          
    BEGIN                                          
        SET @IDSERVPARC = (SELECT TOP 1 IDSERVPARC FROM @SERVICIOPARCIALIDAD)                                          
                                       
        INSERT INTO TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA(IDSERVICIOPARCIALIDAD,                                          
              IDCARACTERISTICASESPECIALES,                                          
              ESTADOTIPOSERVICIOPARCIALIDADCARACTERISTICA,                                          
              MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA,                                          
              MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA,                   
              PORCENTAJEPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA,                                          
              IDSOLICITUDPAGO)                                          
        VALUES(@IDSERVPARC, @IDCARACTERISTICASESPECIALES, 1, NULL, NULL, NULL, NULL)                                          
                                     
        DELETE FROM @SERVICIOPARCIALIDAD WHERE IDSERVPARC = @IDSERVPARC                                          
    END                                          
                                       
	IF(@@ERROR <> 0)                                          
	BEGIN                                          
		ROLLBACK SELECT 'ERROR AL INSERTAR TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA'                                          
	END                                          
                                       
	END                                        
	END                                        
  ---------------------------------------------------------FIN_ASIGNACION_PLANTILLAS---------------------------------------------------                                          

	IF(EXISTS(SELECT 1 FROM CARACTERISTICAS_ESPECIALES where IDMAESTROPROGRAMA = @8 and IDMAESTROTIPOLOGIA = @8 and IDCARACTERISTICASESPECIALES = @IDCARACTERISTICASESPECIALES ))       
	begin  
		if ((select count(IDTIPOINCREMENTO) from TIPO_INCREMENTO where IDMAESTROINCREMENTO = @19 and IDCARACTERISTICASESPECIALES = @IDCARACTERISTICASESPECIALES) = @0)  
		begin  
			INSERT INTO [dbo].[TIPO_INCREMENTO]( IDMAESTROINCREMENTO, IDCARACTERISTICASESPECIALES ,IDSOLICITUDPAGO, SELECCIONADOTIPOINCREMENTO, ESTADOTIPOINCREMENTO)          
			select top 1 @19 as IDMAESTROINCREMENTO ,@IDCARACTERISTICASESPECIALES,null,@0, @1 from CARACTERISTICAS_ESPECIALES ca where IDMAESTROPROGRAMA = @8 and IDMAESTROTIPOLOGIA = @8 and IDCARACTERISTICASESPECIALES = @IDCARACTERISTICASESPECIALES  
	    end  
	end  

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
             
	COMMIT TRAN                                          
	SELECT 'OK'                                          
 END 
GO
/****** Object:  StoredProcedure [dbo].[INSERTA_PROYECTO_SNATDS49]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



/********************************************************************************************  
NOMBRE DEL PROCEDIMIENTO 	: [INSERTA_PROYECTO_SNATDS49]
FECHA DE CREACIÓN      		: 02/10/2018
USUARIO DE CREACIÓN      	: Daniel Orozco - Anticipa.
VERSIÓN            			: 1.0                            

Visado por DBA              : José López
Fecha Aprobación DBA        : 20181029 
Comentarios DBA             : 
          
OBJETIVO     				: Inserta en SNAT Simplificado los proyectos obtenidos desde Rukan
  
TABLAS      				: DIRECCION
							  PROVEEDOR
							  INFORMACION_PROYECTO
          
  
QUE RETORNA     			: --
  
PARAMETROS     				:@IDMAESTROALTERNATIVAPOSTULACION BigInt,
							 @CODIGOPROYECTOINFORMACIONPROYECTO BigInt,
							 @NOMBREPROYECTOINFORMACIONPROYECTO VarChar(200),
							 @FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTO DateTime,
							 @ESTADOPROYECTOINFORMACIONPROYECTO Bit,
							 @CANTIDADVIVIENDASINFORMACIONPROYECTO int,
							 @CANTIDADBENEFICIARIOSINFORMACIONPROYECTO int,
							 @AGNOFACTIBILIDADINFORMACIONPROYECTO Int,
							 @MONTOSUBSIDIOBASEINFORMACIONPROYECTO decimal(18,3),
							 @MONTOSUBSIDIOBASEFACTIBILIDADINFORMACIONPROYECTO decimal(18,3),
							 @MARCALOCALIZACIONINFORMACIONPROYECTO bit,
							 @MARCAFACTIBILIZACIONINFORMACIONPROYECTO bit,
							 @FECHAFACTIBILIZACIONINFORMACIONPROYECTO DateTime,
							 @NUMEROFAMILIASABSCRITASINFORMACIONPROYECTO Int,
							 @CODIGOCOMUNADIRECCION int,
							 @CODIGOPROVINCIADIRECCION int,
							 @CODIGOREGIONDIRECCION int,
							 @RUTENTIDAD int,
							 @DIGITOENTIDAD char(1),
							 @NOMBREENTIDAD VarChar(200),
							 @RUTEMPRESACONSTRUCTORA int,
							 @DIGITOEMPRESACONSTRUCTORA char(1),
							 @NOMBREEMPRESACONSTRUCTORA VarChar(200),
							 @DESCTIPOMODALIDAD VarChar(200)
            
PRUEBA						:                   

           
PROYECTO     				: SNAT SIMPLIFICADO
RESPONSABLE     			: DINFO  
							  
********************************************************************************************/  
CREATE PROCEDURE [dbo].[INSERTA_PROYECTO_SNATDS49]
	@IDMAESTROALTERNATIVAPOSTULACION BigInt,
	@CODIGOPROYECTOINFORMACIONPROYECTO BigInt,
	@NOMBREPROYECTOINFORMACIONPROYECTO VarChar(200),
	@FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTO DateTime,
	@ESTADOPROYECTOINFORMACIONPROYECTO Bit,
	@CANTIDADVIVIENDASINFORMACIONPROYECTO int,
	@CANTIDADBENEFICIARIOSINFORMACIONPROYECTO int,
	@AGNOFACTIBILIDADINFORMACIONPROYECTO Int,
	@MONTOSUBSIDIOBASEINFORMACIONPROYECTO decimal(18,3),
	@MONTOSUBSIDIOBASEFACTIBILIDADINFORMACIONPROYECTO decimal(18,3),
	@MARCALOCALIZACIONINFORMACIONPROYECTO bit,
	@MARCAFACTIBILIZACIONINFORMACIONPROYECTO bit,
	@FECHAFACTIBILIZACIONINFORMACIONPROYECTO DateTime,
	@NUMEROFAMILIASABSCRITASINFORMACIONPROYECTO Int,
	@CODIGOCOMUNADIRECCION int,
	@CODIGOPROVINCIADIRECCION int,
	@CODIGOREGIONDIRECCION int,
	@RUTENTIDAD int,
	@DIGITOENTIDAD char(1),
	@NOMBREENTIDAD VarChar(200),
	@RUTEMPRESACONSTRUCTORA int,
	@DIGITOEMPRESACONSTRUCTORA char(1),
	@NOMBREEMPRESACONSTRUCTORA VarChar(200),
	@DESCTIPOMODALIDAD VarChar(200)
AS
BEGIN
	BEGIN TRAN
	BEGIN
		--INICIO -- Inserta Direccion
		IF NOT((@CODIGOCOMUNADIRECCION IS NULL) OR (@CODIGOPROVINCIADIRECCION IS NULL) OR (@CODIGOREGIONDIRECCION IS NULL))
		BEGIN
			DECLARE @IDDIRECCION BigInt
			SET @IDDIRECCION = (SELECT MAX(IDDIRECCION) FROM DIRECCION) + 1

			INSERT INTO DIRECCION(IDDIRECCION,NUMERODIRECCION,CODIGOCOMUNADIRECCION,CODIGOPROVINCIADIRECCION,CODIGOREGIONDIRECCION)
			VALUES(@IDDIRECCION,NULL,@CODIGOCOMUNADIRECCION,@CODIGOPROVINCIADIRECCION,@CODIGOREGIONDIRECCION)

			IF(@@ERROR <> 0)
			BEGIN
				ROLLBACK SELECT 'ERROR EN INSERTAR DIRECCION'
			END
		END
		--FIN -- Inserta Direccion

		--INICIO -- Ingreso Proveedor
		DECLARE @Ciclo int
		SET @Ciclo = 1

		DECLARE @IDProveedor1 BigInt,
				@IDProveedor2 BigInt

		SET @IDProveedor1 = 0
		SET @IDProveedor2 = 0

		WHILE(@Ciclo >= 0)
		BEGIN
			DECLARE @IDMAESTROTIPOPROVEEDOR Int,--1 EP / --2 EC
					@RUTPROVEEDOR Int,
					@RUTDVPROVEEDOR Char(1),
					@NOMBREPROVEEDOR VarChar(200)
			
			IF(@Ciclo = 1)
			BEGIN
				SET @IDMAESTROTIPOPROVEEDOR = 1
				SET @RUTPROVEEDOR = @RUTENTIDAD
				SET @RUTDVPROVEEDOR = @DIGITOENTIDAD
				SET @NOMBREPROVEEDOR = @NOMBREENTIDAD
			END
			ELSE
			BEGIN
				SET @IDMAESTROTIPOPROVEEDOR = 2
				SET @RUTPROVEEDOR = @RUTEMPRESACONSTRUCTORA
				SET @RUTDVPROVEEDOR = @DIGITOEMPRESACONSTRUCTORA
				SET @NOMBREPROVEEDOR = @NOMBREEMPRESACONSTRUCTORA
			END

			IF(@RUTPROVEEDOR <> 0)
			BEGIN
				DECLARE @IDPROVEEDOR BigInt

				IF(@Ciclo = 1)
				BEGIN
					SET @IDProveedor1 = @IDPROVEEDOR
				END
				ELSE
				BEGIN
					SET @IDProveedor2 = @IDPROVEEDOR
				END

				IF NOT EXISTS(SELECT 1 FROM PROVEEDOR WHERE RUTPROVEEDOR = @RutProveedor)
				BEGIN
					SET @IDPROVEEDOR = (SELECT MAX(IDPROVEEDOR) FROM PROVEEDOR) + 1

					INSERT INTO PROVEEDOR(IDPROVEEDOR,IDMAESTROTIPOPROVEEDOR,NOMBREPROVEEDOR,RUTPROVEEDOR,DVPROVEDIGITOVERIFICADORPROVEEDOR)
					VALUES(@IDPROVEEDOR,@IDMAESTROTIPOPROVEEDOR,@NOMBREPROVEEDOR,@RUTPROVEEDOR,@RUTDVPROVEEDOR)
				END
				ELSE
				BEGIN
					SET @IDPROVEEDOR = (SELECT IDPROVEEDOR FROM PROVEEDOR WHERE RUTPROVEEDOR = @RutProveedor)
				END
			END

			SET @Ciclo = (@Ciclo - 1)
		END

		IF(@@ERROR <> 0)
		BEGIN
			ROLLBACK SELECT 'ERROR EN INSERTAR PROVEEDOR'
		END
		--FIN -- Inserta Proveedor

		--INICIO -- MODALIDAD
		DECLARE @IDMAESTROMODALIDAD BigInt

		IF NOT(@DESCTIPOMODALIDAD IS NULL)
		BEGIN
			IF(UPPER(LTRIM(RTRIM(@DESCTIPOMODALIDAD))) = 'AVC')
			BEGIN
				SET @IDMAESTROMODALIDAD = 7
			END
			ELSE
			BEGIN
				SET @IDMAESTROMODALIDAD = 2
			END
		END
		ELSE
		BEGIN
			SET @IDMAESTROMODALIDAD = NULL
		END
		--FIN -- MODALIDAD
		
		--INICIO -- Ingreso Proyecto
		DECLARE @IDINFORMACIONPROYECTO BigInt
		
		SET @IDINFORMACIONPROYECTO = (SELECT MAX(IDINFORMACIONPROYECTO) FROM INFORMACION_PROYECTO) + 1

		INSERT INTO INFORMACION_PROYECTO(
			IDINFORMACIONPROYECTO,
			IDDIRECCION,
			IDMAESTROTITULO,
			IDMAESTROMODALIDAD,
			IDMAESTROLLAMADO,
			IDMAESTROALTERNATIVAPOSTULACION,
			CODIGOPROYECTOINFORMACIONPROYECTO,
			NOMBREPROYECTOINFORMACIONPROYECTO,
			FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTO,
			ESTADOPROYECTOINFORMACIONPROYECTO,
			RESOLUCIONATINFORMACIONPROYECTO,
			FECHARESOLUCIONATINFORMACIONPROYECTO,
			CANTIDADVIVIENDASINFORMACIONPROYECTO,
			CANTIDADBENEFICIARIOSINFORMACIONPROYECTO,
			IDMAESTROPROGRAMA,
			AGNOFACTIBILIDADINFORMACIONPROYECTO,
			MONTOSUBSIDIOBASEINFORMACIONPROYECTO,
			MONTOSUBSIDIOBASEFACTIBILIDADINFORMACIONPROYECTO,
			MARCALOCALIZACIONINFORMACIONPROYECTO,
			MARCAFACTIBILIZACIONINFORMACIONPROYECTO,
			FECHAFACTIBILIZACIONINFORMACIONPROYECTO,
			NUMEROFAMILIASADSCRITASINFORMACIONPROYECTO)
		VALUES (@IDINFORMACIONPROYECTO,
				@IDDIRECCION,
				NULL,--Maestro Titulo
				@IDMAESTROMODALIDAD,
				NULL,--MAESTROLLAMADO
				@IDMAESTROALTERNATIVAPOSTULACION,
				@CODIGOPROYECTOINFORMACIONPROYECTO,
				@NOMBREPROYECTOINFORMACIONPROYECTO,
				@FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTO,
				@ESTADOPROYECTOINFORMACIONPROYECTO,
				NULL,--@RESOLUCIONATINFORMACIONPROYECTO
				NULL,--@FECHARESOLUCIONATINFORMACIONPROYECTO
				@CANTIDADVIVIENDASINFORMACIONPROYECTO,
				@CANTIDADBENEFICIARIOSINFORMACIONPROYECTO,
				3,--DS49 (3)
				@AGNOFACTIBILIDADINFORMACIONPROYECTO,
				@MONTOSUBSIDIOBASEINFORMACIONPROYECTO,
				@MONTOSUBSIDIOBASEFACTIBILIDADINFORMACIONPROYECTO,
				@MARCALOCALIZACIONINFORMACIONPROYECTO,
				@MARCAFACTIBILIZACIONINFORMACIONPROYECTO,
				@FECHAFACTIBILIZACIONINFORMACIONPROYECTO,
				@NUMEROFAMILIASABSCRITASINFORMACIONPROYECTO)

		IF(@@ERROR <> 0)
		BEGIN
			ROLLBACK SELECT 'ERROR AL INSERTAR PROYECTO'
		END
		--FIN -- Ingreso Proyecto

		--INICIO -- Ingreso ProveedorNomalizada
		IF(@IDProveedor1 <> 0)
		BEGIN
			INSERT INTO TIPO_PROVEEDOR_INFORMACION_PROYECTO(IDPROVEEDOR,IDINFORMACIONPROYECTO,ESTADOTIPOPROVEEDORINFORMACIONPROYECTO)
			VALUES(@IDProveedor1,@IDINFORMACIONPROYECTO,1)
		END
		ELSE IF(@IDProveedor2 <> 0)
		BEGIN
			INSERT INTO TIPO_PROVEEDOR_INFORMACION_PROYECTO(IDPROVEEDOR,IDINFORMACIONPROYECTO,ESTADOTIPOPROVEEDORINFORMACIONPROYECTO)
			VALUES(@IDProveedor2,@IDINFORMACIONPROYECTO,1)
		END

		IF(@@ERROR <> 0)
		BEGIN
			ROLLBACK SELECT 'ERROR AL INSERTAR TIPOPROVEEDORINFORMACIONPROYECTO'
		END
		--FIN -- Ingreso ProveedorNomalizada

		COMMIT TRAN
			SELECT 'OK'
	END
END



GO
/****** Object:  StoredProcedure [dbo].[INSERTA_PROYECTOS_SNATDS10]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Daniel Orozco
-- Create date: 31/10/2018
-- Description:	Inserta en SNAT Simplificado los proyectos obtenidos desde Rukan
-- =============================================
CREATE PROCEDURE [dbo].[INSERTA_PROYECTOS_SNATDS10]
	@codigoProyecto bigint,
	@nombreProyecto varchar(100),
	@AlternativaPostulacion varchar(100),
	@Titulo varchar(100),
    @regionProyecto int,
	@provinciaProyecto int,
	@comunaProyecto int,
	@AnioLlamado int,
    @NumeroLlamado int,
	@cantidadBeneficiarios int,
	@cantidadViviendas int,
	@Modalidad varchar(100),
    @Tipologia varchar(100),
	@ModalidadEjecucionObra varchar(100),
	--@CodigoEstadoSubsidio varchar(100),
	--@NombreEstadoSubsidio varchar(100),
	--@EsReconstruccion int,
	--@TipoCatastrofe varchar(100),
	--@AnoCatastrofe int,
	@RutPrestadorAsistenciaTecnica varchar(100),
    @PrestadorAsistenciaTecnica varchar(100),
	@estadoProyecto bit,
	@NombreEstadoProyecto varchar(100),
	@EstadoBeneficio bigint
AS
BEGIN
	BEGIN TRAN
	BEGIN
		----------MAESTROBANCO----------
		DECLARE @IDMAESTROBANCO bigint
		SET @IDMAESTROBANCO = 3--No aplica
		----------MAESTROBANCO----------
		
		--------MAESTRO_PROGRAMA--------
		DECLARE	@IDMAESTROPROGRAMA bigint
		SET @IDMAESTROPROGRAMA = 1--DS10
		--------MAESTRO_PROGRAMA--------

		------------------------ID's------------------------
		DECLARE @IDHOMOLOGACIONINFORMACIONSISTEMA bigint,
				@IDINFORMACIONPROYECTO BigInt,
				@IDCARACESP BigInt
		------------------------ID's------------------------

		--------------------------------------------------------MAESTRO_MODALIDAD--------------------------------------------------------
		DECLARE @IDMAESTROMODALIDAD BigInt

		IF EXISTS(SELECT 1 FROM MAESTRO_MODALIDAD WHERE UPPER(LTRIM(RTRIM(NOMBREMAESTROMODALIDAD))) = UPPER(LTRIM(RTRIM(@Modalidad))))
		BEGIN
			SET @IDMAESTROMODALIDAD = (SELECT	IDMAESTROMODALIDAD
											FROM	MAESTRO_MODALIDAD
											WHERE	UPPER(LTRIM(RTRIM(NOMBREMAESTROMODALIDAD))) = UPPER(LTRIM(RTRIM(@Modalidad))))

			IF(@@ERROR <> 0)
			BEGIN
				ROLLBACK SELECT 'ERROR AL OBTENER MODALIDAD'
			END
		END
		ELSE
		BEGIN
			SET @IDMAESTROMODALIDAD = ((SELECT MAX(IDMAESTROMODALIDAD) FROM MAESTRO_MODALIDAD) + 1)

			INSERT INTO MAESTRO_MODALIDAD(IDMAESTROMODALIDAD,NOMBREMAESTROMODALIDAD,ESTADOMAESTROMODALIDAD)
			VALUES(@IDMAESTROMODALIDAD,LTRIM(RTRIM(@Modalidad)),1)

			IF(@@ERROR <> 0)
			BEGIN
				ROLLBACK SELECT 'ERROR AL INSERTAR MODALIDAD'
			END
		END
		--------------------------------------------------------MAESTRO_MODALIDAD--------------------------------------------------------

		--------------------------------------------------------MAESTRO_ALTERNATIVA_POSTULACION--------------------------------------------------------
		DECLARE	@IDMAESTROALTERNATIVAPOSTULACION bigint

		IF EXISTS(SELECT 1 FROM MAESTRO_ALTERNATIVA_POSTULACION WHERE UPPER(LTRIM(RTRIM(NOMBREMAESTROALTERNATIVAPOSTULACION))) = UPPER(LTRIM(RTRIM(@AlternativaPostulacion))))
		BEGIN
			SET @IDMAESTROALTERNATIVAPOSTULACION = (SELECT	IDMAESTROALTERNATIVAPOSTULACION
													FROM	MAESTRO_ALTERNATIVA_POSTULACION
													WHERE	UPPER(LTRIM(RTRIM(NOMBREMAESTROALTERNATIVAPOSTULACION))) = UPPER(LTRIM(RTRIM(@AlternativaPostulacion))))

			IF(@@ERROR <> 0)
			BEGIN
				ROLLBACK SELECT 'ERROR AL OBTENER ALTERNATIVA POSTULACION'
			END
		END
		ELSE
		BEGIN
			SET @IDMAESTROALTERNATIVAPOSTULACION = ((SELECT MAX(IDMAESTROALTERNATIVAPOSTULACION) FROM MAESTRO_ALTERNATIVA_POSTULACION) + 1)
			
			INSERT INTO MAESTRO_ALTERNATIVA_POSTULACION(IDMAESTROALTERNATIVAPOSTULACION,NOMBREMAESTROALTERNATIVAPOSTULACION,ESTADOMAESTROALTERNATIVAPOSTULACION)
			VALUES(@IDMAESTROALTERNATIVAPOSTULACION,LTRIM(RTRIM(@AlternativaPostulacion)),1)

			IF(@@ERROR <> 0)
			BEGIN
				ROLLBACK SELECT 'ERROR AL INSERTAR ALTERNATIVA POSTULACION'
			END
		END
		--------------------------------------------------------MAESTRO_ALTERNATIVA_POSTULACION--------------------------------------------------------

		----------------------------------------------------------TITULO----------------------------------------------------------
		DECLARE	@IDMAESTROTITULO bigint
		
		SET @Titulo = (CASE
							WHEN @Titulo = 'Título I' THEN 'Título 1'
							WHEN @Titulo = 'Título II' THEN 'Título 2'
						END)

		IF EXISTS(SELECT 1 FROM MAESTRO_TITULO WHERE UPPER(LTRIM(RTRIM(NOMBREMAESTROTITULO))) = UPPER(LTRIM(RTRIM(@Titulo))))
		BEGIN
			SET @IDMAESTROTITULO = (SELECT	IDMAESTROTITULO
									FROM	MAESTRO_TITULO
									WHERE	UPPER(LTRIM(RTRIM(NOMBREMAESTROTITULO))) = UPPER(LTRIM(RTRIM(@Titulo))))

			IF(@@ERROR <> 0)
			BEGIN
				ROLLBACK SELECT 'ERROR AL OBTENER TITULO'
			END
		END
		ELSE
		BEGIN
			SET @IDMAESTROTITULO = ((SELECT MAX(IDMAESTROTITULO) FROM MAESTRO_TITULO) + 1)
			
			INSERT INTO MAESTRO_TITULO(IDMAESTROTITULO,NOMBREMAESTROTITULO,ESTADOMAESTROTITULO)
			VALUES(@IDMAESTROTITULO,LTRIM(RTRIM(@Titulo)),1)

			IF(@@ERROR <> 0)
			BEGIN
				ROLLBACK SELECT 'ERROR AL INSERTAR TITULO'
			END
		END
		----------------------------------------------------------TITULO----------------------------------------------------------

		----------------------------------------------------------------DIRECCION----------------------------------------------------------------
		DECLARE @IDDIRECCION BigInt

		IF NOT((@regionProyecto IS NULL) OR (@provinciaProyecto IS NULL) OR (@comunaProyecto IS NULL))
		BEGIN
			DECLARE @NUMERODIRECCION VarChar(4)
			SET @NUMERODIRECCION = 'N/A'

			IF(NOT EXISTS(SELECT	1
							FROM	DIRECCION
							WHERE	RTRIM(LTRIM(UPPER(DESCRIPCIONDIRECCION))) IS NULL
								AND RTRIM(LTRIM(UPPER(NUMERODIRECCION))) = RTRIM(LTRIM(UPPER(@NUMERODIRECCION)))
								AND CODIGOREGIONDIRECCION = @regionProyecto
								AND CODIGOPROVINCIADIRECCION = @provinciaProyecto
								AND CODIGOCOMUNADIRECCION = @comunaProyecto))
			BEGIN
				INSERT INTO DIRECCION(NUMERODIRECCION,DESCRIPCIONDIRECCION,CODIGOCOMUNADIRECCION,CODIGOPROVINCIADIRECCION,CODIGOREGIONDIRECCION)
				VALUES(@NUMERODIRECCION,NULL,@regionProyecto,@provinciaProyecto,@comunaProyecto)
				
				SET @IDDIRECCION = @@IDENTITY

				IF(@@ERROR <> 0)
				BEGIN
					ROLLBACK SELECT 'ERROR AL INSERTAR DIRECCION'
				END
			END
			ELSE
			BEGIN
				SET @IDDIRECCION = (SELECT	IDDIRECCION
									FROM	DIRECCION
									WHERE	RTRIM(LTRIM(UPPER(DESCRIPCIONDIRECCION))) IS NULL
										AND RTRIM(LTRIM(UPPER(NUMERODIRECCION))) = RTRIM(LTRIM(UPPER(@NUMERODIRECCION)))
										AND CODIGOREGIONDIRECCION = @regionProyecto
										AND CODIGOPROVINCIADIRECCION = @provinciaProyecto
										AND CODIGOCOMUNADIRECCION = @comunaProyecto)

				IF(@@ERROR <> 0)
				BEGIN
					ROLLBACK SELECT 'ERROR AL OBTENER DIRECCION'
				END
			END
		END
		----------------------------------------------------------------DIRECCION----------------------------------------------------------------

		--------------------------------------------------------------MAESTRO_ESTADO_PROYECTO--------------------------------------------------------------
		DECLARE @IDMAESTROESTADOPROYECTO BigInt
		
		IF(@NombreEstadoProyecto != NULL)
		BEGIN
			IF EXISTS(SELECT 1 FROM MAESTRO_ESTADO_PROYECTO WHERE UPPER(LTRIM(RTRIM(NOMBREMAESTROESTADOPROYECTO))) = UPPER(LTRIM(RTRIM(@NombreEstadoProyecto))))
			BEGIN
				SET @IDMAESTROESTADOPROYECTO = (SELECT	IDMAESTROESTADOPROYECTO
												FROM	MAESTRO_ESTADO_PROYECTO
												WHERE	UPPER(LTRIM(RTRIM(NOMBREMAESTROESTADOPROYECTO))) = UPPER(LTRIM(RTRIM(@NombreEstadoProyecto))))

				IF(@@ERROR <> 0)
				BEGIN
					ROLLBACK SELECT 'ERROR AL OBTENER ESTADO PROYECTO'
				END
			END
			ELSE
			BEGIN
				INSERT INTO MAESTRO_ESTADO_PROYECTO(NOMBREMAESTROESTADOPROYECTO,ESTADOMAESTROESTADOPROYECTO)
				VALUES(LTRIM(RTRIM(@NombreEstadoProyecto)),1)

				SET @IDMAESTROESTADOPROYECTO = @@IDENTITY

				IF(@@ERROR <> 0)
				BEGIN
					ROLLBACK SELECT 'ERROR AL INSERTAR ESTADO PROYECTO'
				END
			END
		END
		--------------------------------------------------------------MAESTRO_ESTADO_PROYECTO--------------------------------------------------------------

		----------------------------------------------------------------TIPOLOGIA----------------------------------------------------------------
		DECLARE @IDMAESTROTIPOLOGIA BigInt

		SET @IDHOMOLOGACIONINFORMACIONSISTEMA = (SELECT	IDHOMOLOGACIONINFORMACIONSISTEMA
												FROM	HOMOLOGACION_INFORMACION_SISTEMA
												WHERE	IDMAESTROINFORMACIONSISTEMAEXTERNO = (SELECT	IDMAESTROINFORMACIONSISTEMAEXTERNO
																								FROM	MAESTRO_INFORMACION_SISTEMA_EXTERNO
																								WHERE	UPPER(LTRIM(RTRIM(DESCRIPCIONMAESTROINFORMACIONSISTEMAEXTERNO))) = UPPER(LTRIM(RTRIM(@Tipologia))))
													AND NOMBRETABLAHOMOLOGACIONINFORMACIONSISTEMA = 'MAESTRO_TIPOLOGIA')

		SET @IDMAESTROTIPOLOGIA = (SELECT	CODIGOSISTEMAINTERNOHOMOLOGACIONINFORMACIONSISTEMA
									FROM	HOMOLOGACION_INFORMACION_SISTEMA
									WHERE	IDHOMOLOGACIONINFORMACIONSISTEMA = @IDHOMOLOGACIONINFORMACIONSISTEMA)

		IF(@IDMAESTROTIPOLOGIA IS NULL)
		BEGIN
			ROLLBACK SELECT 'ERROR, NO SE ENCUENTRA TIPOLOGÍA'
		END
		----------------------------------------------------------------TIPOLOGIA----------------------------------------------------------------

		----------------------------------------------------------------------------------------------MAESTRO_SUB_MODALIDAD----------------------------------------------------------------------------------------------
		DECLARE @IDSUBMODALIDAD bigint

		SET @IDHOMOLOGACIONINFORMACIONSISTEMA = (SELECT	IDHOMOLOGACIONINFORMACIONSISTEMA
												FROM	HOMOLOGACION_INFORMACION_SISTEMA
												WHERE	IDMAESTROINFORMACIONSISTEMAEXTERNO = (SELECT	IDMAESTROINFORMACIONSISTEMAEXTERNO
																								FROM	MAESTRO_INFORMACION_SISTEMA_EXTERNO
																								WHERE	UPPER(LTRIM(RTRIM(DESCRIPCIONMAESTROINFORMACIONSISTEMAEXTERNO))) = UPPER(LTRIM(RTRIM(@ModalidadEjecucionObra))))
												AND NOMBRETABLAHOMOLOGACIONINFORMACIONSISTEMA = 'MAESTRO_SUB_MODALIDAD')
		--SET @IDHOMOLOGACIONINFORMACIONSISTEMA = (CASE
		--											WHEN UPPER(LTRIM(RTRIM(@ModalidadEjecucionObra))) = UPPER('AutoconstruccionAsistida') THEN 1 -- ACA
		--											WHEN UPPER(LTRIM(RTRIM(@ModalidadEjecucionObra))) = UPPER('Mixto') THEN 2 -- ACA
		--											WHEN UPPER(LTRIM(RTRIM(@ModalidadEjecucionObra))) = UPPER('Contrato de Construcción') THEN 3 -- Pago por Avance
		--											WHEN UPPER(LTRIM(RTRIM(@ModalidadEjecucionObra))) = UPPER('PagoporAvance') THEN 4 -- Pago por Avance
		--										END)

		SET @IDSUBMODALIDAD = (SELECT	CODIGOSISTEMAINTERNOHOMOLOGACIONINFORMACIONSISTEMA
								FROM	HOMOLOGACION_INFORMACION_SISTEMA
								WHERE	IDHOMOLOGACIONINFORMACIONSISTEMA = @IDHOMOLOGACIONINFORMACIONSISTEMA)

		IF(@IDSUBMODALIDAD IS NULL)
		BEGIN
			ROLLBACK SELECT 'ERROR, NO SE ENCUENTRA SUB MODALIDAD'
		END
		----------------------------------------------------------------------------------------------MAESTRO_SUB_MODALIDAD----------------------------------------------------------------------------------------------

		------------------------------------------------------------------------------------------MAESTRO_CLASE------------------------------------------------------------------------------------------
		DECLARE @IDMAESTROCLASE bigint

		SET @IDHOMOLOGACIONINFORMACIONSISTEMA = (SELECT	IDHOMOLOGACIONINFORMACIONSISTEMA
												FROM	HOMOLOGACION_INFORMACION_SISTEMA
												WHERE	IDMAESTROINFORMACIONSISTEMAEXTERNO = (SELECT	IDMAESTROINFORMACIONSISTEMAEXTERNO
																								FROM	MAESTRO_INFORMACION_SISTEMA_EXTERNO
																								WHERE	UPPER(LTRIM(RTRIM(DESCRIPCIONMAESTROINFORMACIONSISTEMAEXTERNO))) = UPPER(LTRIM(RTRIM(@Tipologia))))
													AND NOMBRETABLAHOMOLOGACIONINFORMACIONSISTEMA = 'MAESTRO_CLASE')

		SET @IDMAESTROCLASE = (SELECT	CODIGOSISTEMAINTERNOHOMOLOGACIONINFORMACIONSISTEMA
								FROM	HOMOLOGACION_INFORMACION_SISTEMA
								WHERE	IDHOMOLOGACIONINFORMACIONSISTEMA = @IDHOMOLOGACIONINFORMACIONSISTEMA)

		IF(@IDMAESTROCLASE IS NULL)
		BEGIN
			ROLLBACK SELECT 'ERROR, NO SE ENCUENTRA CLASE'
		END
		------------------------------------------------------------------------------------------MAESTRO_CLASE------------------------------------------------------------------------------------------

		
		--VERIFICA SI PROYECTO EXISTE Y MODIFICAR DATOS SI ASI ES, SI NO INSERTA NUEVO
		IF(EXISTS(SELECT 1 FROM INFORMACION_PROYECTO WHERE CODIGOPROYECTOINFORMACIONPROYECTO = @codigoProyecto AND IDMAESTROPROGRAMA = @IDMAESTROPROGRAMA))
		BEGIN
			------------------------------------Actualiza Proyecto------------------------------------
			SELECT	@IDINFORMACIONPROYECTO = IDINFORMACIONPROYECTO
			FROM	INFORMACION_PROYECTO
			WHERE	CODIGOPROYECTOINFORMACIONPROYECTO = @codigoProyecto
				AND IDMAESTROPROGRAMA = @IDMAESTROPROGRAMA

			UPDATE	INFORMACION_PROYECTO
			SET IDDIRECCION = @IDDIRECCION,
				IDMAESTROTITULO = @IDMAESTROTITULO,
				IDMAESTROMODALIDAD = @IDMAESTROMODALIDAD,
				IDMAESTROALTERNATIVAPOSTULACION = @IDMAESTROALTERNATIVAPOSTULACION,
				NOMBREPROYECTOINFORMACIONPROYECTO = @nombreProyecto,
				ESTADOPROYECTOINFORMACIONPROYECTO = @estadoProyecto,
				CANTIDADVIVIENDASINFORMACIONPROYECTO = @cantidadViviendas,
				CANTIDADBENEFICIARIOSINFORMACIONPROYECTO = @cantidadBeneficiarios,
				IDMAESTROESTADOPROYECTO = @IDMAESTROESTADOPROYECTO,
				IDMAESTROESTADOBENEFICIO = @EstadoBeneficio,
				IDMAESTROBANCO = @IDMAESTROBANCO
			WHERE	IDINFORMACIONPROYECTO = @IDINFORMACIONPROYECTO
			
			IF(@@ERROR <> 0)
			BEGIN
				ROLLBACK SELECT 'ERROR AL ACTUALIZAR PROYECTO'
			END
			------------------------------------Actualiza Proyecto------------------------------------

			----------------Actualiza CARACTERISTICAS_ESPECIALES----------------
			UPDATE	CARACTERISTICAS_ESPECIALES
			SET IDMAESTROMODALIDAD = @IDMAESTROMODALIDAD,
				IDMAESTROTIPOLOGIA = @IDMAESTROTIPOLOGIA,
				IDINFORMACIONPROYECTO = @IDINFORMACIONPROYECTO,
				IDMAESTROCLASE = @IDMAESTROCLASE,
				IDMAESTROSUBMODALIDAD = @IDSUBMODALIDAD
			WHERE	IDINFORMACIONPROYECTO = @IDINFORMACIONPROYECTO

			SELECT	@IDCARACESP = IDCARACTERISTICASESPECIALES
			FROM	CARACTERISTICAS_ESPECIALES
			WHERE	IDINFORMACIONPROYECTO = @IDINFORMACIONPROYECTO--14

			IF(@@ERROR <> 0)
			BEGIN
				ROLLBACK SELECT 'ERROR AL INSERTAR CARACTERISTICAS_ESPECIALES'
			END
			----------------Actualiza CARACTERISTICAS_ESPECIALES----------------
		END
		ELSE --NUEVO INGRESO
		BEGIN
			----------------------------------------INICIO -- Ingreso Proyecto----------------------------------------
			INSERT INTO INFORMACION_PROYECTO(IDDIRECCION,
											IDMAESTROTITULO,
											IDMAESTROMODALIDAD,
											IDMAESTROLLAMADO,
											IDMAESTROALTERNATIVAPOSTULACION,
											CODIGOPROYECTOINFORMACIONPROYECTO,
											NOMBREPROYECTOINFORMACIONPROYECTO,
											FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTO,
											ESTADOPROYECTOINFORMACIONPROYECTO,
											RESOLUCIONATINFORMACIONPROYECTO,
											FECHARESOLUCIONATINFORMACIONPROYECTO,
											CANTIDADVIVIENDASINFORMACIONPROYECTO,
											CANTIDADBENEFICIARIOSINFORMACIONPROYECTO,
											IDMAESTROPROGRAMA,
											AGNOFACTIBILIDADINFORMACIONPROYECTO,
											MONTOSUBSIDIOBASEINFORMACIONPROYECTO,
											MONTOSUBSIDIOBASEFACTIBILIDADINFORMACIONPROYECTO,
											MARCALOCALIZACIONINFORMACIONPROYECTO,
											MARCAFACTIBILIZACIONINFORMACIONPROYECTO,
											FECHAFACTIBILIZACIONINFORMACIONPROYECTO,
											NUMEROFAMILIASADSCRITASINFORMACIONPROYECTO,
											IDMAESTROESTADOPROYECTO,
											IDMAESTROESTADOBENEFICIO,
											IDMAESTROBANCO,
											AVANCEOBRAINFORMACIONPROYECTO,
											ESTADOAVANCEOBRAINFORMACIONPROYECTO,
											PERMISOUSUARIOADMINISTRATIVOINFORMACIONPROYECTO,
											NOMBREUSUARIOOTORGAPERMISOINFORMACIONPROYECTO)
			VALUES (@IDDIRECCION,--Direccion
					@IDMAESTROTITULO,--Maestro Titulo
					@IDMAESTROMODALIDAD,
					NULL,--MAESTROLLAMADO
					@IDMAESTROALTERNATIVAPOSTULACION,--MAESTRO_ALTERNATIVA_POSTULACION
					@codigoProyecto,
					@nombreProyecto,
					NULL,--@FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTO
					@estadoProyecto,--@ESTADOPROYECTOINFORMACIONPROYECTO
					NULL,--@RESOLUCIONATINFORMACIONPROYECTO
					NULL,--@FECHARESOLUCIONATINFORMACIONPROYECTO
					@cantidadViviendas,
					@cantidadBeneficiarios,
					@IDMAESTROPROGRAMA,--DS10
					NULL,--@AGNOFACTIBILIDADINFORMACIONPROYECTO
					NULL,--@MONTOSUBSIDIOBASEINFORMACIONPROYECTO
					NULL,--@MONTOSUBSIDIOBASEFACTIBILIDADINFORMACIONPROYECTO
					NULL,--@MARCALOCALIZACIONINFORMACIONPROYECTO
					NULL,--@MARCAFACTIBILIZACIONINFORMACIONPROYECTO
					NULL,--@FECHAFACTIBILIZACIONINFORMACIONPROYECTO
					NULL,--@NUMEROFAMILIASABSCRITASINFORMACIONPROYECTO
					@IDMAESTROESTADOPROYECTO,
					@EstadoBeneficio,
					@IDMAESTROBANCO,
					NULL,--AVANCEOBRAINFORMACIONPROYECTO,
					NULL,--ESTADOAVANCEOBRAINFORMACIONPROYECTO,
					NULL,--PERMISOUSUARIOADMINISTRATIVOINFORMACIONPROYECTO,
					NULL)--NOMBREUSUARIOOTORGAPERMISOINFORMACIONPROYECTO

			SET @IDINFORMACIONPROYECTO = @@IDENTITY

			IF(@@ERROR <> 0)
			BEGIN
				ROLLBACK SELECT 'ERROR AL INSERTAR PROYECTO'
			END
			----------------------------------------FIN -- Ingreso Proyecto----------------------------------------

			--------------------------------------------CARACTERISTICAS_ESPECIALES--------------------------------------------
			INSERT INTO CARACTERISTICAS_ESPECIALES(IDMAESTROPROGRAMA,
													IDMAESTROMODALIDAD,
													IDMAESTROTIPOLOGIA,
													IDINFORMACIONPROYECTO,
													IDMAESTROCLASE,
													IDMAESTROSUBMODALIDAD)
			VALUES(1, @IDMAESTROMODALIDAD, @IDMAESTROTIPOLOGIA, @IDINFORMACIONPROYECTO, @IDMAESTROCLASE, @IDSUBMODALIDAD)

			--Obtencion del Identity recien ingresado--
			SET @IDCARACESP = @@IDENTITY
			-------------------------------------------
		
			IF(@@ERROR <> 0)
			BEGIN
				ROLLBACK SELECT 'ERROR AL INSERTAR CARACTERISTICAS_ESPECIALES'
			END
			--------------------------------------------CARACTERISTICAS_ESPECIALES--------------------------------------------

			--------------------------------------INICIO -- Ingreso TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA--------------------------------------
			------------------------------------Obtencion de Plantilla------------------------------------
			DECLARE @NUMPTLLA Int
		
			SET @NUMPTLLA = (SELECT	DISTINCT sp.NUMEROPLANTILLASERVICIOPARCIALIDAD
							FROM	SERVICIO_PARCIALIDAD sp
								INNER JOIN TIPOLOGIA_SERVICIO ts
									ON sp.IDTIPOLOGIASERVICIO = ts.IDTIPOLOGIASERVICIO
								INNER JOIN MAESTRO_TIPOLOGIA mt
									ON ts.IDMAESTROTIPOLOGIA = mt.IDMAESTROTIPOLOGIA
								INNER JOIN MAESTRO_SERVICIO ms
									ON ts.IDMAESTROSERVICIO = ms.IDMAESTROSERVICIO
								INNER JOIN SUB_MODALIDAD_PARCIALIDAD smp
									ON sp.IDSUBMODALIDADPARCIALIDAD = smp.IDSUBMODALIDADPARCIALIDAD
								INNER JOIN MAESTRO_SUB_MODALIDAD msm
									ON smp.IDMAESTROSUBMODALIDAD = msm.IDMAESTROSUBMODALIDAD
								INNER JOIN MAESTRO_PARCIALIDAD mp
									ON smp.IDMAESTROPARCIALIDAD = mp.IDMAESTROPARCIALIDAD
							WHERE	sp.IDMAESTROPROGRAMA = @IDMAESTROPROGRAMA
								AND ts.IDMAESTROTIPOLOGIA = @IDMAESTROTIPOLOGIA
								AND msm.IDMAESTROSUBMODALIDAD = @IDSUBMODALIDAD)

			IF(@@ERROR <> 0)
			BEGIN
				ROLLBACK SELECT 'ERROR AL OBTENER PLANTILLA'
			END

			DECLARE @SERVICIOPARCIALIDAD TABLE (IDSERVPARC BigInt)

			INSERT INTO @SERVICIOPARCIALIDAD (IDSERVPARC)
			SELECT	IDSERVICIOPARCIALIDAD
			FROM	SERVICIO_PARCIALIDAD
			WHERE	NUMEROPLANTILLASERVICIOPARCIALIDAD = @NUMPTLLA
			ORDER BY IDSERVICIOPARCIALIDAD
			----------------------------------------------------------------------------------------------

			WHILE((SELECT COUNT(1) FROM @SERVICIOPARCIALIDAD) > 0)
			BEGIN
				DECLARE @IDSERVPARC BigInt
				SET @IDSERVPARC = (SELECT TOP 1 IDSERVPARC FROM @SERVICIOPARCIALIDAD)

				INSERT INTO	TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA(IDSERVICIOPARCIALIDAD,
															IDCARACTERISTICASESPECIALES,
															ESTADOTIPOSERVICIOPARCIALIDADCARACTERISTICA,
															MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA,
															MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA,
															PORCENTAJEPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA,
															IDSOLICITUDPAGO)
				VALUES(@IDSERVPARC, @IDCARACESP, 1, NULL, NULL, NULL, NULL)

				DELETE FROM @SERVICIOPARCIALIDAD WHERE IDSERVPARC = @IDSERVPARC
			END

			IF(@@ERROR <> 0)
			BEGIN
				ROLLBACK SELECT 'ERROR AL INSERTAR TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA'
			END
			--------------------------------------FIN -- Ingreso TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA--------------------------------------

			------------------------------------------INICIO -- Ingreso TIPO_INCREMENTO----------------------------------------
			----Creacion tabla con Incrementos para recorrer
			--DECLARE @INCREMENTO TABLE (IDMAESTROINCREMENTO BigInt)
		
			--INSERT INTO @INCREMENTO
			--SELECT	IDMAESTROINCREMENTO
			--FROM	MAESTRO_INCREMENTO

			--WHILE ((SELECT COUNT(1) FROM @INCREMENTO) > 0)
			--BEGIN
			--	DECLARE @IDMAESTROINCREMENTO BigInt
			--	SET @IDMAESTROINCREMENTO = (SELECT TOP 1 IDMAESTROINCREMENTO FROM @INCREMENTO ORDER BY IDMAESTROINCREMENTO)

			--	INSERT INTO TIPO_INCREMENTO(IDMAESTROINCREMENTO,
			--								IDCARACTERISTICASESPECIALES,
			--								IDSOLICITUDPAGO,
			--								SELECCIONADOTIPOINCREMENTO,
			--								ESTADOTIPOINCREMENTO)
			--	VALUES(@IDMAESTROINCREMENTO, @IDCARACESP, NULL, 0, 1)

			--	--Se Elimina el ya ingresado
			--	DELETE FROM @INCREMENTO WHERE IDMAESTROINCREMENTO = @IDMAESTROINCREMENTO
			--END

			--IF(@@ERROR <> 0)
			--BEGIN
			--	ROLLBACK SELECT 'ERROR AL INSERTAR TIPO_INCREMENTO'
			--END
			--------------------------------------------FIN -- Ingreso TIPO_INCREMENTO------------------------------------------
		END
		
		COMMIT TRAN
			SELECT 'OK'
	END
END


GO
/****** Object:  StoredProcedure [dbo].[INSERTA_PROYECTOS_SNATDS19]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
/********************************************************************************************        
* Nombre del procedimiento      :  INSERTA_PROYECTOS_SNATDS19        
* Fecha de creación             :  29/11/2018        
* Usuario de creación           :  DOrozco        
* Versión.                      :  V.0.0.0.1        
  
* Fecha de modificación         :    
* Usuarios de modificación      :    
* Motivo de modificación        :    
  
* Fecha de modificación         :    
* Usuarios de modificación      :    
* Motivo de modificación        :        
        
* Visado por DBA                : Mirto Blanco
* Fecha Aprobación DBA          : 20190708
* Comentarios DBA               :     ---          
  
* Objetivo                      : Inserta en SNAT Simplificado los proyectos obtenidos desde Rukan    
* Objetos Utilizados y para Que : Tablas          
  
   INSERTA_PROYECTOS_SNATDS19 130689, 'EL ROSARIO II', 1, 137, 138, 8108, 81, 8, 'CAMINO A CORONEL', 8165, 'Seleccionado', NULL, 3, 'Regular', 100                   
            
                              
**********************************************************************************************/    
  
CREATE PROCEDURE [dbo].[INSERTA_PROYECTOS_SNATDS19]    
 @CODIGOPROYECTOINFORMACIONPROYECTO BigInt,    
 @NOMBREPROYECTOINFORMACIONPROYECTO VarChar(200),    
 @ESTADOPROYECTOINFORMACIONPROYECTO bit,    
 @CANTIDADVIVIENDASINFORMACIONPROYECTO int,    
 @CANTIDADBENEFICIARIOSINFORMACIONPROYECTO int,    
 @CODIGOCOMUNADIRECCION int,    
 @CODIGOPROVINCIADIRECCION int,    
 @CODIGOREGIONDIRECCION int,    
 @DESCDIRECCION VarChar(200),    
 @NUMERODIRECCION VarChar(100),    
 @MAESTROESTADOPROYECTO VarChar(100),    
 @ESTADOBENEFICIO bigint,    
 @IDMAESTROBANCO bigint,    
 @TIPOLLAMADO varchar(40),    
 @AVANCEOBRA int    
AS    
BEGIN  
      
 DECLARE @NUMPTLLA   Int    
    DECLARE @IDSERVPARC   BigInt  
 DECLARE @IDMAESTROPROGRAMA BigInt  
 DECLARE @IDDIRECCION  BigInt  
 DECLARE @SERVICIOPARCIALIDAD TABLE (IDSERVPARC BigInt)   
    
 SET     @NUMPTLLA = 30 --Numero de plantilla ingresado  
 SET     @IDMAESTROPROGRAMA = 5 --DS19        
      
 BEGIN TRAN    
    BEGIN  --Programa DS19     
            
  ---------------------------------------------------------Inserta Direccion---------------------------------------------------------   
     
        IF NOT((@CODIGOCOMUNADIRECCION IS NULL) OR (@CODIGOPROVINCIADIRECCION IS NULL) OR (@CODIGOREGIONDIRECCION IS NULL))    
        BEGIN    
            IF(ISNUMERIC(@NUMERODIRECCION) = 0)    
            BEGIN    
                SET @NUMERODIRECCION = 'N/A'    
            END    
      
            IF(NOT EXISTS(SELECT 1    
               FROM DIRECCION    
               WHERE RTRIM(LTRIM(UPPER(DESCRIPCIONDIRECCION))) = RTRIM(LTRIM(UPPER(@DESCDIRECCION)))    
                 AND RTRIM(LTRIM(UPPER(NUMERODIRECCION))) = RTRIM(LTRIM(UPPER(@NUMERODIRECCION)))    
                 AND CODIGOREGIONDIRECCION = @CODIGOREGIONDIRECCION    
                 AND CODIGOPROVINCIADIRECCION = @CODIGOPROVINCIADIRECCION    
                 AND CODIGOCOMUNADIRECCION = @CODIGOCOMUNADIRECCION))    
            BEGIN    
               INSERT INTO DIRECCION(NUMERODIRECCION,DESCRIPCIONDIRECCION,CODIGOCOMUNADIRECCION,CODIGOPROVINCIADIRECCION,CODIGOREGIONDIRECCION)    
               VALUES(@NUMERODIRECCION,@DESCDIRECCION,@CODIGOCOMUNADIRECCION,@CODIGOPROVINCIADIRECCION,@CODIGOREGIONDIRECCION)    
               SET @IDDIRECCION = @@IDENTITY    
            END    
            ELSE    
            BEGIN    
               SET   @IDDIRECCION = (SELECT IDDIRECCION    
               FROM  DIRECCION    
               WHERE RTRIM(LTRIM(UPPER(DESCRIPCIONDIRECCION))) = RTRIM(LTRIM(UPPER(@DESCDIRECCION)))    
                 AND RTRIM(LTRIM(UPPER(NUMERODIRECCION))) = RTRIM(LTRIM(UPPER(@NUMERODIRECCION)))    
                 AND CODIGOREGIONDIRECCION = @CODIGOREGIONDIRECCION    
                 AND CODIGOPROVINCIADIRECCION = @CODIGOPROVINCIADIRECCION    
                 AND CODIGOCOMUNADIRECCION = @CODIGOCOMUNADIRECCION)    
            END    
  
            IF(@@ERROR <> 0)    
            BEGIN    
                ROLLBACK SELECT 'ERROR AL INSERTAR DIRECCION'    
            END    
        END    
  ---------------------------------------------------------Inserta Direccion---------------------------------------------------------    
    
  ---------------------------------------------------------MAESTRO_ESTADO_PROYECTO----------------------------------------------------------------    
        DECLARE @IDMAESTROESTADOPROYECTO BigInt    
    
        IF EXISTS(SELECT 1   
               FROM   MAESTRO_ESTADO_PROYECTO   
               WHERE  UPPER(LTRIM(RTRIM(NOMBREMAESTROESTADOPROYECTO))) = UPPER(LTRIM(RTRIM(@MAESTROESTADOPROYECTO))))    
        BEGIN    
            SET @IDMAESTROESTADOPROYECTO = (SELECT IDMAESTROESTADOPROYECTO    
            FROM MAESTRO_ESTADO_PROYECTO    
            WHERE UPPER(LTRIM(RTRIM(NOMBREMAESTROESTADOPROYECTO))) = UPPER(LTRIM(RTRIM(@MAESTROESTADOPROYECTO))))    
        END    
        ELSE    
        BEGIN    
            INSERT INTO MAESTRO_ESTADO_PROYECTO(NOMBREMAESTROESTADOPROYECTO,ESTADOMAESTROESTADOPROYECTO)    
            VALUES(LTRIM(RTRIM(@MAESTROESTADOPROYECTO)),1)    
    
            SET @IDMAESTROESTADOPROYECTO = @@IDENTITY    
    
            IF(@@ERROR <> 0)    
            BEGIN    
                ROLLBACK SELECT 'ERROR AL INSERTAR ESTADO PROYECTO'    
            END    
        END  
  ----------------------------------------------------------------MAESTRO_ESTADO_PROYECTO----------------------------------------------------------------    
    
  ----------------------------------MAESTRO_LLAMADO----------------------------------    
        DECLARE @IDMAESTROLLAMADO bigint    
    
        SET @IDMAESTROLLAMADO = (CASE    
        WHEN UPPER(RTRIM(LTRIM(@TIPOLLAMADO))) = 'REGULAR' THEN 1    
        END)    
  ----------------------------------MAESTRO_LLAMADO----------------------------------    
    
  --------------Variables ID--------------    
        DECLARE @IDINFORMACIONPROYECTO BigInt,    
                @IDMAESTROMODALIDAD    BigInt,    
                @IDCARACESP            BigInt    
  --------------Variables ID--------------    
    
  ------------- VERIFICA SI PROYECTO EXISTE Y MODIFICAR DATOS SI ASI ES, SI NO INSERTA NUEVO    
        IF(EXISTS(SELECT 1 FROM INFORMACION_PROYECTO   
            WHERE CODIGOPROYECTOINFORMACIONPROYECTO = @CODIGOPROYECTOINFORMACIONPROYECTO AND IDMAESTROPROGRAMA = @IDMAESTROPROGRAMA))    
        BEGIN   
   ------------------------------------Actualiza Proyecto------------------------------------    
            SELECT @IDINFORMACIONPROYECTO = IDINFORMACIONPROYECTO    
              FROM INFORMACION_PROYECTO    
             WHERE CODIGOPROYECTOINFORMACIONPROYECTO = @CODIGOPROYECTOINFORMACIONPROYECTO    
               AND IDMAESTROPROGRAMA = @IDMAESTROPROGRAMA    
              
            UPDATE INFORMACION_PROYECTO    
               SET IDDIRECCION =                              @IDDIRECCION,    
                   IDMAESTROMODALIDAD =                       @IDMAESTROMODALIDAD,    
                   NOMBREPROYECTOINFORMACIONPROYECTO =        @NOMBREPROYECTOINFORMACIONPROYECTO,    
                   CANTIDADVIVIENDASINFORMACIONPROYECTO =     @CANTIDADVIVIENDASINFORMACIONPROYECTO,    
                   CANTIDADBENEFICIARIOSINFORMACIONPROYECTO = @CANTIDADBENEFICIARIOSINFORMACIONPROYECTO,    
                   IDMAESTROESTADOPROYECTO =                  @IDMAESTROESTADOPROYECTO,    
                   IDMAESTROESTADOBENEFICIO =                 @ESTADOBENEFICIO,    
                   IDMAESTROBANCO =                           @IDMAESTROBANCO    
            WHERE IDINFORMACIONPROYECTO = @IDINFORMACIONPROYECTO    
                
            IF(@@ERROR <> 0)    
            BEGIN    
                ROLLBACK SELECT 'ERROR AL ACTUALIZAR PROYECTO'    
            END    
   ------------------------------------Actualiza Proyecto------------------------------------    
    
   ----------------Actualiza CARACTERISTICAS_ESPECIALES----------------    
            UPDATE CARACTERISTICAS_ESPECIALES    
               SET IDMAESTROMODALIDAD =    @IDMAESTROMODALIDAD    
             WHERE IDINFORMACIONPROYECTO = @IDINFORMACIONPROYECTO    
              
            SELECT @IDCARACESP = IDCARACTERISTICASESPECIALES    
            FROM   CARACTERISTICAS_ESPECIALES    
            WHERE  IDINFORMACIONPROYECTO = @IDINFORMACIONPROYECTO--14    
              
            IF(@@ERROR <> 0)    
            BEGIN    
                ROLLBACK SELECT 'ERROR AL INSERTAR CARACTERISTICAS_ESPECIALES'    
            END    
   ----------------Actualiza CARACTERISTICAS_ESPECIALES----------------   
     
   -------------------------Verificar si existe registro TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA----------------------------    
   --------------------------Obtencion de Plantilla--------------------------    
             
            INSERT INTO @SERVICIOPARCIALIDAD (IDSERVPARC)    
            SELECT      IDSERVICIOPARCIALIDAD    
            FROM        SERVICIO_PARCIALIDAD    
            WHERE       NUMEROPLANTILLASERVICIOPARCIALIDAD = @NUMPTLLA    
            ORDER BY    IDSERVICIOPARCIALIDAD    
   --------------------------------------------------------------------------    
    
            WHILE((SELECT COUNT(1) FROM @SERVICIOPARCIALIDAD) > 0)    
            BEGIN   
          
                 
               SET @IDSERVPARC = (SELECT TOP 1 IDSERVPARC FROM @SERVICIOPARCIALIDAD)  
        
      IF ((SELECT COUNT(1) FROM TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA a,   
                                CARACTERISTICAS_ESPECIALES b  
           WHERE  IDSERVICIOPARCIALIDAD = @IDSERVPARC  
       AND  a.IDCARACTERISTICASESPECIALES = b.IDCARACTERISTICASESPECIALES  
       AND  b.IDINFORMACIONPROYECTO = @IDINFORMACIONPROYECTO) = 0)    
                BEGIN  
                    INSERT INTO TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA(IDSERVICIOPARCIALIDAD,    
                               IDCARACTERISTICASESPECIALES,    
                               ESTADOTIPOSERVICIOPARCIALIDADCARACTERISTICA,    
                               MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA,    
                               MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA,    
                               PORCENTAJEPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA,    
                               IDSOLICITUDPAGO)    
                    VALUES(@IDSERVPARC, @IDCARACESP, 1, NULL, NULL, NULL, NULL)    
         END  
          
                DELETE FROM @SERVICIOPARCIALIDAD WHERE IDSERVPARC = @IDSERVPARC  
            END    
              
  
            IF(@@ERROR <> 0)    
            BEGIN    
                ROLLBACK SELECT 'ERROR AL INSERTAR TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA'    
            END    
   ----------------------------FIN -- Ingreso TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA----------------------------    
     
      
        END    
        ELSE --NUEVO INGRESO    
        BEGIN    
   ----------------------------------INICIO -- Ingreso Proyecto----------------------------------    
            SET @IDMAESTROMODALIDAD = 1--N/A    
              
            INSERT INTO INFORMACION_PROYECTO(IDDIRECCION,    
                    IDMAESTROTITULO,    
                    IDMAESTROMODALIDAD,    
                    IDMAESTROLLAMADO,    
                    IDMAESTROALTERNATIVAPOSTULACION,    
                    CODIGOPROYECTOINFORMACIONPROYECTO,    
                    NOMBREPROYECTOINFORMACIONPROYECTO,    
                    FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTO,    
                    ESTADOPROYECTOINFORMACIONPROYECTO,    
                    RESOLUCIONATINFORMACIONPROYECTO,    
                    FECHARESOLUCIONATINFORMACIONPROYECTO,    
                    CANTIDADVIVIENDASINFORMACIONPROYECTO,    
                    CANTIDADBENEFICIARIOSINFORMACIONPROYECTO,    
                    IDMAESTROPROGRAMA,    
                    AGNOFACTIBILIDADINFORMACIONPROYECTO,    
   MONTOSUBSIDIOBASEINFORMACIONPROYECTO,    
                    MONTOSUBSIDIOBASEFACTIBILIDADINFORMACIONPROYECTO,    
                    MARCALOCALIZACIONINFORMACIONPROYECTO,    
                    MARCAFACTIBILIZACIONINFORMACIONPROYECTO,    
                    FECHAFACTIBILIZACIONINFORMACIONPROYECTO,    
                    NUMEROFAMILIASADSCRITASINFORMACIONPROYECTO,    
                    IDMAESTROESTADOPROYECTO,    
                    IDMAESTROESTADOBENEFICIO,    
                    IDMAESTROBANCO,    
                    AVANCEOBRAINFORMACIONPROYECTO)    
            VALUES (@IDDIRECCION,    
              NULL,--Maestro Titulo    
              @IDMAESTROMODALIDAD,    
              @IDMAESTROLLAMADO,--MAESTROLLAMADO    
              NULL,--@IDMAESTROALTERNATIVAPOSTULACION    
              @CODIGOPROYECTOINFORMACIONPROYECTO,    
              @NOMBREPROYECTOINFORMACIONPROYECTO,    
              NULL,--@FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTO    
              NULL,--@ESTADOPROYECTOINFORMACIONPROYECTO    
              NULL,--@RESOLUCIONATINFORMACIONPROYECTO    
              NULL,--@FECHARESOLUCIONATINFORMACIONPROYECTO    
              @CANTIDADVIVIENDASINFORMACIONPROYECTO,    
              @CANTIDADBENEFICIARIOSINFORMACIONPROYECTO,    
              @IDMAESTROPROGRAMA,--DS19    
              NULL,--@AGNOFACTIBILIDADINFORMACIONPROYECTO    
              NULL,--@MONTOSUBSIDIOBASEINFORMACIONPROYECTO    
              NULL,--@MONTOSUBSIDIOBASEFACTIBILIDADINFORMACIONPROYECTO    
              NULL,--@MARCALOCALIZACIONINFORMACIONPROYECTO    
              NULL,--@MARCAFACTIBILIZACIONINFORMACIONPROYECTO    
              NULL,--@FECHAFACTIBILIZACIONINFORMACIONPROYECTO    
              NULL,--@NUMEROFAMILIASABSCRITASINFORMACIONPROYECTO    
              @IDMAESTROESTADOPROYECTO,    
              @ESTADOBENEFICIO,    
              @IDMAESTROBANCO,    
              @AVANCEOBRA)    
              
            SET @IDINFORMACIONPROYECTO = @@IDENTITY    
              
            IF(@@ERROR <> 0)    
            BEGIN    
                ROLLBACK SELECT 'ERROR AL INSERTAR PROYECTO'    
            END    
   ----------------------------------FIN -- Ingreso Proyecto----------------------------------    
    
   ----------------------------INICIO -- Ingreso CARACTERISTICAS_ESPECIALES----------------------------    
            DECLARE @IDTIPOLOGIA BigInt    
            SET     @IDTIPOLOGIA = 19--N/A  
                
            SELECT * FROM MAESTRO_CLASE    
            INSERT INTO CARACTERISTICAS_ESPECIALES(IDMAESTROPROGRAMA,    
                      IDMAESTROMODALIDAD,    
                      IDMAESTROTIPOLOGIA,    
                      IDINFORMACIONPROYECTO,    
                      IDMAESTROCLASE)--1 No aplica    
            VALUES(@IDMAESTROPROGRAMA, @IDMAESTROMODALIDAD, @IDTIPOLOGIA, @IDINFORMACIONPROYECTO, 1)    
              
            --Obtencion del Identity recien ingresado--    
            SET @IDCARACESP = @@IDENTITY    
   -------------------------------------------    
    
            IF(@@ERROR <> 0)    
            BEGIN    
                ROLLBACK SELECT 'ERROR AL INSERTAR CARACTERISTICAS_ESPECIALES'    
            END    
   ----------------------------FIN -- Ingreso CARACTERISTICAS_ESPECIALES----------------------------    
    
   ----------------------------INICIO -- Ingreso TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA----------------------------    
   --------------------------Obtencion de Plantilla--------------------------    
             
            INSERT INTO @SERVICIOPARCIALIDAD (IDSERVPARC)    
            SELECT      IDSERVICIOPARCIALIDAD    
            FROM        SERVICIO_PARCIALIDAD    
            WHERE       NUMEROPLANTILLASERVICIOPARCIALIDAD = @NUMPTLLA    
            ORDER BY    IDSERVICIOPARCIALIDAD    
   --------------------------------------------------------------------------    
    
            WHILE((SELECT COUNT(1) FROM @SERVICIOPARCIALIDAD) > 0)    
            BEGIN    
  
               SET @IDSERVPARC = (SELECT TOP 1 IDSERVPARC FROM @SERVICIOPARCIALIDAD)    
    
               INSERT INTO TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA(IDSERVICIOPARCIALIDAD,    
                           IDCARACTERISTICASESPECIALES,    
                           ESTADOTIPOSERVICIOPARCIALIDADCARACTERISTICA,    
                           MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA,    
                           MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA,    
                           PORCENTAJEPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA,    
                           IDSOLICITUDPAGO)    
               VALUES(@IDSERVPARC, @IDCARACESP, 1, NULL, NULL, NULL, NULL)    
    
               DELETE FROM @SERVICIOPARCIALIDAD WHERE IDSERVPARC = @IDSERVPARC    
            END  
     
            IF(@@ERROR <> 0)    
            BEGIN    
                ROLLBACK SELECT 'ERROR AL INSERTAR TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA'    
            END    
   ----------------------------FIN -- Ingreso TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA----------------------------    
    
   ------------------------------------------INICIO -- Ingreso TIPO_INCREMENTO----------------------------------------    
   ----Creacion tabla con Incrementos para recorrer    
   --DECLARE @INCREMENTO TABLE (IDMAESTROINCREMENTO BigInt)    
      
   --INSERT INTO @INCREMENTO    
   --SELECT IDMAESTROINCREMENTO    
   --FROM MAESTRO_INCREMENTO    
    
   --WHILE ((SELECT COUNT(1) FROM @INCREMENTO) > 0)    
   --BEGIN    
   -- DECLARE @IDMAESTROINCREMENTO BigInt    
   -- SET @IDMAESTROINCREMENTO = (SELECT TOP 1 IDMAESTROINCREMENTO FROM @INCREMENTO ORDER BY IDMAESTROINCREMENTO)    
    
   -- INSERT INTO TIPO_INCREMENTO(IDMAESTROINCREMENTO,    
   --        IDCARACTERISTICASESPECIALES,    
   --        IDSOLICITUDPAGO,    
   --        SELECCIONADOTIPOINCREMENTO,    
   --        ESTADOTIPOINCREMENTO)    
   -- VALUES(@IDMAESTROINCREMENTO, @IDCARACESP, NULL, 0, 1)    
    
   -- --Se Elimina el ya ingresado    
   -- DELETE FROM @INCREMENTO WHERE IDMAESTROINCREMENTO = @IDMAESTROINCREMENTO    
   --END    
    
   --IF(@@ERROR <> 0)    
   --BEGIN    
   -- ROLLBACK SELECT 'ERROR AL INSERTAR TIPO_INCREMENTO'    
   --END    
   --------------------------------------------FIN -- Ingreso TIPO_INCREMENTO------------------------------------------    
        END    
    
        COMMIT TRAN    
            SELECT 'OK'    
        END    
    END    
GO
/****** Object:  StoredProcedure [dbo].[INSERTA_PROYECTOS_SNATDS49]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/********************************************************************************************      
* Nombre del procedimiento      :  INSERTA_PROYECTOS_SNATDS49      
* Fecha de creación             :  29/11/2018      
* Usuario de creación           :  DOrozco      
* Versión.                      :  V.0.0.0.1      

* Fecha de modificación         :  
* Usuarios de modificación      :  
* Motivo de modificación        :  

* Fecha de modificación         :  
* Usuarios de modificación      :  
* Motivo de modificación        :      
      
* Visado por DBA                :	Iván Frade Cortés
* Fecha Aprobación DBA          :   13122018
* Comentarios DBA               :     ---  

* Objetivo                      : Inserta en SNAT Simplificado los proyectos obtenidos desde Rukan  
* Objetos Utilizados y para Que : Tablas        

**********************************************************************************************/  

CREATE PROCEDURE [dbo].[INSERTA_PROYECTOS_SNATDS49]  
 @CODIGOPROYECTOINFORMACIONPROYECTO BigInt,  
 @NOMBREPROYECTOINFORMACIONPROYECTO VarChar(200),  
 @NOMBREESTADOPROYECTO Varchar(200),  
 @CODIGOREGIONDIRECCION int,  
 @CODIGOPROVINCIADIRECCION int,  
 @CODIGOCOMUNADIRECCION int,  
 @NOMBRETIPOLOGIA Varchar(100),  
 @NOMBREBANCO VarChar(200),  
 @MODALIDAD VarChar(200),  
 @CANTIDADVIVIENDASINFORMACIONPROYECTO int,  
 @CANTIDADBENEFICIARIOSINFORMACIONPROYECTO int,  
 @NUMEROFAMILIASADSCRITASINFORMACIONPROYECTO Int,  
 @RUTENTIDAD int,  
 @DIGITOENTIDAD char(1),  
 @NOMBREENTIDAD VarChar(200),  
 @MONTOSUBSIDIOBASEINFORMACIONPROYECTO decimal(18,3),  
 @MARCALOCALIZACIONINFORMACIONPROYECTO bit,  
 @MARCAFACTIBILIZACIONINFORMACIONPROYECTO bit,  
 @IDMAESTROALTERNATIVAPOSTULACION BigInt,  
 @NOMBREEMPRESACONSTRUCTORA VarChar(200),  
 @RUTEMPRESACONSTRUCTORA int,  
 @DIGITOEMPRESACONSTRUCTORA char(1),  
 @FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTO DateTime,  
 @MONTOSUBSIDIOBASEFACTIBILIDADINFORMACIONPROYECTO decimal(18,3),  
 @AGNOFACTIBILIDADINFORMACIONPROYECTO Int,  
 @FECHAFACTIBILIZACIONINFORMACIONPROYECTO DateTime,  
 @ESTADOBENEFICIO BigInt,  
 @IDMAESTROPROGRAMA BigInt  
AS  
BEGIN  
 BEGIN TRAN  
 BEGIN  
  ----------------------------------------------------------------TIPOLOGIA----------------------------------------------------------------  
  DECLARE @IDTIPOLOGIA BigInt  
  
  IF EXISTS(SELECT 1 FROM MAESTRO_TIPOLOGIA WHERE UPPER(LTRIM(RTRIM(NOMBREABREVIADOMAESTROTIPOLOGIA))) = UPPER(LTRIM(RTRIM(@NOMBRETIPOLOGIA))))  
  BEGIN  
   SET @IDTIPOLOGIA = (SELECT IDMAESTROTIPOLOGIA  
         FROM MAESTRO_TIPOLOGIA  
         WHERE UPPER(LTRIM(RTRIM(NOMBREABREVIADOMAESTROTIPOLOGIA))) = UPPER(LTRIM(RTRIM(@NOMBRETIPOLOGIA))))  
  END  
  ELSE  
  BEGIN  
   ROLLBACK SELECT CONVERT(Varchar,('ERROR NO SE ENCUENTRA TIPOLOGIA' + @NOMBRETIPOLOGIA)) AS ERR  
  END  
  
  IF(@@ERROR <> 0)  
  BEGIN  
   ROLLBACK SELECT 'ERROR BUSCAR TIPOLOGIA'  
  END  
  ----------------------------------------------------------------TIPOLOGIA----------------------------------------------------------------  
  
  ----------------------------------------------------------Inserta Direccion----------------------------------------------------------  
  DECLARE @IDDIRECCION BigInt  
  
  IF NOT((@CODIGOCOMUNADIRECCION IS NULL) OR (@CODIGOPROVINCIADIRECCION IS NULL) OR (@CODIGOREGIONDIRECCION IS NULL))  
  BEGIN  
   DECLARE @NUMERODIRECCION VarChar(4)  
   SET @NUMERODIRECCION = 'N/A'  
  
   IF(NOT EXISTS(SELECT 1  
       FROM DIRECCION  
       WHERE RTRIM(LTRIM(UPPER(DESCRIPCIONDIRECCION))) IS NULL  
        AND RTRIM(LTRIM(UPPER(NUMERODIRECCION))) = RTRIM(LTRIM(UPPER(@NUMERODIRECCION)))  
        AND CODIGOREGIONDIRECCION = @CODIGOREGIONDIRECCION  
        AND CODIGOPROVINCIADIRECCION = @CODIGOPROVINCIADIRECCION  
        AND CODIGOCOMUNADIRECCION = @CODIGOCOMUNADIRECCION))  
   BEGIN  
    INSERT INTO DIRECCION(NUMERODIRECCION,DESCRIPCIONDIRECCION,CODIGOCOMUNADIRECCION,CODIGOPROVINCIADIRECCION,CODIGOREGIONDIRECCION)  
    VALUES(@NUMERODIRECCION,NULL,@CODIGOCOMUNADIRECCION,@CODIGOPROVINCIADIRECCION,@CODIGOREGIONDIRECCION)  
      
    SET @IDDIRECCION = @@IDENTITY  
  
    IF(@@ERROR <> 0)  
    BEGIN  
     ROLLBACK SELECT 'ERROR AL INSERTAR DIRECCION'  
    END  
   END  
   ELSE  
   BEGIN  
    SET @IDDIRECCION = (SELECT IDDIRECCION  
         FROM DIRECCION  
         WHERE RTRIM(LTRIM(UPPER(DESCRIPCIONDIRECCION))) IS NULL  
          AND RTRIM(LTRIM(UPPER(NUMERODIRECCION))) = RTRIM(LTRIM(UPPER(@NUMERODIRECCION)))  
          AND CODIGOREGIONDIRECCION = @CODIGOREGIONDIRECCION  
          AND CODIGOPROVINCIADIRECCION = @CODIGOPROVINCIADIRECCION  
          AND CODIGOCOMUNADIRECCION = @CODIGOCOMUNADIRECCION)  
  
    IF(@@ERROR <> 0)  
    BEGIN  
     ROLLBACK SELECT 'ERROR AL OBTENER DIRECCION'  
    END  
   END  
  END  
  ----------------------------------------------------------Inserta Direccion----------------------------------------------------------  
  
  --------------------------------------------------------------MAESTRO_ESTADO_PROYECTO--------------------------------------------------------------  
  DECLARE @IDMAESTROESTADOPROYECTO BigInt  
  
  IF EXISTS(SELECT 1 FROM MAESTRO_ESTADO_PROYECTO WHERE UPPER(LTRIM(RTRIM(NOMBREMAESTROESTADOPROYECTO))) = UPPER(LTRIM(RTRIM(@NOMBREESTADOPROYECTO))))  
  BEGIN  
   SET @IDMAESTROESTADOPROYECTO = (SELECT IDMAESTROESTADOPROYECTO  
           FROM MAESTRO_ESTADO_PROYECTO  
           WHERE UPPER(LTRIM(RTRIM(NOMBREMAESTROESTADOPROYECTO))) = UPPER(LTRIM(RTRIM(@NOMBREESTADOPROYECTO))))  
  
   IF(@@ERROR <> 0)  
   BEGIN  
    ROLLBACK SELECT 'ERROR AL OBTENER ESTADO PROYECTO'  
   END  
  END  
  ELSE  
  BEGIN  
   INSERT INTO MAESTRO_ESTADO_PROYECTO(NOMBREMAESTROESTADOPROYECTO,ESTADOMAESTROESTADOPROYECTO)  
   VALUES(LTRIM(RTRIM(@NOMBREESTADOPROYECTO)),1)  
  
   SET @IDMAESTROESTADOPROYECTO = @@IDENTITY  
  
   IF(@@ERROR <> 0)  
   BEGIN  
    ROLLBACK SELECT 'ERROR AL INSERTAR ESTADO PROYECTO'  
   END  
  END  
  --------------------------------------------------------------MAESTRO_ESTADO_PROYECTO--------------------------------------------------------------  
  
  --------------------------------------------------------MAESTRO_MODALIDAD--------------------------------------------------------  
  DECLARE @IDMAESTROMODALIDAD BigInt  
  
  IF EXISTS(SELECT 1 FROM MAESTRO_MODALIDAD WHERE UPPER(LTRIM(RTRIM(NOMBREMAESTROMODALIDAD))) = UPPER(LTRIM(RTRIM(@MODALIDAD))))  
  BEGIN  
   SET @IDMAESTROMODALIDAD = (SELECT IDMAESTROMODALIDAD  
           FROM MAESTRO_MODALIDAD  
           WHERE UPPER(LTRIM(RTRIM(NOMBREMAESTROMODALIDAD))) = UPPER(LTRIM(RTRIM(@MODALIDAD))))  
  
   IF(@@ERROR <> 0)  
   BEGIN  
    ROLLBACK SELECT 'ERROR AL OBTENER MODALIDAD'  
   END  
  END  
  ELSE  
  BEGIN  
   SET @IDMAESTROMODALIDAD = ((SELECT MAX(IDMAESTROMODALIDAD) FROM MAESTRO_MODALIDAD) + 1)  
  
   INSERT INTO MAESTRO_MODALIDAD(IDMAESTROMODALIDAD,NOMBREMAESTROMODALIDAD,ESTADOMAESTROMODALIDAD)  
   VALUES(@IDMAESTROMODALIDAD,LTRIM(RTRIM(@MODALIDAD)),1)  
  
   IF(@@ERROR <> 0)  
   BEGIN  
    ROLLBACK SELECT 'ERROR AL INSERTAR MODALIDAD'  
   END  
  END  
  --------------------------------------------------------MAESTRO_MODALIDAD--------------------------------------------------------  
  
  ------------------------------------------------------MAESTRO_BANCO------------------------------------------------------  
  DECLARE @IDMAESTROBANCO BigInt  
  
  IF EXISTS(SELECT 1 FROM MAESTRO_BANCO WHERE UPPER(LTRIM(RTRIM(NOMBREMAESTROBANCO))) = UPPER(LTRIM(RTRIM(@NOMBREBANCO))))  
  BEGIN  
   SET @IDMAESTROBANCO = (SELECT IDMAESTROBANCO  
         FROM MAESTRO_BANCO  
         WHERE UPPER(LTRIM(RTRIM(NOMBREMAESTROBANCO))) = UPPER(LTRIM(RTRIM(@NOMBREBANCO))))  
  END  
  ELSE  
  BEGIN  
   INSERT INTO MAESTRO_BANCO(NOMBREMAESTROBANCO,ESTADOMAESTROBANCO)  
   VALUES(LTRIM(RTRIM(@NOMBREBANCO)),1)  
  
   SET @IDMAESTROBANCO = @@IDENTITY  
  
   IF(@@ERROR <> 0)  
   BEGIN  
    ROLLBACK SELECT 'ERROR EN INSERTAR BANCO'  
   END  
  END  
  ------------------------------------------------------MAESTRO_BANCO------------------------------------------------------  
  
  --------------Variables ID--------------  
  DECLARE @IDINFORMACIONPROYECTO BigInt,  
    @IDCARACESP BigInt  
  --------------Variables ID--------------  
  
  --VERIFICA SI PROYECTO EXISTE Y MODIFICAR DATOS SI ASI ES, SI NO INSERTA NUEVO  
  IF(EXISTS(SELECT 1 FROM INFORMACION_PROYECTO WHERE CODIGOPROYECTOINFORMACIONPROYECTO = @CODIGOPROYECTOINFORMACIONPROYECTO AND IDMAESTROPROGRAMA = @IDMAESTROPROGRAMA))  
  BEGIN  
   ------------------------------------Actualiza Proyecto------------------------------------  
   SELECT @IDINFORMACIONPROYECTO = IDINFORMACIONPROYECTO  
   FROM INFORMACION_PROYECTO  
   WHERE CODIGOPROYECTOINFORMACIONPROYECTO = @CODIGOPROYECTOINFORMACIONPROYECTO  
    AND IDMAESTROPROGRAMA = @IDMAESTROPROGRAMA  
  
   UPDATE INFORMACION_PROYECTO  
   SET IDDIRECCION = @IDDIRECCION,  
    IDMAESTROMODALIDAD = @IDMAESTROMODALIDAD,  
    IDMAESTROALTERNATIVAPOSTULACION = @IDMAESTROALTERNATIVAPOSTULACION,  
    NOMBREPROYECTOINFORMACIONPROYECTO = @NOMBREPROYECTOINFORMACIONPROYECTO,  
    FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTO = @FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTO,  
    CANTIDADVIVIENDASINFORMACIONPROYECTO = @CANTIDADVIVIENDASINFORMACIONPROYECTO,  
    CANTIDADBENEFICIARIOSINFORMACIONPROYECTO = @CANTIDADBENEFICIARIOSINFORMACIONPROYECTO,  
    MONTOSUBSIDIOBASEINFORMACIONPROYECTO = @MONTOSUBSIDIOBASEINFORMACIONPROYECTO,  
    MONTOSUBSIDIOBASEFACTIBILIDADINFORMACIONPROYECTO = @MONTOSUBSIDIOBASEFACTIBILIDADINFORMACIONPROYECTO,  
    MARCALOCALIZACIONINFORMACIONPROYECTO = @MARCALOCALIZACIONINFORMACIONPROYECTO,  
    MARCAFACTIBILIZACIONINFORMACIONPROYECTO = @MARCAFACTIBILIZACIONINFORMACIONPROYECTO,  
    FECHAFACTIBILIZACIONINFORMACIONPROYECTO = @FECHAFACTIBILIZACIONINFORMACIONPROYECTO,  
    NUMEROFAMILIASADSCRITASINFORMACIONPROYECTO = @NUMEROFAMILIASADSCRITASINFORMACIONPROYECTO,  
    AGNOFACTIBILIDADINFORMACIONPROYECTO = @AGNOFACTIBILIDADINFORMACIONPROYECTO,  
    IDMAESTROESTADOPROYECTO = @IDMAESTROESTADOPROYECTO,  
    IDMAESTROESTADOBENEFICIO = @ESTADOBENEFICIO,  
    IDMAESTROBANCO = @IDMAESTROBANCO  
   WHERE IDINFORMACIONPROYECTO = @IDINFORMACIONPROYECTO  
     
   IF(@@ERROR <> 0)  
   BEGIN  
    ROLLBACK SELECT 'ERROR AL ACTUALIZAR PROYECTO'  
   END  
   ------------------------------------Actualiza Proyecto------------------------------------  
  
   ----------------Actualiza CARACTERISTICAS_ESPECIALES----------------  
   UPDATE CARACTERISTICAS_ESPECIALES  
   SET IDMAESTROMODALIDAD = @IDMAESTROMODALIDAD  
   WHERE IDINFORMACIONPROYECTO = @IDINFORMACIONPROYECTO  
  
   SELECT @IDCARACESP = IDCARACTERISTICASESPECIALES  
   FROM CARACTERISTICAS_ESPECIALES  
   WHERE IDINFORMACIONPROYECTO = @IDINFORMACIONPROYECTO  
  
   IF(@@ERROR <> 0)  
   BEGIN  
    ROLLBACK SELECT 'ERROR AL INSERTAR CARACTERISTICAS_ESPECIALES'  
   END  
   ----------------Actualiza CARACTERISTICAS_ESPECIALES----------------  
  END  
  ELSE --NUEVO INGRESO  
  BEGIN  
   ------------------------------------------------INICIO -- Ingreso Proveedor------------------------------------------------  
   DECLARE @Ciclo int  
   SET @Ciclo = 1  
  
   DECLARE @IDProveedor1 BigInt,  
     @IDProveedor2 BigInt  
  
   SET @IDProveedor1 = 0  
   SET @IDProveedor2 = 0  
  
   WHILE(@Ciclo >= 0)  
   BEGIN  
    DECLARE @IDMAESTROTIPOPROVEEDOR Int,--1 EP / --2 EC  
      @RUTPROVEEDOR Int,  
      @RUTDVPROVEEDOR Char(1),  
      @NOMBREPROVEEDOR VarChar(200)  
     
    IF(@Ciclo = 1)  
    BEGIN  
     SET @IDMAESTROTIPOPROVEEDOR = 1  
     SET @RUTPROVEEDOR = @RUTENTIDAD  
     SET @RUTDVPROVEEDOR = @DIGITOENTIDAD  
     SET @NOMBREPROVEEDOR = @NOMBREENTIDAD  
    END  
    ELSE  
    BEGIN  
     SET @IDMAESTROTIPOPROVEEDOR = 2  
     SET @RUTPROVEEDOR = @RUTEMPRESACONSTRUCTORA  
     SET @RUTDVPROVEEDOR = @DIGITOEMPRESACONSTRUCTORA  
     SET @NOMBREPROVEEDOR = @NOMBREEMPRESACONSTRUCTORA  
    END  
  
    IF(@RUTPROVEEDOR <> 0)  
    BEGIN  
     DECLARE @IDPROVEEDOR BigInt  
  
     IF NOT EXISTS(SELECT 1 FROM PROVEEDOR WHERE RUTPROVEEDOR = @RutProveedor)  
     BEGIN  
      INSERT INTO PROVEEDOR(IDMAESTROTIPOPROVEEDOR,NOMBREPROVEEDOR,RUTPROVEEDOR,DVPROVEDIGITOVERIFICADORPROVEEDOR)  
      VALUES(@IDMAESTROTIPOPROVEEDOR,@NOMBREPROVEEDOR,@RUTPROVEEDOR,@RUTDVPROVEEDOR)  
  
      SET @IDPROVEEDOR = @@IDENTITY  
     END  
     ELSE  
     BEGIN  
      SET @IDPROVEEDOR = (SELECT IDPROVEEDOR FROM PROVEEDOR WHERE RUTPROVEEDOR = @RutProveedor)  
     END  
  
     IF(@Ciclo = 1)  
     BEGIN  
      SET @IDProveedor1 = @IDPROVEEDOR  
     END  
     ELSE  
     BEGIN  
      SET @IDProveedor2 = @IDPROVEEDOR  
     END  
    END  
  
    SET @Ciclo = (@Ciclo - 1)  
   END  
  
   IF(@@ERROR <> 0)  
   BEGIN  
    ROLLBACK SELECT 'ERROR EN INSERTAR PROVEEDOR'  
   END  
   ------------------------------------------------FIN -- Inserta Proveedor------------------------------------------------  
  
   ----------------------------------------INICIO -- Ingreso Proyecto----------------------------------------  
   INSERT INTO INFORMACION_PROYECTO(IDDIRECCION,  
           IDMAESTROTITULO,  
           IDMAESTROMODALIDAD,  
           IDMAESTROLLAMADO,  
           IDMAESTROALTERNATIVAPOSTULACION,  
           CODIGOPROYECTOINFORMACIONPROYECTO,  
           NOMBREPROYECTOINFORMACIONPROYECTO,  
           FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTO,  
           ESTADOPROYECTOINFORMACIONPROYECTO,  
           RESOLUCIONATINFORMACIONPROYECTO,  
           FECHARESOLUCIONATINFORMACIONPROYECTO,  
           CANTIDADVIVIENDASINFORMACIONPROYECTO,  
           CANTIDADBENEFICIARIOSINFORMACIONPROYECTO,  
           IDMAESTROPROGRAMA,  
           MONTOSUBSIDIOBASEINFORMACIONPROYECTO,  
           MONTOSUBSIDIOBASEFACTIBILIDADINFORMACIONPROYECTO,  
           MARCALOCALIZACIONINFORMACIONPROYECTO,  
           MARCAFACTIBILIZACIONINFORMACIONPROYECTO,  
           FECHAFACTIBILIZACIONINFORMACIONPROYECTO,  
           NUMEROFAMILIASADSCRITASINFORMACIONPROYECTO,  
           IDMAESTRORESOLUCION,  
           AGNOFACTIBILIDADINFORMACIONPROYECTO,  
           IDMAESTROESTADOPROYECTO,  
           IDMAESTROESTADOBENEFICIO,  
           IDMAESTROBANCO)  
   VALUES (@IDDIRECCION,  
     NULL,--Maestro Titulo  
     @IDMAESTROMODALIDAD,  
     NULL,--MAESTROLLAMADO  
     @IDMAESTROALTERNATIVAPOSTULACION,  
     @CODIGOPROYECTOINFORMACIONPROYECTO,  
     @NOMBREPROYECTOINFORMACIONPROYECTO,  
     @FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTO,  
     1,--Estado Proyecto  
     NULL,--@RESOLUCIONATINFORMACIONPROYECTO  
     NULL,--@FECHARESOLUCIONATINFORMACIONPROYECTO  
     @CANTIDADVIVIENDASINFORMACIONPROYECTO,  
     @CANTIDADBENEFICIARIOSINFORMACIONPROYECTO,  
     @IDMAESTROPROGRAMA,  
     @MONTOSUBSIDIOBASEINFORMACIONPROYECTO,  
     @MONTOSUBSIDIOBASEFACTIBILIDADINFORMACIONPROYECTO,  
     @MARCALOCALIZACIONINFORMACIONPROYECTO,  
     @MARCAFACTIBILIZACIONINFORMACIONPROYECTO,  
     @FECHAFACTIBILIZACIONINFORMACIONPROYECTO,  
     @NUMEROFAMILIASADSCRITASINFORMACIONPROYECTO,  
     NULL,--@IDMAESTRORESOLUCION  
     @AGNOFACTIBILIDADINFORMACIONPROYECTO,  
     @IDMAESTROESTADOPROYECTO,  
     NULL,--MAESTRO_ESTADO_BENEFICIO  
     @IDMAESTROBANCO)  
  
   SET @IDINFORMACIONPROYECTO = @@IDENTITY  
  
   IF(@@ERROR <> 0)  
   BEGIN  
    ROLLBACK SELECT 'ERROR AL INSERTAR PROYECTO'  
   END  
   ----------------------------------------FIN -- Ingreso Proyecto----------------------------------------  
  
   --------------------------------------------INICIO -- Ingreso ProveedorNomalizada--------------------------------------------  
   IF(@IDProveedor1 <> 0)  
   BEGIN  
    INSERT INTO TIPO_PROVEEDOR_INFORMACION_PROYECTO(IDPROVEEDOR,IDINFORMACIONPROYECTO,ESTADOTIPOPROVEEDORINFORMACIONPROYECTO)  
    VALUES(@IDProveedor1,@IDINFORMACIONPROYECTO,1)  
   END  
    
   IF(@IDProveedor2 <> 0)  
   BEGIN  
    INSERT INTO TIPO_PROVEEDOR_INFORMACION_PROYECTO(IDPROVEEDOR,IDINFORMACIONPROYECTO,ESTADOTIPOPROVEEDORINFORMACIONPROYECTO)  
    VALUES(@IDProveedor2,@IDINFORMACIONPROYECTO,1)  
   END  
  
   IF(@@ERROR <> 0)  
   BEGIN  
    ROLLBACK SELECT 'ERROR AL INSERTAR TIPOPROVEEDORINFORMACIONPROYECTO'  
   END  
   --FIN -- Ingreso ProveedorNomalizada  
    
   ----------------------------INICIO -- Ingreso CARACTERISTICAS_ESPECIALES----------------------------  
   INSERT INTO CARACTERISTICAS_ESPECIALES(IDMAESTROPROGRAMA,  
             IDMAESTROMODALIDAD,  
             IDMAESTROTIPOLOGIA,  
             IDINFORMACIONPROYECTO,  
             IDMAESTROCLASE)--1 No aplica  
   VALUES(@IDMAESTROPROGRAMA, @IDMAESTROMODALIDAD, @IDTIPOLOGIA, @IDINFORMACIONPROYECTO, 1)  
  
   --Obtencion del Identity recien ingresado--  
   SET @IDCARACESP = @@IDENTITY  
   -------------------------------------------  
  
   IF(@@ERROR <> 0)  
   BEGIN  
    ROLLBACK SELECT 'ERROR AL INSERTAR CARACTERISTICAS_ESPECIALES'  
   END  
   --------------------------------------------FIN -- Ingreso CARACTERISTICAS_ESPECIALES--------------------------------------------  
  
   ----------------------------INICIO -- Ingreso TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA----------------------------  
   --------------------------Obtencion de Plantilla--------------------------  
   DECLARE @NUMPTLLA Int  
   SET @NUMPTLLA = (SELECT DISTINCT sp.NUMEROPLANTILLASERVICIOPARCIALIDAD  
       FROM SERVICIO_PARCIALIDAD sp  
        INNER JOIN TIPOLOGIA_SERVICIO ts  
         ON sp.IDTIPOLOGIASERVICIO = ts.IDTIPOLOGIASERVICIO  
        INNER JOIN MAESTRO_TIPOLOGIA mt  
         ON ts.IDMAESTROTIPOLOGIA = mt.IDMAESTROTIPOLOGIA  
        INNER JOIN MAESTRO_SERVICIO ms  
         ON ts.IDMAESTROSERVICIO = ms.IDMAESTROSERVICIO  
        INNER JOIN SUB_MODALIDAD_PARCIALIDAD smp  
         ON sp.IDSUBMODALIDADPARCIALIDAD = smp.IDSUBMODALIDADPARCIALIDAD  
        INNER JOIN MAESTRO_SUB_MODALIDAD msm  
         ON smp.IDMAESTROSUBMODALIDAD = msm.IDMAESTROSUBMODALIDAD  
        INNER JOIN MAESTRO_PARCIALIDAD mp  
         ON smp.IDMAESTROPARCIALIDAD = mp.IDMAESTROPARCIALIDAD  
       WHERE sp.IDMAESTROPROGRAMA = @IDMAESTROPROGRAMA  
        AND ts.IDMAESTROTIPOLOGIA = @IDTIPOLOGIA)  
  
   DECLARE @SERVICIOPARCIALIDAD TABLE (IDSERVPARC BigInt)  
  
   INSERT INTO @SERVICIOPARCIALIDAD (IDSERVPARC)  
   SELECT IDSERVICIOPARCIALIDAD  
   FROM SERVICIO_PARCIALIDAD  
   WHERE NUMEROPLANTILLASERVICIOPARCIALIDAD = @NUMPTLLA  
   ORDER BY IDSERVICIOPARCIALIDAD  
   --------------------------------------------------------------------------  
  
   WHILE((SELECT COUNT(1) FROM @SERVICIOPARCIALIDAD) > 0)  
   BEGIN  
    DECLARE @IDSERVPARC BigInt  
    SET @IDSERVPARC = (SELECT TOP 1 IDSERVPARC FROM @SERVICIOPARCIALIDAD)  
  
    INSERT INTO TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA(IDSERVICIOPARCIALIDAD,  
               IDCARACTERISTICASESPECIALES,  
               ESTADOTIPOSERVICIOPARCIALIDADCARACTERISTICA,  
               MONTOSERVICIOTIPOSERVICIOPARCIALIDADCARACTERISTICA,  
               MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA,  
               PORCENTAJEPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA,  
               IDSOLICITUDPAGO)  
    VALUES(@IDSERVPARC, @IDCARACESP, 1, NULL, NULL, NULL, NULL)  
  
    DELETE FROM @SERVICIOPARCIALIDAD WHERE IDSERVPARC = @IDSERVPARC  
   END  
  
   IF(@@ERROR <> 0)  
   BEGIN  
    ROLLBACK SELECT 'ERROR AL INSERTAR TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA'  
   END  
   ----------------------------FIN -- Ingreso TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA----------------------------  
  
   ------------------------------------------INICIO -- Ingreso TIPO_INCREMENTO----------------------------------------  
   ----Creacion tabla con Incrementos para recorrer  
   --DECLARE @INCREMENTO TABLE (IDMAESTROINCREMENTO BigInt)  
    
   --INSERT INTO @INCREMENTO  
   --SELECT IDMAESTROINCREMENTO  
   --FROM MAESTRO_INCREMENTO  
  
   --WHILE ((SELECT COUNT(1) FROM @INCREMENTO) > 0)  
   --BEGIN  
   -- DECLARE @IDMAESTROINCREMENTO BigInt  
   -- SET @IDMAESTROINCREMENTO = (SELECT TOP 1 IDMAESTROINCREMENTO FROM @INCREMENTO ORDER BY IDMAESTROINCREMENTO)  
  
   -- INSERT INTO TIPO_INCREMENTO(IDMAESTROINCREMENTO,  
   --        IDCARACTERISTICASESPECIALES,  
   --        IDSOLICITUDPAGO,  
   --        SELECCIONADOTIPOINCREMENTO,  
   --        ESTADOTIPOINCREMENTO)  
   -- VALUES(@IDMAESTROINCREMENTO, @IDCARACESP, NULL, 0, 1)  
  
   -- --Se Elimina el ya ingresado  
   -- DELETE FROM @INCREMENTO WHERE IDMAESTROINCREMENTO = @IDMAESTROINCREMENTO  
   --END  
  
   --IF(@@ERROR <> 0)  
   --BEGIN  
   -- ROLLBACK SELECT 'ERROR AL INSERTAR TIPO_INCREMENTO'  
   --END  
   --------------------------------------------FIN -- Ingreso TIPO_INCREMENTO------------------------------------------  
  END  
  
  COMMIT TRAN  
   SELECT 'OK'  
 END  
END  

GO
/****** Object:  StoredProcedure [dbo].[INSERTA_SOLICITUD_ATP]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************************************************              
* Nombre del procedimiento      : INSERTA_SOLICITUD_ATP      
* Fecha de creación             : 13/11/2018              
* Usuario de creación           : Anticipa             
* Versión.                      : V.0.0.0.1              
      
  Fecha de modificación   : 18-02-2019      
Usuario de modificación : cfajardo    
Motivo de modificación  : regularización de procedimientos almacenados entre ambientes.   
  
  Fecha de modificación   : 08-04-2019      
Usuario de modificación : cfajardo    
Motivo de modificación  : Nueva columna FECHACREACIONSOLICITUDPAGOATP.    
    
* Visado por DBA                :  Mirto Blanco      
* Fecha Aprobación DBA          :  20190906     
      
* Parametros     :@IDSOLICITUDATP        
         @IDCONTRATO      
         @ANNOPRESUPUESTO      
         @MONTOPRESUPESTO      
         @CODIGOREGION      
         @MONTOPAGADO      
         @MONTOCOMPROMETIDO      
         @MONTOPORPAGAR      
         @NUMEROBOLFACT      
         @FECHABOLETAFACT      
         @MONTOTOTALAPAGAR      
         @VBSERVICIO      
         @VBRESPONSABLE      
         @VBFECHA      
              
* Objetivo                      : Procedimiento que inserta Solicitud de Pago ATP            
* Prueba      : exec INSERTA_SOLICITUD_ATP 30,17,2018,367300,13,0,100,100,'SAT¬49¬50¬|SAT¬50¬50¬|',6665,'20181127',100,false,'fquezada','20181127',6      
**********************************************************************************************/           
      
CREATE PROCEDURE [dbo].[INSERTA_SOLICITUD_ATP]              
@IDSOLICITUDATP BIGINT,        
@IDCONTRATO BIGINT,      
@ANNOPRESUPUESTO INT,      
@MONTOPRESUPESTO BIGINT,      
@CODIGOREGION INT,      
@MONTOPAGADO BIGINT,      
@MONTOCOMPROMETIDO BIGINT,      
@MONTOPORPAGAR BIGINT,      
@ACTIVIDADES VARCHAR(MAX),      
@NUMEROBOLFACT BIGINT,      
@FECHABOLETAFACT DATETIME,      
@MONTOTOTALAPAGAR BIGINT,      
@VBSERVICIO BIT,      
@VBRESPONSABLE VARCHAR(50),      
@VBFECHA DATETIME,      
@ESTADOSOLICITUD BIGINT,      
@IDMAESTROTIPOPAGO BIGINT NULL     
AS      
      
BEGIN      
      
    /****** Control Errores ***********/          
    DECLARE @ERR INT, @MSG VARCHAR(250)          
             
    SET @ERR = 0          
    /**********************************/       
          
    DECLARE @VIGENCIA BIT      
    DECLARE @CERO INT      
          
    SET @VIGENCIA = 1      
    SET @CERO = 0       
          
    BEGIN TRANSACTION      
      
    INSERT INTO [dbo].[SOLICITUD_PAGO_ATP]      
        ([IDCONTRATOATP]      
        ,[AGNOPRESUPUESTOSOLICITUDPAGOATP]      
        ,[PRESUPUESTOREGIONALSOLICITUDPAGOATP]      
        ,[CODIGOREGIONSOLICITUDPAGOATP]      
        ,[MONTOPAGADOSOLICITUDPAGOATP]      
        ,[MONTOCOMPROMETIDOSOLICITUDPAGOATP]      
        ,[MONTOPORPAGARSOLICITUDPAGOATP]      
        ,[NUMEROBOLETAFACTURASOLICITUDPAGOATP]      
        ,[FECHABOLETAFACTURASOLICITUDPAGOATP]      
        ,[MONTOTOTALAPAGARSOLICITUDPAGOATP]      
        ,[VBSERVICIOSOLICITUDPAGOATP]      
        ,[VBRESPONSABLESOLICITUDPAGOATP]      
        ,[VBFECHASOLICITUDPAGOATP]      
        ,[IDMAESTROESTADOSOLICITUD]  
     ,[FECHACREACIONSOLICITUDPAGOATP]
	 ,[IDMAESTROTIPOPAGO])      
    VALUES(@IDCONTRATO      
        ,@ANNOPRESUPUESTO      
        ,@MONTOPRESUPESTO      
        ,@CODIGOREGION      
        ,@MONTOPAGADO      
        ,@MONTOCOMPROMETIDO      
        ,@MONTOPORPAGAR      
        ,@NUMEROBOLFACT      
        ,@FECHABOLETAFACT      
        ,@MONTOTOTALAPAGAR      
        ,@VBSERVICIO      
        ,@VBRESPONSABLE      
        ,@VBFECHA      
        ,@ESTADOSOLICITUD 
     ,GETDATE(),
	 @IDMAESTROTIPOPAGO)      
      
    IF @@rowcount <= @CERO          
    BEGIN          
        SET @err = -1          
        SET @msg = 'ERROR EN INSERTAR SOLICITUD.'      
        GOTO error          
    END       
      
    SELECT @IDSOLICITUDATP = CONVERT(INT,@@Identity)    
    SET @MSG = 'Solicitud AT Previa ' +CAST(@IDSOLICITUDATP as varchar(6))  + ' ingresada correctamente.'     
    
    BEGIN      
     DECLARE @TIPOSERVICIO varchar(max)      
        DECLARE @ITEMTIPOSERVICIO varchar(max)      
        DECLARE @ITEMIDSERVICIO varchar(max)      
        DECLARE @ITEMMONTOPAGO varchar(max)      
        DECLARE @pos INT      
        DECLARE @posI INT      
        DECLARE @posF INT      
        DECLARE @IDACTIVIDAD BIGINT      
        DECLARE @largo INT      
             
             
        DECLARE @SOLICITUDES TABLE (IDSOLICITUDPAGO varchar(50))      
       
        WHILE CHARINDEX('|', @ACTIVIDADES) > 0      
        BEGIN        
            SELECT @pos = CHARINDEX('|', @ACTIVIDADES)        
            SELECT @TIPOSERVICIO = SUBSTRING(@ACTIVIDADES, 1, @pos-1)      
              
            BEGIN      
                WHILE CHARINDEX('¬', @TIPOSERVICIO) > 0      
                BEGIN        
                    SELECT @posI = CHARINDEX('#', @TIPOSERVICIO)        
                    SELECT @posF = CHARINDEX('°', @TIPOSERVICIO)        
                    SELECT @largo = @posF - @posI      
                    SELECT @ITEMTIPOSERVICIO = SUBSTRING(@TIPOSERVICIO, 1, 3)      
                    SELECT @ITEMIDSERVICIO = SUBSTRING(@TIPOSERVICIO, 5, 2)      
                    SELECT @ITEMMONTOPAGO = SUBSTRING(@TIPOSERVICIO, @posI+1, @largo-1)      
                     
                    INSERT INTO TIPO_ACTIVIDAD_MONTO(  
            IDSOLICITUDATP,      
                           TIPOSERVICIOTIPOACTIVIDADMONTO,      
                           IDMAESTROSERVICIO,      
                           MONTOPAGOTIPOACTIVIDADMONTO)       
                    SELECT @IDSOLICITUDATP,      
                           @ITEMTIPOSERVICIO,      
                           @ITEMIDSERVICIO,      
                           @ITEMMONTOPAGO    
            
                    SELECT @IDACTIVIDAD = CONVERT(INT,@@Identity)       
            
                    SELECT @TIPOSERVICIO = ''      
                END      
            END      
            
            SELECT @ACTIVIDADES = SUBSTRING(@ACTIVIDADES, @pos+1, LEN(@ACTIVIDADES)-@pos)      
        END      
    END      
      
END      
      
SET NOCOUNT OFF        
    COMMIT TRANSACTION        
    SELECT @ERR AS err, @MSG AS MSG      
    RETURN        
ERROR: ROLLBACK  TRANSACTION        
    SELECT @ERR AS err, @MSG AS MSG  
  
GO
/****** Object:  StoredProcedure [dbo].[InsertaProyectosSNAT49]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Daniel Orozco
-- Create date: 29/06-2018
-- Description:	Inserta en SNAT Simplificado los proyectos obtenidos desde Rukan
-- =============================================
CREATE PROCEDURE [dbo].[InsertaProyectosSNAT49]
	@IDMAESTROALTERNATIVAPOSTULACION BigInt,
	@CODIGOPROYECTOINFORMACIONPROYECTO BigInt,
	@NOMBREPROYECTOINFORMACIONPROYECTO VarChar(200),
	@FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTO DateTime,
	@ESTADOPROYECTOINFORMACIONPROYECTO Bit,
	@CANTIDADVIVIENDASINFORMACIONPROYECTO int,
	@CANTIDADBENEFICIARIOSINFORMACIONPROYECTO int,
	@AGNOFACTIBILIDADINFORMACIONPROYECTO Int,
	@MONTOSUBSIDIOBASEINFORMACIONPROYECTO decimal(18,3),
	@MONTOSUBSIDIOBASEFACTIBILIDADINFORMACIONPROYECTO decimal(18,3),
	@MARCALOCALIZACIONINFORMACIONPROYECTO bit,
	@MARCAFACTIBILIZACIONINFORMACIONPROYECTO bit,
	@FECHAFACTIBILIZACIONINFORMACIONPROYECTO DateTime,
	@NUMEROFAMILIASABSCRITASINFORMACIONPROYECTO Int,
	@CODIGOCOMUNADIRECCION int,
	@CODIGOPROVINCIADIRECCION int,
	@CODIGOREGIONDIRECCION int,
	@RUTENTIDAD int,
	@DIGITOENTIDAD char(1),
	@NOMBREENTIDAD VarChar(200),
	@RUTEMPRESACONSTRUCTORA int,
	@DIGITOEMPRESACONSTRUCTORA char(1),
	@NOMBREEMPRESACONSTRUCTORA VarChar(200),
	@DESCTIPOMODALIDAD VarChar(200)
AS
BEGIN
	BEGIN TRAN
	BEGIN
		--INICIO -- Inserta Direccion
		IF NOT((@CODIGOCOMUNADIRECCION IS NULL) OR (@CODIGOPROVINCIADIRECCION IS NULL) OR (@CODIGOREGIONDIRECCION IS NULL))
		BEGIN
			DECLARE @IDDIRECCION BigInt
			SET @IDDIRECCION = (SELECT MAX(IDDIRECCION) FROM DIRECCION) + 1

			INSERT INTO DIRECCION(IDDIRECCION,NUMERODIRECCION,CODIGOCOMUNADIRECCION,CODIGOPROVINCIADIRECCION,CODIGOREGIONDIRECCION)
			VALUES(@IDDIRECCION,NULL,@CODIGOCOMUNADIRECCION,@CODIGOPROVINCIADIRECCION,@CODIGOREGIONDIRECCION)

			IF(@@ERROR <> 0)
			BEGIN
				ROLLBACK SELECT 'ERROR EN INSERTAR DIRECCION'
			END
		END
		--FIN -- Inserta Direccion

		--INICIO -- Ingreso Proveedor
		DECLARE @Ciclo int
		SET @Ciclo = 1

		DECLARE @IDProveedor1 BigInt,
				@IDProveedor2 BigInt

		SET @IDProveedor1 = 0
		SET @IDProveedor2 = 0

		WHILE(@Ciclo >= 0)
		BEGIN
			DECLARE @IDMAESTROTIPOPROVEEDOR Int,--1 EP / --2 EC
					@RUTPROVEEDOR Int,
					@RUTDVPROVEEDOR Char(1),
					@NOMBREPROVEEDOR VarChar(200)
			
			IF(@Ciclo = 1)
			BEGIN
				SET @IDMAESTROTIPOPROVEEDOR = 1
				SET @RUTPROVEEDOR = @RUTENTIDAD
				SET @RUTDVPROVEEDOR = @DIGITOENTIDAD
				SET @NOMBREPROVEEDOR = @NOMBREENTIDAD
			END
			ELSE
			BEGIN
				SET @IDMAESTROTIPOPROVEEDOR = 2
				SET @RUTPROVEEDOR = @RUTEMPRESACONSTRUCTORA
				SET @RUTDVPROVEEDOR = @DIGITOEMPRESACONSTRUCTORA
				SET @NOMBREPROVEEDOR = @NOMBREEMPRESACONSTRUCTORA
			END

			IF(@RUTPROVEEDOR <> 0)
			BEGIN
				DECLARE @IDPROVEEDOR BigInt

				IF(@Ciclo = 1)
				BEGIN
					SET @IDProveedor1 = @IDPROVEEDOR
				END
				ELSE
				BEGIN
					SET @IDProveedor2 = @IDPROVEEDOR
				END

				IF NOT EXISTS(SELECT 1 FROM PROVEEDOR WHERE RUTPROVEEDOR = @RutProveedor)
				BEGIN
					SET @IDPROVEEDOR = (SELECT MAX(IDPROVEEDOR) FROM PROVEEDOR) + 1

					INSERT INTO PROVEEDOR(IDPROVEEDOR,IDMAESTROTIPOPROVEEDOR,NOMBREPROVEEDOR,RUTPROVEEDOR,DVPROVEDIGITOVERIFICADORPROVEEDOR)
					VALUES(@IDPROVEEDOR,@IDMAESTROTIPOPROVEEDOR,@NOMBREPROVEEDOR,@RUTPROVEEDOR,@RUTDVPROVEEDOR)
				END
				ELSE
				BEGIN
					SET @IDPROVEEDOR = (SELECT IDPROVEEDOR FROM PROVEEDOR WHERE RUTPROVEEDOR = @RutProveedor)
				END
			END

			SET @Ciclo = (@Ciclo - 1)
		END

		IF(@@ERROR <> 0)
		BEGIN
			ROLLBACK SELECT 'ERROR EN INSERTAR PROVEEDOR'
		END
		--FIN -- Inserta Proveedor

		--INICIO -- MODALIDAD
		DECLARE @IDMAESTROMODALIDAD BigInt

		IF NOT(@DESCTIPOMODALIDAD IS NULL)
		BEGIN
			IF(UPPER(LTRIM(RTRIM(@DESCTIPOMODALIDAD))) = 'AVC')
			BEGIN
				SET @IDMAESTROMODALIDAD = 7
			END
			ELSE
			BEGIN
				SET @IDMAESTROMODALIDAD = 2
			END
		END
		ELSE
		BEGIN
			SET @IDMAESTROMODALIDAD = NULL
		END
		--FIN -- MODALIDAD
		
		--INICIO -- Ingreso Proyecto
		DECLARE @IDINFORMACIONPROYECTO BigInt
		
		SET @IDINFORMACIONPROYECTO = (SELECT MAX(IDINFORMACIONPROYECTO) FROM INFORMACION_PROYECTO) + 1

		INSERT INTO INFORMACION_PROYECTO(
			IDINFORMACIONPROYECTO,
			IDDIRECCION,
			IDMAESTROTITULO,
			IDMAESTROMODALIDAD,
			IDMAESTROLLAMADO,
			IDMAESTROALTERNATIVAPOSTULACION,
			CODIGOPROYECTOINFORMACIONPROYECTO,
			NOMBREPROYECTOINFORMACIONPROYECTO,
			FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTO,
			ESTADOPROYECTOINFORMACIONPROYECTO,
			RESOLUCIONATINFORMACIONPROYECTO,
			FECHARESOLUCIONATINFORMACIONPROYECTO,
			CANTIDADVIVIENDASINFORMACIONPROYECTO,
			CANTIDADBENEFICIARIOSINFORMACIONPROYECTO,
			IDMAESTROPROGRAMA,
			AGNOFACTIBILIDADINFORMACIONPROYECTO,
			MONTOSUBSIDIOBASEINFORMACIONPROYECTO,
			MONTOSUBSIDIOBASEFACTIBILIDADINFORMACIONPROYECTO,
			MARCALOCALIZACIONINFORMACIONPROYECTO,
			MARCAFACTIBILIZACIONINFORMACIONPROYECTO,
			FECHAFACTIBILIZACIONINFORMACIONPROYECTO,
			NUMEROFAMILIASADSCRITASINFORMACIONPROYECTO)
		VALUES (@IDINFORMACIONPROYECTO,
				@IDDIRECCION,
				NULL,--Maestro Titulo
				@IDMAESTROMODALIDAD,
				NULL,--MAESTROLLAMADO
				@IDMAESTROALTERNATIVAPOSTULACION,
				@CODIGOPROYECTOINFORMACIONPROYECTO,
				@NOMBREPROYECTOINFORMACIONPROYECTO,
				@FECHACALIFICACIONDEFINITIVAINFORMACIONPROYECTO,
				@ESTADOPROYECTOINFORMACIONPROYECTO,
				NULL,--@RESOLUCIONATINFORMACIONPROYECTO
				NULL,--@FECHARESOLUCIONATINFORMACIONPROYECTO
				@CANTIDADVIVIENDASINFORMACIONPROYECTO,
				@CANTIDADBENEFICIARIOSINFORMACIONPROYECTO,
				3,--DS49 (3)
				@AGNOFACTIBILIDADINFORMACIONPROYECTO,
				@MONTOSUBSIDIOBASEINFORMACIONPROYECTO,
				@MONTOSUBSIDIOBASEFACTIBILIDADINFORMACIONPROYECTO,
				@MARCALOCALIZACIONINFORMACIONPROYECTO,
				@MARCAFACTIBILIZACIONINFORMACIONPROYECTO,
				@FECHAFACTIBILIZACIONINFORMACIONPROYECTO,
				@NUMEROFAMILIASABSCRITASINFORMACIONPROYECTO)

		IF(@@ERROR <> 0)
		BEGIN
			ROLLBACK SELECT 'ERROR AL INSERTAR PROYECTO'
		END
		--FIN -- Ingreso Proyecto

		--INICIO -- Ingreso ProveedorNomalizada
		IF(@IDProveedor1 <> 0)
		BEGIN
			INSERT INTO TIPO_PROVEEDOR_INFORMACION_PROYECTO(IDPROVEEDOR,IDINFORMACIONPROYECTO,ESTADOTIPOPROVEEDORINFORMACIONPROYECTO)
			VALUES(@IDProveedor1,@IDINFORMACIONPROYECTO,1)
		END
		ELSE IF(@IDProveedor2 <> 0)
		BEGIN
			INSERT INTO TIPO_PROVEEDOR_INFORMACION_PROYECTO(IDPROVEEDOR,IDINFORMACIONPROYECTO,ESTADOTIPOPROVEEDORINFORMACIONPROYECTO)
			VALUES(@IDProveedor2,@IDINFORMACIONPROYECTO,1)
		END

		IF(@@ERROR <> 0)
		BEGIN
			ROLLBACK SELECT 'ERROR AL INSERTAR TIPOPROVEEDORINFORMACIONPROYECTO'
		END
		--FIN -- Ingreso ProveedorNomalizada

		COMMIT TRAN
			SELECT 'OK'
	END
END


GO
/****** Object:  StoredProcedure [dbo].[SNAT_SIMPLIFICADO_V_CONSULTA_ANO_CONTRATO_ATP]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



/********************************************************************************************      
* Nombre del procedimiento      :  SNAT_SIMPLIFICADO_V_CONSULTA_ANO_CONTRATO_ATP
* Fecha de creación             :  17/01/2019
* Usuario de creación           :  Cfajardo     
* Versión.                      :  V.0.0.0.1      

* Fecha de modificación         :  
* Usuarios de modificación      :  
* Motivo de modificación        :     
      
* Visado por DBA                : 
* Fecha Aprobación DBA          : 
* Comentarios DBA               :       

* Objetivo                      : Procedimiento almacenado para rescatar los años en la tabla CONTRATO_ATP.      
* Objetos Utilizados			: CONTRATO_ATP
      

                            
**********************************************************************************************/      
CREATE PROCEDURE [dbo].[SNAT_SIMPLIFICADO_V_CONSULTA_ANO_CONTRATO_ATP]      
AS       

declare @cero int
set @cero = 0

     
begin
if((select COUNT (AGNOPRESUPUESTOCONTRATOATP) from CONTRATO_ATP) > @cero)
begin 
select distinct AGNOPRESUPUESTOCONTRATOATP from CONTRATO_ATP where AGNOPRESUPUESTOCONTRATOATP > @cero
end
else 
begin
SELECT TOP 1 YEAR(GETDATE()) as AGNOPRESUPUESTOCONTRATOATP
end
end



GO
/****** Object:  StoredProcedure [dbo].[SNAT_SIMPLIFICADO_V_CONSULTA_SERVICIOS_SOLICITUD_ATP]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/********************************************************************************************            
Nombre del Procedimiento: SNAT_SIMPLIFICADO_V_CONSULTA_SERVICIOS_SOLICITUD_ATP
Fecha de creación  : 01/04/2017
Usuario de creación  : Cfajardo
Versión     : 1.0                                      
      
FECHA DE MODIFICACIÓN   :  --
USUARIO DE MODIFICACIÓN : 
MOTIVO DE MODIFICACIÓN  : 
      
        
VISADO POR (DBA)     : --
FECHA APROBACIÓN DBA : --
COMENTARIOS DBA.     :   
        
Objetivo     : Procedimiento que muestra los servicios de la solicitud de pago
            
Tablas       : TIPO_ACTIVIDAD_MONTO
               MAESTRO_SERVICIO
               
            
Que retorna     : NOMBREMAESTROSERVICIO
				  MONTOPAGOTIPOACTIVIDADMONTO
				  @nIdSolicitudPago
				  @nRegion
				  @nResolucionContrato
				  @nFechaResolucionContrato
				  @nMontoContrato
				  @nRutProveedor
				  @nMontoAutorizacionPago
				  @nFechaDevengo             
            
Parametros :  @nIdSolicitudPago bigint,
			  @nRegion varchar(200),
			  @nResolucionContrato varchar(200),
			  @nFechaResolucionContrato varchar(200),
			  @nMontoContrato varchar (200),
			  @nRutProveedor varchar (200),
			  @nMontoAutorizacionPago varchar (200),
			  @nFechaDevengo varchar (200)

Comando de Prueba :
            
********************************************************************************************/            
            
CREATE PROCEDURE [dbo].[SNAT_SIMPLIFICADO_V_CONSULTA_SERVICIOS_SOLICITUD_ATP]  
(      
    @nIdSolicitudPago bigint,
	@nRegion varchar(200),
	@nResolucionContrato varchar(200),
	@nFechaResolucionContrato varchar(200),
	@nMontoContrato varchar (200),
	@nRutProveedor varchar (200),
	@nMontoAutorizacionPago varchar (200),
	@nFechaDevengo varchar (200)
	

)              
AS            
BEGIN          

											 
select  IDSOLICITUDATP,NOMBREMAESTROSERVICIO, REPLACE(REPLACE(CONVERT(VARCHAR,CONVERT(Money, MONTOPAGOTIPOACTIVIDADMONTO),1),'.00',''),',','.') as MONTOPAGOTIPOACTIVIDADMONTO  from TIPO_ACTIVIDAD_MONTO a inner join MAESTRO_SERVICIO b on a.IDMAESTROSERVICIO = b.IDMAESTROSERVICIO
                                            where a.IDSOLICITUDATP = @nIdSolicitudPago
END 


GO
/****** Object:  StoredProcedure [dbo].[SNAT_SIMPLIFICADO_V_UPD_AUTORIZACION_ORDEN_PAGO_SIMP]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/********************************************************************************************      
* Nombre del procedimiento      :  SNAT_SIMPLIFICADO_V_UPD_AUTORIZACION_ORDEN_PAGO_SIMP      
* Fecha de creación             :  29/11/2018      
* Usuario de creación           :  Marcelo Miola R.-      
* Versión.                      :  V.0.0.0.1      

* Fecha de modificación         :  Marcelo Miola
* Usuarios de modificación      :  10/07/2019
* Motivo de modificación        :  Incorporación de estado de reversa

* Fecha de modificación         :  
* Usuarios de modificación      :  
* Motivo de modificación        :      
      
* Visado por DBA                : Iván Frade Cortés
* Fecha Aprobación DBA          : 11072019
* Comentarios DBA               :   ---    

* Objetivo                      : Procedimiento almacenado para ser utilizado por SPS
                                  para actualizar estado de una autorización de pago he indicar
								  que esta autorizado.      
* Objetos Utilizados y para Que : Tablas       
      
* Parámetros de retorno         : 
                    
SNAT_SIMPLIFICADO_V_UPD_AUTORIZACION_ORDEN_PAGO_SIMP '7777777052018', 'mmiola', 0, 'Se devuelve autorización'           
                            
**********************************************************************************************/      
CREATE PROCEDURE [dbo].[SNAT_SIMPLIFICADO_V_UPD_AUTORIZACION_ORDEN_PAGO_SIMP]      
@ORD_PAG_NUM_DOC Varchar(20), 			 -- Número de orden de pago (@NumeroDocumento)      
@USER varchar(50), 						 -- usuario      
@IndicaATPrevia int, 					 -- 0 No, 1 Si
@Comentario varchar(500), 				 -- Informa resultado de la autorización
@FechaAutorizacion date,				 -- Indica de autorización
@ValorUfAutorizacion numeric(10,2),		 -- Devuelve valor de la UF el día de la autorización.
@MontoEnPesosAutorización numeric(20,2), -- Recibe el monto total autorizado en pesos.
@reversa int = null
AS       
SET DATEFORMAT DMY      
/****** Control Errores ***********/        
declare @err int, @msg varchar(250)        
set @err = 0        
set @msg = 'Información actualizada con éxito.'      
/**********************************/        
      
/**********************************/      
DECLARE @LOG_FECH DATETIME      
DECLARE @EST_PAG_ID INT, @FOL_ORD_PAG_ID INT   
DECLARE @ESTADO_PAGADO AS INT
DECLARE @ESTADO_PAGADO_ATP AS INT
DECLARE @ESTADO_REVERSADO INT
DECLARE @ESTADO_REVERSADO_ATP INT
DECLARE @REGISTRO varchar(50)
DECLARE  @VARUNO int, @VARCERO int 

DECLARE @ATZ_PAG_ID AS INT
      
set @FOL_ORD_PAG_ID = convert(int,left(@ORD_PAG_NUM_DOC,(len(@ORD_PAG_NUM_DOC)-6)))      
set @LOG_FECH = getdate()  
SET @ESTADO_PAGADO = 3
SET @ESTADO_REVERSADO = 4
SET @ESTADO_PAGADO_ATP = 9
SET @ESTADO_REVERSADO_ATP = 10
SET @REGISTRO = 'ACTUALIZACION '
SET @VARCERO = 0 -- Reversar autorización de simplificado      
SET @VARUNO =  1 -- Reversar autorización de AT previa.     
        
BEGIN TRANSACTION   
IF (@reversa is null)
	BEGIN
		IF  @IndicaATPrevia = @VARCERO
		BEGIN
			-- 'Actualiza SNAT Simplificado'
			UPDATE AUTORIZACION SET 
				IDMAESTROESTADOAUTORIZACION = @ESTADO_PAGADO,		--Se deja en estado PAGADO
				FECHAINYECCIONAUTORIZACION = @FechaAutorizacion,	--Fecha retornada desde SPS. que indica cunado se genero pago
				VALORUFPESOAUTORIZACION = @ValorUfAutorizacion,		--Valor de la uf con el que se pago.
				MONTOPESOAUTORIZACION = @MontoEnPesosAutorización	--Monto en pesos pagados.
			where IDAUTORIZACION = @FOL_ORD_PAG_ID
		
			if @@rowcount <= 0        
			begin        
				set @err = -1        
				set @msg = 'Error al actualizar estado de pago.'        
				goto error        
			end
		
		END
		ELSE IF @IndicaATPrevia = @VARUNO
		BEGIN
			-- 'Actualiza SNAT Simplificado'
			UPDATE SOLICITUD_PAGO_ATP SET 
				IDMAESTROESTADOSOLICITUD= @ESTADO_PAGADO_ATP --Se deja en estado EN RATIFICACIÓN
			where IDSOLICITUDATP = @FOL_ORD_PAG_ID
		
			if @@rowcount <= 0        
			begin        
				set @err = -1        
				set @msg = 'Error al actualizar estado de pago.'        
				goto error        
			end
		END
	END
ELSE
	BEGIN
		IF  @IndicaATPrevia = @VARCERO
		BEGIN
			-- 'Actualiza SNAT Simplificado'
			UPDATE AUTORIZACION SET 
				IDMAESTROESTADOAUTORIZACION= @ESTADO_REVERSADO,		--Se deja en estado PAGADO
				FECHAINYECCIONAUTORIZACION = @FechaAutorizacion,	--Fecha retornada desde SPS. que indica cunado se genero pago
				VALORUFPESOAUTORIZACION = @ValorUfAutorizacion,		--Valor de la uf con el que se pago.
				MONTOPESOAUTORIZACION = @MontoEnPesosAutorización	--Monto en pesos pagados.  --Se deja en estado REVERSADO
			where IDAUTORIZACION = @FOL_ORD_PAG_ID
		
			if @@rowcount <= 0        
			begin        
				set @err = -1        
				set @msg = 'Error al actualizar estado de pago.'        
				goto error        
			end
		
		END
		ELSE IF @IndicaATPrevia = @VARUNO
		BEGIN
			-- 'Actualiza SNAT Simplificado'
			UPDATE SOLICITUD_PAGO_ATP SET 
				IDMAESTROESTADOSOLICITUD= @ESTADO_REVERSADO_ATP --Se deja en estado EN RATIFICACIÓN
			where IDSOLICITUDATP = @FOL_ORD_PAG_ID
		
			if @@rowcount <= 0        
			begin        
				set @err = -1        
				set @msg = 'Error al actualizar estado de pago.'        
				goto error        
			end
		END
	END


	COMMIT TRANSACTION        
	select @err as CodMensaje , @msg as Mensaje, @ORD_PAG_NUM_DOC as IdORden         
	return        
error:          
    ROLLBACK  TRANSACTION        
    select @err as CodMensaje, @msg as Mensaje, @ORD_PAG_NUM_DOC  as IdORden


GO
/****** Object:  StoredProcedure [dbo].[SNAT_SIMPLIFICADO_V_UPD_REPARO_ORDEN_PAGO_SIMP]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/********************************************************************************************        
* Nombre del procedimiento      :  SNAT_SIMPLIFICADO_V_UPD_REPARO_ORDEN_PAGO_SIMP        
* Fecha de creación             :  29/11/2018        
* Usuario de creación           :  Marcelo Miola R.-        
* Versión.                      :  V.0.0.0.1        
  
Fecha de modificación   : 18-02-2019  
Usuario de modificación : cfajardo
Motivo de modificación  : regularización de procedimientos almacenados entre ambientes.
        
* Visado por DBA                :  Mirto Blanco 
* Fecha Aprobación DBA          :  20190220 
  
* Objetivo                      : Procedimiento almacenado para ser utilizado por SPS  
                                  para actualizar estado de una autorización de pago he indicar  
          que esta con reparos.        
* Objetos Utilizados y para Que : Tablas         
        
* Parámetros de retorno         : Exito  
                                  Fracaso  
  
SNAT_SIMPLIFICADO_V_UPD_REPARO_ORDEN_PAGO_SIMP '01052018', 'mmiola', 0, 'Se devuelve autorización'  
                              
**********************************************************************************************/        
CREATE PROCEDURE [dbo].[SNAT_SIMPLIFICADO_V_UPD_REPARO_ORDEN_PAGO_SIMP]        
@ORD_PAG_NUM_DOC Varchar(20),  -- Número de orden de pago (@NumeroDocumento)        
@USER varchar(50),     -- usuario        
@IndicaATPrevia int,    -- 0 No (=> Simplificado) , 1 Si (=> AT Previa)  
@Comentario varchar(500)   -- Informa motivo del reparo       
AS         
SET DATEFORMAT DMY        
/****** Control Errores ***********/          
declare @err int, @msg varchar(250)          
set @err = 0          
set @msg = 'Información actualizada con éxito.'        
/**********************************/          
        
/**********************************/        
DECLARE @LOG_FECH DATETIME        
DECLARE @EST_PAG_ID INT, @FOL_ORD_PAG_ID INT   
DECLARE @ESTADO_ENREPARO AS INT  
DECLARE @ESTADO_ENREPARO_ATP AS INT  
  
DECLARE @ATZ_PAG_ID AS INT  
        
        
set @FOL_ORD_PAG_ID = convert(int,left(@ORD_PAG_NUM_DOC,(len(@ORD_PAG_NUM_DOC)-6)))        
set @LOG_FECH = getdate()    
SET @ESTADO_ENREPARO = 1  
SET @ESTADO_ENREPARO_ATP = 6  
        
DECLARE @REGISTRO varchar(50)        
SET @REGISTRO = 'ACTUALIZACION '        
          
begin TRANSACTION         
DECLARE  @VARUNO int, @VARCERO int        
SET @VARCERO = 0 -- Reversar autorización de simplificado        
SET @VARUNO =  1 -- Reversar autorización de AT previa.  
  
IF  @IndicaATPrevia = @VARCERO  
 BEGIN  
  -- 'Actualiza SNAT Simplificado'  
  UPDATE AUTORIZACION SET   
   IDMAESTROESTADOAUTORIZACION= @ESTADO_ENREPARO --Se deja en estado EN RATIFICACIÓN  
  where IDAUTORIZACION = @FOL_ORD_PAG_ID  
    
  if @@rowcount <= 0          
  begin          
   set @err = -1          
   set @msg = 'Error al actualizar estado de pago.'          
   goto error          
  end  
    
 END  
ELSE IF @IndicaATPrevia = @VARUNO  
 BEGIN  
  -- 'Actualiza SNAT Simplificado'  
  UPDATE SOLICITUD_PAGO_ATP SET   
   IDMAESTROESTADOSOLICITUD= @ESTADO_ENREPARO_ATP --Se deja en estado EN RATIFICACIÓN  
  where IDSOLICITUDATP = @FOL_ORD_PAG_ID  
    
  if @@rowcount <= 0          
  begin          
   set @err = -1          
   set @msg = 'Error al actualizar estado de pago.'          
   goto error          
  end  
 END  
  
  
 COMMIT TRANSACTION          
 select @err as CodMensaje , @msg as Mensaje, @ORD_PAG_NUM_DOC as IdORden           
 return          
error:            
    ROLLBACK  TRANSACTION          
    select @err as CodMensaje, @msg as Mensaje, @ORD_PAG_NUM_DOC  as IdORden  

GO
/****** Object:  StoredProcedure [dbo].[SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/********************************************************************************************                        
Nombre del Procedimiento: [SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION]
Fecha de creación  : 09/11/2018                                 
Usuario de creación  : cfajardo                      
Versión     : 1.0                                                  
          
Fecha de Modificacion : --------          
Usuario de creación   : --------          
Motivo de modificacion: --------          
                    
* Visado por DBA                :  --          
* Fecha Aprobación DBA          :  --          
* Comentarios DBA               :  --                   
                        
Objetivo     : procedimiento almacenado que obtiene las autorizaciones generadas          
                        
Tablas      : CARACTERISTICAS_ESPECIALES           
              INFORMACION_PROYECTO           
     DIRECCION          
     TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA         SERVICIO_P ARCIALIDAD          
     TIPO_PROVEEDOR_INFORMACION_PROYECTO           
     PROVEEDOR          
                            
                              
                                 
Vistas      : ---                        
Otros      : ---                        
                        
Que retorna     :
              
Parametros     :

Proyecto     : SNAT SIMPLIFICADO                        
Responsable  : CFAJARDO                 

********************************************************************************************/                        
  
CREATE PROCEDURE [dbo].[SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_AUTORIZACION]                        
(
	@pCODIGOPROYECTO varchar(15) = null,
	@pIDPROGRAMA bigint = null,
	@pIDTIPOLOGIA bigint = null,
	@pIDLLAMADO bigint = null,
	@pIDTIPOPROVEEDOR bigint = null,
	@pNOMBREPROVEEDOR VARCHAR (255) = null,
	@pIDSERVICIO bigint = null,
	@pREGION bigint = null,
	@pPROVINCIA bigint = null,
	@pCOMUNA  bigint = null,
	@pIDMODALIDAD  bigint = null,
	@pIDAUTORIZACION  bigint = null,
	@pIDMAESTROTITULO  bigint = null,
	@pIDMAESTROESTADOAUTORIZACION  bigint = null
)        
AS
BEGIN
	DECLARE @CONDICION AS VARCHAR (max),
			@final AS VARCHAR (max),
			@CONSULTA1 AS VARCHAR (max),
			@group AS VARCHAR (max),
			@WHERE AS VARCHAR (4),
			@AND AS VARCHAR (3),
			@ESPACIO AS VARCHAR (1)

	SET @WHERE ='WHERE'
	SET @AND ='AND'
	SET @ESPACIO =' '
	SET @CONDICION = ''

	BEGIN
		IF(@pCODIGOPROYECTO IS NOT NULL)
		BEGIN
			SET @CONDICION = @CONDICION + ' ip.CODIGOPROYECTOINFORMACIONPROYECTO = '+ CONVERT(varchar(max), @pCODIGOPROYECTO) + @ESPACIO + @AND
		END
	
		IF(@pIDPROGRAMA IS NOT NULL)
		BEGIN
			SET @CONDICION = @CONDICION + ' ip.IDMAESTROPROGRAMA = '+CONVERT(varchar(max),  @pIDPROGRAMA )+ @ESPACIO + @AND
		END

		IF(@pIDTIPOLOGIA IS NOT NULL)
		BEGIN
			SET @CONDICION = @CONDICION + ' a.IDMAESTROTIPOLOGIA = '+ CONVERT(varchar(max), @pIDTIPOLOGIA) + @ESPACIO + @AND
		END
	
		IF(@pIDAUTORIZACION IS NOT NULL)
		BEGIN
			SET @CONDICION = @CONDICION + ' a.IDAUTORIZACION = '+ CONVERT(varchar(max), @pIDAUTORIZACION) + @ESPACIO + @AND
		END

		IF(@pIDLLAMADO IS NOT NULL)
		BEGIN
			SET @CONDICION = @CONDICION + ' ip.IDMAESTROLLAMADO = '+ CONVERT(varchar(max),  @pIDLLAMADO) + @ESPACIO + @AND
		END

		IF(@pIDTIPOPROVEEDOR IS NOT NULL)
		BEGIN
			SET @CONDICION = @CONDICION +  ' pp.IDMAESTROTIPOPROVEEDOR = '+CONVERT(varchar(max),  @pIDTIPOPROVEEDOR) + @ESPACIO + @AND
		END
	
		IF(@pNOMBREPROVEEDOR IS NOT NULL)
		BEGIN
			SET @CONDICION = @CONDICION + ' pp.NOMBREPROVEEDOR like '''+ '%' + @pNOMBREPROVEEDOR +'%'+''''+ @ESPACIO + @AND
		END
	
		IF(@pIDMODALIDAD IS NOT NULL)
		BEGIN
			SET @CONDICION = @CONDICION + ' ip.IDMAESTROMODALIDAD = '+CONVERT(varchar(max),  @pIDMODALIDAD) + @ESPACIO + @AND
		END
	
		IF(@pREGION is not null)
		BEGIN
			SET @CONDICION = @CONDICION + ' d.CODIGOREGIONDIRECCION = '+ CONVERT(varchar(max), @pREGION) + @ESPACIO + @AND
		END
	
		IF(@pPROVINCIA IS NOT NULL)
		BEGIN
			SET @CONDICION = @CONDICION + ' d.CODIGOPROVINCIADIRECCION = '+ CONVERT(varchar(max), @pPROVINCIA) + @ESPACIO + @AND
		END
	
		IF(@pCOMUNA IS NOT NULL)
		BEGIN
			SET @CONDICION = @CONDICION + ' d.CODIGOCOMUNADIRECCION = '+CONVERT(varchar(max),  @pCOMUNA) + @ESPACIO + @AND
		END
	
		IF(@pIDMAESTROTITULO IS NOT NULL)
		BEGIN
			SET @CONDICION = @CONDICION + ' ip.IDMAESTROTITULO = '+CONVERT(varchar(max),  @pIDMAESTROTITULO) + @ESPACIO + @AND
		END
	
		IF(@pIDMAESTROESTADOAUTORIZACION IS NOT NULL)
		BEGIN
			SET @CONDICION = @CONDICION + ' a.IDMAESTROESTADOAUTORIZACION = '+CONVERT(varchar(max),  @pIDMAESTROESTADOAUTORIZACION) + @ESPACIO + @AND
		END

		IF(@pIDSERVICIO IS NOT NULL)          
		BEGIN
			SET @CONDICION = @CONDICION + '  tss.IDMAESTROSERVICIO = '+CONVERT(varchar(max),  @pIDSERVICIO) + @ESPACIO + @AND           
		END
	END
	
	BEGIN
		SET @CONSULTA1 = 'SELECT	a.NUMEROAUTORIZACION,
									a.IDAUTORIZACION,
									a.CODIGOREGIONAUTORIZACION,
									a.CANTIDADPROYECTOSAUTORIZACION,
									(SELECT	NOMBREMAESTROPROGRAMA
									FROM	MAESTRO_PROGRAMA
									WHERE	IDMAESTROPROGRAMA = a.IDMAESTROPROGRAMA) as PROGRAMA,
									(SELECT NOMBREABREVIADOMAESTROTIPOLOGIA
									FROM	MAESTRO_TIPOLOGIA
									WHERE	IDMAESTROTIPOLOGIA = a.IDMAESTROTIPOLOGIA) AS TIPOLOGIA,
									(SELECT	(SELECT	tp.NOMBREMAESTROTIPOPROVEEDOR
											FROM	MAESTRO_TIPO_PROVEEDOR tp
											WHERE	tp.IDMAESTROTIPOPROVEEDOR = p.IDMAESTROTIPOPROVEEDOR)
									FROM PROVEEDOR p
									WHERE	p.IDPROVEEDOR = a.IDPROVEEDOR) as TIPO_PROVEEDOR,
									pp.RUTPROVEEDOR AS RUT,
									pp.DVPROVEDIGITOVERIFICADORPROVEEDOR AS DV,
									'''' AS RUT_PROVEEDOR,
									(SELECT  p.NOMBREPROVEEDOR
									FROM	PROVEEDOR p
									WHERE	p.IDPROVEEDOR = a.IDPROVEEDOR) as NOMBREPROVEEDOR,
									a.MONTOTOTALAUTORIZACION,
									EA.NOMBREMAESTROESTADOAUTORIZACION,
									a.ESPECIALAUTORIZACION,
									Convert(varchar,a.FECHAINGRESOAUTORIZACION,105) AS FECHAINGRESOAUTORIZACION,
									(SELECT	p.IDMAESTROTIPOPROVEEDOR
									FROM PROVEEDOR p
									WHERE	p.IDPROVEEDOR = a.IDPROVEEDOR) as ID_TIPO_PROVEEDOR
							FROM	AUTORIZACION a
								INNER JOIN	TIPO_AUTORIZACION au
									ON a.IDAUTORIZACION = au.IDAUTORIZACION
								INNER JOIN	SOLICITUD_PAGO s
									ON  au.IDSOLICITUDPAGO = s.IDSOLICITUDPAGO
								INNER JOIN	CARACTERISTICAS_ESPECIALES ce
									ON s.IDCARACTERISTICASESPECIALES = ce.IDCARACTERISTICASESPECIALES
								INNER JOIN	INFORMACION_PROYECTO ip
									ON ce.IDINFORMACIONPROYECTO = ip.IDINFORMACIONPROYECTO
								INNER JOIN	DIRECCION d
									ON ip.IDDIRECCION = d.IDDIRECCION
								INNER JOIN	TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA tspc
									ON ce.IDCARACTERISTICASESPECIALES =  tspc.IDCARACTERISTICASESPECIALES
								INNER JOIN	SERVICIO_PARCIALIDAD spp
									ON tspc.IDSERVICIOPARCIALIDAD =  spp.IDSERVICIOPARCIALIDAD
								INNER JOIN	TIPOLOGIA_SERVICIO tss
									ON spp.IDTIPOLOGIASERVICIO = tss.IDTIPOLOGIASERVICIO
								INNER JOIN	PROVEEDOR pp
									ON s.IDPROVEEDOR = pp.IDPROVEEDOR
								INNER JOIN	MAESTRO_ESTADO_AUTORIZACION EA
									ON a.IDMAESTROESTADOAUTORIZACION = EA.IDMAESTROESTADOAUTORIZACION'
		SET @CONDICION = 'WHERE	a.NUMEROAUTORIZACION > 0
							AND tspc.IDCARACTERISTICASESPECIALES = ce.IDCARACTERISTICASESPECIALES
							AND tspc.MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA > 0
							AND tspc.IDSOLICITUDPAGO > 0
							AND ' + @CONDICION

		SET @group ='GROUP BY	a.NUMEROAUTORIZACION,
								a.IDAUTORIZACION,
								a.CODIGOREGIONAUTORIZACION,
								a.CANTIDADPROYECTOSAUTORIZACION,
								a.IDMAESTROPROGRAMA,
								a.IDMAESTROTIPOLOGIA,
								a.IDPROVEEDOR,
								pp.RUTPROVEEDOR,
								pp.DVPROVEDIGITOVERIFICADORPROVEEDOR,
								a.MONTOTOTALAUTORIZACION,
								EA.NOMBREMAESTROESTADOAUTORIZACION,
								a.ESPECIALAUTORIZACION,
								a.FECHAINGRESOAUTORIZACION,
								a.IDMAESTROESTADOAUTORIZACION'
		
		IF(Len(@CONDICION) > 1)
		BEGIN
			SET @CONDICION = SUBSTRING (@CONDICION, 1, Len(@CONDICION) - 3)
		END

		IF(Len(@CONDICION) > 5)
		BEGIN
			SET @final = @CONSULTA1 +' '+ @CONDICION +' '+ @group
		END

		SET @final = @CONSULTA1 +' '+ @CONDICION +' '+ @group
		
		DECLARE	@AUTORIZACIONES TABLE(
			NUMEROAUTORIZACION bigint,
			IDAUTORIZACION bigint,
			CODIGOREGIONAUTORIZACION bigint,
			CANTIDADPROYECTOSAUTORIZACION bigint,
			PROGRAMA varchar(max),
			TIPOLOGIA varchar(max),
			TIPO_PROVEEDOR varchar(max),
			RUT int,
			DV char(1),
			RUT_PROVEEDOR varchar(max),
			NOMBREPROVEEDOR varchar(max),
			MONTOTOTALAUTORIZACION DECIMAL(18,3),
			NOMBREMAESTROESTADOAUTORIZACION varchar(max),
			ESPECIALAUTORIZACION BIT,
			FECHAINGRESOAUTORIZACION varchar(10),
			ID_TIPO_PROVEEDOR bigint)
		
		INSERT INTO @AUTORIZACIONES
		EXEC (@final)
		
		SELECT	a.NUMEROAUTORIZACION,
				a.IDAUTORIZACION,
				a.CODIGOREGIONAUTORIZACION,
				a.CANTIDADPROYECTOSAUTORIZACION,
				a.PROGRAMA,
				a.TIPOLOGIA,
				a.TIPO_PROVEEDOR,
				a.RUT,
				a.DV,
				RUT_PROVEEDOR = Convert(varchar,a.RUT) + '-' + CONVERT(varchar,a.DV),
				a.NOMBREPROVEEDOR,
				a.MONTOTOTALAUTORIZACION,
				a.NOMBREMAESTROESTADOAUTORIZACION,
				a.ESPECIALAUTORIZACION,
				a.FECHAINGRESOAUTORIZACION,
				a.ID_TIPO_PROVEEDOR
		FROM	@AUTORIZACIONES a
		ORDER BY	a.IDAUTORIZACION DESC
	END
END


GO
/****** Object:  StoredProcedure [dbo].[SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_SOLICITUDES_PAGO]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
/********************************************************************************************          
NOMBRE DEL PROCEDIMIENTO  :[SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_SOLICITUDES_PAGO]           
FECHA DE CREACIÓN        : 29/08/2018          
USUARIO DE CREACIÓN       : Cristobal Fajardo        
VERSIÓN               : 1.0                
                            
Fecha de Modificacion : 02/10/2018          
Usuario de creación   : Daniel Orozco          
Motivo de modificacion: Se agrega Filtro para no obtener las Solicitudes Eliminadas y cambios para obtencion correcta de datos de Autorizacion        
      
Fecha de Modificacion : 02/10/2018          
Usuario de creación   : Daniel Orozco          
Motivo de modificacion: Se modifica el tipo de dato del parametro @pCODIGOPROYECTO a varchar       
        
* Visado por DBA                :   Mirto Blanco
* Fecha Aprobación DBA          :   20190906    
                  
OBJETIVO         : procedimiento almacenado que obtiene las solicitudes de pago generadas                    
          
TABLAS          :CARACTERISTICAS_ESPECIALES                     
              INFORMACION_PROYECTO        
        DIRECCION                    
        TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA            
        SERVICIO_PARCIALIDAD                    
        TIPO_PROVEEDOR_INFORMACION_PROYECTO                     
        PROVEEDOR          
          
                  
          
QUE RETORNA        : --        
          
PARAMETROS         :@pCODIGOPROYECTO bigint = null,        
        @pIDPROGRAMA bigint = null,        
        @pIDTIPOLOGIA bigint = null,        
        @pIDLLAMADO bigint = null,        
        @pIDTIPOPROVEEDOR bigint = null,        
        @pNOMBREPROVEEDOR VARCHAR (255) = null,        
        @pNOMBRESERVICIO varchar(max) = null,        
        @pREGION bigint = null,        
        @pPROVINCIA bigint = null,        
        @pCOMUNA  bigint = null,        
        @pIDMODALIDAD  bigint = null,        
        @pIDAUTORIZACION  bigint = null        
                    
PRUEBA      :                           
        
                   
PROYECTO         : SNAT SIMPLIFICADO        
RESPONSABLE        : DINFO          
                 
********************************************************************************************/                               
            
CREATE PROCEDURE [dbo].[SNAT_SIMPLIFICADO_V_USP_CON_LISTADO_SOLICITUDES_PAGO]        
(        
 @pCODIGOPROYECTO varchar(15) = null,          
 @pIDPROGRAMA bigint = null,        
 @pIDTIPOLOGIA bigint = null,        
 @pIDLLAMADO bigint = null,        
 @pIDTIPOPROVEEDOR bigint = null,        
 @pNOMBREPROVEEDOR VARCHAR (255) = null,        
 @pNOMBRESERVICIO varchar(max) = null,        
 @pREGION bigint = null,        
 @pPROVINCIA bigint = null,        
 @pCOMUNA  bigint = null,        
 @pIDMODALIDAD  bigint = null,        
 @pIDAUTORIZACION  bigint = null        
)        
AS                         
BEGIN                    
 DECLARE @CONDICION AS VARCHAR (max),        
   @final AS VARCHAR (max),        
   @CONSULTA1 AS VARCHAR (max),        
   @group AS VARCHAR (max),        
   @WHERE AS VARCHAR (4),        
   @AND AS VARCHAR (3),        
   @ESPACIO AS VARCHAR (1)        
        
 SET @WHERE ='WHERE'        
 SET @AND ='AND'        
 SET @ESPACIO =' '        
 set @CONDICION = ''        
          
 BEGIN          
  IF(@pCODIGOPROYECTO IS NOT NULL)            
  BEGIN            
   SET @CONDICION = @CONDICION + ' ip.CODIGOPROYECTOINFORMACIONPROYECTO = '+ CONVERT(varchar(max), @pCODIGOPROYECTO) + @ESPACIO + @AND            
  END            
              
  IF(@pIDPROGRAMA IS NOT NULL)            
  BEGIN            
   SET @CONDICION = @CONDICION + ' ip.IDMAESTROPROGRAMA = '+ CONVERT(varchar(max),  @pIDPROGRAMA )+ @ESPACIO + @AND            
  END            
              
  IF(@pIDTIPOLOGIA IS NOT NULL)            
  BEGIN            
   SET @CONDICION = @CONDICION + ' ips.IDMAESTROTIPOLOGIA = '+ CONVERT(varchar(max), @pIDTIPOLOGIA) + @ESPACIO + @AND            
  END            
              
  IF(@pIDLLAMADO IS NOT NULL)            
  BEGIN            
   SET @CONDICION = @CONDICION + ' ip.IDMAESTROLLAMADO = '+ CONVERT(varchar(max), @pIDLLAMADO) + @ESPACIO + @AND            
  END            
              
  IF(@pIDTIPOPROVEEDOR IS NOT NULL)            
  BEGIN            
   SET @CONDICION = @CONDICION +  ' pp.IDMAESTROTIPOPROVEEDOR = '+ CONVERT(varchar(max),  @pIDTIPOPROVEEDOR) + @ESPACIO + @AND            
  END            
              
  IF(@pNOMBREPROVEEDOR IS NOT NULL)            
  BEGIN            
   SET @CONDICION = @CONDICION + ' pp.NOMBREPROVEEDOR like ''' +'%'+ @pNOMBREPROVEEDOR +'%'+ ''''+ @ESPACIO + @AND          
  END            
              
  IF(@pIDMODALIDAD IS NOT NULL)            
  BEGIN            
   SET @CONDICION = @CONDICION + ' ip.IDMAESTROMODALIDAD = '+CONVERT(varchar(max),  @pIDMODALIDAD) + @ESPACIO + @AND            
  END            
              
  IF(@pREGION is not null)            
  BEGIN            
   SET @CONDICION = @CONDICION + ' d.CODIGOREGIONDIRECCION = '+ CONVERT(varchar(max), @pREGION) + @ESPACIO + @AND            
  END            
              
  IF(@pPROVINCIA IS NOT NULL)            
  BEGIN            
   SET @CONDICION = @CONDICION + ' d.CODIGOPROVINCIADIRECCION = '+ CONVERT(varchar(max), @pPROVINCIA) + @ESPACIO + @AND            
  END            
              
  IF(@pCOMUNA IS NOT NULL)            
  BEGIN            
   SET @CONDICION = @CONDICION + ' d.CODIGOCOMUNADIRECCION = '+CONVERT(varchar(max),  @pCOMUNA) + @ESPACIO + @AND            
  END        
 END            
        
 BEGIN            
  SET @CONSULTA1 = 'select    
         ce.IDCARACTERISTICASESPECIALES,    
         ce.IDMAESTROPROGRAMA,    
         s.IDSOLICITUDPAGO,    
         (SELECT (SELECT (SELECT mastp.NOMBREMAESTROTIPOPROVEEDOR    
             FROM MAESTRO_TIPO_PROVEEDOR mastp    
             WHERE mastp.IDMAESTROTIPOPROVEEDOR = prov.IDMAESTROTIPOPROVEEDOR)    
           FROM PROVEEDOR prov    
           WHERE prov.IDPROVEEDOR = ippinp.IDPROVEEDOR)    
         FROM SOLICITUD_PAGO ippinp    
         WHERE IDSOLICITUDPAGO = s.IDSOLICITUDPAGO) AS TIPO_PROVEEDOR,    
         (SELECT (SELECT prov.NOMBREPROVEEDOR    
           FROM PROVEEDOR prov    
           WHERE prov.IDPROVEEDOR = ippinp.IDPROVEEDOR)    
         FROM SOLICITUD_PAGO ippinp    
         WHERE IDSOLICITUDPAGO = s.IDSOLICITUDPAGO) AS NOMBREPROVEEDOR,    
         (SELECT (SELECT prov.RUTPROVEEDOR    
           FROM PROVEEDOR prov    
           WHERE prov.IDPROVEEDOR = ippinp.IDPROVEEDOR)    
         FROM SOLICITUD_PAGO ippinp    
         WHERE IDSOLICITUDPAGO = s.IDSOLICITUDPAGO) AS RUTPROVEEDOR,    
         (SELECT (SELECT prov.DVPROVEDIGITOVERIFICADORPROVEEDOR    
           FROM PROVEEDOR prov    
           WHERE prov.IDPROVEEDOR = ippinp.IDPROVEEDOR)    
         FROM SOLICITUD_PAGO ippinp    
         WHERE IDSOLICITUDPAGO = s.IDSOLICITUDPAGO) AS DVPROVEDIGITOVERIFICADORPROVEEDOR,    
         d.CODIGOREGIONDIRECCION,    
         ip.CODIGOPROYECTOINFORMACIONPROYECTO,    
         (SELECT NOMBREABREVIADOMAESTROTIPOLOGIA    
         FROM MAESTRO_TIPOLOGIA    
         WHERE IDMAESTROTIPOLOGIA = ce.IDMAESTROTIPOLOGIA) AS nombreMaestroTIpologia,    
         ip.CANTIDADVIVIENDASINFORMACIONPROYECTO,    
         (SELECT STUFF((SELECT ''-''+ CONVERT(varchar(max),    
               (SELECT mes.NOMBREABREVIADOMAESTROSERVICIO    
               FROM MAESTRO_SERVICIO mes    
               WHERE IDMAESTROSERVICIO = tss.IDMAESTROSERVICIO))    
             FROM TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA tsp inner join    
               SERVICIO_PARCIALIDAD spp on tsp.IDSERVICIOPARCIALIDAD = spp.IDSERVICIOPARCIALIDAD inner join    
               TIPOLOGIA_SERVICIO tss on spp.IDTIPOLOGIASERVICIO = tss.IDTIPOLOGIASERVICIO    
             WHERE tsp.IDCARACTERISTICASESPECIALES = ce.IDCARACTERISTICASESPECIALES    
              AND tsp.MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA > 0    
              AND tsp.IDSOLICITUDPAGO > 0    
              AND tsp.IDSOLICITUDPAGO = s.IDSOLICITUDPAGO    
             GROUP BY tss.IDMAESTROSERVICIO    
             ORDER BY tss.IDMAESTROSERVICIO FOR XML PATH('''')),1,1,'''')) AS SERVICIO,   
     (SELECT STUFF((SELECT ''-''+ CONVERT(varchar(max),    
               (SELECT mes.IDMAESTROSERVICIO    
               FROM MAESTRO_SERVICIO mes    
               WHERE IDMAESTROSERVICIO = tss.IDMAESTROSERVICIO))    
             FROM TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA tsp inner join    
               SERVICIO_PARCIALIDAD spp on tsp.IDSERVICIOPARCIALIDAD = spp.IDSERVICIOPARCIALIDAD inner join    
               TIPOLOGIA_SERVICIO tss on spp.IDTIPOLOGIASERVICIO = tss.IDTIPOLOGIASERVICIO    
             WHERE tsp.IDCARACTERISTICASESPECIALES = ce.IDCARACTERISTICASESPECIALES    
              AND tsp.MONTOPARCIALIDADTIPOSERVICIOPARCIALIDADCARACTERISTICA > 0    
              AND tsp.IDSOLICITUDPAGO > 0    
              AND tsp.IDSOLICITUDPAGO = s.IDSOLICITUDPAGO    
             GROUP BY tss.IDMAESTROSERVICIO    
             ORDER BY tss.IDMAESTROSERVICIO FOR XML PATH('''')),1,1,'''')) AS SERVICIO1,    
         (SELECT NOMBREMAESTROESTADOSOLICITUD    
         FROM MAESTRO_ESTADO_SOLICITUD    
         WHERE IDMAESTROESTADOSOLICITUD = s.IDMAESTROESTADOSOLICITUD) AS nombreMaestroEstadoSolicitud,    
         s.MONTOSOLICITUDSOLICITUDPAGO,    
         --CAMBIOS DO    
         ISNULL((SELECT top 1 IDAUTORIZACION        
           FROM TIPO_AUTORIZACION A    
           WHERE A.IDSOLICITUDPAGO = s.IDSOLICITUDPAGO), 0) as numeroAutorizacion,    
         ISNULL((SELECT top 1 NOMBREMAESTROESTADOAUTORIZACION    
           FROM TIPO_AUTORIZACION TA INNER JOIN    
             AUTORIZACION A ON TA.IDAUTORIZACION = A.IDAUTORIZACION INNER JOIN    
             MAESTRO_ESTADO_AUTORIZACION MA ON A.IDMAESTROESTADOAUTORIZACION = MA.IDMAESTROESTADOAUTORIZACION    
           WHERE TA.IDSOLICITUDPAGO = s.IDSOLICITUDPAGO), ''N/A'') AS estadoAutorizacion,    
         (CASE    
          WHEN s.IDMAESTROTIPOPAGO = 1 THEN 1    
         ELSE    
          0    
         END) AS PagoHistorico    
         --CAMBIOS DO    
         FROM CARACTERISTICAS_ESPECIALES ce inner join    
           INFORMACION_PROYECTO ip on ce.IDINFORMACIONPROYECTO = ip.IDINFORMACIONPROYECTO inner join    
           DIRECCION d on ip.IDDIRECCION = d.IDDIRECCION inner join    
           TIPO_SERVICIO_PARCIALIDAD_CARACTERISTICA tspc on ce.IDCARACTERISTICASESPECIALES =  tspc.IDCARACTERISTICASESPECIALES inner join    
           SOLICITUD_PAGO s on  ce.IDCARACTERISTICASESPECIALES = s.IDCARACTERISTICASESPECIALES inner join    
           PROVEEDOR pp on s.IDPROVEEDOR = pp.IDPROVEEDOR inner join    
           INFORMACION_PROYECTO_SOLICITUD ips on s.IDINFORMACIONPROYECTOSOLICITUD = ips.IDINFORMACIONPROYECTOSOLICITUD'    
      
  SET @CONDICION = 'where tspc.IDSOLICITUDPAGO > 0  AND s.MONTOSOLICITUDSOLICITUDPAGO >0  AND s.IDMAESTROESTADOSOLICITUD NOT IN(4) AND' + @CONDICION    
  SET @group = 'group by ce.IDCARACTERISTICASESPECIALES, ce.IDMAESTROPROGRAMA, s.IDSOLICITUDPAGO,d.CODIGOREGIONDIRECCION, ip.CODIGOPROYECTOINFORMACIONPROYECTO,    
  ip.CANTIDADVIVIENDASINFORMACIONPROYECTO , s.IDMAESTROESTADOSOLICITUD, ce.IDMAESTROTIPOLOGIA,s.MONTOSOLICITUDSOLICITUDPAGO, s.IDMAESTROTIPOPAGO, ips.IDMAESTROTIPOLOGIA'    
        
  IF(Len(@CONDICION) > 1)    
  BEGIN        
   SET @CONDICION = SUBSTRING (@CONDICION, 1, Len(@CONDICION) - 3)        
  END        
        
  IF(Len(@CONDICION) > 5)        
  BEGIN        
   SET @final = @CONSULTA1 +' '+ @CONDICION +' '+ @group        
  END        
        
  --select @final        
  DECLARE @SOLICITUDES_DE_PAGO TABLE(        
    IDCARACTERISTICASESPECIALES bigint,        
    IDMAESTROPROGRAMA bigint,        
    IDSOLICITUDPAGO bigint,        
    TIPOPROVEEDOR varchar (max),        
    NOMBREPROVEEDOR varchar(max),        
    RUTPROVEEDOR INT,        
    DV  CHAR,        
    CODIGOREGIONDIRECCION bigint,        
    CODIGOPROYECTOINFORMACIONPROYECTO bigint,        
    NOMBREMAESTROTIPOLOGIA varchar(max),        
    CANTIDADVIVIENDASINFORMACIONPROYECTO int,        
    SERVICIO varchar(max),   
 SERVICIO1 varchar(max),        
    ESTADOSOLICITUD VARCHAR(MAX),        
    MONTOSOLICITUDSOLICITUDPAGO decimal(18,3),        
    NUMEROAUTORIZACION bigint,        
    ESTADOAUTORIZACION varchar(max),        
    PagoHistorico bit)        
        
  INSERT INTO @SOLICITUDES_DE_PAGO        
  EXEC (@final)        
 END        
           
 BEGIN          
  IF(@pNOMBRESERVICIO > 0)        
   SELECT sp.IDCARACTERISTICASESPECIALES,        
     (SELECT mp.NOMBREMAESTROPROGRAMA FROM MAESTRO_PROGRAMA mp WHERE mp.IDMAESTROPROGRAMA = sp.IDMAESTROPROGRAMA) AS NOMBREPROGRAMA,        
     sp.IDSOLICITUDPAGO,        
     TIPOPROVEEDOR,        
     sp.CODIGOREGIONDIRECCION,        
     sp.CODIGOPROYECTOINFORMACIONPROYECTO,        
     sp.NOMBREMAESTROTIPOLOGIA,        
     sp.CANTIDADVIVIENDASINFORMACIONPROYECTO,        
     sp.SERVICIO,        
     sp.NOMBREPROVEEDOR,        
     sp.RUTPROVEEDOR,        
     sp.DV,        
     sp.ESTADOSOLICITUD,        
     sp.MONTOSOLICITUDSOLICITUDPAGO,        
     sp.NUMEROAUTORIZACION,        
     sp.ESTADOAUTORIZACION,        
     sp.PagoHistorico        
   FROM @SOLICITUDES_DE_PAGO sp        
   WHERE sp.SERVICIO1 like '%'+ @pNOMBRESERVICIO +'%'        
     --sp.SERVICIO like '%'+ (SELECT NOMBREABREVIADOMAESTROSERVICIO FROM MAESTRO_SERVICIO WHERE IDMAESTROSERVICIO = CONVERT(bigint,@pNOMBRESERVICIO)) +'%'        
   ORDER BY sp.IDSOLICITUDPAGO DESC        
  ELSE        
   SELECT sp.IDCARACTERISTICASESPECIALES,        
     (SELECT mp.NOMBREMAESTROPROGRAMA FROM MAESTRO_PROGRAMA mp WHERE mp.IDMAESTROPROGRAMA = sp.IDMAESTROPROGRAMA) AS NOMBREPROGRAMA,        
     sp.IDSOLICITUDPAGO,        
     TIPOPROVEEDOR,        
     sp.CODIGOREGIONDIRECCION,        
     sp.CODIGOPROYECTOINFORMACIONPROYECTO,        
     sp.NOMBREMAESTROTIPOLOGIA,        
     sp.CANTIDADVIVIENDASINFORMACIONPROYECTO,        
  sp.SERVICIO,        
     sp.NOMBREPROVEEDOR,        
     sp.RUTPROVEEDOR,        
     sp.DV,        
     sp.ESTADOSOLICITUD,        
     sp.MONTOSOLICITUDSOLICITUDPAGO,        
     sp.NUMEROAUTORIZACION,        
     sp.ESTADOAUTORIZACION,        
     sp.PagoHistorico        
   FROM @SOLICITUDES_DE_PAGO sp        
   ORDER BY sp.IDSOLICITUDPAGO DESC        
 END        
END  
  
GO
/****** Object:  StoredProcedure [dbo].[WEB_ASISTEC_SNATIII_CONSULTA_CONTRATO_PPPF]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/********************************************************************************************          
* Nombre del procedimiento      : WEB_ASISTEC_SNATIII_CONSULTA_CONTRATO_PPPF  
* Fecha de creación             : 18/07/2018          
* Usuario de creación           : Anticipa         
* Versión.                      : V.0.0.0.1          
  
* Visado por DBA                :  Iván Frade Cortés 
* Fecha Aprobación DBA          :  05-09-2018
* Comentario DBA				:  ---
* Parametros     : @ACCION    
          @PPPF_CTO_ID   
          @PPPF_CTO_RUT  
          @PPPF_NOMBRE  
          
* Objetivo                      : Procedimiento que lista los contratos PPPF                            
**********************************************************************************************/       
CREATE PROCEDURE [dbo].[WEB_ASISTEC_SNATIII_CONSULTA_CONTRATO_PPPF]          
@ACCION INT,    
@PPPF_CTO_ID INT=null,    
@PPPF_CTO_RUT VARCHAR(20),  
@PPPF_NOMBRE varchar(15) = null  
  
AS  
DECLARE @VARCERO int, @VARUNO int, @VARDOS int, @VARTRES int  
DECLARE @EXTERNO VARCHAR(15), @MUNI VARCHAR(15), @SERVIU VARCHAR(15)    
DECLARE @CONSULTARUT INT, @CONSULTAID INT, @CONSULTANOMBRE VARCHAR(15), @CONSULTATODOS INT, @CONSULTAPRYCONT INT, @CONSULTAPRYNOM INT, @CONSULTACONTNOM INT  
SET @EXTERNO = 'Externo'    
SET @MUNI = 'Municipalidad'    
SET @SERVIU = 'SERVIU'    
SET @VARCERO = 0          
SET @VARUNO = 1 --MOSTRAR CONTRATOS X REGION      
SET @VARDOS = 2       
SET @VARTRES = 3     
SET @CONSULTARUT = 4  
SET @CONSULTAID = 5  
SET @CONSULTANOMBRE = 6  
SET @CONSULTATODOS = 7  
SET @CONSULTAPRYCONT = 8  
SET @CONSULTAPRYNOM = 9  
SET @CONSULTACONTNOM = 10  
  
  
IF @ACCION = @CONSULTARUT    
BEGIN  
 SELECT  
   CO.PPPF_CTO_ID,   
   CO.PPPF_CTO_NOM AS PPPF_CTO_NOM,           
   PSAT.PPPF_PSAT_NOM,  
   (SELECT COUNT(@VARUNO) FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO WHERE PPPF_CTO_ID = CP.PPPF_CTO_ID) as PPPF_CANT_PROYECTOS,  
   CASE WHEN CO.PPPF_CTO_TIP = @VARUNO THEN @EXTERNO WHEN CO.PPPF_CTO_TIP = @VARDOS THEN @MUNI  ELSE @SERVIU END AS PPPF_CTO_TIP  
  FROM         
   WEB_ASISTEC_SNATIII_PPPF_CONTRATO CO INNER JOIN  
   WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP ON CP.PPPF_CTO_ID = CO.PPPF_CTO_ID INNER JOIN           
   WEB_ASISTEC_SNATIII_PPPF_PROYECTO P ON P.PPPF_PRY_ID = CP.PPPF_PRY_ID INNER JOIN    
   WEB_ASISTEC_SNATIII_PPPF_PRESTADOR_ASISTENCIA_TECNICA PSAT ON P.PPPF_PSAT_RUT = PSAT.PPPF_PSAT_RUT    
 WHERE P.PPPF_PRY_COD = @PPPF_CTO_RUT  
 ORDER BY CO.PPPF_CTO_ID DESC  
END  
  
IF @ACCION = @CONSULTAID  
BEGIN  
  SELECT TOP 1  
   CO.PPPF_CTO_ID,   
   CO.PPPF_CTO_NOM AS PPPF_CTO_NOM,           
   PSAT.PPPF_PSAT_NOM,  
   (SELECT COUNT(@VARUNO) FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO WHERE PPPF_CTO_ID = CP.PPPF_CTO_ID) as PPPF_CANT_PROYECTOS,  
   CASE WHEN CO.PPPF_CTO_TIP = @VARUNO THEN @EXTERNO WHEN CO.PPPF_CTO_TIP = @VARDOS THEN @MUNI  ELSE @SERVIU END AS PPPF_CTO_TIP  
  FROM         
   WEB_ASISTEC_SNATIII_PPPF_CONTRATO CO INNER JOIN  
   WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP ON CP.PPPF_CTO_ID = CO.PPPF_CTO_ID INNER JOIN           
   WEB_ASISTEC_SNATIII_PPPF_PROYECTO P ON P.PPPF_PRY_ID = CP.PPPF_PRY_ID INNER JOIN    
   WEB_ASISTEC_SNATIII_PPPF_PRESTADOR_ASISTENCIA_TECNICA PSAT ON P.PPPF_PSAT_RUT = PSAT.PPPF_PSAT_RUT    
 where co.PPPF_CTO_ID = @PPPF_CTO_ID  
 ORDER BY CO.PPPF_CTO_ID DESC  
  
END  
  
IF @ACCION = @CONSULTANOMBRE  
BEGIN  
  SELECT   
   CO.PPPF_CTO_ID,   
   CO.PPPF_CTO_NOM AS PPPF_CTO_NOM,           
   PSAT.PPPF_PSAT_NOM,  
   (SELECT COUNT(@VARUNO) FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO WHERE PPPF_CTO_ID = CP.PPPF_CTO_ID) as PPPF_CANT_PROYECTOS,  
   CASE WHEN CO.PPPF_CTO_TIP = @VARUNO THEN @EXTERNO WHEN CO.PPPF_CTO_TIP = @VARDOS THEN @MUNI  ELSE @SERVIU END AS PPPF_CTO_TIP  
  FROM         
   WEB_ASISTEC_SNATIII_PPPF_CONTRATO CO INNER JOIN  
   WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP ON CP.PPPF_CTO_ID = CO.PPPF_CTO_ID INNER JOIN           
   WEB_ASISTEC_SNATIII_PPPF_PROYECTO P ON P.PPPF_PRY_ID = CP.PPPF_PRY_ID INNER JOIN    
   WEB_ASISTEC_SNATIII_PPPF_PRESTADOR_ASISTENCIA_TECNICA PSAT ON P.PPPF_PSAT_RUT = PSAT.PPPF_PSAT_RUT  
 WHERE PSAT.PPPF_PSAT_NOM LIKE '%'+ @PPPF_NOMBRE +'%'  
 ORDER BY CO.PPPF_CTO_ID DESC  
END  
  
IF @ACCION = @CONSULTATODOS  
BEGIN  
  SELECT   
   CO.PPPF_CTO_ID,   
   CO.PPPF_CTO_NOM AS PPPF_CTO_NOM,           
   PSAT.PPPF_PSAT_NOM,  
   (SELECT COUNT(@VARUNO) FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO WHERE PPPF_CTO_ID = CP.PPPF_CTO_ID) as PPPF_CANT_PROYECTOS,  
   CASE WHEN CO.PPPF_CTO_TIP = @VARUNO THEN @EXTERNO WHEN CO.PPPF_CTO_TIP = @VARDOS THEN @MUNI  ELSE @SERVIU END AS PPPF_CTO_TIP  
  FROM         
   WEB_ASISTEC_SNATIII_PPPF_CONTRATO CO INNER JOIN  
   WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP ON CP.PPPF_CTO_ID = CO.PPPF_CTO_ID INNER JOIN           
   WEB_ASISTEC_SNATIII_PPPF_PROYECTO P ON P.PPPF_PRY_ID = CP.PPPF_PRY_ID INNER JOIN    
   WEB_ASISTEC_SNATIII_PPPF_PRESTADOR_ASISTENCIA_TECNICA PSAT ON P.PPPF_PSAT_RUT = PSAT.PPPF_PSAT_RUT  
 WHERE P.PPPF_PRY_COD = @PPPF_CTO_RUT AND   
  CO.PPPF_CTO_ID = @PPPF_CTO_ID AND  
  PSAT.PPPF_PSAT_NOM LIKE '%'+ @PPPF_NOMBRE +'%'   
 ORDER BY CO.PPPF_CTO_ID DESC  
END  
  
IF @ACCION = @CONSULTAPRYCONT  
BEGIN  
  SELECT   
   CO.PPPF_CTO_ID,   
   CO.PPPF_CTO_NOM AS PPPF_CTO_NOM,           
   PSAT.PPPF_PSAT_NOM,  
   (SELECT COUNT(@VARUNO) FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO WHERE PPPF_CTO_ID = CP.PPPF_CTO_ID) as PPPF_CANT_PROYECTOS,  
   CASE WHEN CO.PPPF_CTO_TIP = @VARUNO THEN @EXTERNO WHEN CO.PPPF_CTO_TIP = @VARDOS THEN @MUNI  ELSE @SERVIU END AS PPPF_CTO_TIP  
  FROM         
   WEB_ASISTEC_SNATIII_PPPF_CONTRATO CO INNER JOIN  
   WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP ON CP.PPPF_CTO_ID = CO.PPPF_CTO_ID INNER JOIN           
   WEB_ASISTEC_SNATIII_PPPF_PROYECTO P ON P.PPPF_PRY_ID = CP.PPPF_PRY_ID INNER JOIN    
   WEB_ASISTEC_SNATIII_PPPF_PRESTADOR_ASISTENCIA_TECNICA PSAT ON P.PPPF_PSAT_RUT = PSAT.PPPF_PSAT_RUT  
 WHERE P.PPPF_PRY_COD = @PPPF_CTO_RUT AND   
  CO.PPPF_CTO_ID = @PPPF_CTO_ID   
 ORDER BY CO.PPPF_CTO_ID DESC  
END  
  
IF @ACCION = @CONSULTAPRYNOM  
BEGIN  
  SELECT   
   CO.PPPF_CTO_ID,   
   CO.PPPF_CTO_NOM AS PPPF_CTO_NOM,           
   PSAT.PPPF_PSAT_NOM,  
   (SELECT COUNT(@VARUNO) FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO WHERE PPPF_CTO_ID = CP.PPPF_CTO_ID) as PPPF_CANT_PROYECTOS,  
   CASE WHEN CO.PPPF_CTO_TIP = @VARUNO THEN @EXTERNO WHEN CO.PPPF_CTO_TIP = @VARDOS THEN @MUNI  ELSE @SERVIU END AS PPPF_CTO_TIP  
  FROM         
   WEB_ASISTEC_SNATIII_PPPF_CONTRATO CO INNER JOIN  
   WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP ON CP.PPPF_CTO_ID = CO.PPPF_CTO_ID INNER JOIN           
   WEB_ASISTEC_SNATIII_PPPF_PROYECTO P ON P.PPPF_PRY_ID = CP.PPPF_PRY_ID INNER JOIN    
   WEB_ASISTEC_SNATIII_PPPF_PRESTADOR_ASISTENCIA_TECNICA PSAT ON P.PPPF_PSAT_RUT = PSAT.PPPF_PSAT_RUT  
 WHERE P.PPPF_PRY_COD = @PPPF_CTO_RUT AND  
  PSAT.PPPF_PSAT_NOM LIKE '%'+ @PPPF_NOMBRE +'%'   
 ORDER BY CO.PPPF_CTO_ID DESC  
END  
  
IF @ACCION = @CONSULTACONTNOM  
BEGIN  
  SELECT   
   CO.PPPF_CTO_ID,   
   CO.PPPF_CTO_NOM AS PPPF_CTO_NOM,           
   PSAT.PPPF_PSAT_NOM,  
   (SELECT COUNT(@VARUNO) FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO WHERE PPPF_CTO_ID = CP.PPPF_CTO_ID) as PPPF_CANT_PROYECTOS,  
   CASE WHEN CO.PPPF_CTO_TIP = @VARUNO THEN @EXTERNO WHEN CO.PPPF_CTO_TIP = @VARDOS THEN @MUNI  ELSE @SERVIU END AS PPPF_CTO_TIP  
  FROM         
   WEB_ASISTEC_SNATIII_PPPF_CONTRATO CO INNER JOIN  
   WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP ON CP.PPPF_CTO_ID = CO.PPPF_CTO_ID INNER JOIN           
   WEB_ASISTEC_SNATIII_PPPF_PROYECTO P ON P.PPPF_PRY_ID = CP.PPPF_PRY_ID INNER JOIN    
   WEB_ASISTEC_SNATIII_PPPF_PRESTADOR_ASISTENCIA_TECNICA PSAT ON P.PPPF_PSAT_RUT = PSAT.PPPF_PSAT_RUT  
 WHERE CO.PPPF_CTO_ID = @PPPF_CTO_ID AND  
  PSAT.PPPF_PSAT_NOM LIKE '%'+ @PPPF_NOMBRE +'%'   
 ORDER BY CO.PPPF_CTO_ID DESC  
END

GO
/****** Object:  StoredProcedure [dbo].[WEB_ASISTEC_SNATIII_CONSULTA_PROYECTO_PPPF]    Script Date: 02-10-2019 12:16:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/********************************************************************************************        
* Nombre del procedimiento      : WEB_ASISTEC_SNATIII_CONSULTA_PROYECTO_PPPF        
* Fecha de creación             : 18/07/2018        
* Usuario de creación           : Anticipa       
* Versión.                      : V.0.0.0.1        

* Visado por DBA                : 
* Fecha Aprobación DBA          : 

* Parametros					: @ACCION							  
								  @PPPF_CTO_ID	
								  @PPPF_CTO_RUT
								  @PPPF_NOMBRE
        
* Objetivo                      : Procedimiento que lista los proyectos PPPF                          
**********************************************************************************************/     
CREATE PROCEDURE [dbo].[WEB_ASISTEC_SNATIII_CONSULTA_PROYECTO_PPPF]        
@ACCION INT,  
@PPPF_CTO_ID INT=null,  
@PPPF_CTO_RUT VARCHAR(15)=null,
@PPPF_NOMBRE varchar(15) = null

AS
DECLARE @VARCERO int, @VARUNO int, @VARDOS int, @VARTRES int
DECLARE @EXTERNO VARCHAR(15), @MUNI VARCHAR(15), @SERVIU VARCHAR(15)  
DECLARE @CONSULTARUT INT, @CONSULTAID INT, @CONSULTANOMBRE VARCHAR(15)
DECLARE @NOASIGNADO VARCHAR(50), @CONSULTATODOS INT, @CONSULTAPROYCONT INT, @CONSULTAPROYNOM INT, @CONSULTACONTNOM INT
SET @EXTERNO = 'Externo'  
SET @MUNI = 'Municipalidad'  
SET @SERVIU = 'SERVIU'  
SET @VARCERO = 0        
SET @VARUNO = 1 --MOSTRAR CONTRATOS X REGION    
SET @VARDOS = 2     
SET @VARTRES = 3   
SET @CONSULTARUT = 4
SET @CONSULTAID = 5
SET @CONSULTANOMBRE = 6
SET @CONSULTATODOS = 7
SET @CONSULTAPROYCONT = 8
SET @CONSULTAPROYNOM = 9
SET @CONSULTACONTNOM = 10

SET @NOASIGNADO = 'Sin Asignar' 

IF @ACCION = @CONSULTARUT  
BEGIN
 SELECT      
  P.PPPF_PRY_ID,    
  CP.PPPF_CTO_ID, 
  PPPF_PRY_COD,        
  CASE WHEN  CONVERT(VARCHAR,PPPF_PRY_CER) = '0' THEN 'NO APLICA' ELSE CONVERT(VARCHAR,PPPF_PRY_CER) END PPPF_PRY_CER,        
  PPPF_PRY_NOM,         
  PPPF_PRY_TIT,        
  ISNULL((SELECT top 1 PR.PROF_NOM + ' ' + PR.PROF_APP_PAT + ' ' + PR.PROF_APP_MAT FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES PR ON PR.PROF_RUT = CP.PPPF_CTO_RUT_PROF AND CP.PPPF_PRY_ID = P.PPPF_PRY_ID AND PR.PROF_ITO = 1),@NOASIGNADO) as NOMBREITO ,        
  ISNULL((SELECT top 1 CONVERT(VARCHAR,PR.PROF_RUT) + '-' + PR.PROF_DGV FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES PR ON PR.PROF_RUT = CP.PPPF_CTO_RUT_PROF AND CP.PPPF_PRY_ID = P.PPPF_PRY_ID AND PR.PROF_ITO = 1),@NOASIGNADO) as RUTITO ,        
  ISNULL((SELECT top 1 PR.PROF_NOM + ' ' + PR.PROF_APP_PAT + ' ' + PR.PROF_APP_MAT FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES PR ON PR.PROF_RUT = CP.PPPF_CTO_RUT_PROF_RMPLZ AND CP.PPPF_PRY_ID = P.PPPF_PRY_ID AND PR.PROF_ITO = 1),@NOASIGNADO) as NOMBREITOR,
  ISNULL((SELECT top 1 CONVERT(VARCHAR,PR.PROF_RUT) + '-' + PR.PROF_DGV FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES PR ON PR.PROF_RUT = CP.PPPF_CTO_RUT_PROF_RMPLZ AND CP.PPPF_PRY_ID = P.PPPF_PRY_ID AND PR.PROF_ITO = 1),@NOASIGNADO) as RUTITOR,
  SN.PPPF_PSAT_NOM,
  ISNULL((SELECT top 1 PR.PROF_SERVIU_NOM + ' ' + PR.PROF_SERVIU_APP_PAT + ' ' + PR.PROF_SERVIU_APP_MAT FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES_SERVIU PR ON PR.PROF_SERVIU_RUT = CP.PPPF_CTO_RUT_SPV WHERE CP.PPPF_PRY_ID = P.PPPF_PRY_ID),@NOASIGNADO) as NOMBRESPV
 FROM       
  WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO  CP INNER JOIN         
  WEB_ASISTEC_SNATIII_PPPF_PROYECTO P ON P.PPPF_PRY_ID = CP.PPPF_PRY_ID INNER JOIN
  WEB_ASISTEC_SNATIII_PPPF_PRESTADOR_ASISTENCIA_TECNICA SN ON SN.PPPF_PSAT_RUT = P.PPPF_PSAT_RUT 
  WHERE PPPF_PRY_COD = @PPPF_CTO_RUT
  ORDER BY PPPF_PRY_COD
END

IF @ACCION = @CONSULTAID
BEGIN
 SELECT          
  P.PPPF_PRY_ID,        
  CP.PPPF_CTO_ID,        
  PPPF_PRY_COD,        
  CASE WHEN  CONVERT(VARCHAR,PPPF_PRY_CER) = '0' THEN 'NO APLICA' ELSE CONVERT(VARCHAR,PPPF_PRY_CER) END PPPF_PRY_CER,        
  PPPF_PRY_NOM,        
  PPPF_PRY_QTY_FAM,        
  REG_COD,        
  PRV_COD,        
  COM_COD,      
  PPPF_PRY_TIT,        
  ISNULL((SELECT top 1 PR.PROF_NOM + ' ' + PR.PROF_APP_PAT + ' ' + PR.PROF_APP_MAT FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES PR ON PR.PROF_RUT = CP.PPPF_CTO_RUT_PROF AND CP.PPPF_PRY_ID = P.PPPF_PRY_ID AND PR.PROF_ITO = 1),@NOASIGNADO) as NOMBREITO ,        
  ISNULL((SELECT top 1 CONVERT(VARCHAR,PR.PROF_RUT) + '-' + PR.PROF_DGV FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES PR ON PR.PROF_RUT = CP.PPPF_CTO_RUT_PROF AND CP.PPPF_PRY_ID = P.PPPF_PRY_ID AND PR.PROF_ITO = 1),@NOASIGNADO) as RUTITO ,        
  ISNULL((SELECT top 1 PR.PROF_NOM + ' ' + PR.PROF_APP_PAT + ' ' + PR.PROF_APP_MAT FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES PR ON PR.PROF_RUT = CP.PPPF_CTO_RUT_PROF_RMPLZ AND CP.PPPF_PRY_ID = P.PPPF_PRY_ID AND PR.PROF_ITO = 1),@NOASIGNADO) as NOMBREITOR,
  ISNULL((SELECT top 1 CONVERT(VARCHAR,PR.PROF_RUT) + '-' + PR.PROF_DGV FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES PR ON PR.PROF_RUT = CP.PPPF_CTO_RUT_PROF_RMPLZ AND CP.PPPF_PRY_ID = P.PPPF_PRY_ID AND PR.PROF_ITO = 1),@NOASIGNADO) as RUTITOR,
  SN.PPPF_PSAT_NOM,
  ISNULL((SELECT top 1 PR.PROF_SERVIU_NOM + ' ' + PR.PROF_SERVIU_APP_PAT + ' ' + PR.PROF_SERVIU_APP_MAT FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES_SERVIU PR ON PR.PROF_SERVIU_RUT = CP.PPPF_CTO_RUT_SPV WHERE CP.PPPF_PRY_ID = P.PPPF_PRY_ID),@NOASIGNADO) as NOMBRESPV
 FROM       
  WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO  CP INNER JOIN         
  WEB_ASISTEC_SNATIII_PPPF_PROYECTO P ON P.PPPF_PRY_ID = CP.PPPF_PRY_ID INNER JOIN      
  WEB_ASISTEC_SNATIII_PPPF_PRESTADOR_ASISTENCIA_TECNICA SN ON SN.PPPF_PSAT_RUT = P.PPPF_PSAT_RUT 
  WHERE PPPF_CTO_ID = @PPPF_CTO_ID
  ORDER BY PPPF_PRY_COD
END

IF @ACCION = @CONSULTANOMBRE
BEGIN
 SELECT
  P.PPPF_PRY_ID,        
  CP.PPPF_CTO_ID,        
  PPPF_PRY_COD,        
  CASE WHEN  CONVERT(VARCHAR,PPPF_PRY_CER) = '0' THEN 'NO APLICA' ELSE CONVERT(VARCHAR,PPPF_PRY_CER) END PPPF_PRY_CER,        
  PPPF_PRY_NOM,        
  PPPF_PRY_QTY_FAM,        
  REG_COD,        
  PRV_COD,        
  COM_COD,      
  PPPF_PRY_TIT,        
  ISNULL((SELECT top 1 PR.PROF_NOM + ' ' + PR.PROF_APP_PAT + ' ' + PR.PROF_APP_MAT FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES PR ON PR.PROF_RUT = CP.PPPF_CTO_RUT_PROF AND CP.PPPF_PRY_ID = P.PPPF_PRY_ID AND PR.PROF_ITO = 1),@NOASIGNADO) as NOMBREITO ,        
  ISNULL((SELECT top 1 CONVERT(VARCHAR,PR.PROF_RUT) + '-' + PR.PROF_DGV FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES PR ON PR.PROF_RUT = CP.PPPF_CTO_RUT_PROF AND CP.PPPF_PRY_ID = P.PPPF_PRY_ID AND PR.PROF_ITO = 1),@NOASIGNADO) as RUTITO ,        
  ISNULL((SELECT top 1 PR.PROF_NOM + ' ' + PR.PROF_APP_PAT + ' ' + PR.PROF_APP_MAT FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES PR ON PR.PROF_RUT = CP.PPPF_CTO_RUT_PROF_RMPLZ AND CP.PPPF_PRY_ID = P.PPPF_PRY_ID AND PR.PROF_ITO = 1),@NOASIGNADO) as NOMBREITOR,
  ISNULL((SELECT top 1 CONVERT(VARCHAR,PR.PROF_RUT) + '-' + PR.PROF_DGV FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES PR ON PR.PROF_RUT = CP.PPPF_CTO_RUT_PROF_RMPLZ AND CP.PPPF_PRY_ID = P.PPPF_PRY_ID AND PR.PROF_ITO = 1),@NOASIGNADO) as RUTITOR,
  SN.PPPF_PSAT_NOM,
  ISNULL((SELECT top 1 PR.PROF_SERVIU_NOM + ' ' + PR.PROF_SERVIU_APP_PAT + ' ' + PR.PROF_SERVIU_APP_MAT FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES_SERVIU PR ON PR.PROF_SERVIU_RUT = CP.PPPF_CTO_RUT_SPV WHERE CP.PPPF_PRY_ID = P.PPPF_PRY_ID),@NOASIGNADO) as NOMBRESPV
 FROM       
  WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO  CP INNER JOIN         
  WEB_ASISTEC_SNATIII_PPPF_PROYECTO P ON P.PPPF_PRY_ID = CP.PPPF_PRY_ID INNER JOIN        
  WEB_ASISTEC_SNATIII_PPPF_PRESTADOR_ASISTENCIA_TECNICA SN ON SN.PPPF_PSAT_RUT = P.PPPF_PSAT_RUT 
  WHERE SN.PPPF_PSAT_NOM LIKE '%'+ @PPPF_NOMBRE +'%'
  ORDER BY PPPF_PRY_NOM DESC
END

IF @ACCION = @CONSULTATODOS
BEGIN
 SELECT
  P.PPPF_PRY_ID,        
  CP.PPPF_CTO_ID,        
  PPPF_PRY_COD,        
  CASE WHEN  CONVERT(VARCHAR,PPPF_PRY_CER) = '0' THEN 'NO APLICA' ELSE CONVERT(VARCHAR,PPPF_PRY_CER) END PPPF_PRY_CER,        
  PPPF_PRY_NOM,        
  PPPF_PRY_QTY_FAM,        
  REG_COD,        
  PRV_COD,        
  COM_COD,      
  PPPF_PRY_TIT,        
  ISNULL((SELECT top 1 PR.PROF_NOM + ' ' + PR.PROF_APP_PAT + ' ' + PR.PROF_APP_MAT FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES PR ON PR.PROF_RUT = CP.PPPF_CTO_RUT_PROF AND CP.PPPF_PRY_ID = P.PPPF_PRY_ID AND PR.PROF_ITO = 1),@NOASIGNADO) as NOMBREITO ,        
  ISNULL((SELECT top 1 CONVERT(VARCHAR,PR.PROF_RUT) + '-' + PR.PROF_DGV FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES PR ON PR.PROF_RUT = CP.PPPF_CTO_RUT_PROF AND CP.PPPF_PRY_ID = P.PPPF_PRY_ID AND PR.PROF_ITO = 1),@NOASIGNADO) as RUTITO ,        
  ISNULL((SELECT top 1 PR.PROF_NOM + ' ' + PR.PROF_APP_PAT + ' ' + PR.PROF_APP_MAT FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES PR ON PR.PROF_RUT = CP.PPPF_CTO_RUT_PROF_RMPLZ AND CP.PPPF_PRY_ID = P.PPPF_PRY_ID AND PR.PROF_ITO = 1),@NOASIGNADO) as NOMBREITOR,
  ISNULL((SELECT top 1 CONVERT(VARCHAR,PR.PROF_RUT) + '-' + PR.PROF_DGV FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES PR ON PR.PROF_RUT = CP.PPPF_CTO_RUT_PROF_RMPLZ AND CP.PPPF_PRY_ID = P.PPPF_PRY_ID AND PR.PROF_ITO = 1),@NOASIGNADO) as RUTITOR,
  SN.PPPF_PSAT_NOM,
  ISNULL((SELECT top 1 PR.PROF_SERVIU_NOM + ' ' + PR.PROF_SERVIU_APP_PAT + ' ' + PR.PROF_SERVIU_APP_MAT FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES_SERVIU PR ON PR.PROF_SERVIU_RUT = CP.PPPF_CTO_RUT_SPV WHERE CP.PPPF_PRY_ID = P.PPPF_PRY_ID),@NOASIGNADO) as NOMBRESPV
 FROM       
  WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO  CP INNER JOIN         
  WEB_ASISTEC_SNATIII_PPPF_PROYECTO P ON P.PPPF_PRY_ID = CP.PPPF_PRY_ID INNER JOIN        
  WEB_ASISTEC_SNATIII_PPPF_PRESTADOR_ASISTENCIA_TECNICA SN ON SN.PPPF_PSAT_RUT = P.PPPF_PSAT_RUT 
  WHERE PPPF_PRY_COD = @PPPF_CTO_RUT AND
  PPPF_CTO_ID = @PPPF_CTO_ID AND
  SN.PPPF_PSAT_NOM LIKE '%'+ @PPPF_NOMBRE +'%'
  ORDER BY PPPF_PRY_NOM DESC
END

IF @ACCION = @CONSULTAPROYCONT
BEGIN
 SELECT
  P.PPPF_PRY_ID,        
  CP.PPPF_CTO_ID,        
  PPPF_PRY_COD,        
  CASE WHEN  CONVERT(VARCHAR,PPPF_PRY_CER) = '0' THEN 'NO APLICA' ELSE CONVERT(VARCHAR,PPPF_PRY_CER) END PPPF_PRY_CER,        
  PPPF_PRY_NOM,        
  PPPF_PRY_QTY_FAM,        
  REG_COD,        
  PRV_COD,        
  COM_COD,      
  PPPF_PRY_TIT,        
  ISNULL((SELECT top 1 PR.PROF_NOM + ' ' + PR.PROF_APP_PAT + ' ' + PR.PROF_APP_MAT FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES PR ON PR.PROF_RUT = CP.PPPF_CTO_RUT_PROF AND CP.PPPF_PRY_ID = P.PPPF_PRY_ID AND PR.PROF_ITO = 1),@NOASIGNADO) as NOMBREITO ,        
  ISNULL((SELECT top 1 CONVERT(VARCHAR,PR.PROF_RUT) + '-' + PR.PROF_DGV FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES PR ON PR.PROF_RUT = CP.PPPF_CTO_RUT_PROF AND CP.PPPF_PRY_ID = P.PPPF_PRY_ID AND PR.PROF_ITO = 1),@NOASIGNADO) as RUTITO ,        
  ISNULL((SELECT top 1 PR.PROF_NOM + ' ' + PR.PROF_APP_PAT + ' ' + PR.PROF_APP_MAT FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES PR ON PR.PROF_RUT = CP.PPPF_CTO_RUT_PROF_RMPLZ AND CP.PPPF_PRY_ID = P.PPPF_PRY_ID AND PR.PROF_ITO = 1),@NOASIGNADO) as NOMBREITOR,
  ISNULL((SELECT top 1 CONVERT(VARCHAR,PR.PROF_RUT) + '-' + PR.PROF_DGV FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES PR ON PR.PROF_RUT = CP.PPPF_CTO_RUT_PROF_RMPLZ AND CP.PPPF_PRY_ID = P.PPPF_PRY_ID AND PR.PROF_ITO = 1),@NOASIGNADO) as RUTITOR,
  SN.PPPF_PSAT_NOM,
  ISNULL((SELECT top 1 PR.PROF_SERVIU_NOM + ' ' + PR.PROF_SERVIU_APP_PAT + ' ' + PR.PROF_SERVIU_APP_MAT FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES_SERVIU PR ON PR.PROF_SERVIU_RUT = CP.PPPF_CTO_RUT_SPV WHERE CP.PPPF_PRY_ID = P.PPPF_PRY_ID),@NOASIGNADO) as NOMBRESPV
 FROM       
  WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO  CP INNER JOIN         
  WEB_ASISTEC_SNATIII_PPPF_PROYECTO P ON P.PPPF_PRY_ID = CP.PPPF_PRY_ID INNER JOIN        
  WEB_ASISTEC_SNATIII_PPPF_PRESTADOR_ASISTENCIA_TECNICA SN ON SN.PPPF_PSAT_RUT = P.PPPF_PSAT_RUT 
  WHERE PPPF_PRY_COD = @PPPF_CTO_RUT AND
  PPPF_CTO_ID = @PPPF_CTO_ID
  ORDER BY PPPF_PRY_NOM DESC
END

IF @ACCION = @CONSULTAPROYNOM
BEGIN
 SELECT
  P.PPPF_PRY_ID,        
  CP.PPPF_CTO_ID,        
  PPPF_PRY_COD,        
  CASE WHEN  CONVERT(VARCHAR,PPPF_PRY_CER) = '0' THEN 'NO APLICA' ELSE CONVERT(VARCHAR,PPPF_PRY_CER) END PPPF_PRY_CER,        
  PPPF_PRY_NOM,        
  PPPF_PRY_QTY_FAM,        
  REG_COD,        
  PRV_COD,        
  COM_COD,      
  PPPF_PRY_TIT,        
  ISNULL((SELECT top 1 PR.PROF_NOM + ' ' + PR.PROF_APP_PAT + ' ' + PR.PROF_APP_MAT FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES PR ON PR.PROF_RUT = CP.PPPF_CTO_RUT_PROF AND CP.PPPF_PRY_ID = P.PPPF_PRY_ID AND PR.PROF_ITO = 1),@NOASIGNADO) as NOMBREITO ,        
  ISNULL((SELECT top 1 CONVERT(VARCHAR,PR.PROF_RUT) + '-' + PR.PROF_DGV FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES PR ON PR.PROF_RUT = CP.PPPF_CTO_RUT_PROF AND CP.PPPF_PRY_ID = P.PPPF_PRY_ID AND PR.PROF_ITO = 1),@NOASIGNADO) as RUTITO ,        
  ISNULL((SELECT top 1 PR.PROF_NOM + ' ' + PR.PROF_APP_PAT + ' ' + PR.PROF_APP_MAT FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES PR ON PR.PROF_RUT = CP.PPPF_CTO_RUT_PROF_RMPLZ AND CP.PPPF_PRY_ID = P.PPPF_PRY_ID AND PR.PROF_ITO = 1),@NOASIGNADO) as NOMBREITOR,
  ISNULL((SELECT top 1 CONVERT(VARCHAR,PR.PROF_RUT) + '-' + PR.PROF_DGV FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES PR ON PR.PROF_RUT = CP.PPPF_CTO_RUT_PROF_RMPLZ AND CP.PPPF_PRY_ID = P.PPPF_PRY_ID AND PR.PROF_ITO = 1),@NOASIGNADO) as RUTITOR,
  SN.PPPF_PSAT_NOM,
  ISNULL((SELECT top 1 PR.PROF_SERVIU_NOM + ' ' + PR.PROF_SERVIU_APP_PAT + ' ' + PR.PROF_SERVIU_APP_MAT FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES_SERVIU PR ON PR.PROF_SERVIU_RUT = CP.PPPF_CTO_RUT_SPV WHERE CP.PPPF_PRY_ID = P.PPPF_PRY_ID),@NOASIGNADO) as NOMBRESPV
 FROM       
  WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO  CP INNER JOIN         
  WEB_ASISTEC_SNATIII_PPPF_PROYECTO P ON P.PPPF_PRY_ID = CP.PPPF_PRY_ID INNER JOIN        
  WEB_ASISTEC_SNATIII_PPPF_PRESTADOR_ASISTENCIA_TECNICA SN ON SN.PPPF_PSAT_RUT = P.PPPF_PSAT_RUT 
  WHERE PPPF_PRY_COD = @PPPF_CTO_RUT AND
  SN.PPPF_PSAT_NOM LIKE '%'+ @PPPF_NOMBRE +'%'
  ORDER BY PPPF_PRY_NOM DESC
END

IF @ACCION = @CONSULTACONTNOM
BEGIN
 SELECT
  P.PPPF_PRY_ID,        
  CP.PPPF_CTO_ID,        
  PPPF_PRY_COD,        
  CASE WHEN  CONVERT(VARCHAR,PPPF_PRY_CER) = '0' THEN 'NO APLICA' ELSE CONVERT(VARCHAR,PPPF_PRY_CER) END PPPF_PRY_CER,        
  PPPF_PRY_NOM,        
  PPPF_PRY_QTY_FAM,        
  REG_COD,        
  PRV_COD,        
  COM_COD,      
  PPPF_PRY_TIT,        
  ISNULL((SELECT top 1 PR.PROF_NOM + ' ' + PR.PROF_APP_PAT + ' ' + PR.PROF_APP_MAT FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES PR ON PR.PROF_RUT = CP.PPPF_CTO_RUT_PROF AND CP.PPPF_PRY_ID = P.PPPF_PRY_ID AND PR.PROF_ITO = 1),@NOASIGNADO) as NOMBREITO ,        
  ISNULL((SELECT top 1 CONVERT(VARCHAR,PR.PROF_RUT) + '-' + PR.PROF_DGV FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES PR ON PR.PROF_RUT = CP.PPPF_CTO_RUT_PROF AND CP.PPPF_PRY_ID = P.PPPF_PRY_ID AND PR.PROF_ITO = 1),@NOASIGNADO) as RUTITO ,        
  ISNULL((SELECT top 1 PR.PROF_NOM + ' ' + PR.PROF_APP_PAT + ' ' + PR.PROF_APP_MAT FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES PR ON PR.PROF_RUT = CP.PPPF_CTO_RUT_PROF_RMPLZ AND CP.PPPF_PRY_ID = P.PPPF_PRY_ID AND PR.PROF_ITO = 1),@NOASIGNADO) as NOMBREITOR,
  ISNULL((SELECT top 1 CONVERT(VARCHAR,PR.PROF_RUT) + '-' + PR.PROF_DGV FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES PR ON PR.PROF_RUT = CP.PPPF_CTO_RUT_PROF_RMPLZ AND CP.PPPF_PRY_ID = P.PPPF_PRY_ID AND PR.PROF_ITO = 1),@NOASIGNADO) as RUTITOR,
  SN.PPPF_PSAT_NOM,
  ISNULL((SELECT top 1 PR.PROF_SERVIU_NOM + ' ' + PR.PROF_SERVIU_APP_PAT + ' ' + PR.PROF_SERVIU_APP_MAT FROM WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO CP INNER JOIN WEB_ASISTEC_SNATIII_PROFESIONALES_SERVIU PR ON PR.PROF_SERVIU_RUT = CP.PPPF_CTO_RUT_SPV WHERE CP.PPPF_PRY_ID = P.PPPF_PRY_ID),@NOASIGNADO) as NOMBRESPV
 FROM       
  WEB_ASISTEC_SNATIII_PPPF_CONTRATO_PROYECTO  CP INNER JOIN         
  WEB_ASISTEC_SNATIII_PPPF_PROYECTO P ON P.PPPF_PRY_ID = CP.PPPF_PRY_ID INNER JOIN        
  WEB_ASISTEC_SNATIII_PPPF_PRESTADOR_ASISTENCIA_TECNICA SN ON SN.PPPF_PSAT_RUT = P.PPPF_PSAT_RUT 
  WHERE PPPF_CTO_ID = @PPPF_CTO_ID AND
  SN.PPPF_PSAT_NOM LIKE '%'+ @PPPF_NOMBRE +'%'
  ORDER BY PPPF_PRY_NOM DESC
END

GO
