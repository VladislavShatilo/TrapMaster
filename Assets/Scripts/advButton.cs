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
        money = (int)(Storage.Instance.addEnemyPrice * 2f);
        enemiesAdvButton.gameObject.SetActive(false);
        speedAdvButton.gameObject.SetActive(false);
        moneyAdvButton.gameObject.SetActive(false);
       
       
        InvokeRepeating("appear", 30f, 30f);
        enemiesAdvButton.onClick.AddListener(delegate { openReward(1); });
        speedAdvButton.onClick.AddListener(delegate { openReward(2); });
        moneyAdvButton.onClick.AddListener(delegate { openReward(3); });
        YG2.onRewardAdv += addMoneyReward;
    }

  
    void addMoneyReward(string id)
    {
        if (id == "1")
        {
            if(Storage.Instance.scene == 1)
            {
                FindObjectOfType<GameManager>().addEnemyPub();
            }
            else if(Storage.Instance.scene == 2)
            {
                FindObjectOfType<Manager>().addEnemyPub();
            }

        }
        else if (id == "2")
        {
            if (Storage.Instance.scene == 1)
            {
                FindObjectOfType<GameManager>().addSpeedPub();
            }
            else if (Storage.Instance.scene == 2)
            {
                FindObjectOfType<Manager>().addSpeedPub();
            }
        }
        else if(id == "3")
        {
            Storage.Instance.money += money;
        }


    }
    void OnDisable()
    {

        YG2.onRewardAdv += addMoneyReward;

    }
    void openReward(int id)
    {
        YG2.RewardedAdvShow(id.ToString());
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

        money = (int)(Storage.Instance.addEnemyPrice * 2f);
        moneyAdvButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = 
            FindObjectOfType<Numbers>().FormatNumber(money); 
        moneyAdvButton.gameObject.SetActive(true);

    }
}
