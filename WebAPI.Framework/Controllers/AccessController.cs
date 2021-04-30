using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Framework.Models.NorthwindDB;
using WebAPI.Framework.Models.WS;

namespace WebAPI.Framework.Controllers
{
    public class AccessController : ApiController
    {
        [HttpGet]
        public Reply HelloWorld()
        {
            Reply oR = new Reply();
            oR.Result = 1;
            oR.Message = "Hello world";

            return oR;
        }

        [HttpPost]
        public Reply Login([FromBody] AccessViewModel model) 
        {
            Reply oR = new Reply();
            try
            {
                using (NorthwindDBCon db = new NorthwindDBCon()) {
                    var lst = db.Users.Where(d => d.Email == model.email && d.Password == model.password && d.idEstatus == 1);

                    if (lst.Count() > 0)
                    {
                        oR.Result = 1;
                        oR.Data = Guid.NewGuid().ToString();

                        Users other = lst.First();
                        other.Token = (string)oR.Data;

                        db.Entry(other).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        oR.Message = "Datos incorrectos.";
                    }
                }
            }
            catch (Exception ex)
            {
                oR.Result = 0;
                oR.Message = "Ocurrio un error, lo estamos corrigiendo.";
                //throw;
            }

            return oR;
        }
    }
}
