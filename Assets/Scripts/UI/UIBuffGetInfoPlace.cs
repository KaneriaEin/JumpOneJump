using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuffGetInfoPlace : MonoBehaviour
{
    public Text buffInfo;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTextandColor(string text, Color color)
    {
        //Debug.LogFormat("old: {0} \n new :{1}", buffInfo.color, color);
        buffInfo.text = text;
        buffInfo.color = color;
        //Debug.LogFormat("oldnew :{0}", buffInfo.color);
    }
}
