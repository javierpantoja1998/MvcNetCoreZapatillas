using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcNetCoreZapatillas.Data;
using MvcNetCoreZapatillas.Models;

#region SQL SERVER
//VUESTRO PROCEDIMIENTO DE PAGINACION DE IMAGENES DE ZAPATILLAS
#endregion

namespace MvcNetCoreZapatillas.Repositories
{
    public class RepositoryZapatillas
    {
        private ZapatillasContext context;

        public RepositoryZapatillas(ZapatillasContext context)
        {
            this.context = context;
        }

        //Funcion para sacar el total de  las zapatillas
        public int TotalZapatillas()
        {
            return this.context.Zapatillas.Count();
        }

        //Funcion para sacar todas las zapatillas
        public List<Zapatilla> GetZapatillas()
        {
            var consulta = from datos in this.context.Zapatillas
                           select datos;
            return consulta.ToList();
        }

        //Funcion para sacar los detalles de las zapatillas
        public Zapatilla GetZapatillaDetalle(int idZapatilla)
        {
            var consulta = from datos in this.context.Zapatillas
                           where datos.IdProducto == idZapatilla
                           select datos;
            return consulta.FirstOrDefault();
        }

        public List<ImagenZapatilla> GetImagenesZapatillaId(int idZapatilla)
        {
            var consulta = from datos in this.context.ImagenesZapatillas
                           where datos.IdProducto == idZapatilla
                           select datos;
            return consulta.ToList();
        }

        //Funcion para paginar zapatillas
        public List<Zapatilla> ListarZapatillas(int posicion)
        {
            string sql = "SP_ZAPATILLAS @POSICION";

            SqlParameter pamPos = new SqlParameter("@POSICION", posicion);

            var consulta = this.context.Zapatillas.FromSqlRaw(sql, pamPos);

            List<Zapatilla> zapatillas = consulta.AsEnumerable().ToList();

            return zapatillas;
        }
    }
}
