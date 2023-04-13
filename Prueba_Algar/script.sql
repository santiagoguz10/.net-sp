CREATE DATABASE DbSisVentas;

USE DbSisVentas;

CREATE TABLE Venta(
	Id INT PRIMARY KEY IDENTITY,
	CedulaCliente int,
	DireccionCliente VARCHAR(200),
	IdProducto int,
	FOREIGN KEY (IdProducto) REFERENCES Producto(Id)
);

CREATE TABLE Producto(
	Id INT PRIMARY KEY IDENTITY,
	NombreProducto VARCHAR(200)

)

INSERT INTO Producto(NombreProducto)
VALUES('Cafe'),('Pan'),('Arroz')


-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE DeleteProduct
	-- Add the parameters for the stored procedure here
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE Producto
	WHERE Id= @Id
END
GO



-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Santiago Guzman
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE Sp_CrearProducto 
	-- Add the parameters for the stored procedure here
	@jsonVenta VARCHAR(MAX),
	@cedula int,
	@direccion varchar(200)

AS
BEGIN
	
	SET NOCOUNT ON;
	 
	 DECLARE @tablaVenta AS TABLE
	 (
		CedulaCliente int,
		DireccionCliente VARCHAR(200),
		IdProducto int
	 )

	

	 BEGIN
	INSERT INTO @tablaVenta
	SELECT @cedula,
		   @direccion,
			id
		FROM OPENJSON(@jsonVenta) WITH (
			id int
    );

	END

	SELECT * FROM @tablaVenta

	INSERT INTO Venta (CedulaCliente, DireccionCliente,IdProducto)
	SELECT CedulaCliente,
		   DireccionCliente,
		   IdProducto FROM @tablaVenta

END
GO


-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Sp_ListaProductos 
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Producto

END
GO
