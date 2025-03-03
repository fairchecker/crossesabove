using Interfaces;

namespace Model
{
    public class Buff : IBuff
    {
        public IBuff.BuffType Type { get; set; }
        public float Effect { get; set; }

        public Buff(IBuff.BuffType type, float effect)
        {
            Effect = effect;
            Type = type;
        }
    }
}