﻿using System;

namespace Aop.Proxy
{
    class ProductsProvider
    {
        public virtual Product GetProduct(ProductType productType, int take, int skip, SortType sortType)
        {
            Product product = GetFromDb(productType, take, skip, sortType);

            return product;
        }

        protected bool CheckForChangesCallback(DateTime storeDateTime, object storedObject)
        {
            return true;
        }

        private Product GetFromDb(ProductType productType, int take, int skip, SortType sortType)
        {
            throw new System.NotImplementedException();
        }
    }


    class ProductsProviderProxy : ProductsProvider
    {
        public override Product GetProduct(ProductType productType, int take, int skip, SortType sortType)
        {
            string cacheKey = Cache.CreateCacheKey(nameof(GetProduct), productType, take, skip, sortType);

            if (Cache.Contains(cacheKey) && Cache.IsActual(cacheKey, CheckForChangesCallback))
            {
                return Cache.Get<Product>(cacheKey);
            }

            Product product = base.GetProduct(productType, take, skip, sortType);

            Cache.Store(cacheKey, product);

            return product;
        }
    }

    internal class Product
    {
    }

    internal static class Cache
    {
        public static string CreateCacheKey(string methodName, params object[] args) 
        {
            throw new System.NotImplementedException();
        }

        public static bool Contains(string cacheKey)
        {
            throw new System.NotImplementedException();
        }

        public static T Get<T>(string cacheKey)
        {
            throw new System.NotImplementedException();
        }

        public static void Store<T>(string cacheKey, T t)
        {
            throw new System.NotImplementedException();
        }

        public static bool IsActual(string cacheKey, Func<DateTime, object, bool> chek)
        {
            throw new NotImplementedException();
        }
    }

    internal class SortType
    {
    }

    internal enum ProductType
    {
    }
}
