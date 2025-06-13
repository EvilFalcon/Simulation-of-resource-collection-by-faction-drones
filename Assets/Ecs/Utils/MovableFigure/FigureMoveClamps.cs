namespace Ecs.Utils.MovableFigure
{
    public readonly struct FigureMoveClamps
    {
        public readonly float MinPositionX;
        public readonly float MaxPositionX;
        public readonly float MinPositionY;
        public readonly float MaxPositionY;

        public FigureMoveClamps(
            float minPositionX,
            float maxPositionX,
            float minPositionY,
            float maxPositionY
        )
        {
            this.MinPositionX = minPositionX;
            this.MaxPositionX = maxPositionX;
            this.MinPositionY = minPositionY;
            this.MaxPositionY = maxPositionY;
        }
    }
}