CREATE TRIGGER ActualizaCantidadPresupusto
ON Gastos
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    DECLARE @IdPresupuesto INT;
    DECLARE @Cantidad DECIMAL(10,2);
    DECLARE @TipoMovimiento CHAR(1);
    -- VARIABLES --
    SELECT @IdPresupuesto = IdPresupuesto, 
           @Cantidad = Cantidad
    FROM INSERTED;
	    SELECT @IdPresupuesto = IdPresupuesto, 
           @Cantidad = Cantidad
    FROM UPDATED;
		    SELECT @IdPresupuesto = IdPresupuesto, 
           @Cantidad = Cantidad
    FROM DELETED;
    -- ACCION
    BEGIN
        UPDATE Presupuestos
        SET DineroActual = DineroActual + @Cantidad
        WHERE IdPresupuesto = @IdPresupuesto;
    END
END;