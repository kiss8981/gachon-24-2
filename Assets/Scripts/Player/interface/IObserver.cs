public interface IHealthObserver
{
    void OnHealthChanged(int newHealth);
}

public interface IWaveObserver
{
    void OnWaveChanged(int newWave);
}

public interface IResourcesObserver
{
    void OnResourcesChanged(int newResources);
}
