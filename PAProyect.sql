ALTER PROCEDURE [dbo].[RegistrarUsuario]
    @CorreoElectronico varchar(100),
    @Contrasenna varchar(255),
	@cedula varchar(20),
	@Nombre varchar(100),
	@Apellidos VARCHAR(100)
AS
BEGIN

	IF NOT EXISTS(SELECT 1 FROM dbo.Usuario
				  WHERE CorreoElectronico = @CorreoElectronico)
	BEGIN

		INSERT INTO dbo.Usuario(CorreoElectronico,Contrasenna,cedula,nombre,Apellidos,activo)
		VALUES (@CorreoElectronico, @Contrasenna,@cedula,@Nombre,@Apellidos, 1)
	END		
END


CREATE PROCEDURE Login
	@CorreoElectronico varchar(100),
    @Contrasenna varchar(255)
AS
BEGIN
	SELECT 
    idUsuario,
    cedula,
    nombre,
    apellidos,
    CorreoElectronico,
    telefono,
    fotografia,
    activo
FROM dbo.Usuario
WHERE CorreoElectronico = @CorreoElectronico
  AND Contrasenna = @Contrasenna;

END