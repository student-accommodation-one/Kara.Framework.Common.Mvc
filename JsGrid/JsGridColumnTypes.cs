using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kara.Framework.Common.Helpers;

namespace Kara.Framework.Common.Mvc.JsGrid
{
    public enum JsGridColumnTypes
    {
        [Code("string")]
        [JsGridColumnTypeMetadata(StandardFormat = "")]
        String,
        [Code("number")]
        [JsGridColumnTypeMetadata(StandardFormat = "")]
        NonDecimal,
        [Code("number")]
        [JsGridColumnTypeMetadata(StandardFormat = "")]
        Decimal,
        [Code("date")]
        [JsGridColumnTypeMetadata(StandardFormat = "d")]
        Date,
        [Code("date")]
        [JsGridColumnTypeMetadata(StandardFormat = "g")]
        DateTime,
        [Code("number")]
        [JsGridColumnTypeMetadata(StandardFormat = "c")]
        Money,
        [Code("date")]
        [JsGridColumnTypeMetadata(StandardFormat = "HH:mm")]
        Time
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    class JsGridColumnTypeMetadata : Attribute
    {
        public JsGridColumnTypeMetadata()
        {
            this.StandardFormat = "";
        }

        public string StandardFormat { get; set; }
       
    }


    public static class JsGridColumnTypeExtensions
    {
        public static string ToStandardFormat(this JsGridColumnTypes ct)
        {
            var metadata = ct.GetCustomAttribute<JsGridColumnTypeMetadata>();
            return (metadata != null) ? ((JsGridColumnTypeMetadata)metadata).StandardFormat : ct.ToString();
        }
    }
}
