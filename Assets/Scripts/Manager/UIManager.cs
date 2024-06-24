using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Text ScoreNumberText;

    public GameObject resultPanel;
    public GameObject titlePanel;
    public GameObject settingPanel;
    public GameObject gamingPanel;
    public Text resultText;
    public Text resultScore;

    public GameObject buffGetInfo;
    public Transform buffGetInfoPlace;

    public Color buffInfoDoubleHigh;
    public Color buffInfoBloodDrink;
    public Color buffInfoForthDamage;
    public Color buffInfoBigSquare;

    public Material BuffBloodDrinkEffect;
    public FullScreenPassRendererFeature Feature;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //titlePlane.SetActive(true);
    }

    public void UpdateScoreNum(int num)
    {
        ScoreNumberText.text = num.ToString();
    }

    public void GameResult(int score)
    {
        ScoreNumberText.gameObject.SetActive(false);
        gamingPanel.gameObject.SetActive(false);
        resultPanel.SetActive(true);
        resultScore.text = score.ToString();
        if (score == 8)
        {
            resultText.text = "��Ҫ�������˲�������~";
        }
        else if(score < 5)
        {
            resultText.text = "����ˣ�\n������һ��������\n����\n����23�Ŵ�����";
        }
        else if(score == 5)
        {
            resultText.text = "������һ̯��\n��ղһ�ֱ����������";
        }
        else if (score < 9)
        {
            resultText.text = "����\n��ղһ�������Ĳ���";
        }
        else if (score < 13)
        {
            resultText.text = "����\n��ղ���ڵ�Ŵ���";
        }
        else if (score < 20)
        {
            resultText.text = "��Ҳ\n��ղ�볡���˴���";
        }
        else if (score < 30)
        {
            resultText.text = "��Ұ\n��ղһ������������";
        }
        else if (score < 50)
        {
            resultText.text = "����\n�����·���";
        }
        else
        {
            resultText.text = "����ΰ��������ԣ�";
        }
    }

    public void OnGameStart()
    {
        SetFullScreenShader(0f, true);
        titlePanel.SetActive(false);
        resultPanel.SetActive(false);
        settingPanel.SetActive(false);
        gamingPanel.SetActive(true);
        ScoreNumberText.gameObject.SetActive(true);
    }

    public void OnGamePause()
    {
        gamingPanel.SetActive(false);
    }

    public void OnSettingClick()
    {
        settingPanel.SetActive(true);
        gamingPanel.SetActive(false);
    }

    internal void OnGameResume()
    {
        gamingPanel.SetActive(true);
    }

    internal void ShowGetBuff(BuffType buff)
    {
        GameObject go = Instantiate(buffGetInfo);
        Color color = Color.white;
        switch (buff)
        {
            case BuffType.DoubleHigher:
                color = buffInfoDoubleHigh;
                go.GetComponent<UIBuffGetInfoPlace>().SetTextandColor("��������", color);
                go.transform.SetParent(buffGetInfoPlace);
                Destroy(go, 1.3f);
                break;
            case BuffType.ForthDamage:
                color= buffInfoForthDamage;
                go.GetComponent<UIBuffGetInfoPlace>().SetTextandColor("�ı��˺�", color);
                go.transform.SetParent(buffGetInfoPlace);
                Destroy(go, 1.3f);
                break;
            case BuffType.BloodDrinking:
                color = buffInfoBloodDrink;
                go.GetComponent<UIBuffGetInfoPlace>().SetTextandColor("��ɱ�ظ�", color);
                go.transform.SetParent(buffGetInfoPlace);
                Destroy(go, 1.3f);
                break;
            case BuffType.BigSquare:
                color = buffInfoBigSquare;
                go.GetComponent<UIBuffGetInfoPlace>().SetTextandColor("����ƽ̨", color);
                go.transform.SetParent(buffGetInfoPlace);
                Destroy(go, 1.3f);
                break;
            default:
                break;
        }

        return;
    }
    public void SetFullScreenShader(float vignetteItensity, bool immediate)
    {
        if(immediate)
        {
            Feature.passMaterial.SetFloat("_VignetteIntensity", vignetteItensity);
        }
        else
        {
            StartCoroutine(SetBuffBlookDrinkEffectShaderVolume(vignetteItensity));
        }
    }

    IEnumerator SetBuffBlookDrinkEffectShaderVolume(float vol)
    {
        float old = Feature.passMaterial.GetFloat("_VignetteIntensity");
        Debug.LogFormat("old volume {0}", old);
        if (old < vol)
        {
            while (old < vol)
            {
                old += 0.05f;
                Feature.passMaterial.SetFloat("_VignetteIntensity", old);
                yield return null;
            }
            old = Feature.passMaterial.GetFloat("_VignetteIntensity");
            Debug.LogFormat("new volume {0}", old);
            yield break;
        }
        else
        {
            while (old > vol)
            {
                if(old > 0.01f)
                {
                    old -= 0.01f;
                }
                else
                {
                    old = 0;
                }
                Feature.passMaterial.SetFloat("_VignetteIntensity", old);
                yield return null;
            }
            old = Feature.passMaterial.GetFloat("_VignetteIntensity");
            Debug.LogFormat("new volume {0}", old);
            yield break;
        }
    }

}
