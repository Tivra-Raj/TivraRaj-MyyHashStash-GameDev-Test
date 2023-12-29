﻿using System.Collections.Generic;

namespace Utilities
{
    public abstract class GenericObjectPool<T> where T : class
    {
        public List<PooledItem<T>> pooledItems = new List<PooledItem<T>>();

        public virtual T GetItem()
        {
            if (pooledItems.Count > 0)
            {
                PooledItem<T> item = pooledItems.Find(item => !item.isUsed);
                if (item != null)
                {
                    item.isUsed = true;
                    return item.Item;
                }
            }
            return CreateNewPooledItem();
        }

        private T CreateNewPooledItem()
        {
            PooledItem<T> newItem = new PooledItem<T>();
            newItem.Item = CreateItem();
            newItem.isUsed = true;
            pooledItems.Add(newItem);
            return newItem.Item;
        }

        protected abstract T CreateItem();

        public virtual void ReturnItem(T item)
        {
            PooledItem<T> pooledItem = pooledItems.Find(i => i.Item.Equals(item));
            if (pooledItem != null)
            {
                pooledItem.isUsed = false;
            }

        }

        public class PooledItem<T>
        {
            public T Item;
            public bool isUsed;
        }
    }
}