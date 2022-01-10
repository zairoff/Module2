using System;
using Task1.Enums;

namespace Task1.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheAttribute : Attribute
    {
        public CacheType CacheBy { get; set; }
    }
}
