using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIconItem : MonoBehaviour
{
    public Image mainImage;

    public Text mainText;

    public void SetMainIcon(string path, string text)
    {
        this.mainImage.overrideSprite = Resloader.Load<Sprite>(path);
        this.mainText.text = text;
    }
}
