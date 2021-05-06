using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
                using (NorthwindDBCon db = new NorthwindDBCon())
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

        [HttpPost]
        public Reply Add([FromBody] AnimalViewModel model)
        {
            Reply oR = new Reply();
            oR.Result = 0;

            if (!Verify(model.token))
            {
                oR.Message = "No Autorizado";
                return oR;
            }

            // Agregar validaciones del modelo
            if (!Validate(model))
            {
                oR.Message = error;
                return oR;
            }


            try
            {
                using (NorthwindDBCon db = new NorthwindDBCon())
                {
                    Animals oAnimal = new Animals();
                    oAnimal.idState = 1;
                    oAnimal.Name = model.Name;
                    oAnimal.Patas = model.Patas;

                    db.Animals.Add(oAnimal);
                    db.SaveChanges();

                    // se devuelve la lista de los registros para que no se  haga una nueva peticion 
                    oR.Data = List(db);

                    oR.Result = 1;
                }
            }
            catch (Exception ex)
            {
                //Poner log en bd
                oR.Message = "Ocurrio un error con el servidor, por favor intenta mas tarde.";
                //throw;
            }

            return oR;
        }

        [HttpPut]
        public Reply Edit([FromBody] AnimalViewModel model)
        {
            Reply oR = new Reply();
            oR.Result = 0;

            if (!Verify(model.token))
            {
                oR.Message = "No Autorizado";
                return oR;
            }

            // Agregar validaciones del modelo
            if (!Validate(model))
            {
                oR.Message = error;
                return oR;
            }

            try
            {
                using (NorthwindDBCon db = new NorthwindDBCon())
                {
                    Animals oAnimal = db.Animals.Find(model.Id);
                    oAnimal.Name = model.Name;
                    oAnimal.Patas = model.Patas;

                    db.Entry(oAnimal).State = EntityState.Modified;
                    db.SaveChanges();

                    // se devuelve la lista de los registros para que no se  haga una nueva peticion 
                    oR.Data = List(db);

                    oR.Result = 1;
                }
            }
            catch (Exception ex)
            {
                //Poner log en bd
                oR.Message = "Ocurrio un error con el servidor, por favor intenta mas tarde.";
                //throw;
            }

            return oR;
        }

        [HttpDelete]
        public Reply Delete([FromBody] AnimalViewModel model)
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
                using (NorthwindDBCon db = new NorthwindDBCon())
                {
                    Animals oAnimal = db.Animals.Find(model.Id);
                    oAnimal.idState = 2;

                    db.Entry(oAnimal).State = EntityState.Modified;
                    db.SaveChanges();

                    // se devuelve la lista de los registros para que no se  haga una nueva peticion 
                    oR.Data = List(db);

                    oR.Result = 1;
                }
            }
            catch (Exception ex)
            {
                //Poner log en bd
                oR.Message = "Ocurrio un error con el servidor, por favor intenta mas tarde.";
                //throw;
            }

            return oR;
        }

        [HttpPost]
        public async Task<Reply> Photo([FromUri] AnimalPictureViewModel model) 
        {
            Reply oR = new Reply();
            oR.Result = 0;

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            if (!Verify(model.token))
            {
                oR.Message = "No Autorizado";
                return oR;
            }

            // Verificar si viene multipart
            if (!Request.Content.IsMimeMultipartContent()) 
            {
                oR.Message = "No viene imagen";
                return oR;
            }

            await Request.Content.ReadAsMultipartAsync(provider);

            FileInfo fileInfoPicture = null;

            foreach (MultipartFileData fileData in provider.FileData)
            {
                if (fileData.Headers.ContentDisposition.Name.Replace("\\","").Replace("\"","").Equals("picture"))
                    fileInfoPicture = new FileInfo(fileData.LocalFileName);
            }

            if (fileInfoPicture != null) 
            {
                // Guardar el archivo
                using (FileStream fs = fileInfoPicture.Open(FileMode.Open, FileAccess.Read)) 
                {
                    byte[] b = new byte[fileInfoPicture.Length];
                    UTF8Encoding temp = new UTF8Encoding(true);
                    while (fs.Read(b, 0, b.Length) > 0) ;

                    // Guardar el archivo
                    try
                    {
                        using (NorthwindDBCon db = new NorthwindDBCon()) 
                        {
                            var oAnimal = db.Animals.Find(model.Id);
                            oAnimal.Picture = b;
                            db.Entry(oAnimal).State = EntityState.Modified;
                            db.SaveChanges();

                            oR.Result = 1;
                        }
                    }
                    catch (Exception ex)
                    {

                        oR.Message = "Intenta mas tarde.";
                    }
                }

            }

            return oR;

        }

        #region Helpers
        private bool Validate(AnimalViewModel model) 
        {
            if (model.Name == "")
            {
                error = "El nombre es obligatorio";
                return false;
            }

            return true;
        }

        private List<ListAnimalsViewModel> List(NorthwindDBCon db) 
        {

            return (from d in db.Animals
                    where d.idState == 1
                    select new ListAnimalsViewModel
                    {
                        Name = d.Name,
                        Patas = d.Patas
                    }).ToList();
        }
        #endregion

    }
}
 