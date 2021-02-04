using System;                                               //
using System.Collections;                                   //
using System.Collections.Generic;                           //<RectTransform>().localPosition --> posx
using UnityEngine;                                          //<RectTransform>().sizeDelta --> width height -->alttaki
using UnityEngine.UI;                                       //
                                                            //
public class HealthUI : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;

    private Transform barTransform;
    private Transform lerpBarTransform;
    private Transform seperatorContainerTransform;
    private Transform bgTransform;
    private Transform borderTransform;

    private float lerpSpeed = 3f;

    private float barOffsetAmount = 20f;
    private float barsizeDeltaXAmount = 40f;
    private float barSizeDeltaYAmount = 20f;
    private float extraBorderAmount = 5f;

    private MouseEnterExitEvents mouseEnterExitEvents;

    private void Awake()
    {
        borderTransform = transform.Find("border");
        bgTransform = transform.Find("background");
        barTransform = transform.Find("bar");
        lerpBarTransform = transform.Find("lerpBar");
    }

    private void Start()
    {
        UpdateBarSizes(); // level atladıgında değişen barlar
        UpdateBars(); // canın değiştiğinde değişen barlar

        healthSystem.OnDamaged += HealthSystem_OnDamaged;

        mouseEnterExitEvents = transform.GetComponent<MouseEnterExitEvents>();
        mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) => { TooltipUI.Instance.Show($"Current Health: <color=#FF0000>{healthSystem.GetHealthAmount()}</color>"); };
        mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) => { TooltipUI.Instance.Hide(); };
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        UpdateBars();
    }
    private void Update()
    {
        CheckLerp();
    }
    private void UpdateBars()
    {
        barTransform.localScale = new Vector3(healthSystem.GetHealthAmountNormalized(), 1f, 1f);
        lerpBarTransform.localScale = new Vector3(healthSystem.GetLastHealthAmountNormalized(), 1f, 1f);
    }
    private void CheckLerp()
    {
        lerpBarTransform.localScale = new Vector3(Mathf.Lerp
        (lerpBarTransform.localScale.x, barTransform.localScale.x, lerpSpeed * Time.deltaTime), 1f, 1f);
    }
    private void UpdateBarSizes()
    {
        UpdateBorder();
        UpdateBackground();
        UpdateHealthBar();
        UpdateLerpBar();
        UpdateSeperatorContainer();
    }
    private void UpdateBorder()
    {
        int healthCount = healthSystem.GetHealthAmount();
        borderTransform.transform.GetComponent<RectTransform>().localPosition = new Vector3
            (barOffsetAmount * (int)healthCount, 0f, 0f);   
        borderTransform.transform.GetComponent<RectTransform>().sizeDelta = new Vector2
            (barsizeDeltaXAmount * (int)healthCount, barSizeDeltaYAmount + extraBorderAmount);
    }
    private void UpdateBackground()
    {
        int healthCount = healthSystem.GetHealthAmount();
        bgTransform.transform.GetComponent<RectTransform>().sizeDelta = new Vector2
            (barsizeDeltaXAmount * (int)healthCount, barSizeDeltaYAmount);
    }
    private void UpdateHealthBar()
    {
        Transform bar = barTransform.Find("barImage");
        int healthCount = healthSystem.GetHealthAmount();
        bar.transform.GetComponent<RectTransform>().localPosition = new Vector3
            (barOffsetAmount * (int)healthCount, 0f, 0f);
        bar.transform.GetComponent<RectTransform>().sizeDelta = new Vector2
            (barsizeDeltaXAmount * (int)healthCount, barSizeDeltaYAmount);
    }
    private void UpdateLerpBar()
    {
        Transform bar = lerpBarTransform.Find("barImage");
        int healthCount = healthSystem.GetHealthAmount();
        bar.transform.GetComponent<RectTransform>().localPosition = new Vector3
            (barOffsetAmount * (int)healthCount, 0f, 0f);
        bar.transform.GetComponent<RectTransform>().sizeDelta = new Vector2
            (barsizeDeltaXAmount * (int)healthCount, barSizeDeltaYAmount);
    }
    private void UpdateSeperatorContainer()
    {
        seperatorContainerTransform = transform.Find("seperatorContainer");
        Transform seperatorTemplate = seperatorContainerTransform.Find("seperatorTemplate");
        //seperatorTemplate.gameObject.SetActive(false);
        int seperatorTemplateCount = healthSystem.GetHealthAmount();

        for (int i = 1; i < seperatorTemplateCount + 1; i++) //total'den bir eksik
        {
            Transform templateTransform = Instantiate(seperatorTemplate, seperatorContainerTransform);
            //templateTransform.gameObject.SetActive(true);
            templateTransform.localPosition = new Vector3(barsizeDeltaXAmount * i, 0f, 0f);
        }
    }

}
