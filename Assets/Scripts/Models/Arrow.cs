using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Arrow : MonoBehaviour
{
    public GameObject hitParticle;

    public ParticleSystem trailParticle;

    public int Speed = 50;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            return;
        }

        //Debug.LogFormat("### Hit Name:{0}", collision.gameObject.name);

        GameObject go = Instantiate(hitParticle, transform.position, Quaternion.identity);
        Debug.LogFormat("### explode position:{0}", transform.position);

        if (collision.gameObject.tag == "Enemy")
        {
            EntityController entityController = collision.gameObject.GetComponent<EntityController>();
            if(entityController != null)
            {
                entityController.GetHitByBullet(BulletType.Arrow);
            }
        }

        //trailParticle.transform.parent = transform.parent;
        trailParticle.Stop();

        //Camera.main.transform.DOComplete();
        //Camera.main.transform.DOShakePosition(.4f, .5f, 20, 90, false, true);

        Destroy(go,1);
        Destroy(gameObject);
        //UnityEngine.Profiling.Profiler.EndSample();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);

        //用射线检测，子弹的碰撞，当物体在两点之间产生的射线内时，算作发生碰撞
        //1.获得射线的起点（原点）
        //2.获得射线的方向
        //3.获得射线的距离
        Vector3 oriPos = transform.position;//1.射线的起点
        Vector3 direction = transform.position - oriPos;//2.方向
        float ength = (transform.position - oriPos).magnitude;//3.射线的距离，取向量的大小
        //光线投射碰撞
        RaycastHit hitinfo;//存储碰撞信息
        //光线投射，检测是否发生碰撞
        bool isCillider = Physics.Raycast(oriPos, direction, out hitinfo, ength);
        if (isCillider)
        {

        }
    }
}
