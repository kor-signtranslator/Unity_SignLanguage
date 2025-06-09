using UnityEngine;
using UnityEngine.UI;    // UI 네임스페이스
using System;
using TMPro;

[Serializable]
public class CommandPlayer : MonoBehaviour
{
    [Header("Inspector에서 연결")]
    public TMP_InputField commandInput;
    public Button playButton;        // UI ▶ PlayButton 할당
    public Animator animator;        // 애니메이터 할당

    void Start()
    {
        Debug.Log($"[CmdPlayer] Animator: {animator}");
        Debug.Log($"[CmdPlayer] Controller: {animator.runtimeAnimatorController.name}");
        Debug.Log($"[CmdPlayer] LayerCount: {animator.layerCount}");
        for (int i = 0; i < animator.layerCount; i++)
            Debug.Log($"[CmdPlayer] Layer {i}: {animator.GetLayerName(i)}");

        // (기존) 버튼 이벤트 연결
        playButton.onClick.AddListener(OnPlayButtonClicked);
    }


    void OnPlayButtonClicked()
    {
        string cmd = commandInput.text.Trim();
        if (string.IsNullOrEmpty(cmd)) return;

        int layer = 0;
        int hash = Animator.StringToHash(cmd);

        if (animator.HasState(layer, hash))
        {
            // ▶ State 이름(cmd)으로 바로 Play
            animator.Play(cmd, layer, 0f);
            Debug.Log($"State '{cmd}' 재생");
        }
        else
        {
            Debug.LogWarning($"State '{cmd}'을(를) 찾을 수 없습니다.");
        }
    }

}
