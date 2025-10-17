using System;

public class SimpleLinkList<T>
{

    private class Node
    {
        public T Value { get; set; }
        public Node Next { get; set; }
        public Node(T value)
        {
            Value = value;
            Next = null;
        }

    }
    int cont = 0;
    Node head;
    public void AddAtStart(T value)
    {
        Node newNode = new Node(value);
        newNode.Next = head;
        head = newNode;
        cont++;
    }

    public void AddAtEnd(T value)
    {

        if (head == null)
        {
            AddAtStart(value);
        }
        else
        {
            Node newNode = new Node(value);
            Node aux = head;
            while (aux.Next != null)
            {
                aux = aux.Next;

            }
            aux.Next = newNode;
            cont++;
        }
    }


    public void AddToPosition(T value, int position)
    {

        if (position == 0)
        {
            AddAtStart(value);
        }
        else if (position == cont - 1)
        {
            AddAtEnd(value);
        }
        else if (position >= cont)
        {
            throw new NullReferenceException("No existe esa posicion en la lista");
        }
        else
        {
            Node newNode = new Node(value);
            Node aux = head;
            int i = 0;
            while (i < position - 1)
            {
                i++;
                aux = aux.Next;
            }
            Node posNextPast = aux.Next;
            aux.Next = newNode;
            newNode.Next = posNextPast;

            cont++;

        }



    }

    public void DeleteAtStart()
    {
        if (head == null)
        {
            throw new NullReferenceException("No hay nada que borrar xd");
        }
        else
        {
            head = head.Next;
            cont--;

        }
    }

    public void DeleteAtEnd()
    {
        if (head == null)
        {
            throw new NullReferenceException("No hay nada que borrar xd");
        }
        else if (cont == 1)
        {
            DeleteAtStart();
        }
        else
        {
            Node aux = head;
            while (aux.Next.Next != null)
            {
                aux = aux.Next;
            }
            cont--;
            aux.Next = null;
        }

    }

    public void DeleteAtPosition(int position)
    {
        if (head == null)
        {
            throw new NullReferenceException("No hay nada que borrar");
        }
        else if (position == 0)
        {
            DeleteAtStart();
        }
        else if (position == cont - 1)
        {
            DeleteAtEnd();
        }
        else if (position >= cont)
        {
            throw new NullReferenceException("Esa posicion no existe");
        }
        else
        {
            Node aux = head;
            int i = 0;
            while (i < position - 1)
            {
                i++;
                aux = aux.Next;
            }
            aux.Next = aux.Next.Next;
            cont--;

        }
    }
    public void ModifyAtStart(T value)
    {
        if (head == null)
        {
            throw new NullReferenceException("No hay nada que modificar en la lista");
        }
        else
        {
            head.Value = value;
        }

    }
    public void ModifyAtEnd(T value)
    {
        Node aux = head;
        while (aux.Next != null)
        {
            aux = aux.Next;
        }
        aux.Value = value;
    }

    public void ModifyAtPosition(T value, int position)
    {
        if (head == null)
        {
            throw new NullReferenceException("No hay nada que modificar");
        }
        else if (position == 0)
        {
            ModifyAtStart(value);
        }
        else if (position == cont - 1)
        {
            ModifyAtEnd(value);
        }
        else if (position >= cont)
        {
            throw new NullReferenceException("Esa posicion no existe");
        }
        else
        {
            Node aux = head;
            int i = 0;
            while (i < position)
            {
                i++;
                aux = aux.Next;
            }
            aux.Value = value;
        }
    }

    public T GetValueStart()
    {
        return head.Value;
    }
    public T GetValueEnd()
    {
        Node aux = head;
        while (aux.Next != null)
        {
            aux = aux.Next;
        }
        return aux.Value;
    }

    public T GetValueAtPosition(int position)
    {
        if (position == 0)
        {
            return GetValueStart();
        }
        else if (position == cont)
        {
            return GetValueEnd();
        }
        else if (position >= cont)
        {
            throw new NullReferenceException("No existe esa posicion en la lista");
        }
        else
        {
            Node aux = head;
            int i = 0;
            while (i < position)
            {
                aux = aux.Next;
                i++;
            }
            return aux.Value;
        }

    }
    public int GetCapacityList()
    {
        return cont;
    }
    public void PrintAtAll()
    {
        Node aux = head;
        while (aux != null)
        {
            Console.Write(aux.Value + " ");
            aux = aux.Next;
        }
    }





}