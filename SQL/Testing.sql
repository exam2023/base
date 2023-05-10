--SET STATISTICS TIME ON
DECLARE @MIN INT, @MAX INT;
SET @MIN = 1
SET @MAX = 1000000

-- 4651800
-- 4653290
/**
[sp_Alumno_Add]
200			: 00.02
1500		: 01:11

[sp_Alumno_Add_V2]
200			: 00.02
1500		: 02:14

[sp_Alumno_Add_V3]
200			: 00.01
1500		: 00.02

[sp_Alumno_Add_V4]
200			: 00.01
1500		: 00.02
93900		: 00.14
100000		: 00.16
500000		: 01.41
1000000		: 08.25
*/
WHILE @MAX>=@MIN
BEGIN
	EXEC [dbo].[sp_Alumno_Add_V3] 'Jacob', 'Brown', 'Prueba', '98586525', 'Direccion', '987589658'
	SET @MIN = @MIN + 1

END
-- SELECT @MIN, @MAX RETURN
 SELECT MAX(IdAlumno) FROM Alumno WITH(NOLOCK)
-- 4659500
-- 4661000
-- 