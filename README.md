
This is demo project of how to work with streams in .NET (some practical aspects)

# Notes
IAsyncEnumerable
 - Extend [MaxIAsyncEnumerableBufferLimit](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.mvcoptions.maxiasyncenumerablebufferlimit?view=aspnetcore-3.0)
 - .NET Core 3.1 buffers all collection in memory and then serialize full json before sending to client. 
 - In .NET 6 System.Text.Json was updated and now its possible to send a stream to client via IAsyncEnumerable responses.

Stream string from DB:
- We cannot stream single varchar(max) value. [SQL Client streaming support](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/sqlclient-streaming-support)
and [supported list](https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqldatareader.getstream?view=dotnet-plat-ext-5.0)

# Links
- [IAsyncEnumerable bufffering](https://docs.microsoft.com/en-us/dotnet/core/compatibility/aspnet-core/6.0/iasyncenumerable-not-buffered-by-mvc)
- [EF Core buffering and streaming](https://docs.microsoft.com/en-us/ef/core/performance/efficient-querying#:~:text=database%20roundtrip%20occurs.-,buffering%20and%20streaming,-Buffering%20refers%20to)

# Summary
 - Use IAsyncEnumerable starting from .NET 6 (Does not make sense to use it in .NET Core 3.1)
 - You cannot stream varchar(max) from MSSQL DB. 

