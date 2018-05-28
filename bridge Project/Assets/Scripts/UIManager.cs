using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] private KeyCode m_Kpackage;
    private PackageManager thePackageManager;
    private void Start()
    {
        thePackageManager = FindObjectOfType<PackageManager>();         //抓取背包腳本
    }
    private void Update()
    {
        if (Input.GetKeyDown(m_Kpackage))
        {
            if (thePackageManager.m_CanPackage.enabled)
            {
                thePackageManager.m_CanPackage.enabled = false;
            }
            else
            {
                thePackageManager.m_CanPackage.enabled = true;
            }
        }
    }
}
