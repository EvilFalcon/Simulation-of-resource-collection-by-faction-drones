using System;
using System.Collections.Generic;
using Game.Utils;
using JCMG.EntitasRedux;
using UnityEngine.Pool;

namespace Ecs.Utils.Groups.Impl
{
    public class GameGroupUtils : IGameGroupUtils
    {
        private readonly IGroup<GameEntity> _linesGroup;
        private readonly IGroup<GameEntity> _movableFiguresGroup;
 
        public GameGroupUtils(GameContext game)
        {
            // _linesGroup = game.GetGroup(GameMatcher.AllOf(GameMatcher.Line));
            // _movableFiguresGroup = game.GetGroup(GameMatcher.AllOf(GameMatcher.MovableFigure));
        }
        
        // public IDisposable GetLines(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null)
        // {
        //     return GetEntities(out buffer, _linesGroup, e => !e.IsDestroyed, filter);
        // }
        //
        // public IDisposable GetLinesWithActiveCheck(out List<GameEntity> buffer, bool isActive, Func<GameEntity, bool> filter = null)
        // {
        //     return isActive 
        //         ? GetEntities(out buffer, _linesGroup, e => e.IsActive && !e.IsDestroyed, filter) 
        //         : GetEntities(out buffer, _linesGroup, e => !e.IsActive && !e.IsDestroyed, filter);
        // }
        //
        // public IDisposable GetMovableFigures(out List<GameEntity> buffer, Func<GameEntity, bool> filter = null)
        // {
        //     return GetEntities(out buffer, _movableFiguresGroup, e => !e.IsDestroyed, filter);
        //
        // }
        // public IDisposable GetMovableFigureWithActiveCheck(out List<GameEntity> buffer, bool isActive, Func<GameEntity, bool> filter = null)
        // {
        //     return isActive 
        //         ? GetEntities(out buffer, _movableFiguresGroup, e => e.IsActive && !e.IsDestroyed, filter) 
        //         : GetEntities(out buffer, _movableFiguresGroup, e => !e.IsActive && !e.IsDestroyed, filter);
        // }
        //
        // private IDisposable GetEntities(
        //     out List<GameEntity> buffer,  
        //     IGroup<GameEntity> group,
        //     Func<GameEntity, bool> baseFilter, 
        //     Func<GameEntity, bool> filter = null)
        // {
        //     var pooledObject = ListPool<GameEntity>.Get(out buffer);
        //     group.GetEntities(buffer);
        //
        //     if (filter != null)
        //     {
        //         buffer.RemoveAllWithSwap(e => !(baseFilter(e) && filter(e)));    
        //     }
        //     else
        //     {
        //         buffer.RemoveAllWithSwap(e => !baseFilter(e));
        //     }
        //
        //     return pooledObject;
        // }
    }
}
