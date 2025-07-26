USE master;
GO

-- Forzar desconexión y eliminar la base si existe
IF DB_ID('bdBoteroBeauty') IS NOT NULL
BEGIN
    ALTER DATABASE bdBoteroBeauty SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE bdBoteroBeauty;
END
GO

-- Crear la base de datos
CREATE DATABASE bdBoteroBeauty;
GO

USE bdBoteroBeauty;
GO

-- Crear tabla: Categoria
CREATE TABLE Categoria (
    id_categoria INT IDENTITY(1,1) PRIMARY KEY,
    descripcion VARCHAR(1000) NOT NULL,
    ruta_imagen VARCHAR(1024),
    activo BIT NOT NULL
);
GO

-- Crear tabla: Producto
CREATE TABLE Producto (
    id_producto INT IDENTITY(1,1) PRIMARY KEY,
    id_categoria INT NOT NULL,
    descripcion VARCHAR(1000) NOT NULL,
    detalle VARCHAR(1600) NOT NULL,
    precio FLOAT NOT NULL,
    existencias INT NOT NULL,
    ruta_imagen VARCHAR(1024),
    activo BIT NOT NULL,
    FOREIGN KEY (id_categoria) REFERENCES Categoria(id_categoria)
);
GO

-- Crear tabla: Usuario
CREATE TABLE Usuario (
    idUsuario INT IDENTITY(1,1) PRIMARY KEY,
    cedula VARCHAR(20),
    nombre VARCHAR(100),
    apellidos VARCHAR(100),
    CorreoElectronico VARCHAR(100) NOT NULL,
    Contrasenna VARCHAR(255) NOT NULL,
    telefono VARCHAR(20),
    fotografia VARBINARY(MAX),
    activo BIT NOT NULL DEFAULT 1
);
GO

-- Crear tabla: Rol
CREATE TABLE Rol (
    id_rol INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(20),
    id_usuario INT,
    FOREIGN KEY (id_usuario) REFERENCES Usuario(idUsuario)
);
GO

-- Crear tabla Rutina
CREATE TABLE Rutina (
    IdRutina INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255),
    Imagen NVARCHAR(1024)
);


-- Crear tabla RutinaProducto
CREATE TABLE RutinaProducto (
    IdRutina INT,
    id_producto INT,
    PRIMARY KEY (IdRutina, id_producto),
    FOREIGN KEY (IdRutina) REFERENCES Rutina(IdRutina),
    FOREIGN KEY (id_producto) REFERENCES Producto(id_producto)
);


-- Crear tabla: CompanyInfo
IF OBJECT_ID('dbo.CompanyInfo', 'U') IS NOT NULL
    DROP TABLE dbo.CompanyInfo;
GO

CREATE TABLE dbo.CompanyInfo (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Titulo NVARCHAR(150) NOT NULL,
    Descripcion NVARCHAR(MAX) NOT NULL,
    BannerUrl NVARCHAR(255) NULL
);
GO


-- Crear tabla: TeamMember
IF OBJECT_ID('dbo.TeamMember', 'U') IS NOT NULL
    DROP TABLE dbo.TeamMember;
GO
 
CREATE TABLE dbo.TeamMember (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Puesto NVARCHAR(100) NOT NULL,
    ImagenUrl NVARCHAR(255) NULL,
    FacebookUrl NVARCHAR(255) NULL,
    TwitterUrl NVARCHAR(255) NULL,
    LinkedinUrl NVARCHAR(255) NULL
);
GO