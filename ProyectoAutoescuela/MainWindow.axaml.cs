using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;

namespace ProyectoAutoescuela;

public partial class MainWindow : Window
{
    List<Alumno> listaAlumno;
    int posicion = 0, auxregistros;
    private Bitmap imagenSeleccionada;
    public MainWindow()
      {
         listaAlumno = new List<Alumno>();
         InitializeComponent();
         //Extrae los datos del fichero cuando se inicia la aplicación
         ExtraerDatosFichero();


         if (listaAlumno.Count > 0)
         {
             mostrarAlumno();

         }


         //Cuenta el numero de registros
         auxregistros = listaAlumno.Count;
         //Etiqueta utilizada para mostrar el numero de registros que hay 
         etiquetaRegistros.Text = ($"Número de registros: {Convert.ToString(auxregistros)}");

         tbCodigo.IsEnabled = false;
         tbNombre.IsEnabled = false;
         tbTipo.IsEnabled = false;
         tbGasto.IsEnabled = false;
         tbMatricula.IsEnabled = false;
         chAprobado.IsChecked = false;

         //Se utilizará para manejar el cierre del formulario, para guardar los datos del fichero 
         this.Closed += (sender,args)=> GuardarAlcerrar() ;

         btnConfirmar.IsEnabled = false;
         btnConfirmarCambios.IsEnabled = false;
         btnSeleccionarImagen.IsEnabled = false;
     }

     //Metodo que se utilizará cuando se cierre la aplicación 
     private void GuardarAlcerrar()
     {
         //Condición que se utiliza cuando no hay datos en la lista, entonces no hará nada
         if (listaAlumno.Count == 0)
         {

            textodialogo.Text="No hay datos que guardar";
            OcultarVista();
         }
         //En caso contrario se llama al metodo que guarda los datos al fichero
         else
         {
             guardarDatosFichero();
         }
     }


     private void Form1_Load(object sender, EventArgs e)
     {

     }

     private void label1_Click(object sender, EventArgs e)
     {

     }

     private void label2_Click(object sender, EventArgs e)
     {

     }



     private void tbCodigo_TextChanged(object sender, EventArgs e)
     {

     }

     private void tbNombre_TextChanged(object sender, EventArgs e)
     {

     }

     private void tbTipo_TextChanged(object sender, EventArgs e)
     {

     }


     private void btnCrear_Click()
     {
         //Habilita los textbox
         btnConfirmar.IsEnabled = true;
         btnSeleccionarImagen.IsEnabled = true;
         tbCodigo.IsEnabled = true;
         tbNombre.IsEnabled = true;
         tbMatricula.IsEnabled = true;
         tbTipo.IsEnabled = true;
         tbGasto.IsEnabled = true;
         chAprobado.IsEnabled = true;

         btnAnterior.IsEnabled = false;
         btnSiguiente.IsEnabled = false;
         btnEliminar.IsEnabled = false;
         btnModificar.IsEnabled = false;
         btnCrear.IsEnabled = false;

         VaciarCampos();
     }

     private void btnEliminar_Click()
     {
         //Comprueba si es el ultimo alumno y si se cumple lo elimina y muestra el anterior a este
         if (posicion == listaAlumno.Count() - 1)
         {
             listaAlumno.RemoveAt(posicion);
             posicion--;


             //Actualiza el número de registros 
             auxregistros = listaAlumno.Count;
             etiquetaRegistros.Text = ($"Número de registros: {Convert.ToString(auxregistros)}");
             if (listaAlumno.Count() > 0)
             {
               
                 mostrarAlumno();
             }

             else
             {
                 tbCodigo.Text = string.Empty;
                 tbNombre.Text = string.Empty;
                 tbTipo.Text = string.Empty;
                 tbGasto.Text = string.Empty;
                 tbMatricula.Text = string.Empty;
                 chAprobado.IsChecked = false;
             }
             

         }
         //En caso contrario se realiza el mismo proceso pero sin actualizar la posicion
         else
         {
             listaAlumno.RemoveAt(posicion);

             auxregistros = listaAlumno.Count;
             etiquetaRegistros.Text = ($"Número de registros: {Convert.ToString(auxregistros)}");

             if (listaAlumno.Count() > 0)
             {
                 
                 mostrarAlumno();
             }

             else
             {
                 tbCodigo.Text = string.Empty;
                 tbNombre.Text = string.Empty;
                 tbTipo.Text = string.Empty;
                 tbGasto.Text = string.Empty;
                 tbMatricula.Text = string.Empty;
                 chAprobado.IsChecked = false;
             }
         }

     }

     private void chAprobado_CheckedChanged(object sender, EventArgs e)
     {


     }

     //Si el usuario pulsa este boton, se guardarán los datos almacenados del nuevo registro
     private void btnConfirmar_Click()
     {
         //Variables auxiliares para almacenar el nuevo alumno
         int codigo;
         String nombre;
         String tipoPermiso;
         double gastos_total;
         char matricula;
         Boolean aprobado = false;

         if (chAprobado.IsChecked==true)
         {

             aprobado = true;
         }

         else if (!chAprobado.IsChecked==false)
         {
             aprobado = false;

         }

       


         codigo = Convert.ToInt32(tbCodigo.Text);
         nombre = tbNombre.Text;
         tipoPermiso = tbTipo.Text;
         gastos_total = Convert.ToDouble(tbGasto.Text);
         matricula = Convert.ToChar(tbMatricula.Text);





         Alumno al = new Alumno(codigo, nombre, tipoPermiso, gastos_total, matricula, aprobado)
         {
            // Foto=imagen;
         };


         listaAlumno.Add(al);
         btnConfirmar.IsEnabled = false;
         btnSeleccionarImagen.IsEnabled = false;
         btnCrear.IsEnabled = true;
         btnAnterior.IsEnabled = true;
         btnSiguiente.IsEnabled = true;
         btnEliminar.IsEnabled = true;
         btnModificar.IsEnabled = true;


         mostrarAlumno();

         auxregistros = listaAlumno.Count;

         etiquetaRegistros.Text = ($"Número de registros: {Convert.ToString(auxregistros)}");

         btnConfirmarCambios.IsEnabled = false;
         tbCodigo.IsEnabled = false;
         tbNombre.IsEnabled = false;
         tbMatricula.IsEnabled = false;
         tbTipo.IsEnabled = false;
         tbGasto.IsEnabled = false;
         chAprobado.IsEnabled = false;
     }

     public void mostrarAlumno()
     {



         tbCodigo.Text = (Convert.ToString(listaAlumno[posicion].codigo));
         tbNombre.Text = (listaAlumno[posicion].nombre);
         tbTipo.Text = (listaAlumno[posicion].TipoPermiso);
         tbGasto.Text = (Convert.ToString(listaAlumno[posicion].Gastos));
         tbMatricula.Text = (Convert.ToString(listaAlumno[posicion].matricula));
         if (listaAlumno[posicion].aprobado == true)
         {

             chAprobado.IsChecked = true;
         }

         else
         {
             chAprobado.IsChecked = false;
         }

     }

     //Metodo que vacia todos los campos, se utilizará cuando se quiera añadir un nuevo registro
     public void VaciarCampos()
     {


         tbCodigo.Text = string.Empty;
         tbNombre.Text = string.Empty;
         tbTipo.Text = string.Empty;
         tbGasto.Text = string.Empty;
         tbMatricula.Text = string.Empty;
         chAprobado.IsChecked = false;

     }

     private void btnAnterior_Click()
     {
         //Condición que comprueba si esta en la primera posición para deshabilitar el boton anterior
         if (posicion == 0)
         {
             btnAnterior.IsEnabled = false;

         }

         //En caso contrario, retrocede a la posición anterior
         else
         {

             btnAnterior.IsEnabled = true;
             btnSiguiente.IsEnabled = true;
             posicion--;
         }

         mostrarAlumno();
     }

     private void btnSiguiente_Click()
     {
         if (posicion == listaAlumno.Count() - 1)
         {
             btnSiguiente.IsEnabled = false;

         }

         else
         {

             btnAnterior.IsEnabled = true;
             btnSiguiente.IsEnabled = true;
             posicion++;
         }

         mostrarAlumno();
     }

     private void etiquetaPos_Click(object sender, EventArgs e)
     {

     }

     private void btnModificar_Click()
     {

         //Habilita el boton confirmar cambios y deshabilito el resto
         btnConfirmarCambios.IsEnabled = true;

         btnCrear.IsEnabled = false;
         btnAnterior.IsEnabled = false;
         btnSiguiente.IsEnabled = false;
         btnEliminar.IsEnabled = false;
         btnModificar.IsEnabled = false;

         //Activo los textbox para que pueda realizar cambios
         tbCodigo.IsEnabled = true;
         tbNombre.IsEnabled = true;
         tbMatricula.IsEnabled = true;
         tbTipo.IsEnabled = true;
         tbGasto.IsEnabled = true;
         chAprobado.IsEnabled = true;

     }

     private void btnConfirmarCambios_Click()
     {

         int codigo;
         String nombre;
         String tipoPermiso;
         double gastos_total;
         char matricula;
         Boolean aprobado = false;

         //Asigno el valor de los textos a variables auxiliares

         codigo = Convert.ToInt32(tbCodigo.Text);
         nombre = tbNombre.Text;
         tipoPermiso = tbTipo.Text;
         gastos_total = Convert.ToDouble(tbGasto.Text);
         matricula = Convert.ToChar(tbMatricula.Text);

         //Le asigno los nuevos datos modificados
         listaAlumno[posicion].codigo = codigo;
         listaAlumno[posicion].nombre = nombre;
         listaAlumno[posicion].TipoPermiso = tipoPermiso;
         listaAlumno[posicion].Gastos = gastos_total;
         listaAlumno[posicion].matricula = matricula;

         if (chAprobado.IsChecked==true)
         {
             aprobado = true;
             listaAlumno[posicion].aprobado = aprobado;
         }

         btnConfirmarCambios.IsEnabled = false;


         btnCrear.IsEnabled = true;
         btnAnterior.IsEnabled = true;
         btnSiguiente.IsEnabled = true;
         btnEliminar.IsEnabled = true;
         btnModificar.IsEnabled = true;

         tbCodigo.IsEnabled = false;
         tbNombre.IsEnabled = false;
         tbMatricula.IsEnabled = false;
         tbTipo.IsEnabled = false;
         tbGasto.IsEnabled = false;
         chAprobado.IsEnabled = false;
     }
     public byte[] imageToByteArray(System.Drawing.Image imageIn)
     {
         MemoryStream ms = new MemoryStream();
         imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
         return ms.ToArray();
     }

     public Bitmap byteArrayToImage(byte[] byteArrayIn)
     {
         MemoryStream ms = new MemoryStream(byteArrayIn);
         Bitmap returnImage = new Bitmap(ms);
         return returnImage;
     }

     //Metodo que extrae los datos del fichero y los almacena en la aplicación 
     public void ExtraerDatosFichero()
     {
         string nombrefich = "databank.data";

         if (File.Exists(nombrefich))
         {
             BinaryReader br = new BinaryReader(File.Open(nombrefich, FileMode.Open));

             while (br.BaseStream.Position < br.BaseStream.Length)
             {
                 int codigo = br.ReadInt32();
                 String nombre = br.ReadString();
                 String tipopermiso = br.ReadString();
                 char matricula = br.ReadChar();
                 double gastos = br.ReadDouble();
                 Boolean aprobado = br.ReadBoolean();

                 Alumno al = new Alumno(codigo, nombre, tipopermiso, gastos, matricula, aprobado);
                 listaAlumno.Add(al);
             }

             br.Close();
         }

         else
         {
 
           textodialogo.Text = "No se ha podido extraer los datos porque el fichero indicado no existe";
           OcultarVista();
         }

     }

     //Metodo que recogera los datos almacenados y los guardara en un fichero 
     public void guardarDatosFichero()
     {
         BinaryWriter escribirfichero = new BinaryWriter(File.Open("databank.data", FileMode.Create));

         foreach (var alumno in listaAlumno)
         {
             escribirfichero.Write(alumno.codigo);
             escribirfichero.Write(alumno.nombre);
             escribirfichero.Write(alumno.TipoPermiso);
             escribirfichero.Write(alumno.matricula);
             escribirfichero.Write(alumno.Gastos);
             escribirfichero.Write(alumno.aprobado);
         }
         escribirfichero.Close();

     }

     //Cuando pulsa el boton se guardan los datos de los alumnos en el fichero databank.data
     private void btnGuardar_Click()
     {
         guardarDatosFichero();
     }

     private async void  btnSeleccionarImagen_Click()
     {
         // Crear un cuadro de diálogo para seleccionar una imagen
         OpenFileDialog dlg = new OpenFileDialog();
         dlg.Title = "Seleccione una imagen";

         // Mostrar el cuadro de diálogo y esperar la respuesta del usuario
         var result = await dlg.ShowAsync(this);


         // Verificar si el usuario seleccionó una imagen
         if (result != null)
         {
             // Obtener la ruta de la imagen seleccionada
             string rutaImagen = result[0];

             // Crear un objeto Bitmap para cargar la imagen desde el archivo
             using (var stream = new FileStream(rutaImagen, FileMode.Open))
             {
                 var bitmap = new Bitmap(stream);

                 // Asignar la imagen al control de imagen en tu interfaz gráfica (imagen)
                 imagen.Source = bitmap;
             }
         }
     }

     private void BtnAnterior_OnClick(object? sender, RoutedEventArgs e)
     {
         btnAnterior_Click();
     }


     private void BtnSiguiente_OnClick(object? sender, RoutedEventArgs e)
     {
        btnSiguiente_Click();
     }

    


     private void BtnEliminar_OnClick(object? sender, RoutedEventArgs e)
     {
         btnEliminar_Click();
     }

     private void BtnModificar_OnClick(object? sender, RoutedEventArgs e)
     {
         btnModificar_Click();
     }

     private void BtnConfirmar_OnClick(object? sender, RoutedEventArgs e)
     {
         btnConfirmar_Click();
     }

     private void BtnCrear_OnClick(object? sender, RoutedEventArgs e)
     {
         btnCrear_Click();
     }

     private void BtnConfirmarCambios_OnClick(object? sender, RoutedEventArgs e)
     {
        btnConfirmarCambios_Click();
     }

     private void BtnGuardar_OnClick(object? sender, RoutedEventArgs e)
     {
         guardarDatosFichero();
     }


     private void OcultarVista()
     {
         dialogo.IsVisible = true;
         cuadro.IsVisible = false;
     }

     private void BtnVolver_OnClick(object? sender, RoutedEventArgs e)
     {
         dialogo.IsVisible = false;
         cuadro.IsVisible = true;
     }

     private void BtnSeleccionarImagen_OnClick(object? sender, RoutedEventArgs e)
     {
         btnSeleccionarImagen_Click();
     }
}
