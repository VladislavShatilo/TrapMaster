using System;
using UnityEngine;
using Zenject;

namespace TrapMaster
{
    public class GameManager : MonoBehaviour
    {
        public int superSize = 2; // во сколько раз больше разрешение

        private SaveService saveService;
        private Wallet wallet;
        private KillsCounter killsCounter;
        [Inject]
        public void Construct(SaveService saveService, Wallet wallet, KillsCounter killsCounter)
        {
            this.saveService = saveService ?? throw new ArgumentNullException(nameof(saveService));
            this.wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
            this.killsCounter = killsCounter ?? throw new ArgumentNullException(nameof(killsCounter));
        }




        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.R))
            {
                saveService.ResetSave(1);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                wallet.AddCoins(10_000);
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                killsCounter.AddKills(1_000);
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                Time.timeScale = 5f;
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                Time.timeScale = 1f;
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                TakeScreenshot();
            }
#endif
        }
    
       void TakeScreenshot()
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string filename = "screenshot_" + timestamp + ".png";

            ScreenCapture.CaptureScreenshot(filename, superSize);

            Debug.Log("Скриншот сохранён: " + filename);
        }
    }
}