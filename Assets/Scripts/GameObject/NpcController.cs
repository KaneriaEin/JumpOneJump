using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class NpcController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    public int npcId;

    Animator anim;
    SkinnedMeshRenderer render;
    Color originColor;

    NpcDefine npc;

    //用于玩家互动冷却时间
    bool inInteractive = false;



    void Start()
    {
        render = this.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        anim = this.gameObject.GetComponent<Animator>();
        originColor = render.sharedMaterial.color;
        GameManager.Instance.OnGameStartInit += Init;
    }

    private void Init()
    {
        npc = NpcManager.Instance.GetNpcDefine(npcId);
        NpcManager.Instance.UpdateNpcPosition(this.npcId, this.transform.position);
        this.StartCoroutine(Actions());
        RefreshNpcStatus();
    }

    private void RefreshNpcStatus()
    {
        //throw new System.NotImplementedException();
    }

    private IEnumerator Actions()
    {
        while (true)
        {
            if (inInteractive)
                yield return new WaitForSeconds(2f);
            else
                yield return new WaitForSeconds(Random.Range(5f, 10f));

            this.Relax();
        }
    }

    private void Relax()
    {
        this.anim.SetTrigger("Relax");
    }

    //private void OnMouseDown()
    //{
    //    if(Vector3.Distance(this.transform.position, Player.Instance.transform.position) < 2)
    //    {
    //        //Interactive();
    //    }
    //    else
    //    {
    //        //导航，mmorpg需要，此tps不需要
    //    }
    //}

    void Interactive()
    {
        if (!inInteractive)
        {
            inInteractive = true;
            StartCoroutine(DoInteractive());
        }
    }

    IEnumerator DoInteractive()
    {
        yield return FaceToPlayer();

        //虚拟摄像机开启，怼脸
        virtualCamera.gameObject.SetActive(true);
        InputManager.Instance.current_virtual_camera = virtualCamera;

        if (NpcManager.Instance.Interactive(this.npcId))
        {
            anim.SetTrigger("Talk");
        }
        yield return new WaitForSeconds(3f);
        inInteractive = false;
    }

    public void DoInteractiveOver()
    {
        virtualCamera.enabled = false;
    }

    IEnumerator FaceToPlayer()
    {
        Vector3 faceTo = (Player.Instance.transform.position - this.transform.position).normalized;
        faceTo.y = 0;
        while(Mathf.Abs(Vector3.Angle(this.transform.forward, faceTo)) > 5)
        {
            this.gameObject.transform.forward = Vector3.Lerp(this.gameObject.transform.forward, faceTo, Time.deltaTime * 5f);
            yield return null;
        }
    }

    private void Update()
    {
        if(Vector3.Distance(this.transform.position, Player.Instance.transform.position) < 3)
        {
            Highlight(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interactive();
            }
        }
        else
        {
            Highlight(false);
        }
    }

    //private void OnMouseEnter()
    //{
    //    Highlight(true);
    //}

    //private void OnMouseExit()
    //{
    //    Highlight(false);
    //}

    void Highlight(bool highlight)
    {
        if (highlight)
        {
            if(render.sharedMaterial.color != Color.white)
            {
                render.sharedMaterial.color = Color.white;
            }
        }
        else
        {
            if (render.sharedMaterial.color != originColor)
                render.sharedMaterial.color = originColor;
        }
    }

}
