# CsvBuilder
تهیه فایل CSV از لیست ورودی

#Sample Code

    public class CsvDownloadController : ControllerBase
    {

        public record Product(string Name, int Price, int Stock);


        [HttpGet]
        public IActionResult Get()
        {

            var list = new List<Product>()
            {
                new("BMW 3 Series (320i, 2018)",8600,5),
                new("BMW 5 Series (530i, 2018)",18000,3),
                new("Porsche 718 Boxster (2017)",21000,1),
                new("Porsche Macan (2018)",21000,0)
            };

            var csvBuilder = new CsvBuilder.CsvBuilder<Product>()
                .AddColumn("Name", i => i.Name.ToUpper())
                .AddColumn("Price", i => i.Price.ToString("C"))
                .AddColumn("Stock", i => i.Stock == 0 ? "-" : i.Stock)
                .ExportAsBytes(list);

            using var memoryStream = new MemoryStream(csvBuilder);
            return File(memoryStream.ToArray(), "application/octet-stream", "file.csv");
        }
    }
