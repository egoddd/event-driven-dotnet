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

## Current Architecture

- `EventDrivenDotNet.Contracts` — event contracts + event bus abstraction
- `EventDrivenDotNet.API` — publishes events (in-memory bus for now)
- `EventDrivenDotNet.Worker` — subscribes and handles events

## Run locally

Terminal 1 (API):
<bash>
dotnet run --project EventDrivenDotNet.API

## Getting Started
<bash>
dotnet restore
dotnet build -c Release
dotnet run --project EventDrivenDotNet.API