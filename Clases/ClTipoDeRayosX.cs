using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xprecion.Clases
{
    internal class ClTipoDeRayosX
    {
        private int ID_TIPO_RAYOS_X;
        private float PRECIOSERVICIO;
        private string SERVICIO;
        public int ID_TIPO_RAYOS_X1 { get => ID_TIPO_RAYOS_X; set => ID_TIPO_RAYOS_X = value; }
        public float PRECIOSERVICIO1 { get => PRECIOSERVICIO; set => PRECIOSERVICIO = value; }
        public string SERVICIO1 { get => SERVICIO; set => SERVICIO = value; }

        public ClTipoDeRayosX()
        {

        }

        public ClTipoDeRayosX(int iD_TIPO_RAYOS_X1)
        {
            ID_TIPO_RAYOS_X1 = iD_TIPO_RAYOS_X1;
        }

        public ClTipoDeRayosX(float pRECIOSERVICIO1, string sERVICIO1)
        {
            PRECIOSERVICIO1 = pRECIOSERVICIO1;
            SERVICIO1 = sERVICIO1;
        }

        public ClTipoDeRayosX(int iD_TIPO_RAYOS_X1, float pRECIOSERVICIO1, string sERVICIO1)
        {
            ID_TIPO_RAYOS_X1 = iD_TIPO_RAYOS_X1;
            PRECIOSERVICIO1 = pRECIOSERVICIO1;
            SERVICIO1 = sERVICIO1;
        }
        public string buscartodos()
        {
            return ("select * from TIPO_DE_RAYOS_X");
        }
        public string grabar()
        {
            return ("sp_grabar_TipoDeRayosx");
        }
        public string consultar()
        {
            return ("select * from TIPO_DE_RAYOS_X where ID_TIPO_RAYOS_X = '" + this.ID_TIPO_RAYOS_X + "'");
        }
        public string modificar()
        {
            return ("sp_modificar_TipoDeRayosx");
        }
        public string borrar()
        {
            return ("sp_borrar_TipoDeRayosx");
        }
    }
}
