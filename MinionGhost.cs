//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class MinionGhost : MonoBehaviour
//{
//    private GameObject spriteGameObject;
//    private void Awake()
//    {
//        spriteGameObject = transform.Find("sprite").gameObject;
//        Hide();
//    }
//    private void Start()
//    {
//        MinionManager.Instance.OnActiveMinionTypeChanged += MinionManager_OnActiveMinionTypeChanged;
//    }
//    private void Update()
//    {
//        transform.position = UtilsClass.GetMouseWorldPosition();
//    }
//    private void MinionManager_OnActiveMinionTypeChanged(object sender, MinionManager.OnActiveMinionTypeChangedEventArgs e)
//    {                                                        //(object sender, System.EventArgs e)
//        if (e.activeMinionType == null)
//        {
//            Hide();
//        }
//        else
//        {
//            Show(e.activeMinionType.sprite);
//        }
//    }
//    private void Show(Sprite ghostSprite)
//    {
//        spriteGameObject.SetActive(true);
//        spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
//    }
//    private void Hide()
//    {
//        spriteGameObject.SetActive(false);
//    }
//}
