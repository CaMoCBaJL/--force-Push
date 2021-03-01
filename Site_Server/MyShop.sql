create database MyShop;
use MyShop
create table ShopUser
(
UserId int primary key,
UserName nvarchar(20),
UserPassword nvarchar(20),
IsAdmin bit,
UserCart nvarchar(255)
)
create table Producer
(
Id int identity(1,1) primary key,
ProducerName nvarchar(255)
)
drop table goods
create table Goods
(
	GoodId int identity(1,1) primary key,
	GoodName nvarchar(255),
	GoodProducerid int foreign key references Producer(Id),
	GoodStackAmount int,
	PathToGoodPicture varchar(255),
	GoodPrice int
)

insert into Producer values('Hermes')
insert into Producer values('Lancome')

insert into Goods values ('H24', 1, 150, N'C:\DBImages\1.jpg', 5000)
select * from Goods


insert into Goods values ('Idole', 2, 300,  N'C:\DBImages\2.jpg', 1200)

select * from Goods
