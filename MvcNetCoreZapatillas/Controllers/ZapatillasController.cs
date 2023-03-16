using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MvcNetCoreZapatillas.Models;
using MvcNetCoreZapatillas.Repositories;

namespace MvcNetCoreZapatillas.Controllers
{
    #region PROCEDURES
   /* CREATE PROCEDURE SP_ZAPATILLAS(@POSICION INT)
    AS

        SELECT* FROM(SELECT CAST(ROW_NUMBER() OVER(ORDER BY IDPRODUCTO) AS INT) AS POSICION, IDPRODUCTO, NOMBRE, PRECIO FROM ZAPASPRACTICA) AS QUERY

        WHERE QUERY.POSICION >= @POSICION AND QUERY.POSICION<(@POSICION)
    GO*/
    #endregion
    public class ZapatillasController : Controller
    {
        private RepositoryZapatillas repo;

        public ZapatillasController(RepositoryZapatillas repo)
        {
            this.repo = repo;
        }

        public IActionResult GetZapatillas()
        {
            List<Zapatilla> zapatillas = this.repo.GetZapatillas();
            return View(zapatillas);
        }

        
        public IActionResult DetallesZapatillas(int idZapatilla)
        {
            return View(repo.GetZapatillaDetalle(idZapatilla));
        }

        public IActionResult FotosZapatilla(int idZapatilla)
        {
            List<ImagenZapatilla> imagenes = this.repo.GetImagenesZapatillaId(idZapatilla);
            return View(imagenes);
        }

        //EN INDEX ES DONDE VOY A INTEGRAR LAS PETICIONES ASINCRONAS
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult _ZapatillasPartial()
        {
            List<Zapatilla> zapatillas = this.repo.GetZapatillas();
            //Devolvemos partialView ya que estamos usando ajax
            return PartialView("_ZapatillasPartial", zapatillas);
        }
        
    }
    
   /* f*/
}
