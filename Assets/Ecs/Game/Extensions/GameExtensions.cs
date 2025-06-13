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
            EUnitType unitType
        )
        {
            var entity = context.CreateEntity();
            entity.AddUid(UidGenerator.Next());
            InitUnit(entity, unitType);
            return entity;
        }

        public static GameEntity CreateGameMap(this GameContext context)
        {
            var entity = context.CreateEntity();
            entity.AddUid(UidGenerator.Next());
            entity.AddCorePrefab(EObjectType.WorldMap);
            entity.IsInstantiate = true;
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

        private static void InitUnit(GameEntity gamePlayer, EUnitType unitType)
        {
            gamePlayer.AddUnitPrefab(unitType);
            gamePlayer.AddVelocity(Vector3.zero);
            gamePlayer.AddPosition(Vector3.zero);
            gamePlayer.AddRotation(Quaternion.identity);
            gamePlayer.AddLookDirection(Vector3.forward);
            gamePlayer.AddSpeed(0);
            gamePlayer.IsInstantiate = true;
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
    }
}