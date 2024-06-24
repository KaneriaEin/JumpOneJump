using Managers;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance;

    public GameObject nowPlatform;
    public GameObject nextPlatform;

    public GameObject deathPlatform;

    public GameObject plat_level1;
    public GameObject plat_level2;
    public GameObject plat_enemy1;

    public GameObject addBuff1;
    public GameObject longBuff1;
    public GameObject longBuff2;
    public GameObject longBuff3;
    public GameObject longBuff4;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void handleNewPlatform(GameObject go)
    {
        if (go == nowPlatform)
            return;

        if(nowPlatform != null)
        {
            Destroy(nowPlatform);
        }
        else
        {
            MusicManager.Instance.PlayMusic(MusicManager.Instance.stageBgm);
            UIWorldElementManager.Instance.RemoveLogoGameStart(nextPlatform.transform);
        }
        nowPlatform = go;

        GameManager.Instance.Score++;

        deathPlatform.gameObject.transform.position = nowPlatform.transform.position - new Vector3(0, 1, 0);

        if(GameManager.Instance.Score > 6)
        {
            Vector3 newPos = newPlatPosition(go.transform.position);
            if (Player.Instance.sqrFactor)
            {
                nextPlatform = Instantiate(plat_level1, newPos, go.transform.rotation);
                CreatNewBuff(newPos);
            }
            else
            {
                nextPlatform = Instantiate(plat_level2, newPos, go.transform.rotation);
                CreatNewBuff(newPos);
            }

            if (GameManager.Instance.spawnScore > 3)
            {
                GameObject enemyPlat = Instantiate(plat_enemy1, newEnemyPlatPosition(newPos), go.transform.rotation);
                EnemyManager.Instance.enemies.Add(enemyPlat);
                GameManager.Instance.spawnScore = 0;
            }
        }
        else if(GameManager.Instance.Score > 3)
        {
            nextPlatform = Instantiate(plat_level2, newPlatPosition(go.transform.position), go.transform.rotation);
        }
        else
        {
            nextPlatform = Instantiate(plat_level1, newPlatPosition(go.transform.position), go.transform.rotation);
        }
    }

    private void CreatNewBuff(Vector3 newPos)
    {
        if(Random.Range(0, 10) < 5)//刷不刷buff
        {
            int i = Random.Range(0, 100);
            if(Player.Instance.HP > 600 || Player.Instance.HP < 800)//可能刷：加血，高跳，四倍伤害，大平台，吸血
            {
                if(i < 20)
                {
                    Instantiate(addBuff1, newPos + addBuff1.transform.position, addBuff1.transform.rotation);
                }
                else if(i < 40)
                {
                    Instantiate(longBuff1, newPos + longBuff1.transform.position, longBuff1.transform.rotation);
                }
                else if(i < 60)
                {
                    Instantiate(longBuff2, newPos + longBuff2.transform.position, longBuff2.transform.rotation);
                }
                else if(i < 80)
                {
                    Instantiate(longBuff3, newPos + longBuff3.transform.position, longBuff3.transform.rotation);
                }
                else if(i < 100)
                {
                    Instantiate(longBuff4, newPos + longBuff4.transform.position, longBuff4.transform.rotation);
                }
            }
            else//不刷加血，只刷buff
            {
                if (i < 25)
                {
                    Instantiate(longBuff1, newPos, longBuff1.transform.rotation);
                }
                else if (i < 50)
                {
                    Instantiate(longBuff2, newPos, longBuff2.transform.rotation);
                }
                else if (i < 75)
                {
                    Instantiate(longBuff3, newPos, longBuff3.transform.rotation);
                }
                else if (i < 100)
                {
                    Instantiate(longBuff4, newPos, longBuff4.transform.rotation);
                }
            }
        }
        
    }

    public Vector3 newPlatPosition(Vector3 nowpos)
    {
        int newY = Random.Range(2, 4);
        int newZ = (int)(Random.Range(4, 7) * Mathf.Pow(-1, Random.Range(0, 2)));
        int newX = (int)(Random.Range(4, 7) * Mathf.Pow(-1, Random.Range(0, 2)));

        return new Vector3(newX, newY, newZ) + nowpos;
    }

    public Vector3 newEnemyPlatPosition(Vector3 nowpos)
    {
        int newY = Random.Range(0, 1);
        int newZ = (int)(Random.Range(10, 12) * Mathf.Pow(-1, Random.Range(0, 2)));
        int newX = (int)(Random.Range(10, 12) * Mathf.Pow(-1, Random.Range(0, 2)));

        return new Vector3(newX, newY, newZ) + nowpos;
    }

    public void OnGameStart()
    {
        if (nowPlatform != null)
        {
            Destroy(nowPlatform);
        }
        if (nextPlatform != null)
        {
            Destroy(nextPlatform);
        }
        nextPlatform = Instantiate(plat_level1, new Vector3(0,3,0), new Quaternion(0,0,0,0));
        UIWorldElementManager.Instance.AddLogoGameStart(nextPlatform.transform);
        deathPlatform.gameObject.transform.position = new Vector3(0, -10, 0);
    }
}
