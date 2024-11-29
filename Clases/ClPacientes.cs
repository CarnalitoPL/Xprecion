using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xprecion.Clases
{
    internal class ClPacientes
    {
        private int ID_PACIENTE;
        private string NOMBRE;
        private string APELLIDO_PA;
        private string APELLIDO_MA;
        private DateTime FECHA_DE_NACIMIENTO;
        private string CORREO_ELECTRONICO;
        private string DIRECCION;
        private string CONTACTO;
        public int ID_PACIENTE1 { get => ID_PACIENTE; set => ID_PACIENTE = value; }
        public string NOMBRE1 { get => NOMBRE; set => NOMBRE = value; }
        public string APELLIDO_PA1 { get => APELLIDO_PA; set => APELLIDO_PA = value; }
        public string APELLIDO_MA1 { get => APELLIDO_MA; set => APELLIDO_MA = value; }
        public DateTime FECHA_DE_NACIMIENTO1 { get => FECHA_DE_NACIMIENTO; set => FECHA_DE_NACIMIENTO = value; }
        public string CORREO_ELECTRONICO1 { get => CORREO_ELECTRONICO; set => CORREO_ELECTRONICO = value; }
        public string DIRECCION1 { get => DIRECCION; set => DIRECCION = value; }
        public string CONTACTO1 { get => CONTACTO; set => CONTACTO = value; }

        public ClPacientes()
        {

        }
        public ClPacientes(int iD_PACIENTE1)
        {
            ID_PACIENTE1 = iD_PACIENTE1;
        }

        public ClPacientes(string nOMBRE1, string aPELLIDO_PA1, string aPELLIDO_MA1, DateTime fECHA_DE_NACIMIENTO1, string cORREO_ELECTRONICO1, string dIRECCION1, string cONTACTO1)
        {
            NOMBRE1 = nOMBRE1;
            APELLIDO_PA1 = aPELLIDO_PA1;
            APELLIDO_MA1 = aPELLIDO_MA1;
            FECHA_DE_NACIMIENTO1 = fECHA_DE_NACIMIENTO1;
            CORREO_ELECTRONICO1 = cORREO_ELECTRONICO1;
            DIRECCION1 = dIRECCION1;
            CONTACTO1 = cONTACTO1;
        }

        public ClPacientes(int iD_PACIENTE1, string nOMBRE1, string aPELLIDO_PA1, string aPELLIDO_MA1, DateTime fECHA_DE_NACIMIENTO1, string cORREO_ELECTRONICO1, string dIRECCION1, string cONTACTO1)
        {
            ID_PACIENTE1 = iD_PACIENTE1;
            NOMBRE1 = nOMBRE1;
            APELLIDO_PA1 = aPELLIDO_PA1;
            APELLIDO_MA1 = aPELLIDO_MA1;
            FECHA_DE_NACIMIENTO1 = fECHA_DE_NACIMIENTO1;
            CORREO_ELECTRONICO1 = cORREO_ELECTRONICO1;
            DIRECCION1 = dIRECCION1;
            CONTACTO1 = cONTACTO1;
        }
        public string buscartodos()
        {
            return ("select * from REGISTRO_DE_PACIENTE");
        }
        public string buscarPaciente()
        {
            return ("select * from REGISTRO_DE_PACIENTE");
        }
        public string grabar()
        {
            return ("sp_grabar_Paciente");
        }
        public string consultar()
        {
            return ("select * from REGISTRO_DE_PACIENTE where ID_PACIENTE = '" + this.ID_PACIENTE + "'");
        }
        public string modificar()
        {
            return ("sp_modificar_Paciente");
        }
        public string borrar()
        {
            return ("sp_borrar_Paciente");
        }
    }
}
