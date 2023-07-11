# movie-api
CRUD Movies API with C# .NET and SQL Database

### 1. Movie List
- Endpoint: ```/Movies```
- Method: ```GET```
- Description: return movie list in database in JSON format
- result example:
```
{
 {
 "id" : 1,
 "title" : "Pengabdi Setan 2 Comunion",
 "description" : "film horor Indonesia tahun 2022 yang disutradarai dan ditulis oleh Joko Anwar sebagai sekuel dari film tahun 2017, Pengabdi Setan.",
 "rating" : 7.0,
 "image" : "",
 "created_at" : "2022-08-01 10:56:31",
 "updated_at": "2022-08-13 09:30:23"
 },
 {
 "id" : 2,
 "title" : "Pengabdi Setan",
 "description" : "",
 "rating" : 8.0,
 "image" : "",
 "created_at" : "2022-08-01 10:56:31",
 "updated_at": "2022-08-13 09:30:23"
 }
}
```
### 2. Movie Details
- Endpoint: ```/Movies/{id}```
- Method: ```GET```
- Description: return movie details in JSON format
- result example: ```Movies/1``` will result:
```
{
 "id" : 1,
 "title" : "Pengabdi Setan 2 Comunion",
 "description" : "film horor Indonesia tahun 2022 yang disutradarai dan ditulis oleh Joko Anwar sebagai sekuel dari film tahun 2017, Pengabdi Setan.",
 "rating" : 7.0,
 "image" : "",
 "created_at" : "2022-08-01 10:56:31",
 "updated_at": "2022-08-13 09:30:23"
}
```

### 3. Add New Movie
- Endpoint: ```/Movies/```
- Method: ```POST```
- Description: add new movie to database, added movie will be posted as JSON
- result example:
```
{
 "id" : 3,
 "title" : "Pengabdi Setan",
 "description" : "film horor Indonesia tahun 2017 yang disutradarai dan ditulis oleh Joko Anwar",
 "rating" : 8.0,
 "image" : "",
 "created_at" : "2022-10-01 10:56:31",
 "updated_at": "2022-10-01 10:56:31"
}
```

### 4. Add New Movie
- Endpoint: ```/Movies/{id}```
- Method: ```PATCH```
- Description: edit movie in database, edited move will be posted as JSON
- result:
```
{
 "id" : 2,
 "title" : "Pengabdi Setan",
 "description" : "film horor Indonesia tahun 2017 yang disutradarai dan ditulis oleh Joko Anwar",
 "rating" : 8.0,
 "image" : "112.jpeg",
 "created_at" : "2022-08-01 10:56:31",
 "updated_at": "2022-10-01 10:56:31"
}
```

### 5. Delete Movie
- Endpoint: ```/Movies/{id}```
- Method: ```DELETE```
- Description: delete movie in database
