using System;
using Core.Composition;

namespace Core.Composition
{
    public abstract class Command
    {
        public abstract void Execute();
    }
    
    public class DealDamageCommand : Command
    {
        // Поля инициализируются через конструктор и защищены от изменений
        private readonly Character _target;
        private readonly int _damage;

        // Конструктор (constructor в JS)
        public DealDamageCommand(Character target, int damage)
        {
            _target = target;
            _damage = damage;
        }

        // Метод выполнения логики (execute в JS)
        public override void Execute()
        {
            // Используем ваш рабочий метод GetAbility
            var healthAbility = _target.GetComponent<HealAbility>();

            if (healthAbility == null)
            {
                // В C# вместо Error бросают специализированные исключения
                throw new InvalidOperationException("NoHealthAbility");
            }

            int resultHealth = healthAbility.hp - _damage;

            // Math.Max возвращает большее из двух чисел (аналог Math.max в JS)
            healthAbility.hp = Math.Max(0, resultHealth);
        }
    }
}