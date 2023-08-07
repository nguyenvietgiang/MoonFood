using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoonModels.DTO.ResponseDTO
{
    public class OrderStatistics
    {
        public decimal TotalSales { get; set; }
        public Guid MostFrequentUserId { get; set; }
        public Guid MostFrequentFoodId { get; set; }
    }
}
