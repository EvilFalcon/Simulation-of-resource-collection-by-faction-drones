using Ecs.Views.Linkable;
using JCMG.EntitasRedux;

namespace Ecs.Game.Components
{
    [Game]
    public class LinkComponent : IComponent
    {
        public IObjectLinkable View;

        public override string ToString()
            => "Link: " + ((View != null && View.Transform != null) ? View.Transform.name : "Null");
    }
}