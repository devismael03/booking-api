namespace BookingApi.Application.DataStructures;

public class IntervalTreeNode
{
    public Interval Interval { get; set; }
    public DateOnly MaxEnd { get; set; }
    public IntervalTreeNode? Left { get; set; }
    public IntervalTreeNode? Right { get; set; }

    public IntervalTreeNode(Interval interval)
    {
        Interval = interval;
        MaxEnd = interval.End;
    }
}