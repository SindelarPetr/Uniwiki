using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uniwiki.Client.Host.Extensions
{
    public static class ObjectExtensions
    {
        public static void CopyPropertiesTo<TObject>(this TObject sourceObject, TObject targetObject) 
            => typeof(TObject)
                .GetProperties()
                .Where(p => p.CanWrite)
                .ToList()
                .ForEach(p => p.SetValue(targetObject, p.GetValue(sourceObject, null), null));
    }
}
