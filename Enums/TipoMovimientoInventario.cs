namespace AgroManager.Enums;

public enum TipoMovimientoInventario
{
    Compra,              // Ingreso de stock desde proveedor
    Venta,               // Salida por venta a cliente
    DevolucionCliente,   // Cliente devuelve producto
    DevolucionProveedor, // Devolvés stock al proveedor
    Merma,               // Producto dañado, vencido, perdido
    Ajuste              // Corrección manual de stock (conteo físico)
}   