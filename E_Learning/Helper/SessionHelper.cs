using Newtonsoft.Json;


namespace E_Learning.Helper
{
    public static class SessionHelper
    {
        public static void SetObjectsJSON(this ISession session , string key , object value )
        {
            session.SetString(key, JsonConvert.SerializeObject(value));

        }

        public static T GetObjectsJSON<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }


    }
}
