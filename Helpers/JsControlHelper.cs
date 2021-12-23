using Kara.Framework.Common.Helpers;
using Kara.Framework.Common.Mvc.JsGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kara.Framework.Common.Mvc.Helpers
{
    internal class JsControlHelper
    {
        internal static string ReplaceParamWithValue(Type propertyOfType, object param, string script)
        {
            var propertyInfos = propertyOfType.GetProperties();
            string toReplacePattern = "[{0}.{1}]";
            foreach (var property in propertyInfos)
            {
                try
                {
                    var propertyReplacePattern = toReplacePattern;
                    if (property.PropertyType.IsGenericType)
                        continue;

                    if (property.PropertyType != typeof(string) && property.PropertyType.IsClass)
                        continue;

                    var propertyAttributes = property.GetCustomAttributes(typeof(IgnoreOnParsing), false);
                    if (propertyAttributes != null && propertyAttributes.Count() != 0) continue;

                    string value = property.GetValue(param).ToString();
                    if (property.PropertyType == typeof(bool))
                        value = value.ToLower();
                    if (property.PropertyType.IsEnum)
                    {
                        value = ((Enum)Enum.Parse(property.PropertyType, value, true)).GetCode();
                        bool parseResult;
                        if (bool.TryParse(value, out parseResult))
                            propertyReplacePattern = string.Concat("\"", propertyReplacePattern, "\"");

                    }
                    script = script.Replace(string.Format(propertyReplacePattern, propertyOfType.Name, property.Name), value);

                }
                catch (Exception ex)
                {
                    //TODO: Logging
                    throw new ApplicationException("Mapping error for property: " + property.Name, ex);
                }
              
            }
            return script;
        }
    }
}
