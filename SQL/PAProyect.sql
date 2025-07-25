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
    SELECT IdUsuario, cedula, Nombre, apellidos, CorreoElectronico, telefono, fotografia, activo
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



-- =============================================
-- Obtener todas las rutinas
-- =============================================
IF OBJECT_ID('ObtenerRutinas', 'P') IS NOT NULL DROP PROCEDURE ObtenerRutinas;
GO

CREATE PROCEDURE ObtenerRutinas
AS
BEGIN
    SELECT 
        IdRutina, 
        Nombre, 
        Descripcion, 
        Imagen
    FROM Rutina
END;
GO

-- =============================================
-- Obtener una rutina específica por ID
-- =============================================
IF OBJECT_ID('ObtenerRutinaPorId', 'P') IS NOT NULL DROP PROCEDURE ObtenerRutinaPorId;
GO

CREATE PROCEDURE ObtenerRutinaPorId
    @IdRutina INT
AS
BEGIN
    SELECT 
        r.IdRutina,
        r.Nombre,
        r.Descripcion,
        r.Imagen,
        p.id_producto AS IdProducto,
        p.descripcion,
        p.detalle,
        p.precio,
        p.ruta_imagen AS RutaImagen
    FROM Rutina r
    LEFT JOIN RutinaProducto rp ON r.IdRutina = rp.IdRutina
    LEFT JOIN Producto p ON rp.id_producto = p.id_producto
    WHERE r.IdRutina = @IdRutina
END;

-- =============================================
-- Obtener productos por rutina específica
-- =============================================
IF OBJECT_ID('ObtenerProductosPorRutina', 'P') IS NOT NULL DROP PROCEDURE ObtenerProductosPorRutina;
GO

CREATE PROCEDURE ObtenerProductosPorRutina
    @IdRutina INT
AS
BEGIN
    SELECT 
        p.id_producto,
        p.descripcion,
        p.detalle,
        p.precio,
        p.ruta_imagen
    FROM Producto p
    INNER JOIN RutinaProducto rp ON p.id_producto = rp.id_producto
    WHERE rp.IdRutina = @IdRutina
END;
GO

-- =============================================
-- Insertar una nueva rutina
-- =============================================
IF OBJECT_ID('InsertarRutina', 'P') IS NOT NULL DROP PROCEDURE InsertarRutina;
GO

CREATE PROCEDURE InsertarRutina
    @Nombre NVARCHAR(100),
    @Descripcion NVARCHAR(255),
    @Imagen NVARCHAR(255)
AS
BEGIN
    INSERT INTO Rutina (Nombre, Descripcion, Imagen)
    VALUES (@Nombre, @Descripcion, @Imagen)
END;
GO

-- =============================================
-- Asociar producto a rutina
-- =============================================
IF OBJECT_ID('InsertarProductoEnRutina', 'P') IS NOT NULL DROP PROCEDURE InsertarProductoEnRutina;
GO

CREATE PROCEDURE InsertarProductoEnRutina
    @IdRutina INT,
    @id_producto INT
AS
BEGIN
    INSERT INTO RutinaProducto (IdRutina, id_producto)
    VALUES (@IdRutina, @id_producto)
END;
GO

-- =============================================
-- Actualizar una rutina existente
-- =============================================
IF OBJECT_ID('ActualizarRutina', 'P') IS NOT NULL DROP PROCEDURE ActualizarRutina;
GO

CREATE PROCEDURE ActualizarRutina
    @IdRutina INT,
    @Nombre NVARCHAR(100),
    @Descripcion NVARCHAR(255),
    @Imagen NVARCHAR(255)
AS
BEGIN
    UPDATE Rutina
    SET Nombre = @Nombre,
        Descripcion = @Descripcion,
        Imagen = @Imagen
    WHERE IdRutina = @IdRutina
END;
GO

-- =============================================
-- Eliminar una rutina y sus productos asociados
-- =============================================
IF OBJECT_ID('EliminarRutina', 'P') IS NOT NULL DROP PROCEDURE EliminarRutina;
GO

CREATE PROCEDURE EliminarRutina
    @IdRutina INT
AS
BEGIN
    DELETE FROM RutinaProducto WHERE IdRutina = @IdRutina;
    DELETE FROM Rutina WHERE IdRutina = @IdRutina;
END;
GO


-- Obtener la información de la empresa
CREATE OR ALTER PROCEDURE sp_GetCompanyInfo
AS
BEGIN
    SELECT TOP 1 * FROM CompanyInfo WHERE IsActive = 1 ORDER BY Id DESC;
END
GO

-- Obtener miembros activos del equipo
CREATE OR ALTER PROCEDURE sp_GetTeamMembers
AS
BEGIN
    SELECT * FROM TeamMember WHERE IsActive = 1 ORDER BY DisplayOrder;
END
GO