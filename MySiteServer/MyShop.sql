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

create table Goods
(
	GoodId int identity(1,1) primary key,
	GoodName nvarchar(255),
	GoodProducerid int foreign key references Producer(Id),
	GoodStackAmount int,
	GoodPrice int,
	GoodInfo text
)
create table News
(
NewsItemId int identity(1,1) primary key,
NewsItemTitle nvarchar(255),
ReleaseDateTime datetime,
NewsItemContent text
)
insert into Producer values('Hermes', null)
insert into Producer values('Lancome', null)

insert into Goods values ('H24', 1, 150, 5000, 'Супер - духи')
select * from Goods

insert into Goods values ('Idole', 2, 300, 1200, 'Классные духи)')
insert into Goods values ('la vie est belle', 2, 100, 4848, null)
insert into Goods values ('figues hesperides', 2, 200, 18980, null)
insert into Goods values ('miracle', 2, 70, 4554, null)

select * from Goods

insert into UserInfo values ('Ivan', '123', 'BaH9)', 0, null)

insert into UserInfo values ('Alena', '125', 'BaH9)', 1, null)

select * from UserInfo

update UserInfo
set L0gin  = 'olek' where Userid = 3
