using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Numbers : MonoBehaviour
{
    
    
    [SerializeField] private TextMeshProUGUI kills;
   
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject newStageObject;
    public  string FormatNumber(float number)
    {
        if (number >= 1_000_000)
        {
            return (number / 1_000_000).ToString("0.##") + "m";
        }
        else if (number >= 1_000)
        {
            return (number / 1_000).ToString("0.##") + "k";
        }
        else
        {
            return number.ToString();
        }
    }
    void Start()
    {
        if (YandexGame.savesData.kills >= YandexGame.savesData.killsGoal)
        {
            newStageObject.SetActive(true);
        }

    }
    void Update()
    {
        kills.text = FormatNumber(YandexGame.savesData.kills) + " / "+ FormatNumber(YandexGame.savesData.killsGoal);
        slider.value = YandexGame.savesData.kills / YandexGame.savesData.killsGoal;
        if(YandexGame.savesData.kills == YandexGame.savesData.killsGoal)
        {
            newStageObject.SetActive(true);
        }
    }

    // Update is called once per frame

}
