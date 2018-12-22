using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;

public class Facebookscript : MonoBehaviour {

	public Text IdText;
	public Text Liketext;

	// Use this for initialization
	void Awake () 
	{
		if (!FB.IsInitialized) {
			FB.Init ();
		} else 
		{
			FB.ActivateApp ();
		}
	}

	public void Login()
	{
		FB.LogInWithReadPermissions (callback: OnLogin);
	}
	void OnLogin(ILoginResult loginresult)
	{
		if (FB.IsLoggedIn) 
		{
			Debug.Log ("Successfully Logged In");
			IdText.text = loginresult.AccessToken.UserId;
		} 
		else if (loginresult.Cancelled) 
		{
			Debug.Log ("Error While Logging in " + loginresult.Error);
		}
	}

	public void Like()
	{
		FB.API ("/me/likes", HttpMethod.GET, callback: OnPageLike);
	}
	void OnPageLike(IGraphResult LikeResult)
	{
		if (LikeResult.Cancelled || !string.IsNullOrEmpty (LikeResult.Error)) {
			Debug.Log ("Operation CAncelled with error " + LikeResult.Error);
		} else 
		{
			Liketext.text = "Success";
		}
	}
}
