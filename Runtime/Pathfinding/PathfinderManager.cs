using InfenixTools.DesignPatterns;
using Priority_Queue;
using System.Collections.Generic;
using UnityEngine;

namespace InfenixTools.Pathfinding
{
    public class PathfinderManager : Singleton<PathfinderManager>
    {
        [SerializeField]
        private float defaultNodeSize = .1f;

        [SerializeField]
        private LayerMask pathfindingOptimizationLayers;

        private List<PathNode> existingNodes;

        protected override void Awake()
        {
            base.Awake();
            existingNodes = new List<PathNode>();
        }

        public void RegisterPathNode(PathNode nodeToRegister)
        {
            nodeToRegister.nodeSize = defaultNodeSize;

            existingNodes.Add(nodeToRegister);

            nodeToRegister.UpdateGroupStatus();
        }

        public void UnregisterPathNode(PathNode nodeToUnregister)
        {
            existingNodes.Remove(nodeToUnregister);
            nodeToUnregister.UnlinkAll();
        }

        public PathNode GetClosestNode(Vector3 position)
        {
            PathNode closestFound = null;
            float minDistanceFound = float.PositiveInfinity;

            foreach (PathNode node in existingNodes)
            {
                float nodeDistance = node.DistanceTo(position);

                if (closestFound == null)
                {
                    closestFound = node;
                    minDistanceFound = nodeDistance;
                }
                else if (nodeDistance < minDistanceFound)
                {
                    closestFound = node;
                    minDistanceFound = nodeDistance;
                }
            }

            return closestFound;
        }

        public PathNode GetRandomNode(params PathNode[] nodesToExclude)
        {
            if (existingNodes.Count <= nodesToExclude.Length)
                return null;

            int maxAttempts = 5;

            PathNode res;
            bool foundNode = false;
            int attempts = 0;

            while (!foundNode && attempts < maxAttempts)
            {
                res = existingNodes[Random.Range(0, existingNodes.Count)];
                if (System.Array.IndexOf(nodesToExclude, res) == -1)
                {
                    return res;
                }

                attempts++;
            }

            return null;
        }

        public LinkedList<PathNode> GetPathAStar(PathNode from, PathNode to)
        {
            if (from == null || to == null)
                return null;

            Queue<PathNode> closedList = new Queue<PathNode>();
            SimplePriorityQueue<PathNode> openList = new SimplePriorityQueue<PathNode>();
            from.cost = 0;
            from.heuristic = 0;
            openList.Enqueue(from, from.DistanceTo(to));

            while (openList.Count > 0)
            {
                PathNode current = openList.Dequeue();
                if (current == to)
                {
                    LinkedList<PathNode> result = ConstructResultingPath(to);
                    ResetNodes(closedList, openList);
                    current.cost = float.PositiveInfinity;
                    return result;
                }

                foreach (PathNode neighbour in current.LinkedNodes)
                {
                    if (!(closedList.Contains(neighbour) || (openList.Contains(current) && openList.GetPriority(current) < neighbour.cost)))
                    {
                        neighbour.cost = current.cost + 1;
                        neighbour.heuristic = neighbour.cost + neighbour.DistanceTo(to);
                        openList.Enqueue(neighbour, neighbour.heuristic);
                    }
                }
                closedList.Enqueue(current);
            }
            ResetNodes(closedList, openList);
            return null;

            static void ResetNodes(Queue<PathNode> closedList, SimplePriorityQueue<PathNode> openList)
            {
                while (closedList.Count > 0)
                    closedList.Dequeue().cost = float.PositiveInfinity;

                while (openList.Count > 0)
                    openList.Dequeue().cost = float.PositiveInfinity;
            }
        }

        public LinkedList<PathNode> GetPathAStar(Vector3 from, PathNode to)
        {
            return GetPathAStar(GetClosestNode(from), to);
        }

        public LinkedList<PathNode> GetPathAStar(Vector3 from, Vector3 to)
        {
            return GetPathAStar(from, GetClosestNode(to));
        }

        LinkedList<PathNode> ConstructResultingPath(PathNode endpointNode)
        {
            LinkedList<PathNode> foundPath = new LinkedList<PathNode>();

            PathNode current = endpointNode;
            while (current != null)
            {
                foundPath.AddFirst(current);

                float cost = float.PositiveInfinity;
                PathNode lowestCostNode = null;
                foreach (var node in current.LinkedNodes)
                {
                    if (node.cost < cost && node.cost < current.cost)
                        lowestCostNode = node;
                }
                current = lowestCostNode;
            }

            return foundPath;
        }

        public static LinkedList<PathNode> OptimizePath(LinkedList<PathNode> originalPath)
        {
            return OptimizePath(originalPath, Instance.pathfindingOptimizationLayers);
        }

        private static LinkedList<PathNode> OptimizePath(LinkedList<PathNode> originalPath, LayerMask obstructingObjects)
        {
            if (originalPath == null)
                return null;

            LinkedList<PathNode> optimizedResult = new LinkedList<PathNode>();

            var current = originalPath.First;
            optimizedResult.AddLast(current.Value);
            current = current.Next;

            PathNode previousCorrectNode = null;
            while (current != null)
            {
                if (previousCorrectNode == null)
                {
                    previousCorrectNode = current.Value;
                }
                else if (IsLinkObstructed(optimizedResult.Last.Value, current.Value, obstructingObjects))
                {
                    optimizedResult.AddLast(previousCorrectNode);
                    previousCorrectNode = current.Value;
                }
                else
                {
                    previousCorrectNode = current.Value;
                }

                current = current.Next;
            }

            if (previousCorrectNode != null)
            {
                optimizedResult.AddLast(previousCorrectNode);
            }

            return optimizedResult;
        }

        private static bool IsLinkObstructed(PathNode from, PathNode to, LayerMask obstructingObjects)
        {
            return Physics.Raycast(from.transform.position, (to.transform.position - from.transform.position).normalized, Vector3.Distance(from.transform.position, to.transform.position), obstructingObjects);
        }

        private void OnDrawGizmosSelected()
        {
            if (existingNodes == null)
                return;

            foreach (PathNode node in existingNodes)
                node.OnDrawGizmosSelected();
        }
    }
}
