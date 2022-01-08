using MovieApp.Data.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace MovieApp.DataAccess.Utilities
{
    public class DummyJsonDeserialize<T>
    {
        public List<T> GetJsonValues(string workingDirectory, string jsonFileName)
        {
            List<T> values = null;
            var userDataPath = Path.Combine(workingDirectory, "DummyDatas", $"{jsonFileName}.json");
            using (StreamReader r = new StreamReader(userDataPath))
            {
                string json = r.ReadToEnd();
                values = JsonConvert.DeserializeObject<List<T>>(json);
            }

            return values;
        }
    }
}
