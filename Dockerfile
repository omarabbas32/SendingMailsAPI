# 1️⃣ استخدم صورة SDK عشان تبني المشروع
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# نسخ ملفات المشروع
COPY . ./

# استعادة الباكجات
RUN dotnet restore

# بناء المشروع في وضع Release
RUN dotnet publish -c Release -o /app

# 2️⃣ استخدم صورة Runtime خفيفة لتشغيل المشروع
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# نسخ الملفات من مرحلة البناء
COPY --from=build /app ./

# تحديد البورت اللي هيشتغل عليه
EXPOSE 8080


ENTRYPOINT ["dotnet", "SendingMailsAPI.dll"]
