use Concert_company;
go

create table Product(
	ID int not null identity(1,1) constraint PK_ProductID primary key,
	Name nvarchar(30) not null,
	Quantity int not null,
	Cost_price money not null,
	Type_of_productId int not null
);
go


create table Type_of_product(
	ID int not null identity(1,1) constraint PK_Type_of_productID primary key,
	Name nvarchar(30) not null 
);
go


create table Manager(
	ID int not null identity(1,1) constraint PK_ManagerID primary key,
	Name nvarchar(30) not null,
	Surname nvarchar(30) not null,
	Salary money not null
);
go



create table Sale(
	ID int not null identity(1,1) constraint PK_SaleID primary key,
	ProductId int not null,
	Name_buyer nvarchar(30),
	ManagerId int not null,
	Quantity int not null,
	Price money not null,
	[Date] date 
);
go


alter table Product add foreign key ([Type_of_productId]) references [Type_of_product](ID);
go
alter table Sale add foreign key ([ProductId]) references [Product](ID);
go
alter table Sale add foreign key ([ManagerId]) references [Manager](ID);
go
INSERT INTO Type_of_product(Name)
VALUES (N'Корондаш'),
(N'Ручка'),
(N'Кисточка');

INSERT INTO Product (Name, Quantity, Cost_price,Type_of_productId)
VALUES (N'Кубик', 100,10,1),
(N'Ромбик', 90,19,1),
(N'Кружёк', 10,50,1),
(N'Смурфик', 100,10,2),
(N'ЕГЭ на 100 б', 88,100,2),
(N'Буква зю', 300,14,2),
(N'Белочка', 44,44,3),
(N'Лисичка', 213,10,3),
(N'Ежик', 142,7,3);

INSERT INTO Manager(Name,Surname,Salary)
VALUES (N'Иван','Иванов',10000),
(N'Сергей','Сергеич',1000),
(N'Семён','Семёныч',2000),
(N'Старый','Бог',3000),
(N'Радан','Яторо',4100);

INSERT INTO Sale(ProductId,Name_buyer,ManagerId,Quantity,Price)
VALUES (1,N'ООО.Вам Бан', 1,10,1000),
(2,N'ООО.Вам Вак Бан', 2,1,10),
(3,N'ООО.Вам ', 3,10,10000),
(4,N'ООО.Вам Дом', 4,17,1000000);



