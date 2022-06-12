CREATE DATABASE [testedb]
GO

USE [testedb];
GO

CREATE TABLE Teste (
	Id INT NOT NULL IDENTITY,
	Descricao varchar(100) NOT NULL,
	DataCriacao DATETIME NOT NULL,
    DataAlteracao DATETIME,
	PRIMARY KEY (Id)
);
GO