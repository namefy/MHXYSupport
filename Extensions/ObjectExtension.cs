using Newtonsoft.Json;

namespace MHXYSupport.Extensions;

public static class ObjectExtension
{
    public static string ToJson(this object source) => JsonConvert.SerializeObject(source);
}
