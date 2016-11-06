using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Whisperer.Extensions
{
    public static class UriBuilderExtensions
    {
        public static UriBuilder Append(this UriBuilder uriBuilder, string name, string value)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name", "`Parameter name must be set");

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("value", "`Parameter value must be set");

            var queryToAppend = string.Concat(name, "=", value);

            if (uriBuilder.Query != null && uriBuilder.Query.Length > 1)
                uriBuilder.Query = uriBuilder.Query.Substring(1) + "&" + queryToAppend;
            else
                uriBuilder.Query = queryToAppend;

            return uriBuilder;
        }
    }
}