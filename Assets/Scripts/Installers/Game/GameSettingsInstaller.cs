using Db.GameObjectsBase;
using Db.GameObjectsBase.Impl;
using Db.Generation.ResourcesParameters;
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
        private ResourcesParametersBase _resourcesParametersBase;
        
        [SerializeField]
        private ResourcePrefabsCollection _resourcePrefabsCollection;
        
        [SerializeField]
        private UnitBase _unitBase;

        public override void InstallBindings()
        {
            // Container.Bind<IEnemySettingsBase>().FromSubstitute(enemySettingsBase).AsSingle();
            // Container.Bind<IPlayerGameParameters>().FromSubstitute(playerGameParametersBase).AsSingle();
            Container.Bind<IPrefabsBase>().FromSubstitute(_prefabsBase).AsSingle();
            Container.Bind<IResourcesParameters>().FromSubstitute(_resourcesParametersBase).AsSingle();
            Container.Bind<IResourcePrefabsCollection>().FromSubstitute(_resourcePrefabsCollection).AsSingle();
            Container.Bind<IUnitBase>().FromSubstitute(_unitBase).AsSingle();
            // Container.Bind<IFxObjectBase>().FromSubstitute(fxObjectBase).AsSingle();
            // Container.Bind<IGameFieldParameters>().FromSubstitute(_gameFieldParameters).AsSingle();
            // Container.Bind<IInputParameters>().FromSubstitute(_inputParameters).AsSingle();
        }
    }
}