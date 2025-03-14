CREATE TRIGGER ActualizarSaldoCuenta
ON Transaccion
AFTER INSERT
AS
BEGIN
    DECLARE @IdCuenta INT;
    DECLARE @Cantidad DECIMAL(10,2);
    DECLARE @TipoMovimiento CHAR(1);
    -- VARIABLES --
    SELECT @IdCuenta = IdCuenta, 
           @Cantidad = Cantidad,
           @TipoMovimiento = TipoMovimiento
    FROM INSERTED;

    -- INGRESOS
    IF @TipoMovimiento = 'I'
    BEGIN
        UPDATE Cuenta
        SET Dinero = Dinero + @Cantidad
        WHERE IdCuenta = @IdCuenta
    END
    -- GASTOS
    ELSE IF @TipoMovimiento = 'G'
    BEGIN
        UPDATE Cuenta
        SET Dinero = Dinero - @Cantidad
        WHERE IdCuenta = @IdCuenta;
    END
END;
