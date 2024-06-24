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
            resultText.text = "你要的天龙八步，给你~";
        }
        else if(score < 5)
        {
            resultText.text = "你挂了！\n你玩跳一跳的样子\n像极了\n湖人23号打篮球！";
        }
        else if(score == 5)
        {
            resultText.text = "【两手一摊】\n老詹一局比赛犯规次数";
        }
        else if (score < 9)
        {
            resultText.text = "还行\n老詹一次上篮的步数";
        }
        else if (score < 13)
        {
            resultText.text = "不错\n老詹单节垫脚次数";
        }
        else if (score < 20)
        {
            resultText.text = "可也\n老詹半场肘人次数";
        }
        else if (score < 30)
        {
            resultText.text = "狂野\n老詹一赛季废人人数";
        }
        else if (score < 50)
        {
            resultText.text = "阳神\n天神下凡！";
        }
        else
        {
            resultText.text = "我阳伟大！无需多言！";
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
                go.GetComponent<UIBuffGetInfoPlace>().SetTextandColor("二倍高跳", color);
                go.transform.SetParent(buffGetInfoPlace);
                Destroy(go, 1.3f);
                break;
            case BuffType.ForthDamage:
                color= buffInfoForthDamage;
                go.GetComponent<UIBuffGetInfoPlace>().SetTextandColor("四倍伤害", color);
                go.transform.SetParent(buffGetInfoPlace);
                Destroy(go, 1.3f);
                break;
            case BuffType.BloodDrinking:
                color = buffInfoBloodDrink;
                go.GetComponent<UIBuffGetInfoPlace>().SetTextandColor("击杀回复", color);
                go.transform.SetParent(buffGetInfoPlace);
                Destroy(go, 1.3f);
                break;
            case BuffType.BigSquare:
                color = buffInfoBigSquare;
                go.GetComponent<UIBuffGetInfoPlace>().SetTextandColor("扩大平台", color);
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
