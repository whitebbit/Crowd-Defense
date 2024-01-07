namespace _3._Scripts.Game.Units.Interfaces
{
    public interface IAnimator
    {
        public void State(bool state);
        public void Play(string key);
        public void PlayRandom(string key);
    }
}