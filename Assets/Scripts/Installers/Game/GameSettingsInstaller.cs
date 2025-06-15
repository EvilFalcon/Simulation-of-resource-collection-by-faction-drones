using Db.GameObjectsBase;
using Db.GameObjectsBase.Impl;
using Db.GameSceneSettings;
using Db.GameSceneSettings.Impl;
using Db.Generation.ResourcesParameters;
using Db.Generation.ResourcesParameters.Impl;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using ZenjectUtil.Test.Extensions;

namespace Installers.Game
{
    [CreateAssetMenu(menuName = "Installers/" + nameof(GameSettingsInstaller), fileName = nameof(GameSettingsInstaller))]
    public class GameSettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private FractionParameters _fractionParameters;
        [SerializeField] private PrefabsBase _prefabsBase;
        [SerializeField] private ResourcePrefabsCollection _resourcePrefabsCollection;
        [SerializeField] private ResourcesParametersBase _resourcesParametersBase;
        [FormerlySerializedAs("_unitBase")] [SerializeField] private UnitPrefabsCollection _unitPrefabsCollection;

        public override void InstallBindings()
        {
            Container.Bind<IPrefabsBase>().FromSubstitute(_prefabsBase).AsSingle();
            Container.Bind<IResourcesParameters>().FromSubstitute(_resourcesParametersBase).AsSingle();
            Container.Bind<IFractionParameters>().FromSubstitute(_fractionParameters).AsSingle();
            Container.Bind<IResourcePrefabsCollection>().FromSubstitute(_resourcePrefabsCollection).AsSingle();
            Container.Bind<IUnitPrefabsCollection>().FromSubstitute(_unitPrefabsCollection).AsSingle();
        }
    }
}