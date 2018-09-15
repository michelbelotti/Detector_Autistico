[System.Serializable]
public class Report_Save
{
    public int phase2Total;
    public float phase2average;
    public float[] phase2latency;

    public Report_Save(int phase2LatencyLength)
    {
        phase2latency = new float[phase2LatencyLength];
    }

    public Report_Save(Report_Save data)
    {
        phase2Total = data.phase2Total;
        phase2average = data.phase2average;
        phase2latency = new float[data.phase2latency.Length];

        for(int i = 0; i < data.phase2latency.Length; i++)
        {
            data.phase2latency[i] = data.phase2latency[i];
            i++;
        }

    }
}
