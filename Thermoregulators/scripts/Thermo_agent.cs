using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents.Sensors;
using MLAgents;
using System;
using System.Linq;
using System.IO;

public class Thermo_agent : Agent
{
    private float deltaT=0.15f; //temperature increment
    public int ID;
    private float T_group;
    // private float T_p; //translated to the height y
    public int N_agents;
    float T_target = 0f;
    private float beta = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        OnEpisodeBegin();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(Mathf.Clamp(GetMeanTemperature()/40, -1, 1)); //group level obs
        sensor.AddObservation(Mathf.Clamp(transform.position.y/40, -1, 1)); //individual level observation
    }

    public override float[] Heuristic()
    {
        var useraction = new float[1];
        // useraction should be all zero by default?
        useraction[0] = 0f;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            useraction[0] = 1f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            useraction[0] = 2f; 
        }
        return useraction;
    }

    public override void OnActionReceived(float[] vectoraction)
    {
        if (vectoraction[0]==1)
        {
            transform.position =  transform.position + new Vector3 (0, deltaT, 0); //NOTE YOU CANT JUST MODIFY TRANSFORM.POSITION.Y FOR SILLY REASONS. MUST RESET THE WHOLE TRANSFORM.POSITION STRUCT
        }
        else if (vectoraction[0]==2)
        {
            transform.position =  transform.position + new Vector3 (0, -deltaT, 0); 
        }

        T_group = GetMeanTemperature();
        AddReward(0.25f*(float)Math.Pow(4f, 1f - Math.Abs(T_group - T_target)*beta)); //group level reward!


        AddReward(-1f / maxStep);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void OnEpisodeBegin()
    {
        transform.position = new Vector3(ID*3, UnityEngine.Random.Range(-10, 10), 0);
    }

    public float GetMeanTemperature()
    {
        T_group = 0;
        GameObject[] agents = GameObject.FindGameObjectsWithTag("agent");
        foreach (GameObject p in agents)
        {
            T_group += p.GetComponent<Thermo_agent>().transform.position.y;
        }
        return T_group/N_agents;
    }

    // public void WriteToCSV(IEnumerable<float> data)
    // {
    //     string filename = "D:/ml-agents-master/ml-agents/Extracted_data/group_mean_obsTp_RTg.csv";
    //     using (var writer = new StreamWriter(filename, append: true))
    //     {
    //         // string AllSensorsObs = null;
    //         // GameObject[] agents = GameObject.FindGameObjectsWithTag("agent");

    //         float[] rowarray = data.Cast<float>().ToArray(); // cast it as an array of ints
    //         string data_string = string.Join(",", rowarray); //convert array to string separated by commas
    //         // AllSensorsObs += "," + m_data_string;
    //         writer.WriteLine(data_string);  //write each line to csv
            
            
    //     }
    // }
}
