-- Tabla Cliente
create table CLIENTE
(
  RUC  VARCHAR(13) primary key,
  NOMBRE VARCHAR(50) not null,
  TELEFONO VARCHAR(10) not null,
  CORREO VARCHAR(50) not null,
  DIRECCION VARCHAR(50) not null
);
go

-- Procedimiento para Obtener Cliente
CREATE PROC ObtenerCliente
@Condicion nvarchar(50)
as
begin
	SET NOCOUNT ON
	select *from CLIENTE where RUC like @Condicion+'%' or NOMBRE like @Condicion+'%'
end
go

--Procedimiento para Crear Cliente
CREATE PROC CrearCliente
@RUC  VARCHAR(13),
@NOMBRE VARCHAR(50),
@TELEFONO VARCHAR(10),
@CORREO VARCHAR(50),
@DIRECCION VARCHAR(50)
as
begin
	SET NOCOUNT ON
	insert into CLIENTE values(@RUC,@NOMBRE,@TELEFONO,@CORREO,@DIRECCION)
end
go

--Procedimiento para Eliminar Cliente
CREATE PROC EliminarCliente
@RUC  VARCHAR(13)
as
begin
	SET NOCOUNT ON
	delete from CLIENTE where RUC = @RUC
end
go

--Procedimiento para Modificar Cliente
CREATE PROC ModificarCliente
@RUC  VARCHAR(13),
@NOMBRE VARCHAR(50),
@TELEFONO VARCHAR(10),
@CORREO VARCHAR(50),
@DIRECCION VARCHAR(50)
as
BEGIN 
     SET NOCOUNT ON 

     UPDATE CLIENTE
     SET    NOMBRE = @NOMBRE,
			TELEFONO = @TELEFONO,
			CORREO = @CORREO,
			DIRECCION = @DIRECCION
     WHERE  RUC = @RUC
END 
go