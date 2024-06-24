using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SimpleUIAnimationControl : MonoBehaviour
{
    [Header("Vertical Shrink")]
    public float VerShrink_v = 0;
    public float VerShrink_a = 0;
    public float VerShrink = 0;

    [Header("Horizontal Shrink")]
    public float HorShrink_v = 0;
    public float HorShrink_a = 0;
    public float HorShrink = 0;

    [Header("Vertical Levitation")]
    public float VerLev_v = 0;
    public float VerLev_a = 0;
    public float VerLev = 0;

    [Header("Turning LF")]
    public float TurnLF_v = 0;
    public float TurnLF_a = 0;
    public float TurnLF = 0;

    public float randomPhase = 0;

    [SerializeField]
    protected bool right = true;

    private Vector3 baseLocalPosition;  //动画的基准位置
    private Vector3 originLocalPosition;  //初始位置

    private float baseScaleX;
    private float baseScaleY;

    private bool initialized = false;

    protected virtual void Awake()
    {
        if(transform.localScale.x == -1)
            right = false;

        baseScaleX = Mathf.Abs(transform.localScale.x);
        baseScaleY = transform.localScale.y;

        originLocalPosition = this.transform.localPosition;

    }

    void Start()
    {
        if (!initialized)
            Init();
    }

    private void Init()
    {
        baseLocalPosition = originLocalPosition;

        if (randomPhase == 0)
            randomPhase = Random.Range(0f, 360f);

        transform.localPosition = new Vector3(right ? 1 : -1, 1, 1);

        initialized = true;
    }

    public void Restore()
    {
        if(!initialized)
            Init();

    }

    private float GetValue(float speed, float phase)
    {
        float angle = speed * Time.fixedTime;
        float v = -Mathf.Cos((angle+phase+randomPhase) * Mathf.Deg2Rad);

        return v;
    }

    private void FixedUpdate()
    {
        float v;

        //收缩
        float vvh = 1;
        float vvs = 1;
        if(VerShrink != 0 || HorShrink != 0)
        {
            v = (GetValue(VerShrink_v, VerShrink_a) + 1) / 2;
            vvs = v * VerShrink + baseScaleY;

            v = (GetValue(HorShrink_v, HorShrink_a) + 1) / 2;
            vvh = v * HorShrink + baseScaleX;
        }

        vvh = right ? vvh : -vvh;
        transform.localScale = new Vector3(vvh, vvs, 1);

        //悬浮
        if(VerLev != 0)
        {
            v = GetValue(VerLev_v, VerLev_a);
            float vvf = v * VerLev;
            float x = baseLocalPosition.x;
            float y = baseLocalPosition.y;
            float z = baseLocalPosition.z;
            transform.localPosition = new Vector3(x, y + vvf, z);
        }

        //转动
        if(TurnLF != 0)
        {
            v = GetValue(TurnLF_v, TurnLF_a);
            float rotz = v * TurnLF;

            transform.localRotation = Quaternion.Euler(0, 0, rotz);
        }
    }
}
