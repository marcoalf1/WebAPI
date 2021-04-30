using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Framework.Models.NorthwindDB;
using WebAPI.Framework.Models.WS;

namespace WebAPI.Framework.Controllers
{
    public class AnimalController : BaseController
    {
        [HttpPost]
        public Reply Get([FromBody] SecurityViewModel model) 
        {
            Reply oR = new Reply();
            oR.Result = 0;

            if (!Verify(model.token)) 
            {
                oR.Message = "No Autorizado";
                return oR;
            }

            try
            {
                using (NorthwindDBCon db= new NorthwindDBCon()) 
                {
                    oR.Data = (from d in db.Animals
                              where d.idState == 1
                              select new ListAnimalsViewModel 
                              { 
                                  Name = d.Name,
                                  Patas = d.Patas
                              }).ToList();

                    oR.Result = 1;
                }
            }
            catch (Exception ex)
            {
                oR.Message = "Ocurrio un error con el servidor, por favor intenta mas tarde.";
                //throw;
            }

            return oR;
        }

    }
}
