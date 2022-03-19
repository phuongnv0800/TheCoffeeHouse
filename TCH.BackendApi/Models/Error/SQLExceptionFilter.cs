using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace TCH.BackendApi.Models.Error
{
    public class SQLExceptionFilter : IExceptionFilter
    {
        ILogger<SQLExceptionFilter> _logger;
        public SQLExceptionFilter(ILogger<SQLExceptionFilter> logger)
        {
            _logger = logger;
        }

        public static void AddFileCheckSQL(Exception ex)
        {
            if (ex.GetType() == typeof(SqlException))
            {
                var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfigurationRoot configuration = builder.Build();

                if (!IsServerConnected(configuration.GetConnectionString("DefaultConnection")))
                {
                    string fileName = @"checkSQLconnect.xpcmd";

                    if (!File.Exists(fileName))
                    {
                        FileStream fs = File.Create(fileName);
                        fs.Close();
                    }
                }
            }

        }

        public static void AddFileCheckSQLNoCheck()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();

            if (!IsServerConnected(configuration.GetConnectionString("DefaultConnection")))
            {
                string fileName = @"checkSQLconnect.xpcmd";
                if (!File.Exists(fileName))
                {
                    FileStream fs = File.Create(fileName);
                    fs.Close();
                }
            }
        }


        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception.ToString());

            HttpStatusCode status = HttpStatusCode.BadRequest;
            string message = string.Empty;

            var exceptionType = context.Exception.GetType();
            if (exceptionType == typeof(SqlException))
            {
                message = "SQL Exception";
                status = HttpStatusCode.BadRequest;

                AddFileCheckSQLNoCheck();
            }
            else
            {
                message = "A server error occurred.";
                status = HttpStatusCode.InternalServerError;
            }

            context.ExceptionHandled = true;

            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";

            response.WriteAsync(JsonConvert.SerializeObject(new RespondException<string>() { Result = -2, Message = message, Data = context.Exception.Message }));
        }

        private static bool IsServerConnected(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }
    }

    public class RespondException<T> where T : class
    {
        public int Result { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
