using UnityEngine;

public class KeypointApplier : MonoBehaviour
{
    public Transform headJoint;              // Unity에서 연결할 본
    public UdpReceiver receiver;             // UDP 데이터 수신기

    void Update()
    {
        if (receiver.keypoints.Count >= 33 * 3) // MediaPipe pose: 33 joints * 3 (x, y, z)
        {
            Vector3 headPos = new Vector3(
                receiver.keypoints[0 * 3],
                receiver.keypoints[0 * 3 + 1],
                receiver.keypoints[0 * 3 + 2]
            );

            headJoint.localPosition = headPos * 2f; // 스케일은 네 모델에 맞춰 조정
        }
    }
}
