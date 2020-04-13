using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents.Sensors;
using MLAgents;
using System;
using System.Linq;
using System.IO;

public class Thermoregulator_Area : MonoBehaviour
{

    public Thermo_agent agent_p;

    private void FixedUpdate()
    {
        // float tt = Time.fixedTime;
        if (agent_p.StepCount % 100 ==0) //record timestamp and group mean temp
        {
            float[] frame_data = new float[3];
            frame_data[0] = (float)agent_p.StepCount;
            frame_data[1] = GetComponentInChildren<Thermo_agent>().GetMeanTemperature();
            frame_data[2] = GetSTDTemperature(GetT_array());
            WriteToCSV(frame_data);

        }

    }

    public void WriteToCSV(float[] data)
    {
        string filename = "D:/ml-agents-release-0.15.1/ml-agents-release-0.15.1/Project/Assets/ML-Agents/Examples/Thermoregulators/extracted_data/group_mean_Tp_RTg_2.csv";
        using (var writer = new StreamWriter(filename, append: true))
        {
            string data_string = string.Join(",", data); //convert array to string separated by commas
            // AllSensorsObs += "," + m_data_string;
            writer.WriteLine(data_string);  //write each line to csv
        }
    }

    public float GetSTDTemperature(float[] T_array)
    {
        var T_groupmean = GetComponentInChildren<Thermo_agent>().GetMeanTemperature(); //you could also have defined the getmeantemp function in this script
        double sumOfSquares = 0.0;
        foreach (float t in T_array)
        {
            sumOfSquares += Math.Pow((t - T_groupmean), 2.0);  ;
        }
        return (float)Math.Sqrt(sumOfSquares / (double)(T_array.Length));
    }

    public float[] GetT_array()
    {
        GameObject[] agents = GameObject.FindGameObjectsWithTag("agent");
        List<float> T_array = new List<float>();
        foreach (GameObject p in agents)
        {
            T_array.Add((float)p.GetComponent<Thermo_agent>().transform.position.y);
        }
        return T_array.ToArray();
    }
}