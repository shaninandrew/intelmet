using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SlabMovement
{
    public interface IReader
    {
        public DataModel Read(string url);
    }

    /// <summary>
    /// Файловый загрузчик
    /// </summary>
    public class FileReader : IReader
    {
        public DataModel Read(string url)
        {
            DataModel dataModel = new DataModel();
            string s =  System.IO.File.ReadAllText(url, Encoding.UTF8);
            JsonSerializerOptions opt = new JsonSerializerOptions();

            opt.AllowTrailingCommas = true;
            opt.PropertyNameCaseInsensitive = true; 
            dataModel = JsonSerializer.Deserialize<DataModel>(s, opt);

            return dataModel;
        
        }
    }

    /// <summary>
    /// Сетевой загрузчик
    /// </summary>
    public class NetworkReader : IReader
    {
        public DataModel Read(string url)
        {
            System.Net.WebClient web = new System.Net.WebClient();
            string s = web.DownloadString (url);
            web.Dispose();
            DataModel dataModel = JsonSerializer.Deserialize<DataModel>(s);

            return dataModel;

        }
    }


}
