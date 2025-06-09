using UnityEngine;

public class CanvasAutoHide3s : MonoBehaviour
{
    [SerializeField] private Canvas canvasToHide;

    void Start()
    {
        // 시작 후 3초 뒤에 HideCanvas() 호출
        Invoke(nameof(HideCanvas), 3f);
    }

    private void HideCanvas()
    {
        if (canvasToHide != null)
            canvasToHide.gameObject.SetActive(false);
        else
            Debug.LogWarning("canvasToHide가 할당되지 않았습니다.");
    }
}
