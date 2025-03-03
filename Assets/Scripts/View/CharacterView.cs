using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Utils;

namespace View
{
    public class CharacterView : MonoBehaviour, IMovable, IBuffable, IDamageable
    {
        public float Speed { get; set; }
        
        public float HealthPoint { get; set; }
        public float Armor { get; set; }
        public float MaxHealthPoint { get; set; }
        
        public List<IBuff> Buffs { get; set; }
        public IBuff FindBuffByType { get; set; }
        
        private bool _isMoving = false;

        public void Move(Vector2 movement)
        {
            if (_isMoving) return;
            
            if (movement.x > 0.3)
            {
                StartCoroutine(MovingCooldownCoroutine(new Vector2(0.32f, 0)));
            }
            else if (movement.x < -0.3)
            {
                StartCoroutine(MovingCooldownCoroutine(new Vector2(-0.32f, 0)));
            }
            else if (movement.y > 0.3)
            {
                StartCoroutine(MovingCooldownCoroutine(new Vector2(0, 0.32f)));
            }
            else if (movement.y < -0.3)
            {
                StartCoroutine(MovingCooldownCoroutine(new Vector2(0, -0.32f)));
            }
        }

        private IEnumerator MovingCooldownCoroutine(Vector2 direction)
        {
            Vector2 targetPosition = new Vector2(transform.position.x + direction.x, transform.position.y  + direction.y);
            
            if (!PhysicsUtils.CheckPosition(targetPosition) || _isMoving) yield break;
            
            _isMoving = true;
            while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
                yield return null;
            }
            
            transform.position = targetPosition;
            _isMoving = false;
        }
        
        IBuff IBuffable.FindBuffByType(IBuff.BuffType buffType)
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
        
        public void TakeDamage(float damage)
        {
            HealthPoint -= damage;
            if (HealthPoint < 0)
            {
                Debug.Log("Pizda tebe");
            }
        }
        
        public void Initialize(float speed)
        {
            Speed = speed;
        }
    }
}