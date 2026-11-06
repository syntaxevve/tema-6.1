using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;//importar el espacio de nombres NECESARIO
namespace Subtema5._4
{
    internal class Program
    {
        static string ArchivoInventario = "inventario_pipe.txt";
        static void Main(string[] args)
        {
            int opcion = 0;
            while (opcion != 6)
            {
                Console.Clear();
                Console.WriteLine("\n=== MINIMARKET - INVENTARIO CON PIPE ===");
                Console.WriteLine("1. Registrar producto");
                Console.WriteLine("2. Buscar por código");
                Console.WriteLine("3. Listar productos");
                Console.WriteLine("4. Modificar");
                Console.WriteLine("5. Eliminar");
                Console.WriteLine("6. Salir");
                Console.Write("Opción: ");
                opcion = int.Parse(Console.ReadLine());
                switch (opcion)
                {
                    case 1:
                        ModuloVentas.RegistrarProducto();
                        break;
                    case 2:
                        ModuloVentas.BuscarPorCodigo();
                        break;
                    case 3:
                        ModuloVentas.Listar();
                        break;
                    case 4:
                        ModuloInventario.ModificarProducto();
                        break;
                    case 5:
                        ModuloInventario.EliminarProducto();
                        break;
                    case 6:
                        Console.WriteLine("Saliendo del inventario...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
                Console.ReadKey();
            }
        }
    }
    public class ModuloVentas  //modulo
    {
        public static string ArchivoInventario = "inventario_pipe";
        public static void RegistrarProducto()
        {
            Console.Write("Ingrese código del producto (Ej: P10): ");
            string codigo = Console.ReadLine().Trim().ToUpper();
            Console.Write("Ingrese nombre o descripción: ");
            string nombre = Console.ReadLine().Trim();
            Console.Write("Ingrese precio en soles (Ej: 4.50): ");
            double precio = double.Parse(Console.ReadLine());
            Console.Write("Ingrese stock disponible: ");
            int stock = int.Parse(Console.ReadLine());
            // Unimos los 4 campos mediante el delimitador PIPE
            string registro = $"{codigo}|{nombre}|{precio:F2}|{stock}";
            StreamWriter escritor = new StreamWriter(ArchivoInventario, true);
            escritor.WriteLine(registro);
            Console.WriteLine(">> Producto registrado con éxito con formato PIPE.");
            escritor.Close();
        }
        public static void BuscarPorCodigo()
        {
            Console.Write("Ingrese código del producto a buscar: ");
            string codigoBuscado = Console.ReadLine().Trim().ToUpper();
            StreamReader lector = new StreamReader(ArchivoInventario);

            string linea;
            bool encontrado = false;
            while ((linea = lector.ReadLine()) != null)
            {
                string[] campos = linea.Split('|');
                if (campos.Length == 4 && campos[0] == codigoBuscado)
                {
                    Console.WriteLine($"\n>> Producto encontrado: {campos[1]}");
                    Console.WriteLine($"   Precio: S/ {campos[2]}");
                    Console.WriteLine($"   Stock disponible: {campos[3]} unidades");
                    encontrado = true;
                    break;
                }
            }
            if (!encontrado)
            {
                Console.WriteLine("\nProducto no encontrado en el inventario.");
            }
            lector.Close();
        }
        public static void Listar()
        {
            StreamReader lector = new StreamReader(ArchivoInventario);
            string linea;
            while ((linea = lector.ReadLine()) != null)
            {
                string[] campos = linea.Split('|');

                string codigo = campos[0];
                string nombre = campos[1];
                double precio = double.Parse(campos[2]);
                int stock = int.Parse(campos[3]);
                Console.WriteLine($" [{codigo}] {nombre} - Stock actual: {stock} - precio {precio:F2} ");
            }
            lector.Close();
        }
    }
    public class ModuloInventario //modulo
    {
        public static string ArchivoInventario = "inventario_pipe";
        public static void ModificarProducto()
        {
            Console.Write("Ingrese código del producto a modificar: ");
            string codigoBuscado = Console.ReadLine().Trim().ToUpper();
            string[] lineas = File.ReadAllLines(ArchivoInventario);
            List<string> listaActualizada = new List<string>();
            bool encontrado = false;
            foreach (string linea in lineas)
            {
                string[] campos = linea.Split('|');
                // Si encontramos el código coincidente
                if (campos.Length == 4 && campos[0] == codigoBuscado)
                {
                    encontrado = true;
                    Console.WriteLine($"Producto actual: {campos[1]} (Precio actual: S/ {campos[2]}, Stock actual: {campos[3]})");
                    Console.Write("Ingrese NUEVO precio en soles: ");
                    double nuevoPrecio = double.Parse(Console.ReadLine());
                    Console.Write("Ingrese NUEVO stock: ");
                    int nuevoStock = int.Parse(Console.ReadLine());
                    listaActualizada.Add($"{campos[0]}|{campos[1]}|{nuevoPrecio:F2}|{nuevoStock}");
                }
                else
                {
                    // Los demás productos se conservan exactamente iguales
                    listaActualizada.Add(linea);
                }
            }
            if (encontrado)
            {
                // Reescribimos todo el archivo en modo sobreescritura con la lista modificada
                File.WriteAllLines(ArchivoInventario, listaActualizada);
                Console.WriteLine("¡Producto actualizado correctamente en disco!");
            }
            else
            {
                Console.WriteLine("Error: No se encontró ningún producto con el código ingresado.");
            }
        }
        public static void EliminarProducto()
        {
            Console.Write("Ingrese código del producto a eliminar: ");
            string codigoEliminar = Console.ReadLine().Trim().ToUpper();
            string[] lineas = File.ReadAllLines(ArchivoInventario);
            List<string> listaFiltrada = new List<string>();
            bool eliminado = false;
            foreach (string linea in lineas)
            {
                string[] campos = linea.Split('|');
                // Si es el código a eliminar, lo ignoramos (no se agrega a la lista final)
                if (campos.Length == 4 && campos[0] == codigoEliminar)
                {
                    eliminado = true;
                    Console.WriteLine($"Eliminando del archivo: {campos[1]} ({campos[0]})...");
                }
                else
                {
                    listaFiltrada.Add(linea); // Conservamos todos los demás
                }
            }
            if (eliminado)
            {
                // Reescribimos el archivo sin el producto excluido
                File.WriteAllLines(ArchivoInventario, listaFiltrada);
                Console.WriteLine("¡Producto eliminado exitosamente del archivo!");
            }
            else
            {
                Console.WriteLine("Error: El código ingresado no existe en el inventario.");
            }

        }
    }
}