create table ventas_mensuales (
id int identity primary key not null,
fecha_informe date not null,
codigo_vendedor varchar(3) not null,
venta float not null,
tamañoEmpresa bit not null
)

create table parametria (
id int identity primary key not null,
fecha_proceso date not null,
);
insert into parametria (fecha_proceso) values ('2023-10-31')

create table rechazos (
id int identity primary key not null,
informe varchar(100) not null,
)

