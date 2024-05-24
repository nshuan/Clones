namespace Character.Interfaces
{
    public interface IDamageable
    {
        int MaxHealth { get; set; }
        int CurrentHealth { get; set; }
        
        void Damage(int value);
        void Die();
    }
}