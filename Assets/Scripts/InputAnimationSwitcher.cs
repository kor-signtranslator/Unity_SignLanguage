using UnityEngine;

public class InputAnimationSwitcher : MonoBehaviour
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 스페이스바를 누르면 button = true → feel_2_mp4_Armature 전환
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("button", true);
        }
        // 스페이스바가 아닌 다른 키를 누르면 button = false → 기본 상태로 복귀
        else if (Input.anyKeyDown)
        {
            anim.SetBool("button", false);
        }
    }
}
