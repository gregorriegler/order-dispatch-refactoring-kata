using System.Collections.Generic;
using System.Linq;
using OrderDispatchKata.Domain;
using OrderDispatchKata.Repository;

namespace OrderDispatchKata.Tests.Doubles;

public class InMemoryProductCatalog : ProductCatalog
{
    private readonly List<Product> products;

    public InMemoryProductCatalog(List<Product> products)
    {
        this.products = products;
    }

    public Product getByName(string name)
    {
        return products.FirstOrDefault(p => p.getName().Equals(name));
    }
}