FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5110

ENV ASPNETCORE_URLS=http://+:5110

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["/MyMVCapp.csproj", "."]
RUN dotnet restore "MyMVCapp.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "MyMVCapp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyMVCapp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyMVCapp.dll"]
