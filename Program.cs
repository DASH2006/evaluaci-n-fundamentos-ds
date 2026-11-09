using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evaluación_fundamentos_ds
{
    internal class Program
    {
        static bool ValidarDatos(double precio, int cantidad)
        {
            if (precio > 0 || cantidad > 0)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Error: Datos invalidos. El precio y cantidad deben ser mayores a 0");
                return false;
            }
        }
        static void BuscarOrden(string codigoBuscar, string[] codigos, string[] provedores, double[,] datos)
        {
            if (string.IsNullOrWhiteSpace(codigoBuscar))
            {
                Console.WriteLine("Error: El código de orden no puede estar vacío.");
                return;
            }
            for (int i = 0; i < codigos.Length; i++) 
            {
                double monto = datos[i, 0] * datos[i, 1];
                Console.WriteLine($"{i + 1}.Código: {codigos[i]}, Provedor: {provedores[i]},Montoneto: $ {monto:N2}");
                return;
            }
            Console.WriteLine("Error: Código no encontrado :");
        } 
        static void Main()
        {
            string[] codigos = {"OC-101", "OC-102","OC-103","OC-104","OC-105","OC-106"};
            string[] provedores = {"TechCorp", "LogiExpress", "InsumoGlobal", "TechCorp2", "AlfaSUministros", "Servidores"};
            double[,] datos =
            {
                {150.0, 10 },
                {450.0, 2 },
                {80.050, 50},
                {1200.0, 1 },
                {25.0, 100},
                {600.0, 3},

            };
            Console.WriteLine("====================================");
            Console.WriteLine("Control de ordenes de compra(Evaluación 45 min)");
            Console.WriteLine("====================================");

            double[] montos = new double[codigos.Length];
            double granTotal = 0;
            int comprasCriticas = 0;
            for (int i = 0; i < codigos.Length; i++)
            {
                double precio = datos[i, 0];
                int cantidad = (int)datos[i, 1];

                if (ValidarDatos(precio, cantidad))
                {
                    double monto = precio * cantidad;
                    montos[i] = monto;
                    granTotal += monto;

                    if (monto > 1000)
                        comprasCriticas++;
                    
                }
                else
                {
                   montos[i] = 0;
                }
            }
            int n=codigos.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (montos[j] < montos[j + 1])
                    {
                        double tempMonto = montos[j]; montos[j] = montos[j + 1]; montos[j + 1] = tempMonto;
                        string tempCodigo = codigos[j];codigos[j] = codigos[j + 1]; codigos[j + 1] = tempCodigo;
                        string tempProvedor = provedores[j]; provedores[j] = provedores[j + 1]; provedores[j + 1] = tempProvedor;
                        double tempDato0 = datos[j, 0]; datos[j, 0] = datos[j + 1, 0]; datos[j + 1, 0] = tempDato0;
                        double tempCantidad = datos[j, 1]; datos[j, 1] = datos[j + 1, 1]; datos[j + 1, 1] = tempCantidad;
                    }
                }
            }
            Console.WriteLine("\n--Reporte ordenado por monto neto--");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"{i + 1}Código: {codigos[i]}, Provedor: {provedores[i]}, Montoneto: ${montos[i]:N2}");
            }
            Console.WriteLine("\nMetricas:");
            Console.WriteLine($"Gran total de compras: ${granTotal:N2}");
            Console.WriteLine($"Número de compras críticas (monto neto > $1000): {comprasCriticas}");
            Console.WriteLine("\n--Busqueda--");
            Console.WriteLine("Ingresee el código de orden a buscar:");
            string codigoBuscar = Console.ReadLine().Trim().ToUpper();
            BuscarOrden(codigoBuscar, codigos, provedores, datos);
            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }

    }
}
