using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tienda;

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public bool Activo { get; set; } = true;
    public Categoria? Categoria { get; set; }

    public static List<Producto> Listar(int? categoriaId = null, string? nombre = null)
    {
        throw new NotImplementedException();
    }

    
    public void Modificar(string nombre, decimal precio, int categoriaId)
    {
        if(!Validar(nombre, precio, Stock, categoriaId, out string mensaje))
        {
            throw new ArgumentException(mensaje);
        }

        Nombre = nombre;
        Precio = precio;
        throw new NotImplementedException();
    }

    public void Eliminar()
    {
        throw new NotImplementedException();
    }

    private static bool Validar(string nombre, decimal precio, int stock, int categoriaId, out string mensajeError)
    {
        mensajeError = string.Empty;

        if (string.IsNullOrWhiteSpace(nombre))
        {
            mensajeError = "El nombre del producto es obligatorio.";
            return false;
        }

        if (precio <= 0)
        {
            mensajeError = "El precio debe ser mayor que cero.";
            return false;
        }

        if (stock < 0)
        {
            mensajeError = "El stock no puede ser negativo.";
            return false;
        }

        var categoria = Contexto.Db.Categorias.Find(categoriaId);
        if (categoria is null)
        {
            mensajeError = "La categoria indicada no existe.";
            return false;
        }

        return true;
    }
}
