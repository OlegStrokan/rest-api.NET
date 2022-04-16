using System;
using System.Collections.Generic;
using mvc.NET.Models;

namespace mvc.NET.Repositories;

public interface IItemsRepository
{
    Item GetItem(Guid id);

    IEnumerable<Item> GetItems();
}