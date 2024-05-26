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
    public class UserController : ControllerBase
    {

        private readonly DbHelper _dbHelper;

        public UserController(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }




        [HttpGet("user")]
        public async Task<IActionResult> GetUser()
        {
            //Token Kontrolü
            GetToken g = new GetToken(_dbHelper);
            var login = g.GetUserByToken(ControllerContext);
            if (!login.status)
                return BadRequest(ResponseHelper.UnAuthorizedResponse());

            using (var connection = _dbHelper.GetConnection())
            {
                try
                {
                    string sql = @"SELECT * FROM users where is_active = true";

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

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(UserModel user)
        {
            

            using (var connection = _dbHelper.GetConnection())
            {
                try
                {
                    //Check unique username
                    string sql = @"SELECT COUNT(*) 
                                   FROM users 
                                   WHERE 
                                       username = @username AND is_active = true";
                    int count = connection.QueryFirstOrDefault<int>(sql, user);
                    if (count > 0)
                    {
                        return BadRequest(ResponseHelper.ExceptionResponse("Aynı Kullanıcı Adına Sahip Bir Kullanıcı Var!"));
                    }
                    //
                    user.password = Helper.ComputeMD5Hash(user.password);

                    //Create User 
                    sql = @"INSERT INTO users (
                                name, surname, username, password, is_admin, is_active) 
                            VALUES (
                                @name, @surname, @username, @password, @is_admin, true)";
                    connection.QueryFirstOrDefault<int>(sql, user);

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



        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(UserModel user)
        {
            GetToken g = new GetToken(_dbHelper);
            var login = g.GetUserByToken(ControllerContext);
            if (!login.status)
                return BadRequest(ResponseHelper.UnAuthorizedResponse());

            using (var connection = _dbHelper.GetConnection())
            {
                try
                {
                    //Check unique username
                    string sql = @"SELECT COUNT(*) 
                                   FROM users 
                                   WHERE 
                                        username = @username AND id != @id";
                    int count = connection.QueryFirstOrDefault<int>(sql, user);
                    if (count > 0)
                    {
                        return BadRequest(ResponseHelper.ExceptionResponse("Aynı Kullanıcı Adına Sahip Bir Kullanıcı Var!"));
                    }
                    //
                    //Update User 
                    sql = @"UPDATE users 
                            SET 
                                name = @name, 
                                surname = @surname,
                                username = @username
                            WHERE
                                id = @id";

                    connection.Execute(sql, user);

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

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            GetToken g = new GetToken(_dbHelper);
            var login = g.GetUserByToken(ControllerContext);
            if (!login.status)
                return BadRequest(ResponseHelper.UnAuthorizedResponse());

            using (var connection = _dbHelper.GetConnection())
            {
                try
                {
                    string sql = @"SELECT COUNT(*)
                                   FROM users
                                   WHERE
                                        id = @id AND is_active = false";

                    int count = connection.QueryFirstOrDefault<int>(sql, new { id });
                    if (count > 0)
                    {
                        return BadRequest(ResponseHelper.ExceptionResponse("Kullanıcı Zaten Silinmiş!"));
                    }
                    //Delete User
                    sql = @"UPDATE users 
                            SET 
                                username = CONCAT('DELETED-',username), 
                                is_active = false
                            WHERE
                                id = @id AND is_active = true";

                    connection.Execute(sql, new { id });

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

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginModel userLogin)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                try
                {
                    string sql = @"SELECT COUNT(*)
                                   FROM users
                                   WHERE
                                        is_active = true AND username = @username";

                    int count = connection.QueryFirstOrDefault<int>(sql, userLogin);
                    if (count == 0)
                    {
                        return Unauthorized(new { message = "Kullanıcı Adı Bulunamadı!" });
                    }
                    //
                    userLogin.password = Helper.ComputeMD5Hash(userLogin.password);
                    //
                    count = 0;
                    sql = @"SELECT COUNT(*) 
                            FROM users  
                            WHERE
                                username = @username AND password = @password";
                    
                    count = connection.QueryFirstOrDefault<int>(sql, userLogin);

                    if (count == 0)
                    {
                        return Unauthorized(new { message = "Şifre Bulunamadı!" });
                    }

                    var token = Guid.NewGuid().ToString("N");
                    var token_time = DateTime.Now;

                    sql = @"UPDATE users 
                            SET 
                                token = @token, 
                                token_time = @token_time
                            WHERE
                                username = @username AND password = @password";

                    connection.Execute(sql, new { token, token_time, userLogin.username, userLogin.password });

                    return Ok(new { token });
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

        [HttpPut("state/{id}")]
        public async Task<IActionResult> UserState(UserStateModel state, int id)
        {
            GetToken g = new GetToken(_dbHelper);
            var login = g.GetUserByToken(ControllerContext);
            if (!login.status)
                return BadRequest(ResponseHelper.UnAuthorizedResponse());

            using (var connection = _dbHelper.GetConnection())
            {
                try
                {
                    string sql = string.Empty;
                    if (state.is_active)
                    {
                        sql = @"UPDATE users 
                            SET 
                                is_active = false
                            WHERE
                                id = @id";
                    }
                    else
                    {
                        sql = @"UPDATE users 
                            SET 
                                is_active = true
                            WHERE
                                id = @id";
                    }
                    connection.Execute(sql, new { id, state.is_active });
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
