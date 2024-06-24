using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.Instance != null)
        {
            if(Player.Instance.gameObject.transform.position.y < this.transform.position.y - 2)
            {
                GameManager.Instance.Over();
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Player" && GameManager.Instance.isOver == false)
    //    {
    //        GameManager.Instance.Over();
    //    }
    //}
}
