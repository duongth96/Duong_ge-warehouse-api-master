* Test api trên local nhận kết quả thành công như sau:

** api đăng nhập:
	- api: http://localhost:37916/Authenticate/Login
	- method: POST
	- body: 
		{
			"username":"user01",
			"password":"1"
		}
	- response:
		{
			"id": 1,
			"username": "user01",
			"token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJuYmYiOjE2MjQ4ODczMTksImV4cCI6MTYyNTQ5MjExOSwiaWF0IjoxNjI0ODg3MzE5fQ.EmFQQeZ_cJukbxLiZ2wu6GYJpypK00BaI36LRPYPp4Y"
		}
	
** api danh sách kho:
 	- api: http://localhost:37916/Warehouse
	- method: GET
	- response:
		{
			"isSuccessful": true,
			"messages": [],
			"data": [
				{
					"id": 1,
					"name": "abc"
				},
				{
					"id": 2,
					"name": "dfe"
				}
			]
		}

** api nhập kho:
	- api: http://localhost:37916/Inventory
	- method: POST
	- body:
		{
			"WarehouseId":1,
			"Items":[
				{
					"Status":2,
					"QRCode":"4565748754545",
					"Quantity":1
				}
			]
		}
	- response:
		{
			"isSuccessful": true,
			"messages": []
		}