using UnityEngine;
using System.Collections;

public class Share : MonoBehaviour
{
		void Start ()
		{
		}

		void Update ()
		{
			
		}

		public void ButtonShare ()
		{
			StartCoroutine( ShareScreenshot() );

		}

	public void ButtonPlay(){
		Debug.Log("Reload");
		Application.LoadLevel(Application.loadedLevel);
	}
	public IEnumerator ShareScreenshot()
	{
		Debug.Log("Share");

		// wait for graphics to render
		yield return new WaitForEndOfFrame();
		//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
		// create the texture
		Texture2D screenTexture = new Texture2D(Screen.width, Screen.height,TextureFormat.RGB24,true);
		// put buffer into texture
		screenTexture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height),0,0);
		// apply
		screenTexture.Apply();
		//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
		byte[] dataToSave = screenTexture.EncodeToPNG();
		string destination = System.IO.Path.Combine(Application.temporaryCachePath,"Screenshot.png");
		System.IO.File.WriteAllBytes(destination, dataToSave);
		if(!Application.isEditor)
		{
			// block to open the file and share it
			AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
			intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
			//Put image to intent
			AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
			AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse","file://" + destination);
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

			intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");

			//Get activity
			AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
			//Create chooser to share
			AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share your score");
			currentActivity.Call("startActivity", jChooser);
			

		}
	}

}
