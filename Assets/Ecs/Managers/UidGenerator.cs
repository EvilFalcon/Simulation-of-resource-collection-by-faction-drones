using System;

namespace Ecs.Managers
{
    public static class UidGenerator
    {
        private static readonly object Locker = new();
        private static uint _current = uint.MinValue;

        public static Uid Next()
        {
            lock (Locker)
            {
                if (_current == uint.MaxValue)
                    throw new Exception($"[{nameof(UidGenerator)}] Uid reached max value: {uint.MaxValue}");
                
                var uid = (Uid)_current;
                
                _current++;

                return uid;
            }
        }
    }
}