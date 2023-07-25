-- Procedimiento para Obtener Secretarias
CREATE PROC ObtenerSecretaria
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
		E.DIRECCION as 'Direccion'

		FROM EMPLEADOS E
		JOIN EMPLEADOS_CARGOS E_C ON E.CI = E_C.CI
		where E_C.ID_CARGO = '00S' and (E.CI like @Condicion+'%' or E.NOMBRE like @Condicion+'%' or E.APELLIDO like @Condicion+'%')
end
go