using System.Linq;
using UnityEngine;
using UnityEditor;

namespace net.krej.AutoQualityChooser {
    [CustomEditor(typeof(AutoQualityChooser))]
    public class AutoQualityChooserEditor : Editor {
        private AutoQualityChooser mTarget;
        private AutoQualityChooserSettings settings;

        void OnEnable() {
            mTarget = target as AutoQualityChooser;
            settings = mTarget.settings;
        }

        public override void OnInspectorGUI() {
            ShowCurrentQuality();
            GUILayout.Label(string.Format("If framerate will be lower than {0}FPS for {1} seconds,\nquality will decrease.", settings.minAcceptableFramerate, settings.timeBeforeQualityDowngrade), EditorStyles.objectFieldThumb);
            DrawExpandableSettingsInspector();
            if(Application.isPlaying)DrawPlayModeInspector();
        }

        private void ShowCurrentQuality() {
            GUILayout.Label(QualityChanger.GetCurrentQualityName());
        }

        private void DrawPlayModeInspector(){
            GUILayout.Label(string.Format("Time with low FPS: {0}/{1}s", settings.timeBeforeQualityDowngrade - mTarget.secondsBeforeDecreasingQuality, settings.timeBeforeQualityDowngrade));
        }

        private static bool settingsOpen = true;
        private void DrawExpandableSettingsInspector() {
            settingsOpen = EditorGUILayout.Foldout(settingsOpen, "Settings");
            if (settingsOpen) DrawSettingsInspector();
        }

        private void DrawSettingsInspector() {
            DrawMinimalFramerateSlider();
            DrawTimeBeforeQualityDowngradeSlider();
            GUILayout.Space(15);
            GUILayout.Label("Default quality on start:");
            DrawStartQualityDropdown();
            DrawEditorQualityDropdown();
            GUILayout.Space(15);
            DrawLinkToUnityQuality();
            GUILayout.Space(15);
            DrawDisableAfterManualQualityChangeToggle();
            GUILayout.Space(15);
            DrawOnQualityChangeEventField();
        }

        private void DrawMinimalFramerateSlider() {
            var newFps = EditorGUILayout.IntSlider("Min. FPS", Mathf.RoundToInt(settings.minAcceptableFramerate), 2, 60);
            if (newFps != Mathf.RoundToInt(settings.minAcceptableFramerate)) {
                Undo.RecordObject(mTarget, "Changed minimal acceptable framerate");
                settings.minAcceptableFramerate = newFps;
            }
        }

        private void DrawTimeBeforeQualityDowngradeSlider() {
            var newTime = EditorGUILayout.IntSlider("Max low FPS seconds", settings.timeBeforeQualityDowngrade, 2, 30);
            if (newTime != settings.timeBeforeQualityDowngrade) {
                Undo.RecordObject(mTarget, "Changed maximal acceptable time with FPS lower than min.");
                settings.timeBeforeQualityDowngrade = newTime;
            }
        }

        private void DrawDisableAfterManualQualityChangeToggle() {
            var newToggleValue = GUILayout.Toggle(settings.disableAfterManualQualityChange, "Disable after manual quality change");
            if (newToggleValue != settings.disableAfterManualQualityChange) {
                Undo.RecordObject(mTarget, "Changed diasble after manual quality change toggle");
                settings.disableAfterManualQualityChange = newToggleValue;
            }
        }

        private void DrawStartQualityDropdown() {
            var newDropdownValue = DrawQualityDropdown(settings.startQuality, "  for build");
            if (settings.startQuality != newDropdownValue) {
                Undo.RecordObject(mTarget, "Start quality changed");
                settings.startQuality = newDropdownValue;
            }
            if( newDropdownValue!=-1 ) GUILayout.Label("Warning: Setting start quality to less than highest one is not recommended.");
        }

        private void DrawEditorQualityDropdown() {
            var newDropdownValue = DrawQualityDropdown(settings.startQualityInUnityEditor, "  for Unity Editor");
            if (settings.startQualityInUnityEditor != newDropdownValue) {
                Undo.RecordObject(mTarget, "Start quality for Unity Editor changed");
                settings.startQualityInUnityEditor = newDropdownValue;
            }
        }

        private void DrawLinkToUnityQuality() {
            if(GUILayout.Button("Quality Settings")) {
                EditorApplication.ExecuteMenuItem("Edit/Project Settings/Quality");
            }
        }

        private void DrawOnQualityChangeEventField() {
            serializedObject.Update();
            SerializedProperty prop = serializedObject.FindProperty("onQualityChange");
            EditorGUILayout.PropertyField(prop);
            serializedObject.ApplyModifiedProperties();
        }

        static int DrawQualityDropdown(int selected, string description) {
            var options = QualitySettings.names;
            if (selected == -1) selected = QualitySettings.names.Length - 1;
            selected = EditorGUILayout.Popup(description, selected, options);
            if (selected == QualitySettings.names.Length - 1) selected = -1;
            return selected;
        }
    }
}