# Модуль логирования

Модуль замены стандартного логгера на [Serilog](https://serilog.net/)

```sh
dotnet add AspNet.Module.Logging
```

## Конфигурация

Настройка логирования через appsettings.json или переменные окружения.

> Настройка [Serilog](https://github.com/serilog/serilog-settings-configuration) через appsettings.json

```json
{
  "Serilog": {
    "MinimumLevel": "Verbose",
    "ServiceName": "Название сервиса для Elastic",
    "Sinks": {
      "Elasticsearch": {
        "Url": "УРЛ к Elastic",
        "TemplateVersion": 7
      }
    }
  }
}
```

## Регистрация модуля

Добавляем в Host проект nuget пакет `AspNet.Module.Logging`.

```cs
using AspNet.Module.Logging;

var builder = AspNetWebApplication.CreateBuilder(args);
builder.RegisterModule<LoggingModule>();

...
var app = builder.Build();
...
await app.RunWithLogging();
```


