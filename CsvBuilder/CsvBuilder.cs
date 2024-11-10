using System.Text;

namespace CsvBuilder
{
    public class CsvBuilder<T> where T : class
    {
        public record Column(string Title, Func<T, object?> Columns);


        private readonly List<Column> _columns = [];


        public CsvBuilder<T> AddColumn(string title, Func<T, object?> column)
        {
            _columns.Add(new Column(title,column));

            return this;
        }

        public byte[] ExportAsBytes (IEnumerable<T> list) 
        {

            if (!_columns.Any())
            {
                throw new Exception("befor call export csv,add columns need for export.");
            }

            var csvContent = new StringBuilder();

            //- heads
            for (var index = 0; index < _columns.Count; index++)
            {
                var property = _columns[index];
                csvContent.Append(EscapeComma(property.Title));
                if (index < _columns.Count - 1)
                {
                    csvContent.Append(',');
                }
            }
            csvContent.AppendLine("");


            //- body
            foreach (var item in list)
            {
                var index = 0;
                foreach (var value in _columns.Select(row => row.Columns(item)?.ToString()))
                {
                    csvContent.Append(EscapeComma(value));
                    if (index < _columns.Count - 1)
                    {
                        csvContent.Append(',');
                    }

                    index++;
                }

                csvContent.AppendLine("");
            }


            var data = Encoding.UTF8.GetBytes(csvContent.ToString());
            var result = Encoding.UTF8.GetPreamble().Concat(data).ToArray();
            return result;
        }

        private static string EscapeComma(string? str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }

            var mustQuote = str.Contains(",") || str.Contains("\"") || str.Contains("\r") || str.Contains("\n");
            return mustQuote ? $"\"{str.Replace("\"", "\"\"")}\"" : str;
        }


       
    }
}
