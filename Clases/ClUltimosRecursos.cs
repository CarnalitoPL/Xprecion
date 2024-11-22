    using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xprecion.Clases
{
    internal class ClUltimosRecursos
    {
        public class Medico
        {
            public byte IdMedico { get; set; }
            public string Nombre { get; set; }
            public string ApellidoPaterno { get; set; }
            public string ApellidoMaterno { get; set; }
            public byte IdArea { get; set; }
        }

    public List<Medico> ObtenerUltimosMedicos()
        {
            var medicos = new List<Medico>();
            using (var connection = new SqlConnection(ClGlobales.Globales.miconexion))
            {
                connection.Open();
                string query = "SELECT TOP 10 ID_MEDICO, NOMBRE, APELLIDO_PA, APELLIDO_MA, ID_AREA FROM MEDICO ORDER BY ID_MEDICO DESC";

                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        medicos.Add(new Medico
                        {
                            IdMedico = (byte)reader["ID_MEDICO"],
                            Nombre = reader["NOMBRE"].ToString(),
                            ApellidoPaterno = reader["APELLIDO_PA"].ToString(),
                            ApellidoMaterno = reader["APELLIDO_MA"].ToString(),
                            IdArea = (byte)reader["ID_AREA"]
                        });
                    }
                }
            }
            return medicos;
        }
    }
}
