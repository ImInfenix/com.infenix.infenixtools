using InfenixTools.DataStructures;
using System.Collections.Generic;
using UnityEngine;

namespace InfenixTools.Pathfinding
{
    public class PathNode : MonoBehaviour
    {
        #region Id Generators
        private static ColorBinder<uint> nodesGroupsColorBinder;
        private static ColorBinder<uint> NodesGroupsColorBinder
        {
            get
            {
                if (nodesGroupsColorBinder == null)
                    nodesGroupsColorBinder = new ColorBinder<uint>();

                return nodesGroupsColorBinder;
            }
        }

        private static IdGenerator nodesGroupsIdGenerator;
        private static IdGenerator NodesGroupsIdGenerator
        {
            get
            {
                if (nodesGroupsIdGenerator == null)
                    nodesGroupsIdGenerator = new IdGenerator();

                return nodesGroupsIdGenerator;
            }
        }

        private static IdGenerator nodesGenerator;
        private static IdGenerator NodesIdGenerator
        {
            get
            {
                if (nodesGenerator == null)
                    nodesGenerator = new IdGenerator();

                return nodesGenerator;
            }
        }
        #endregion

        public float nodeSize;

        private uint id;
        public uint Id { get => id; }

        public uint groupId;

        private List<PathNode> linkedNodes;
        public List<PathNode> LinkedNodes
        {
            get
            {
                if (linkedNodes == null)
                    linkedNodes = new List<PathNode>();

                return linkedNodes;
            }
        }

        public bool IsInRange(Vector3 position)
        {
            return DistanceTo(position) < nodeSize;
        }

        public float cost = float.PositiveInfinity;
        public float heuristic;

        private void Awake()
        {
            id = NodesIdGenerator.Next;
        }

        public void LinkTo(PathNode neighbour)
        {
            if (linkedNodes == null)
                linkedNodes = new List<PathNode>();
            if (neighbour.linkedNodes == null)
                neighbour.linkedNodes = new List<PathNode>();

            if (linkedNodes.Contains(neighbour))
                return;

            linkedNodes.Add(neighbour);
            neighbour.linkedNodes.Add(this);
        }

        public void UnlinkFrom(PathNode neighbour)
        {
            linkedNodes.Remove(neighbour);
            neighbour.linkedNodes.Remove(this);

            CheckIfNeighboursAreStillLinked(neighbour);
        }

        private void CheckIfNeighboursAreStillLinked(PathNode neighbour)
        {
            foreach (PathNode otherNeighbour in linkedNodes)
            {
                if (neighbour.HasPathTo(otherNeighbour))
                {
                    NodesGroupsColorBinder.BindToRandomColor(NodesGroupsIdGenerator.Next);
                    neighbour.AssignToGroup(NodesGroupsIdGenerator.Current);
                }
            }
        }

        public void UnlinkAll()
        {
            if (linkedNodes == null)
                return;

            while (linkedNodes.Count > 0)
                UnlinkFrom(linkedNodes[0]);
        }

        public void UpdateGroupStatus()
        {
            if (linkedNodes == null || linkedNodes.Count == 0)
            {
                groupId = NodesGroupsIdGenerator.Next;
                NodesGroupsColorBinder.BindToRandomColor(groupId);
                return;
            }

            AssignToGroup(linkedNodes[0].groupId);
        }

        private void AssignToGroup(uint newGroupToAssignTo)
        {
            groupId = newGroupToAssignTo;

            foreach (PathNode neighbour in linkedNodes)
            {
                if (neighbour.groupId != newGroupToAssignTo)
                    neighbour.AssignToGroup(newGroupToAssignTo);
            }
        }

        public float DistanceTo(PathNode other)
        {
            return DistanceTo(other.transform.position);
        }

        public float DistanceTo(Vector3 targetPosition)
        {

            return Vector3.Distance(transform.position, targetPosition);
        }

        private bool HasPathTo(PathNode target)
        {
            return PathfinderManager.Instance.GetPathAStar(this, target) == null;
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = NodesGroupsColorBinder.GetValue(groupId);
            Gizmos.DrawSphere(transform.position, nodeSize);

            if (linkedNodes == null)
                return;

            foreach (var node in linkedNodes)
                Gizmos.DrawLine(transform.position, transform.position + (node.transform.position - transform.position) * 0.5f);
        }
    }
}
