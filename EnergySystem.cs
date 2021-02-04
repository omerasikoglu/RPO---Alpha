using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    public EnergySystem Instance { get; private set; }

    public event System.EventHandler OnEnergyGenerateActivated;

    //private PlayerController playerController;

    [SerializeField] private int maxEnergy = 3;
    private EnergyTimer energyTimer;
    private float energyRechargeDeltaTime = 2f;

    private float currentEnergy;
    private bool isEnergyGenerating = true;
    private float lastEnergyStopTime = Mathf.NegativeInfinity; //generate'in durduğu an

    private void Awake()
    {
        //playerController = GetComponent<PlayerController>();
        currentEnergy = maxEnergy;
    }
    private void Start()
    {
        Instance = this;
        //playerController.OnEnergySpended += PlayerController_OnEnergySpended;
    }
    private void Update()
    {
        EnergyGenerator();

    }
    private void EnergyGenerator()
    {
        if (energyTimer != null && !isEnergyGenerating) //null kısmı doğru değiştirme
        {
            energyTimer.timer -= Time.deltaTime;
            if (energyTimer.timer <= 0)
            {
                OnEnergyGenerateActivated?.Invoke(this, EventArgs.Empty);
                isEnergyGenerating = true;
            }
        }
        if (!HaveFullEnergy() && isEnergyGenerating)
        {
            currentEnergy += Time.deltaTime;
        }
    }
    //private void PlayerController_OnEnergySpended(object sender, PlayerController.OnEnergySpendedEventArgs e)
    //{
    //    SpendEnergy(e.energyAmount);
    //}

    public bool SpendEnergy(float energyAmount, EnergyTimer energyTimer = null) //enerjiyi harvamaya çalışır.yeterli yoksa false döndürür
    {
        if (HaveEnoughEnergy(energyAmount))
        {
            this.energyTimer = energyTimer;

            if (energyTimer == null)
            {
                this.energyTimer = new EnergyTimer { timer = 3f };
            }
            currentEnergy -= energyAmount;
            currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
            isEnergyGenerating = false;
            return true;
        }
        else
        {
            return false;
            //TODO: kırmızı yanar enerji barın ya da öksürürsün gibi bir şey
            //TODO: tooltiple belki neden enerji harcıyamadığını döndürebilirsin
        }

    }
    public class EnergyTimer //coroutine'in canı cehenneme
    {
        public float timer = 2f;
    }
    public float GetEnergyAmount() { return currentEnergy; }
    public float GetEnergyAmountNormalized() { return currentEnergy / maxEnergy; }
    private bool HaveEnoughEnergy(float needingEnergyAmount)
    {
        return currentEnergy >= needingEnergyAmount ? true : false;
    }
    private bool HaveFullEnergy()
    {
        return currentEnergy >= maxEnergy;
    }


}
