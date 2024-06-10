using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using static CrudProject.Common.Helper;
using CrudProject.Models;

namespace CrudProject.Common
{
    public class GetToken 
    {

        public readonly DbHelper _dbHelper;
        public  GetToken(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public apiReturn<UserModel> GetUserByToken(ControllerContext context)
        {


            string token = string.Empty;
            if (context != null)
                if (context.HttpContext != null)
                    if (context.HttpContext.Request != null)
                        if (context.HttpContext.Request.Headers != null)
                            token = getString(context.HttpContext.Request?.Headers["token"]);

            if (string.IsNullOrEmpty(token))
                return new apiReturn<UserModel>() { status = false, message = "Token Boş Olamaz" };

            using (var connection = _dbHelper.GetConnection())
            {
                try
                {
                    var sql = @"SELECT * FROM users WHERE token = @token  AND is_active = true;";
                    var user = connection.QuerySingleOrDefault<UserModel>(sql, new { token = token });
                    if (user != null)
                    {
                        user.password = null;
                        return new apiReturn<UserModel>() { status = true, data = user, message = "Token Doğru" };
                    }
                    else
                    {
                        return new apiReturn<UserModel>() { status = false, message = "Token Hatalı" };

                    }

                }
                catch (Exception ex)
                {
                    return new apiReturn<UserModel>() { status = false, message = ex.ToString() };

                }
            }
            return new apiReturn<UserModel>() { status = false, message = "Boşa Düştü Şaşırtıcı" };
        }






        public apiReturn<CompanyUsersModel> GetCompanyUserByToken(ControllerContext context)
        {


            string token = string.Empty;
            if (context != null)
                if (context.HttpContext != null)
                    if (context.HttpContext.Request != null)
                        if (context.HttpContext.Request.Headers != null)
                            token = getString(context.HttpContext.Request?.Headers["token"]);

            if (string.IsNullOrEmpty(token))
                return new apiReturn<CompanyUsersModel>() { status = false, message = "Token Boş Olamaz" };

            using (var connection = _dbHelper.GetConnection())
            {
                try
                {
                    var sql = @"SELECT * FROM company_users WHERE token = @token  AND is_active = true;";
                    var user = connection.QuerySingleOrDefault<CompanyUsersModel>(sql, new { token = token });
                    if (user != null)
                    {
                        user.password = null;
                        return new apiReturn<CompanyUsersModel>() { status = true, data = user, message = "Token Doğru" };
                    }
                    else
                    {
                        return new apiReturn<CompanyUsersModel>() { status = false, message = "Token Hatalı" };

                    }

                }
                catch (Exception ex)
                {
                    return new apiReturn<CompanyUsersModel>() { status = false, message = ex.ToString() };

                }
            }
            return new apiReturn<CompanyUsersModel>() { status = false, message = "Boşa Düştü Şaşırtıcı" };
        }





    }
}
