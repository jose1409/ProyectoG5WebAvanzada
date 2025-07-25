USE bdBoteroBeauty;
GO

-- Procedimiento: RegistrarUsuario
IF OBJECT_ID('[dbo].[RegistrarUsuario]', 'P') IS NOT NULL
    DROP PROCEDURE [dbo].[RegistrarUsuario];
GO

CREATE PROCEDURE [dbo].[RegistrarUsuario]
    @CorreoElectronico varchar(100),
    @Contrasenna varchar(255),
    @cedula varchar(20),
    @Nombre varchar(100),
    @Apellidos VARCHAR(100)
AS
BEGIN
    IF NOT EXISTS(SELECT 1 FROM dbo.Usuario WHERE CorreoElectronico = @CorreoElectronico)
    BEGIN
        INSERT INTO dbo.Usuario(CorreoElectronico, Contrasenna, cedula, nombre, Apellidos, activo)
        VALUES (@CorreoElectronico, @Contrasenna, @cedula, @Nombre, @Apellidos, 1)
    END
END
GO

-- Procedimiento: Login
IF OBJECT_ID('[dbo].[Login]', 'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Login];
GO

CREATE PROCEDURE [dbo].[Login]
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
GO

-- Procedimiento: GetUserProfileData
IF OBJECT_ID('[dbo].[GetUserProfileData]', 'P') IS NOT NULL
    DROP PROCEDURE [dbo].[GetUserProfileData];
GO

CREATE PROCEDURE [dbo].[GetUserProfileData]
    @IdUsuario BIGINT
AS
BEGIN
    SELECT IdUsuario, cedula, Nombre, CorreoElectronico, telefono, fotografia, activo
    FROM dbo.Usuario
    WHERE IdUsuario = @IdUsuario
END
GO

-- Procedimiento: UpdateUserProfileData
IF OBJECT_ID('[dbo].[UpdateUserProfileData]', 'P') IS NOT NULL
    DROP PROCEDURE [dbo].[UpdateUserProfileData];
GO

CREATE PROCEDURE [dbo].[UpdateUserProfileData]
    @IdUsuario int,
    @Cedula varchar(20),
    @Nombre varchar(100),
    @Apellidos varchar(100),
    @CorreoElectronico varchar(100),
    @Telefono varchar(20),
    @Fotografia varbinary(max)
AS
BEGIN
    IF NOT EXISTS(SELECT 1 FROM Usuario WHERE cedula = @Cedula AND CorreoElectronico = @CorreoElectronico AND IdUsuario != @IdUsuario)
    BEGIN
        UPDATE dbo.Usuario
        SET cedula = @Cedula,
            Nombre = @Nombre,
            Apellidos = @Apellidos,
            CorreoElectronico = @CorreoElectronico,
            telefono = @Telefono,
            fotografia = @Fotografia
        WHERE IdUsuario = @IdUsuario
    END
END
GO

-- Procedimiento: ValidarCorreo
IF OBJECT_ID('[dbo].[ValidarCorreo]', 'P') IS NOT NULL
    DROP PROCEDURE [dbo].[ValidarCorreo];
GO

CREATE PROCEDURE [dbo].[ValidarCorreo]
    @CorreoElectronico VARCHAR(100)
AS
BEGIN
    SELECT CorreoElectronico, idUsuario
    FROM dbo.Usuario
    WHERE CorreoElectronico = @CorreoElectronico
END
GO

-- Procedimiento: ActualizarContrasenna
IF OBJECT_ID('[dbo].[ActualizarContrasenna]', 'P') IS NOT NULL
    DROP PROCEDURE [dbo].[ActualizarContrasenna];
GO

CREATE PROCEDURE [dbo].[ActualizarContrasenna]
    @IdUsuario INT,
    @Contrasenna VARCHAR(255)
AS
BEGIN
    UPDATE dbo.Usuario
    SET Contrasenna = @Contrasenna
    WHERE IdUsuario = @IdUsuario
END
GO