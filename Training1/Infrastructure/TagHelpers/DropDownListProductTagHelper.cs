using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training1.Models;
using Training1.Repositories;
using Training1.Repositories.Interfaces;

namespace Training1.Infrastructure.TagHelpers
{
    public class DropDownListProductTagHelper : TagHelper
    {
        private readonly IProductRepository _productRepository;
        public DropDownListProductTagHelper(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public override async Task ProcessAsync(TagHelperContext context,
                TagHelperOutput output)
        {
            output.TagName = "ul";
            IEnumerable<Product> products = await _productRepository.ListAsync();
            string result = String.Empty;
            foreach(Product product in products.OrderBy(p => p.Name))
            {
                result += $@"<li class='dropdown-item' id='{product.Id}'>{product.Name}</li>";
            }

            output.Content.SetHtmlContent(result);
        }
    }
}
