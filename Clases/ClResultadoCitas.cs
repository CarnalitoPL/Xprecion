using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xprecion.Clases
{
    internal class ClResultadoCitas
    {
        private int ID_RESULTADOCITA;
        private string COMENTARIOFINAL;
        private int ID_CITAS;

        public int ID_RESULTADOCITA1 { get => ID_RESULTADOCITA; set => ID_RESULTADOCITA = value; }
        public string COMENTARIOFINAL1 { get => COMENTARIOFINAL; set => COMENTARIOFINAL = value; }
        public int ID_CITAS1 { get => ID_CITAS; set => ID_CITAS = value; }


        public ClResultadoCitas()
        { 

        }

        public ClResultadoCitas(int iD_RESULTADOCITA1, string cOMENTARIOFINAL1, int iD_CITAS1)
        {
            ID_RESULTADOCITA1 = iD_RESULTADOCITA1;
            COMENTARIOFINAL1 = cOMENTARIOFINAL1;
            ID_CITAS1 = iD_CITAS1;
        }

        public ClResultadoCitas(int iD_RESULTADOCITA1)
        {
            ID_RESULTADOCITA1 = iD_RESULTADOCITA1;
        }

        public string buscartodos()
        {
            return ("select * from vta_resultado");
        }

        public string grabar()
        {
            return ("sp_grabar_resultado");
        }
        public string consultar()
        {
            return ("select * from RESULTADO_CITAS where ID_RESULTADOCITA = '" + this.ID_RESULTADOCITA1 + "'");
        }
        public string modificar()
        {
            return ("sp_modificar_resultado");
        }
    }
}
