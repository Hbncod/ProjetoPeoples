-- Cria o banco de dados
CREATE DATABASE Peoples;

-- Define qual banco de dados será utilizado
USE Peoples;

-- Cria a tabela Funcionarios
CREATE TABLE Funcionarios 
(
	IdFuncionario	INT IDENTITY PRIMARY KEY
	,Nome			VARCHAR(200) NOT NULL
	,Sobrenome		VARCHAR(255)
);
GO

-- Adiciona a coluna DataNascimento na tabela Funcionarios
ALTER TABLE Funcionarios
ADD DataNascimento DATE
CREATE TABLE TipoUsuario(
	Id_TipoUsuario	INT PRIMARY KEY IDENTITY,
	Tipo			VARCHAR(30)
);

CREATE TABLE Usuarios(
	Id_Usuario		INT PRIMARY KEY IDENTITY,
	Email			VARCHAR(100) UNIQUE NOT NULL,
	Senha			VARCHAR(100) NOT NULL,
	Fk_TipoUsuario	INT FOREIGN KEY REFERENCES TipoUsuario(Id_TipoUsuario)
);

Select * from Usuarios
INSERT INTO TipoUsuario(Tipo)
Values	('Adm'),
		('Comum');




INSERT INTO Usuarios(Email,Senha,Fk_TipoUsuario)
VALUES	('hbncod@gmail.com','12345',1),
		('henrique@gmail.com','12345',2),
		('joana@gmail.com','12345',2);

	