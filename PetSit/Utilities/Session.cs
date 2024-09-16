using System;
using System.Web;

namespace Utilities
{
    public static class SessionUtility
    {
        // Get session value by key
        public static object Get(string key)
        {
            return HttpContext.Current.Session[key] ?? null;
        }

        // Put value in session with key
        public static void Put(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        // Check if the session has the key
        public static bool Has(string key)
        {
            return HttpContext.Current.Session[key] != null;
        }

        // Flash session value (stored temporarily, removed after retrieval)
        public static void Flash(string key, object value)
        {
            Put($"_flash_{key}", value);
        }

        // Get flashed value and remove it from the session
        public static object GetFlash(string key)
        {
            var value = HttpContext.Current.Session[$"_flash_{key}"];
            if (value != null)
            {
                HttpContext.Current.Session.Remove($"_flash_{key}");
            }
            return value;
        }

        // Clear all session values
        public static void Flush()
        {
            HttpContext.Current.Session.Clear();
        }
    }
}