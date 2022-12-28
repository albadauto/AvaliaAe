SELECT * FROM DOCUMENTATIONS AS D
INNER JOIN Associations AS A
ON A.Id = D.AssociationsId
INNER JOIN [USER] AS U
ON U.Id = A.UserModelId
INNER JOIN [Institution] AS I
ON I.Id = A.InstitutionModelId

SELECT * FROM [Documentations]

INSERT INTO Documentations VALUES (1, '/wwwroot/seupai');