using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // React(WebGL)에서 호출할 메서드
    public void SetTrigger(string triggerName)
    {
        animator.ResetTrigger(triggerName);
        animator.SetTrigger(triggerName);
    }

    public void SetBool(string paramName, bool value)
    {
        animator.SetBool(paramName, value);
    }

    // 에디터에서 바로 실행해볼 ContextMenu 예시
    [ContextMenu("Play HelloAnim")]
    private void PlayHelloAnim()
    {
        SetTrigger("Play_안녕하세요");
    }

    // 런타임 키 테스트 예시
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SetTrigger("Play_안녕하세요");
    }
}
