﻿USE avaliae;
--Procedure para deixar todos as associações aprovadas
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.SETALLASSOCIATIONSINAPROVED'))
BEGIN 
	DROP PROCEDURE dbo.SETALLASSOCIATIONSINAPROVED;
END
GO

CREATE PROCEDURE SETALLASSOCIATIONSINAPROVED
AS 
BEGIN
	IF EXISTS(SELECT * FROM Associations)
	BEGIN
		UPDATE [Associations] SET STATUS = 'A'
	END
		
END;
--Procedure para deixar todas as associações desaprovadas
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.SETALLASSOCIATIONINPENDENT'))
BEGIN 
	DROP PROCEDURE dbo.SETALLASSOCIATIONINPENDENT
END
GO

CREATE PROCEDURE SETALLASSOCIATIONINPENDENT
AS 
BEGIN
	IF EXISTS(SELECT * FROM Associations)
	BEGIN
		UPDATE [Associations] SET STATUS = 'P'
	END
END;

 --Procedure feita para setar status de um usuário na tabela association
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.SETASSOCIATIONOFUSER'))
BEGIN
	DROP PROCEDURE dbo.SETASSOCIATIONOFUSER
END
GO

CREATE PROCEDURE SETASSOCIATIONOFUSER(@UserId int, @Status varchar(1))
AS
BEGIN
	IF EXISTS(SELECT * FROM [User] WHERE Id = @UserId)
	BEGIN
		UPDATE [Associations] SET STATUS = @Status WHERE UserId = @UserId
	END
	SELECT * FROM [Associations] WHERE UserId = @UserId;
END
GO

--Procedure para deixar todos as associações aprovadas
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.ADDINSTITUTIONSTYPE'))
BEGIN 
	DROP PROCEDURE dbo.ADDINSTITUTIONSTYPE;
END
GO

CREATE PROCEDURE ADDINSTITUTIONSTYPE
AS 
BEGIN
	INSERT INTO 
		INSTITUTIONTYPE 
	VALUES 
	('ESCOLA'), 
	('ESCOLA TÉCNICA'), 
	('ESCOLA PARTICULAR'), 
	('FACULDADE PARTICULAR'), 
	('FACULDADE PÚBLICA'), 
	('ESCOLA INFANTIL'),
	('OUTROS')
		
END
GO


