using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xprecion.Clases
{
    internal class ClCitas
    {
        private int ID_CITAS;
        private DateTime FECHA;
        private string HORA;
        private bool ESTADOCITA;
        private string COMENTARIO;
        private int ID_TIPO_DE_RAYOS_X;
        private int ID_MEDICO;
        private int ID_PACIENTE;

        public int ID_CITAS1 { get => ID_CITAS; set => ID_CITAS = value; }
        public DateTime FECHA1 { get => FECHA; set => FECHA = value; }
        public string HORA1 { get => HORA; set => HORA = value; }
        public bool ESTADOCITA1 { get => ESTADOCITA; set => ESTADOCITA = value; }
        public string COMENTARIO1 { get => COMENTARIO; set => COMENTARIO = value; }
        public int ID_TIPO_DE_RAYOS_X1 { get => ID_TIPO_DE_RAYOS_X; set => ID_TIPO_DE_RAYOS_X = value; }
        public int ID_MEDICO1 { get => ID_MEDICO; set => ID_MEDICO = value; }
        public int ID_PACIENTE1 { get => ID_PACIENTE; set => ID_PACIENTE = value; }

        public ClCitas()
        {

        }

        public ClCitas(int iD_CITAS1, DateTime fECHA1, string hORA1, bool eSTADOCITA1, string cOMENTARIO1, int iD_TIPO_DE_RAYOS_X1, int iD_MEDICO1, int iD_PACIENTE1)
        {
            ID_CITAS1 = iD_CITAS1;
            FECHA1 = fECHA1;
            HORA1 = hORA1;
            ESTADOCITA1 = eSTADOCITA1;
            COMENTARIO1 = cOMENTARIO1;
            ID_TIPO_DE_RAYOS_X1 = iD_TIPO_DE_RAYOS_X1;
            ID_MEDICO1 = iD_MEDICO1;
            ID_PACIENTE1 = iD_PACIENTE1;
        }

        public ClCitas(int iD_CITAS1)
        {
            ID_CITAS1 = iD_CITAS1;
        }

        public string buscartodos()
        {
            return ("select * from vta_citas");
        }
        public string buscarcita()
        {
            return ("select * from vta_buscarcita");
        }
        public string grabar()
        {
            return ("sp_grabar_citas");
        }
        public string consultar()
        {
            return ("select * from CITAS where ID_CITAS = '" + this.ID_CITAS1 + "'");
        }
        public string modificar()
        {
            return ("sp_modificar_citas");
        }
        public string borrar()
        {
            return ("sp_borrar_cita");
        }
    }
}
