using System;

namespace AlgoMe.Controllers {
    public class Algorithm {
        public class Item {
            public long Weight { get; set; }
            public long Value { get; set; }
        }
        
        public static long Knapsack(Item[] items, long capacity) {
            var matrix = new long[items.Length + 1, capacity + 1];
            for (var itemIndex = 0; itemIndex <= items.Length; itemIndex++) {
                var currentItem = itemIndex == 0 ? null : items[itemIndex - 1];
                for (var currentCapacity = 0; currentCapacity <= capacity; currentCapacity++) {
                    if (currentItem == null || currentCapacity == 0) { 
                        matrix[itemIndex, currentCapacity] = 0;
                    } else if (currentItem.Weight <= currentCapacity) { 
                        matrix[itemIndex, currentCapacity] = Math.Max(
                            currentItem.Value 
                            + matrix[itemIndex - 1, currentCapacity - currentItem.Weight],
                            matrix[itemIndex - 1, currentCapacity]);
                    } else {
                        matrix[itemIndex, currentCapacity] = matrix[itemIndex - 1, currentCapacity];
                    }
                }
            }
            return matrix[items.Length, capacity];
        }
    }
}