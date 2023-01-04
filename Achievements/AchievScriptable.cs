using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Achievement", menuName = "Achievement") ]
public class AchievScriptable : ScriptableObject
{
    public string achievementName;
    public string achievementDesc;
    public int achievementProgress;
    public Sprite achievementImage;


}
