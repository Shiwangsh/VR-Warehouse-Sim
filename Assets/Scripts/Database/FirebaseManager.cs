using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Extensions;

public class FirebaseManager : MonoBehaviour
{
    private DependencyStatus dependencyStatus;

    void Start()
    {
        // Check Firebase dependencies
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => 
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                Debug.Log("✅ Firebase is ready to use!");
            }
            else
            {
                Debug.LogError($"❌ Firebase setup failed: {dependencyStatus}");
            }
        });
    }
}
