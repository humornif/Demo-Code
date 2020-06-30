#!/usr/local/bin python3
# -*- coding: UTF-8 -*-

import pymongo
import datetime
from bson.objectid import ObjectId

database_connection_uri = "mongodb://localhost:27031/admin"

def main():

    client = pymongo.MongoClient(database_connection_uri)
    db = client["Test"]
    collection = db["TestCollection"]
    
    collection.create_index([("article_id", pymongo.ASCENDING)], background=True)
    # article1 = {
    #     "article_id": 1,
    #     "title": "文章标题1",
    #     "body": "文章内容1",
    #     "action_time": datetime.datetime.utcnow()
    # }
    # article2 = {
    #     "article_id": 2,
    #     "title": "文章标题2",
    #     "body": "文章内容2",
    #     "action_time": datetime.datetime.utcnow()
    # }
    # # result = collection.insert([article1, article2])
    # result = collection.insert_many([article1, article2])

    # print(result)
    # print(result.inserted_ids)

    #result = collection.find_one({"article_id": 1})
    #result = collection.find_one({'_id': ObjectId('5efb1ecc7ca7cd50ed1150ed')})

    #results = collection.find({"title": {"$ne":""}}).count()
    #results = collection.find({"title": {"$ne":""}}).sort("action_time", pymongo.DESCENDING).skip(1).limit(1)

    # filter = {"article_id": 1}
    # update = {"$set": {"body": "新的文章内容pp"}}
    # result = collection.update_many(filter, update)
    # print(type(result))
    # print(result)

    filter = {"article_id": 1}
    update = {"$set": {"body": "新的文章内容1"}}
    result = collection.find_one_and_update(filter, update)
    print(type(result))
    print(result)
    
        # for result in results:
    #     print(result)

    print("Success !!!")

if __name__ == '__main__':
    main()
