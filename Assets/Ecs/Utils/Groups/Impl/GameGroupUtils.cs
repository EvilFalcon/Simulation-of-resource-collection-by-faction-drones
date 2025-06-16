using System;
using System.Collections.Generic;
using Game.Utils;
using JCMG.EntitasRedux;
using UnityEngine.Pool;

namespace Ecs.Utils.Groups.Impl
{
    public class GameGroupUtils : IGameGroupUtils
    {
        private readonly IGroup<GameEntity> _unitsGroup;
        private readonly IGroup<GameEntity> _movableFiguresGroup;
 
        public GameGroupUtils(GameContext game)
        {
            _unitsGroup = game.GetGroup(GameMatcher.AllOf(GameMatcher.Unit));
        }
        
        public IDisposable GetUnits(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null)
        {
            return GetEntities(out buffer, _unitsGroup, e => !e.IsDestroyed, filter);
        }
        
        private IDisposable GetEntities(
            out List<GameEntity> buffer,  
            IGroup<GameEntity> group,
            Func<GameEntity, bool> baseFilter, 
            Func<GameEntity, bool> filter = null)
        {
            var pooledObject = ListPool<GameEntity>.Get(out buffer);
            group.GetEntities(buffer);
 
            if (filter != null)
            {
                buffer.RemoveAllWithSwap(e => !(baseFilter(e) && filter(e)));    
            }
            else
            {
                buffer.RemoveAllWithSwap(e => !baseFilter(e));
            }
 
            return pooledObject;
        }
    }
}
