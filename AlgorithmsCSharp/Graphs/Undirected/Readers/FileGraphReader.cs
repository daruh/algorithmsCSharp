namespace AlgorithmsCSharp.Graphs.Undirected.Readers;

public interface IGraphReader
{
    IGraph ReadFromFile(string path);
}

public class FileGraphReader : IGraphReader
{
    public IGraph ReadFromFile(string path)
    {
        var lines = File.ReadAllLines(path);
        if (lines.Length < 2)
        {
            throw new Exception("no enough arguments");
        }

        var verticies = int.Parse(lines[0]);
        var edges = int.Parse(lines[1]);

        if (lines.Length - 2 != edges)
        {
            throw new Exception("no declared number of edges");
        }

        var graph = new Graph(verticies);

        for (int i = 2; i < lines.Length; i++)
        {
            var edgeLine = lines[i];
            var edgeArray = edgeLine.Split(" ");

            if (edgeArray.Length != 2)
            {
                throw new Exception($"not enough edges at line {i}");
            }

            var v = int.Parse(edgeArray[0]);
            var w = int.Parse(edgeArray[1]);

            graph.AddEdge(v, w);
        }

        return graph;
    }
}