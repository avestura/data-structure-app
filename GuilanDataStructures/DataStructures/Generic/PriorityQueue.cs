using GuilanDataStructures.DataStructures.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuilanDataStructures.DataStructures.Generic
{
    public class PriorityQueue<T>
    {

        List<QueueData<T>> Datas { get; set; } = new List<QueueData<T>>();

        public int Size { get { return Datas.Count; } }

        public bool Empty { get { return (Datas.Count == 0);  } }


        private int MaxHeapify(int position)
        {
            if (position > Datas.Count - 1) return -1;

            while (position > 0)
            {
                if (Datas[ParentLocationOf(position)].Priority < Datas[position].Priority)
                {
                    SwapByIndex(ParentLocationOf(position), position);
                    position = ParentLocationOf(position);
                }
                else break;
            }
            return position;

        }

        private void SwapByIndex(int index1, int index2)
        {
            var temp = Datas[index1];
            Datas[index1] = Datas[index2];
            Datas[index2] = temp;
        }

        public int ParentLocationOf(int position)
        {
            return (position - 1) / 2;
        }
        public bool HasLeft(int position)
        {
            return ((2 * position + 1) < Size);
        }
        public bool HasRight(int position)
        {
            return ((2 * position + 2) < Size);
        }
        public int LeftPositionOf(int position)
        {
            if (!HasLeft(position)) return -1;
            return (2 * position + 1);
        }
        public int RightPositionOf(int position)
        {
            if (!HasRight(position)) return -1;
            return (2 * position + 2);
        }

        public void EnqueueNew(int priority, T data)
        {

            Datas.Add(new QueueData<T>(priority, data));

            MaxHeapify(Datas.Count - 1);

        }

        public void Enqueue(QueueData<T> q)
        {
            Datas.Add(q);
            MaxHeapify(Datas.Count - 1);
        }

        private void MoveDown(int index)
        {
            int largerIndex = index;

            if (HasLeft(index) && Datas[LeftPositionOf(index)].Priority > Datas[largerIndex].Priority) largerIndex = LeftPositionOf(index);
            if (HasRight(index) && Datas[RightPositionOf(index)].Priority > Datas[largerIndex].Priority) largerIndex = RightPositionOf(index);

            if (largerIndex != index)
            {
                SwapByIndex(largerIndex, index);
                MoveDown(largerIndex);
            }
  
        }

        public QueueData<T> DequeueData()
        {
            QueueData<T> tree = Datas[0];
            Datas.RemoveAt(0);
            MoveLastItemToFirst();
            MoveDown(0);
            return tree;
        }



        private void MoveLastItemToFirst()
        {
            if (Datas.Count == 0) return;
            var tree = Datas[Datas.Count - 1];
            Datas.RemoveAt(Datas.Count - 1);
            Datas.Insert(0, tree);
        }


        public void Clear()
        {
            Datas.Clear();
        }

        


    }

    public class QueueData<T>
    {
        public T Data { get; set; }
        public int Priority { get; set; }

        public QueueData(){}
        public QueueData(int p, T d)
        {
            Data = d;
            Priority = p;
        }
    }


}
