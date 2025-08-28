namespace BookingApi.Application.DataStructures;

public class IntervalTree
{
    private IntervalTreeNode? _root;

    public void Insert(Interval interval)
    {
        _root = Insert(_root, interval);
    }

    private IntervalTreeNode Insert(IntervalTreeNode? node, Interval interval)
    {
        if (node == null)
            return new IntervalTreeNode(interval);

        if (interval.End > node.MaxEnd)
            node.MaxEnd = interval.End;

        if (interval.Start < node.Interval.Start)
            node.Left = Insert(node.Left, interval);
        else
            node.Right = Insert(node.Right, interval);

        return node;
    }

    public List<string> SearchHomeIds(DateOnly queryStart, DateOnly queryEnd)
    {
        var result = new HashSet<string>();
        SearchHomeIds(_root, queryStart, queryEnd, result);
        return result.ToList();
    }

    private void SearchHomeIds(IntervalTreeNode? node, DateOnly queryStart, DateOnly queryEnd, HashSet<string> result)
    {
        if (node == null)
            return;

        if (node.Interval.Contains(queryStart, queryEnd))
        {
            result.Add(node.Interval.HomeId);
        }

        if (node.Left != null && node.Left.MaxEnd >= queryStart)
        {
            SearchHomeIds(node.Left, queryStart, queryEnd, result);
        }

        if (node.Right != null && node.Interval.Start <= queryEnd)
        {
            SearchHomeIds(node.Right, queryStart, queryEnd, result);
        }
    }

}