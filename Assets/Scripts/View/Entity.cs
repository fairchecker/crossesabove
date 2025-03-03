using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace View
{
    public abstract class Entity : MonoBehaviour, IDamageable, IBuffable, IMovable
    {
        public float HealthPoint { get; set; }
        public float Armor { get; set; }
        public float MaxHealthPoint { get; set; }
        public float Speed { get; set; }
        
        public List<IBuff> Buffs { get; set; }
        
        public void TakeDamage(float damage)
        {
            HealthPoint -= damage;
            if (HealthPoint < 0)
            {
                Debug.Log("Pizda tebe");
            }
        }
        
        public IBuff FindBuffByType(IBuff.BuffType buffType)
        {
            foreach (var buff in Buffs)
            {
                if (buff.Type == buffType) return buff;
            }
            return null;
        }

        public void AddBuff(IBuff buff)
        {
            switch (buff.Type)
            {
                case IBuff.BuffType.ArmorBuff:
                    Armor += buff.Effect;
                    Armor = Armor < 0 ? 0 : Armor;
                    break;
                case IBuff.BuffType.HealthBuff:
                    MaxHealthPoint += buff.Effect;
                    HealthPoint += buff.Effect;
                    MaxHealthPoint = MaxHealthPoint < 1 ? 1 : MaxHealthPoint;
                    HealthPoint = HealthPoint < 1 ? 1 : HealthPoint;
                    break;
                case IBuff.BuffType.SpeedBuff:
                    Speed += buff.Effect;
                    Speed = Speed < 0.25f ? 0.25f : Speed;
                    break;
            }
            Buffs.Add(buff);
        }

        public void RemoveBuff(IBuff buff)
        {
            switch (buff.Type)
            {
                case IBuff.BuffType.ArmorBuff:
                    Armor -= buff.Effect;
                    Armor = Armor < 0 ? 0 : Armor;
                    break;
                case IBuff.BuffType.HealthBuff:
                    MaxHealthPoint -= buff.Effect;
                    HealthPoint -= buff.Effect;
                    MaxHealthPoint = MaxHealthPoint < 1 ? 1 : MaxHealthPoint;
                    HealthPoint = HealthPoint < 1 ? 1 : HealthPoint;
                    break;
                case IBuff.BuffType.SpeedBuff:
                    Speed -= buff.Effect;
                    Speed = Speed < 0.25f ? 0.25f : Speed;
                    break;
            }
            Buffs.Remove(buff);
        }
        
        public virtual void Move(Vector2 movement)
        {
            throw new System.NotImplementedException();
        }

        public void Initialize(float healthPoint, float armor, float maxHealthPoint, float speed)
        {
            HealthPoint = healthPoint;
            Armor = armor;
            MaxHealthPoint = maxHealthPoint;
            Speed = speed;
        }
    }
}