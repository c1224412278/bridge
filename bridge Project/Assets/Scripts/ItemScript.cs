using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ItemScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private int m_iLayerIndex;

    private Vector3 v3_mousePos
    {
        get { return Input.mousePosition; }
    }
    private Vector3 v3_originalPosition;            //拖曳的開始座標位置
    private GameObject m_ObjDragItem;               //當前正在拖曳的物件
    private RectTransform m_RectParentField;        //父物件儲存格
    private PackageManager thePackageManager;
    private void Start()
    {
        thePackageManager = FindObjectOfType<PackageManager>();         //抓取背包腳本
        if (this.gameObject.layer != m_iLayerIndex) this.gameObject.layer = m_iLayerIndex;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject m_SelectItem = eventData.pointerEnter;

        if (m_SelectItem != null)
        {
            if (m_SelectItem.layer == m_iLayerIndex && !thePackageManager.m_BIsDraging)
            {
                m_ObjDragItem = m_SelectItem;
                m_RectParentField = this.transform.parent.GetComponent<RectTransform>();       //抓取父物件背包格子
                v3_originalPosition = m_RectParentField.gameObject.transform.position;          //儲存開始拖曳座標位置

                CanvasGroup theGroup = this.GetComponent<CanvasGroup>();
                theGroup.blocksRaycasts = false;

                gameObject.transform.parent = thePackageManager.m_CanPackage.transform;
                thePackageManager.m_BIsDraging = true;              //執行拖曳
            }
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (thePackageManager.m_BIsDraging && m_ObjDragItem != null)
        {
            m_ObjDragItem.transform.position = v3_mousePos;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject m_ExitItem = eventData.pointerEnter;
        if (m_ExitItem == this.gameObject) m_ExitItem = null;

        if (m_ExitItem == null || m_ExitItem.name != "field")
        {
            thePackageManager.m_BIsDraging = false;
            this.transform.position = v3_originalPosition;
            this.transform.parent = m_RectParentField.transform;
        }
        else
        {
            RectTransform m_RectField = m_ExitItem.GetComponent<RectTransform>();
            Vector3 pos = m_RectField.transform.position;

            this.transform.parent = m_RectField.transform;
            this.transform.position = pos;
        }

        CanvasGroup theGroup = this.GetComponent<CanvasGroup>();
        theGroup.blocksRaycasts = true;

        thePackageManager.m_LayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        thePackageManager.m_LayoutGroup.constraintCount = 3;

        thePackageManager.m_BIsDraging = false;              //執行拖曳
    }
}
