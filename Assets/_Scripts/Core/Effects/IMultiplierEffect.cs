namespace Core
{
    public interface IMultiplierEffect
    {
        float Factor { get; }
        bool IsExpired { get; }
        void Tick(float deltaTime);
    }
}
