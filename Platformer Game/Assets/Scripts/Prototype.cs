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
            return _originalPrototype == null;
        }
    }

    public Prototype originalPrototype
    {
        get
        {
            return _originalPrototype;
        }
    }

    void Start()
    {
        if (isOriginalPrototype)
            this.gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        if (_instancePool != null)
            foreach (var inst in _instancePool)
                Destroy(inst);
    }

    public T Instantiate<T>() where T : Component
    {
        Prototype instance = null;

        // Re-use instance from pool
        if (_instancePool != null && _instancePool.Count > 0)
        {
            var instanceIdx = _instancePool.Count - 1;
            instance = _instancePool[instanceIdx];
            _instancePool.RemoveAt(instanceIdx);
            instance.transform.localPosition = transform.localPosition;
            instance.transform.localRotation = transform.localRotation;
            instance.transform.localScale = transform.localScale;
            instance.transform.GetChild(0).localPosition = instance.transform.GetChild(0).localPosition;
            instance.transform.GetChild(0).localRotation = instance.transform.GetChild(0).localRotation;
            instance.transform.GetChild(0).localScale = instance.transform.GetChild(0).localScale;
        }

        // Instantiate fresh instance
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

            instance._originalPrototype = this;
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

        _originalPrototype.AddToPool(this);
    }

    public T GetOriginal<T>()
    {
        if (isOriginalPrototype)
            return GetComponent<T>();
        else
            return originalPrototype.GetComponent<T>();
    }

    void AddToPool(Prototype instancePrototype)
    {
        if (!isOriginalPrototype)
            Debug.LogError("Can't add " + instancePrototype.name + " to prototype pool of " + this.name );

        instancePrototype.gameObject.SetActive(false);

        if (_instancePool == null) _instancePool = new List<Prototype>();
        _instancePool.Add(instancePrototype);

        if (instancePrototype.OnReturnToPool != null)
            instancePrototype.OnReturnToPool();
    }

    Prototype _originalPrototype;

    List<Prototype> _instancePool;
}