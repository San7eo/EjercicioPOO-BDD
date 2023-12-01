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

select * from parametria

select * from rechazos

select * from ventas_mensuales

select codigo_vendedor, sum(venta) as TotalVentas
from ventas_mensuales
group by codigo_vendedor
having  sum(venta) > 100000

select codigo_vendedor, sum(venta) as TotalVentas
from ventas_mensuales
where codigo_vendedor = 123
group by codigo_vendedor
