
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 0;
        public float addBladePrice = 300;
        public float addEnemyPrice = 3;
        public float addSpeedPrice = 100;
        public float spawnInterval = 1;
        public int countOfBlades = 1;
        public int countOfMaces = 1;
        public float speedBlade = 2f;
        public float speedMace = 100f;
        public int addBladeLevel = 1;
        public int addEnemyLevel = 1;
        public int addSpeedLevel = 1;
        public float kills = 0;
        public float killsGoal = 10000;
        public int scene = 1;
        // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];

        // Ваши сохранения

        // ...

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива

            openLevels[1] = true;
        }
    }
}
