#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class OsuToSoConverter
{
    [MenuItem("RhythmGame/Convert .osu file to ScriptableObject")]
    public static void ConvertOsuToSO()
    {
        // Mở cửa sổ chọn file .osu trên máy tính
        string filePath = EditorUtility.OpenFilePanel("Chọn file .osu của bạn", "", "osu");
        if (string.IsNullOrEmpty(filePath)) return;

        string[] lines = File.ReadAllLines(filePath);
        
        // Tạo instance của ScriptableObject
        SongMapData chartSO = ScriptableObject.CreateInstance<SongMapData>();
        chartSO.notes = new List<NoteData>();

        bool readingHitObjects = false;

        foreach (string rawLine in lines)
        {
            string line = rawLine.Trim();

            // Đọc tên bài hát từ file osu
            if (line.StartsWith("Title:"))
            {
                chartSO.songName = line.Substring("Title:".Length).Trim();
            }

            // Nhảy tới khối chứa nốt nhạc
            if (line == "[HitObjects]")
            {
                readingHitObjects = true;
                continue;
            }

            // Đọc dữ liệu nốt
            if (readingHitObjects && !string.IsNullOrEmpty(line) && !line.StartsWith("["))
            {
                string[] parts = line.Split(',');
                if (parts.Length >= 3)
                {
                    float x = float.Parse(parts[0]);
                    float timeMs = float.Parse(parts[2]);

                    // Quy đổi tọa độ X của osu! (0 - 512) thành 4 làn (Lane 0 đến 3)
                    int lane = Mathf.FloorToInt((x * 4) / 512f);
                    lane = Mathf.Clamp(lane, 0, 3);

                    NoteData note = new NoteData
                    {
                        targetTime = timeMs / 1000f, // Đổi mili-giây sang giây
                        laneIndex = lane
                    };

                    chartSO.notes.Add(note);
                }
            }
        }

        // Sắp xếp danh sách nốt theo thời gian tăng dần
        chartSO.notes.Sort((a, b) => a.targetTime.CompareTo(b.targetTime));

        // Đường dẫn lưu file .asset trong dự án
        string folderPath = "Assets/Resources/SongCharts";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string safeSongName = string.IsNullOrEmpty(chartSO.songName) ? "OsuSong" : chartSO.songName;
        // Loại bỏ các ký tự đặc biệt có thể gây lỗi tên file
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            safeSongName = safeSongName.Replace(c, '_');
        }

        string assetPath = $"{folderPath}/{safeSongName}_SO.asset";

        // Lưu xuống Unity Asset Database
        AssetDatabase.CreateAsset(chartSO, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"Đã chuyển đổi thành công file .osu thành ScriptableObject tại: {assetPath}");
    }
}
#endif