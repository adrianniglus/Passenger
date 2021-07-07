using System;

namespace Passenger.Core.Domain
{
    public class Route
    {
        
        public string Name { get; protected set; }
        public Node StartNode {get; protected set;}
        public Node EndNode {get; protected set;}
        public double Distance { get; set; }
    
        public Route()
        {
        }

        private Route(string name,Node startNode, Node endNode, double distance)
        {
            SetName(name);
            SetStartNode(startNode);
            SetEndNode(endNode);
            if(distance < 0)
            {
                throw new Exception("Distance can not be less the zero!");
                
            }
            Distance = distance;
        }

        public void SetName(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Route name is invalid");
            }
            if(Name == name)
            {
                return;
            }
            Name = name;
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

        public static Route Create(string name,Node startNode, Node endNode, double distance)
            => new Route(name,startNode, endNode, distance);
    }

    
}