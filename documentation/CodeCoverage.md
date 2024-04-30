# Code Coverage

Run:

```shell
dotnet test --collect:"XPlat Code Coverage"
```

Then:

```shell
reportgenerator -reports:'./**/coverage.*.xml' -targetdir:"coveragereport" -reporttypes:Html
```

Now, view the coverage in the browser

Source: [learn.microsoft.com](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-code-coverage?tabs=linux)
