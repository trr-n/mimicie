[System.Serializable]
public class TestData
{
    public string name;
    public int time;
    public int score;
    public TestData() {; }
    public TestData(string name, int time, int score)
    {
        this.name = name;
        this.time = time;
        this.score = score;
    }
}