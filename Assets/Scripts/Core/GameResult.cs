namespace Core
{
    public readonly struct GameResult
    {
        public readonly bool Win;
        public readonly EFailReason? FailReason;

        private GameResult(bool win, EFailReason? failReason)
        {
            Win = win;
            FailReason = failReason;
        }

        public static GameResult Success()
        {
            return new GameResult(true, null);
        }
        
        public static GameResult Fail(EFailReason reason)
        {
            return new GameResult(false, reason);
        }
        
        public enum EFailReason
        {
            KilledByMonster,
            KilledByCollisions
        }
    }
}