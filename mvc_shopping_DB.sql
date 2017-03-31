create database mvc_shopping 
use mvc_shopping

drop database mvc_shopping

create table Customers
(
CustomerId int identity(1000,1),

CustomerName varchar(100),
CustomerMobile varchar(10),
CustomerEmailId varchar(100) primary key,


)


alter table customers drop column customeraddress
drop table customers
select * from customers
delete  customers where customerid=1000
create table Administrator
(
AdminId int identity(2000,1) ,

AdminName varchar(100),
AdminMobile varchar(10),
AdminEmailId varchar(100) primary key,


)
drop table Administrator
select * from Administrator

create table Delivery
(
DeliveryIdNO int identity(3000,1) ,

DeliveryName varchar(100),
DeliveryMobile varchar(10),
DeliveryEmailId varchar(100) primary key,

)
insert delivery values('aa',11,'114@gmail.com')
sp_help delivery
sp_help customers
sp_help administrator
drop table Delivery
select * from customers
select * from Delivery order by DeliveryIdNO
select * from Administrator order by adminid




Create table Products(

CategoryName varchar(50),
ProductId int identity(4000,1) primary key,
ProductName varchar(100),
ProductPrice int,
ProductDescription varchar(max),
ProductImageAddr varchar(100),
AvailableQty int
)
drop table products
select * from Products
delete products where productid=4031

alter table products alter column productdescription varchar(max)
alter table products add  

create table Cart(
CartId int identity(5000,1) primary key,
Customeremailid varchar(100) foreign key references customers(customeremailid),
ProductId int foreign key references products(ProductId),
AddedDate datetime
)

drop table cart
truncate table cart
alter table cart add  CustomerId int foreign key references customers(customerid)
select * from cart


create table Orders(
OrderID int identity(6000,1) primary key,
CustomerEmailId varchar(100) foreign key references customers(CustomerEmailId),
ProductId int foreign key references products(productId),
ProductName varchar(100),
ProductPrice int,
ProductQuantity int,
OrderPrice int,
CustomerName varchar(max),
DeliveryAddress varchar(max),
Deliverycity varchar(100),
State varchar(100),
Pincode varchar(6),
MobileNo varchar(10),
OrderDate datetime,
PaymentOption varchar(50)
)

create table status(
Orderstatid int ,
orderstatus varchar(100)
)
select * from orderstatus
drop table orderstatus

drop table orders

select * from orders
select * from customers
select * from  cart
select * from delivery order by deliveridno  
select * from Administrator


select count(*) from customers where customeremailid=1

create table city(
cityid int identity (7000,1) primary key,
cityname varchar(100)
)

select * from city

create table state (

stateid int identity (8000,1) primary key,
statename varchar(100)
)


insert city values('Chennai')
insert city values('Madurai')
insert city values('Salem')


insert state values ('Tamilnadu')
insert state values('Kerala')
insert state values('Maharashtra')
insert state values ('Karnataka')

select * from city
select * from state


select AvailableQty from products where productid=4014


create function getcityname(@cityid int ) returns varchar(100)
as 
begin
declare @cityname varchar(100)
select @cityname=cityname from city where cityid=@cityid
return @cityname
end



create function getstatename(@stateid int )returns varchar(100)
as
begin
declare @statename varchar(100)
select @statename=statename from state where stateid=@stateid
return @statename
end

select dbo.getcityname(Deliverycity) as 'cityname' from orders

select dbo.getstatename(state) as'statename' from orders
create function getstatename


create trigger update_cart on orders
for insert 
as
begin
declare @pid int
declare @cemailid varchar(max)
select  @pid=ProductId,@cemailid=CustomerEmailId from orders
delete cart where productid=@pid and customeremailid=@cemailid

end

select * from Orders
sp_help delivery
select * FROM customers order by customerid 

select * from products
select * from products where productname like '%iphone%'

select * from products where productname like '%iphone%'

select * from customers

select orderstatus from status where orderstatid=6000
truncate table orders

create table deliveryorders(
deliveryorderid int identity(10000,1),
orderid int ,
deliverydate datetime

)
insert deliveryorders values(6001,getdate())

select * from city
select * from orders
select * from status
select * from deliveryorders
drop table deliveryorders

select * from orders
insert
select * from deliveryorders 
select *,dbo.getcityname(Deliverycity),dbo.getstatename(state) from orders where  orderid not in (select orderid from deliveryorders where deliveryorderid=10002)
select * from orders
select count(*) from deliveryorders where orderid=6001
select * from status
update status set orderstatus='delivered' where orderstatid=6001

truncate table cart
truncate table orders
truncate  table status
truncate table deliveryorders
select count(*) from cart where productid=4035 and customeremailid='g@gmail.com'
select * from cart

select * from cart
select * from orders
select  * from status
select * from deliveryorders
 