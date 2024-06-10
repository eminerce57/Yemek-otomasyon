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
    public class CompanyOrderController : ControllerBase
    {

        private readonly DbHelper _dbHelper;

        public CompanyOrderController(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }


        [HttpGet("")]
        public async Task<IActionResult> GetOrders()
        {
            GetToken g = new GetToken(_dbHelper);
            var login = g.GetCompanyUserByToken(ControllerContext);
            if (!login.status)
                return BadRequest(ResponseHelper.UnAuthorizedResponse());

            using (var connection = _dbHelper.GetConnection())
            {
                try
                {
                    string sql = @"SELECT 
  C.NAME,
  o.order_date,
o.id,
    o.is_okey,
  json_agg(json_build_object('name', f.name,'id', f.id, 'amount', f.amount)) as food_names
FROM 
  ""order"" AS o
  LEFT OUTER JOIN food_menu_item AS fm ON fm.order_id = o.id
  LEFT OUTER JOIN foods AS f ON f.id = fm.food_id
  LEFT OUTER JOIN company AS C ON C.ID = o.company_id
WHERE o.company_id=@id
GROUP BY 
  C.NAME,o.is_okey,o.id, o.order_date;
";
                    var List = connection.Query<dynamic>(sql, new { id = login.data.company_id } ).ToList();

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

        [HttpPost("")]
        public async Task<IActionResult> AddFood(OrderAddModel model)
        {
            GetToken g = new GetToken(_dbHelper);
            var login = g.GetCompanyUserByToken(ControllerContext);
            if (!login.status)
                return BadRequest(ResponseHelper.UnAuthorizedResponse());

            using (var connection = _dbHelper.GetConnection())
            {
                try
                {


                    foreach (var item in model.food_names)
                    {
                        string sql = @"INSERT INTO food_menu_user_item  ( food_menu_id,company_user_id,order_id) VALUES (@food_menu_id,@company_user_id,@order_id)
                                                ";
                        connection.Execute(sql, new { food_menu_id = item.id, company_user_id=login.data.id,order_id=model.order_id }) ;
                    }
                   

                    return Ok();
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
