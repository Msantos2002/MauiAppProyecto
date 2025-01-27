using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppProyecto.Services
{

    public static class APISetting
    {
        public static string GetBaseURL()
        {
            var url = DeviceInfo.Platform == DevicePlatform.Android
                ? "http://10.0.2.2:5027/api/"
                : "http://localhost:5027/api/";
            return url;

        }
    }
}
