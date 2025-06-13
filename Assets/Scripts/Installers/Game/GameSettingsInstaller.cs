using Db.GameObjectsBase;
using Db.GameObjectsBase.Impl;
using UnityEngine;
using Zenject;
using ZenjectUtil.Test.Extensions;

namespace Installers.Game
{
    [CreateAssetMenu(menuName = "Installers/" + nameof(GameSettingsInstaller), fileName = nameof(GameSettingsInstaller))]
    public class GameSettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField]
        private PrefabsBase _prefabsBase;
        
        [SerializeField]
        private ResourceBase _resourceBase;
        
        [SerializeField]
        private UnitBase _unitBase;

        public override void InstallBindings()
        {
            // Container.Bind<IEnemySettingsBase>().FromSubstitute(enemySettingsBase).AsSingle();
            // Container.Bind<IPlayerGameParameters>().FromSubstitute(playerGameParametersBase).AsSingle();
            Container.Bind<IPrefabsBase>().FromSubstitute(_prefabsBase).AsSingle();
            Container.Bind<IResourceBase>().FromSubstitute(_resourceBase).AsSingle();
            Container.Bind<IUnitBase>().FromSubstitute(_unitBase).AsSingle();
            // Container.Bind<IFxObjectBase>().FromSubstitute(fxObjectBase).AsSingle();
            // Container.Bind<IGameFieldParameters>().FromSubstitute(_gameFieldParameters).AsSingle();
            // Container.Bind<IInputParameters>().FromSubstitute(_inputParameters).AsSingle();
        }
    }
}