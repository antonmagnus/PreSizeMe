using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreSizeMe.Controllers
{
    public static class CustomScopes
    {
        private readonly static String scopeMeasurement = "measurements";
        private readonly static String scopeGender = "gender";
        private readonly static String scopeName = "name";

        public static String Name()
        {
            return scopeName;
        }
        public static String PermissionName()
        {
            return "scp:" + Name();
        }
        public static String Measurements()
        {
            return scopeMeasurement;
        }
        public static String PermissionMeasurements()
        {
            return "scp:" + Measurements();
        }
        public static String Gender()
        {
            return scopeGender;
        }
        public static String PermissionGender()
        {
            return "scp:" + Gender();
        }
    }
}
