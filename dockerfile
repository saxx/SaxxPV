FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
COPY ./src/SaxxPv/SaxxPv.csproj /src/SaxxPv/
RUN dotnet restore "/src/SaxxPv/SaxxPv.csproj"
COPY ./src/SaxxPv /src/SaxxPv
WORKDIR "/src/SaxxPv"
RUN dotnet build "SaxxPv.csproj" -c Release -o /app/build
RUN dotnet publish "SaxxPv.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SaxxPv.dll"]
