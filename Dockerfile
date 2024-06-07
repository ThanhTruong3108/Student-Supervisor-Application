FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["/CapstoneProject_FPT_Summer2024/Domain/Domain.csproj", "Domain/"]
COPY ["/CapstoneProject_FPT_Summer2024/Infrastructures/Infrastructures.csproj", "Infrastructures/"]
COPY ["/CapstoneProject_FPT_Summer2024/StudentSupervisorAPI/StudentSupervisorAPI.csproj", "StudentSupervisorAPI/"]
COPY ["/CapstoneProject_FPT_Summer2024/StudentSupervisorService/StudentSupervisorService.csproj", "StudentSupervisorService/"]
RUN dotnet restore "StudentSupervisorAPI/StudentSupervisorAPI.csproj"

COPY . .
WORKDIR "/src/CapstoneProject_FPT_Summer2024/StudentSupervisorAPI"
RUN dotnet build "StudentSupervisorAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StudentSupervisorAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudentSupervisorAPI.dll"]
