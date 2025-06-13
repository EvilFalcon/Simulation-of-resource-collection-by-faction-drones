using System.Collections.Generic;
using Ecs.Utils;
using Ecs.Views.Linkable;
using InstallerGenerator.Attributes;
using InstallerGenerator.Enums;
using JCMG.EntitasRedux;

namespace Ecs.Game.Systems
{
    [Install(ExecutionType.Game, ExecutionPriority.High, 1, nameof(EFeatures.Initialization))]
    public class InstantiateSystem : ReactiveSystem<GameEntity>
    {
        private readonly ILinkedEntityRepository _linkedEntityRepository;
        private readonly ISpawnService<GameEntity, IObjectLinkable> _spawnService;

        public InstantiateSystem(
            GameContext game,
            ILinkedEntityRepository linkedEntityRepository,
            ISpawnService<GameEntity, IObjectLinkable> spawnService
        ) : base(game)
        {
            _linkedEntityRepository = linkedEntityRepository;
            _spawnService = spawnService;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
            => context.CreateCollector(GameMatcher.Instantiate.Added());

        protected override bool Filter(GameEntity entity) 
            => entity.IsInstantiate && !entity.IsDestroyed;

        protected override void Execute(IEnumerable<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var linkable = _spawnService.Spawn(entity);
                if (linkable == null)
                    continue;
				            
                linkable.Link(entity);
                _linkedEntityRepository.Add(linkable.Hash, entity);
            }
        }
    }
}