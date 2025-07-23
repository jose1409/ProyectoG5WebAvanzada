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

CREATE PROCEDURE GetUserProfileData
	@IdUsuario BIGINT
AS
BEGIN
	SELECT	IdUsuario,
			cedula,
			Nombre,
			CorreoElectronico,
			telefono,
			fotografia,
			activo
	  FROM	dbo.Usuario
	WHERE	IdUsuario = @IdUsuario
END

CREATE PROCEDURE UpdateUserProfileData
	@IdUsuario int,
    @Cedula varchar(20),
	@Nombre varchar(100),
	@Apellidos varchar(100),
	@CorreoElectronico varchar(100),
	@Telefono varchar(20),
	@Fotografia varbinary(max)
AS
BEGIN

	IF NOT EXISTS(SELECT 1 FROM Usuario
				  WHERE cedula = @Cedula
					AND	CorreoElectronico = @CorreoElectronico
					AND IdUsuario != @IdUsuario)
	BEGIN

		UPDATE dbo.Usuario
		SET cedula = @Cedula,
			Nombre = @Nombre,
			apellidos = @Apellidos,
			CorreoElectronico = @CorreoElectronico,
			telefono = @Telefono,
			fotografia = @Fotografia
		WHERE IdUsuario = @IdUsuario
	END
END

CREATE PROCEDURE [dbo].[ValidarCorreo]
    @CorreoElectronico VARCHAR(100)
AS
BEGIN
    SELECT CorreoElectronico, idUsuario
    FROM dbo.Usuario
    WHERE CorreoElectronico = @CorreoElectronico
END

CREATE PROCEDURE [dbo].[ActualizarContrasenna]
    @IdUsuario INT,
    @Contrasenna VARCHAR(255)
AS
BEGIN
    UPDATE dbo.Usuario
    SET Contrasenna = @Contrasenna
    WHERE IdUsuario = @IdUsuario
END
