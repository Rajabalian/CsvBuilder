using System.Text;

namespace CsvBuilder
{
    public class CsvBuilder
    {

        public byte[] ExportAsBytes<T>(IEnumerable<T> list, params (string, Func<T, object?>)[] items) where T : class
        {
            var csvContent = new StringBuilder();

            //- heads
            for (var index = 0; index < items.Length; index++)
            {
                var property = items[index];
                csvContent.Append(EscapeComma(property.Item1));
                if (index < items.Length - 1)
                {
                    csvContent.Append(',');
                }
            }
            csvContent.AppendLine("");


            //- body
            foreach (var item in list)
            {
                var index = 0;
                foreach (var row in items)
                {
                    var value = row.Item2(item)?.ToString();
                    csvContent.Append(EscapeComma(value));
                    if (index < items.Length - 1)
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
