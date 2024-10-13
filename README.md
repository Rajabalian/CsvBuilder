# CsvBuilder
تهیه فایل CSV از لیست ورودی

#Sample Code


    [HttpGet]
    [Route("CsvReport")]
    public IActionResult CsvReport()
    {

        var list = new List<ProductDto>
        {
            new()
            {
                Id = 1,
                Name = "Red Car",
                Price = 10000,
                Stock = 5
            },
            new()
            {
                Id = 2,
                Name = "Blue Car",
                Price = 50000,
                Stock = 3
            }
        };


        var bytes = new CsvBuilder().ExportAsBytes(list,
            new ValueTuple<string, Func<ProductDto, object?>>("name", i => i.Name.ToUpper()),
            new ValueTuple<string, Func<ProductDto, object?>>("price", i => i.Price.ToString("##,###")),
            new ValueTuple<string, Func<ProductDto, object?>>("stock", i => i.Stock)
        );

        return File(bytes, "application/csv", "cars.csv");
    }