using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class CutsceneData
{
    public string cutsceneName;
    public PlayableDirector playableDirector;
}
public class CutsceneManager : SceneManager<CutsceneManager>
{
    [SerializeField]
    List<CutsceneData> cutsceneDatas;
    Dictionary<string, PlayableDirector> cutscenesDict = new();
    void Start()
    {
        foreach(var cutscene in cutsceneDatas)
        {
            cutscenesDict[cutscene.cutsceneName] = cutscene.playableDirector;
        }
    }
    public void PlayCutscene(string cutsceneName)
    {
        if(cutscenesDict.TryGetValue(cutsceneName, out PlayableDirector playableDirector))
        {
            playableDirector.Play();
        }
    }
}
