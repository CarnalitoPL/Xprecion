using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xprecion.Clases
{
    internal class ClAreaMedica
    {
        private int ID_AREA;
        private string AREA;

        public int ID_AREA1 { get => ID_AREA; set => ID_AREA = value; }
        public string AREA1 { get => AREA; set => AREA = value; }

        public ClAreaMedica() 
        {

        }

        public ClAreaMedica(int iD_AREA1, string aREA1)
        {
            ID_AREA1 = iD_AREA1;
            AREA1 = aREA1;
        }

        public ClAreaMedica(string aREA1)
        {
            AREA1 = aREA1;
        }

        public ClAreaMedica(int iD_AREA1)
        {
            ID_AREA1 = iD_AREA1;
        }
        public string buscartodos()
        {
            return ("select * from AREA_MEDICO");
        }
        public string grabar()
        {
            return ("sp_grabar_AREA_MEDICO");
        }
        public string consultar()
        {
            return ("select * from AREA_MEDICO where ID_AREA = '" + this.ID_AREA + "'");
        }
        public string modificar()
        {
            return ("sp_modificar_MEDICO_AREA");
        }
        public string borrar()
        {
            return ("sp_borrar_AREA_MEDICA");
        }
    }
}
