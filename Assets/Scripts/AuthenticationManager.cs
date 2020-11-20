using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Threading.Tasks;
using Facebook.MiniJSON;
using Facebook.Unity;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Google;
using TMPro;
using UnityEngine;

public class AuthenticationManager : MonoBehaviour
{
    public static AuthenticationManager Instance { get; set; }

//    public TextMeshProUGUI infoText;
    public string webClientId = "484021923212-n2u2sj86rmhao83745tjplgk3pcpqsh5.apps.googleusercontent.com";
    public GameObject loginButtons;

    internal FirebaseAuth auth;
    private GoogleSignInConfiguration configuration;

    // Firebase User keyed by Firebase Auth.
    protected Dictionary<string, FirebaseUser> userByAuth = new Dictionary<string, FirebaseUser>();

    // Flag to check if fetch token is in flight.
    private bool fetchingToken = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        configuration = new GoogleSignInConfiguration
        {
            WebClientId = webClientId,
            RequestEmail = true,
            RequestIdToken = true
        };
/*        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(SetInit, onHidenUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }*/

        FB.Init(SetInit, onHidenUnity);
        CheckFirebaseDependencies();
    }

    void SetInit()
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("Facebook is Login!");
//            infoText.text = "Facebook is Login!";
        }
        else
        {
            Debug.Log("Facebook is not Logged in!");
//            infoText.text = "Facebook is not Logged in!";
        }

        DealWithFbMenus(FB.IsLoggedIn);
    }

    void onHidenUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void CheckFirebaseDependencies()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                if (task.Result == DependencyStatus.Available)
                {
                    auth = FirebaseAuth.DefaultInstance;
                    auth.StateChanged += AuthStateChanged;
                    auth.IdTokenChanged += IdTokenChanged;
                    AuthStateChanged(this, null);
                }
                else
                    AddToInformation("Could not resolve all Firebase dependencies: " + task.Result.ToString());
            }
            else
            {
                AddToInformation("Dependency check was not completed. Error : " + task.Exception.Message);
            }
        });
    }

    // Track state changes of the auth object.
    void AuthStateChanged(object sender, EventArgs eventArgs)
    {
        FirebaseAuth senderAuth = sender as FirebaseAuth;
        FirebaseUser user = null;

        if (senderAuth != null) userByAuth.TryGetValue(senderAuth.App.Name, out user);

        if (senderAuth == auth && senderAuth.CurrentUser != user)
        {
            bool signedIn = user != senderAuth.CurrentUser && senderAuth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }

            user = senderAuth.CurrentUser;
            userByAuth[senderAuth.App.Name] = user;
            if (signedIn)
            {
//                Debug.Log("Signed in " + user.DisplayName);
                foreach (var profile in user.ProviderData)
                {
                    // Id of the provider (ex: google.com)
                    string providerId = profile.ProviderId;
                    if (providerId.Equals(FacebookAuthProvider.ProviderId))
                    {
                        FBLogin();
                    }
                    else if (providerId.Equals(GoogleAuthProvider.ProviderId))
                    {
                        SignInWithGoogle();
                    }

//                    print(providerId);

                    // UID specific to the provider
//                    string uid = profile.UserId;
//                    print(uid);

                    // Name, email address, and profile photo Url
//                    string name = profile.DisplayName;
//                    print(name);
//                    string email = profile.Email;
//                    print(email);
//                    var photoUrl = profile.PhotoUrl;
//                    print(photoUrl);
                }

//                DisplayDetailedUserInfo(user, 1);
            }
            else
            {
                loginButtons.SetActive(true);
            }
        }
        else
        {
            loginButtons.SetActive(true);
        }
    }

    // Track ID token changes.
    void IdTokenChanged(object sender, System.EventArgs eventArgs)
    {
        FirebaseAuth senderAuth = sender as FirebaseAuth;
        if (senderAuth == auth && senderAuth.CurrentUser != null && !fetchingToken)
        {
            senderAuth.CurrentUser.TokenAsync(false).ContinueWithOnMainThread(
                task => Debug.Log(String.Format("Token[0:8] = {0}", task.Result.Substring(0, 8))));
        }
    }

    public void SignInWithGoogle()
    {
//        if (FB.IsLoggedIn) FB.LogOut();
        OnSignIn();
    }

    public void SignOutFromGoogle()
    {
        OnSignOut();
    }

    private void OnSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        AddToInformation("Calling SignIn");

        GoogleSignIn.DefaultInstance.SignIn().ContinueWithOnMainThread(OnAuthenticationFinished);
    }

/*
    public void Configure(GoogleSignInConfiguration configuration)
    {
        if (configuration != null)
        {
            List<string> scopes = new List<string>();
            if (configuration.AdditionalScopes != null)
            {
                scopes.AddRange(configuration.AdditionalScopes);
            }

            GoogleSignIn_Configure(SelfPtr(), configuration.UseGameSignIn,
                configuration.WebClientId,
                configuration.RequestAuthCode,
                configuration.ForceTokenRefresh,
                configuration.RequestEmail,
                configuration.RequestIdToken,
                configuration.HidePopups,
                scopes.ToArray(),
                scopes.Count,
                configuration.AccountName);
        }
    }
*/

    private void OnSignOut()
    {
        AddToInformation("Calling SignOut");
        GoogleSignIn.DefaultInstance.SignOut();
    }

    public void OnDisconnect()
    {
        AddToInformation("Calling Disconnect");
        GoogleSignIn.DefaultInstance.Disconnect();
    }

    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            using (IEnumerator<Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error = (GoogleSignIn.SignInException) enumerator.Current;
                    AddToInformation("Got Error: " + error.Status + " " + error.Message);
                }
                else
                {
                    AddToInformation("Got Unexpected Exception?!?" + task.Exception);
                }
            }
        }
        else if (task.IsCanceled)
        {
            AddToInformation("Canceled");
        }
        else
        {
            AddToInformation("Welcome: " + task.Result.DisplayName + "!");
            AddToInformation("Email = " + task.Result.Email);
            AddToInformation("Google ID Token = " + task.Result.IdToken);
            AddToInformation("Email = " + task.Result.Email);
            SignInWithGoogleOnFirebase(task.Result.IdToken);
        }
    }

    private void SignInWithGoogleOnFirebase(string idToken)
    {
        Credential credential = GoogleAuthProvider.GetCredential(idToken, null);

/*
        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            AggregateException ex = task.Exception;
            if (ex != null)
            {
                if (ex.InnerExceptions[0] is FirebaseException inner && (inner.ErrorCode != 0))
                    AddToInformation("\nError code = " + inner.ErrorCode + " Message = " + inner.Message);
            }
            else
            {
                AddToInformation("Sign In Successful.");
            }
        });
*/

        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                AddToInformation("SignInWithCredentialAsync was canceled.");
                return;
            }

            if (task.IsFaulted)
            {
                AddToInformation("SignInWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }

            var newUser = task.Result;

//            infoText.text = "User signed in successfully: " + newUser.DisplayName + " (" + newUser.UserId + ")";
            loginButtons.SetActive(false);
        });
    }

    public void OnSignInSilently()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        AddToInformation("Calling SignIn Silently");

        GoogleSignIn.DefaultInstance.SignInSilently().ContinueWith(OnAuthenticationFinished);
    }

    public void OnGamesSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = true;
        GoogleSignIn.Configuration.RequestIdToken = false;

        AddToInformation("Calling Games SignIn");

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
    }

    public void FBLogin()
    {
//        if (!GoogleSignIn.Configuration.AccountName.Equals(null))
//            SignOutFromGoogle();
//        var permissions = new List<string>() {"email", "user_birthday", "user_friends", "public_profile"};
        var permissions = new List<string>() {"public_profile", "email"};
        FB.LogInWithReadPermissions(permissions, LoginCallback);
//        infoText.text = "FB Login";
    }

    void LoginCallback(IResult result)
    {
//        infoText.text = "LoginCallback";

        if (result.Error != null)
        {
//            infoText.text = "Error Response:\n" + result.Error;
        }
        else if (!FB.IsLoggedIn)
        {
//            infoText.text = "Login cancelled by Player";
        }
        else
        {
//            infoText.text = "Login was successful!";
            FB.API("/me?fields=id,name,email", HttpMethod.GET, LoginCallback2);
        }
    }

    void LoginCallback2(IResult result)
    {
        if (result.Error != null)
        {
//            infoText.text = "Error Response:\n" + result.Error;
        }
        else if (!FB.IsLoggedIn)
        {
//            infoText.text = "Login cancelled by Player";
        }
        else
        {
            IDictionary dict = Json.Deserialize(result.RawResult) as IDictionary;
//            infoText.text = dict["first_name"].ToString();
            var accessToken = AccessToken.CurrentAccessToken;
            SignInWithFacebookOnFirebase(accessToken);
            FirebaseUser newUser = auth.CurrentUser;
            if (newUser != null)
            {
//                infoText.text = "User signed in successfully: " + newUser.DisplayName + ", " + newUser.UserId;
                loginButtons.SetActive(false);
            }
            else
            {
//                infoText.text = "Failed to fetch data";
            }

//            print("your name is: " + fbname);
        }
    }

    void SignInWithFacebookOnFirebase(AccessToken accessToken)
    {
        Credential credential = FacebookAuthProvider.GetCredential(accessToken.TokenString);

        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
//                infoText.text = "SignInWithCredentialAsync was canceled.";
                return;
            }

            if (task.IsFaulted)
            {
//                infoText.text += "SignInWithCredentialAsync encountered an error: " + task.Exception;
                return;
            }

            FirebaseUser newUser = task.Result;
//            infoText.text = "User signed in successfully: " + newUser.DisplayName + ", " + newUser.UserId;
        });
    }

    private void AddToInformation(string str)
    {
//        infoText.text = str;
    }

    void DealWithFbMenus(bool isLoggedIn)
    {
        if (isLoggedIn)
        {
            FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
            FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
        }
        else
        {
        }
    }

    void DisplayUsername(IResult result)
    {
        if (result.Error == null)
        {
            string name = "" + result.ResultDictionary["first_name"];
            //  FB_userName.text = name;
//            infoText.text = name;

            Debug.Log("" + name);
        }
        else
        {
            Debug.Log(result.Error);
//            infoText.text = result.Error;
        }
    }

    void DisplayProfilePic(IGraphResult result)
    {
        if (result.Texture != null)
        {
            Debug.Log("Profile Pic");
            // FB_useerDp.sprite = Sprite.Create(result.Texture,new Rect(0,0,128,128),new Vector2());
        }
        else
        {
            Debug.Log(result.Error);
        }
    }
}