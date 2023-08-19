# Hero API data provider
## An API that Implemented by ASP.net Core 6 (C#) can use by library applications.
### there are search by id , search by name , create , update , delete and get all of datas of table is avalable in this app.
 Get by id : first it will search redis for cache if the cache was null ,it will search db,if there were requested record it will show it else it uses SuperHero API Service (online) for search. if there were data it will save it in db and cache then show it.
 search by name : it will searches for record by name in db and cache.
 Get() : it will returns all records from db.
 Post() : it will add entered record to db then it will update the cache.
 Update() :  it will update entered record to db then it will update the cache.
 Delete() : it will delete entered record from db then it will update the cache.
 Database = MySQL & Cache = Redis
