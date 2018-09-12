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
}
