using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChestBox : MonoBehaviour
{
    Animator anim;

    // ���� ������ ���� �߰�
    public GameObject weaponPrefab;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("�������� ����");
            anim.SetTrigger("Open");
        }
    }

    // �ִϸ��̼� �̺�Ʈ�� �޾� ó���ϴ� �޼ҵ�
    public void OnOpenComplete()
    {
        // ���� ������ ����
        Instantiate(weaponPrefab, transform.position, Quaternion.identity);
        // �������� ����
        Destroy(gameObject);
    }
}
