using GameServer.Entities;
using MyAI;
using UnityEngine;


public enum EntityEvent
{
    None,
    Idle,
    MoveFwd,
    MoveBack,
    Jump,
    Ride,
    GetHit,
    Dizzy,
    Die
}
public enum EnemyState
{
    None,
    Idle,
    Battle,
    Dizzy,
    Die
}

public class EntityController : MonoBehaviour
{
    public int HP;
    public int Armor;

    public Animator anim;
    public Rigidbody rb;
    private AnimatorStateInfo currentBaseState;

    public UnityEngine.Vector3 position;
    public UnityEngine.Vector3 direction;
    Quaternion rotation;

    public UnityEngine.Vector3 lastPosition;
    Quaternion lastRotation;

    public float speed;
    public float animSpeed = 1.5f;
    public float jumpPower = 3.0f;

    public bool isPlayer = false;

    public EnemyState enemyState = EnemyState.Idle;

    public GameObject Target;

    public AIAgent aiAgent;

    public AudioSource musicGethit;

    // Start is called before the first frame update
    void Start()
    {
        aiAgent = new AIAgent(this.gameObject.GetComponent<Enemy>());
        Target = Player.Instance.gameObject;
    }

    public void OnEntityEvent(EntityEvent entityEvent, int param)
    {
        switch (entityEvent)
        {
            case EntityEvent.Idle:
                anim.SetBool("MoveFwd", false);
                anim.SetTrigger("Idle");
                break;
            case EntityEvent.MoveFwd:
                anim.SetBool("MoveFwd", true);
                break;
            case EntityEvent.MoveBack:
                anim.SetBool("MoveBack", true);
                break;
            case EntityEvent.Jump:
                anim.SetTrigger("Jump");
                break;
            case EntityEvent.GetHit:
                anim.SetTrigger("GetHit");
                break;
            case EntityEvent.Die:
                anim.SetTrigger("Die");
                break;
            case EntityEvent.Dizzy:
                anim.SetTrigger("Dizzy");
                break;
        }
    }

    public void GetDamage(int hpdamage, int armordamage)
    {
        musicGethit.PlayOneShot(musicGethit.clip);
        if (this.HP < hpdamage)
        {
            this.BeKilled();
            return;
        }
        else
        {
            this.OnEntityEvent(EntityEvent.GetHit, 0);
            this.HP -= hpdamage;
        }
        if (this.Armor < armordamage)
            this.ArmorDestroyed();
        else
        {
            this.Armor -= armordamage;
        }
    }

    private void ArmorDestroyed()
    {
        this.Armor = 0;
        this.OnEntityEvent(EntityEvent.Dizzy, 0);
        this.enemyState = EnemyState.Dizzy;
    }

    private void BeKilled()
    {
        this.HP = 0;
        this.enemyState = EnemyState.Die;
        this.OnEntityEvent(EntityEvent.Die, 0);
        if (Player.Instance.bldFactor)
            Player.Instance.HP += 200;
        this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        GameManager.Instance.Score += 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(aiAgent != null)
        {
            aiAgent.Update();
        }
    }

    public void GetHitByBullet(BulletType bulletType)
    {
        if(this.enemyState == EnemyState.Idle)
        {
            this.enemyState = EnemyState.Battle;
            this.transform.LookAt(Player.Instance.gameObject.transform);
            this.Target = Player.Instance.gameObject;
            aiAgent.OnDamage(this.Target);
        }
        switch (bulletType)
        {
            case BulletType.Arrow:
                this.GetDamage(300 * Player.Instance.dmgFactor, 50);
                break;
        }
    }
}
