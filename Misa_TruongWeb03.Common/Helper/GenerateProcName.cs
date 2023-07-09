using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa_TruongWeb03.Common.Helper
{
    public class GenerateProcName
    {
        public static string Generate<TEntity>(string Type)
        {
            var table = typeof(TEntity).Name.ToLower();
            switch (Type)
            {
                case "Get":
                    return $"proc_{table}_get";
                case "GetById":
                    return $"proc_{table}_getdetail";
                case "Post":
                    return $"proc_{table}_insert";
                case "Put":
                    return $"proc_{table}_update";
                case "Delete":
                    return $"proc_{table}_delete";
                default:
                    return "";
            }
        }
    }
}
