﻿using CqrsInAzure.Categories.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CqrsInAzure.Categories.Storage
{
    public interface ICategoriesStorage
    {
        Task AddAsync(Category category);

        Task<bool> IsEmptyAsync();

        Task<IEnumerable<Category>> GetAllAsync();

        Task<Category> GetAsync(string name);

        Task DeleteAsync(string name);

        Task UpdateAsync(string name, Category category);
    }
}