--Insercion de Datos de Prueba

insert into CARGO values('00S','Secretaria');
go
insert into CARGO values('00C','Chofer');
go
insert into CARGO values('00A','Admin');
go
exec CrearEmpleado '1719963470','Luis','Vera','9090909090','2001-03-15','luis@gmail.com','Muy muy lejos','123','Secretaria'
go
exec CrearEmpleado '1719963471','Braulio','Marcapulpo','9090909091','2001-01-22','braulio@gmail.com','En un agujero','12345','Secretaria'
go
exec CrearEmpleado '1111111111','Jesus','Monserrate','1231231231','2001-06-19','jesus@gmail.com','En su casa','321','Chofer'
go
exec CrearEmpleado '2222222222','Cesar','Carrion','3213213213','2002-05-17','cesar@gmail.com','Marte','1234','Admin'
go
exec CrearCliente '1231231231231','Coca-Cola-Company','1231231231','coca_cola@cocacola.com','La Luna'
go
exec CrearCliente '3213213213213','FaustoShop','2222222222','fausto@gmail.com','Jupiter'
go
insert into LISTADO_TIPOS_VEHICULOS values('000C1','Camioneta');
go
insert into LISTADO_TIPOS_VEHICULOS values('000C0','Camion');
go
insert into LISTADO_TIPOS_VEHICULOS values('000T1','Tanque Pesado Aleman');
go
insert into LISTADO_TIPOS_VEHICULOS values('000A1','Avion Comercial');
go
insert into LISTADO_TIPOS_VEHICULOS values('000B1','Burro');
go
exec CrearVehiculo 'ABC1234','Toyota','2017','Camioneta'
go
exec CrearVehiculo 'CBA4321','Ferrari','2018','Camion'
go
exec CrearVehiculo 'TKK6666','Tiger I','1942','Tanque Pesado Aleman'
go
exec CrearVehiculo 'HCabc91','AirbusA380','2015','Avion Comercial'
go
exec CrearVehiculo 'susij00','Currincho','2018','Burro'
go
exec CrearPedidoEnvio '2022-08-29','1231231231231','5 paquetes de 6 Cocacolas de 5L','50','No','Muy lejos','1234567890','0990634712'
go
exec CrearPedidoEnvio '2022-08-28','3213213213213','70 kg de madera','70','Si','La tiendita de Luis','1234567890','0990634713'
go