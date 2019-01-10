using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;
using System;

public class ReadQR : MonoBehaviour {

	public RawImage rawimage;
	private WebCamTexture camTexture;
	//private Rect screenRect;

	public AudioClip photo;
	AudioSource source;

	public Button captureBtn;

	bool check;

	void Start() {
		Data.Instance.ui.SetStatus (false);
		Data.Instance.ui.ShowBack(false);
		Data.Instance.ui.ShowCapture (true);
		source = GetComponent<AudioSource> ();

		//screenRect = new Rect(0, 0, Screen.width, Screen.height);
		//rawimage.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
		camTexture = new WebCamTexture();

		camTexture.requestedHeight = Screen.height;
		camTexture.requestedWidth = Screen.width;
		if (camTexture != null) {
			camTexture.Play();
		}

		Events.OnRunaFound += OnRunaFound;
	}

	void OnDestroy(){
		Events.OnRunaFound -= OnRunaFound;
		camTexture.Stop ();
	}

	void OnRunaFound(){
		check = false;
		captureBtn.image.color = captureBtn.colors.normalColor;
		captureBtn.gameObject.SetActive (false);
		source.PlayOneShot (photo);
	}

	void OnGUI () {
		// drawing the camera on screen
		//GUI.DrawTexture (screenRect, camTexture, ScaleMode.ScaleToFit);
		//rawimage.texture = camTexture;
		// do the reading — you might want to attempt to read less often than you draw on the screen for performance sake
	/*	try {
			IBarcodeReader barcodeReader = new BarcodeReader ();
			// decode the current frame
			var result = barcodeReader.Decode(camTexture.GetPixels32(), camTexture.width , camTexture.height);
			if (result != null) {
				//Debug.Log("DECODED TEXT FROM QR: " + result.Text);
				Data.Instance.ui.SetStatus(false);
				Data.Instance.figurasData.EnableRuna(result.Text);
			}
		} catch(Exception ex) { Debug.LogWarning (ex.Message); }*/

		/*Texture2D myQR = generateQR("Piramide");
		if (GUI.Button (new Rect (300, 300, 256, 256), myQR, GUIStyle.none)) {
			exportQR ("Piramide", myQR);
		}
		Texture2D myQR2 = generateQR("Cubo");
		if (GUI.Button (new Rect (600, 300, 256, 256), myQR2, GUIStyle.none)) {
			exportQR ("Cubo", myQR2);
		}*/
	}

	public void FindQR(){		
		check = true;
		captureBtn.image.color = captureBtn.colors.pressedColor;
		Debug.Log ("FindQR");
		Invoke ("StopFind", 5);
	}

	void StopFind(){
		Debug.Log ("Stop FindQR");
		check = false;
		captureBtn.image.color = captureBtn.colors.normalColor;
	}

	void Update(){
		rawimage.texture = camTexture;
		if (check) {
			try {
				IBarcodeReader barcodeReader = new BarcodeReader ();
				// decode the current frame
				var result = barcodeReader.Decode (camTexture.GetPixels32 (), camTexture.width, camTexture.height);
				if (result != null) {
					//Debug.Log("DECODED TEXT FROM QR: " + result.Text);
					Data.Instance.ui.SetStatus (false);
					Data.Instance.figurasData.EnableRuna (result.Text);
				}
			} catch (Exception ex) {
				Debug.LogWarning (ex.Message);
			}
		}
	}

	private static Color32[] Encode(string textForEncoding, int width, int height) {
		var writer = new BarcodeWriter {
			Format = BarcodeFormat.QR_CODE,
			Options = new QrCodeEncodingOptions {
				Height = height,
				Width = width
			}
		};
		return writer.Write(textForEncoding);
	}

	public Texture2D generateQR(string text) {
		var encoded = new Texture2D (256, 256);
		var color32 = Encode(text, encoded.width, encoded.height);
		encoded.SetPixels32(color32);
		encoded.Apply();
		return encoded;
	}

	void exportQR(string filename, Texture2D tex){
		Debug.Log ("aca");
		byte[] bytes = tex.EncodeToPNG();
		System.IO.File.WriteAllBytes (Application.dataPath + "\\QR\\" + filename+".png", bytes);
	}
}
