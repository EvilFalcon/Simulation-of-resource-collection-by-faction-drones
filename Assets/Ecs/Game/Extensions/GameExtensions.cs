using Db;
using Db.GameObjectsBase.Impl;
using Ecs.Managers;
using UnityEngine;

namespace Ecs.Game.Extensions
{
    public static class GameExtensions
    {
        public static GameEntity CreateUnit(
            this GameContext context,
            EFractionType fractionType
        )
        {
            var entity = context.CreateEntity();
            entity.AddUid(UidGenerator.Next());
            InitUnit(entity, fractionType);
            return entity;
        }

        public static GameEntity CreateGameMap(this GameContext context)
        {
            var entity = context.CreateEntity();
            entity.AddUid(UidGenerator.Next());
            return entity;
        }

        public static GameEntity CreateFraction(this GameContext context, EFractionType fractionType)
        {
            var entity = context.CreateEntity();
            entity.AddUid(UidGenerator.Next());
            entity.AddFractionType(fractionType);
            return entity;
        }

        public static GameEntity CreateResource(this GameContext context)
        {
            var entity = context.CreateEntity();
            entity.AddUid(UidGenerator.Next());
            return entity;
        }

        public static GameEntity CreatePlayer(
            this GameContext context
        )
        {
            var entity = context.CreateEntity();
            entity.AddUid(UidGenerator.Next());
            return entity;
        }

        public static GameEntity CreateCamera(this GameContext context)
        {
            var entity = context.CreateEntity();
            entity.AddUid(UidGenerator.Next());
            entity.AddCorePrefab(EObjectType.Camera);
            entity.AddPosition(Vector3.zero);
            entity.AddRotation(Quaternion.identity);
            entity.IsCamera = true;
            entity.IsInstantiate = true;
            return entity;
        }

        public static GameEntity CreateVirtualCamera(this GameContext context)
        {
            var entity = context.CreateEntity();
            entity.AddUid(UidGenerator.Next());
            entity.AddCorePrefab(EObjectType.VirtualCamera);
            entity.AddPosition(Vector3.zero);
            entity.AddRotation(Quaternion.identity);
            entity.IsInstantiate = true;
            return entity;
        }

        private static void InitUnit(GameEntity entity, EFractionType fractionType)
        {
            entity.AddUnitPrefab(fractionType);
            entity.AddFractionType(fractionType);
        }
    }
}