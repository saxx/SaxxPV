FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG VERSION=0.0.0.0
RUN dotnet tool install -g dotnet-setversion
ENV PATH="${PATH}:/root/.dotnet/tools"
COPY ./src/SaxxPv.Web /src/SaxxPv.Web
WORKDIR /src/SaxxPv.Web
RUN setversion $VERSION
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_HTTP_PORTS=80
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SaxxPv.Web.dll"]
