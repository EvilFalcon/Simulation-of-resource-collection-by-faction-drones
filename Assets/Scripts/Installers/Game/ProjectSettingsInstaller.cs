using Playdarium.BuildPipelines.Runtime;
using Playdarium.BuildPipelines.Runtime.Impls;
using UnityEngine;
using Zenject;
using ZenjectUtil.Test.Extensions;

namespace Installers.Game
{
    [CreateAssetMenu(menuName = "Installers/" + nameof(ProjectSettingsInstaller), fileName = nameof(ProjectSettingsInstaller))]
    public class ProjectSettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private BuildSetting _buildSettings;
        
        public override void InstallBindings()
        {
            Container.Bind<IBuildSetting>().FromSubstitute(_buildSettings).AsSingle();
        }
    }
}