using Newtonsoft.Json;

namespace MHXYSupport.Extensions;

public static class StringExtension
{
    public static T ToEntity<T>(this string source) => JsonConvert.DeserializeObject<T>(source);
}
