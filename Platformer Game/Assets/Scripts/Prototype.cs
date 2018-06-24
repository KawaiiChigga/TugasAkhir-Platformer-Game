using UnityEngine;
using System.Collections.Generic;
using System;


public class Prototype : MonoBehaviour
{

    public event Action OnReturnToPool;
    
    public bool isOriginalPrototype
    {
        get
        {
            return originalProto == null;
        }
    }

    public Prototype originalPrototype
    {
        get
        {
            return originalProto;
        }
    }

    void Start()
    {
        if (isOriginalPrototype)
            this.gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        if (instancePool != null)
            foreach (var inst in instancePool)
                Destroy(inst);
    }

    public T Instantiate<T>() where T : Component
    {
        
        Prototype instance = null;
        activeProto++;
        // Re-use instance
        if (instancePool != null && instancePool.Count > 0)
        {
            var instanceIdx = instancePool.Count - 1;
            instance = instancePool[instanceIdx];
            instancePool.RemoveAt(instanceIdx);
            instance.transform.localPosition = transform.localPosition;
            instance.transform.localRotation = transform.localRotation;
            instance.transform.localScale = transform.localScale;
            instance.transform.GetChild(0).localPosition = instance.transform.GetChild(0).position;
            instance.transform.GetChild(0).localRotation = instance.transform.GetChild(0).rotation;
            instance.transform.GetChild(0).localScale = instance.transform.GetChild(0).localScale;
            Debug.Log("TOTAL : " +  (activeProto + instancePool.Count));
            Debug.Log("INSTANCE : " + instancePool.Count);
            ReducePool(instancePool.Count);
        }
        // New instance
        else
        {
            instance = UnityEngine.Object.Instantiate(this);

            instance.transform.SetParent(transform.parent, worldPositionStays: false);

            var protoRT = transform as RectTransform;
            if (protoRT)
            {
                var instRT = instance.transform as RectTransform;
                instRT.anchorMin = protoRT.anchorMin;
                instRT.anchorMax = protoRT.anchorMax;
                instRT.pivot = protoRT.pivot;
                instRT.sizeDelta = protoRT.sizeDelta;
            }

            instance.transform.localPosition = transform.localPosition;
            instance.transform.localRotation = transform.localRotation;
            instance.transform.localScale = transform.localScale;

            instance.originalProto = this;
        }
        instance.gameObject.SetActive(true);
        return instance.GetComponent<T>();  
    }

    public void ReturnToPool()
    { 
        if (isOriginalPrototype)
        {
            Debug.Log("This prototype the original, can't return");
            Destroy(gameObject);
            return;
        }
        activeProto--;
        originalProto.AddToPool(this);
    }

    void AddToPool(Prototype instancePrototype)
    {
        if (!isOriginalPrototype)
            Debug.LogError("Can't add " + instancePrototype.name + " to prototype pool of " + this.name );

        instancePrototype.gameObject.SetActive(false);

        if (instancePool == null) instancePool = new List<Prototype>();
        instancePool.Add(instancePrototype);

        if (instancePrototype.OnReturnToPool != null)
            instancePrototype.OnReturnToPool();
    }

    public T GetOriginal<T>()
    {
        if (isOriginalPrototype)
            return GetComponent<T>();
        else
            return originalPrototype.GetComponent<T>();
    }

    void ReducePool(int inactiveProto)
    {
        if (inactiveProto + activeProto > 10 && activeProto < 10)
            {
            int x = inactiveProto + activeProto - 10;
                for (int i = 0; i < x; i++)
                {
                    Debug.Log("REMOVING "+ i);
                    Destroy(instancePool[i].gameObject);
                }
                for (int i = 0; i < x; i++)
                {
                    instancePool.RemoveAt(0);
                }
        }
            
    }
    Prototype originalProto;
    static int activeProto = 0;
    List<Prototype> instancePool;
}