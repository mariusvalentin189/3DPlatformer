using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName ="New Level")]
public class Level : ScriptableObject
{
    public string levelSceneName;
    public int levelId;
    public bool unlocked;
    public bool completedFirstTime;
}
