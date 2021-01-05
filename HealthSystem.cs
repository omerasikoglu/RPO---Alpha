using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamaged;
    public event EventHandler OnDied;

    private int healthAmountMax; //current health amount
    private int healthAmount;

    private void Awake()
    {
        healthAmount = healthAmountMax;
    }
    public void Damage(int damageAmount, DamageTypeSO damageType = null)
    {
        if (damageType != null)
        {
            //damage typea göre damagei buraya ekle
        }

        healthAmount -= damageAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);

        OnDamaged?.Invoke(this, EventArgs.Empty);
        if (IsDead())
        {
            OnDied?.Invoke(this, EventArgs.Empty);
        }
    }

    public void Heal(int healAmount)
    {
        healthAmount += healAmount;
    }
    public bool IsFullHealth()
    {
        return healthAmount == healthAmountMax;
    }
    public bool IsDead()
    {
        return healthAmount <= 0;
    }
    public int GetHealthAmount()
    {
        return healthAmount;
    }
    public void SetHealthAmountMax(int healthAmountMax, bool updateHealthAmount)
    {
        this.healthAmountMax = healthAmountMax;
        if (updateHealthAmount)
        {
            healthAmount = healthAmountMax;
        }
    }
    public float GetHealthAmountNormalized()
    {
        return (float)healthAmount / healthAmountMax;
    }
}
