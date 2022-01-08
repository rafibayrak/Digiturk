using System.IO;
using System.Reflection;

namespace MovieApp.Data.Core
{
    public class AppSettings
    {
        public string SecretKey { get; set; }
        public string WorkingDirectory { get; set; } = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
    }
}
