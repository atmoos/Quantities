﻿global using Quantities.Core;
using BenchmarkDotNet.Running;
using Quantities.Benchmark;

// dotnet run -c Release --project Quantities.Benchmark/
BenchmarkRunner.Run<CreateQuantities>();
