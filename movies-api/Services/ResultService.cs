using movies_api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace movies_api.Services
{
    public enum CodeEnum : int
    {
        ok = 200,
        Success = 0,
        Normal = 1,
        LogParameter = 2,
        DataError = 400,
        SqlError = 408,
        SystemError = 9000,
    }

    public class ResultFormat : JsonResult
    {
        private readonly string data;

        public ResultFormat(string data)
        {
            this.data = data;
        }

        public override void ExecuteResult(ControllerContext controllerContext)
        {
            if (controllerContext != null)
            {
                HttpResponseBase Response = controllerContext.HttpContext.Response;

                Response.ContentType = "application/json";
                if (data != null)
                {
                    Response.Write(data);
                }
            }
        }
    }

    public static class ResultService
    {
        private static JsonSerializerSettings jsonSerializerSettings
        {
            get
            {
                return new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    DateFormatString = "yyyy-MM-dd HH:mm:ss",
                };
            }
        }

        public static ResultFormat SuccessResult()
        {
            return new ResultFormat(JsonConvert.SerializeObject(new BaseResult<object>()
            {
                Code = (int)CodeEnum.Success,
                Msg = "Success",
                Data = null,
            }, jsonSerializerSettings));
        }

        public static ResultFormat SuccessResult<T>(T data)
        {
            return new ResultFormat(JsonConvert.SerializeObject(new BaseResult<T>()
            {
                Code = (int)CodeEnum.Success,
                Msg = "Success",
                Data = data,
            }, jsonSerializerSettings));
        }

        public static ResultFormat ErrorResult(CodeEnum errorCode, string message)
        {
            return new ResultFormat(JsonConvert.SerializeObject(new BaseResult<object>()
            {
                Code = (int)errorCode,
                Msg = message,
                Data = null,
            }, jsonSerializerSettings));
        }

        public static ResultFormat SuccessResultOk<T>(T data)
        {
            return new ResultFormat(JsonConvert.SerializeObject(new BaseResult<object>()
            {
                Code = (int)CodeEnum.ok,
                Msg = "Success",
                Data = data,
            }, jsonSerializerSettings));
        }

        public static string ErrorBaseResult(CodeEnum errorCode, string message)
        {
            return JsonConvert.SerializeObject(new BaseResult<object>()
            {
                Code = (int)errorCode,
                Msg = message,
                Data = null,
            }, jsonSerializerSettings);
        }

        internal static JsonResult SuccessResult<T>(bool hasChecked)
        {
            throw new NotImplementedException();
        }
    }
}