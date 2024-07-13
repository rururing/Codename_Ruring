using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleSheetsToUnity;
using GoogleSheetsToUnity.ThirdPary;

public struct MusicData
{
    public float time;
    public string route;
}

public enum Mode
{
    Easy,
    Normal,
    Hard,
}
public class PatternReader : MonoBehaviour
{
    [Header("스프레드 시트 주소")] private string sheetID = "1lPHdtH27k1ShD0M3IEVtqN_YfnZEMzsLAs80--AZzYk";
    [Header("스프레드 시트 시작 행")] private string startCell = "A2";

    public Dictionary<Mode, List<MusicData>> musicPattern = new Dictionary<Mode, List<MusicData>>();
    
    void Awake()
    {
        foreach (Mode mode in Enum.GetValues(typeof(Mode)))
        {
            musicPattern.Add(mode, new List<MusicData>());
        }
        
        // 데이터 요청을 위한 설정 - Read 딜레이가 긺(아마 비동기 처리). 무조건 로비에서 로드해야 접근 시 오류 생기지 않을 듯.
        SpreadsheetManager.Read(new GSTU_Search(sheetID, Mode.Easy.ToString()), Read_Easy);
        SpreadsheetManager.Read(new GSTU_Search(sheetID, Mode.Normal.ToString()), Read_Normal);
        SpreadsheetManager.Read(new GSTU_Search(sheetID, Mode.Hard.ToString()), Read_Hard);
    }
    
    void Read_Easy(GstuSpreadSheet spreadSheet)
    {
        foreach (var row in spreadSheet.rows.primaryDictionary)
        {
            MusicData data = new MusicData();
            if (!float.TryParse(row.Value[0].value, out data.time)) continue;
            data.route = row.Value[1].value;
            musicPattern[Mode.Easy].Add(data);
            //Debug.Log($"{data.time}, {data.route}");
        }
        
        Debug.Log($"count : {musicPattern[Mode.Easy].Count}");

    }

    // 데이터를 읽어온 후의 콜백 함수
    void Read_Normal(GstuSpreadSheet spreadSheet)
    {
        foreach (var row in spreadSheet.rows.primaryDictionary)
        {
            MusicData data = new MusicData();
            if (!float.TryParse(row.Value[0].value, out data.time)) continue;
            data.route = row.Value[1].value;
            musicPattern[Mode.Normal].Add(data);
        }
        
        Debug.Log($"count : {musicPattern[Mode.Normal].Count}");
    }
    
    void Read_Hard(GstuSpreadSheet spreadSheet)
    {
        foreach (var row in spreadSheet.rows.primaryDictionary)
        {
            MusicData data = new MusicData();
            if (!float.TryParse(row.Value[0].value, out data.time)) continue;
            data.route = row.Value[1].value;
            musicPattern[Mode.Hard].Add(data);
            //Debug.Log($"{data.time}, {data.route}");
        }
        Debug.Log($"count : {musicPattern[Mode.Hard].Count}");

    }

}