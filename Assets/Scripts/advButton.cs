using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class advButton : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Button enemiesAdvButton;
    [SerializeField] private Button speedAdvButton;
    [SerializeField] private Button moneyAdvButton;
    private int money;
    void OnEnable()
    {
        money = (int)(YandexGame.savesData.addEnemyPrice * 2f);
        enemiesAdvButton.gameObject.SetActive(false);
        speedAdvButton.gameObject.SetActive(false);
        moneyAdvButton.gameObject.SetActive(false);
       
       
        InvokeRepeating("appear", 30f, 30f);
        enemiesAdvButton.onClick.AddListener(delegate { openReward(1); });
        speedAdvButton.onClick.AddListener(delegate { openReward(2); });
        moneyAdvButton.onClick.AddListener(delegate { openReward(3); });
        YandexGame.RewardVideoEvent += addMoneyReward;
    }

  
    void addMoneyReward(int id)
    {
        if (id == 1)
        {
            if(YandexGame.savesData.scene == 1)
            {
                FindObjectOfType<GameManager>().addEnemyPub();
            }
            else if(YandexGame.savesData.scene == 2)
            {
                FindObjectOfType<Manager>().addEnemyPub();
            }

        }
        else if (id == 2)
        {
            if (YandexGame.savesData.scene == 1)
            {
                FindObjectOfType<GameManager>().addSpeedPub();
            }
            else if (YandexGame.savesData.scene == 2)
            {
                FindObjectOfType<Manager>().addSpeedPub();
            }
        }
        else if(id == 3)
        {
            YandexGame.savesData.money += money;
        }


    }
    void OnDisable()
    {

        YandexGame.RewardVideoEvent -= addMoneyReward;

    }
    void openReward(int id)
    {
        YandexGame.RewVideoShow(id);
        if (id == 1)
        {
            enemiesAdvButton.gameObject.SetActive(false);
        }
        else if (id == 2)
        {
            speedAdvButton.gameObject.SetActive(false);
        }
        else if (id == 3)
        {
            moneyAdvButton.gameObject.SetActive(false);
        }

    }
    void appear()
    {
        enemiesAdvButton.gameObject.SetActive(true);
        speedAdvButton.gameObject.SetActive(true);

        money = (int)(YandexGame.savesData.addEnemyPrice * 2f);
        moneyAdvButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = 
            FindObjectOfType<Numbers>().FormatNumber(money); 
        moneyAdvButton.gameObject.SetActive(true);

    }
    // Update is called once per frame
    void Update()
    {

    }
}
