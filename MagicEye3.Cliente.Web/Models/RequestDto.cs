﻿//using Microsoft.AspNetCore.Mvc;
using static MagicEye3.Cliente.Web.Utility.SD;

namespace MagicEye3.Cliente.Web.Models
{
    public class RequestDto
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data {  get; set; }
        public string AccesToken { get; set; }
    }
}
