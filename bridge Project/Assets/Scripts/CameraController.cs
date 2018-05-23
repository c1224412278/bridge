using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private camera.CameraData theCameraData;

    private Transform _transform;
    private void Start()
    {
        this._transform = this.transform;
    }
    private void LateUpdate()
    {
        Fn_CameraMove();
    }
    private void Fn_CameraMove()
    {
        if (theCameraData.m_Ttarget == null)
        {
            return;
        }

        Vector3 direction = (theCameraData.m_Ttarget.position - this.transform.position);
        Vector3 pos = this.transform.position;
        pos += new Vector3(direction.x, direction.y, 0f);
        pos = new Vector3(Mathf.Clamp(pos.x , theCameraData.m_fMinXvalue , theCameraData.m_fMaxXvalue) , pos.y , pos.z);

        this.transform.position = pos;
    }
    public void Fn_SetLookTarget(Transform trans)
    {
        theCameraData.m_Ttarget = trans;
    }
}
namespace camera
{
    [System.Serializable]public class CameraData
    {
        public float m_fMinXvalue;
        public float m_fMaxXvalue;
        public float m_fMoveSpeed;
        public Transform m_Ttarget;
    }
}
