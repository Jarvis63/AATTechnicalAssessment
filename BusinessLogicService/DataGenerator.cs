using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

public class DataGenerator
{
    // A thread-safe collection that holds all generated numbers.
    private ConcurrentBag<int> _sharedBag = new();

    // Method to start data computation using multiple tasks.
    public async Task StartComputation()
    {
        // Start tasks for odd numbers, negated primes, and even numbers (if necessary).
        var oddTask = Task.Run(() => GenerateOddNumbers());
        var primeTask = Task.Run(() => GenerateNegatedPrimes());

        // Wait for both tasks to complete.
        await Task.WhenAll(oddTask, primeTask);

        // If the list size is sufficient, start generating even numbers.
        if (_sharedBag.Count >= 2_500_000)
        {
            var evenTask = Task.Run(() => GenerateEvenNumbers());
            await evenTask; // Wait for even number generation task to complete.
        }

        // Once all data is generated, sort the list.
        var sortedList = _sharedBag.OrderBy(x => x).ToList();
        _sharedBag = new ConcurrentBag<int>(sortedList);
    }

    private void GenerateOddNumbers()
    {
        // Logic for generating odd numbers.
        for (int i = 1; i <= 2_500_000; i += 2)
        {
            _sharedBag.Add(i);
        }
    }

    private void GenerateNegatedPrimes()
    {
        // Logic for generating negated prime numbers.
        for (int i = 2; i <= 2_500_000; i++)
        {
            if (IsPrime(i))
            {
                _sharedBag.Add(-i); // Negated primes
            }
        }
    }

    private void GenerateEvenNumbers()
    {
        // Logic for generating even numbers.
        for (int i = 2; i <= 2_500_000; i += 2)
        {
            _sharedBag.Add(i);
        }
    }

    // Helper method to check if a number is prime.
    private bool IsPrime(int number)
    {
        if (number <= 1) return false;
        for (int i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0) return false;
        }
        return true;
    }

    // Method to retrieve the computed list.
    public List<int> GetComputedList() => _sharedBag.ToList();
}
