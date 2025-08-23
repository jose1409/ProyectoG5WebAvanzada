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
        u.idUsuario,
        u.cedula,
        u.nombre,
        u.apellidos,
        u.CorreoElectronico,
        u.telefono,
        u.fotografia,
        u.activo,
        r.nombre AS Rol
    FROM dbo.Usuario u
    INNER JOIN dbo.Rol r ON u.id_rol = r.id_rol
    WHERE u.CorreoElectronico = @CorreoElectronico
      AND u.Contrasenna = @Contrasenna;
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
-- Listar Productos
-- =============================================
IF OBJECT_ID('ObtenerProductos', 'P') IS NOT NULL 
    DROP PROCEDURE ObtenerProductos;
GO

CREATE PROCEDURE [dbo].[ObtenerProductos]
AS
BEGIN
    SELECT 
        id_producto   AS IdProducto,
        id_categoria  AS IdCategoria,
        descripcion   AS Descripcion,
        detalle       AS Detalle,
        precio        AS Precio,
        existencias   AS Existencias,
        ruta_imagen   AS RutaImagen,
        activo        AS Activo
    FROM Producto;
END
GO

-- =============================================
-- Editar Productos
-- =============================================
IF OBJECT_ID('EditarProducto', 'P') IS NOT NULL DROP PROCEDURE EditarProducto;
GO
CREATE PROCEDURE EditarProducto
    @IdProducto   INT,
    @IdCategoria  INT,
    @Descripcion  VARCHAR(1000),
    @Detalle      VARCHAR(2000),
    @RutaImagen   VARBINARY(MAX),
    @Precio       DECIMAL(18,2),
    @Activo       BIT
AS
BEGIN
    UPDATE producto
    SET 
        id_categoria = @IdCategoria,
        descripcion  = @Descripcion,
        detalle      = @Detalle,
        ruta_imagen  = @RutaImagen,
        precio       = @Precio,
        activo       = @Activo
    WHERE id_producto = @IdProducto;
END
GO
-- =============================================
-- Eliminar Productos
-- =============================================
IF OBJECT_ID('EliminarProducto', 'P') IS NOT NULL DROP PROCEDURE EliminarProducto;
GO
CREATE PROCEDURE EliminarProducto
	@idProducto int
AS
BEGIN
    Delete Producto
	Where id_producto = @idProducto
END 
-- =============================================
-- Crear Productos y devolver objeto con id
-- =============================================
IF OBJECT_ID('CrearProducto', 'P') IS NOT NULL DROP PROCEDURE CrearProducto;
GO
CREATE PROCEDURE CrearProducto
    @Descripcion VARCHAR(1000),
    @Detalle VARCHAR(2000),
    @Precio DECIMAL(18,2),
    @IdCategoria INT,
    @RutaImagen VARBINARY(MAX),
    @Activo BIT
AS
BEGIN
    -- Insertar el producto
    INSERT INTO Producto (Descripcion, Detalle, Precio, id_categoria, ruta_imagen, Activo, existencias)
    VALUES (@Descripcion, @Detalle, @Precio, @IdCategoria, @RutaImagen, @Activo, 0);

    -- Retornar el producto recién insertado
    SELECT 
        Id_Producto AS IdProducto,
        Descripcion,
        Detalle,
        Precio,
        id_categoria,
        ruta_imagen,
        Activo,
		existencias
    FROM Producto
    WHERE Id_Producto = SCOPE_IDENTITY();
END
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
    SELECT TOP 1 * 
    FROM CompanyInfo
    ORDER BY Id DESC;
END
GO

-- Obtener miembros del equipo
CREATE OR ALTER PROCEDURE sp_GetTeamMembers
AS
BEGIN
    SELECT * 
    FROM TeamMember
    ORDER BY Id ASC;
END
GO



-- =============================================
-- Edita la categoria x Id
-- =============================================
IF OBJECT_ID('EditarCategoria', 'P') IS NOT NULL DROP PROCEDURE EditarCategoria;
GO

CREATE PROCEDURE EditarCategoria
	@IdCategoria int,
	@Descripcion varchar(1000),
	@Imagen varbinary(MAX),
	@Activo bit
AS
BEGIN
    Update Categoria SET
	descripcion = @Descripcion,
	ruta_imagen = @Imagen,
	activo = @Activo
	Where id_categoria = @IdCategoria
END 

-- =============================================
-- Elimina la categoria x Id (verificar primero la vinculacion de productos) si existen, no se puede eliminar
-- =============================================
IF OBJECT_ID('EliminarCategoria', 'P') IS NOT NULL DROP PROCEDURE EliminarCategoria;
GO
CREATE PROCEDURE EliminarCategoria
    @id_categoria INT
AS
BEGIN
    -- Verificamos si la categoría tiene productos asociados
    IF EXISTS (SELECT 1 FROM Producto WHERE id_categoria = @id_categoria)
    BEGIN
        RETURN;
    END
    DELETE FROM Categoria
    WHERE id_categoria = @id_categoria;
END

-- =============================================
-- Obtener categoria x nombre
-- =============================================
IF OBJECT_ID('ObtenerPorNombreCategoria', 'P') IS NOT NULL DROP PROCEDURE ObtenerPorNombreCategoria;
GO
CREATE PROCEDURE ObtenerPorNombreCategoria
    @descripcion VARCHAR(1000)
AS
BEGIN
    SELECT * 
    FROM Categoria
    WHERE LOWER(descripcion) LIKE '%' + LOWER(@descripcion) + '%'
END


-- =============================================
-- Crear Categoria y devolver objeto con id
-- =============================================
IF OBJECT_ID('CrearCategoria', 'P') IS NOT NULL DROP PROCEDURE CrearCategoria;
GO
CREATE PROCEDURE CrearCategoria
    @descripcion VARCHAR(1000),
    @ruta_imagen VARBINARY(MAX),
    @activo BIT
AS
BEGIN
    INSERT INTO Categoria (descripcion, ruta_imagen, activo)
    VALUES (@descripcion, @ruta_imagen, @activo)

    -- Retornar la nueva categoría insertada (con su ID)
    SELECT 
	descripcion AS Descripcion,
	ruta_imagen AS Imagen,
	activo AS Activo

	FROM Categoria
    WHERE id_categoria = SCOPE_IDENTITY()
END

-- =============================================
-- Listar todas las Categorias
-- =============================================
IF OBJECT_ID('ObtenerCategorias', 'P') IS NOT NULL DROP PROCEDURE ObtenerCategorias;
GO
CREATE PROCEDURE ObtenerCategorias
AS
BEGIN
	SELECT 
    id_categoria AS IdCategoria,
    descripcion,
    ruta_imagen AS Imagen,
    activo
	FROM Categoria
END
GO

-- =============================================
-- Agregar producto al carrito
-- =============================================
CREATE PROCEDURE AgregarProductoAlCarrito
    @IdUsuario INT,
    @IdProducto INT,
    @Cantidad INT
AS
BEGIN
    DECLARE @IdCarrito INT;

    -- Buscar carrito existente del usuario
    SELECT @IdCarrito = IdCarrito FROM Carrito WHERE IdUsuario = @IdUsuario;

    -- Si no existe, crearlo
    IF @IdCarrito IS NULL
    BEGIN
        INSERT INTO Carrito (IdUsuario) VALUES (@IdUsuario);
        SET @IdCarrito = SCOPE_IDENTITY();
    END

    -- Insertar o actualizar producto en el carrito
    IF EXISTS (
        SELECT 1 FROM CarritoProducto
        WHERE IdCarrito = @IdCarrito AND IdProducto = @IdProducto
    )
    BEGIN
        UPDATE CarritoProducto
        SET Cantidad = Cantidad + @Cantidad
        WHERE IdCarrito = @IdCarrito AND IdProducto = @IdProducto;
    END
    ELSE
    BEGIN
        INSERT INTO CarritoProducto (IdCarrito, IdProducto, Cantidad)
        VALUES (@IdCarrito, @IdProducto, @Cantidad);
    END
END;
GO

-- =============================================
-- Crear orden desde carrito
-- =============================================
CREATE PROCEDURE CrearOrdenDesdeCarrito
    @IdUsuario INT
AS
BEGIN
    DECLARE @IdCarrito INT;
    DECLARE @Total FLOAT = 0;

    SELECT @IdCarrito = IdCarrito FROM Carrito WHERE IdUsuario = @IdUsuario;

    IF @IdCarrito IS NULL
    BEGIN
        RAISERROR('El usuario no tiene un carrito.', 16, 1);
        RETURN;
    END

    -- Calcular total
    SELECT @Total = SUM(cp.Cantidad * p.precio)
    FROM CarritoProducto cp
    JOIN Producto p ON cp.IdProducto = p.id_producto
    WHERE cp.IdCarrito = @IdCarrito;

    -- Crear orden
    INSERT INTO Orden (IdUsuario, Total, Estado)
    VALUES (@IdUsuario, @Total, 'Pendiente');

    DECLARE @IdOrden INT = SCOPE_IDENTITY();

    -- Insertar detalles
    INSERT INTO OrdenDetalle (IdOrden, IdProducto, Cantidad, PrecioUnitario)
    SELECT @IdOrden, IdProducto, Cantidad, p.precio
    FROM CarritoProducto cp
    JOIN Producto p ON cp.IdProducto = p.id_producto
    WHERE cp.IdCarrito = @IdCarrito;

    -- Limpiar carrito
    DELETE FROM CarritoProducto WHERE IdCarrito = @IdCarrito;
    DELETE FROM Carrito WHERE IdCarrito = @IdCarrito;
END;
GO