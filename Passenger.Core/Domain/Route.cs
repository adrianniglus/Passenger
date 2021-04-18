using System;

namespace Passenger.Core.Domain
{
    public class Route
    {
        public Guid Id {get; protected set;}
        public Node StartNode {get; protected set;}
        public Node EndNode {get; protected set;}
    
        public Route()
        {
        }

        public Route(Node startNode, Node endNode)
        {
            Id = Guid.NewGuid();
            SetStartNode(startNode);
            SetEndNode(endNode);
        }

        public void SetStartNode(Node startNode)
        {
            if(startNode == null)
            {
                throw new Exception("Provide proper start node!");
            }

            if(StartNode == startNode)
            {
                return;
            }

            StartNode = startNode;
        }

        public void SetEndNode(Node endNode)
        {
            if(endNode == null)
            {
                throw new Exception("Provide proper end node!");
            }

            if(EndNode == endNode)
            {
                return;
            }

            EndNode = endNode;
        }
    }

    
}