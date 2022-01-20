 using UnityEngine;

 namespace Components.Hero_files
{
    public class TargetFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _CameraSpeed;

        // ������ ������ ������ ���� � late update, ����� ������ ������������ ��� ��������� ������� �����

        private void LateUpdate()
        {
            var destination = new Vector3(_target.position.x, _target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * _CameraSpeed);

        }

    }

}
