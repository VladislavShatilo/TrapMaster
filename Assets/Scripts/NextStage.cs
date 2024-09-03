using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class NextStage : MonoBehaviour
{
    [SerializeField] private Button nextStageButton;
    [SerializeField] private GameObject[] blades;
    [SerializeField] private GameObject manager;
    [SerializeField] private TextMeshProUGUI stageValueText;
    [SerializeField] private Image onTrapButtonImage;
    [SerializeField] private Sprite onTrapButtonSprite;

    void Start()
    {
        nextStageButton.onClick.AddListener(delegate { nextStage(); });
    }

    void nextStage()
    {
        manager.GetComponent<GameManager>().enabled = false;
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();
        YandexGame.savesData.killsGoal = 50000;
        YandexGame.savesData.scene = 2;
        YandexGame.SaveProgress();
        manager.GetComponent<Manager>().enabled = true;
        
        for(int i = 0;  i < blades.Length; i++)
        {
            blades[i].SetActive(false);
        }
       
        stageValueText.text = "2";
        onTrapButtonImage.sprite = onTrapButtonSprite;
        gameObject.SetActive(false);


    }
   

   
    void Update()
    {
        
    }
}
