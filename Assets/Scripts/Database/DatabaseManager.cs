using UnityEngine;
using Firebase.Database;
using System.Threading.Tasks;

public class DatabaseManager : MonoBehaviour
{
    private DatabaseReference dbRef;

    void Start()
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public async Task GetAllData<T>(string tableName, System.Action<T> onDataReceived)
    {
        try
        {
            DataSnapshot snapshot = await dbRef.Child(tableName).GetValueAsync();
            if (snapshot.Exists)
            {
                foreach (var child in snapshot.Children)
                {
                    string json = child.GetRawJsonValue();
                    T data = JsonUtility.FromJson<T>(json);
                    onDataReceived(data);
                }
            }
            else
            {
                Debug.Log("Data not found.");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to get data: " + ex.Message);
        }
    }

    public async Task AddData<T>(string tableName, string id, T data)
    {
        try
        {
            string json = JsonUtility.ToJson(data);
            await dbRef.Child(tableName).Child(id).SetRawJsonValueAsync(json);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to add data: " + ex.Message);
        }
    }

    public async Task<T> GetDataById<T>(string tableName, string id)
    {
        try
        {
            DataSnapshot snapshot = await dbRef.Child(tableName).Child(id).GetValueAsync();
            if (snapshot.Exists)
            {
                string json = snapshot.GetRawJsonValue();
                return JsonUtility.FromJson<T>(json);
            }
            else
            {
                Debug.Log("Data not found.");
                return default;
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to get data: " + ex.Message);
            return default;
        }
    }

    public async Task UpdateData<T>(string tableName, string id, T data)
    {
        try
        {
            string json = JsonUtility.ToJson(data);
            await dbRef.Child(tableName).Child(id).SetRawJsonValueAsync(json);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to update data: " + ex.Message);
        }
    }

    public async Task DeleteData(string tableName, string id)
    {
        try
        {
            await dbRef.Child(tableName).Child(id).RemoveValueAsync();
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to delete data: " + ex.Message);
        }
    }









    // Function to save user data
    public void SaveUserData(User user)
    {
        string json = JsonUtility.ToJson(user);
        //Debug.Log("User Data JSON: " + json);
        dbRef.Child("users").Child(user.UserId).SetRawJsonValueAsync(json).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("User data saved successfully!");
            }
            else
            {
                Debug.LogError("Failed to save user data: " + task.Exception);
            }
        });
    }

    // Function to get user data
    public void GetUserData(string userId)
    {
        dbRef.Child("users").Child(userId).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    string json = snapshot.GetRawJsonValue();
                    User user = JsonUtility.FromJson<User>(json);
                    Debug.Log("User Data: " + user.Name + ", " + user.Email);
                }
                else
                {
                    Debug.Log("User data not found.");
                }
            }
        });
    }
}