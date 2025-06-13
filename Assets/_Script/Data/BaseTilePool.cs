using UnityEngine;
using System.Collections.Generic;

public abstract class BaseTilePool<T> : MonoBehaviour where T : MonoBehaviour
{
    public T prefab;
    public int initialSize = 10;
    public RectTransform canvas;

    private Queue<T> pool = new Queue<T>();

    protected virtual void Awake()
    {
        for (int i = 0; i < initialSize; i++)
        {
            T obj = Instantiate(prefab, transform);
            obj.gameObject.SetActive(false);
            ResetObject(obj);
            pool.Enqueue(obj);
        }
    }

    public T GetFromPool()
    {
        if (pool.Count > 0)
        {
            var item = pool.Dequeue();
            item.gameObject.SetActive(true);
            return item;
        }
        else
        {
            var item = Instantiate(prefab, transform);
            ResetObject(item);
            return item;
        }
    }

    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
    public abstract void ResetPool();
    protected abstract void ResetObject(T obj);
}
