using System.Text;

public interface IGraph
{
    void AddEdge(int v, int w);
    IEnumerable<int> Adjacent(int v);
    int V();
    int E();
}

public class Graph : IGraph
{
    private readonly int _verticies;
    private int _edges;
    private readonly Dictionary<int, HashSet<int>> _adjacencyList;

    public Graph(int v)
    {
        _verticies = v;
        _edges = 0;
        _adjacencyList = new Dictionary<int, HashSet<int>>();
        for (var i = 0; i < _verticies; i++)
        {
            _adjacencyList.Add(i, new HashSet<int>());
        }
    }

    public void AddEdge(int v, int w)
    {
        _adjacencyList[v].Add(w);
        _adjacencyList[w].Add(v);
        _edges++;
    }

    public IEnumerable<int> Adjacent(int v)
    {
        return _adjacencyList[v];
    }

    public int V()
    {
        return _verticies;
    }

    public int E()
    {
        return _edges;
    }

    public override string ToString()
    {
        var strBuilder = new StringBuilder();
        strBuilder.AppendLine($"{_verticies} verticies, {_edges} edges ");
        for (var i = 0; i < _verticies; i++)
        {
            strBuilder.AppendLine($"{i}: {string.Join(" ", Adjacent(i))}");
        }

        return strBuilder.ToString();
    }
}