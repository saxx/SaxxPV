FROM mcr.microsoft.com/dotnet/sdk:6.0 AS version
ARG VERSION=0.0.0.0
RUN dotnet tool install -g dotnet-setversion
ENV PATH="${PATH}:/root/.dotnet/tools"
COPY ./src/SaxxPv.Web /src/SaxxPv.Web
WORKDIR /src/SaxxPv.Web
RUN setversion $VERSION

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
COPY --from=version /src/SaxxPv.Web /src/SaxxPv.Web
RUN dotnet publish "/src/SaxxPv.Web/SaxxPv.Web.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SaxxPv.Web.dll"]
