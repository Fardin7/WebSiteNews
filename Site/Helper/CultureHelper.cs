using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

using System.Web.SessionState;

namespace Site.Helper
{
    public class CultureHelper
    {
        public static CultureInfo CultureInfo;

        public static string EnumLocalizeValueToName(string value)
        {
          
            var enumerator = Resource.Resource.ResourceManager.GetResourceSet(CultureInfo, false, false).GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Value.ToString() == value)
                {
                    return enumerator.Key.ToString();
                }

            }
            return value;

        }
        public static string EnumLocalize(string name)
        {
            return Resource.Resource.ResourceManager.GetString(name, CultureInfo);

        }

    }
}