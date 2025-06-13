using Ecs.Utils;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components
{
    [Game]
    [Unique]
    [Event(EventTarget.Self)]
    public class GameStateComponent : IComponent
    {
        public EGameState Value;
    }
}