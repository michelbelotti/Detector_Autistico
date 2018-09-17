[System.Serializable]
public class Report_Save
{
    //fase 2
    public int phase2Total;
    public float phase2average;
    public float[] phase2latency;

    //fase 4

    //fase 6

    //fase 7

    public Report_Save(int phase2LatencyLength)
    {
        phase2latency = new float[phase2LatencyLength];
    }
}
