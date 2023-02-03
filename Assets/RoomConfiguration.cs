using System;
using System.Collections.Generic;
using System.Linq;
using ToyBoxHHH;
using UnityEngine;

[ExecuteAlways]
public class RoomConfiguration : MonoBehaviour
{
    public string conName = "Just Another Room";
    
    [System.Serializable]
    public class Config
    {
        public Transform obj;
        public Pose pose;
        public Vector3 localScale;
    }

    public List<Config> configs = new List<Config>();

    public List<Transform> objToSave = new List<Transform>();

    private void Update()
    {
        transform.name = conName;
    }

    [DebugButton]
    public void Load()
    {
        foreach (var c in configs.OrderBy(con=>CountParents(con.obj)))
        {
            c.obj.position = c.pose.position;
            c.obj.rotation = c.pose.rotation;
            c.obj.localScale = c.localScale;
        }
    }

    private int CountParents(Transform argObj)
    {
        int pareCount = 0;
        var p = argObj.parent;
        while (p != null)
        {
            pareCount++;
            p = p.parent;
        }

        return pareCount;
    }

    [DebugButton]
    public void Save()
    {
        configs.Clear();
        for (int i = 0; i < objToSave.Count; i++)
        {
            var o = objToSave[i];
            //var c = configs.Find(c => c.obj == o);
            RoomConfiguration.Config c = null;
            if (c == null)
            {
                c = new Config();
                c.obj = o;
            }
            c.pose = new Pose(o.position, o.rotation);
            c.localScale = o.localScale;
            configs.Add(c);
        }
    }

    [DebugButton]
    public void AddFurniture()
    {
        objToSave.Clear();
        foreach (var t in FindObjectsOfType<SelectionScript>())
        {
            objToSave.Add(t.transform);
        }
    }
}
