using System;
using System.Collections.Generic;
using System.Text;

using System.Reflection;
using System.Runtime.Versioning;

namespace Helpers
{
    public static class AssemblyInfo
    {
        public static string RuntimeVersion()
        {
            var frameworkversion = System.Reflection.Assembly
                            .GetEntryAssembly()?.GetCustomAttribute<System.Runtime.Versioning.TargetFrameworkAttribute>()?
                            .FrameworkName;
            return frameworkversion;
        }
    }
}
