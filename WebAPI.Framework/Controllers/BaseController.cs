using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Framework.Models.NorthwindDB;

namespace WebAPI.Framework.Controllers
{
    public class BaseController : ApiController
    {
        public string error = "";

        public bool Verify(string token) 
        {
            using (NorthwindDBCon db = new NorthwindDBCon()) 
            {
                return db.Users.Where(d => d.Token == token && d.idEstatus == 1).Count() > 0 ? true : false; 
            }
        }
    }
}
