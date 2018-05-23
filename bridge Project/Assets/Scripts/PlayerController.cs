using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_rigidbody;
    [SerializeField] private LayerMask Layer_FootFloor;
    [SerializeField] private character.PlayerData thePlayerData;
    private Vector3 v3_Move;
    private Transform _transform;
    private Transform _cameraTransform;
    private void Start()
    {
        _transform = this.transform;
        _cameraTransform = Camera.main.transform;
        _cameraTransform.GetComponent<CameraController>().Fn_SetLookTarget(_transform);         //設定攝影機的追蹤目標為自己
    }
    private void LateUpdate()
    {
        Fn_CharacterMove();
    }
    #region 角色移動
    private void Fn_CharacterMove()
    {
        if (m_rigidbody == null)
        {
            return;
        }

        Vector3 v3_Controller = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        if (v3_Controller.sqrMagnitude >= 0.001f)
        {
            v3_Move = v3_Controller.normalized * thePlayerData.m_fMoveSpeed;
        }
        else
        {
            v3_Move = Vector3.zero;
        }

        Fn_CharacterJump();
        m_rigidbody.velocity = new Vector2(v3_Move.x , m_rigidbody.velocity.y);
    }
    #endregion
    #region 角色跳躍
    private void Fn_CharacterJump()
    {
        if (Fn_IsJumping() && Input.GetKeyDown(KeyCode.Space))
        {
            m_rigidbody.AddForce(Vector2.up * thePlayerData.m_fJumpHight);
        }
    }
    private bool Fn_IsJumping()
    {
        return Physics2D.Raycast((_transform.position + _transform.up) , Vector2.down , 1.45f , Layer_FootFloor);
    }
    #endregion
}
namespace character
{
    [System.Serializable] public class PlayerData
    {
        public float m_fMoveSpeed;
        public float m_fJumpHight;
    }
}
