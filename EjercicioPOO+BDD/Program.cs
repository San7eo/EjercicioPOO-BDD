using EjercicioPOO_BDD.Domain;
using EjercicioPOO_BDD.Repository;
using Microsoft.EntityFrameworkCore;
namespace EjercicioPOO_BDD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            

            List<CAceptado> aceptados = new List<CAceptado>();
            List<CRechazado> rechazos = new List<CRechazado>();

            string StringConnection = @"Data Source=localhost;Initial Catalog=EjecicioPOO+BDD;Integrated Security=True;Encrypt=False;Trust Server Certificate=True";

            var options = new DbContextOptionsBuilder<DataBaseContext>().UseSqlServer(StringConnection).Options;

            var context = new DataBaseContext(options);

            var ResultFechaProceso = context.Parametria.FirstOrDefault();
            string fechaFormat = "";
            if (ResultFechaProceso != null )
            {
                DateTime fecha_proceso = ResultFechaProceso.Fecha_Proceso.Date;
                fechaFormat = fecha_proceso.ToString("yyyy-MM-dd");
            }

            

            try
            {
                string path = @"C:\Users\miroa\Desktop\cdaCap\EjercicioPOO+BDD\data.txt";

                string[] data = File.ReadAllLines(path);

                string fecha;
                string codigo;
                float venta;
                string empresa;
                string fechaConFormato;
                string motivo;
                int I = 1;
                CAceptado aceptado = new CAceptado();
                CRechazado rechazado = new CRechazado();



                foreach (string dat in  data) 
                {
                    fecha = dat.Substring(0, 8);
                    codigo = dat.Substring(8, 3);
                    venta = float.Parse(dat.Substring (11,11));
                    empresa = dat.Substring (22);

                    fechaConFormato = $"{fecha.Substring(0, 4)}-{fecha.Substring(4, 2)}-{fecha.Substring(6, 2)}";

                    

                    if (codigo != null)
                    {
                        if (fechaConFormato.Equals(fechaFormat))
                        {
                            if (empresa.ToUpper() == "S" || empresa.ToUpper() == "N")
                            {
                                
                                aceptado.FechaInforme = DateTime.Parse(fechaConFormato);
                                aceptado.CodigoVenderdor = codigo;
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
                                motivo = $"El tamaño de la empresa no esta correctamente especificado por la letra S o N {I}";
                                rechazado.Motivo = motivo;
                                rechazos.Add(rechazado);
                            }
                        }
                        else
                        {
                            motivo = $"La fecha no coincide con la parametrizada {I}";
                            rechazado.Motivo = motivo;
                            rechazos.Add(rechazado);
                        }
                    }
                    else
                    {
                        motivo = $"No se encontro un codigo dentro de la linea {I}";
                        rechazado.Motivo = motivo;
                        rechazos.Add(rechazado);

                    }

                    I++;
                }

                foreach(CAceptado acep in aceptados) 
                {

                    Console.WriteLine($"{acep.FechaInforme}\n{acep.CodigoVenderdor}\n{acep.Venta}\n{acep.TamañoEmpresa}\n\n");
                    Console.ReadKey();
                    //context.Aceptado.Add(acep);
                    //context.SaveChanges();
                }

                //foreach (CRechazado rech in  rechazos) 
                //{ 
                //    context.Rechazado.Add(rech);
                //}
                //context.SaveChanges();
            }
            catch(IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Hubo un error general");
            }
           
        }
    }
}
