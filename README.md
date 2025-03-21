IPFS File Storage in C#

Этот мини-проект демонстрирует, как загружать и скачивать файлы в IPFS с использованием C# и HttpClient.

📌 Описание

Проект включает сервис IpfsService, который позволяет:

Загружать объекты C# в IPFS в формате JSON

Получать CID (Content Identifier) загруженного файла

Скачивать файлы из IPFS по CID через несколько шлюзов

🛠️ Технологии

.NET 8

C#

IPFS

HttpClient

📂 Структура проекта

IPFSTest/
│── Interface/
│   ├── IIpfsService.cs  # Интерфейс IPFS сервиса
│── Model/
│   ├── Person.cs  # Модель данных
│── Services/
│   ├── IpfsService.cs  # Реализация загрузки и скачивания файлов
│── Program.cs  # Точка входа в приложение
│── Const.cs  # Константы для работы с IPFS

🔧 Настройка и запуск

1. Запуск локального IPFS-демона

Перед использованием убедитесь, что у вас установлен IPFS и запущен демон:

ipfs daemon

2. Установка зависимостей

Убедитесь, что у вас установлен .NET 8 и запустите:

dotnet restore

3. Запуск проекта

dotnet run

📤 Загрузка файла в IPFS

Метод UploadFileAsync<T> сериализует объект в JSON, загружает его в IPFS и возвращает CID файла.

IIpfsService ipfsService = new IpfsService();
List<Person> persons = new()
{
    new Person { Id = 1, FullName = "Rustamov Yusuf", Age = 15, Gender = "Male" },
    new Person { Id = 2, FullName = "Rustam Yusufov", Age = 23, Gender = "Male" }
};
string? cid = await ipfsService.UploadFileAsync(persons, "person.json");
Console.WriteLine($"Файл загружен в IPFS. CID: {cid}");

📥 Скачивание файла из IPFS

Метод DownloadFileAsync получает файл по его CID и сохраняет его локально.

await ipfsService.DownloadFileAsync(cid!, "C:\\Users\\VICTUS\\OneDrive\\Desktop\\PersonData.json");

❓ Возможные ошибки и решения

Ошибка

Возможная причина

Решение

Not Found

Неверный CID или файл не реплицирован в сети

Проверьте, доступен ли файл в IPFS: ipfs cat <CID>

GatewayTimeout

Медленный доступ к шлюзам IPFS

Попробуйте другой шлюз или локальный узел

🔗 Полезные ссылки

IPFS Docs

IPFS API
