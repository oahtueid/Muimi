using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : PersistentManager<ObjectPool>
{
    Dictionary<string, Queue<GameObject>> poolDictionary = new();

    [SerializeField]
    int maxPoolSize = 5;

    public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion quaternion)
    {
        if (!poolDictionary.ContainsKey(prefab.name))
        {
            poolDictionary[prefab.name] = new Queue<GameObject>();
        }
        GameObject obj;
        try
        {
            if (poolDictionary[prefab.name].Count > maxPoolSize)
            {
                obj = poolDictionary[prefab.name].Dequeue();
                obj.transform.SetPositionAndRotation(position, quaternion);
            }
            else
            {
                obj = Instantiate(prefab, position, quaternion);
            }
        }
        catch
        {
            obj = Instantiate(prefab, position, quaternion);
        }
        obj.SetActive(true);
        return obj;
    }

    public void Despawn(GameObject obj, string prefabKeyName)
    {
        obj.SetActive(false);
        if (!poolDictionary.ContainsKey(prefabKeyName))
        {
            poolDictionary[prefabKeyName] = new Queue<GameObject>();
        }
        poolDictionary[prefabKeyName].Enqueue(obj);
    }
}
