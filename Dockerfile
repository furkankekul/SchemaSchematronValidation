# 1. Aşama: Uygulamayı Build Etme
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Proje dosyalarını kopyala ve bağımlılıkları yükle
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o /publish --no-restore

# 2. Aşama: Çalışma zamanı için hafif bir imaj kullanma
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Yayınlanan çıktıyı çalışma zamanına kopyala
COPY --from=build /publish .

# Çalıştırılacak komut
ENTRYPOINT ["dotnet", "C#BitirmeOdevi.dll"]