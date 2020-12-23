using System;
using System.Collections.Generic;
using System.Linq;

namespace Nintex.Services.ShorteningURL.KeysGenerationService.Service
{
    public class Counters
    {
        private LinkedList<int> nodes { set; get; }
        public Counters(List<int> initialList)
        {
            Reset(initialList);
        }
        public List<int> PlusOne()
        {            
            return Move(nodes.Last);
        }
        private List<int> Move(LinkedListNode<int> node)
        {
            node.Value++;
            if (node.Value == 64)
            {
                node.Value = 0;
                if (node.Previous == null)
                    throw new Exception("no More keys options available");
                return Move(node.Previous);
            }
            return nodes.ToList();                
        }
        public List<int> Current()
        {
           return nodes.ToList();
        }
        public void Reset(List<int> values)
        {
            nodes = new LinkedList<int>();
            for (int i = 0; i < 6; i++)
            {
                nodes.AddLast(values[i]);
            }            
        }
        public override string ToString()
        {
            return string.Join(",", nodes.ToList());
        }

    }
}
