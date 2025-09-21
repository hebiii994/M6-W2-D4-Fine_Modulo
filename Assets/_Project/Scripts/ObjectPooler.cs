using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance; //sconsigliata come cosa?

    [SerializeField] private GameObject _objectToPool;
    [SerializeField] private int _initialPoolSize = 20;

    private List<GameObject> _pooledObjects;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _pooledObjects = new List<GameObject>();
        for (int i = 0; i < _initialPoolSize; i++)
        {
            GameObject obj = Instantiate(_objectToPool);
            obj.SetActive(false);
            _pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        foreach(GameObject obj in _pooledObjects)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        GameObject newObj = Instantiate(_objectToPool);
        newObj.SetActive(false);
        _pooledObjects.Add(newObj);
        return newObj;
    }
}
