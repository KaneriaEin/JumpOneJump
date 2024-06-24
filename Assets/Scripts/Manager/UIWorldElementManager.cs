using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class UIWorldElementManager : MonoBehaviour
{
    public static UIWorldElementManager Instance;

    public GameObject logoGameStart;

    private Dictionary<Transform, GameObject> elementNames = new Dictionary<Transform, GameObject>();


    private void Awake()
    {
        Instance = this;
    }

    public void AddLogoGameStart(Transform owner)
    {
        UnityEngine.Profiling.Profiler.BeginSample("AddLogoGameStart");
        GameObject logo = Instantiate(logoGameStart, this.transform);
        logo.GetComponent<UIWorldElement>().owner = owner;
        logo.SetActive(true);
        this.elementNames[owner] = logo;
        UnityEngine.Profiling.Profiler.EndSample();

    }

    public void RemoveLogoGameStart(Transform owner)
    {
        if (this.elementNames.ContainsKey(owner))
        {
            Destroy(this.elementNames[owner]);
            this.elementNames.Remove(owner);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
