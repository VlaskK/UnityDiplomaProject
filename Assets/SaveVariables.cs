using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class SaveVariables : MonoBehaviour
{

}

[Serializable]
class SaveData
{
  public float x;
  public float y;
  public float r;
}
