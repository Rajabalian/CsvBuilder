# CsvBuilder
تهیه فایل CSV از لیست ورودی

#Sample Code

var bytes = new CsvBuilder<ProductDto>()
         .AddColumn("name", i => i.Name.ToUpper())
		 .AddColumn("price", i => i.Price.ToString("##,###"))
		 .AddColumn("stock", i => i.Stock)
		 .ExportAsBytes(list);
