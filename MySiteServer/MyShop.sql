create database MyShop;
use MyShop
create table UserInfo
(
UserId int identity(1,1) primary key,
L0gin nvarchar(20), 
Passwrd nvarchar(20),
UserName nvarchar(20),
IsAdmin bit,
UserCart nvarchar(255)
)
create table Producer
(
Id int identity(1,1) primary key,
ProducerName nvarchar(255),
ProducerInfo text
)
drop table goods
create table Goods
(
	GoodId int identity(1,1) primary key,
	GoodName nvarchar(255),
	GoodProducerid int foreign key references Producer(Id),
	GoodStackAmount int,
	GoodPrice int,
	GoodInfo text
)

insert into Producer values('Hermes')
insert into Producer values('Lancome')

insert into Goods values ('H24', 1, 150, 5000, 'Супер - духи')
select * from Goods


insert into Goods values ('Idole', 2, 300, 1200, 'Классные духи)')

select * from Goods

insert into UserInfo values ('Ivan', '123', 'BaH9)', 0, null)

insert into UserInfo values ('Alena', '125', 'BaH9)', 1, null)

select * from UserInfo

update UserInfo
set UserCart = null where UserId=1
