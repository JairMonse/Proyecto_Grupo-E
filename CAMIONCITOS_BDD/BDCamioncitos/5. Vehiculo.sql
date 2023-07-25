-- Tabla Vehiculo
create table VEHICULO
(
  MATRICULA  VARCHAR(10) primary key,
  MARCA VARCHAR(20) not null,
  AÑO VARCHAR(5) not null
);
go

-- Tabla Disponibilidad_Vehiculos
create table DISPONIBILIDAD_VEHICULO
(
  MATRICULA  VARCHAR(10) primary key,
  DISPONIBILIDAD BIT not null,
  CONSTRAINT fk_MATRICULA FOREIGN KEY (MATRICULA) REFERENCES VEHICULO (MATRICULA)
  ON UPDATE cascade 
  ON DELETE cascade
);
go

-- Tabla LISTADO_TIPOS_VEHICULOS
create table LISTADO_TIPOS_VEHICULOS
(
  ID_TIPO_VEHICULO VARCHAR(5) primary key,
  NOMBRE VARCHAR(20) not null,
);
go

-- Tabla UNION entre VEHICULO y LISTADO_TIPOS_VEHICULOS
create table TIPOS_VEHICULOS
(
  MATRICULA  VARCHAR(10) primary key,
  ID_TIPO_VEHICULO VARCHAR(5) not null,
  CONSTRAINT fk_MATRICULA_TV FOREIGN KEY (MATRICULA) REFERENCES VEHICULO (MATRICULA) 
  ON UPDATE cascade 
  ON DELETE cascade,
  CONSTRAINT fk_ID_TIPO_VEHICULO FOREIGN KEY (ID_TIPO_VEHICULO) REFERENCES LISTADO_TIPOS_VEHICULOS (ID_TIPO_VEHICULO) 
  ON UPDATE cascade 
  ON DELETE cascade
);
go

-- Tabla Chofer_Vehiculo
create table CHOFER_VEHICULO
(
  CI_CHOFER VARCHAR(10) primary key,
  MATRICULA_VEHICULO VARCHAR(10) not null
  CONSTRAINT fk_MATRICULA_CV FOREIGN KEY (MATRICULA_VEHICULO) REFERENCES VEHICULO (MATRICULA),
  CONSTRAINT fk_CI_CV FOREIGN KEY (CI_CHOFER) REFERENCES EMPLEADOS (CI)
  ON UPDATE cascade
  ON DELETE cascade
);
go

--Procedimiento para Obtener Choferes Vinculados a Vehiculo
CREATE PROC ObtenerVinculoChoferVehiculo
@Condicion nvarchar(30)
as
begin
	SET NOCOUNT ON
	SELECT
		CI_CHOFER as 'CEDULA CHOFER',
		MATRICULA_VEHICULO as 'MATRICULA VEHICULO'
		FROM CHOFER_VEHICULO
		where CI_CHOFER like @Condicion+'%' or MATRICULA_VEHICULO like @Condicion+'%'
end
go

--Procedimiento para crear Union Chofer_Vehiculo 
CREATE PROC UnirChoferVehiculo
@CI_CHOFER VARCHAR(10),
@MATRICULA_VEHICULO VARCHAR(10)
as
begin
	if exists
	(
		select CI
		from EMPLEADOS_CARGOS
		where CI = @CI_CHOFER and ID_CARGO = '00C'
	)
	begin
		SET NOCOUNT ON
		insert into CHOFER_VEHICULO values(@CI_CHOFER,@MATRICULA_VEHICULO);
	end
	if exists
	(
		select CI_CHOFER
		from CHOFER_VEHICULO
		where CI_CHOFER = @CI_CHOFER
	)
	begin
		SET NOCOUNT ON
		UPDATE DISPONIBILIDAD_VEHICULO
		 SET	DISPONIBILIDAD = 0
		 WHERE	MATRICULA = @MATRICULA_VEHICULO;
		UPDATE DISPONIBILIDAD_CHOFER
		 SET	DISPONIBILIDAD = 0
		 WHERE	CI = @CI_CHOFER;
	 end
end
go

--Procedimiento para eliminar Union Chofer_Vehiculo (falta implementar)
CREATE PROC DesvincularChoferVehiculo
@CI_CHOFER VARCHAR(10),
@MATRICULA_VEHICULO VARCHAR(10)
as
begin
	if exists
	(
		select CI_CHOFER, MATRICULA_VEHICULO
		from CHOFER_VEHICULO
		where CI_CHOFER = @CI_CHOFER and MATRICULA_VEHICULO = @MATRICULA_VEHICULO
	)
	begin
	SET NOCOUNT ON
	delete from CHOFER_VEHICULO where CI_CHOFER = @CI_CHOFER;
	UPDATE DISPONIBILIDAD_VEHICULO
		 SET	DISPONIBILIDAD = 1
		 WHERE	MATRICULA = @MATRICULA_VEHICULO;
	UPDATE DISPONIBILIDAD_CHOFER
		 SET	DISPONIBILIDAD = 1
		 WHERE	CI = @CI_CHOFER;
	end
end
go

-- Procedimiento para Obtener Vehiculo
CREATE PROC ObtenerVehiculo
@Condicion nvarchar(30)
as
begin
	SET NOCOUNT ON
	SELECT
		V.MATRICULA as 'Matricula',
		V.MARCA as 'Marca',
		V.AÑO as 'Año',
		L_T_V.NOMBRE as 'Tipo',
		CASE WHEN D_V.DISPONIBILIDAD = 1 then 'Disponible' else 'No disponible' end as 'Disponibilidad'

		FROM VEHICULO V
		JOIN DISPONIBILIDAD_VEHICULO D_V ON V.MATRICULA = D_V.MATRICULA
		JOIN TIPOS_VEHICULOS T_V ON V.MATRICULA = T_V.MATRICULA
		JOIN LISTADO_TIPOS_VEHICULOS L_T_V ON T_V.ID_TIPO_VEHICULO = L_T_V.ID_TIPO_VEHICULO
		where V.MATRICULA like @Condicion+'%' or L_T_V.NOMBRE like @Condicion+'%' or V.MARCA like @Condicion+'%'
end
go

-- Procedimiento para Obtener Vehiculos Disponibles
CREATE PROC ObtenerVehiculoDisponible
@Condicion nvarchar(30)
as
begin
	SET NOCOUNT ON
	SELECT
		V.MATRICULA as 'Matricula',
		V.MARCA as 'Marca',
		V.AÑO as 'Año',
		L_T_V.NOMBRE as 'Tipo',
		CASE WHEN D_V.DISPONIBILIDAD = 1 then 'Disponible' else 'No disponible' end as 'Disponibilidad'

		FROM VEHICULO V
		JOIN DISPONIBILIDAD_VEHICULO D_V ON V.MATRICULA = D_V.MATRICULA
		JOIN TIPOS_VEHICULOS T_V ON V.MATRICULA = T_V.MATRICULA
		JOIN LISTADO_TIPOS_VEHICULOS L_T_V ON T_V.ID_TIPO_VEHICULO = L_T_V.ID_TIPO_VEHICULO
		where D_V.DISPONIBILIDAD = 1 and (V.MATRICULA like @Condicion+'%' or L_T_V.NOMBRE like @Condicion+'%' or V.MARCA like @Condicion+'%')
end
go

--Procedimiento para Crear Vehiculo
CREATE PROC CrearVehiculo
@MATRICULA  VARCHAR(10),
@MARCA VARCHAR(50),
@AÑO VARCHAR(10),
@TIPO VARCHAR(50)
as
begin
	if exists
	(
		select NOMBRE
		from LISTADO_TIPOS_VEHICULOS
		where NOMBRE = @TIPO
	)
	begin
		SET NOCOUNT ON
		insert into VEHICULO values(@MATRICULA,@MARCA,@AÑO);
		insert into TIPOS_VEHICULOS values(@MATRICULA,(select L_T_V.ID_TIPO_VEHICULO FROM LISTADO_TIPOS_VEHICULOS L_T_V WHERE  L_T_V.NOMBRE = @TIPO));
		insert into DISPONIBILIDAD_VEHICULO values(@MATRICULA,1);
	end
end
go

--Procedimiento para Eliminar Vehiculo
CREATE PROC EliminarVehiculo
@MATRICULA  VARCHAR(10)
as
begin
	SET NOCOUNT ON
	delete from VEHICULO where MATRICULA = @MATRICULA
end
go

--Procedimiento para Modificar Vehiculo
CREATE PROC ModificarVehiculo
@MATRICULA  VARCHAR(10),
@MARCA VARCHAR(50),
@AÑO VARCHAR(10),
@TIPO VARCHAR(50),
@DISPONIBILIDAD VARCHAR(15)
as
BEGIN 
     SET NOCOUNT ON 

     UPDATE VEHICULO
     SET    MARCA = @MARCA,
			AÑO = @AÑO
     WHERE  MATRICULA = @MATRICULA
	 UPDATE T_V
	 SET	T_V.ID_TIPO_VEHICULO = L_T_V.ID_TIPO_VEHICULO
	 FROM	TIPOS_VEHICULOS T_V
	 JOIN	LISTADO_TIPOS_VEHICULOS L_T_V on L_T_V.NOMBRE = @TIPO
	 WHERE  MATRICULA = @MATRICULA
	 UPDATE DISPONIBILIDAD_VEHICULO
	 SET	DISPONIBILIDAD = CASE WHEN @DISPONIBILIDAD = 'Disponible' then 1 else 0 end
	 WHERE	MATRICULA = @MATRICULA
END 
go

-- Procedimiento para Mostrar Lista Tipos de Vihiculos
CREATE PROC Lista_Tipos_Vehiculos
as
begin
	select NOMBRE from LISTADO_TIPOS_VEHICULOS
end
go