using Newtonsoft.Json;

namespace MHXYWF.Extensions;

public static class ObjectExtension
{
    public static string ToJson(this object source) => JsonConvert.SerializeObject(source);
}
