using UnityEngine;

public class HeadController : MonoBehaviour
{
    public Transform headJoint; // <- ¿©±â!
    public UdpReceiver receiver;

    void Update()
    {
        if (receiver.keypoints.Count >= 33 * 3) // Pose 33°³ ÁÂÇ¥
        {
            Vector3 headPos = new Vector3(
                receiver.keypoints[0 * 3],
                receiver.keypoints[0 * 3 + 1],
                receiver.keypoints[0 * 3 + 2]
            );

            headJoint.localPosition = headPos * 2f;
        }
    }
}
