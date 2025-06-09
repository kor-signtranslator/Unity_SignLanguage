using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class VoiceControlledAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space 눌림 → sayHello");
            animator.SetTrigger("sayHello");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W 눌림 → sayWhere");
            animator.SetTrigger("sayWhere");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("S 눌림 → saySick");
            animator.SetTrigger("saySick");
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("H 눌림 → sayWhen");
            animator.SetTrigger("sayWhen");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F 눌림 → sayFrom");
            animator.SetTrigger("sayFrom");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E 눌림 → sayExamine");
            animator.SetTrigger("sayExamine");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("T 눌림 → sayStart");
            animator.SetTrigger("sayStart");
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log(" M눌림 → sayMedicine");
            animator.SetTrigger("sayMedicine");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(" A눌림 → sayEat");
            animator.SetTrigger("sayEat");
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log(" N눌림 → sayFinish");
            animator.SetTrigger("sayFinish");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log(" C눌림 → sayNeck");
            animator.SetTrigger("sayNeck");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log(" B눌림 → sayBoom");
            animator.SetTrigger("sayBoom");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log(" O눌림 → sayHot");
            animator.SetTrigger("sayHot");
        }
    }


    public void OnVoiceCommand(string text)
    {
        if (text.Contains("안녕하세요") || text.Contains("안녕"))
        {
            Debug.Log($"▶ OnVoiceCommand 호출됨: {text}");
            animator.SetTrigger("sayHello");     // ← SetFloat → SetTrigger
        }
        else if (text.Contains("어디"))
        {
            Debug.Log($"▶ OnVoiceCommand 호출됨: {text}");
            animator.SetTrigger("sayWhere");
        }
        else if (text.Contains("불편") || text.Contains("아프"))
        {
            Debug.Log($"▶ OnVoiceCommand 호출됨: {text}");
            animator.SetTrigger("saySick");
        }
        else if (text.Contains("언제"))
        {
            Debug.Log($"▶ OnVoiceCommand 호출됨: {text}");
            animator.SetTrigger("sayWhen");
        }
        else if (text.Contains("부터"))
        {
            Debug.Log($"▶ OnVoiceCommand 호출됨: {text}");
            animator.SetTrigger("sayFrom");
        }
        else if (text.Contains("진단") || text.Contains("진료")
            || text.Contains("진찰") || text.Contains("검진"))
        {
            Debug.Log($"▶ OnVoiceCommand 호출됨: {text}");
            animator.SetTrigger("sayExamine");
        }
        else if (text.Contains("시작"))
        {
            Debug.Log($"▶ OnVoiceCommand 호출됨: {text}");
            animator.SetTrigger("sayStart");
        }
        else if (text.Contains("약"))
        {
            Debug.Log($"▶ OnVoiceCommand 호출됨: {text}");
            animator.SetTrigger("sayMedicine");
        }
        else if (text.Contains("먹다") || text.Contains("드세요"))
        {
            Debug.Log($"▶ OnVoiceCommand 호출됨: {text}");
            animator.SetTrigger("sayEat");
        }
        else if (text.Contains("마치다") || text.Contains("마무리"))
        {
            Debug.Log($"▶ OnVoiceCommand 호출됨: {text}");
            animator.SetTrigger("sayFinish");
        }
        else if (text.Contains("목"))
        {
            Debug.Log($"▶ OnVoiceCommand 호출됨: {text}");
            animator.SetTrigger("sayNeck");
        }
        else if (text.Contains("부어"))
        {
            Debug.Log($"▶ OnVoiceCommand 호출됨: {text}");
            animator.SetTrigger("sayBoom");
        }
        else if (text.Contains("따뜻한") || text.Contains("따뜻하다")
    || text.Contains("뜨겁다") || text.Contains("뜨거운"))
        {
            Debug.Log($"▶ OnVoiceCommand 호출됨: {text}");
            animator.SetTrigger("sayHot");
        }
    }


    IEnumerator ResetParam(string paramName, float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetFloat(paramName, 0f);
    }
}