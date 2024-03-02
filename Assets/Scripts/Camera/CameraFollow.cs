using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private Vector3 _offset;
    [SerializeField]
    private float _damping;
    [SerializeField]
    private Transform _target;

    private Vector3 _velocity = Vector3.zero;

    #endregion

    #region Unity Methods

    private void FixedUpdate()
    {
        Vector3 targetPos = _target.position + _offset;
        targetPos.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _velocity, _damping);
    }

    #endregion
}
