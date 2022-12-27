USE AVALIAE;

--SELECT PARA INSTITUIÇÃO
SELECT InstitutionName, UserModelId, [Name], [Status] FROM [USER] AS U
INNER JOIN [Associations] AS A
INNER JOIN [Institution] AS I
ON A.InstitutionModelId = I.Id
ON A.UserModelId = U.Id
WHERE A.UserModelId = 1;
GO

update Associations set Status = 'P' where Id = 1;