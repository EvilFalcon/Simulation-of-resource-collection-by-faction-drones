namespace Game.Services.Pools
{
    public interface IClearablePool
    {
        void CreateSubPools();
        public void Clear();
    }
}