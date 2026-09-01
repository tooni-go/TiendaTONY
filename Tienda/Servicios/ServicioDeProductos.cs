namespace Tienda;

public class ServicioDeProductos : IServicioDeProductos
{
    private readonly TiendaDBContext _contexto;
    
    public ServicioDeProductos(TiendaDBContext contexto)
    {
        _contexto = contexto;
    }
    
    public Producto Crear(string nombre, decimal precio, int stock, int categoriaId)
    {
        if (!Validar(nombre, precio, stock, out string mensaje)) 
        {  
            throw new ArgumentException(mensaje); 
        }

        Categoria categoria = _contexto.Categorias.Find(categoriaId); 
        if (categoria is null)
        {
            throw new ArgumentException("La categoria indicada no existe.");
        }
        
        var nuevoProducto = new Producto
        {
            Nombre = nombre.Trim(),
            Precio = precio,
            Stock = stock,
            Categoria = categoria,
        };

        _contexto.Productos.Add(nuevoProducto);
        _contexto.SaveChanges();
        
        return nuevoProducto;
    }

    public void Modificar(int id, string nombre, decimal precio, int stock, int categoriaId)
    {
        Producto producto = _contexto.Productos.Find(id);
        if (producto is null)
        {
            throw new ArgumentException("El producto no existe.");
        }
        
        if(!Validar(nombre, precio, stock, out string mensaje))
        {
            throw new ArgumentException(mensaje);
        }

        Categoria categoria = _contexto.Categorias.Find(categoriaId); 
        if (categoria is null)
        {
            throw new ArgumentException("La categoria indicada no existe.");
        }

        producto.Nombre = nombre.Trim();
        producto.Precio = precio;
        producto.Stock = stock;
        producto.Categoria = categoria;

        Contexto.Db.SaveChanges();
        
    }

    public void Eliminar(int id)
    {
        Producto producto = _contexto.Productos.Find(id);
        if (producto is null)
        {
            throw new ArgumentException("El producto no existe.");
        }

        producto.Activo = false;
        _contexto.SaveChanges();
    }

    public List<Producto> Listar(int? categoriaId = null, string? nombre = null)
    {
        return _contexto.Productos.ToList();   
    }

    private bool Validar(string nombre, decimal precio, int stock, out string mensajeError)
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
 
        // Aca se buscaba la categoria por id, lo definio el profe pero me tosquea, pq si no busco la base de datos dos veces en la de crear y aca.

        return true;
    }

}
