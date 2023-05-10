ALTER PROCEDURE [dbo].[sp_Nota_All]
AS
BEGIN  
   SELECT N.IdNota , A.IdAlumno  , A.Nombres as Alumno,C.IdCurso, C.Nombre as Curso, N.Nota
   FROM Nota N  WITH (NOLOCK)
   INNER JOIN ALUMNO A WITH (NOLOCK) ON A.IdAlumno = N.IdAlumno
   INNER JOIN CURSO C WITH (NOLOCK) ON C.IdCurso = N.IdCurso
END

-- EXEC [sp_Nota_All]

ALTER PROCEDURE [dbo].[sp_Nota_ById]
@IdNota integer
AS
BEGIN  
   SELECT N.IdNota , A.IdAlumno , A.Nombres as Alumno,C.IdCurso, C.Nombre as Curso, N.Nota
   FROM Nota N  WITH (NOLOCK)
   INNER JOIN ALUMNO A WITH (NOLOCK) ON A.IdAlumno = N.IdAlumno
   INNER JOIN CURSO C WITH (NOLOCK) ON C.IdCurso = N.IdCurso
    WHERE IdNota = @IdNota
END

-- EXEC [sp_Nota_ById] 1

ALTER PROCEDURE [dbo].[sp_Nota_Add]
@IdAlumno int ,
@IdCurso int ,
@Nota int
AS
BEGIN  
    BEGIN TRY
        BEGIN TRANSACTION 
                INSERT INTO [Nota]
		        VALUES (@IdAlumno,@IdCurso,@Nota)
		        SELECT SCOPE_IDENTITY();
        COMMIT
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK
    END CATCH
END



-- EXEC [dbo].[sp_Nota_Add] 4, 1,18

ALTER PROCEDURE [dbo].[sp_Nota_Update]
@IdNota integer,
@Nota int
AS
BEGIN  
    BEGIN TRY
        BEGIN TRANSACTION 
                UPDATE [Nota]
		        SET [Nota] = @Nota
                WHERE [IdNota] = @IdNota
        COMMIT
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK
    END CATCH
END
 --[sp_Nota_All]
-- EXEC [sp_Nota_Update] 3 , 14


CREATE PROCEDURE [dbo].[sp_Nota_Delete]
@IdNota integer
AS
BEGIN  
    BEGIN TRY
        BEGIN TRANSACTION 
                DELETE FROM [Nota] WHERE [IdNota] = @IdNota
        COMMIT
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK
    END CATCH
END

-- EXEC [sp_Nota_Delete] 6



 -- INSERT INTO curso VALUES ('Cocina Selvatica', 'Docente')