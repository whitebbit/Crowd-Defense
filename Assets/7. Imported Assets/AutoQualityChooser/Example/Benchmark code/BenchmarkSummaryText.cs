using UnityEngine;
using UnityEngine.UI;
using net.krej.AutoQualityChooser;
using net.krej.FPSCounter;

public class BenchmarkSummaryText : MonoBehaviour{

    public ParticleSystem particles;
    private Text text;

	void Start (){
	    text = GetComponent<Text>();
        // Here I could do:
	    // AutoQualityChooser.Instance.onQualityChange.AddListener(SaveCurrentLine);
        // But instead I'm attaching it in inspector.
	}

    private string currentLine;
    private string oldLines;
    private int maxParticles;

	void Update (){
	    maxParticles = Mathf.Max(maxParticles, particles.particleCount);
	    currentLine = string.Format("Quality: {0} - {1} particles, {2}FPS \n" , QualityChanger.GetCurrentQualityName(), maxParticles, FramerateCounter.Instance.currentFrameRate.ToString("0.0"));
	    text.text = oldLines + currentLine;
	}

    public void GoToNextLine(){
        oldLines += currentLine;
    }
}
