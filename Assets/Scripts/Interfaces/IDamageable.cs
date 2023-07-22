using Enums;

namespace Interfaces
{
    public interface IDamageable
    {
        void Damage(float amount, ColorClass color);
        void Kill();
    }
}