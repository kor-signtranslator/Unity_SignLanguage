using System.Runtime.InteropServices;
using UnityEngine;

[RequireComponent(typeof(VoiceControlledAnimator))]
public class Voice : MonoBehaviour
{
    VoiceControlledAnimator vca;

    // WebGL 전용 외부 함수 선언
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void StartSpeechRecognition();
#endif

    void Awake()
    {
        vca = GetComponent<VoiceControlledAnimator>();
    }

    // ★ Start() 대신 이 메서드를 버튼 클릭 시 호출 ★
    public void StartRecognition()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        StartSpeechRecognition();
        Debug.Log("▶ WebGL 음성인식 시작 (버튼 클릭)");
#else
        Debug.Log("▶ Editor/Standalone 모드 (음성인식 미실행)");
#endif
    }

    // jslib에서 SendMessage로 호출됩니다.
    public void OnRecognized(string text)
    {
        Debug.Log("Unity OnRecognized: " + text);
        vca.OnVoiceCommand(text);
    }
}

