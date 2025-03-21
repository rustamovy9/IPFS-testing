# IPFS File Storage in C#

Этот мини-проект демонстрирует, как загружать и скачивать файлы в IPFS с использованием C# и `HttpClient`.

## 📌 Описание

Проект включает сервис `IpfsService`, который позволяет:

- Загружать объекты C# в IPFS в формате JSON
- Получать CID (Content Identifier) загруженного файла
- Скачивать файлы из IPFS по CID через несколько шлюзов

## 🛠️ Технологии

- .NET 8
- C#
- IPFS
- HttpClient

## 📂 Структура проекта

IPFSTest/ │── Interface/ │ ├── IIpfsService.cs # Интерфейс IPFS сервиса │── Model/ │ ├── Person.cs # Модель данных │── Services/ │ ├── IpfsService.cs # Реализация загрузки и скачивания файлов │── Program.cs # Точка входа в приложение │── Const.cs # Константы для работы с IPFS
