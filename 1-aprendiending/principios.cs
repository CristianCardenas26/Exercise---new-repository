// violacion KISS

// using System.Runtime.InteropServices.Marshalling;
// using System.Security.Cryptography.X509Certificates;

// bool ValidarEnvioGratis(double totalCompra)
// {
//     bool elEnvioEsGratis;

//     if (totalCompra >= 100)
//     {
//         elEnvioEsGratis = true;
//     }
//     else
//     {
//         elEnvioEsGratis = false;
//     }

//     return elEnvioEsGratis;
// }

// solucion KISS no te compliques estupido, o algo asi... 
// acontinuacion se muestra una solucion mas simple y directa para la funcion validar envio gratis, eliminando
// el uso de variables innecesarias y simplificando la logica de la funcion.

 bool ValidarEnvioGratis(double totalCompra)
{
    return totalCompra >= 100;
}   

// violacion DRY 
//en el siguiente ejemplo se requiere calcular el precio final de un prducto mas 
//el 19% de impuesto, ademas el total se debe mostrar en el carrito de compras y en el recibo de compra

//  EN LA PANTALLA DEL CARRITO 
//  double totalCarrito = (TotalCompra) * 1.19; 
//  Console.WriteLine($"Total en carrito: ${totalCarrito}");

//  //  EN EL MÓDULO DE LA FACTURA
//  double totalFactura = (TotalCompra) * 1.19; 
//  Console.WriteLine($"Total en factura: ${totalFactura}");

//solucion DRY

// EN LA PANTALLA DEL CARRITO
double TotalCompra = 200; // ejemplo de total de compra
double totalCarrito = CalcularPrecioConIVA(TotalCompra); 
Console.WriteLine($"Total en carrito: ${totalCarrito}");

// EN EL MÓDULO DE LA FACTURA 
double totalFactura = CalcularPrecioConIVA(TotalCompra); 
Console.WriteLine($"Total en factura: ${totalFactura}");

double CalcularPrecioConIVA(double precioBase)
{
    return precioBase * 1.19;
}


//violacion YAGNI
//Un cliente solicita crear una base de datos con el nombre y correo de los estuiantes.
public void RegistrarEstudiante(string nombre, string correo)
{Console.WriteLine($"Estudiante {nombre} registrado con el correo {correo}");
// posiblemente el dia de mañana el cliente solicitara el numero telefonico del estudiante
 ValidarTelefonoCelular();
}

//Solucion YAGNI 
//solo realizar lo solicitado por el cliente
void RegistrarEstudiante(string nombre, string correo)
{
    Console.WriteLine($"Estudiante {nombre} registrado con el correo {correo}");
}


//SOLID 
//Single Responsability Principle SRP
//violacion SRP
//public class GeneradorFactura
//{
//public void GenerarFactura()
//{
  //  Console.WriteLine("calculando los precios del carrito");
    //Console.WriteLine("imprimiendo el recibo fisico");
    //Console.WriteLine("enviando correo electrocico con la factura");
//}
//}

//solucion SRP responsabilidad por clase

public class GeneradorFactura
{
    public void calculartotal()
    {
        Console.WriteLine("calculando los precios del carrito");
    }
}
public class ImpresoraRecibo
{
    public void ImprimirRecibo()
    {
        Console.WriteLine("imprimiendo el recibo fisico");
    }
}
public class EnviarCorreo
{
    public void Enviarfactura()
    {
        Console.WriteLine("enviando correo electrocico con la factura");
    }
}

// Open Closed Principle OCP
//violacion OCP
//public class ProcesadorDePagos
//{
//    public void Procesar(string tipo, double total)
//    {
//        if (tipo == "Tarjeta")
//        {
//            Console.WriteLine($"Cobrando ${total} a la tarjeta de crédito...");
//        }
//        else if (tipo == "PayPal")
//        {
//            Console.WriteLine($"Iniciando sesión en PayPal para cobrar ${total}...");
//        }
//    }
//}
      //ahora cada que se quiera agragar un nuevo metodo de pago, se debera modificar toda la clase procesador de pagos.

  //solucion OCP

  public interface IMetodoPago
  {
      void ProcesarPago(double total);
  }
  public class PagoEfectivo : IMetodoPago
{
    public void ProcesarPago(double total)
    {
        Console.WriteLine($"Recibiendo ${total} en efectivo...");
    }
}    
public class PagoTarjeta : IMetodoPago
{
    public void ProcesarPago(double total)
    {
        Console.WriteLine($"Cobrando ${total} a la tarjeta de crédito...");
    }
}
public class Nequi : IMetodoPago
{
    public void ProcesarPago(double total)
    {
        Console.WriteLine($"Iniciando sesión en Nequi para cobrar ${total}...");
    }
}       //ahora solo se debera crear una clase nueva que funcione de manera independiente cada que se quiera agragar un metodo de pago.

// Liskov Substitution Principle LSP
//violacion LSP

//public class vehiculo
//{
    public virtual void EncenderMotor()
  //  {
        console.writeLine("Encendiendo el motor del vehículo...");
    //}
// }
// public class carro : vehiculo
// {
// }
// public class bicicleta : vehiculo
// {
//  public override void EncenderMotor()
// {
//    throw new NotImplementedException("Las bicicletas no tienen motor");
// }
// }       //en este caso la clase bicicleta viola el principio de sustitucion de Liskov, ya que no puede ser sustituida 
        // por la clase padre vehiculo sin generar un error.


//solucion LSP

public class vehiculo
{
    public virtual void Desplazarse()
    {
        Console.WriteLine("El vehículo se está desplazando...");
    }
}
         //se crea una interfaz solo para los vehiculos a motor.
Public interface VeiculoMotorizado
{
    void EncenderMotor(); 
}
public class carro : vehiculo, VeiculoMotorizado
{
    public void EncenderMotor()
    {
        Console.WriteLine("Encendiendo el motor del carro...");
    }
}
public class bicicleta : vehiculo
{  
}
// la bicileta no se encuentra en la interfaz de vehiculo a motor por lo que no pasa nada a no tener 
    //motor, sin embargo cumple con el principio liskov pues al igual que la clase padre, la vicicleta se desplaza.


// Interface Segregation Principle ISP
//violacion ISP
// public interface IEmpleado
// {
//     void trabajar();
//     void firmarContrato();
//     void cocinar();
//     void entregarpedidos();
// }
//     public class cocinero : IEmpleado
//     {
//         public void trabajar()
//         {
//             Console.WriteLine("Cocinando los pedidos...");
//         }
//         public void firmarContrato()
//         {
//             Console.WriteLine("Firmando contrato de trabajo...");
//         }
//         public void cocinar()
//         {
//             Console.WriteLine("Cocinando los pedidos...");
//         }
//         public void entregarpedidos()
//         {
//             throw new NotImplementedException("Los cocineros no entregan pedidos");
//         }
//   }       //en este caso la clase cocinero viola el principio de segregacion de interfaces pues le le dan todas las responsabilidades a un solo empleado

//solucion ISP 
// crear una interfas con responsabilidades que todos los empleados deben cumplir, para despues crear 
// interfaces mas especificas para cada empleado    

public interface IEmpleado
{
    void trabajar();
    void firmarContrato();
}
public interface ICocinero : IEmpleado
{
    void cocinar();
}
public interface IRep repartidor : IEmpleado
{
    void entregarpedidos();
}

public class cocinero : IEmpleado, ICocinero
{
    public void trabajar()
    {
        Console.WriteLine("Cocinando los pedidos...");
    }
    public void firmarContrato()
    {
        Console.WriteLine("Firmando contrato de trabajo...");
    }
    public void cocinar()
    {
        Console.WriteLine("Cocinando los pedidos...");
    }
}          //ahora el cocinero solo tiene las responsabilidades que le corresponden y asi mismo el repartidor solo tendra 
            //las responsabilidades que le corresponden, cumpliendo con el principio de segregacion de interfaces.

        // Dependency Inversion Principle DIP
    //violacion DIP

    // public class Carro
    // {
    //     public void EncenderMotor()
    //     {
    //         Console.WriteLine("Encendiendo el motor del carro...");
    //     }
    // }
    // public class ControlRemoto
    // {
    //     private Carro _carro;

    //     public ControlRemoto(Carro carro)
    //     {
    //         _carro = carro;
    //     }

    //     public void EncenderCarro()
    //     {
    //         _carro.EncenderMotor();
    //    }
    // }       //en este caso el control remoto depende directamente de la clase carro,
            //   lo que hace que si se quiere cambiar el tipo de vehiculo, se deba modificar el control remoto.

    //solucion DIP
    public interface IVehiculo
    {
        void EncenderMotor();
    }
    public class Carro : IVehiculo
    {
        public void EncenderMotor()
        {
            Console.WriteLine("Encendiendo el motor del carro...");
        }
    }
    public class ControlRemoto
    {
        private IVehiculo _vehiculo;

        public ControlRemoto(IVehiculo vehiculo)
        {
            _vehiculo = vehiculo;
        }

        public void EncenderVehiculo()
        {
            _vehiculo.EncenderMotor();
        }
    }       //ahora el control remoto depende de la abstraccion IVehiculo, lo que hace que se pueda cambiar el tipo de vehiculo sin 
            // modificar el control remoto, cumpliendo con el principio de inversion de dependencias.

            //BUENAS PRACTICAS DE PROGRAMNACION
            //1. Nombres descriptivos: Utilizar nombres claros y descriptivos para variables,
             
            