using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    bool isRotating;
    /// <summary>
    /// ������Ʈ�� ȸ�� ���Դϱ�?
    /// </summary>
    public bool IsRotating
    {
        get { return isRotating; }
    }

    const float angle = 90f;
    public float rotSpeed = 1f;
    GameObject shadow;

    private void Start()
    {
        isRotating = false;
        shadow = new GameObject();
    }

    public void RotateAround(Vector3 dir)
    {
        StartCoroutine(RotateAround_co(dir));
    }

    IEnumerator RotateAround_co(Vector3 dir)
    {
        isRotating = true;

        //ȸ���� ����
        //���� ������ �Ʒ��� �𼭸�
        Vector3 pivotPoint = transform.position + new Vector3(dir.x * .5f, -.5f, dir.z * .5f);
        Vector3 axis = new Vector3(dir.z, 0, -dir.x);
        
        Vector3 targetPosition = transform.position + dir;

        //���� ������Ʈ ���
        shadow.transform.position = transform.position;
        shadow.transform.rotation = transform.rotation;
        shadow.transform.RotateAround(pivotPoint, axis, angle);

        float elapsed = 0f;
        while (elapsed < 1)
        {
            transform.RotateAround(pivotPoint, axis, angle * Time.fixedDeltaTime * rotSpeed);
            yield return new WaitForFixedUpdate(); // 0.02�� ���
            elapsed += Time.fixedDeltaTime * rotSpeed;
        }

        transform.position = targetPosition;
        transform.rotation = shadow.transform.rotation;

        if (!Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, .51f))
        {
            if (TryGetComponent(out Rigidbody rb))
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }
            yield break;
        }
        else
        {
            print(hit.transform.name);
        }

        isRotating = false;
    }

    private void OnBecameInvisible()
    {
        SceneManager.LoadScene(0);
    }
}
