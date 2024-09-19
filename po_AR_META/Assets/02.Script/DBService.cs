using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class DBService : MonoBehaviour
{
    public class Store
    {
        public string name;
        public float x;
        public float y;

        public Store()
        {

        }

        public Store(string name, float x, float y)
        {
            this.name = name;
            this.x = x;
            this.y = y;
        }
    }

    DatabaseReference reference;

    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        writeNewStore("10", "seoul", 37.47737f, 126.8626f);
        writeNewStore("20", "pusan", 38.1795f, 128.8626f);
    }

    private void writeNewStore(string storeId, string name, float x, float y)
    {
        Store store = new Store(name, x, y);
        string str = JsonUtility.ToJson(store);

        reference.Child("stores").Child(storeId).SetRawJsonValueAsync(str);
    }

    public void CreateStoreCharacter(Vector2 gpsPos, Transform image)
    {
        FirebaseDatabase.DefaultInstance.GetReference("stores").
            GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {

                }
                else if(task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    foreach(DataSnapshot data in snapshot.Children)
                    {
                        /*
                         데이터베이스에서 스냅샷(그 순간 값)을 가져옵니다. 
                         */
                        string value = data.GetRawJsonValue();
                        Store store = JsonUtility.FromJson<Store>(value);

                        Vector2 dbPos = new Vector2(store.x, store.y);
                        float d = Vector2.Distance(gpsPos, dbPos);
                        if ((d<0.0005f))
                        {
                            GameObject prefab = Resources.Load(store.name) as GameObject;
                            Instantiate(prefab, image.position, image.rotation);
                        }
                    }
                }
            }
            );
    }
    
    void Update()
    {
        
    }
}
