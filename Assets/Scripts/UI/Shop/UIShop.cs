using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class UIShop : UIWindow
{
    public Text title;
    public Text money;

    public GameObject shopItem;
    public ShopDefine shop;
    public Transform[] itemRoot;

    private UIShopItem selectedItem;
    public void SelectShopItem(UIShopItem item)
    {
        if (selectedItem != null)
            selectedItem.Selected = false;
        selectedItem = item;
    }


    private void Start()
    {
        StartCoroutine(InitItems());
    }

    IEnumerator InitItems()
    {
        int count = 0;
        int page = 0;
        foreach (var kv in DataManager.Instance.ShopItems[this.shop.ID]) 
        {
            GameObject go = Instantiate(shopItem, itemRoot[page]);
            UIShopItem ui = go.GetComponent<UIShopItem>();
            ui.SetShopItem(kv.Key, kv.Value, this);
            count++;
            if(count >= 10)
            {
                count = 0;
                page++;
                itemRoot[page].gameObject.SetActive(true);
            }
        }
        yield return null;
    }

    public void SetShop(ShopDefine shop)
    {
        this.shop = shop;
        this.title.text = shop.Name;
        this.money.text = Player.Instance.Gold.ToString();
    }

    public override void OnCloseClick()
    {
        base.OnCloseClick();
        InputManager.Instance.duringUI--;
        InputManager.Instance.InGamingBattleMode();
    }

    public void OnClickBuy()
    {
        if (this.selectedItem == null)
        {
            return;
        }
        if (!ShopManager.Instance.BuyItem(this.shop.ID, this.selectedItem.ShopItemID))
        {
            Debug.LogFormat("OnClickBuy £º BuyItem failed!");
        }
        else
        {
            this.money.text = Player.Instance.Gold.ToString();
        }
    }
}
