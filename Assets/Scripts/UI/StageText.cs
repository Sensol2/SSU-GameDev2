using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StageText : MonoBehaviour
{
    SpriteRenderer spriter;
    void Start()
    {
        spriter = GetComponent<SpriteRenderer>();
        StartCoroutine(TextOut());
    }

    IEnumerator TextOut()
    {
        yield return new WaitForSeconds(3.0f);
        this.transform.DOMove(new Vector3(transform.position.x, 3000), 1.0f).SetEase(Ease.InOutQuad);
    }
}
