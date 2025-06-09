using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class BoneInitialDirectionExtractor : MonoBehaviour
{
    public Transform rootBone;

    void Start()
    {
        if (rootBone == null)
        {
            Debug.LogError("Root Bone을 지정하세요.");
            return;
        }

        Dictionary<string, Vector3> directionMap = new Dictionary<string, Vector3>();
        ExtractDirections(rootBone, directionMap);

        string output = "{\n";
        foreach (var pair in directionMap)
        {
            Vector3 dir = pair.Value;
            output += $"  \"{pair.Key}\": new Vector3({dir.x}f, {dir.y}f, {dir.z}f),\n";
        }
        output += "}";

        Debug.Log(output);

        // Optional: 텍스트 파일로 저장
        File.WriteAllText(Application.dataPath + "/InitialDirectionMap.txt", output);
        Debug.Log("초기 방향 저장 완료: Assets/InitialDirectionMap.txt");
    }

    void ExtractDirections(Transform bone, Dictionary<string, Vector3> map)
    {
        if (bone.parent != null)
        {
            Vector3 worldDir = (bone.position - bone.parent.position).normalized;
            Vector3 localDir = bone.parent.InverseTransformDirection(worldDir);
            map[bone.name] = localDir;
        }

        foreach (Transform child in bone)
        {
            ExtractDirections(child, map);
        }
    }

}
