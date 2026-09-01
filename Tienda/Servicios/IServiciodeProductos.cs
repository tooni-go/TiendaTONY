public interface IServicioDeProductos
{
    Producto Crear(string nombre, decimal precio, int stock, int categoriaId);

    void Modificar(int id, string nombre, decimal precio, int stock, int categoriaId);

    void Eliminar(int id);

    List<Producto> Listar(int? categoriaId = null, string? nombre = null);
}
