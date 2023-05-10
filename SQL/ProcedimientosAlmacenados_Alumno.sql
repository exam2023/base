ALTER PROCEDURE [dbo].[sp_Alumno_All]
AS
BEGIN  
   SELECT TOP 100 IdAlumno,Nombres,ApellidoPaterno,ApellidoMaterno,DNI,Direccion,Telefono FROM Alumno  WITH (NOLOCK)
END

-- sp_Alumno_All 

CREATE PROCEDURE [dbo].[sp_Alumno_ById]
@IdAlumno integer
AS
BEGIN  
   SELECT  IdAlumno,Nombres,ApellidoPaterno,ApellidoMaterno,DNI,Direccion,Telefono FROM Alumno WITH (NOLOCK) WHERE IdAlumno = @IdAlumno
END

-- EXEC [sp_Alumno_ById] 456321

CREATE PROCEDURE [dbo].[sp_Alumno_Add]
@Nombres varchar(80),
@ApellidoPaterno varchar(80),
@ApellidoMaterno varchar(80),
@DNI varchar(9),
@Direccion varchar(150),
@Telefono varchar (15)
AS
BEGIN  
    BEGIN TRY
        BEGIN TRANSACTION 
                INSERT INTO [Alumno]
		        VALUES (@Nombres,@ApellidoPaterno,@ApellidoMaterno,@DNI,@Direccion,@Telefono)
		        SELECT SCOPE_IDENTITY();
        COMMIT
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK
    END CATCH
END

CREATE PROCEDURE [dbo].[sp_Alumno_Add_V2]
@Nombres varchar(80),
@ApellidoPaterno varchar(80),
@ApellidoMaterno varchar(80),
@DNI varchar(9),
@Direccion varchar(150),
@Telefono varchar (15)
AS
BEGIN  
     INSERT INTO [Alumno]
		        VALUES (@Nombres,@ApellidoPaterno,@ApellidoMaterno,@DNI,@Direccion,@Telefono)
		        SELECT SCOPE_IDENTITY();
END

CREATE PROCEDURE [dbo].[sp_Alumno_Add_V3]
@Nombres varchar(80),
@ApellidoPaterno varchar(80),
@ApellidoMaterno varchar(80),
@DNI varchar(9),
@Direccion varchar(150),
@Telefono varchar (15)
AS
BEGIN  
     INSERT INTO [Alumno]
		        VALUES (@Nombres,@ApellidoPaterno,@ApellidoMaterno,@DNI,@Direccion,@Telefono)
		        --SELECT SCOPE_IDENTITY();
END

CREATE PROCEDURE [dbo].[sp_Alumno_Add_V4]
@Nombres varchar(80),
@ApellidoPaterno varchar(80),
@ApellidoMaterno varchar(80),
@DNI varchar(9),
@Direccion varchar(150),
@Telefono varchar (15)
AS
BEGIN  
    BEGIN TRY
        BEGIN TRANSACTION 
                INSERT INTO [Alumno]
		        VALUES (@Nombres,@ApellidoPaterno,@ApellidoMaterno,@DNI,@Direccion,@Telefono)
		        --SELECT SCOPE_IDENTITY();
        COMMIT
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK
    END CATCH
END

SELECT MAX(IdAlumno) FROM Alumno WITH(NOLOCK)

-- EXEC [dbo].[sp_Alumno_Add] 'Jacob', 'Brown', 'Prueba', '98586525', 'Direccion', '987589658'

CREATE PROCEDURE [dbo].[sp_Alumno_Update]
@IdAlumno integer,
@Nombres varchar(80),
@ApellidoPaterno varchar(80),
@ApellidoMaterno varchar(80),
@DNI varchar(9),
@Direccion varchar(150),
@Telefono varchar (15)
AS
BEGIN  
    BEGIN TRY
        BEGIN TRANSACTION 
                UPDATE [Alumno]
		        SET [Nombres] = @Nombres, 
				    [ApellidoPaterno] = @ApellidoPaterno,
                    [ApellidoMaterno] = @ApellidoMaterno,
                    [DNI] = @DNI,
                    [Direccion] = @Direccion,
                    [Telefono] = @Telefono
                WHERE [IdAlumno] = @IdAlumno
        COMMIT
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK
    END CATCH
END
 
-- EXEC [sp_Alumno_Update] 1 , 'JacobUpdate', 'BrownUpdate', 'PruebaUpdate', 'dniUpdate', 'DireccionUpdate', 'phoneUpdate'


CREATE PROCEDURE [dbo].[sp_Alumno_Delete]
@IdAlumno integer
AS
BEGIN  
    BEGIN TRY
        BEGIN TRANSACTION 
                DELETE FROM [Alumno] WHERE [IdAlumno] = @IdAlumno
        COMMIT
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK
    END CATCH
END

-- EXEC [sp_Alumno_Delete] 6