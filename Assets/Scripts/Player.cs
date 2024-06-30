using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BuffType
{
    None,
    ForthDamage,
    DoubleHigher,
    BloodDrinking,
    BigSquare,
    AddHp,
}
public class Player : MonoBehaviour
{
    public static Player Instance;
    public Slider HpSlider;

    public GameObject leftFootFx;
    public GameObject rightFootFx;

    public GameObject backFireFX;

    public AudioSource musicFire;

    public AnimationAndMovementController moveController;
    public WeaponController weaponController;

    private int hp = 1000;
    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            HpSlider.value = hp;
        }
    }

    private int gold = 10000;
    public int Gold
    {
        get { return gold; }
        set { gold = value; }
    }

    public float buff_FourDamage = 0;
    public float buff_BigSquare = 0;
    public float buff_TwoJumpHigh = 0;
    public float buff_BloodDrink = 0;

    public int dmgFactor = 1;
    public bool sqrFactor = false;
    public float jmpFactor = 1f;
    public bool bldFactor = false;

    public UIBuffIcon uiBuffIcons;
    public List<Buff> buffs = new List<Buff>();

    public Action<Buff> OnBuffAdd;
    public Action<Buff> OnBuffRemove;

    private void Awake()
    {
        Instance = this;
        PlayerStatusReset();
    }

    void Start()
    {
        Time.timeScale = 0;
        PlayerManager.Instance.PlayerObject = this.gameObject;
        PlayerStatusReset();
    }

    void Update()
    {
        UpdateBuffs();
    }

    private void UpdateBuffs()
    {
        List<int> needRemove = new List<int>();
        foreach (var bu in buffs)
        {
            if(bu == null) continue;
            bu.restTime -= Time.deltaTime;
            if (bu.restTime <= 0)
            {
                if(OnBuffRemove != null)
                    OnBuffRemove(bu);
                needRemove.Add(buffs.IndexOf(bu));
            }
        }
        foreach (var key in needRemove)
        {
            buffs.RemoveAt(key);
        }
        if (buff_FourDamage > 0)
        {
            buff_FourDamage -= Time.deltaTime;
            if(buff_FourDamage <= 0)
            {
                backFireFX.SetActive(false);
                dmgFactor = 1;
                buff_FourDamage = 0;
            }
        }
        if(buff_BigSquare > 0)
        {
            buff_BigSquare -= Time.deltaTime;
            if(buff_BigSquare < 0)
            {
                sqrFactor = false;
                buff_BigSquare = 0;
            }
        }
        if(buff_TwoJumpHigh > 0)
        {
            buff_TwoJumpHigh -= Time.deltaTime;
            if(buff_TwoJumpHigh < 0)
            {
                leftFootFx.SetActive(false);
                rightFootFx.SetActive(false);
                jmpFactor = 1;
                buff_TwoJumpHigh = 0;
            }
        }
        if(buff_BloodDrink > 0)
        {
            buff_BloodDrink -= Time.deltaTime;
            if(buff_BloodDrink < 0)
            {
                UIManager.Instance.SetFullScreenShader(0f, false);
                bldFactor = false;
                buff_BloodDrink = 0;
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Platform")
        {
            PlatformManager.Instance.handleNewPlatform(hit.gameObject);
        }
        if(hit.gameObject.tag == "FireBall")
        {
            hit.gameObject.GetComponent<Fireball>().Boom();
        }
    }

    public void OnGameStart()
    {
        this.PlayerStatusReset();
        this.uiBuffIcons.SetOwner();

        this.GetComponent<CharacterController>().enabled = false;
        this.gameObject.transform.position = new Vector3(0, 1, -7);
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        this.GetComponent<CharacterController>().enabled = true;
    }

    public void PlayerStatusReset()
    {
        this.HP = 1000;
        this.dmgFactor = 1;
        this.sqrFactor = false;
        this.jmpFactor = 1;
        this.bldFactor = false;
        this.leftFootFx.SetActive(false);
        this.rightFootFx.SetActive(false);
        this.backFireFX.SetActive(false);
    }

        public void GetDamage(int damage)
    {
        if(HP <= damage)
        {
            print("Game Over! Player Die!");
            GameManager.Instance.Over();
        }
        else
        {
            HP -= damage;
        }
    }

    internal void GetBuff(BuffType buff)
    {
        MusicManager.Instance.PlaySound("Music/getBuff");
        int exist = 0;
        foreach(var bu in buffs)
        {
            if(bu != null && bu.buffType == buff)
            {
                bu.restTime = bu.totalTime;
                exist = 1;
                break;
            }
        }
        if(exist == 0)
        {
            UIManager.Instance.ShowGetBuff(buff);
            if(buff == BuffType.BloodDrinking)
            {
                UIManager.Instance.SetFullScreenShader(0.7f, false);
            }
            Buff newBuff = new Buff(buff);
            if (OnBuffAdd != null)
                OnBuffAdd(newBuff);
            buffs.Add(newBuff);
        }

        switch (buff)
        {
            case BuffType.DoubleHigher:
                this.leftFootFx.SetActive(true);
                this.rightFootFx.SetActive(true);
                this.buff_TwoJumpHigh = 10;
                this.jmpFactor = Mathf.Sqrt(2);
                break;
            case BuffType.ForthDamage:
                this.backFireFX.SetActive(true);
                this.buff_FourDamage = 6;
                this.dmgFactor = 4;
                break;
            case BuffType.BloodDrinking:
                this.buff_BloodDrink = 10;
                this.bldFactor = true;
                break;
            case BuffType.BigSquare:
                this.buff_BigSquare = 5;
                this.sqrFactor = true;
                break;
            case BuffType.AddHp:
                this.HP += 150;
                break;
            default:
                break;
        }
        return;
    }

    public void StopMove(bool on)
    {
        this.moveController.enabled = !on;
        this.weaponController.enabled = !on;
    }

}
