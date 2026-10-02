-- Esquema INFERIDO a partir de las entidades y configuraciones de EF Core.
-- Si tu BD ya existe, verifica que "comment.Id" y "post.Id" sean AUTO_INCREMENT.
CREATE DATABASE IF NOT EXISTS DbSocialMedia;
USE DbSocialMedia;

CREATE TABLE IF NOT EXISTS `user` (
  Id INT NOT NULL AUTO_INCREMENT,
  FirstName VARCHAR(50) NOT NULL,
  LastName VARCHAR(50) NOT NULL,
  Email VARCHAR(30) NOT NULL,
  DateOfBirth DATE NOT NULL,
  Telephone VARCHAR(10) NULL,
  IsActive BIT(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (Id)
);

CREATE TABLE IF NOT EXISTS post (
  Id INT NOT NULL AUTO_INCREMENT,
  UserId INT NOT NULL,
  Date DATETIME NOT NULL,
  Description VARCHAR(1000) NOT NULL,
  Imagen VARCHAR(500) NULL,
  PRIMARY KEY (Id),
  KEY FK_Post_User (UserId),
  CONSTRAINT FK_Post_User FOREIGN KEY (UserId) REFERENCES `user` (Id)
);

CREATE TABLE IF NOT EXISTS comment (
  Id INT NOT NULL AUTO_INCREMENT,
  PostId INT NOT NULL,
  UserId INT NOT NULL,
  Description VARCHAR(500) NOT NULL,
  Date DATETIME NOT NULL,
  IsActive BIT(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (Id),
  KEY FK_Comment_Post (PostId),
  KEY FK_Comment_User (UserId),
  CONSTRAINT FK_Comment_Post FOREIGN KEY (PostId) REFERENCES post (Id),
  CONSTRAINT FK_Comment_User FOREIGN KEY (UserId) REFERENCES `user` (Id)
);

-- Datos de prueba
INSERT INTO `user` (FirstName, LastName, Email, DateOfBirth, Telephone, IsActive)
VALUES ('Samuel', 'Laubner', 'samuel@ucb.edu', '2000-01-01', '70000000', b'1');
