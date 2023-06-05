using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChestBox : MonoBehaviour
{
    Animator anim;

    // 무기 프리팹 변수 추가
    public GameObject weaponPrefab;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("보물상자 열림");
            anim.SetTrigger("Open");
        }
    }

    // 애니메이션 이벤트를 받아 처리하는 메소드
    public void OnOpenComplete()
    {
        // 무기 프리팹 생성
        Instantiate(weaponPrefab, transform.position, Quaternion.identity);
        // 보물상자 삭제
        Destroy(gameObject);
    }
}
