using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SceneTransition : MonoBehaviour
{
    public float transitionTime = 1f; // ���̵� ��/�ƿ��� �ɸ��� �ð�

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
