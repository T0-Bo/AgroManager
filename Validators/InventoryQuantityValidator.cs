
using AgroManager.Enums;

namespace AgroManager.Validators;

public class InventoryQuantityValidator
{
    public static string? ValidateQuantity(decimal quantity, UnidadMedida unit)
    {
        if (quantity <= 0)
            return "La cantidad debe ser mayor a cero";

        if ((unit == UnidadMedida.Bolsa || unit == UnidadMedida.Unidad)
            && quantity % 1 != 0)
        {
            return "La cantidad debe ser un número entero para esta unidad de medida";
        }

        return null;
    }
}
