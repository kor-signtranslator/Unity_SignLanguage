using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class FeelDataPlayer : MonoBehaviour
{
    [System.Serializable]
    public class MotionFrame
    {
        public int frame_index;
        public Dictionary<string, List<float>> joints;
    }

    [System.Serializable]
    public class MotionSequence
    {
        public string label;
        public List<MotionFrame> frames;
    }

    [System.Serializable]
    public class MotionData
    {
        public List<MotionSequence> motions;
    }

    public TextAsset jsonFile;
    public Transform root;
    public float frameRate = 30f;

    private Dictionary<string, Transform> boneMap = new Dictionary<string, Transform>();
    private Dictionary<string, Quaternion> initialRotationMap = new Dictionary<string, Quaternion>();
    private Dictionary<string, Vector3> smoothedDirections = new Dictionary<string, Vector3>();
    private List<MotionFrame> frames;
    private int currentFrame = 0;
    private float timer = 0f;

    // ✅ 네가 추출한 초기 방향 벡터 그대로
    private Dictionary<string, Vector3> initialDirectionMap = new Dictionary<string, Vector3>
    {
        { "mixamorig2:RightShoulder", new Vector3(-0.2751737f, -0.9608111f, -0.03348824f) },
        { "mixamorig2:RightArm", new Vector3(-2.771616e-06f, -0.9998977f, -0.01430523f) },
        { "mixamorig2:RightForeArm", new Vector3(-2.17557e-06f, -0.9997227f, -0.02354854f) },
        { "mixamorig2:RightHand", new Vector3(-0.00276193f, -0.9999763f, 0.006299615f) },
        { "mixamorig2:LeftShoulder", new Vector3(-0.2752085f, -0.9608097f, -0.03323871f) },
        { "mixamorig2:LeftArm", new Vector3(3.427267e-06f, -0.9999037f, -0.01389718f) },
        { "mixamorig2:LeftForeArm", new Vector3(3.188848e-06f, -0.9997333f, -0.02310151f) },
        { "mixamorig2:LeftHand", new Vector3(-0.01890096f, -0.9997622f, -0.01089987f) },
        { "mixamorig2:RightHandThumb1", new Vector3(0.3821649f, -0.8943816f, 0.232447f) },
        { "mixamorig2:RightHandThumb2", new Vector3(0.3843884f, -0.8944129f, 0.2286283f) },
        { "mixamorig2:RightHandThumb3", new Vector3(0.383341f, -0.8944007f, 0.2304282f) },
        { "mixamorig2:RightHandThumb4", new Vector3(0.383341f, -0.8944007f, 0.2304282f) },
        { "mixamorig2:RightHandIndex1", new Vector3(-3.427267e-06f, -0.9999931f, 0.00379014f) },
        { "mixamorig2:RightHandIndex2", new Vector3(-3.367662e-06f, -0.9999934f, 0.003773272f) },
        { "mixamorig2:RightHandIndex3", new Vector3(-2.861023e-06f, -0.999993f, 0.003796518f) },
        { "mixamorig2:RightHandIndex4", new Vector3(-2.861023e-06f, -0.999993f, 0.003796518f) },
        { "mixamorig2:RightHandMiddle1", new Vector3(-3.188848e-06f, -0.9999523f, 0.009791106f) },
        { "mixamorig2:RightHandMiddle2", new Vector3(-3.099442e-06f, -0.999953f, 0.009719878f) },
        { "mixamorig2:RightHandMiddle3", new Vector3(-3.248453e-06f, -0.9999526f, 0.009771079f) },
        { "mixamorig2:RightHandMiddle4", new Vector3(-3.248453e-06f, -0.9999526f, 0.009771079f) },
        { "mixamorig2:RightHandRing1", new Vector3(-3.993511e-06f, -0.9999998f, 0.0009041429f) },
        { "mixamorig2:RightHandRing2", new Vector3(-3.427267e-06f, -0.9999998f, 0.0009136498f) },
        { "mixamorig2:RightHandRing3", new Vector3(-3.188848e-06f, -0.9999998f, 0.0008630753f) },
        { "mixamorig2:RightHandRing4", new Vector3(-3.188848e-06f, -0.9999998f, 0.0008630753f) },
        { "mixamorig2:RightHandPinky1", new Vector3(-3.546476e-06f, -0.99998f, 0.006317645f) },
        { "mixamorig2:RightHandPinky2", new Vector3(-2.712011e-06f, -0.9999806f, 0.0062823f) },
        { "mixamorig2:RightHandPinky3", new Vector3(-3.069639e-06f, -0.9999804f, 0.006271094f) },
        { "mixamorig2:RightHandPinky4", new Vector3(-3.069639e-06f, -0.9999804f, 0.006271094f) },
        { "mixamorig2:LeftHandThumb1", new Vector3(-0.3997303f, -0.8941464f, 0.2017887f) },
        { "mixamorig2:LeftHandThumb2", new Vector3(-0.4032682f, -0.8939606f, 0.1954743f) },
        { "mixamorig2:LeftHandThumb3", new Vector3(-0.4163855f, -0.8928412f, 0.1716337f) },
        { "mixamorig2:LeftHandThumb4", new Vector3(-0.4163855f, -0.8928412f, 0.1716337f) },
        { "mixamorig2:LeftHandIndex1", new Vector3(2.235174e-06f, -0.9999419f, -0.01083058f) },
        { "mixamorig2:LeftHandIndex2", new Vector3(2.294779e-06f, -0.9999427f, -0.01073086f) },
        { "mixamorig2:LeftHandIndex3", new Vector3(3.069639e-06f, -0.9999428f, -0.01072985f) },
        { "mixamorig2:LeftHandIndex4", new Vector3(3.069639e-06f, -0.9999428f, -0.01072985f) },
        { "mixamorig2:LeftHandMiddle1", new Vector3(2.086163e-06f, -0.9999419f, -0.01079351f) },
        { "mixamorig2:LeftHandMiddle2", new Vector3(1.996756e-06f, -0.9999429f, -0.01068771f) },
        { "mixamorig2:LeftHandMiddle3", new Vector3(2.294779e-06f, -0.9999435f, -0.01068199f) },
        { "mixamorig2:LeftHandMiddle4", new Vector3(2.294779e-06f, -0.9999435f, -0.01068199f) },
        { "mixamorig2:LeftHandRing1", new Vector3(2.145767e-06f, -0.9999295f, -0.01190776f) },
        { "mixamorig2:LeftHandRing2", new Vector3(2.17557e-06f, -0.9999304f, -0.01182026f) },
        { "mixamorig2:LeftHandRing3", new Vector3(1.966953e-06f, -0.9999307f, -0.0117864f) },
        { "mixamorig2:LeftHandRing4", new Vector3(1.966953e-06f, -0.9999307f, -0.0117864f) },
        { "mixamorig2:LeftHandPinky1", new Vector3(2.413988e-06f, -0.9998838f, -0.01526713f) },
        { "mixamorig2:LeftHandPinky2", new Vector3(2.801418e-06f, -0.999884f, -0.01525247f) },
        { "mixamorig2:LeftHandPinky3", new Vector3(2.980232e-06f, -0.9998842f, -0.01524645f) },
        { "mixamorig2:LeftHandPinky4", new Vector3(2.980232e-06f, -0.9998842f, -0.01524645f) },
    };

    void Start()
    {
        MotionData data = JsonConvert.DeserializeObject<MotionData>(jsonFile.text);
        frames = data.motions[0].frames;

        foreach (Transform t in root.GetComponentsInChildren<Transform>())
        {
            boneMap[t.name] = t;
            initialRotationMap[t.name] = t.localRotation;
        }
    }

    void Update()
    {
        if (frames == null || frames.Count == 0) return;

        timer += Time.deltaTime;
        if (timer >= 1f / frameRate)
        {
            timer = 0f;
            ApplyFrame(frames[currentFrame]);
            currentFrame = (currentFrame + 1) % frames.Count;
        }
    }

    void ApplyFrame(MotionFrame frame)
    {
        foreach (var pair in frame.joints)
        {
            if (!boneMap.ContainsKey(pair.Key)) continue;

            Transform bone = boneMap[pair.Key];
            Transform parent = bone.parent;
            if (parent == null || !boneMap.ContainsValue(parent)) continue;

            // 1. 좌표 변환
            Vector3 targetPos = new Vector3(pair.Value[0], pair.Value[1], pair.Value[2]);  // ★ Z 반전 제거
            targetPos.x = -targetPos.x; // 좌우 반전 (필요할 경우만)

            // 2. 방향 벡터 계산
            Vector3 worldDirection = (targetPos - parent.position).normalized;
            Vector3 localDirection = parent.InverseTransformDirection(worldDirection);
            Vector3 targetDirection = localDirection;

            // 3. 디버그 시각화
            Debug.DrawRay(bone.position, bone.TransformDirection(targetDirection) * 0.1f, Color.red);
            if (initialDirectionMap.ContainsKey(bone.name))
                Debug.DrawRay(bone.position, bone.TransformDirection(initialDirectionMap[bone.name]) * 0.1f, Color.green);

            // 4. 회전 계산 및 적용
            if (initialDirectionMap.ContainsKey(bone.name) && initialRotationMap.ContainsKey(bone.name))
            {
                Vector3 initialDir = initialDirectionMap[bone.name];
                Quaternion deltaRotation = Quaternion.FromToRotation(initialDir.normalized, targetDirection.normalized);
                Quaternion baseRotation = initialRotationMap[bone.name];
                bone.localRotation = baseRotation * deltaRotation;
            }
        }
    }


}
