using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#region Verili event yollama
public class Notes : MonoBehaviour
{
    public static Notes Instance { get; private set; }
    private MinionTypeSO activeMinionType;

    public event EventHandler<OnActiveMinionTypeChangedEventArgs> OnActiveMinionTypeChanged;

    public class OnActiveMinionTypeChangedEventArgs : EventArgs
    {
        public MinionTypeSO activeMinionType;
    }
    public void SetActiveMinionType(MinionTypeSO minionType)
    {
        activeMinionType = minionType;
        OnActiveMinionTypeChanged?.Invoke(this,
            new OnActiveMinionTypeChangedEventArgs { activeMinionType = activeMinionType });
    }
    private void Start()
    {
        Instance = this;
    }
}
public class Notes2 : MonoBehaviour
{
    private void Start()
    {
        Notes.Instance.OnActiveMinionTypeChanged += MinionManager_OnActiveMinionTypeChanged;
    }
    private void MinionManager_OnActiveMinionTypeChanged(object sender, Notes.OnActiveMinionTypeChangedEventArgs e)
    {                                                        //(object sender, System.EventArgs e)
        if (e.activeMinionType == null)
        {
            Hide();
        }
        else
        {
            Show(e.activeMinionType.sprite);
        }
    }
    private void Hide() { }
    private void Show(Sprite st) { }
} 
#endregion