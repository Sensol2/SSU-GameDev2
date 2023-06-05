using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SceneTransition : MonoBehaviour
{
    public float transitionTime = 1f; // 페이드 인/아웃에 걸리는 시간

    private Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
        DontDestroyOnLoad(transform.parent.gameObject);
    }

    public void FadeIn()
    {
        image.DOFade(0, transitionTime);
    }

    public void FadeOut()
    {
        image.DOFade(1, transitionTime);
    }
}
