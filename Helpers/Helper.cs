using System.Web.Script.Serialization;

namespace Helpers
{
    public static class Helper
    {
        public static string ToJson(this object obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }
    }
}
