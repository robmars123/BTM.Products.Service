using BTM.Products.Application.Abstractions;
using BTM.Products.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTM.Products.Application.Commands
{
    public class AddProductCommand : ICommand
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        // Constructor for setting the properties
        public AddProductCommand(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}
