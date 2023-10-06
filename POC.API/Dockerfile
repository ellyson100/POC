FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY POC.API.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
WORKDIR /app
EXPOSE 80
RUN apk add --no-cache icu-libs icu-data-full
ENV ASPNETCORE_URLS=http://+:80
ENV DOTNET_SYSTEM_GLOBALIZTION_INVARIANT=false
COPY --from=build /app .
ENTRYPOINT ["dotnet", "POC.API.dll"]