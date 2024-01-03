using UnityEngine;
using System.Collections;
using net.krej.AutoQualityChooser;
using net.krej.FPSCounter;

public class FramerateCounterDisplay : MonoBehaviour {
    private void OnGUI() {
        ShowFpsInCorner();
    }

    const int FPS_BOX_WIDTH = 128;
    const int FPS_BOX_HEIGHT = 32;
    private void ShowFpsInCorner() {
        var fpsTextStyle = new GUIStyle(GUI.skin.box) { fontSize = 10, alignment = TextAnchor.MiddleCenter, richText = true };
        var txt = string.Format("<color=white><size=12><B>Auto Quality Chooser</B></size>\n{1}FPS ({0})</color>", QualityChanger.GetCurrentQualityName(), FramerateCounter.Instance.currentFrameRate.ToString("0"));
        GUI.Box(new Rect(0, 0, FPS_BOX_WIDTH, FPS_BOX_HEIGHT), txt, fpsTextStyle);
    }
}
