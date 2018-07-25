create table [User] (
Id uniqueidentifier primary key,
[Name] varchar(150),
Email varchar(150)not null,
[Password] varchar(300) not null);

create table Score(
Id uniqueidentifier primary key,
ScorePoint int not null,
GameId int not null,
UserId uniqueidentifier not null,
Foreign key (UserId) references [User](Id)
);

select * from [User]