-- Tabla Disponibilidad_CHOFER
create table DISPONIBILIDAD_CHOFER
(
  CI  VARCHAR(10) primary key,
  DISPONIBILIDAD BIT not null,
  CONSTRAINT fk_CEDULA_C FOREIGN KEY (CI) REFERENCES EMPLEADOS (CI)
  ON UPDATE cascade 
  ON DELETE cascade
);
go

-- Procedimiento para Obtener Choferes
CREATE PROC ObtenerChofer
@Condicion nvarchar(30)
as
begin
	SET NOCOUNT ON
	SELECT
		E.CI as 'Cedula',
		E.NOMBRE as 'Nombre',
		E.APELLIDO as 'Apellido',
		E.CELULAR as 'Celular',
		E.FECHA_NACIMIENTO as 'Fecha de Nacimiento',
		E.CORREO as 'Correo',
		E.DIRECCION as 'Direccion',
		CASE WHEN D_C.DISPONIBILIDAD = 1 then 'Disponible' else 'No disponible' end as 'Disponibilidad'

		FROM EMPLEADOS E
		JOIN DISPONIBILIDAD_CHOFER D_C ON E.CI = D_C.CI
		where E.CI like @Condicion+'%' or E.NOMBRE like @Condicion+'%' or E.APELLIDO like @Condicion+'%'
end
go

-- Procedimiento para Obtener Choferes Disponibles
CREATE PROC ObtenerChoferDisponible
@Condicion nvarchar(30)
as
begin
	SET NOCOUNT ON
	SELECT
		E.CI as 'Cedula',
		E.NOMBRE as 'Nombre',
		E.APELLIDO as 'Apellido',
		E.CELULAR as 'Celular',
		E.FECHA_NACIMIENTO as 'Fecha de Nacimiento',
		E.CORREO as 'Correo',
		E.DIRECCION as 'Direccion',
		CASE WHEN D_C.DISPONIBILIDAD = 1 then 'Disponible' else 'No disponible' end as 'Disponibilidad'

		FROM EMPLEADOS E
		JOIN DISPONIBILIDAD_CHOFER D_C ON E.CI = D_C.CI
		where D_C.DISPONIBILIDAD=1 and (E.CI like @Condicion+'%' or E.NOMBRE like @Condicion+'%' or E.APELLIDO like @Condicion+'%')
end
go

--Procedimiento para Modificar Chofer
CREATE PROC ModificarChofer
@CI  VARCHAR(10),
@NOMBRE VARCHAR(50),
@APELLIDO VARCHAR(50),
@CELULAR VARCHAR(10),
@FECHA_N VARCHAR(10),
@CORREO VARCHAR(50),
@DIRECCION VARCHAR(50),
@DISPONIBILIDAD VARCHAR(15)
as
BEGIN 
     SET NOCOUNT ON 
     UPDATE EMPLEADOS
     SET    NOMBRE = @NOMBRE,
			APELLIDO = @APELLIDO,
			CELULAR = @CELULAR,
			FECHA_NACIMIENTO = @FECHA_N,
			CORREO = @CORREO,
			DIRECCION = @DIRECCION
     WHERE  CI = @CI
	 UPDATE DISPONIBILIDAD_CHOFER
	 SET	DISPONIBILIDAD = CASE WHEN @DISPONIBILIDAD = 'Disponible' then 1 else 0 end
	 WHERE	CI = @CI
END 
go

drop procedure ModificarChofer

--Procedimiento para Modificar Disponibilidad Chofer
CREATE PROC ModificarDisponibilidadChofer
@CI  VARCHAR(10),
@DISPONIBILIDAD VARCHAR(15)
as
BEGIN 
	DECLARE @Result BIT;
	if not exists
	(
		SELECT
		CI_CHOFER as 'CEDULA CHOFER',
		MATRICULA_VEHICULO as 'MATRICULA VEHICULO'
		FROM CHOFER_VEHICULO
		where CI_CHOFER like @CI
	)
		begin
			SET NOCOUNT ON 
			UPDATE DISPONIBILIDAD_CHOFER
			SET	DISPONIBILIDAD = CASE WHEN @DISPONIBILIDAD = 'Disponible' then 1 else 0 end
			WHERE	CI = @CI;
			SET @Result = 1;
			SELECT @Result as Resultado;
		end
	else
		begin
			SET @Result = 0;
			SELECT @Result as Resultado;
		end
END 
go

