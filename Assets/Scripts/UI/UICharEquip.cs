using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharEquip : UIWindow
{
    public Text title;
    public Text money;

    public GameObject itemPrefab;
    public GameObject itemEquipedPrefab;

    public Transform itemListRoot;

    public List<Transform> slots;

    public Text hp;
    public Slider hpBar;

    public Text mp;
    public Slider mpBar;

    public Text[] attrs;

    public UIEquipItem selectedItem;
    public void SelectEquipItem(UIEquipItem item)
    {
        if(selectedItem != null)
        {
            selectedItem.Selected = false;
        }
        selectedItem = item;
    }

    void Start()
    {
        this.RefreshUI();
        EquipManager.Instance.OnEquipChanged += this.RefreshUI;
    }

    private void OnDestroy()
    {
        EquipManager.Instance.OnEquipChanged -= this.RefreshUI;
    }

    void RefreshUI()
    {
        ClearAllEquipList();
        InitAllEquipItems();
        ClearEquipedList();
        InitEquipedItems();
        this.money.text = Player.Instance.Gold.ToString();
    }

    void ClearAllEquipList()
    {
        foreach(var item in itemListRoot.GetComponentsInChildren<UIEquipItem>())
        {
            Destroy(item.gameObject);
        }
    }

    void InitAllEquipItems()
    {
        foreach(var kv in ItemManager.Instance.Items)
        {
            if(kv.Value.Count > 0 && kv.Value.Define.Type == ItemType.EQUIP)
            {
                if (EquipManager.Instance.Contains(kv.Key))
                    continue;
                GameObject go = Instantiate(itemPrefab, itemListRoot);
                UIEquipItem ui = go.GetComponent<UIEquipItem>();
                ui.SetEquipItem(kv.Key, kv.Value, this, false);
            }
        }
    }

    void ClearEquipedList()
    {
        for(int i = 0;i< slots.Count; i++)
        {
            if(slots[i].childCount > 0)
            {
                Destroy(slots[i].GetChild(0).gameObject);
            }
        }
    }

    void InitEquipedItems()
    {
        for(int i = 0;i < EquipManager.Instance.Equips.Length; i++)
        {
            if (EquipManager.Instance.Equips[i] != null)
            {
                GameObject go = Instantiate(itemEquipedPrefab, slots[i]);
                UIEquipItem ui = go.GetComponent<UIEquipItem>();
                ui.SetEquipItem(i, EquipManager.Instance.Equips[i], this, true);
            }
        }
    }

    public void DoEquip(Item item)
    {
        EquipManager.Instance.EquipItem(item);
    }

    public void UnEquip(Item item)
    {
        EquipManager.Instance.UnEquipItem(item);
    }

    public override void OnCloseClick()
    {
        base.OnCloseClick();
        InputManager.Instance.duringUI--;
        InputManager.Instance.InGamingBattleMode();
    }

}
