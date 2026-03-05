# event-driven-dotnet

Event-driven systems laboratory in .NET.

## Purpose
Demonstrate production-grade asynchronous workflows:
- events
- background workers
- idempotency
- retries
- observability-ready design

## Target Architecture
API → Application → Domain → Infrastructure → Broker → Workers

## Getting Started
<bash>
dotnet restore
dotnet build -c Release
dotnet run --project EventDrivenDotNet.API