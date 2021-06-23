using UnityEngine;
using System.Runtime.InteropServices;

public class BrowserJS : MonoBehaviour
{
    [DllImport("__Internal")]
    public static extern void Alert(string str);

    [DllImport("__Internal")]
    public static extern void Log(string str);

    [DllImport("__Internal")]
    public static extern void Warn(string str);

    [DllImport("__Internal")]
    public static extern void Error(string str);
}