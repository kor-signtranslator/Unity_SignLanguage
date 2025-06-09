using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JsonSkeletonVisualizer : MonoBehaviour
{
    public TextAsset jsonFile;
    private Dictionary<string, GameObject> jointSpheres = new Dictionary<string, GameObject>();
    private List<MotionFrame> frames;
    private int currentFrame = 0;
    private float timer = 0f;
    public float frameRate = 30f;

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

    void Start()
    {
        var data = JsonConvert.DeserializeObject<MotionData>(jsonFile.text);
        frames = data.motions[0].frames;

        // 관절마다 Sphere 생성
        foreach (var joint in frames[0].joints.Keys)
        {
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.localScale = Vector3.one * 0.05f;
            jointSpheres[joint] = sphere;
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
            if (!jointSpheres.ContainsKey(pair.Key)) continue;

            Vector3 pos = new Vector3(pair.Value[0], pair.Value[1], -pair.Value[2]);
            pos.x = -pos.x;
            jointSpheres[pair.Key].transform.localPosition = pos;
        }
    }
}
