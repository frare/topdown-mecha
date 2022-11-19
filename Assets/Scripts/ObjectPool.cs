using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int amount = 10;
    [SerializeField] private bool poolAsChild = false;
    [SerializeField] private bool dynamicSize = true;
    private List<GameObject> pooledObjects = new List<GameObject>();



    #region UNITY CALLBACKS
    private void Awake()
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject newObj = poolAsChild ? Instantiate(prefab, transform) : Instantiate(prefab);
            newObj.SetActive(false);
            
            pooledObjects.Add(newObj);
        }
    }
    #endregion



    #region CLASS METHODS
    public GameObject GetNext()
    {
        foreach (GameObject obj in pooledObjects)
        {
            if (!obj.activeSelf) return obj;
        }

        if (dynamicSize)
        {
            GameObject newObj = poolAsChild ? Instantiate(prefab, transform) : Instantiate(prefab);
            newObj.SetActive(false);
            
            pooledObjects.Add(newObj);
            return newObj;
        }
        else
        {
            return null;
        }
    }

    public List<GameObject> GetAll()
    {
        return pooledObjects;
    }
    #endregion
}