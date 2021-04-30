# Getting Started

## Install .NET

```
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin
echo 'export DOTNET_ROOT=$HOME/.dotnet' >> ~/.bashrc
echo 'export PATH=$PATH:$HOME/.dotnet' >> ~/.bashrc
source ~/.bashrc
```

## App starten

```
dotnet run --urls=http://0.0.0.0:44304/ --project [path to project]/Haushaltsbuch-Backend/WebApi/WebApi.csproj
```
