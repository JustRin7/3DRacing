public class Node<T>
{
    public Node<T> Next;
    public T Data;
}


class Main
{
    public Main()
    {
        Node<Timer> a = new Node<Timer>();
        a.Next = null;
        a.Data = new Timer();

        Node<int> b = new Node<int>();
        b.Next = null;
        b.Data = 3;

        b.Data = 4;
    }
}