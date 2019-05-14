using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AlgoMe.Models;
using AlgoMe.Models.DataManager;
using Microsoft.EntityFrameworkCore;

namespace AlgoMe.Controllers {
    public class Algorithm {
        public class Item {
            public int Weight { get; set; }
            public int Value { get; set; }
        }
        
        public static async Task Knapsack(DbContextOptions<AlgoMeContext> options, Request request) {
            // TODO: The direct use of DbContext here is a patch
            // TODO: It spoils the idea of using repository in controller
            // Btw it works ¯\_(ツ)_/¯
            using (var context = new AlgoMeContext(options)) {
                var requestRepository = new RequestManager(context);
                var realR = await requestRepository.Get(request.RequestId);
                var items = request.Parameters
                    .Select(p => new Item {Value = (int) p.Price, Weight = (int) p.Weight}).ToArray();

                var matrix = new int[items.Length + 1, request.Capacity + 1];
                request.FullAnswer = new int[(items.Length + 1) * (request.Capacity + 1)];
                for (var itemIndex = 0; itemIndex <= items.Length; itemIndex++) {
                    var currentItem = itemIndex == 0 ? null : items[itemIndex - 1];
                    for (var currentCapacity = 0; currentCapacity <= request.Capacity; currentCapacity++) {
                        if (currentItem == null || currentCapacity == 0) {
                            matrix[itemIndex, currentCapacity] = 0;
                        }
                        else if (currentItem.Weight <= currentCapacity) {
                            matrix[itemIndex, currentCapacity] = Math.Max(
                                currentItem.Value
                                + matrix[itemIndex - 1, currentCapacity - currentItem.Weight],
                                matrix[itemIndex - 1, currentCapacity]);
                        }
                        else {
                            matrix[itemIndex, currentCapacity] = matrix[itemIndex - 1, currentCapacity];
                        }
                    }

                    Buffer.BlockCopy(matrix, 0, request.FullAnswer, 0, matrix.Length * sizeof(int));
                    request.Percentage = 100 / items.Length * itemIndex;
                    await requestRepository.Update(realR, request);
                    
                    Thread.Sleep(1500);
                }

                Buffer.BlockCopy(matrix, 0, request.FullAnswer, 0, matrix.Length * sizeof(int));
                request.Status = true;
                request.Percentage = 100;
                request.Answer = matrix[items.Length, request.Capacity];
                await requestRepository.Update(realR, request);
            }
        }
    }
}