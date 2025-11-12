FROM mcr.microsoft.com/dotnet/sdk:9.0 AS setversion
ARG VERSION=0.0.0.0
RUN dotnet tool install -g dotnet-setversion
ENV PATH="${PATH}:/root/.dotnet/tools"
COPY ./src/ /src/
WORKDIR /src/SaxxPv.Web
RUN setversion $VERSION

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
COPY --from=setversion /src/ /src/
WORKDIR /src/SaxxPv.Web
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_HTTP_PORTS=80
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SaxxPv.Web.dll"]
