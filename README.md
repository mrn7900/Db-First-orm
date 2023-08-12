# Hero API data provider
# An API that Implemented by ASP.net Core 6 (C#) can use by library aplications.
## there are search by id , search by name , create , update , delete and get all of datas of table is avalable in this app.
## search by id : this method firstly will search the id in databse , if there were the requested data it will return it else it will search in online API (Superhero API) if the requested data was available , it will save it in database , update the cache and return it.
## search by name :it will searches for record by name in db. 
## Get() : it will returns all records from db then it will update the cache.
## Post() : it will add entered record to db then it will update the cache.
## Update() :  it will update entered record to db then it will update the cache.
## Delete() : it will delete entered record from db then it will update the cache.
