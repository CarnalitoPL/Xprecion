using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xprecion.Clases
{
    internal class ClMedico
    {
        private int ID_MEDICO;
        private string NOMBRE;
        private string APELLIDO_PA;
        private string APELLIDO_MA;
        private int ID_AREA;

        public int ID_MEDICO1 { get => ID_MEDICO; set => ID_MEDICO = value; }
        public string NOMBRE1 { get => NOMBRE; set => NOMBRE = value; }
        public string APELLIDO_PA1 { get => APELLIDO_PA; set => APELLIDO_PA = value; }
        public string APELLIDO_MA1 { get => APELLIDO_MA; set => APELLIDO_MA = value; }
        public int ID_AREA1 { get => ID_AREA; set => ID_AREA = value; }

        public ClMedico()
        {

        }
        public ClMedico(int iD_MEDICO1)
        {
            ID_MEDICO1 = iD_MEDICO1;
        }

        public ClMedico(string nOMBRE1, string aPELLIDO_PA1, string aPELLIDO_MA1, int iD_AREA1)
        {
            NOMBRE1 = nOMBRE1;
            APELLIDO_PA1 = aPELLIDO_PA1;
            APELLIDO_MA1 = aPELLIDO_MA1;
            ID_AREA1 = iD_AREA1;
        }

        public ClMedico(int iD_MEDICO1, string nOMBRE1, string aPELLIDO_PA1, string aPELLIDO_MA1, int iD_AREA1)
        {
            ID_MEDICO1 = iD_MEDICO1;
            NOMBRE1 = nOMBRE1;
            APELLIDO_PA1 = aPELLIDO_PA1;
            APELLIDO_MA1 = aPELLIDO_MA1;
            ID_AREA1 = iD_AREA1;
        }
        public string buscartodos()
        {
            return ("select * from vta_medico_areamedico");
        }
        public string grabar()
        {
            return ("sp_grabar_MEDICO");
        }
        public string consultar()
        {
            return ("select * from MEDICO where ID_MEDICO = '" + this.ID_MEDICO + "'");
        }
        public string modificar()
        {
            return ("sp_modificar_MEDICO");
        }
        public string borrar()
        {
            return ("sp_borrar_MEDICO");
        }
    }
}
