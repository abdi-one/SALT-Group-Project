using UnityEngine;
using System.Collections.Generic;

public class Rewind : MonoBehaviour
{
    public static float gravity = -100;

    public struct TimeRecordedData
    {
      public Vector2 pos;
      public Vector2 vel;
    }

    TimeRecordedData[][] recordedData;
    int recordMax = 100000;
    int recordCount;
    int recordIndex;
    bool wasSteppingBack = false;

    RewindedP[] timeObjects;

    
    private void Awake()
    {

      timeObjects = GameObject.FindObjectsByType<RewindedP>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
      recordedData = new TimeRecordedData[recordMax][];

      for (int i = 0; i < recordMax; i++)
      {
        recordedData[i] = new TimeRecordedData[timeObjects.Length];
      }

    }

    void Start()
    {
        
    }

    
    void Update()
    {
      bool pause = Input.GetKey(KeyCode.I);
      bool stepBack = Input.GetKey(KeyCode.R);
      bool stepForward = Input.GetKey(KeyCode.L);

      if (stepBack)
      {
        wasSteppingBack = true;

        recordIndex--;


        if (recordIndex < 0)
        {
          recordIndex = 0;
        }

        for (int objectIndex = 0; objectIndex < timeObjects.Length; objectIndex++)
        {
          RewindedP timeObject = timeObjects[objectIndex];
          TimeRecordedData data = recordedData[recordIndex][objectIndex];
          timeObject.transform.position = data.pos;
          timeObject.velocity = data.vel;
        }
      }
      else if (pause && stepForward)
      {

      }
      else if (!pause && !stepBack)
      {
        if (wasSteppingBack)
        {
          recordCount = recordIndex;
          wasSteppingBack = false;
        }

            
        for(int objectIndex = 0; objectIndex < timeObjects.Length; objectIndex++)
        {
          RewindedP timeObject = timeObjects[objectIndex];
          TimeRecordedData data = new TimeRecordedData();
          data.pos = timeObject.transform.position;
          data.vel = timeObject.velocity; 
          recordedData[recordCount][objectIndex] = data;
        }
        recordCount++;
        recordIndex = recordCount;

        foreach(RewindedP timeObject in timeObjects) 
        {
          timeObject.TimeUpdate();
        }
        
    }
  }
}
