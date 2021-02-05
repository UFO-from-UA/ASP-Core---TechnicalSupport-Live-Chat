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

--CREATE TABLE SkillLevel
--(
--	[SkillLevelId] int IDENTITY(1, 1) PRIMARY KEY not null,
--	[SkillLevel]  int not null,
--)
  

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
	FirstName NVARCHAR(100)  ,  
	LastName NVARCHAR(100)  ,  
	SecondName  NVARCHAR(100)  ,  
	Age int,
	Phone NVARCHAR(22)  ,
	Email NVARCHAR(100),
	UserIp NVARCHAR(100),
	Sex int ,
	CONSTRAINT [FK_UserSex] FOREIGN KEY ([Sex]) REFERENCES [Sex]([SexId]),
)

INSERT INTO [User] (FirstName , sex)  VALUES ('Valera' , 3);

CREATE TABLE Employee
(
	[EmployeeId] int IDENTITY(1, 1) PRIMARY KEY not null,
	FirstName NVARCHAR(100)  ,  
	LastName NVARCHAR(100)  ,  
	SecondName  NVARCHAR(100)  ,  
	Age int,
	Phone NVARCHAR(22)  ,
	Email NVARCHAR(100),
	TaskCount int  ,
	Sex int not null ,
	CONSTRAINT [FK_EmployeeSex] FOREIGN KEY ([Sex]) REFERENCES [Sex]([SexId]),
	WorkTime int  not null ,
	CONSTRAINT [FK_WorkTime] FOREIGN KEY ([WorkTime]) REFERENCES [WorkTime]([WorkTimeId]),
)

INSERT INTO Employee VALUES ('Грозный','Иван','Царевич',23,'+1234567823','a@a.a',1,2,4);

--select * from Employee

CREATE TABLE [Application]
(
	[ApplicationId] int IDENTITY(1, 1) PRIMARY KEY not null,
	Topic NVARCHAR(150) not null,  
	[User] int not null ,
	CONSTRAINT [FK_User] FOREIGN KEY ([User]) REFERENCES [User]([UserId]),
	[Status] int  not null , -- mayby do defaul = 1 (NEW)
	CONSTRAINT [FK_Status] FOREIGN KEY ([Status]) REFERENCES [Status]([StatusId]),
)

INSERT INTO [Application] VALUES ('What is thet?',1,2,2);
--select * from [Application]

CREATE TABLE Task
(
	[TaskId] int IDENTITY(1, 1) PRIMARY KEY not null,
	Task NVARCHAR(150),   -- Пока не  понятно  что  тут
	[Employee] int  , 
	CONSTRAINT [FK_TaskEmployee] FOREIGN KEY ([Employee]) REFERENCES [Employee]([EmployeeId]),
)




CREATE TABLE Details
(
	[DetailsId] int IDENTITY(1, 1) PRIMARY KEY not null,
	[Timestamp] DATETIME default CURRENT_TIMESTAMP,
	[Data] NVARCHAR(1000) , 
	[Application] int ,
	CONSTRAINT [FK_Application] FOREIGN KEY ([Application]) REFERENCES [Application]([ApplicationId]),
	RequestType int ,
	CONSTRAINT [FK_RequestType] FOREIGN KEY ([RequestType]) REFERENCES [RequestType]([RequestTypeId]),	
	CommunicationType int ,
	CONSTRAINT [FK_DetailsCommunicationType] FOREIGN KEY ([CommunicationType]) REFERENCES [CommunicationType]([CommunicationTypeId]),
)


CREATE TABLE AnswerDetails
(
	[AnswerDetailsId] int IDENTITY(1, 1) PRIMARY KEY not null,
	[Timestamp] DATETIME default CURRENT_TIMESTAMP,
	[Data] NVARCHAR(1000)  , 
	Employee int ,
	CONSTRAINT [FK_AnswerEmployee] FOREIGN KEY ([Employee]) REFERENCES [Employee]([EmployeeId]),
	CommunicationType int ,
	CONSTRAINT [FK_CommunicationType] FOREIGN KEY ([CommunicationType]) REFERENCES [CommunicationType]([CommunicationTypeId]),	
	Details int ,
	CONSTRAINT [FK_Details] FOREIGN KEY ([Details]) REFERENCES [Details]([DetailsId]),
)