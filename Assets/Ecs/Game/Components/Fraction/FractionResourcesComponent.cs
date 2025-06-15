using JCMG.EntitasRedux;

namespace Ecs.Game.Components.Fraction
{
    [Game]
    [Event(EventTarget.Self)]
    public class FractionResourcesComponent : IComponent
    {
        public FractionResources Value;
    }

    public struct FractionResources
    {
        public int Mithril;
        public int Crystal;
    }
}