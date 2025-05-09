using System;
using System.Collections.Generic;
using System.Linq;


  var trip = new OneDimensionTrip();

        int start = 2;
        int[] shops = { 4, 9 };
        int[] stations = { 3, 6, 8 };
        int target = 7;

        int result = trip.MinEnergy(start, shops, stations, target);
        Console.WriteLine("Minimum energy: " + result); // คาดว่าได้ 5

public class OneDimensionTrip
{
    public int MinEnergy(int start, int[] shops, int[] stations, int target)
    {
        var shopSet = new HashSet<int>(shops);
        var stationSet = new HashSet<int>(stations);

        var queue = new Queue<(int pos, HashSet<int> visitedShops, int energy)>();
        var visited = new HashSet<string>();

        queue.Enqueue((start, new HashSet<int>(), 0));

        while (queue.Count > 0)
        {
            var (pos, visitedShops, energy) = queue.Dequeue();

            var visitedKey = $"{pos}-{string.Join(",", visitedShops.OrderBy(x => x))}";
            if (visited.Contains(visitedKey)) continue;
            visited.Add(visitedKey);

            if (shopSet.Contains(pos)) visitedShops.Add(pos);

            // Check goal condition
            if (visitedShops.Count == shopSet.Count && pos == target)
                return energy;

            // Move left
            if (pos - 1 >= 0)
                queue.Enqueue((pos - 1, new HashSet<int>(visitedShops), energy + 1));

            // Move right
            queue.Enqueue((pos + 1, new HashSet<int>(visitedShops), energy + 1));

            // Use station to teleport
            if (stationSet.Contains(pos))
            {
                foreach (var station in stations)
                {
                    if (station != pos)
                        queue.Enqueue((station, new HashSet<int>(visitedShops), energy));
                }
            }
        }

        return -1; // No valid path found
    }


    
}






