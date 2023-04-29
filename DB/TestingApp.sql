CREATE DATABASE TestingApp


USE TestingApp

--“аблица тест
-- код теста, название и описание
CREATE TABLE Test
(
TestId INT PRIMARY KEY,
TestName VARCHAR(30),
TestTime INT,
TestDescription VARCHAR(3000)
)
SELECT * FROM Test
--“аблица вопрос
CREATE TABLE Question
(
IdTest INT FOREIGN KEY REFERENCES Test(TestId),
QuestionId INT PRIMARY KEY,
QuestionNumber INT,
QuestionDescription VARCHAR(300)
)

--“аблица варианты ќтвета
--код варианта, код вопроса (к какому вопросу это вариант ответа) и текст варианта ответа
CREATE TABLE OptionAnswer
(
AnswerId INT PRIMARY KEY,
IdQuestion INT FOREIGN KEY REFERENCES Question(QuestionId),
AnswerDescription VARCHAR(300)
)

--“аблица с возможными результатами
--код теста (в каком тесте может быть такой результат), код результата и текст результата
CREATE TABLE Result
(
IdTest INT FOREIGN KEY REFERENCES Test(TestId),
ResultId INT PRIMARY KEY,
ResultDescription VARCHAR(300)
)

--ѕереходна€
CREATE TABLE Temp
(
IdAnswer INT FOREIGN KEY REFERENCES OptionAnswer(AnswerId),
IdResult INT FOREIGN KEY REFERENCES Result(ResultId),
CountScore DECIMAL(6, 2)
)
--DROP TABLE Temp
SELECT * FROM Question
--WHERE IdTest = 2
SELECT * FROM OptionAnswer
--Where IdQuestion > 6
SELECT * FROM Result
--WHERE IdTest = 2
SELECT * FROM Temp
--Where IdAnswer > 27




INSERT Temp VALUES(35,5,10)
INSERT Temp VALUES(37,5,2)
INSERT Temp VALUES(36,5,2)



SELECT TestName, QuestionDescription
FROM Test INNER JOIN Question
ON Test.TestId = Question.IdTest



SELECT TestName, Count(QuestionDescription) as [ ол-во вопросов]
FROM Test INNER JOIN Question
ON Test.TestId = Question.IdTest
GROUP BY TestName
ORDER BY Count(QuestionDescription) DESC --DESC по убыванию, ASC по возрастанию 

SELECT TestName
FROM Test INNER JOIN Question
ON Test.TestId = Question.IdTest
GROUP BY TestName

/*
--не правильно
SELECT TestName, SUM(CountScore) AS [ ол-во "очков" за прохождение теста]
FROM Test INNER JOIN
(
Question INNER JOIN
(
OptionAnswer INNER JOIN Temp
ON OptionAnswer.AnswerId = Temp.IdAnswer
)
ON OptionAnswer.AnswerId = Temp.IdAnswer
)
ON Question.IdTest = Test.TestId
GROUP BY TestName*/

CREATE VIEW TestTotalScores 
AS SELECT TestId, SUM(CountScore) AS CountScore
FROM Test INNER JOIN
(Question INNER JOIN 
(OptionAnswer INNER JOIN Temp
ON OptionAnswer.AnswerId = Temp.IdAnswer
)
ON Question.QuestionId = OptionAnswer.IdQuestion
)
ON Test.TestId = Question.IdTest
GROUP BY TestId

Select * from Test
Select * from Question
Select * from OptionAnswer
Select * from Result
Select * from Temp


SELECT * from TestTotalScores

DELETE FROM Test WHERE TestId > 2

CREATE TABLE Users
(
UserId INT PRIMARY KEY,
UserLogin VARCHAR(30),
UserPassword VARCHAR(30),
UserName VARCHAR(30),
UserSurname VARCHAR(30),
UserStanding INT,
UserWhoRegId INT,
UserStatus BIT, --2 значени€ 1 -true, 0 - false; статус если пользователь онлайн
)
select * FROM Users where UserLogin = 'admin' and UserPassword = 'admin'
CREATE TABLE Tags
(
TagId INT PRIMARY KEY,
TagDescription VARCHAR(50)
)

CREATE TABLE UserTags 
(
IdTag INT FOREIGN KEY REFERENCES Tags(TagId),
IdUser INT FOREIGN KEY REFERENCES Users(UserId)
)

CREATE TABLE TestTags
(
IdTag INT FOREIGN KEY REFERENCES Tags(TagId),
IdTest INT FOREIGN KEY REFERENCES Test(TestId)
)

CREATE TABLE TryUsers
(
TryId INT PRIMARY KEY,
IdUser INT FOREIGN KEY REFERENCES Users(UserId),
IdTest INT FOREIGN KEY REFERENCES Test(TestId),
StartTime VARCHAR(30),
FinishTime VARCHAR(30)
)
select * from TryUsers
INSERT INTO TryUsers VALUES(5, 3, 1, 'Yes', 'NO')
UPDATE TryUsers set FinishTime = 'Yes' where TryId = 2

CREATE TABLE UserAnswer
(
IdTry INT FOREIGN KEY REFERENCES TryUsers(TryId),
IdAnswer INT FOREIGN KEY REFERENCES OptionAnswer(AnswerId)
)

select * from TryUsers
select * from UserAnswer

ALTER TABLE Test
ADD IDCreater INT FOREIGN KEY REFERENCES Users(UserId)


delete from TryUsers where TryId > 0
--то что выше добавлено, то что ниже еще нет