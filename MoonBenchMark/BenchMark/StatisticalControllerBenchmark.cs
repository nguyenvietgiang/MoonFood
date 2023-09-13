using BenchmarkDotNet.Attributes;
using MoonBussiness.Repository;
using MoonDataAccess;
using MoonFood.Controllers.v1;

namespace MoonBenchMark.Benchmark
{
    [MemoryDiagnoser]
    public class StatisticalControllerBenchmark
    {
        private readonly StatisticalController _controller;
        private readonly StatisticalRepository _repository;

        public StatisticalControllerBenchmark()
        {
            var dbContext = new DataContext();
            _repository = new StatisticalRepository(dbContext);
            _controller = new StatisticalController(_repository);
        }

        [Benchmark]
        public void GetAccountStatisticsBenchmark()
        {
            // Thực hiện benchmark cho GetAccountStatistics
            _controller.GetAccountStatistics(DateTime.Now);
        }

        [Benchmark]
        public void GetOdersStatisticsBenchmark()
        {
            // Thực hiện benchmark cho GetOdersStatistics
            _controller.GetOdersStatistics(DateTime.Now);
        }
    }
}
