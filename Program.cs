using IPFSTest.Interface;
using IPFSTest.Model;
using IPFSTest.Services;


IIpfsService ipfsService = new IpfsService();
Person person = new Person();
person.FullName = "Rustamov Yusuf";
person.Age = 15;
person.Gender = "Male";
person.Id = 1;


Person person2 = new Person();
person2.FullName = "Rustam Yusufov";
person2.Age = 23;
person2.Gender = "Male";
person2.Id = 2;


List<Person> persons = new List<Person>();
persons.Add(person);
persons.Add(person2);


string? cid = await ipfsService.UploadFileAsync(persons, "person.json");




Console.WriteLine($"Файл загружен в IPFS. CID: {cid}");



await ipfsService.DownloadFileAsync(cid!,"C:\\Users\\VICTUS\\OneDrive\\Desktop\\PersonData.json");