USE [master]
GO
CREATE DATABASE [GL_Support]
 GO

USE [GL_Support]
GO
CREATE TABLE Sex
(
	[SexId] int IDENTITY(1, 1) PRIMARY KEY not null,
	[Sex]  NVARCHAR(100) not null,  
)

INSERT INTO Sex  VALUES ('Man');
INSERT INTO Sex  VALUES ('Woman');
INSERT INTO Sex  VALUES ('Not set');
--INSERT INTO Sex  VALUES ('Wild Animal');
--SELECT * FROM Sex

CREATE TABLE WorkTime
(
	[WorkTimeId] int IDENTITY(1, 1) PRIMARY KEY not null,
	[From] time not null,
	[To] time not null,
)

INSERT INTO WorkTime  VALUES ('6:00:00','14:00:00');
INSERT INTO WorkTime  VALUES ('8:00:00','17:00:00');
INSERT INTO WorkTime  VALUES ('12:00:00','21:00:00');
INSERT INTO WorkTime  VALUES ('18:00:00','2:00:00');
INSERT INTO WorkTime  VALUES ('00:00:00','8:00:00');

CREATE TABLE CommunicationType
(
	[CommunicationTypeId] int IDENTITY(1, 1) PRIMARY KEY not null,
	[CommunicationType] NVARCHAR(300) not null,
)

INSERT INTO [CommunicationType]  VALUES ('Chat');
INSERT INTO [CommunicationType]  VALUES ('Telegramm');
INSERT INTO [CommunicationType]  VALUES ('Email');
INSERT INTO [CommunicationType]  VALUES ('Phone');
--INSERT INTO [CommunicationType]  VALUES ('Psionic');

CREATE TABLE RequestType
(
	[RequestTypeId] int IDENTITY(1, 1) PRIMARY KEY not null,
	[RequestType] NVARCHAR(300) not null,
)

INSERT INTO RequestType  VALUES ('General issues');
INSERT INTO RequestType  VALUES ('Technical issues');
INSERT INTO RequestType  VALUES ('Commercial issues');
INSERT INTO RequestType  VALUES ('Other issues');


CREATE TABLE [Status]
(
	[StatusId] int IDENTITY(1, 1) PRIMARY KEY not null,
	[Status] NVARCHAR(100)  not null,  
)

INSERT INTO [Status]   VALUES ('New');
INSERT INTO [Status]   VALUES ('In waiting');
INSERT INTO  [Status]  VALUES ('Solved');

CREATE TABLE [User]
(
	[UserId] int IDENTITY(1, 1) PRIMARY KEY not null,
	[UserGUID] uniqueidentifier  not null default NEWID(),
	FirstName NVARCHAR(100)  ,  
	LastName NVARCHAR(100)  ,  
	SecondName  NVARCHAR(100)  ,  
	Age int,
	Phone NVARCHAR(22)  ,
	Email NVARCHAR(100),
	UserIp NVARCHAR(100),
	Sex int ,
	PasswordHash VARBINARY(64) ,
	CONSTRAINT [FK_UserSex] FOREIGN KEY ([Sex]) REFERENCES [Sex]([SexId]),
)
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserGUID] ON [User](UserGUID Asc)

INSERT INTO [User] (FirstName , sex)  VALUES ('Valera' , 3);
--select * from [User]

CREATE TABLE Employee
(
	[EmployeeId] int IDENTITY(1, 1) PRIMARY KEY not null,
	FirstName NVARCHAR(100)  ,  
	LastName NVARCHAR(100)  ,  
	SecondName  NVARCHAR(100)  ,  
	Age int,
	Phone NVARCHAR(22)  ,
	Email NVARCHAR(100),
	Sex int not null ,
	CONSTRAINT [FK_EmployeeSex] FOREIGN KEY ([Sex]) REFERENCES [Sex]([SexId]),
	WorkTime int  not null ,
	CONSTRAINT [FK_WorkTime] FOREIGN KEY ([WorkTime]) REFERENCES [WorkTime]([WorkTimeId]),
	PasswordHash VARBINARY(64) not null ,
	[EmployeeGUID] uniqueidentifier  not null default NEWID(),
)
CREATE UNIQUE NONCLUSTERED INDEX [IX_EmployeeGUID] ON [Employee](EmployeeGUID Asc)

INSERT INTO Employee VALUES ('√розный','»ван','÷аревич',23,'+1234567823','a@a.a',1,2 ,Cast('zzz' As varbinary(max)),NEWID());

--select * from Employee

CREATE TABLE Chat
(
	[ChatId] int IDENTITY(1, 1) PRIMARY KEY not null,
	[GUIDEmployee] uniqueidentifier  default null , 
	[GUIDUser] uniqueidentifier  default null, 
	--CONSTRAINT [FK_Chat_GUIDEmployee] FOREIGN KEY ([GUIDEmployee])
	-- REFERENCES [Employee]([EmployeeGUID]),
	--CONSTRAINT [FK_Chat_GUIDUser] FOREIGN KEY ([GUIDUser]) 
	--REFERENCES [Employee]([UserGUID]),
)

INSERT INTO Chat(GUIDUser) VALUES ((SELECT MAX([UserGUID]) FROM [User]) );
--select * from Chat


CREATE TABLE [Application]
(
	[ApplicationId] int IDENTITY(1, 1) PRIMARY KEY not null,
	Topic NVARCHAR(150) not null,  
	Chat int not null ,
	CONSTRAINT [FK_Chat] FOREIGN KEY (Chat) REFERENCES [Chat]([ChatId]),
	[Status] int  not null default 1 , -- mayby do defaul = 1 (NEW)
	CONSTRAINT [FK_Status] FOREIGN KEY ([Status]) REFERENCES [Status]([StatusId]),
)

INSERT INTO [Application] VALUES ('What is thet?',1,2);
--select * from [Application]

CREATE TABLE Task
(
	[TaskId] int IDENTITY(1, 1) PRIMARY KEY not null,
	TaskCount int not null default 1, 
	GUIDEmployy uniqueidentifier not null  , 
	CONSTRAINT FK_Task_GUIDEmployy FOREIGN KEY ([GUIDEmployy]) REFERENCES [Employee](EmployeeGUID),
)


CREATE TABLE Details
(
	[DetailsId] int IDENTITY(1, 1) PRIMARY KEY not null,
	[CreatingDate] DATETIME not null default CURRENT_TIMESTAMP,
	[Data] NVARCHAR(4000) , 
	[Chat] int ,
	CONSTRAINT [FK_DetailsChat] FOREIGN KEY ([Chat]) REFERENCES [Chat]([ChatId]),
	RequestType int ,
	CONSTRAINT [FK_RequestType] FOREIGN KEY ([RequestType]) REFERENCES [RequestType]([RequestTypeId]),	
	CommunicationType int ,
	CONSTRAINT [FK_DetailsCommunicationType] FOREIGN KEY ([CommunicationType]) REFERENCES [CommunicationType]([CommunicationTypeId]),
	GUIDInteracting uniqueidentifier not null  , --сделать констрейн  на  два пол€ ?
)
