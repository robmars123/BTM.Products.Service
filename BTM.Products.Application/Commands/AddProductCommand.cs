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
        public string Name { get; }
        public decimal Price { get; }

        public AddProductCommand(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}
