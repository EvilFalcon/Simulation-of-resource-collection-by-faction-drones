namespace Game.Services.Spawners
{
    public interface IResourcesSpawner
    {
        float SpawnDelay { get; }

        void Create();
        void SetSpawnDelay(float delay);
    }
}