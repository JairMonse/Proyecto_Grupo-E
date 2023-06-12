-- Tabla Empleados
create table EMPLEADOS
(
  CI  VARCHAR(10) primary key,
  NOMBRE VARCHAR(50) not null,
  APELLIDO VARCHAR(50) not null,
  CELULAR VARCHAR(10) not null,
  FECHA_NACIMIENTO DATE not null,
  CORREO VARCHAR(50) not null,
  DIRECCION VARCHAR(50) not null,
  CONTRASEÑA VARBINARY(128) not null
);
go

-- Tabla Cargo
create table CARGO
(
  ID_CARGO  VARCHAR(10) primary key,
  NOMBRE VARCHAR(10) not null
);
go

-- Tabla Empleados_Cargos
create table EMPLEADOS_CARGOS
(
  CI  VARCHAR(10) primary key,
  ID_CARGO VARCHAR(10) not null,
  CONSTRAINT fk_CI FOREIGN KEY (CI) REFERENCES EMPLEADOS (CI)
  ON UPDATE cascade 
  ON DELETE cascade,
  CONSTRAINT fk_CARGO FOREIGN KEY (ID_CARGO) REFERENCES CARGO (ID_CARGO)
  ON UPDATE cascade 
  ON DELETE cascade
);
go

--Procedimiento Login Empleado
CREATE PROC LoginEmpleado
@CI VARCHAR(10),
@CONTRASEÑA VARCHAR(128)
as
SELECT
  E.NOMBRE as 'Nombre',
  E.APELLIDO as 'Apellido',
  E.CI as 'CI',
  C.NOMBRE as 'CARGO'
FROM Empleados E
JOIN EMPLEADOS_CARGOS E_C ON E.CI = E_C.CI
JOIN CARGO C ON E_C.ID_CARGO = C.ID_CARGO
where E.CI= @CI and E.CONTRASEÑA = HASHBYTES('SHA2_512',@CONTRASEÑA)
go

--Procedimiento para Crear EMPLEADO
CREATE PROC CrearEmpleado
@CI VARCHAR(10),
@NOMBRE VARCHAR(50),
@APELLIDO VARCHAR(50),
@CELULAR VARCHAR(10),
@FECHA_N VARCHAR(10),
@CORREO VARCHAR(50),
@DIRECCION VARCHAR(50),
@CONTRASEÑA VARCHAR(128),
@TIPO VARCHAR(50)
as
begin
	if exists
	(
		select NOMBRE
		from CARGO
		where NOMBRE = @TIPO 
	)
	begin
		SET NOCOUNT ON
		insert into EMPLEADOS values(@CI,@NOMBRE,@APELLIDO,@CELULAR,@FECHA_N,@CORREO,@DIRECCION,HASHBYTES('SHA2_512',@CONTRASEÑA));
		insert into EMPLEADOS_CARGOS values(@CI,(select C.ID_CARGO FROM CARGO C WHERE  C.NOMBRE = @TIPO));
		IF @TIPO = 'Chofer' 
			insert into DISPONIBILIDAD_CHOFER values(@CI,1);
	end
end
go

--Procedimiento para Eliminar Empleado
CREATE PROC EliminarEmpleado
@CI  VARCHAR(10)
as
begin
	SET NOCOUNT ON
	delete from EMPLEADOS where CI = @CI
end
go

--Procedimiento para Modificar Empleado
CREATE PROC ModificarEmpleado
@CI  VARCHAR(10),
@NOMBRE VARCHAR(50),
@APELLIDO VARCHAR(50),
@CELULAR VARCHAR(10),
@FECHA_N VARCHAR(10),
@CORREO VARCHAR(50),
@DIRECCION VARCHAR(50)
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
END 
go