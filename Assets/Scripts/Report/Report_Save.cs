[System.Serializable]
public class Report_Save
{

    //nome da crianca
    public string childName;

    //fase 2
    public int phase2Total;
    public float phase2average;
    public float[] phase2latency;

    //fase 4
    public float[,] phase4Positions;

    //fase 6
    public int phase6Tries;

    //fase 7
    public int phase7Total;
    public float phase7average;
    public float[] phase7latency;

    public Report_Save()
    {
        
    }

    public void SetPhase2Data(int phase2LatencyLength)
    {
        phase2latency = new float[phase2LatencyLength];
    }

    public void SetPhase4Data(int phase4PosLength)
    {
        phase4Positions = new float[phase4PosLength,3];
    }

    public void SetPhase7Data(int phase7LatencyLength)
    {
        phase7latency = new float[phase7LatencyLength];
    }
}
