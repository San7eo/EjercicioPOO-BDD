using EjercicioPOO_BDD.Domain;
using EjercicioPOO_BDD.Repository;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
namespace EjercicioPOO_BDD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
           
           //List<CAceptado> aceptados = new List<CAceptado>();
           //List<CRechazado> rechazos = new List<CRechazado>();

            string StringConnection = @"Data Source=localhost;Initial Catalog=EjecicioPOO+BDD;Integrated Security=True;Trust Server Certificate=True";

            var options = new DbContextOptionsBuilder<DataBaseContext>().UseSqlServer(StringConnection).Options;

            var context = new DataBaseContext(options);

            var ResultFechaProceso = context.Parametria.FirstOrDefault();
            string fechaFormat = "";
            if (ResultFechaProceso != null )
            {
                DateTime fecha_proceso = ResultFechaProceso.Fecha_Proceso.Date;
                fechaFormat = fecha_proceso.ToString("yyyy-MM-dd");
            }
            /*
            try
            {
                string path = @"C:\Users\miroa\Desktop\cdaCap\EjercicioPOO+BDD\data.txt";

                string[] data = File.ReadAllLines(path);

                string fecha;
                string codigo;
                decimal venta;
                string empresa;
                string fechaConFormato;
                string motivo;
                int I = 1;
                



                foreach (string dat in  data) 
                {
                    fecha = dat.Substring(0, 8).Trim();
                    codigo = dat.Substring(8, 3).Trim();
                    venta = decimal.Parse(dat.Substring (11,11), CultureInfo.InvariantCulture);
                    empresa = dat.Substring (22);

                    fechaConFormato = $"{fecha.Substring(0, 4)}-{fecha.Substring(4, 2)}-{fecha.Substring(6, 2)}";

                    CAceptado aceptado = new CAceptado();
                    CRechazado rechazado = new CRechazado();

                    if (codigo != null && codigo != "")
                    {
                        if (fechaConFormato.Equals(fechaFormat))
                        {
                            if (empresa.ToUpper() == "S" || empresa.ToUpper() == "N")
                            {
                                
                                aceptado.FechaInforme = DateTime.Parse(fechaConFormato);
                                aceptado.CodigoVendedor = codigo;
                                aceptado.Venta = venta;
                                if (empresa.ToUpper() == "S")
                                {
                                    aceptado.TamañoEmpresa = true;
                                    
                                }
                                else
                                {
                                    aceptado.TamañoEmpresa = false;
                                    
                                }
                                aceptados.Add(aceptado);

                            }
                            else
                            {
                                motivo = $"El tamaño de la empresa no esta correctamente especificado por la letra S o N en la linea: {I}";
                                rechazado.Motivo = motivo;
                                rechazos.Add(rechazado);
                            }
                        }
                        else
                        {
                            motivo = $"La fecha no coincide con la parametrizada en la linea: {I}";
                            rechazado.Motivo = motivo;
                            rechazos.Add(rechazado);
                        }
                    }
                    else
                    {
                        motivo = $"No se encontro un codigo dentro de la linea: {I}";
                        rechazado.Motivo = motivo;
                        rechazos.Add(rechazado);

                    }

                    I++;
                }

                foreach(CAceptado acep in aceptados) 
                {

                    //Console.WriteLine($"{acep.FechaInforme}\n{acep.CodigoVenderdor}\n{acep.Venta}\n{acep.TamañoEmpresa}\n\n");
                    //Console.ReadKey();
                    context.Aceptado.Add(acep);
                }

                foreach (CRechazado rech in  rechazos) 
                { 
                   context.Rechazado.Add(rech);
                }
                context.SaveChanges();
            }
            catch(IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Hubo un error general");
            }
            */
            //PUNTO 4 LISTAR VENDEDORES > 100000
            var ListaVendedores4 = context.Aceptado
                                                  .GroupBy(aceptado => aceptado.CodigoVendedor)
                                                  .Where(aceptado => aceptado.ToList().Sum(s=>s.Venta) >= 100000m)
                                                  .Select(group => new
                                                  {
                                                      CodigoVendedor = group.Key,
                                                      TotalVentas = group.ToList().Sum(group => group.Venta)
                                                  })
                                                  .ToList();

              foreach (var vendedor in ListaVendedores4)
              {
                  Console.WriteLine($"El vendedor {vendedor.CodigoVendedor} vendió {vendedor.TotalVentas:C}");
              }
              Console.ReadKey();

            //PUNTO 5 LISTAR VENDEDORES < 100000
            var ListaVendedores5 = context.Aceptado
                                                 .GroupBy(aceptado => aceptado.CodigoVendedor)
                                                 .Select(group => new
                                                 {
                                                     CodigoVendedor = group.Key,
                                                     TotalVentas = group.Sum(aceptado => aceptado.Venta)
                                                 })
                                                 .Where(resultado => resultado.TotalVentas < 100000)
                                         
                                                 .ToList();

            foreach (var vendedor in ListaVendedores5)
            {
                Console.WriteLine($"El vendedor {vendedor.CodigoVendedor} vendió {vendedor.TotalVentas:C}");
            }
            Console.ReadKey();

            //PUNTO 6 LISTAR VENDEDORES CON ALMENOS UNA EMPRESA GRANDE EN SUS VENTAS

             var ListaVendedoresEmpresaGrande = context.Aceptado
                                                        .Where(aceptado => aceptado.TamañoEmpresa) // Filtrar solo las ventas a empresas grandes
                                                        .Select(aceptado => aceptado.CodigoVendedor)
                                                        .Distinct() // Para obtener códigos de vendedor únicos
                                                        .ToList();

                    Console.WriteLine("Vendedores que han vendido a una empresa grande:");
                    foreach (var codigoVendedor in ListaVendedoresEmpresaGrande)
                    {
                        Console.WriteLine($"Código de Vendedor: {codigoVendedor}");
                    }
                    Console.ReadKey();


        }
    }
}
