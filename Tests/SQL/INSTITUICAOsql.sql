USE AVALIAE;

--SELECT PARA INSTITUIÇÃO
SELECT InstitutionName, UserModelId, Name FROM [USER] AS U
INNER JOIN [Associations] AS A
INNER JOIN [Institution] AS I
ON A.InstitutionModelId = I.Id
ON A.UserModelId = U.Id;
GO