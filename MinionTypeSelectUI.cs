//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class MinionTypeSelectUI : MonoBehaviour
//{
//    [SerializeField] private bool isFirstSet;
//    [SerializeField] private Sprite arrowSprite;
//    [SerializeField] private List<MinionTypeSO> ignoreMinionTypeList;
//    [SerializeField] private bool instantSummon=false;

//    private Dictionary<MinionTypeSO, Transform> btnTransformDictionary;
//    private Transform arrowButtonTransform;
//    private MinionSetSO minionSet;
//    private GridLayoutGroup gridLayoutGroup;

//    private void Awake()
//    {
//        Transform btnTemplate = transform.Find("btnTemplate");
//        btnTemplate.gameObject.SetActive(false);

//        if (isFirstSet) minionSet = Resources.Load<MinionSetSO>("MinionSets/MinionSet1");
//        else minionSet = Resources.Load<MinionSetSO>("MinionSets/MinionSet2");

//        btnTransformDictionary = new Dictionary<MinionTypeSO, Transform>();

//       // int index = 0;

//        arrowButtonTransform = Instantiate(btnTemplate, transform);
//        arrowButtonTransform.gameObject.SetActive(true);

//       // float offsetAmount = +120f;
//       // arrowButtonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

//        arrowButtonTransform.Find("image").GetComponent<Image>().sprite = arrowSprite;
//        arrowButtonTransform.Find("image").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -30f);
//        arrowButtonTransform.GetComponent<Button>().onClick.AddListener(() =>
//        {
//            MinionManager.Instance.SetActiveMinionType(null);
//        });

//        MouseEnterExitEvents mouseEnterExitEvents = arrowButtonTransform.GetComponent<MouseEnterExitEvents>();

//        mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) => { TooltipUI.Instance.Show("Arrow"); };
//        mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) => { TooltipUI.Instance.Hide(); };
//        //index++;

//        foreach (MinionTypeSO minionType in minionSet.list)
//        {
//            if (ignoreMinionTypeList.Contains(minionType)) continue;
//            Transform btnTransform = Instantiate(btnTemplate, transform);
//            btnTransform.gameObject.SetActive(true);

//           // offsetAmount = +120f;
//          //  btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

//            btnTransform.Find("image").GetComponent<Image>().sprite = minionType.sprite;
//            btnTransform.GetComponent<Button>().onClick.AddListener(() =>
//            {
//                if (instantSummon)  //tıklandıgında minyonun olusması
//                {
//                    Minion.Create(minionType, true, UtilsClass.GetMouseWorldPosition()); 
//                }
//                else
//                {
//                    MinionManager.Instance.SetActiveMinionType(minionType);
//                }
//            });

//            mouseEnterExitEvents = btnTransform.GetComponent<MouseEnterExitEvents>();

//            mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) =>
//            { TooltipUI.Instance.Show(minionType.nameString + "\n" + minionType.GetSummoningCostsToString()); };
//            mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) => { TooltipUI.Instance.Hide(); };


//            btnTransformDictionary[minionType] = btnTransform;

//           // index++;
//        }
//    }
//    private void Start()
//    {
//        MinionManager.Instance.OnActiveMinionTypeChanged += MinionManager_OnActiveMinionTypeChanged;
//        UpdateActiveMinionTypeIcon();
//    }

//    private void MinionManager_OnActiveMinionTypeChanged(object sender, MinionManager.OnActiveMinionTypeChangedEventArgs e)
//    {
//        UpdateActiveMinionTypeIcon();
//    }
//    private void UpdateActiveMinionTypeIcon()
//    {
//        arrowButtonTransform.Find("selected").gameObject.SetActive(false);

//        foreach (MinionTypeSO minionType in btnTransformDictionary.Keys)
//        {
//            Transform btnTransform = btnTransformDictionary[minionType];
//            btnTransform.Find("selected").gameObject.SetActive(false);
//        }

//        MinionTypeSO activeMinionType = MinionManager.Instance.GetActiveMinionType();
//        if (activeMinionType == null)
//        {
//            arrowButtonTransform.Find("selected").gameObject.SetActive(true);
//        }
//        else
//        {
//            btnTransformDictionary[activeMinionType].Find("selected").gameObject.SetActive(true);
//        }
//    }
//}
