using System.Collections.Generic;

[System.Serializable]
public class JointFrame
{
    public int frame_index;
    public Dictionary<string, List<float>> joints;
}

[System.Serializable]
public class MotionData
{
    public List<JointFrame> frames;
}
