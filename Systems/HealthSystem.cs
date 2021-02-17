using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
    public HealthSystem Instance { get; private set; }

    public event System.EventHandler OnMaxHealthAmountIncreased;
    public event EventHandler<OnDamagedEventArgs> OnDamaged;
    public event System.EventHandler OnDied;
    public class OnDamagedEventArgs : EventArgs
    {
        public int direction;
    }

    [SerializeField] private int healthAmountMax;

    private int currentHealthAmount;
    private int lastHealthAmount; //damage yemeden önceki canı

    private void Awake()
    {
        currentHealthAmount = healthAmountMax;
        lastHealthAmount = healthAmountMax;
    }
    private void Start()
    {
        Instance = this;
    }
    public void Damage(AttackDetails attackDetails)
    {
        int direction;
        if (attackDetails.damageType != null)
        {
            //TODO: damage typea göre damagei buraya ekle
        }
        if (attackDetails.position.x < transform.position.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
       
        currentHealthAmount -= attackDetails.damageAmount;
        currentHealthAmount = Mathf.Clamp(currentHealthAmount, 0, healthAmountMax);

        OnDamaged?.Invoke(this, new OnDamagedEventArgs { direction = direction });
        if (IsDead())
        {
            OnDied?.Invoke(this, EventArgs.Empty);
        }
        lastHealthAmount = currentHealthAmount;
    }

    public void Heal(int healAmount)
    {
        currentHealthAmount += healAmount;
    }
    public bool IsFullHealth()
    {
        return currentHealthAmount == healthAmountMax;
    }
    public bool IsDead()
    {
        return currentHealthAmount <= 0;
    }
    public int GetHealthAmount()
    {
        return currentHealthAmount;
    }
    public int GetHealthAmountMax()
    {
        return healthAmountMax;
    }
    public void SetHealthAmountMax(int healthAmountMax, bool updateHealthAmount)
    {
        this.healthAmountMax = healthAmountMax;
        if (updateHealthAmount)
        {
            currentHealthAmount = healthAmountMax;
        }
    }
    public float GetHealthAmountNormalized()
    {
        return (float)currentHealthAmount / healthAmountMax;
    }
    public int GetLastHealthAmount()
    {
        return lastHealthAmount;
    }
    public float GetLastHealthAmountNormalized()
    {
        return (float) lastHealthAmount/ healthAmountMax;
    }
    public void IncreaseMaxHealthAmount(int amount)
    {
        healthAmountMax += amount;
        OnMaxHealthAmountIncreased?.Invoke(this, System.EventArgs.Empty);
    }
}
