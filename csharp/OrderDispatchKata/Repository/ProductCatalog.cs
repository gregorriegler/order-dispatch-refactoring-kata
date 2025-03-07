using OrderDispatchKata.Domain;

namespace OrderDispatchKata.Repository;

public interface ProductCatalog
{
    Product getByName(string name);
}