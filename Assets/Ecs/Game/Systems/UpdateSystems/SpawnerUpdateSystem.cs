using System.Collections.Generic;
using Ecs.Utils;
using Game.Services.Spawners;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems.UpdateSystems
{
    [Install(ExecutionType.Game, ExecutionPriority.Normal, 100, nameof(EFeatures.Generation))]
    public class SpawnerUpdateSystem : IUpdateSystem
    {
        private readonly List<ITickedSpawner> _tickedSpawners;

        public SpawnerUpdateSystem(List<ITickedSpawner> tickedSpawners)
        {
            _tickedSpawners = tickedSpawners;
        }

        public void Update()
        {
            foreach (var tickedSpawner in _tickedSpawners)
            {
                tickedSpawner.OnTick();
            }
        }
    }
}