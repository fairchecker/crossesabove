namespace Interfaces
{
    public interface IBuff
    {
        enum BuffType
        {
            SpeedBuff,
            HealthBuff,
            ArmorBuff,
        }
        
        public BuffType Type { get; set; }
        public float Effect { get; set; }
    }
}