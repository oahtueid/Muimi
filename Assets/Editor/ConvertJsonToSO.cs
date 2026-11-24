#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class ChartConverterEditor
{
    [MenuItem("RhythmGame/Convert JSON to ScriptableObject")]
    public static void ConvertJsonToSO()
{
    TextAsset jsonFile = Resources.Load<TextAsset>("sample_chart");
    if (jsonFile == null) return;

    // 1. Tạo sẵn ScriptableObject instance
    SongMapData chartSO = ScriptableObject.CreateInstance<SongMapData>();

    // 2. Dùng FromJsonOverwrite để nhét trực tiếp dữ liệu JSON vào SO
    JsonUtility.FromJsonOverwrite(jsonFile.text, chartSO);

    // 3. Lưu xuống thư mục Assets
    string path = "Assets/Resources/SongCharts/SampleChart_SO.asset";
    if (!Directory.Exists("Assets/Resources/SongCharts"))
        Directory.CreateDirectory("Assets/Resources/SongCharts");

    AssetDatabase.CreateAsset(chartSO, path);
    AssetDatabase.SaveAssets();
    AssetDatabase.Refresh();

    Debug.Log("Đã gộp và tạo thành công ScriptableObject!");
}
}
#endif