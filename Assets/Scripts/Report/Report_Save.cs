[System.Serializable]
public class Report_Save
{

    //nome da crianca
    public string childName;

    //fase 2
    public int phase2Total;
    public double phase2average;
    public double[] phase2latency;

    //fase 4
    public float[] phase4PosX;
    public float[] phase4PosY;

    //fase 6
    public int phase6Tries;

    //fase 7
    public int phase7Total;
    public double phase7average;
    public double[] phase7latency;

    public Report_Save()
    {
        
    }

    public void SetPhase2Data(int phase2LatencyLength)
    {
        phase2latency = new double[phase2LatencyLength];
    }

    public void SetPhase4Data(int phase4PosLength)
    {
        phase4PosX = new float[phase4PosLength];
        phase4PosY = new float[phase4PosLength];
    }

    public void SetPhase7Data(int phase7LatencyLength)
    {
        phase7latency = new double[phase7LatencyLength];
    }
}
