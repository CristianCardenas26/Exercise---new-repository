
// REFACTORIZACION
// EJERCICIO #1

using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

public class ValidadorPassword
{
    public bool EsPasswordSegura(string password)
    {
        bool resultado = false;

        if (password != null)
        {
            if (password.Length >= 8)
            {
                if (password.Contains("@") || password.Contains("#"))
                {
                    resultado = true;
                }
                else
                {
                    Console.WriteLine("Error: Debe contener un carácter especial (@ o #).");
                }
            }
            else
            {
                Console.WriteLine("Error: Demasiado corta.");
            }
        }
        else
        {
            Console.WriteLine("Error: La contraseña no puede ser nula.");
        }

        return resultado;
    }
}
             // aqui vemos una funcion caotica al encontrar un if dentro de otro if, dentro de otro,
             // lo que hace dificil de entender y a la vez mantener el codigo, ademas de que se ve feo

         //CODIGO REFACTORIZADO     
public class ValidadorPassword
{
    public bool EsPasswordSegura(string password)
    {
        if (password == null) {Console.WriteLine("Error: La contraseña no puede ser nula.");return false;}
        if (password.Length < 8) {Console.WriteLine("Error: Contraseña demasiado corta."); return false;}
        if (!password.Contains("@") && !password.Contains("#")) {Console.WriteLine("Error: Debe tener un caracter especial."); return false;}
        return true;
    }     
}
  //ahora el codigo es mas legible y facil de mantener, ya que cada condicion se evalua de manera independiente, 
  //lo que hace que sea mas facil de entender y modificar en caso de ser necesario.

 // EJERCICIO #2:

 // en la siguiente funcion, necesito calcular el suledo neto a pagar a un empleado despues de deducir los impuestos.
using System;

public class ReporteSueldos
{
    public void ImprimirReporte(double s)
    {
        // 1. Calcular impuesto (0.13 es el impuesto estatal obligatorio)
        double i = s * 0.13;
        double r = s - i;

        // 2. Imprimir el reporte
        Console.WriteLine(" REPORTE DE PAGO ");
        Console.WriteLine("Sueldo Bruto: " + s);
        Console.WriteLine("Retención Impuestos: " + i);
        Console.WriteLine("Sueldo Neto a Pagar: " + r);
        Console.WriteLine("---------------------------");
    }
}

// el codigo funciona, sin embargo no es muy legible, ya que es dificil entender que signifcan las variables i,s,r, ademas
// hay un numero magico 0.13, no se sabe que es lo que hace.

    // CODIGO REFACTORIZADO

public class ReporteSueldos
{
    private const double PorsentajeImpuesto = 0.13;
    public void ImprimirReporte(double sueldoBruto)
    {
     double ImpuestoRetenido = CalcularImpuesto(sueldoBruto);
     double SueldoNeto = sueldoBruto - ImpuestoRetenido;
     MostrarEnConsola(sueldoBruto, ImpuestoRetenido, SueldoNeto);
    }
private double CalcularImpuesto(double sueldoBruto)
    {
        return sueldoBruto * PorsentajeImpuesto;
    }     
private void MostrarEnConsola(double sueldoBruto, double impuestoRetenido, double SueldoNeto)
    {
        Console.WriteLine(" REPORTE DE PAGO ");
        Console.WriteLine("Sueldo Bruto: " + sueldoBruto);
        Console.WriteLine("Retención Impuestos: " + impuestoRetenido);
        Console.WriteLine("Sueldo Neto a Pagar: " + SueldoNeto);
        Console.WriteLine("---------------------------");
    }
}
// ahora el codigo es mas legible, ya que se han eliminado los numeros magicos, ademas de que se han creado funciones 
//con nombres descriptivos, lo que hace que sea mas facil de entender y mantener el codigo.

// EJERCICIO #3:
// En la siguiente funcion se requiere verificar si es posible la apertura de una tienda dependiendo del dia habil, stock y mantenimiento de la tienda,
// la dincion funciona perii resulta muy dificil de entender y de mantener, pues la condicion es grande y maneja muchas variables en un solo if.
public class ControlTienda
{
    public void VerificarApertura(string dia, int stock, bool enMantenimiento)
    {
        if ((dia != "Domingo" && dia != "Sábado") && stock > 10 && enMantenimiento == false)
        {
            Console.WriteLine("La tienda está abierta al público.");
        }
        else
        {
            Console.WriteLine("La tienda debe permanecer cerrada.");
        }
    }
}
// Funcion Refactorizada
// Ahora lo que deberia hacer es descomponer el condicional en funciones mas pequeñas, cada una con una responsabilidad clara.
public class ControlTienda
{
    public void VerificarAperturaTienda(string dia, int stock, bool EnMantenimiento)
    {
        if (EsAptoParaAbrir(dia, stock, EnMantenimiento))
        {
            Console.WriteLine("La tienda esta abierta al publico.");
        }
        else
        {
            Console.WriteLine ("La tienda esta cerrada al publico.");
        }
    }
        private bool EsAptoParaAbrir(string dia, int stock, bool EnMantenimiento)
    {
        bool EsDiaHabil = dia != "Domingo" && dia != "Sabado";
        bool SuficienteStock = stock <= 15;
        bool NoEstaEnMantenimiento = !EnMantenimiento;
        return EsDiaHabil && SuficienteStock && NoEstaEnMantenimiento;
    }
}


// ahora el codigo es mas legible, ya que se han creado funciones con nombres descriptivos, lo que hace que sea mas facil de entender
// y mantener el codigo, ademas de que se han eliminado las variables innecesarias, ademas si es necesario modificar la condicion de apertura,
// solo se debe modificar la funcion EsAptoParaAbrir, sin necesidad de modificar la funcion VerificarAperturaTienda.

// EJERCICIO 4 

//En siguiente funcion se requiere calcular el precio total a pagar por el alquiler de un auto, teniendo en cuenta el modelo 
//y el valor de su seguro, sin Sin embargo el codigo es dificil de entender y de mantener, ya que se mezclan las responsabilidades
// de calcular el precio del seguro y el precio total del alquiler en una sola funcion.

public class Auto
{
    public string Modelo { get; set; }
    public int Anio { get; set; }
}

public class ServicioAlquiler
{
    public double CalcularPrecioSeguro(Auto auto)
    {
        if (auto.Anio < 2015)
        {
            return 100.0; 
        }
        else
        {
            return 250.0;
    }

    public void Rentar(Auto auto, int dias)
    {
        double seguro = CalcularPrecioSeguro(auto);
        double total = (dias * 50.0) + seguro;
        
        System.Console.WriteLine($"Auto rentado. Total a pagar: {total}");
    }
}

// Codigo Refactorizado 

// acontinuacion lo que se hará es separar las responsabilidades utlizando el principio de extract method

public class Auto
{
    public string Modelo { get; set; }
    public int Año { get; set; }
    public double CalcularPrecioSeguro()
    {
        if (Año < 2018)
        {
            return 100.0;  // carro antiguio, seguro economico
        }
        else
        {
            return 250.0; // carro nueevo, seguro costoso
        }         
    }
}

public class ServicioAlquiler
    {
        public void Rentar (Auto auto, int dias)
      {
        double seguro = auto.CalcularPrecioSeguro();
        double total = (dias * 50.0) + seguro;    // 50.0 precio base por dia de alquiler
        System.Console.WriteLine ("Vehiculo rentado, total a pagar:" + total);
      }
    }

// ahora el codigo es mas legible, ya que se han separado las responsabilidades, ademas de que se han creado funciones con nombres descriptivos
// y ahora la clase ServicioAlquiler solo se encarga de calcular el precio total del alquiler, mientras que la clase Auto se encarga
// de calcular el precio del seguro.

// EJERCICIO #5

// en la siguiente funcion, se require verificar que un estuduiante sea aprobado o no dependiendo de su nota final y su porcentaje de asisitencias
// ademas se requiere otorgar una beca de exelencia a los estudiantes por una nota mayor a 95.



// public class SistemaEscolar
// {
//     public void ProcesarAlumno(string nombre, double notaFinal, double porcentajeAsistencia)
//     {

//         if (notaFinal >= 60.0 && porcentajeAsistencia >= 85.0)
//         {
//             Console.WriteLine($"Alumno {nombre} APROBADO.");
//             double factorBeca = notaFinal * 0.1;
//             if (notaFinal >= 95.0 && factorBeca > 9.0)
//             {
//                 Console.WriteLine("¡Felicidades! Aplica para Beca de Excelencia.");
//             }
//         }
//         else
//         {
//             Console.WriteLine($"Alumno {nombre} REPROBADO por nota o asistencias.");
//         }
//     }
// }

// CODIGO REFACTORIZADO

public class SistemaEscolar
{
    public void ProcesarAlumno (String nombre, double NotaFinal, double PorcentajeAsistencia)
    {
if (!EsAprobado(NotaFinal, PorcentajeAsistencia))
        {
            Console.WriteLine ($"alumno {nombre} REPROBADO por nota o inasistencias.");
            return;
        }
        Console.WriteLine($"Alumno {nombre} APROBADO.");
        EvaluarBecaDeExelencia(NotaFinal);
    }
    pribate bool EsAptoParaAprobar ( double Nota, double PorcentajeAsisitencia)
    {
        return Nota >= 60.0 && PorcentajeAsisitencia >= 85.0;
    }
    private void EvaluarBecaDeExelencia (double NotaFinal)
    {
        double factorBeca = NotaFinal * 0.1;
        if NotaFinal >= 95.0 && factorBeca > 9.0
        {
            Console.WriteLine ("!Felicidades, aplica para la Beca de Exelencia¡");
        }
    }
}

// aqui utilizamos un estract method para separar las responsabilidades, se crearon funciones con nombres especificos y ademas
// descomponemos el condicional para que la funcion se vea mas ordenada, manejable y facil de entender para proximas modificaciones.


