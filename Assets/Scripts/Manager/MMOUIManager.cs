using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MMOUIManager : Singleton<MMOUIManager>
{
    class UIElement
    {
        public string Resources;
        public bool Cache;
        public GameObject Instance;
    }

    Dictionary<Type, UIElement> UIResources = new Dictionary<Type, UIElement> ();

    public MMOUIManager()
    {
        //this.UIResources.Add (typeof (UIElement), new UIElement ());
        this.UIResources.Add (typeof (UIShop), new UIElement () { Resources = "UI/UIShop", Cache = false });
    }

    public T Show<T>()
    {
        InputManager.Instance.InClickEvent();
        InputManager.Instance.duringUI++;

        Type type = typeof (T);
        if(this.UIResources.ContainsKey (type))
        {
            UIElement info = this.UIResources [type];
            if(info.Instance != null)
            {
                info.Instance.SetActive (true);
            }
            else
            {
                UnityEngine.Object prefab = Resources.Load(info.Resources);
                if(prefab == null)
                {
                    return default (T);
                }
                info.Instance = (GameObject)GameObject.Instantiate (prefab);
            }
            return info.Instance.GetComponent<T>();
        }
        return default(T);
    }

    public void Close(Type type)
    {
        if(this.UIResources.ContainsKey(type))
        {
            UIElement info = this.UIResources[type];
            if (info.Cache)
            {
                info.Instance.SetActive (false);
            }
            else
            {
                GameObject.Destroy (info.Instance);
                info.Instance = null;
            }
        }
    }

    public void Close<T>()
    {
        this.Close (typeof (T));
    }



}