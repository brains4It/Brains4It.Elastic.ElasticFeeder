using System;
using System.Collections.Generic;
using System.Text;

namespace Brains4It.Elastic.ElasticFeeder.DataReader
{
    public abstract class BaseCsvReader
    {
        protected  string[] ParseCsv(string filename)
        {
            // Get the file's text.
            string whole_file = System.IO.File.ReadAllText(filename);

            // Split into lines.
            whole_file = whole_file.Replace('\n', '\r');
            return whole_file.Split(new char[] { '\r' },
                StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
