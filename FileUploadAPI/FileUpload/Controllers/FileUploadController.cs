using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FileUpload.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace FileUpload.Controllers
{
    [Route("api/upload")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {

        [HttpPost, DisableRequestSizeLimit]
        public IEnumerable<object> Get()
        {
            try
            {
                DB db = new DB();
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    DataTable dt = new DataTable();
                    dt.Columns.Add("SchemeId", typeof(string));
                    dt.Columns.Add("FirstName", typeof(string));
                    dt.Columns.Add("LastName", typeof(string));
                    dt.Columns.Add("Email", typeof(string));
                    dt.Columns.Add("Mobile", typeof(Int64));
                    dt.Columns.Add("Error", typeof(string));
                    int i = 0;
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        while (!reader.EndOfStream)
                        {
                            DataRow dr = dt.NewRow();
                            string row = reader.ReadLine();
                            var arr = row.Split("|");

                            dr["SchemeId"] = arr[0];
                            dr["FirstName"] = arr[1];
                            dr["LastName"] = arr[2];
                            bool isEmail = Regex.IsMatch(arr[3], @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                            dr["Email"] = arr[3];
                            dr["Mobile"] = Convert.ToInt64(arr[4]);
                            if (!isEmail)
                                dr["Error"] = "Invalid email";
                            else
                                dr["Error"] = "Success";

                            dt.Rows.Add(dr);


                        }
                        db.BulkCopy(dt.AsEnumerable().Where(row => row.Field<String>("Error") == "Success").CopyToDataTable());
                    }

                     return dt.AsEnumerable().ToList().Take(1);
                }
                else
                {
                    //return BadRequest();
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
                //return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
