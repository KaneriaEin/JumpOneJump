using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIShopItem : MonoBehaviour, ISelectHandler
{
    public Image icon;
    public Text title;
    public Text price;
    public Text limitClass;
    public Text count;

    public Image background;
    public Sprite normalBg;
    public Sprite selectedBg;

    public int ShopItemID { get; set; }
    private UIShop shop;

    private ItemDefine item;
    private ShopItemDefine shopItem { get; set; }

    private bool selected;
    public bool Selected
    {
        get { return selected; }
        set
        {
            selected = value;
            this.background.overrideSprite = selected ? selectedBg: normalBg;
        }
    }


    public void SetShopItem(int id, ShopItemDefine shopItem, UIShop owner)
    {
        this.ShopItemID = id;
        this.shopItem = shopItem;
        this.shop = owner;
        this.item = DataManager.Instance.Items[shopItem.ItemID];

        this.title.text = item.Name;
        this.price.text = shopItem.Price.ToString();
        this.count.text = "x" + this.shopItem.Count.ToString();
        this.limitClass.text = this.item.LimitClass.ToString();
        this.icon.overrideSprite = Resloader.Load<Sprite>(item.Icon);
    }
    
    public void OnSelect(BaseEventData eventData)
    {
        this.Selected = true;
        this.shop.SelectShopItem(this);
    }
}
