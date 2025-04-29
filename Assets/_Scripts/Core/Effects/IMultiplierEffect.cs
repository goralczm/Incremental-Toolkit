namespace Core
{
    public interface IMultiplierEffect
    {
        public float Factor { get; }
        public bool IsExpired { get; }
        public void Tick(float deltaTime);
    }
}
