public interface IGraph
{
    void AddEdge(int v, int w);
    IEnumerable<int> Adjacent(int v);
    int V();
    int E();
}