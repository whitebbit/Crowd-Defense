using UnityEngine;
using UnityEditor;
using net.krej.FPSCounter;

namespace net.krej.AutoQualityChooser{
    [CustomEditor(typeof (FramerateCounter))] public class FramerateCounterEditor : Editor{
        private FramerateCounter mTarget;

        public void OnEnable(){
            mTarget = target as FramerateCounter;
        }

        public override void OnInspectorGUI(){
            if (EditorApplication.isPlaying)
                DrawPlayModeInspector();
            else DrawOutsideOfPlaymodeInformation();
        }

        private void DrawPlayModeInspector(){
            GUILayout.Label(string.Format("{0} FPS", mTarget.currentFrameRate.ToString("0.0"))); 
        }

        private void DrawOutsideOfPlaymodeInformation() {
            GUILayout.Label("FPS info will be shown in play mode.");
        }

    }
}