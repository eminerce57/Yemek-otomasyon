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
    public class OrderController : ControllerBase
    {

        private readonly DbHelper _dbHelper;

        public OrderController(DbHelper dbHelper)
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
                    string sql = @"SELECT 
  C.NAME,
  o.order_date,
    o.is_okey,
  json_agg(json_build_object('name', f.name, 'amount', f.amount)) as food_names
FROM 
  ""order"" AS o
  LEFT OUTER JOIN food_menu_item AS fm ON fm.order_id = o.id
  LEFT OUTER JOIN foods AS f ON f.id = fm.food_id
  LEFT OUTER JOIN company AS C ON C.ID = o.company_id
GROUP BY 
  C.NAME,o.is_okey, o.order_date;
";

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
        public async Task<IActionResult> AddOrder(OrderModel model)
        {
            GetToken g = new GetToken(_dbHelper);
            var login = g.GetUserByToken(ControllerContext);
            if (!login.status)
                return BadRequest(ResponseHelper.UnAuthorizedResponse());

            using (var connection = _dbHelper.GetConnection())
            {
                try
                {
                    model.is_okey = false;
                    model.order_date = DateTime.Now;
                    string sql = @"INSERT INTO ""order"" (company_id, order_date, is_okey) VALUES (@company_id, @order_date, false) RETURNING id";

                    int order_id = connection.Query<int>(sql, model).FirstOrDefault();

                    foreach (var item in model.foods)
                    {
                        string FoodSql = @"INSERT INTO food_menu_item (food_id, order_id) VALUES (@food_id, @order_id)";
                        connection.Execute(FoodSql, new { food_id = item.id, order_id = order_id });
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
