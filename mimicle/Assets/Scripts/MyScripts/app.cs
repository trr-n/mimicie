using UnityEngine;

public static class app
{
    public static void SetFps(int fps = -1)
    => Application.targetFrameRate = fps;
}