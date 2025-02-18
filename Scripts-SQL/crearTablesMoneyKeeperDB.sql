USE MoneyKeeperBD;

CREATE TABLE Usuario (
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    Correo NVARCHAR(255) UNIQUE NOT NULL,
    Contrasena NVARCHAR(255) NOT NULL,
    DNI NVARCHAR(9) UNIQUE NOT NULL,
    FecNacimiento DATE NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Categoria (
    IdCategoria INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255)
);

CREATE TABLE Transaccion (
    IdTransaccion INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL,
    IdCategoria INT NOT NULL,
    Cantidad DECIMAL(10,2) NOT NULL,
    TipoMovimiento CHAR(1) NOT NULL CHECK (TipoMovimiento = 'I' OR TipoMovimiento = 'G'),
    Descripcion NVARCHAR(255),
    FecTransaccion DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario) ON DELETE CASCADE,
    FOREIGN KEY (IdCategoria) REFERENCES Categoria(IdCategoria)
);

CREATE TABLE Cuenta (
    IdCuenta INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL,
    Dinero DECIMAL(10,2) NOT NULL,
    FecCreacion DATETIME DEFAULT GETDATE(),
    Activo BIT DEFAULT 1,
    FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario) ON DELETE CASCADE
);

CREATE TABLE Recibos (
    IdRecibo INT IDENTITY(1,1) PRIMARY KEY,
    IdCuenta INT NOT NULL,
	IdUsuario INT NOT NULL,
    Dinero DECIMAL(10,2) NOT NULL,
    FecRecibo DATETIME DEFAULT GETDATE(),
    Activo BIT DEFAULT 1,
    FOREIGN KEY (IdCuenta) REFERENCES Cuenta(IdCuenta) ON DELETE CASCADE,
    FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
);

CREATE TABLE MetaAhorro (
    IdMeta INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255),
    DineroObjetivo DECIMAL(10,2) NOT NULL,
    DineroActual DECIMAL(10,2) DEFAULT 0,
    Activo BIT DEFAULT 1,
	FecCreacion DATETIME DEFAULT GETDATE(),
	FecObjetivo DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario) ON DELETE CASCADE
);