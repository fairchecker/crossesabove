namespace Interfaces
{
    public interface IDamageable
    {
        public float HealthPoint { get; set; }
        public float Armor { get; set; }
        public float MaxHealthPoint { get; set; }

        public void TakeDamage(float damage);
    }
}