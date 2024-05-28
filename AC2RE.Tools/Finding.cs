using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AC2RE.Tools
{
    public class Finding
    {
        public Vector2 start { get; set; }
        public Vector2 goal { get; set; }
        public List<Vector2> pathList { get; set; }

        public Finding(Vector2 start, Vector2 goal, QuadTree quad)
        {
            this.start = start;
            this.goal = goal;
            pathList = new List<Vector2>();

            pathFinding(quad);
        }

        private void pathFinding(QuadTree quad)
        {
            Dictionary<Vector2, float> pathListOpen = new Dictionary<Vector2, float>();
            HashSet<Vector2> pathListClose = new HashSet<Vector2>();
            Dictionary<Vector2, Vector2> cameFrom = new Dictionary<Vector2, Vector2>(); // Pour reconstruire le chemin

            float scoreStart = 0;
            pathListOpen.Add(start, CalculeScore(goal, start, scoreStart));

            while (pathListOpen.Count > 0)
            {
                // Trouver le nœud avec le score f le plus bas
                var current = pathListOpen.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;

                // Si le but est atteint
                if (current == goal)
                {
                    // Reconstruire le chemin
                    ReconstructPath(cameFrom, current);
                    return;
                }

                pathListOpen.Remove(current);
                pathListClose.Add(current);

                // Rechercher les voisins du nœud courant
                var neighbors = quad.QueryNeighbouringPoints(current);

                foreach (var point in neighbors)
                {
                    if (!point.passable) continue;

                    var neighbor = new Vector2(point.realPoint.X, point.realPoint.Y);
                    if (pathListClose.Contains(neighbor)) continue;

                    float tentativeGScore = scoreStart + 1; // Distance de 1 dans une grille

                    if (!pathListOpen.ContainsKey(neighbor))
                    {
                        cameFrom[neighbor] = current;
                        pathListOpen.Add(neighbor, CalculeScore(goal, neighbor, tentativeGScore));
                    }
                    else if (tentativeGScore < pathListOpen[neighbor])
                    {
                        // Mise à jour du score g si un meilleur chemin est trouvé
                        pathListOpen[neighbor] = CalculeScore(goal, neighbor, tentativeGScore);
                        cameFrom[neighbor] = current;
                    }
                }
            }
        }

        private void ReconstructPath(Dictionary<Vector2, Vector2> cameFrom, Vector2 current)
        {
            List<Vector2> totalPath = new List<Vector2> { current };
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                totalPath.Add(current);
            }
            totalPath.Reverse();
            pathList = totalPath;
        }

        // Calcule h(n) = |x_goal - x_n| + |y_goal - y_n|
        // f(n) = g(n) + h(n)
        private float CalculeScore(Vector2 goal, Vector2 neighbor, float gScoreStartToNeighbor)
        {
            float g = gScoreStartToNeighbor;
            float h = Math.Abs(goal.X - neighbor.X) + Math.Abs(goal.Y - neighbor.Y);
            float f = g + h;

            return f;
        }
    }
}
