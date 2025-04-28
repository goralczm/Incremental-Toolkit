namespace Core
{
    public interface IGenerator
    {
        public float GetProduction();

        public int GetLevel();

        public int GetTier();

        public void SetState(bool state);

        public bool GetState();

        public int GetPreviousTierGeneratorsThreshold();

        public void AddEffect(IMultiplierEffect effect);
    }
}
