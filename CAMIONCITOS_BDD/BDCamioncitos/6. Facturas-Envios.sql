-- Tabla Factura
create table PEDIDO
(
  ID int identity(1,1) primary key,
  FECHA DATE not null,
  RUC_CLIENTE VARCHAR(13) not null,
  DETALLES VARCHAR(50) not null,
  PESO FLOAT not null,
  ENVIO_INTRAPROVINCIAL BIT not null,
  COSTO FLOAT not null,
  CONSTRAINT fk_RUC_FAC FOREIGN KEY (RUC_CLIENTE) REFERENCES CLIENTE (RUC)
  ON UPDATE cascade 
  ON DELETE cascade
);
go

-- Tabla Envio
create table ENVIO
(
  ID_PEDIDO int primary key,
  DIRECCION_DESTINATARIO VARCHAR(50) not null,
  CI_DESTINATARIO VARCHAR(10) not null,
  TELEFONO_DESTINATARIO VARCHAR(10) not null,
  ESTADO BIT not null,
  FECHA_FINALIZACION DATE,
  CONSTRAINT fk_ID_PEDIDO FOREIGN KEY (ID_PEDIDO) REFERENCES PEDIDO(ID)
  ON UPDATE cascade 
  ON DELETE cascade
);
go

-- Tabla Chofer_Envio
create table CHOFER_ENVIO
(
  ID_PEDIDO int primary key,
  CI_CHOFER VARCHAR(10) not null,
  CONSTRAINT fk_ID_ENVIO FOREIGN KEY (ID_PEDIDO) REFERENCES ENVIO (ID_PEDIDO)
  ON UPDATE cascade 
  ON DELETE cascade,
  CONSTRAINT fk_CI_CHOFER_ENVIO FOREIGN KEY (CI_CHOFER) REFERENCES EMPLEADOS (CI)
  ON UPDATE cascade 
  ON DELETE cascade
);
go

-- Procedimiento para Crear Factura y Envio
CREATE PROC CrearPedidoEnvio
@FECHA VARCHAR(10),
@RUC_CLIENTE VARCHAR(13),
@DETALLES VARCHAR(50),
@PESO VARCHAR(8),
@ENVIO_INTRAPROVINCIAL VARCHAR(5),
@DIRECCION_DESTINATARIO VARCHAR(50),
@CI_DESTINATARIO VARCHAR(10),
@TELEFONO_DESTINATARIO VARCHAR(10)
as
begin
	SET NOCOUNT ON
	Declare @ExistRUC bit;
	if exists
	(
		select RUC
		from CLIENTE
		where RUC = @RUC_CLIENTE
	)
		begin
			Set @ExistRUC = 1;
			if
			(
				@ENVIO_INTRAPROVINCIAL = 'Si'
			)
				begin
					SET NOCOUNT ON
					insert into PEDIDO values(@FECHA,@RUC_CLIENTE,@DETALLES,@PESO,1, convert(float,@PESO) * 1.75);
				end
			else
				begin
					SET NOCOUNT ON
					insert into PEDIDO values(@FECHA,@RUC_CLIENTE,@DETALLES,@PESO,0, (convert(float,@PESO) * 1.75)+(7));
				end
			insert into ENVIO(ID_PEDIDO, DIRECCION_DESTINATARIO, CI_DESTINATARIO, TELEFONO_DESTINATARIO, ESTADO) values(Scope_identity(),@DIRECCION_DESTINATARIO,@CI_DESTINATARIO,@TELEFONO_DESTINATARIO,0);
		end
	else
	begin
		Set @ExistRUC = 0;
	end
	Select @ExistRUC as 'ExistRUC';
end
go

-- Procedimiento para Obtener DATOS Pedidos/Envios
CREATE PROC ObtenerPedidoEnvio
@Condicion nvarchar(30)
as
begin
	SET NOCOUNT ON
	SELECT
		F.ID as 'ID',
		F.FECHA as 'Fecha',
		F.RUC_CLIENTE as 'RUC Cliente',
		F.DETALLES as 'Detalles',
		F.PESO as 'Peso',
		CASE WHEN F.ENVIO_INTRAPROVINCIAL = 1 then 'Si' else 'No' end as 'Envio Intraprovincial',
		CAST(ROUND(F.COSTO, 2, 1) AS DECIMAL(20,2)) 'Costo',
		E.DIRECCION_DESTINATARIO as 'Direccion Destinatario',
		E.CI_DESTINATARIO as 'CI Destinatario',
		E.TELEFONO_DESTINATARIO as 'Telefono Destinatario',
		CASE WHEN E.ESTADO = 1 then 'Finalizado' else 'Pendiente' end as 'Estado',
		E.FECHA_FINALIZACION as 'Fecha Finalizacion'

		FROM PEDIDO F
		JOIN ENVIO E ON F.ID = E.ID_PEDIDO
		where F.ID like @Condicion+'%' or F.RUC_CLIENTE like @Condicion+'%' or E.CI_DESTINATARIO like @Condicion+'%'
end
go

-- Procedimiento para Obtener DATOS Pedidos/Envios particular
CREATE PROC ObtenerPedidoEnvioParticular
@Condicion nvarchar(30)
as
begin
	SET NOCOUNT ON
	SELECT
		F.ID as 'ID',
		F.FECHA as 'Fecha',
		F.RUC_CLIENTE as 'RUC Cliente',
		F.DETALLES as 'Detalles',
		F.PESO as 'Peso',
		CASE WHEN F.ENVIO_INTRAPROVINCIAL = 1 then 'Si' else 'No' end as 'Envio Intraprovincial',
		CAST(ROUND(F.COSTO, 2, 1) AS DECIMAL(20,2)) 'Costo',
		E.DIRECCION_DESTINATARIO as 'Direccion Destinatario',
		E.CI_DESTINATARIO as 'CI Destinatario',
		E.TELEFONO_DESTINATARIO as 'Telefono Destinatario',
		CASE WHEN E.ESTADO = 1 then 'Finalizado' else 'Pendiente' end as 'Estado',
		E.FECHA_FINALIZACION as 'Fecha Finalizacion'

		FROM PEDIDO F
		JOIN ENVIO E ON F.ID = E.ID_PEDIDO
		where F.ID = @Condicion
end
go

--Procedimiento para Eliminar PedidoEnvio
CREATE PROC EliminarPedidoEnvio
@ID  VARCHAR(10)
as
begin
	SET NOCOUNT ON
	Declare @Finalizado bit;
	Select @Finalizado = ESTADO from ENVIO where ID_PEDIDO = @ID;
	if
	(
		@Finalizado = 0
	)
		begin
			delete from PEDIDO where ID = @ID
		end
	Select @Finalizado as 'Pedido Finalizado';
end
go

--Procedimiento para PedidoEnvio
CREATE PROC ModificarPedidoEnvio
@ID int,
@DETALLES VARCHAR(50),
@PESO VARCHAR(8),
@ENVIO_INTRAPROVINCIAL VARCHAR(5),
@DIRECCION_DESTINATARIO VARCHAR(50),
@TELEFONO_DESTINATARIO VARCHAR(10),
@ESTADO VARCHAR(10)
as
BEGIN 
     SET NOCOUNT ON 
	 Declare @PostEstado bit;
	 Select @PostEstado = ESTADO from ENVIO where ID_PEDIDO = @ID;
	 if
	 (
		@PostEstado = 0
	 )
		 begin
			SET NOCOUNT ON 
			UPDATE PEDIDO
			SET DETALLES = @DETALLES,
				PESO = @PESO,
				ENVIO_INTRAPROVINCIAL = CASE WHEN @ENVIO_INTRAPROVINCIAL = 'Si' then 1 else 0 end
			WHERE ID = @ID
			if
			(
				@ENVIO_INTRAPROVINCIAL = 'Si'
			)
				begin
					SET NOCOUNT ON
					UPDATE PEDIDO
					SET COSTO = (convert(float,@PESO) * 1.75)
					WHERE ID = @ID
				end
			else
				begin
					SET NOCOUNT ON
					UPDATE PEDIDO
					SET COSTO = (convert(float,@PESO) * 1.75)+(7)
					WHERE ID = @ID
				end
			UPDATE ENVIO
			SET	DIRECCION_DESTINATARIO = @DIRECCION_DESTINATARIO,
					TELEFONO_DESTINATARIO = @TELEFONO_DESTINATARIO,
					ESTADO = CASE WHEN @ESTADO = 'Finalizado' then 1 else 0 end
			WHERE ID_PEDIDO = @ID
			if
			(
				@ESTADO = 'Finalizado'
			)
			begin
				UPDATE ENVIO
				SET	FECHA_FINALIZACION = CAST(GETDATE() AS Date)
				WHERE ID_PEDIDO = @ID
			end
		 end
	 else
		 begin
			select @PostEstado as 'PostEstado';
		 end
END 
go

-- Procedimiento para Obtener DATOS Facturas/Envios Pendientes NO ASIGNADOS
CREATE PROC ObtenerPedidoEnvioPendienteNoAsignado
@Condicion nvarchar(30)
as
begin
	SET NOCOUNT ON
	SELECT
		F.ID as 'ID',
		F.FECHA as 'Fecha',
		F.RUC_CLIENTE as 'RUC Cliente',
		F.DETALLES as 'Detalles',
		F.PESO as 'Peso',
		CASE WHEN F.ENVIO_INTRAPROVINCIAL = 1 then 'Si' else 'No' end as 'Envio Intraprovincial',
		CAST(ROUND(F.COSTO, 2, 1) AS DECIMAL(20,2)) 'Costo',
		E.DIRECCION_DESTINATARIO as 'Direccion Destinatario',
		E.CI_DESTINATARIO as 'CI Destinatario',
		E.TELEFONO_DESTINATARIO as 'Telefono Destinatario',
		CASE WHEN E.ESTADO = 1 then 'Finalizado' else 'Pendiente' end as 'Estado',
		E.FECHA_FINALIZACION as 'Fecha Finalizacion'

	FROM PEDIDO F
	JOIN ENVIO E ON F.ID = E.ID_PEDIDO
	where (F.ID like @Condicion+'%' or F.RUC_CLIENTE like @Condicion+'%' or E.CI_DESTINATARIO like @Condicion+'%') and (E.ESTADO=0)
			and (F.ID NOT IN(SELECT ID_PEDIDO FROM CHOFER_ENVIO))
end
go

-- Procedimiento para Obtener ENVIOS ASIGNADOS
CREATE PROC ObtenerEnvioAsignado
@Condicion nvarchar(30)
as
begin
	SET NOCOUNT ON
	SELECT
		C.ID_PEDIDO AS 'ID Envio',
		C.CI_CHOFER AS 'CI Chofer'

		FROM CHOFER_ENVIO C
		JOIN ENVIO E ON C.ID_PEDIDO = E.ID_PEDIDO
		where (C.ID_PEDIDO like @Condicion+'%' or C.CI_CHOFER like @Condicion+'%') and (E.ESTADO = 0)
end
go

--Procedimiento para Asignar Envio
CREATE PROC AsignarEnvio
@ID_PEDIDO int,
@CI_CHOFER VARCHAR(10)
as
begin
	SET NOCOUNT ON
	if exists
	(
		select ID_PEDIDO
		from ENVIO
		where ID_PEDIDO = @ID_PEDIDO and ESTADO = 0
	)
	begin
		SET NOCOUNT ON
		insert into CHOFER_ENVIO values(@ID_PEDIDO,@CI_CHOFER);
	end
end
go

--Procedimiento para Eliminar Asignacion Envio
CREATE PROC EliminarAsignacionEnvio
@ID_PEDIDO int,
@CI_CHOFER VARCHAR(10)
as
begin
	SET NOCOUNT ON
	if exists
	(
		select ID_PEDIDO
		from CHOFER_ENVIO
		where ID_PEDIDO = @ID_PEDIDO and CI_CHOFER = @CI_CHOFER
	)
	begin
		SET NOCOUNT ON
		delete from CHOFER_ENVIO where ID_PEDIDO = @ID_PEDIDO and CI_CHOFER = @CI_CHOFER;
	end
end
go

-- Procedimiento para Obtener Envios Pendientes Chofer
CREATE PROC ObtenerEnviosPendientesDetalladosChofer
@CI nvarchar(10),
@Condicion nvarchar(30)
as
begin
	SET NOCOUNT ON
	SELECT
		F.ID as 'ID',
		F.FECHA as 'Fecha',
		F.RUC_CLIENTE as 'RUC Cliente',
		C.NOMBRE as 'Nombre Cliente',
		C.DIRECCION as 'Direccion Cliente',
		C.TELEFONO as 'Telefono Cliente',
		F.DETALLES as 'Detalles',
		F.PESO as 'Peso',
		CASE WHEN F.ENVIO_INTRAPROVINCIAL = 1 then 'Si' else 'No' end as 'Envio Intraprovincial',
		CAST(ROUND(F.COSTO, 2, 1) AS DECIMAL(20,2)) 'Costo',
		E.DIRECCION_DESTINATARIO as 'Direccion Destinatario',
		E.CI_DESTINATARIO as 'CI Destinatario',
		E.TELEFONO_DESTINATARIO as 'Telefono Destinatario',
		CASE WHEN E.ESTADO = 1 then 'Finalizado' else 'Pendiente' end as 'Estado',
		E.FECHA_FINALIZACION as 'Fecha Finalizacion'

		FROM PEDIDO F
		JOIN ENVIO E ON F.ID = E.ID_PEDIDO
		JOIN CHOFER_ENVIO CE ON CE.ID_PEDIDO = F.ID
		JOIN CLIENTE C ON C.RUC = F.RUC_CLIENTE
		where (CE.CI_CHOFER = @CI) and (F.ID like @Condicion+'%') and (E.ESTADO = 0)
end
go

--Procedimiento para PedidoEnvio
CREATE PROC FinalizarEnvio
@ID_ENVIO int
as
BEGIN 
     SET NOCOUNT ON 
	 if exists
	 (
		select ID_PEDIDO from ENVIO where ID_PEDIDO = @ID_ENVIO and ESTADO = 0
	 )
		begin
		SET NOCOUNT ON 
			UPDATE ENVIO
			SET	ESTADO = 1
			WHERE ID_PEDIDO = @ID_ENVIO
			UPDATE ENVIO
			SET	FECHA_FINALIZACION = CAST(GETDATE() AS Date)
			WHERE ID_PEDIDO = @ID_ENVIO
		end
END 
go