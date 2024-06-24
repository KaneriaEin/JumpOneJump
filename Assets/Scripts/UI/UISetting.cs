using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : MonoBehaviour
{
    public CinemachineFreeLook playerCamera;

    public Slider verSpeed;
    public Slider horSpeed;

    public Text verSpeedValue;
    public Text horSpeedValue;

    public void OnReturnClick()
    {
        this.gameObject.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        this.horSpeed.value = Config.HorSpeed;
        this.verSpeed.value = Config.VerSpeed;
        this.horSpeedValue.text = ((int)Config.HorSpeed).ToString();
        this.verSpeedValue.text = ((int)Config.VerSpeed).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HorCamSpeed(float vol)
    {
        Config.HorSpeed = (int)vol;
        this.horSpeedValue.text = ((int)Config.HorSpeed).ToString();
    }

    public void VerCamSpeed(float vol)
    {
        Config.VerSpeed = (int)vol;
        this.verSpeedValue.text = ((int)Config.VerSpeed).ToString();
    }

}
