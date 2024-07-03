using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBag : UIWindow
{
    public Text money;
    public GameObject bagItem;
    public Transform[] itemRoot;

    List<Image> slots;

    // Start is called before the first frame update
    void Start()
    {
        if (slots == null)
        {
            slots = new List<Image>();
            for (int i = 0; i < itemRoot.Length; i++)
            {
                slots.AddRange(itemRoot[i].GetComponentsInChildren<Image>());
            }
        }
        StartCoroutine(InitBag());
    }

    IEnumerator InitBag()
    {
        for(int i = 0; i < BagManager.Instance.bagItems.Length; i++)
        {
            var item = BagManager.Instance.bagItems[i];
            if(item.ItemId != 0)
            {
                GameObject go = Instantiate(bagItem, slots[i].transform);
                UIIconItem icon = go.GetComponent<UIIconItem>();
                icon.SetMainIcon(DataManager.Instance.Items[item.ItemId].Icon, item.Count.ToString());
            }
        }
        for(int i = BagManager.Instance.bagItems.Length; i < slots.Count; i++)
        {
            slots[i].color = Color.grey;
        }
        this.money.text = Player.Instance.Gold.ToString();
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnReset()
    {
        BagManager.Instance.Reset();
        this.AllClear();
        StartCoroutine(InitBag());
    }

    public void AllClear()
    {
        for(int i = 0; i < slots.Count; i++)
        {
            if(slots[i].transform.childCount > 0)
            {
                Destroy(slots[i].transform.GetChild(0).gameObject);
            }
        }
    }

    public override void OnCloseClick()
    {
        base.OnCloseClick();
        InputManager.Instance.duringUI--;
        InputManager.Instance.InGamingBattleMode();
    }

}
