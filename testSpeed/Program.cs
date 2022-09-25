using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;

BenchmarkRunner.Run(typeof(Program).Assembly);


Console.WriteLine();


[MemoryDiagnoser]
public class Test
{

    [Benchmark]
    public void Chunk()
    {
        List<int> ls = new List<int>();
        var result = Enumerable.Range(0, 100000).Chunk(10);
        foreach (var collection in result)
        {
            foreach (var item in collection)
            {
                ls.Add(item);
            }
        }
    }

 
    [Benchmark]
    public void ChunkLazy()
    {
        List<int> ls = new List<int>();
        var result = EnumeratorExt.СhunkLazy(Enumerable.Range(0, 100000), 10);
        foreach (var collection in result)
        {
            foreach (var item in collection)
            {
                ls.Add(item);
            }
        }
    }
    [Benchmark]
    public void ChunkLazyNotMat()
    {
        List<int> ls = new List<int>();
        var result = Enumerable.Range(0, 100000).Chunk(10);
        foreach (var collection in result)
        {
            
        }
    }
    [Benchmark]
    public void ChunkNotMat()
    {
        List<int> ls = new List<int>();
        var result = EnumeratorExt.СhunkLazy(Enumerable.Range(0, 100000), 10);
        foreach (var collection in result)
        {
           
        }
    }
} 
     
static class EnumeratorExt
{
    public static IEnumerable<IEnumerable<T>> СhunkLazy<T>(this IEnumerable<T> enumerable, int size)
    {
        int count = 0;
        return enumerable.GroupBy(a =>
        {
            count++;
            if (count >= size)
                count = 0;
            return count;
        }).Select(a => a.AsEnumerable());
    }

}
