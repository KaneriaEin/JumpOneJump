using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuffItem : MonoBehaviour
{
    public Image icon;
    public Image overlay;
    //public Text label;
    public Buff buff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.buff == null) return;
        if (this.buff.restTime > 0)
        {
            if (this.buff.restTime > 0) overlay.enabled = true;
            //if (!label.enabled) label.enabled = true;

            overlay.fillAmount = 1 - this.buff.restTime / this.buff.totalTime;
            //this.label.text = ((int)Math.Ceiling(this.buff.Define.Duration - this.buff.time)).ToString();
        }
        else
        {
            if (overlay.enabled) overlay.enabled = false;
            //if (this.label.enabled) this.label.enabled = false;
        }
    }

    public void SetItem(Buff buff)
    {
        this.buff = buff;
        if (this.icon != null)
        {
            this.icon.overrideSprite = Resloader.Load<Sprite>(this.buff.IconResource);
            this.icon.SetAllDirty();
        }
    }
}
