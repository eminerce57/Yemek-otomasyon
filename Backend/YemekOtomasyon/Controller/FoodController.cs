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
    public class FoodController : ControllerBase
    {

        private readonly DbHelper _dbHelper;

        public FoodController(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }




        [HttpGet("")]
        public async Task<IActionResult> GetFood()
        {
            GetToken g = new GetToken(_dbHelper);
            var login = g.GetUserByToken(ControllerContext);
            if (!login.status)
                return BadRequest(ResponseHelper.UnAuthorizedResponse());

            using (var connection = _dbHelper.GetConnection())
            {
                try
                {
                    string sql = @"SELECT * FROM foods where is_active = true";

                    var List = connection.Query<dynamic>(sql).ToList();

                    return Ok(List);
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


        [HttpPost("add")]
        public async Task<IActionResult> addFood(FoodModel model)
        {
            GetToken g = new GetToken(_dbHelper);
            var login = g.GetUserByToken(ControllerContext);
            if (!login.status)
                return BadRequest(ResponseHelper.UnAuthorizedResponse());

            using (var connection = _dbHelper.GetConnection())
            {
                try
                {
                    model.created_user_id = login.data.id;
                    model.is_active = true; 
                    string sql = @"INSERT INTO foods (name,amount,created_user_id,is_active) VALUES(@name,@amount,@created_user_id,@is_active)";
                    connection.Execute(sql, model);


                    return Ok("Kayıt Başarıyla Eklendi");
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
