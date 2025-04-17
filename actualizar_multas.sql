USE ElSaberDB;
GO

--Actualizar estado de prestamo
UPDATE Prestamo
SET Estado = 'Vencido'
WHERE Estado = 'Activo'
  AND FechaDevolucionEsperada < CONVERT(date, GETDATE());

-- Actualizar estado
UPDATE m
SET m.Estado = 'Pendiente'
FROM Multa m
INNER JOIN Prestamo p ON m.FK_IdPrestamo = p.idPrestamo
WHERE p.FechaDevolucionEsperada < CONVERT(DATE, GETDATE())
  AND p.estado <> 'Devuelto'
  AND m.Estado = 'Inactivo';

-- Actualizar monto
UPDATE m
SET m.MontoTotal = DATEDIFF(DAY, p.FechaDevolucionEsperada, GETDATE())
FROM Multa m
INNER JOIN Prestamo p ON m.FK_IdPrestamo = p.idPrestamo
WHERE m.Estado = 'Pendiente';
