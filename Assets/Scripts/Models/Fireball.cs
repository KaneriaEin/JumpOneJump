using UnityEngine;

public class Fireball : MonoBehaviour
{
    public GameObject hitParticle;

    public ParticleSystem trailParticle;

    public int Speed = 50;

    public void Boom()
    {
        GameObject go = Instantiate(hitParticle, transform.position, Quaternion.identity);

        {
            print("Player Get Hit!!!");
            Player.Instance.GetDamage(100);
        }

        //trailParticle.transform.parent = transform.parent;
        trailParticle.Stop();

        //Camera.main.transform.DOComplete();
        //Camera.main.transform.DOShakePosition(.4f, .5f, 20, 90, false, true);

        Destroy(go, 1);
        Destroy(gameObject);
        //UnityEngine.Profiling.Profiler.EndSample();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);

        //�����߼�⣬�ӵ�����ײ��������������֮�������������ʱ������������ײ
        //1.������ߵ���㣨ԭ�㣩
        //2.������ߵķ���
        //3.������ߵľ���
        Vector3 oriPos = transform.position;//1.���ߵ����
        Vector3 direction = transform.position - oriPos;//2.����
        float ength = (transform.position - oriPos).magnitude;//3.���ߵľ��룬ȡ�����Ĵ�С
        //����Ͷ����ײ
        RaycastHit hitinfo;//�洢��ײ��Ϣ
        //����Ͷ�䣬����Ƿ�����ײ
        bool isCillider = Physics.Raycast(oriPos, direction, out hitinfo, ength);
        if (isCillider)
        {

        }
    }
}
