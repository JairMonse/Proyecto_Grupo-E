--Insercion de Datos de Prueba

insert into CARGO values('00S','Secretaria');
go
insert into CARGO values('00C','Chofer');
go
exec CrearEmpleado '0123456789','Maikel','Meza','0987981832','2001-09-26','Maikeldani@gmail.com','Cerca De mi Casa','251281','Secretaria'
go
exec CrearEmpleado '1234567890','Bryan','Alay','2222222222','2000-05-07','Bryan@gmail.com','En la esquina','1234567890','Secretaria'
go
exec CrearEmpleado '9876543210','Jean','Rivera','1234123412','2001-01-01','jean@gmail.com','En su casa','543210','Chofer'
go
exec CrearEmpleado '2222222222','Jair','Monserrate','4321432143','2000-09-03','Jair@gmail.com','YOYO','43210','Chofer'
go
exec CrearCliente '1234567891234','Leneker','9876543210','Leneker@gmail.com','Universidad'
go
exec CrearCliente '9876543219876','Erick','5432154321','Erick@gmail.com','Centro'
go
