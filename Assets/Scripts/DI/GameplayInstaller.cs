using UnityEngine;
using Zenject;

namespace TrapMaster
{
    public class GameplayInstaller : MonoInstaller
    {
        [Header("Settings")]
        [SerializeField] private SplashSettings splashSettings;
        [SerializeField] private SpawnSetting spawnSetting;
        [SerializeField] private SaveSettings saveSettings;
        [SerializeField] private StageCatalog stageCatalog;

        [Header("Views")]
        [SerializeField] private KillsView killsView;
        [SerializeField] private SettingsButtonView settingsButtonView;
        [SerializeField] private SettingsPopupView settingsPopupView;
        [SerializeField] private UpgradeButtonsView upgradeButtonsView;
        [SerializeField] private StageView stageView;
        [SerializeField] private SpeedUpHintView speedUpHintView;


        [Header("Other")]
     
        [SerializeField] private SpawnPoint spawnPoint;
        [SerializeField] private SplashParticleService splashParticleService;
        [SerializeField] private SaveOnApplicationQuit saveOnApplicationQuit;
        [SerializeField] private AudioPlayer audioPlayer;
        [SerializeField] private Camera gameplayCamera;
        [SerializeField] private GameManager gameManager;


        public override void InstallBindings()
        {
            //Settings
            Container.BindInstance(splashSettings).AsSingle();
            Container.BindInstance(saveSettings).AsSingle();
            Container.BindInstance(spawnSetting).AsSingle();
            Container.BindInstance(stageCatalog).AsSingle();
            //Views
            Container.Bind<IUIKillsView>().FromInstance(killsView).AsSingle();
            Container.Bind<SettingsPopupView>().FromInstance(settingsPopupView).AsSingle();
            Container.Bind<SettingsButtonView>().FromInstance(settingsButtonView).AsSingle();
            Container.Bind<UpgradeButtonsView>().FromInstance(upgradeButtonsView).AsSingle();
            Container.Bind<StageView>().FromInstance(stageView).AsSingle();
            Container.Bind<SpeedUpHintView>().FromInstance(speedUpHintView).AsSingle();

            //Services
            Container.Bind<MonsterSpawnService>().AsSingle();
            Container.Bind<KillRewardService>().AsSingle();
            Container.BindInstance(splashParticleService).AsSingle();
            Container.BindInterfacesAndSelfTo<AutoSaveService>().AsSingle().NonLazy();
            Container.Bind<SaveService>().AsSingle();
            Container.Bind<TrapService>().AsSingle();
            Container.Bind<MonstersService>().AsSingle();
            Container.Bind<SpeedService>().AsSingle();
            Container.Bind<StageService>().AsSingle();
            
            //Presenter
            Container.BindInterfacesAndSelfTo<KillProgressPresenter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SettingsWindowPresenter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<StagePresenter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UpgradePresenter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UpgradeAvailabilityPresenter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SpeedUpHintPresenter>().AsSingle().NonLazy(); 

            //Domain
            Container.Bind<Sound>().AsSingle();
            Container.Bind<Wallet>().AsSingle();
            Container.Bind<KillsCounter>().AsSingle();
            Container.Bind<EventBus>().AsSingle();
            Container.Bind<Trap>().AsSingle();
            Container.Bind<Monsters>().AsSingle();
            Container.Bind<Speed>().AsSingle();
            Container.Bind<Stage>().AsSingle();

            //Factory
            Container.Bind<IMonsterFactory>().To<MonsterFactory>().AsSingle();
            Container.Bind<ITrapFactory>().To<TrapFactory>().AsSingle();

            //Other
            Container.BindInstance(spawnPoint).AsSingle();
            Container.BindInterfacesTo<MonsterSpawnTickable>().AsSingle();
            Container.Bind<GameManager>().FromInstance(gameManager).AsSingle();
            Container.Bind<AudioPlayer>().FromInstance(audioPlayer).AsSingle();
            Container.Bind<Camera>().FromInstance(gameplayCamera).AsSingle();
            Container.Bind<SaveOnApplicationQuit>().FromInstance(saveOnApplicationQuit).AsSingle();
            Container.BindInterfacesAndSelfTo<AudioController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CameraFovService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MonsterKillHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameBootstrapper>().AsSingle().NonLazy();
            Container.Bind<ISaveStorage>().To<YGSaveStorage>().AsSingle();
            Container.BindInterfacesAndSelfTo<SessionTimer>().AsSingle();



        }
    }
}
