using UnityEngine;

public enum BulletType
{
    None,
    Arrow,
    Rifle
}

public class WeaponController : MonoBehaviour
{
    public static WeaponController Instance;

    [Header("Parameters")]
    public Vector3 arrowImpulse;
    public float timeToShoot;
    public float shootWait;
    public bool canShoot;
    public bool shootRest = false;
    public bool isAiming = false;
    public Camera m_Camera;
    private float timer = 0;

    [Header("Shooting Params")]
    public Vector3 targetPoint;

    [Header("Gun")]
    public Transform bowModel;
    private Vector3 bowOriginalPos, bowOriginalRot;
    public Transform bowZoomTransform;
    public float rate = 1;

    [Header("Bullet")]
    public GameObject bulletPrefab;
    public Transform bulletSpawnOrigin;
    private Vector3 bulletOriginalPos;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (GameManager.Instance.gaming == false)
        //    return;

        Shoot();
    }

    public void Shoot()
    {
        timer += Time.deltaTime;
        if(Input.GetKey(KeyCode.Mouse0))
        {
            //Debug.LogFormat("mouse clicked: timer:{0} rate:{1}", timer, rate);
            if (timer > 1f / rate)
            {
                Ray ray = m_Camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    //如果射线撞击到了物体
                    targetPoint = hitInfo.point;
                    Debug.LogFormat("###Test002 targetPoint:{0}", targetPoint);
                }
                else
                {
                    //如果射线没有撞击物体
                    //那么朝向1000米外的目标点
                    targetPoint = m_Camera.transform.forward * 2000;
                    //Debug.LogFormat("###Test003 targetPoint:{0}", targetPoint);
                }

                Player.Instance.musicFire.PlayOneShot(Player.Instance.musicFire.clip);
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnOrigin.position, bulletSpawnOrigin.rotation);
                bullet.transform.LookAt(targetPoint);
                //Debug.LogFormat("###Test000 rotation:{0} targetPoint:{1} , ", bullet.transform.rotation, targetPoint);


                Destroy(bullet, 10);
                timer = 0f;
            }
        }
    }

}
