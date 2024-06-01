namespace Character.Interfaces
{
    public interface IDamageable
    {
        int MaxHealth { get; }
        int CurrentHealth { get; set; }
        
        void Damage(int value);
        void Die();
    }
}