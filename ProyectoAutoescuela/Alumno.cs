using System.Drawing;

namespace ProyectoAutoescuela;

internal class Alumno
{

    public int codigo;
    public string nombre;
    public string TipoPermiso;
    public double Gastos;
    public char matricula;
    public bool aprobado;
    public Bitmap Foto { get; set; }


    public Alumno(int codigo, string nombre, string TipoPermiso, double Gastos, char matricula, bool aprobado) {

        this.codigo = codigo;
        this.nombre = nombre;
        this.TipoPermiso= TipoPermiso;
        this.Gastos = Gastos;
        this.matricula = matricula;
        this.aprobado  = aprobado;
       
        
            
    }


}
