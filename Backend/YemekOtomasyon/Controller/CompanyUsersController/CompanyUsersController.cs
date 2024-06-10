using Dapper;
using CrudProject.Common;
using CrudProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Data.Common;
using Microsoft.VisualBasic;
using System.Security.Cryptography;
using System.Text;

namespace CrudProject.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyUsersController : ControllerBase
    {

        private readonly DbHelper _dbHelper;

        public CompanyUsersController(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginModel userLogin)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                try
                {

                    userLogin.password = Helper.ComputeMD5Hash(userLogin.password);

                    string sql = @"SELECT *
                                   FROM company_users
                                   WHERE
                                       username = @username AND password= @password";
                    var  data = connection.Query<dynamic>(sql, userLogin).FirstOrDefault();
                   
                    if (data==null)
                    {
                        return BadRequest("Kullanici Adi ve Sifre Hatali");
                    }
                    else
                    {
                        var token = Guid.NewGuid().ToString("N");

                        sql = @"UPDATE company_users 
                            SET 
                                token = @token
                            WHERE
                                 id=@id ";

                        connection.Execute(sql, new { token , id=data.id });





                         sql = @"SELECT *
                                   FROM company_users
                                   WHERE
                                       id = @id";
                        var userData = connection.Query<dynamic>(sql,new {id= data.id}).FirstOrDefault();




                        return Ok(userData);

                    }


                }
                catch (Exception ex)
                {
                    return BadRequest(ResponseHelper.ExceptionResponse(ex.Message));
                }
                finally
                {
                    connection.Close();
                }
            }
        }

   


    }
}
